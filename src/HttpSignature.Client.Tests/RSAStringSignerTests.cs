using HttpSignatures.Client.Entities;
using HttpSignatures.Client.Services;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO.Pem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit;

namespace HttpSignature.Client.Tests
{
    public class RSAStringSignerTests
    {
        [Fact]
        public void GivenKnownStringCorrectlyCalculatesHash()
        {
            // Arrange
            string input = "(request-target): put /accounts/00000000-0000-0000-0000-000000000000/payments/00000000-0000-0000-0000-000000000000\naccept: application/json\ncontent-type: application/x-www-form-urlencoded\ndate: Mon, 09 Apr 2018 15:24:16 GMT\ndigest: SHA-256=dX9LYG6i/d+TuzG0QMckFzqOZ6Wll/TlGGjUtqGyMhQ=\nauthorization: Bearer XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\nuser-agent: Super Rad App/0.0.1";
            ISignatureSpecification signatureSpecification = new SignatureSpecification()
            {
                Algorithm = "SHA256"
            };

            IStringSigner stringSigner = new RSAStringSigner(signatureSpecification, "".ToRSAParameters());// StringExtension.PrivateKey);

            string knownResult = "g0nWC3Q4EnzX+gYhbxG38dhyVjlewsgA7xJjrllYc3GVuiYEE0KLEugQ2JhbeJK2zK2FtL1476wsS0QmTd+V+HOs0jdIy+aAmBIOAx6urIvsRYTmgOzUMnPfLDTtda1PYvUkwQMMZcZ2jmVN3lPs4ZWuGc9HwWfBlTtrhdjXPdyvMG8SgfTjM11MLl7b8UYURB4aWA7FG7aaoEGV3d8F5QY85pwgMtzSsRcpZL9cGIh8zr7p79fjeJ0M+arD/5geaATJRLsFovmpBFcdZKve44muLNCNmTO+Uu18sHhX5zi+mkINw4G1wxNXbyrPRaFPhcN9o6ZDlMPgtAogvyqbSQ==";
            // Act
            string result =  stringSigner.Sign(input);

            // Assert
            Assert.Equal(knownResult, result);
        }
    }
}
