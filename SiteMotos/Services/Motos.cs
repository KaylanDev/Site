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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<Motos> _logger;

        public Motos(IHttpClientFactory httpClientFactory, ILogger<Motos> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Models.Motos>> GetAll()
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            try
            {
                var moto = await client.GetFromJsonAsync<IEnumerable<Models.Motos>>($"https://localhost:7213/MotosM", new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return moto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados das motos");
            }

            return Enumerable.Empty<Models.Motos>();
        }
    }
}
