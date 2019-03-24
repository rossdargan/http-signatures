using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HttpSignature.Client.Tests
{
    public static class StringExtension
    {
        public static RSAParameters ToRSAParameters(this string privateKey)
        {
            var reader = new Org.BouncyCastle.OpenSsl.PemReader(new StringReader(privateKey));
            AsymmetricCipherKeyPair KeyPair = (AsymmetricCipherKeyPair)reader.ReadObject();

            RSAParameters rsa = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);
            return rsa;
        }
    }
}
