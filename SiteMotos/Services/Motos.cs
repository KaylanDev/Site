using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using System.Security.AccessControl;
using System.Text.Json;
using System.Threading.Tasks;
using SiteMotos.Models;
using System.Text;

namespace SiteMotos.Services
{
    public class Motos : IMotos
    {
        private IEnumerable<MotosModelView> MotosVW { get; set; } = new List<MotosModelView>();
        private MotosModelView MotoVW { get; set; }

        private const string BaseUrl = "/MotosM";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        private readonly ILogger<Motos> _logger;

        public Motos(IHttpClientFactory httpClientFactory, ILogger<Motos> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
      
            };
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MotosModelView>> GetAll()
        {
            var client = _httpClientFactory.CreateClient("Motos");

            using (var response = await client.GetAsync(BaseUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    MotosVW = await JsonSerializer.DeserializeAsync<IEnumerable<MotosModelView>>(content, _options);

                }
                else
                {
                    _logger.LogError("Error fetching data from API");
                    return Enumerable.Empty<MotosModelView>();
                }
            }
            return MotosVW;
        }

        public async Task<MotosModelView> GetByIdAsync(int id)
        {

            var client = _httpClientFactory.CreateClient("Motos");

            using (var response = await client.GetAsync(BaseUrl +$"/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    MotoVW = await JsonSerializer.DeserializeAsync<MotosModelView>(content, _options);

                }
                else
                {
                    _logger.LogError("Error fetching data from API");
                    return null;
                }
            }
            return MotoVW;
        }

        public async Task<MotosModelView> PostAsync(MotosModelView moto)
        {
            var client = _httpClientFactory.CreateClient("Motos");

            var content = new StringContent(JsonSerializer.Serialize(moto), System.Text.Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(BaseUrl, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var motoResponse = JsonSerializer.Deserialize<MotosModelView>(result, _options);
                    return motoResponse;
                }
                else
                {
                    _logger.LogError("Error fetching data from API");
                    return null;
                }
            }
        }

        public async Task<bool> PutAsync(int id, MotosModelView moto)
        {
            var client = _httpClientFactory.CreateClient("Motos");
            

            using (var response = await client.PutAsJsonAsync(BaseUrl + $"/{id}",moto))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                    
                }
                _logger.LogError("n foi possivel alterar a entidade");
                return false;
            }

        }
    }
}
