using System.Text.Json.Serialization;

namespace SiteMotos.Models
{
    public class MotosModelView
    {
    
        public int Id { get; set; }
        public string? Modelo { get; set; }
        public int Potencia { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public string? Imagem { get; set; }
    }
}
