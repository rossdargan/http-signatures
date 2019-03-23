using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HttpSignatures.Client.Entities;

namespace HttpSignatures.Client.Services
{
    public class DigestGenerator : IDigestGenerator
    {
        private readonly string _hashingAlgorithmName;
        private readonly HashAlgorithm _hashingAlgorithm;
        public DigestGenerator(string hashingAlgorithmName)
        {
            string translatedHashName = hashingAlgorithmName.Replace("-","");

            _hashingAlgorithm = HashAlgorithm.Create(translatedHashName);
            if(_hashingAlgorithm==null)
            {
                throw new ArgumentException($"The passed in hashing algorithm ({hashingAlgorithmName}) was not valid.");
            }
            _hashingAlgorithmName = hashingAlgorithmName.ToUpper();
        }

        public DigestGenerator(ISignatureSpecification signatureSpecification) : this(signatureSpecification.HashAlgorithm)
        {
            
        }

        public string CalculateDigest(Stream data)
        {
            byte[] hash = _hashingAlgorithm.ComputeHash(data);
            string digest = System.Convert.ToBase64String(hash);
            return $"{_hashingAlgorithmName}={digest}";
        }
    }
}
