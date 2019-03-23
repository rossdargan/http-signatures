using HttpSignature.Client.Tests.Mocks;
using HttpSignatures.Client.DelegatingHandlers;
using HttpSignatures.Client.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HttpSignature.Client.Tests
{
    public class HttpDigestDelegateTests
    {
        Moq.Mock<IDigestGenerator> _digestGeneratorMock;
        HttpDigestDelegate _httpDigestDelegate;
        public HttpDigestDelegateTests()
        {
            _digestGeneratorMock = new Moq.Mock<IDigestGenerator>();
            _httpDigestDelegate = new HttpDigestDelegate(_digestGeneratorMock.Object);
            _httpDigestDelegate.InnerHandler = new DelegatingHandlerMock();
        }
        [Fact]
        public async Task CorrectlyAppliesReturnedDigestAsHeader()
        {
            // Arrange
            string digest = "TEST";
            _digestGeneratorMock.Setup(p => p.CalculateDigest(It.IsAny<Stream>())).Returns(digest);
            var invoker = new HttpMessageInvoker(_httpDigestDelegate);

            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Content = new StringContent("");            
            // Act
            var result = await invoker.SendAsync(requestMessage, CancellationToken.None);

            // Assert
            Assert.Contains(requestMessage.Headers, p=>p.Value.Any(q=> q ==digest));

        }
    }
}
