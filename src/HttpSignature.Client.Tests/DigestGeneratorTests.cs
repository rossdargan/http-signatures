using HttpSignatures.Client.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HttpSignature.Client.Tests
{
    public class DigestGeneratorTests
    {
        /// <summary>
        /// This is the sample test given here https://teller.io/developer/documentation#section_tauth_request_signatures
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GivenKnownDataDigestIsCorrectlyCalculatedTellerTest()
        {
            // Arrange
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"bank_code","00-00-00" },
                {"account_number","00000000" },
                {"amount","0.01" }
            };

            var content = new FormUrlEncodedContent(data);
            DigestGenerator generator = new DigestGenerator("SHA256");

            // Act
            var result = generator.CalculateDigest(await content.ReadAsStreamAsync());

            // Assert
            Assert.Equal("SHA256=dX9LYG6i/d+TuzG0QMckFzqOZ6Wll/TlGGjUtqGyMhQ=", result);
        }

        /// <summary>
        /// This is the sample test given here https://tools.ietf.org/id/draft-cavage-http-signatures-08.html#auth-header
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GivenKnownDataDigestIsCorrectlyCalculatedIetfTest()
        {
            // Arrange
            var content = new StringContent("{\"hello\": \"world\"}");

            DigestGenerator generator = new DigestGenerator("SHA-256");

            // Act
            var result = generator.CalculateDigest(await content.ReadAsStreamAsync());

            // Assert
            Assert.Equal("SHA-256=X48E9qOokqqrvdts8nOJRJN3OWDUoyWxBf7kbu9DBPE=", result);
        }

        [Fact]
        public void GivenInvalidHashingAlgorithmThrowsException()
        {
            // Act
            Assert.Throws<ArgumentException>(() => new DigestGenerator("INVALID"));

        }
    }
}
