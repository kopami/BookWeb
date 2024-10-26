using BookWebRazor.BusinessObjects.Enum;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IAccountService _accountService;
        public LoginWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var account = _accountService.Login(txtEmail.Text, txtPassword.Text, out string message);

            if (account != null)
            {
                if (account.Role == RoleEnum.Admin.ToString())
                {
                    MainWindow main = new();
                    main.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("You don't have permission to access this page", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show(message, "Login Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }             
        }

        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
