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
        string itefPrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIICXgIBAAKBgQDCFENGw33yGihy92pDjZQhl0C36rPJj+CvfSC8+q28hxA161QF
NUd13wuCTUcq0Qd2qsBe/2hFyc2DCJJg0h1L78+6Z4UMR7EOcpfdUE9Hf3m/hs+F
UR45uBJeDK1HSFHD8bHKD6kv8FPGfJTotc+2xjJwoYi+1hqp1fIekaxsyQIDAQAB
AoGBAJR8ZkCUvx5kzv+utdl7T5MnordT1TvoXXJGXK7ZZ+UuvMNUCdN2QPc4sBiA
QWvLw1cSKt5DsKZ8UETpYPy8pPYnnDEz2dDYiaew9+xEpubyeW2oH4Zx71wqBtOK
kqwrXa/pzdpiucRRjk6vE6YY7EBBs/g7uanVpGibOVAEsqH1AkEA7DkjVH28WDUg
f1nqvfn2Kj6CT7nIcE3jGJsZZ7zlZmBmHFDONMLUrXR/Zm3pR5m0tCmBqa5RK95u
412jt1dPIwJBANJT3v8pnkth48bQo/fKel6uEYyboRtA5/uHuHkZ6FQF7OUkGogc
mSJluOdc5t6hI1VsLn0QZEjQZMEOWr+wKSMCQQCC4kXJEsHAve77oP6HtG/IiEn7
kpyUXRNvFsDE0czpJJBvL/aRFUJxuRK91jhjC68sA7NsKMGg5OXb5I5Jj36xAkEA
gIT7aFOYBFwGgQAQkWNKLvySgKbAZRTeLBacpHMuQdl1DfdntvAyqpAZ0lY0RKmW
G6aFKaqQfOXKCyWoUiVknQJAXrlgySFci/2ueKlIE1QqIiLSZ8V8OlpFLRnb1pzI
7U1yQXnTAEFYM560yJlzUpOb1V4cScGd365tiSMvxLOvTA==
-----END RSA PRIVATE KEY-----";

        /// <summary>
        /// End to end test of https://teller.io/developer/documentation#section_tauth
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TellerIntegrationTest()
        {
            string privateKey = @"-----BEGIN RSA PRIVATE KEY-----
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
            Guid idempotencyToken = Guid.Empty;

            string paymentUri = "https://api.teller.io/accounts/00000000-0000-0000-0000-000000000000/payments";

            Uri requestUri = new Uri($"{paymentUri}/{idempotencyToken}");

            SignatureSpecification signatureSpecification = new SignatureSpecification()
            {
                Algorithm = "rsa-sha256",
                HashAlgorithm = "SHA-256",
                KeyId = "certificate",
                Headers = new List<string>
                {
                    "(request-target)",
                    "accept",
                    "content-type",
                    "date",
                    "digest",
                    "authorization",
                    "user-agent"
                },
                IncludePseduoHeaderInSigantureString = false,
                UseSignatureAsAuthroizationHeader = false
            };

            IStringSigner stringSigner = new RSAStringSigner(signatureSpecification, privateKey.ToRSAParameters());
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

        /// <summary>
        /// End to end test of https://tools.ietf.org/id/draft-cavage-http-signatures-08.html#appendix-c
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IetfIntegrationDefaultTest()
        {


           
            Uri requestUri = new Uri("https://example.com/foo?param=value&pet=dog");

            SignatureSpecification signatureSpecification = new SignatureSpecification()
            {
                Algorithm = "rsa-sha256",
                HashAlgorithm = "SHA-256",
                KeyId = "Test",                
                Headers = new []
                {
                    "(request-target)",
                    "host",
                    "date",
                    "content-type",
                    "digest",
                    "content-length"
                }
            };

            IStringSigner stringSigner = new RSAStringSigner(signatureSpecification, itefPrivateKey.ToRSAParameters());
            HttpSignatureStringExtractor httpSignatureExtractor = new HttpSignatureStringExtractor();
            ISignatureGenerator signatureGenerator = new SignatureGenerator(stringSigner, httpSignatureExtractor, signatureSpecification.KeyId);

            var httpSignatureHandler = new SignatureDelegatingHandler(signatureGenerator, signatureSpecification);
            var httpDigestDelegate = new DigestDelegatingHandler(new DigestGenerator(signatureSpecification));
            httpDigestDelegate.InnerHandler = httpSignatureHandler;
            httpSignatureHandler.InnerHandler = new Mocks.DelegatingHandlerMock();
            var invoker = new HttpMessageInvoker(httpDigestDelegate);

            HttpRequestMessage requestMessage = new HttpRequestMessage();

            var content = new StringContent("{\"hello\": \"world\"}");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentLength = 18;

            requestMessage.RequestUri = requestUri;
            requestMessage.Method = HttpMethod.Post;
            requestMessage.Content = content;
            requestMessage.Headers.TryAddWithoutValidation("Date", "Thu, 05 Jan 2014 21:31:40 GMT");            
            requestMessage.Headers.Add("Host", "example.com");

            var result = await invoker.SendAsync(requestMessage, CancellationToken.None);

            string expectedSignature = "keyId=\"Test\",algorithm=\"rsa-sha256\",headers=\"(request-target) host date content-type digest content-length\",signature=\"Ef7MlxLXoBovhil3AlyjtBwAL9g4TN3tibLj7uuNB3CROat/9KaeQ4hW2NiJ+pZ6HQEOx9vYZAyi+7cmIkmJszJCut5kQLAwuX+Ms/mUFvpKlSo9StS2bMXDBNjOh4Auj774GFj4gwjS+3NhFeoqyr/MuN6HsEnkvn6zdgfE2i0=\"";

            Assert.Equal(expectedSignature, requestMessage.Headers.Authorization.Parameter);

        }
    }
}
