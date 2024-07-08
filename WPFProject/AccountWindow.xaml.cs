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
using System.Xml.Linq;
using WPFProject2;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        private readonly IAccountMemberService accountMemberService;
        private readonly AccountMember yourAcccount;
        public AccountWindow(AccountMember accountMember)
        {
            InitializeComponent();
            this.yourAcccount = accountMember;
            accountMemberService = new AccountMemberService();
            LoadAccounts();
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


        private void LoadAccounts()
        {
            dgAccount.ItemsSource = null;
            var Accounts = accountMemberService.GetAllAccounts();
            dgAccount.ItemsSource = Accounts;

            cbFilterRole.ItemsSource = Accounts.Select(a => new
            {
                a.Role
            }).Distinct().ToList();
            cbFilterRole.SelectedValuePath = "Role";
            cbFilterRole.DisplayMemberPath = "Role";
            

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

        private void dgAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            if (dataGrid?.ItemsSource != null)
            {
                DataGridRow? row = dataGrid?.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                DataGridCell? cell = dataGrid?.Columns[1].GetCellContent(row).Parent as DataGridCell;
                string accountEmail = ((TextBlock)cell.Content).Text;
                if (!accountEmail.Equals(""))
                {
                    AccountMember account = accountMemberService.GetAccountByEmail(accountEmail);
                    txtEmail.Text = accountEmail;
                    txtId.Text = account.AccountId.ToString();
                    txtFullName.Text = account.FullName;
                    if(account.Role.Equals("Admin")) rbAdmin.IsChecked = true;
                    if(account.Role.Equals("Staff")) rbStaff.IsChecked = true;
                }
            }
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Text.Length > 0)
            {
                txtBlockID.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockID.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            foreach(Window window in Application.Current.Windows)
            {
                if(window is MainWindow)
                {
                    window.Show();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text.Length > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to block this account?", "Block account", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AccountMember account = accountMemberService.GetAccountById(Int32.Parse(txtId.Text));
                    account.Enable = false;
                    accountMemberService.UpdateAccount(account);
                    LoadAccounts();
                }
            }
            else
            {
                MessageBox.Show("Choose an account!");
            }
        }

        private void btnUnBlock_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to unlock this account?", "Unlock account", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AccountMember account = accountMemberService.GetAccountById(Int32.Parse(txtId.Text));
                    account.Enable = true;
                    accountMemberService.UpdateAccount(account);
                    LoadAccounts();
                }
            }
            else
            {
                MessageBox.Show("Choose an account!");
            }
        }

        private void btnUpdateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
               
                AccountMember account = accountMemberService.GetAccountById(Int32.Parse(txtId.Text));
                account.FullName = txtFullName.Text;
                account.Email = txtEmail.Text;
                if(rbAdmin.IsChecked == true)
                {
                    account.Role = "Admin";
                }
                if (rbStaff.IsChecked == true)
                {
                    account.Role = "Staff";
                }
                accountMemberService.UpdateAccount(account);
                LoadAccounts();
                
            }
            else
            {
                MessageBox.Show("Choose an account!");
            }
        }

        private void txtBlockSignUp_MouseEnter(object sender, MouseEventArgs e)
        {
            txtBlockSignUp.Foreground = new SolidColorBrush(Colors.DarkCyan);
        }



        private void txtBlockSignUp_MouseLeave(object sender, MouseEventArgs e)
        {
            txtBlockSignUp.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void txtBlockSignUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            SignupWindow signupWindow = new SignupWindow();
            signupWindow.Show();
        }

       

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void txtFilterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterName.Text) && txtFilterName.Text.Length >= 0)
            {
                txtBlockFilterName.Visibility = Visibility.Collapsed;
                dgAccount.ItemsSource = null;

                var foundAccount = accountMemberService.FindByFullName(txtFilterName.Text);
                dgAccount.ItemsSource = foundAccount;
                

            }
            else
            {
                txtBlockFilterName.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockFilterName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterName.Focus();
        }

        private void cbFilterRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   if (cbFilterRole.SelectedValue != null)
            {
                var foundAccount = accountMemberService.FindByRole(cbFilterRole.SelectedValue.ToString()).ToList();
                dgAccount.ItemsSource = null;
                dgAccount.ItemsSource = foundAccount;
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            LoadAccounts();
            txtEmail.Text = "";
            txtFullName.Text = "";
            txtId.Text = "";
            rbAdmin.IsChecked = false;
            rbStaff.IsChecked = false;
            txtFilterName.Text = "";
            cbFilterRole.SelectedValue = null;
        }
        private void btnCloseAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            LoadAccounts();

        }

        private void txtChangePassword_MouseEnter(object sender, MouseEventArgs e)
        {
            txtChangePassword.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void txtChangePassword_MouseLeave(object sender, MouseEventArgs e)
        {
            txtChangePassword.Foreground = new SolidColorBrush(Colors.IndianRed);
        }

        private void txtChangePassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(yourAcccount);
            changePasswordWindow.Show();
            this.Hide();
            
        }
    }
}
