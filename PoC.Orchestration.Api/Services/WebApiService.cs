using System.Net.Http.Headers;
using System.Text;

namespace PoC.Orchestration.Api.Services
{
    public class WebApiService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<string> PostAsync(string url, string? stringContent = null, string contentType = "application/json", IDictionary<string, string>? additionalHeaders = null)
        {
            try
            {
                HttpContent? content = null;

                if (!String.IsNullOrEmpty(stringContent))
                    content = new StringContent(stringContent, Encoding.UTF8, "application/json");

                if(additionalHeaders != null && content != null)
                    foreach(var header  in additionalHeaders)
                        content!.Headers.Add(header.Key, header.Value);

                var response = await this.httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string responseMessage = await response.Content.ReadAsStringAsync();

                return responseMessage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string?> SendMessageAsync(HttpMethod method, string url, string? stringContent = null, string contentType = "application/json", IDictionary<string, string>? additionalHeaders = null)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(method, url)
                {
                    Headers =
                    {
                        { "Accept", contentType }
                    }
                };

                if (additionalHeaders != null)
                    foreach (var header in additionalHeaders)
                        httpRequest.Headers.Add(header.Key, header.Value);

                if (!String.IsNullOrEmpty(stringContent))
                {
                    httpRequest.Content = new StringContent(contentType);
                    httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }

                var response = await this.httpClient.SendAsync(httpRequest);
                response.EnsureSuccessStatusCode();

                string responseMessage = await response.Content.ReadAsStringAsync();

                return responseMessage;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
