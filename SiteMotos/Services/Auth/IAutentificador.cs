using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using SiteMotos.Models;

namespace SiteMotos.Services.Auth
{
    public interface IAutentificador
    {
        Task<TokenViewModel> Autentificacao(UserAuthViewModel UsuarioVW);
    }
}
