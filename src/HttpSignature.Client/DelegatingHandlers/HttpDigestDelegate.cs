using HttpSignatures.Client.Services;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpSignatures.Client.DelegatingHandlers
{
    /// <summary>
    /// The http delegate is used to add a digest header to the request. 
    /// It will apply the requested hasing algorithm to the body of the request to be made and 
    /// add it as a header to the request pipeline.
    /// </summary>
    public class DigestDelegatingHandler : DelegatingHandler
    {
        private const string DigestHeaderKey = "digest";
        private readonly IDigestGenerator _digestGenerator;

        public DigestDelegatingHandler(IDigestGenerator digestGenerator)
        {
            _digestGenerator = digestGenerator;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string digest = _digestGenerator.CalculateDigest(await request.Content.ReadAsStreamAsync());
            request.Headers.Add(DigestHeaderKey, digest);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
