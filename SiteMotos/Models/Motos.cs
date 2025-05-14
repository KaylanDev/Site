using System.Text.Json.Serialization;

namespace SiteMotos.Models
{
    public class Motos
    {
        [JsonConstructor]
        public Motos(int id, string? modelo, int potencia, decimal preco, string marca, string? imagem)
        {
            Id = id;
            Modelo = modelo;
            Potencia = potencia;
            Preco = preco;
            Marca = marca;
            Imagem = imagem;
        }

        public int Id { get; set; }
        public string? Modelo { get; set; }
        public int Potencia { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public string? Imagem { get; set; }
    }
}
