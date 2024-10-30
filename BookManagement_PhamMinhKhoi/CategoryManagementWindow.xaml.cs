using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.Services;
using BookWebRazor.Services.Interface;
using System;
using System.Collections.Generic;
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

namespace BookManagement_PhamMinhKhoi
{
    /// <summary>
    /// Interaction logic for CategoryManagementWindow.xaml
    /// </summary>
    public partial class CategoryManagementWindow : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private Category? _selected;
        public CategoryManagementWindow()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            _productService = new ProductService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            dgCategory.ItemsSource = null;
            dgCategory.ItemsSource = _categoryService.GetAll().OrderBy(x => x.DisplayOrder);
            _selected = null;
            TxtDisplayOrder.Text = null;
            TxtId.Text = null;
            TxtName.Text = null;
        }

        private void dgCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kiểm tra nếu có một hàng nào đó được chọn
            if (dgCategory.SelectedItem != null)
            {
                // Lấy dòng dữ liệu được chọn và map vào một object

                if (dgCategory.SelectedItem is Category selectedRow)
                {
                    TxtId.Text = selectedRow.Id.ToString();
                    TxtName.Text = selectedRow.Name;
                    TxtDisplayOrder.Text = selectedRow.DisplayOrder.ToString();
                    _selected = _categoryService.GetById(int.Parse(TxtId.Text));
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            TxtId.Text = null;
            TxtName.Text = null;
            TxtDisplayOrder.Text = null;
            _selected = null;
            dgCategory.SelectedItem = null;
        }

        

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //Validate
            if(!ValidateInput())
            {
                return;
            }
            if (_selected != null)
            {
                _selected = _categoryService.GetById(int.Parse(TxtId.Text));
                //Edit mode
                _selected.Name = TxtName.Text;
                _selected.DisplayOrder = int.Parse(TxtDisplayOrder.Text);
                if (_categoryService.Update(_selected))
                {
                    Refresh();
                    MessageBox.Show("Update successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);                    
                }
                else
                {
                    MessageBox.Show("Update failed", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                //Add mode
                Category category = new();
                category.Name = TxtName.Text;
                category.DisplayOrder = int.Parse(TxtDisplayOrder.Text);
                if (_categoryService.Add(category))
                {
                    Refresh();
                    MessageBox.Show("Add successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                else
                {
                    MessageBox.Show("Add failed", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(TxtName.Text))
            {
                MessageBox.Show("Category Name is required", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TxtDisplayOrder.Text))
            {
                MessageBox.Show("Display Order is required", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!int.TryParse(TxtDisplayOrder.Text, out int order))
            {
                MessageBox.Show("Display Order must be numeric", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (order < 0)
            {
                MessageBox.Show("Display Order must be larger than 0", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCategory.SelectedItem != null)
            {
                _selected = dgCategory.SelectedItem as Category;

                if (_selected != null)
                {
                    if (_productService.GetProducts().Any(x => x.CategoryId == _selected.Id))
                    {
                        MessageBox.Show("This category is being used by some books, you can not delete it", "Delete warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var result = MessageBox.Show("Are you sure to delete this category?", "Delete confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _selected = _categoryService.GetById(_selected.Id);
                        _categoryService.Delete(_selected);
                        Refresh();                        
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a category before deleting it", "Select a category warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
