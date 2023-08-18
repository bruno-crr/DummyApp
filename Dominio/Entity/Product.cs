using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dominio.Entity
{ 
    public class Product
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("title")]  public string Title { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("price")] public decimal Price { get; set; }
        [JsonPropertyName("stock")] public decimal Stock { get; set; }
        [JsonPropertyName("brand")] public string Brand { get; set; }
        [JsonPropertyName("category")] public string Category { get; set; }
        [JsonPropertyName("thumbnail")] public string Thumbnail { get; set; }
        [JsonPropertyName("images")] public List<string> Images { get; set; }

        //0 - Deletado
        //1 - Ativo
        //2 - Bloqueado
        public int Status { get; set; }
    }
}
