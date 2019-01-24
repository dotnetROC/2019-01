using System;
using System.Net;
using System.Net.Http;
using HttpClientExamples.Tests.Helpers;

namespace HttpClientExamples.Tests.DataFactories
{
    public abstract class HttpClientFactory
    {
        public static HttpClient CreateHttpClient(HttpStatusCode statusCode, object response) =>
            new HttpClient(
                handler: new FakeResponseDelegationHandler(statusCode, response)
            ) {
                BaseAddress = new Uri("http://fake-address.com")
            };
    }
}