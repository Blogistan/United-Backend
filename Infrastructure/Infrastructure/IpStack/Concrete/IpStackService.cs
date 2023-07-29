using Infrastructure.IpStack.Abstract;
using Infrastructure.IpStack.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Infrastructure.IpStack.Concrete
{
    public class IpStackService:IIpStackService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IpStackConfiguration ipStackConfiguration;

        public IpStackService(IConfiguration configuration, HttpClient httpClient)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
            this.ipStackConfiguration = configuration.GetSection("IpStackConfiguration").Get<IpStackConfiguration>() ?? throw new NullReferenceException();
        }

        public async Task<IPInfo> GetClientIpInfo(string IpAddress)
        {
            HttpResponseMessage httpResponse = httpClient.GetAsync($"{ipStackConfiguration.ApiUrl}{IpAddress}?access_key={ipStackConfiguration.AccessKey}").Result;

            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<IPInfo>(responseBody);

            return result;
        }
    }
}
