using HttpSignatures.Client.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace HttpSignatures.Client.Services
{
    public class SignatureGenerator : ISignatureGenerator
    {
        private readonly IStringSigner _stringSigner;
        private readonly IHttpSignatureStringExtractor _httpSignatureStringExtractor;
        private readonly string _key;

        public SignatureGenerator(IStringSigner stringSigner, IHttpSignatureStringExtractor httpSignatureStringExtractor, string key)
        {
            _stringSigner = stringSigner;
            _httpSignatureStringExtractor = httpSignatureStringExtractor;
            _key = key;
        }


        public string GenerateSignature(IRequest request, ISignatureSpecification signatureSpecification)
        {
            var signatureString = _httpSignatureStringExtractor.ExtractSignatureString(request, signatureSpecification);
            var signedString = _stringSigner.Sign(signatureString);

            string authorizationHeader =  FormatAuthorization(signatureSpecification, signedString);
            return authorizationHeader;
        }

        private string FormatAuthorization(ISignatureSpecification spec, string signature)
        {
            string listOfHeaders = string.Join(" ", spec.Headers);
            if (!spec.IncludePseduoHeaderInSigantureString)
            {
                listOfHeaders = listOfHeaders.Replace("(request-target) ", "");
            }

            return
                $"keyId=\"{spec.KeyId}\",algorithm=\"{spec.Algorithm}\",headers=\"{listOfHeaders}\",signature=\"{signature}\"";
        }
    }
}
