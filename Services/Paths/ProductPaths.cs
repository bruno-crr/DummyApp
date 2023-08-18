using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Services.Paths
{
    public class ProductPaths
    {
        public static string Get = "https://dummyjson.com/products";

        public static string GetId = "https://dummyjson.com/products/{id}";
        public static string Post = "https://dummyjson.com/products/add";
    }

    public class ProductUrls
    {
        public string GetId(int productId)
        {
            return $"{ProductPaths.Get}/{productId}";
        }

        public string GetDescription(string term)
        {
            return $"https://dummyjson.com/products/search?q={term}";
        }

        public string Post()
        {
            return $"{ProductPaths.Post}";
        }

        public string Put(int idProduct)
        {
            return $"{ProductPaths.Get}/{idProduct}";
        }
    }
}
