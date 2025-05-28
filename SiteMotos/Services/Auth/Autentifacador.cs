using SiteMotos.Models;
using SiteMotos.Services.Motos;
using System.Text.Json;

namespace SiteMotos.Services.Auth
{
    public class Autentifacador : IAutentificador
    {
        public TokenViewModel token { get; set; } = new TokenViewModel();
        private const string UrlLogin = "/auth/login";
        private const string UrlRegister = "/Auth/register";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        private readonly ILogger<MotosService> _logger;

        public Autentifacador(IHttpClientFactory httpClientFactory, ILogger<MotosService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            _logger = logger;
        }

        public async Task<TokenViewModel> Autentificacao(UserAuthViewModel UsuarioVW)
        {
            var client = _httpClientFactory.CreateClient("Auth");
            var content = new StringContent(
                JsonSerializer.Serialize(UsuarioVW,_options),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            using (var response = await client.PostAsync(UrlLogin,content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiresponse = await response.Content.ReadAsStreamAsync();

                    token = await JsonSerializer.DeserializeAsync<TokenViewModel>(apiresponse, _options);

                }else return null;
            }
            return token;
        }
    }
}
