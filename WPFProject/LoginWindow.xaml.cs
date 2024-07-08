using BussinessObjects;
using Services;
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

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountMemberService accountMemberService;


        public LoginWindow()
        {
            InitializeComponent();
            accountMemberService = new AccountMemberService();
        }

        private void txtBlockEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                txtBlockEmail.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockEmail.Visibility = Visibility.Visible;
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                txtBlockPassword.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockPassword.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            AccountMember account = accountMemberService.GetAccountByEmail(txtEmail.Text);
            if (account != null && BCrypt.Net.BCrypt.Verify(txtPassword.Password, account.Password))
            {
                if(account.Enable == true) {
                    this.Hide();
                    MainWindow mainWindow = new MainWindow(account);
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Your account have been blocked!");
                }

            }
            else
            {
                MessageBox.Show("Invalid account!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }


        

        
    }
}
