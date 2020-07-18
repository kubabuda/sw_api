using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApi.FunctionalTests.Infrastructure
{
    public abstract class ABaseFunctionalTest
    {
        protected HttpContent GetJsonContent(object requestBody)
        {
            return new StringContent(JsonConvert.SerializeObject(requestBody),
                Encoding.UTF8, "application/json");
        }

        protected async Task<T> UnpackResponse<T>(HttpResponseMessage httpResponse)
        {
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}
