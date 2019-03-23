using HttpSignatures.Client.Services;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpSignatures.Client.DelegatingHandlers
{
    public class HttpDigestDelegate : DelegatingHandler
    {
        private const string DigestHeaderKey = "digest";
        private readonly IDigestGenerator _digestGenerator;

        public HttpDigestDelegate(IDigestGenerator digestGenerator)
        {
            _digestGenerator = digestGenerator;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string digest = await _digestGenerator.CalculateDigest(await request.Content.ReadAsStreamAsync());
            request.Headers.Add(DigestHeaderKey, digest);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
