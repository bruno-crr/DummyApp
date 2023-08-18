using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dominio.Entity
{
    public class ProductsResponse
    {
        [JsonPropertyName("products")] public List<Product> Products { get; set; }
        [JsonPropertyName("total")]  public int Total { get; set; }
        [JsonPropertyName("skip")]  public int Skip { get; set; }
        [JsonPropertyName("limit")]  public int Limit { get; set; }
    }
}
