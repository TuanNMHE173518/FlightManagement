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
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {

        private readonly IAccountMemberService accountMemberService;
        public SignupWindow()
        {
            InitializeComponent();
            accountMemberService = new AccountMemberService();
        }

        private void txtBlockFullName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFullName.Focus();
            
        }

        private void txtFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFullName.Text) && txtFullName.Text.Length > 0)
            {
                txtBlockFullName.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockFullName.Visibility = Visibility.Visible;
            }
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
        private void btnCloseSignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
          
        }



        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(txtFullName.Text.Length <=0 || txtEmail.Text.Length <=0 || txtPassword.Password.Length <=0 || (rbAdmin.IsChecked ==false && rbStaff.IsChecked == false )) {

                MessageBox.Show("Please fill out all fields completely!");

            }
            else
            {
                AccountMember account = new AccountMember();
                account.FullName = txtFullName.Text;
                account.Email = txtEmail.Text;
                account.Password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);
                account.Enable = true;
                if(rbAdmin.IsChecked == true)
                {
                    account.Role = "Admin";
                }
                else if (rbStaff.IsChecked == true)
                {
                    account.Role = "Staff";
                }

                accountMemberService.CreateAccount(account);
                this.Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is AccountWindow)
                {
                    window.Show();
                }
            }
        }
    }
}
