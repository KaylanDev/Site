using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using System.Security.AccessControl;
using System.Text.Json;
using System.Threading.Tasks;
using SiteMotos.Models;

namespace SiteMotos.Services
{
    public class Motos : IMotos
    {
        private IEnumerable<MotosModelView> MotosVW { get; set; } = new List<MotosModelView>();
        private Motos MotoVW { get; set; }

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

        public Task<MotosModelView> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MotosModelView> PostAsync(MotosModelView moto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutAsync(int id, MotosModelView moto)
        {
            throw new NotImplementedException();
        }
    }
}
