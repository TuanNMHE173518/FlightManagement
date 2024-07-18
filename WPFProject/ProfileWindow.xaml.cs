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
using WPFProject;

namespace WPFProject2
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {

        private readonly AccountMember accountMember;
        private readonly IAccountMemberService accountMemberService;
        public ProfileWindow(AccountMember account)
        {
            InitializeComponent();
            accountMemberService = new AccountMemberService();
            this.accountMember = account;
            lblProfile.Content = accountMember.Email;
            txtEmail.Text = accountMember.Email;
            txtFullName.Text = accountMember.FullName;
            dtDateofbirth.Text = accountMember.Dob.ToString();
            if (accountMember.Gender.Equals("Male")) rbMale.IsChecked = true;
            if (accountMember.Gender.Equals("Female")) rbFemale.IsChecked = true;
        }
        

       

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(accountMember);
            changePasswordWindow.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow(accountMember);
            profileWindow.Show();
            this.Hide();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
                this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Show();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to change your profile?", "Change profile", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                accountMember.FullName = txtFullName.Text;
                if (rbMale.IsChecked == true) accountMember.Gender = "Male";
                if (rbFemale.IsChecked == true) accountMember.Gender = "Female";
                accountMember.Dob = DateOnly.FromDateTime(dtDateofbirth.SelectedDate.Value);
                accountMemberService.UpdateAccount(accountMember);
                
            }
        }
    }
}
