using HttpSignatures.Client.Entities;
using HttpSignatures.Client.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HttpSignature.Client.Tests
{
    public class HttpSignatureStringExtractorTests
    {
        [Fact]
        public void GivenSameHeaderStringIncludeBothInSignature()
        {
            // Arrange
            IHttpSignatureStringExtractor signatureStringExtractor = new HttpSignatureStringExtractor();
            Request request = new Request();

            request.SetHeader("test", new[] { "value1", "value2" });

            ISignatureSpecification specification = new SignatureSpecification()
            {                
                Headers = new[] { "test" }
            };
            // Act
            string result = signatureStringExtractor.ExtractSignatureString(request, specification);

            // Assert
            Assert.Equal("test: value1\ntest: value2", result);
        }

        [Fact]
        public void IfHeaderNotInSpecificationDontIncludeInOutput()
        {
            // Arrange
            IHttpSignatureStringExtractor signatureStringExtractor = new HttpSignatureStringExtractor();
            Request request = new Request();

            request.SetHeader("test", new[] { "value1", "value2" });
            request.SetHeader("ignoredheader", new[] { "value1", "value2" });

            ISignatureSpecification specification = new SignatureSpecification()
            {
                Headers = new[] { "test" }
            };
            // Act
            string result = signatureStringExtractor.ExtractSignatureString(request, specification);

            // Assert
            Assert.Equal("test: value1\ntest: value2", result);
        }

        [Fact]
        public void CorrectlyGeneratesRequestTarget()
        {
            // Arrange
            IHttpSignatureStringExtractor signatureStringExtractor = new HttpSignatureStringExtractor();
            Request request = new Request();

            request.Method = "GET";
            request.Path = "/test";
            
                

            ISignatureSpecification specification = new SignatureSpecification()
            {
                Headers = new[] { "(request-target)" }
            };
            // Act
            string result = signatureStringExtractor.ExtractSignatureString(request, specification);

            // Assert
            Assert.Equal("(request-target): get /test", result);
        }


    }
}
