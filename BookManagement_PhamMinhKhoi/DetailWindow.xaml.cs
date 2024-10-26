using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Services;
using BookWebRazor.Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BookManagement_PhamMinhKhoi
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        public Product? EditedProduct { get; set; }
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public DetailWindow()
        {
            InitializeComponent();
            _productService = new ProductService();
            _categoryService = new CategoryService();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = false;
            //valid before save
            if (!ValidateInput())
            {
                return;
            }
            if (EditedProduct != null)
            {
                //edit mode
                MapData(EditedProduct);
                EditedProduct.Category = _categoryService.GetById(EditedProduct.CategoryId) ?? new();
                isSuccess = _productService.UpdateProduct(EditedProduct);
            }
            else
            {
                //add mode
                try
                {
                    Product product = new();
                    MapData(product);
                    isSuccess = _productService.CreateProduct(product);
                }
                catch (Exception)
                {

                    MessageBox.Show("Product Id is duplicated");
                }
            }
            if (isSuccess)
            {
                MessageBox.Show("Save successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Save failed", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void MapData(Product p)
        {
            p.Title = txtTitle.Text;
            p.Description = txtDescription.Text;
            p.Author = txtAuthor.Text;
            p.ISBN = txtISBN.Text;
            p.ListPrice = double.Parse(txtListPrice.Text);
            p.Price50 = double.Parse(txtListPrice50.Text);
            p.Price100 = double.Parse(txtListPrice100.Text);
            p.Price = double.Parse(txtPrice.Text);
            p.CategoryId = int.Parse(cbCategory.SelectedValue.ToString() ?? string.Empty);
        }

        private bool ValidateInput()
        {
            List<string> errorMessages = new List<string>();
            if (string.IsNullOrEmpty(txtTitle.Text) ||
                string.IsNullOrEmpty(txtDescription.Text) ||
                string.IsNullOrEmpty(txtISBN.Text) ||
                string.IsNullOrEmpty(txtPrice.Text) ||
                string.IsNullOrEmpty(txtAuthor.Text) ||
                string.IsNullOrEmpty(txtListPrice.Text) ||
                string.IsNullOrEmpty(txtListPrice50.Text) ||
                string.IsNullOrEmpty(txtListPrice100.Text)
                )
            {
                errorMessages.Add("All field are required");
            }
            else
            {
                if (!double.TryParse(txtPrice.Text, out _))
                {
                    errorMessages.Add("Price must be numeric");
                }
                if (!double.TryParse(txtListPrice.Text, out _))
                {
                    errorMessages.Add("List Price must be numeric");
                }
                if (!double.TryParse(txtListPrice50.Text, out _))
                {
                    errorMessages.Add("List Price 50 must be numeric");
                }
                if (!double.TryParse(txtListPrice100.Text, out _))
                {
                    errorMessages.Add("List Price 100 must be numeric");
                }
            }



            if (errorMessages.Count > 0)
            {
                string errorMessage = string.Join(Environment.NewLine, errorMessages);
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbCategory.ItemsSource = _categoryService.GetAll();
            cbCategory.DisplayMemberPath = "Name";
            cbCategory.SelectedValuePath = "Id";

            if (EditedProduct != null)
            {
                lblHeader.Content = "Edit Product";
                txtId.Text = EditedProduct.Id.ToString();
                txtTitle.Text = EditedProduct.Title;
                txtDescription.Text = EditedProduct.Description;
                txtAuthor.Text = EditedProduct.Author;
                txtISBN.Text = EditedProduct.ISBN;
                txtListPrice.Text = EditedProduct.ListPrice.ToString();
                txtListPrice50.Text = EditedProduct.Price50.ToString();
                txtListPrice100.Text = EditedProduct.Price100.ToString();
                txtPrice.Text = EditedProduct.Price.ToString();
                cbCategory.SelectedValue = EditedProduct.CategoryId;
            }
            else
            {
                lblHeader.Content = "Add Product";
                cbCategory.SelectedIndex = 0;
            }
        }
    }
}
