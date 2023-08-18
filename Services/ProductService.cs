using Dominio.Entity;
using Newtonsoft.Json;
using Services.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IDisposable
    {
        private bool disposedValue;

        public ProductService()
        {
            Urls = new ProductUrls();
        }
        public ProductUrls Urls { get; }
        public async Task<ProductsResponse> GetAsync()
        {
            var httpClient = new HttpClient();
            using (var response = await httpClient.GetAsync(ProductPaths.Get))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.NoContent
                    ? null
                    : JsonConvert.DeserializeObject<ProductsResponse>(responseBody);
            }
        }
        public async Task<ProductsResponse> GetDescriptionAsync(string term)
        {
            var httpClient = new HttpClient();
            using (var response = await httpClient.GetAsync(Urls.GetDescription(term)))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.NoContent
                    ? null
                    : JsonConvert.DeserializeObject<ProductsResponse>(responseBody);
            }
        }

        public async Task<Product> GetIdAsync(int id)
        {
            var httpClient = new HttpClient();
            using (var response = await httpClient.GetAsync(Urls.GetId(id)))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.NoContent
                    ? null
                    : JsonConvert.DeserializeObject<Product>(responseBody);
            }
        }

        public async Task<Product> PostAsyc(Product product)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(product);

            using (var response = await httpClient.PostAsync(Urls.Post(),
                new StringContent(json, Encoding.UTF8, "application/json")))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.NoContent
                    ? null
                    : JsonConvert.DeserializeObject<Product>(responseBody);
            }
        }
        public async Task<Product> PutAsyc(Product product)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(product);

            using (var response = await httpClient.PutAsync(Urls.Put(product.Id),
                new StringContent(json, Encoding.UTF8, "application/json")))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.NoContent
                    ? null
                    : JsonConvert.DeserializeObject<Product>(responseBody);
            }
        }

        public async Task<Product> DeleteAsyc(Product product)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(product);

            using (var response = await httpClient.DeleteAsync(Urls.Put(product.Id)))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.NoContent
                    ? null
                    : JsonConvert.DeserializeObject<Product>(responseBody);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ProductService()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
