using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.BusinessObjects.ViewModel;
using BookWebRazor.Services;
using BookWebRazor.Services.Interface;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BookManagement_PhamMinhKhoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductService _productService;
        private Product? _selected;
        public MainWindow()
        {
            InitializeComponent();
            _productService = new ProductService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            dgvProduct.ItemsSource = null;
            dgvProduct.ItemsSource = _productService.GetProducts(includeProperties: "Category");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTitleSearch.Text) || !string.IsNullOrEmpty(txtAuthorSearch.Text))
            {
                dgvProduct.ItemsSource = null;
                dgvProduct.ItemsSource = _productService.GetProducts(includeProperties: "Category")
                    .Where(x => (!string.IsNullOrEmpty(txtTitleSearch.Text) && x.Title.ToLower().Contains(txtTitleSearch.Text.ToLower())) ||
                                (!string.IsNullOrEmpty(txtAuthorSearch.Text) && x.Author.ToLower().Contains(txtAuthorSearch.Text.ToLower())));
            }
            else
            {
                FillDataGrid();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DetailWindow detailWindow = new();
            detailWindow.ShowDialog();
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgvProduct.SelectedItem != null)
            {
                _selected = dgvProduct.SelectedItem as Product;

                if (_selected != null)
                {
                    //đẩy selectedBook vào form detail để hiển thị
                    DetailWindow detailForm = new();
                    detailForm.EditedProduct = _productService.GetProductById(_selected.Id, includeProperties: "Category");
                    detailForm.ShowDialog();
                    FillDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select a book before updating it", "Select a book warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvProduct.SelectedItem != null)
            {
                _selected = dgvProduct.SelectedItem as Product;

                if (_selected != null)
                {
                    var result = MessageBox.Show("Are you sure to delete this product?", "Delete confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _productService.DeleteProduct(_selected);
                        FillDataGrid();
                        _selected = null;
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a product before deleting it", "Select a book warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}