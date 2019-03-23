using HttpSignatures.Client.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSignatures.Client.Services
{
    public class SignatureStringGenerator : ISignatureGenerator
    {
        private readonly IHttpSignatureStringExtractor _httpSignatureStringExtractor;
        public SignatureStringGenerator(IHttpSignatureStringExtractor httpSignatureStringExtractor)
        {
            _httpSignatureStringExtractor = httpSignatureStringExtractor;
        }


        public string GenerateSignature(ISignatureSpecification signatureSpecification, IRequest request)
        {
            if (signatureSpecification == null)
            {
                throw new ArgumentNullException(nameof(signatureSpecification));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return "TODO";
            //StringBuilder signatureString = new StringBuilder();
            //signatureString.AppendLine($"{PseudoHeader}: {httpMethod.ToLower()} {path}");
            //foreach (var header in signatureSpecification.Headers)
            //{
            //    signatureString.AppendLine($"{}")
            //}
        }
    }
}
