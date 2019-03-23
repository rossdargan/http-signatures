using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HttpSignatures.Client.Entities;
using HttpSignatures.Client.Services;

namespace HttpSignatures.Client.DelegatingHandlers
{
    public class HttpSignatureHandler : DelegatingHandler
    {
        private readonly ISignatureGenerator _signatureGenerator;
        private readonly ISignatureSpecification _signatureSpecification;

        public HttpSignatureHandler(ISignatureGenerator signatureGenerator, ISignatureSpecification signatureSpecification)
        {
            _signatureGenerator = signatureGenerator;
            _signatureSpecification = signatureSpecification;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var signature =_signatureGenerator.GenerateSignature(Request.FromHttpRequest(request), _signatureSpecification);
            request.Headers.Add(_signatureSpecification.SignatureHeaderName, signature);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
