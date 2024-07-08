using BussinessObjects;
using Microsoft.Identity.Client.NativeInterop;
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
using WPFProject;

namespace WPFProject2
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private readonly IAccountMemberService accountMemberService;
        private readonly AccountMember account;
        public ChangePasswordWindow(AccountMember account)
        {
            InitializeComponent();
            this.account = account;
            accountMemberService = new AccountMemberService();
        }

        private void txtBlockOldPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtOldPass.Focus();
        }

       

        private void txtBlockNewPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNewPassword.Focus();
        }

        private void txtNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewPassword.Password) && txtNewPassword.Password.Length > 0)
            {
                txtBlockNewPassword.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockNewPassword.Visibility = Visibility.Visible;
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtOldPass.Password) && !string.IsNullOrEmpty(txtNewPassword.Password) && !string.IsNullOrEmpty(txtVerify.Password))
            {
                
                if (account != null)
                {
                    if(BCrypt.Net.BCrypt.Verify(txtOldPass.Password, account.Password))
                    {
                        if (txtNewPassword.Password.Equals(txtVerify.Password))
                        {
                            account.Password = BCrypt.Net.BCrypt.HashPassword(txtNewPassword.Password);
                            accountMemberService.UpdateAccount(account);
                            this.Close();
                            MessageBox.Show("Change password successful!");
                        }
                        else
                        {
                            MessageBox.Show("Verify password do not match!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Old password is incorrect!");
                    }


                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Have an error during change password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (result == MessageBoxResult.OK) {
                        this.Close();

                    }
                    else
                    {
                        this.Close();
                    }
                }



            }else
            {
                MessageBox.Show("You must input all fields to change password!");
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

        private void btnCloseSignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void txtBlockVerify_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtVerify.Focus();
        }

        

        private void txtOldPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOldPass.Password) && txtOldPass.Password.Length > 0)
            {
                txtBlockOldPassword.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockOldPassword.Visibility = Visibility.Visible;
            }
        }

        private void txtVerify_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVerify.Password) && txtVerify.Password.Length > 0)
            {
                txtBlockVerify.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockVerify.Visibility = Visibility.Visible;
            }
        }
    }
}
