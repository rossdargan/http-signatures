using HttpSignatures.Client.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace HttpSignatures.Client.Services
{
    public class RSAStringSigner : IStringSigner
    {
        private readonly ISignatureSpecification _signatureSpecification;
        private readonly RSAParameters _rsaParameters;

        public RSAStringSigner(ISignatureSpecification signatureSpecification, RSAParameters rsaParameters)
        {
            _signatureSpecification = signatureSpecification;
            _rsaParameters = rsaParameters;
        }
        public string Sign(string input)
        {
            using (var algorithm = RSA.Create())
            {
                algorithm.ImportParameters(_rsaParameters);
                byte[] data = ASCIIEncoding.Default.GetBytes(input);                

                var signature = algorithm.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(signature);
            }
        }
    }
}
