using HttpSignatures.Client.DelegatingHandlers;
using HttpSignatures.Client.Entities;
using HttpSignatures.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HttpSignature.Client.Tests
{
    public class IntegrationTest
    {

        /// <summary>
        /// End to end test of https://teller.io/developer/documentation#section_tauth
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CompleteIntegrationTest()
        {
            Guid idempotencyToken = Guid.Empty;

            string paymentUri = "https://api.teller.io/accounts/00000000-0000-0000-0000-000000000000/payments";

            Uri requestUri = new Uri($"{paymentUri}/{idempotencyToken}");

            SignatureSpecification signatureSpecification = new SignatureSpecification()
            {
                Algorithm = "rsa-sha256",
                HashAlgorithm = "SHA-256",
                KeyId = "certificate",
                SignatureHeaderName = "signature",
                Headers = new List<string>
                {
                    "(request-target)",
                    "accept",
                    "content-type",
                    "date",
                    "digest",
                    "authorization",
                    "user-agent"
                }
            };

            IStringSigner stringSigner = new RSAStringSigner(signatureSpecification, Fixture.PrivateKey);
            HttpSignatureStringExtractor httpSignatureExtractor = new HttpSignatureStringExtractor()
            {
                HeaderSeperationString = " "
            };
            ISignatureGenerator signatureGenerator = new SignatureGenerator(stringSigner, httpSignatureExtractor, signatureSpecification.KeyId);

            var httpSignatureHandler = new SignatureDelegatingHandler(signatureGenerator, signatureSpecification);
            var httpDigestDelegate = new DigestDelegatingHandler(new DigestGenerator(signatureSpecification));
            httpDigestDelegate.InnerHandler = httpSignatureHandler;
            httpSignatureHandler.InnerHandler = new Mocks.DelegatingHandlerMock();
            var invoker = new HttpMessageInvoker(httpDigestDelegate);

            HttpRequestMessage requestMessage = new HttpRequestMessage();
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"bank_code","00-00-00" },
                {"account_number","00000000" },
                {"amount","0.01" }
            };

            var content = new FormUrlEncodedContent(data);
            requestMessage.RequestUri = requestUri;
            requestMessage.Method = HttpMethod.Put;
            requestMessage.Content = content;
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            requestMessage.Headers.Add("Date", "Mon, 09 Apr 2018 15:24:16 GMT");
            requestMessage.Headers.Add("accept", "application/json");            
            requestMessage.Headers.UserAgent.ParseAdd("Super Rad App/0.0.1" );
            var result = await invoker.SendAsync(requestMessage, CancellationToken.None);

            string expectedSignature = "keyId=\"certificate\",algorithm=\"rsa-sha256\",headers=\"accept content-type date digest authorization user-agent\",signature=\"g0nWC3Q4EnzX+gYhbxG38dhyVjlewsgA7xJjrllYc3GVuiYEE0KLEugQ2JhbeJK2zK2FtL1476wsS0QmTd+V+HOs0jdIy+aAmBIOAx6urIvsRYTmgOzUMnPfLDTtda1PYvUkwQMMZcZ2jmVN3lPs4ZWuGc9HwWfBlTtrhdjXPdyvMG8SgfTjM11MLl7b8UYURB4aWA7FG7aaoEGV3d8F5QY85pwgMtzSsRcpZL9cGIh8zr7p79fjeJ0M+arD/5geaATJRLsFovmpBFcdZKve44muLNCNmTO+Uu18sHhX5zi+mkINw4G1wxNXbyrPRaFPhcN9o6ZDlMPgtAogvyqbSQ==\"";

            Assert.Contains(requestMessage.Headers, header=>header.Key=="signature" && header.Value.First()== expectedSignature);

        }
    }
}
