using System.Reflection.Metadata.Ecma335;

namespace SiteMotos.Models
{
    public class TokenViewModel
    {
        public bool Autentificado { get; set; }
        public string? Token { get; set; }
        public string? FullName { get; set; }
    }
}
