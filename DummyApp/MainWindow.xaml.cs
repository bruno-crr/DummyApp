using Dominio.Entity;
using Dominio.Validation;
using FluentValidation;
using Services;
using Services.Paths;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wrappers;

namespace DummyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, System.ComponentModel.INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            SearchTerm = "Iphone";
        }

        public ObservableCollection<ProductWrapper> Products { get; set; } = new ObservableCollection<ProductWrapper>();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string searchTerm;

        public string SearchTerm
        {
            get { return searchTerm; }
            set { searchTerm = value; NotifyPropertyChanged(); }
        }

        private Visibility paneAddVisibility = Visibility.Hidden;

        public Visibility PaneAddVisibility
        {
            get { return paneAddVisibility; }
            set { paneAddVisibility = value; NotifyPropertyChanged(); }
        }

        private ProductWrapper product;

        public ProductWrapper Product
        {
            get { return product; }
            set { product = value; NotifyPropertyChanged(); }
        }

        private async void BtnGetAll_Click(object sender, RoutedEventArgs e)
        {
            await Search();
        }

        private async Task Search()
        {
            Products.Clear();

            if (string.IsNullOrWhiteSpace(SearchTerm))
                await GetAll();
            else if (int.TryParse(SearchTerm, out int index))
            {
                await GetId(index);
            }
            else
            {
                await GetDescription();
            }
        }

        public async Task GetDescription()
        {
            try
            {
                using (var service = new ProductService())
                {
                    var response = await service.GetDescriptionAsync(SearchTerm);

                    if (response == null) return;

                    foreach (var prod in response.Products)
                    {
                        Products.Add(new ProductWrapper()
                        {
                            Id = prod.Id,
                            Title = prod.Title,
                            Description = prod.Description
                        });
                    }
                }
            }
            catch (Exception a)
            {

                MessageBox.Show(a.Message);
            }
        }
        public async Task GetId(int id)
        {
            try
            {
                using (var service = new ProductService())
                {
                    var response = await service.GetIdAsync(id);

                    if (response == null) return;

                    Products.Add(new ProductWrapper()
                    {
                        Id = response.Id,
                        Title = response.Title,
                        Description = response.Description
                    });

                }
            }
            catch (Exception a)
            {

                MessageBox.Show(a.Message);
            }
        }
        public async Task GetAll()
        {
            try
            {
                using (var service = new ProductService())
                {
                    var response = await service.GetAsync();

                    if (response == null) return;

                    foreach (var prod in response.Products)
                    {
                        Products.Add(new ProductWrapper()
                        {
                            Id = prod.Id,
                            Title = prod.Title,
                            Description = prod.Description
                        });
                    }
                }
            }
            catch (Exception a)
            {

                MessageBox.Show(a.Message);
            }
        }

        private async void TxtSearchTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await Search();
                e.Handled = true;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product = new();
            PaneAddVisibility = Visibility.Visible;
        }

        private void BtnClosePaneAdd_Click(object sender, RoutedEventArgs e)
        {
            PaneAddVisibility = Visibility.Hidden;
        }

        private async void BtnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            await Save();
        }

        public async Task Save()
        {
            if (Product.Id == 0)
            {
                await AddProduct();
            }
            else
            {
                await UpdateProduct();
            }
        }

        private async Task AddProduct()
        {
            try
            {
                using (var srv = new ProductService())
                {

                    var obj = new Product() { Title = Product.Title, Description = Product.Description };

                    var validation = new ProductValidation();
                    validation.Validate(obj, obj =>
                    {
                        obj.IncludeRuleSets("Front");
                        obj.ThrowOnFailures();
                    });

                    var ret = await srv.PostAsyc(obj);

                    if (ret != null)
                    {
                        Product.Id = ret.Id;
                        Products.Add(Product);
                        PaneAddVisibility = Visibility.Hidden;
                    }
                }
            }
            catch (ValidationException v)
            {
                MessageBox.Show("Erro de validação" + v.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task UpdateProduct()
        {
            try
            {
                using (var srv = new ProductService())
                {
                    var obj = new Product() { Id = Product.Id, Title = Product.Title, Description = Product.Description };
                    var ret = await srv.PutAsyc(obj);

                    if (ret != null)
                        Product.Id = ret.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Product == null) return;
            Product.Status = 2;
            PaneAddVisibility = Visibility.Visible;
        }

        private async void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (Product == null) return;
            if (e.Key == Key.Delete)
            {
                var result = MessageBox.Show("Deseja realmente excluir o registro?", "Atenção", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    await DeleteProduct();
                }
                e.Handled = true;

            }
        }

        private async Task DeleteProduct()
        {
            try
            {
                using (var srv = new ProductService())
                {
                    var obj = new Product() { Id = Product.Id };
                    var ret = await srv.PutAsyc(obj);
                    if (ret != null)
                    {
                        Products.Remove(Product);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSaveProductEntity_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Id = int.Parse(TxtId.Text);
            product.Title = TxtTitle.Text;
            product.Description = TxtDescription.Text;

            MessageBox.Show($"Save\nProduto ID - {product.Id}\nTitle - {product.Title}");
            PaneAddEntity.Visibility = Visibility.Hidden;
        }

        private void BtnClosePaneAddEntity_Click(object sender, RoutedEventArgs e)
        {
            PaneAddEntity.Visibility = Visibility.Hidden;
        }

        private void BtnAddEntity_Click(object sender, RoutedEventArgs e)
        {
            PaneAddEntity.Visibility = Visibility.Visible;
        }

        private void BtnEditEntity_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Id = 6;
            product.Title = "Teste";
            product.Description = "Dados ok";
            product.Status = 1;

            //0 - Deletado
            //1 - Ativo
            //2 - Bloqueado

            TxtId.Text = product.Id.ToString();
            TxtTitle.Text = product.Title;
            TxtDescription.Text = product.Description;

            if (product.Status == 0)
            {
                TxtStatus.Text = "Deletado";
            }
            else if (product.Status == 1)
            {
                TxtStatus.Text = "Ativo";
            }
            else if (product.Status == 2)
            {
                TxtStatus.Text = "Bloquedo";
            }


            PaneAddEntity.Visibility = Visibility.Visible;
        }
    }
}
