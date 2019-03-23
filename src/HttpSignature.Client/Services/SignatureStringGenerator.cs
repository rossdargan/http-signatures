using HttpSignatures.Client.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace HttpSignatures.Client.Services
{
    public class SignatureStringGenerator : ISignatureGenerator
    {
        private readonly IHttpSignatureStringExtractor _httpSignatureStringExtractor;
        private readonly string _key;

        public SignatureStringGenerator(IHttpSignatureStringExtractor httpSignatureStringExtractor, string key)
        {
            _httpSignatureStringExtractor = httpSignatureStringExtractor;
            _key = key;
        }


        public string GenerateSignature(IRequest request, ISignatureSpecification signatureSpecification)
        {
            if (signatureSpecification == null)
            {
                throw new ArgumentNullException(nameof(signatureSpecification));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string signature = CalculateSignature(request, signatureSpecification, _key);

            string authorizationHeader =  FormatAuthorization(signatureSpecification, signature);
            return authorizationHeader;
        }

        private string CalculateSignature(IRequest r, ISignatureSpecification spec, string key)
        {
            var algorithm = spec.Algorithm;
            var signatureString = _httpSignatureStringExtractor.ExtractSignatureString(r, spec);
            // TODO: Need to implement a ec/RSA generator https://tools.ietf.org/id/draft-cavage-http-signatures-01.html#rsa-example
           var alg= AsymmetricAlgorithm.Create("");
            RSA.Create("").
            var hmac = KeyedHashAlgorithm.Create(algorithm.Replace("-", "").ToUpper());
            hmac.Initialize();
            hmac.Key = Convert.FromBase64String(key);
            var bytes = hmac.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(signatureString)));
            var signature = Convert.ToBase64String(bytes);
            return signature;
        }

        private string FormatAuthorization(ISignatureSpecification spec, string signature)
        {
            return
                $"Signature keyId=\"{spec.KeyId}\",algorithm=\"{spec.Algorithm}\",headers=\"{string.Join(" ", spec.Headers)}\",signature=\"{signature}\"";
        }
    }
}
