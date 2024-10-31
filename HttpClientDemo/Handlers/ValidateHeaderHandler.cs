
namespace HttpClientDemo.Handlers
{
    public class ValidateHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if(!request.Headers.Contains("Accept"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("The API Key Accept is required")
                };
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
