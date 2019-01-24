using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpClientExamples.Tests.Helpers {
    public class FakeResponseDelegationHandler : DelegatingHandler
    {
        private HttpStatusCode _statusCode;
        private object _response;
        private static string MIME_TYPE = "application/json";
        public FakeResponseDelegationHandler(HttpStatusCode statusCode, object response)
        {
            _statusCode = statusCode;
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            Task.FromResult(
                new HttpResponseMessage(_statusCode)
                {
                    Content = new StringContent(
                        JsonConvert.SerializeObject(_response),
                        Encoding.UTF8,
                        MIME_TYPE
                    )
                }
            );
    }
}