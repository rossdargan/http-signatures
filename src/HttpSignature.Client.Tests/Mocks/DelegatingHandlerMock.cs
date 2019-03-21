using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpSignature.Client.Tests.Mocks
{
    class DelegatingHandlerMock : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK), cancellationToken);
        }
    }
}
