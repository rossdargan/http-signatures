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
    class Fixture
    {
        private static string privateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAunDvDG5JeWU5qaI0evW3Pf+NYJu3xjmet+EYitnSLSC3tVfu
SttzpmiO00B1AZ3S7r9D02E86I850icPLICHoApbvtvQtmH5+YmVAYlSJoyFEDyd
i8ag0KnY3k9v0H3LhUYd4B6/0YwGF44u/Fv9j07fJ1TjF1jSw+QIR6+gpXIhKYV+
bd3wJKZhT8HpYXKDUKKFDXwelZBCdT38r6KcnrjrotsihO+SPOza21sQcHbXucqm
dCc5F84CwhFhBjTmAGX4oirxbzA76Qmd6/nM2KePFzZFW2KQcyWIvVSC1ILcgjsH
ayq0dih4fJBJnBOWw4Ue1dCkHa4ZqT8id4z+8QIDAQABAoIBAQC4AYfcg+ieGCp9
4inbhTERzmsRAv6wc+PS5STcvTfy9Ax7vMnhNY+BmEYF9uLD8qjgmwJs19ZOTDUr
1QLj0AKLEE4WI0ptBmu5PXFDb+0VQLB/IuP2tNW4uzjfyBS5971qzpXjwVVdkc3d
Z6W1yXCKBLS3U89BFpgFX8RQQ+TQAxFSOqsm+XyyNKAdWtArYXEW2SAWkE3doqL9
vSOMrralS0ForSW8Bh5KzMmiAl/dg3u+yma1o/uK9Uo2ko4ZWRROpuQ6rESEMFR+
WuXWSGJRZwbx1/cd4+Yr/Qlf81OHb2PVEL+2CW74/sd1WTAEddPCK9rOw4lwjr0R
9UqrzD9BAoGBAORF7XzeMzdBiIIG9K1JEumjFzXHAPYcWbd4xQpC2nzQSXNTzaw4
+gjFWaNJVjSyIgDF5+O6dmfensst8Hwj5sfmjRsx7jLslXCpBs/zVUL76+7Meo6b
/fproUX0213tapBvGxbWXC8zd+WDffWsDpYJ/Y8FfB7FUaUe8IvkaVAJAoGBANEW
QTdQjUnpydVJbmjeuG/owNgi4uJ5fPWJ92Jq/+jKsA1MuiOZl/ioAugqBICuTvod
CxLdTQ7cGX7cVkqM8+AM4FiK0gfqT05zhG+6tXfNodjYvxsdwLer/V3lCNVSjEEV
99rttEecXE8WNwQavdfX/rK7VQvlpigQMF8GhiGpAoGBAL4y/bRDOA1cTy94ODqC
Xn3JZDdsvwJRkPdsa1EnbwD8U5cCRqavOrZAKXYCTw/NNMPMInD/FlVpionklzH4
f/wjv4LfUYeg1MtwKrruFyae3XC1c5CLrU0QjOnLIVTb0yTRTpLyvRCfI9FahINE
f0rIvDqE6WyCIIsigm8tPApxAoGATHkZTUP3CUuq+ImtCko7pyK4NdU8qpzIqX3W
r3Z3Nwu7LIIdqpuoy9eXiJalovMeC7jHrhSm9IJoCNBJ92ZqZE4RWBEeFKsMaqMx
kzP032akhY1xCFfvfr43Izp76poQllWUm8xJHdAAqyRy5ttpCCMGExUVXA6YoRqa
tobKVFECgYBjppJEgar+LXWloOfFEMJhPCS7Cfa37IVOCnRoSORoZTmvonPQAxS1
yoXQN/1XxDMHBp/rLdMJ5jQiW0AB3b+JV5G9pMsYP7wpRpaL/zJoDBD7IfVlV9Tr
AiTxCpr35nV2PPYaFYwE+mfeT5jFZnhZuRyTlX9cfYx73rukFMrSmg==
-----END RSA PRIVATE KEY-----";

        public static RSAParameters PrivateKey
        {
            get
            {
                var reader = new Org.BouncyCastle.OpenSsl.PemReader(new StringReader(privateKey));
                AsymmetricCipherKeyPair KeyPair = (AsymmetricCipherKeyPair)reader.ReadObject();

                RSAParameters rsa = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);
                return rsa;
            }
        }
    }
}
