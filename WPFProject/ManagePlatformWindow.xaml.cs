using BussinessObjects;
using DataAccessLayer;
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
using WPFProject;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WPFProject2
{
    /// <summary>
    /// Interaction logic for ManagePlatformWindow.xaml
    /// </summary>
    public partial class ManagePlatformWindow : Window
    {
        private readonly IBookingPlatformService bookingPlatformService;
        public ManagePlatformWindow()
        {
            bookingPlatformService = new BookingPlatformService();
            InitializeComponent();
            LoadPlatform();
        }

        private void btnCloseAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Show();
                }
            }
        }

        private void LoadPlatform()
        {
            dgPlatform.ItemsSource = null;
            var platform = bookingPlatformService.GetAll();
            dgPlatform.ItemsSource = platform;
        }


        private void txtPlatformId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPlatformId.Text) && txtPlatformId.Text.Length > 0)
            {
                txtBlockPlatformId.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockPlatformId.Visibility = Visibility.Visible;
            }
        }

        private void txtPlatformName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPlatformName.Text) && txtPlatformName.Text.Length > 0)
            {
                txtBlockPlatformName.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockPlatformName.Visibility = Visibility.Visible;
            }
        }


        private void txtPlatformUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPlatformUrl.Text) && txtPlatformUrl.Text.Length > 0)
            {
                txtBlockPlatformUrl.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockPlatformUrl.Visibility = Visibility.Visible;
            }
        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlatformName.Text))
            {
                MessageBox.Show("You must input Name fields");
            }
            else
            {
                if (bookingPlatformService.GetBookingPlatformByUrl(txtPlatformUrl.Text) != null || bookingPlatformService.GetBookingPlatformByName(txtPlatformName.Text) != null)
                {
                    MessageBox.Show("Name or Url is alreaddy in use");
                }
                else
                {
                    BookingPlatform platform = new BookingPlatform();
                    platform.Name = txtPlatformName.Text;
                    platform.Url = txtPlatformUrl.Text;
                    bookingPlatformService.AddPlatform(platform);
                    LoadPlatform();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPlatformId.Text.Length > 0)
                {
                    BookingPlatform bookingPlatform = bookingPlatformService.GetBookingPlatformById(Int32.Parse(txtPlatformId.Text));
                    if (bookingPlatform != null)
                    {
                        if (!bookingPlatformService.IsPlatformNameExists(txtPlatformName.Text, txtPlatformUrl.Text, Int32.Parse(txtPlatformId.Text)))
                        {
                            bookingPlatform.Name = txtPlatformName.Text;
                            bookingPlatform.Url = txtPlatformUrl.Text;
                            bookingPlatformService.UpdatePlatform(bookingPlatform);
                            LoadPlatform();
                        }
                        else
                        {
                            MessageBox.Show("The platform Name or Url already exists. Please check valid");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Platform is not exist");
                    }

                }
                else
                {
                    MessageBox.Show("Please choose A Platform");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPlatformId.Text.Length > 0)
                {
                    BookingPlatform bookingPlatform = new BookingPlatform();
                    bookingPlatform.Id = Int32.Parse(txtPlatformId.Text);
                    bookingPlatform.Name = txtPlatformName.Text;
                    bookingPlatform.Url = txtPlatformUrl.Text;
                    bookingPlatformService.DeletePlatform(bookingPlatform);
                }
                else
                {
                    MessageBox.Show("Please choose Platform");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadPlatform();
            }
        }

        private void dgPlatform_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPlatform.SelectedItem is BookingPlatform bookingPlatform)
            {
                txtPlatformId.Text = bookingPlatform.Id.ToString();
                txtPlatformName.Text = bookingPlatform.Name.ToString();
                if (bookingPlatform.Url != null)
                {
                    txtPlatformUrl.Text = bookingPlatform.Url.ToString();
                }
                else
                {
                    txtPlatformUrl.Text = null;
                }

            }
        }

        private void txtBlockPlatformUrl_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            txtPlatformUrl.Focus();
        }

        private void txtBlockPlatformName_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            txtPlatformName.Focus();
        }

        private void txtBlockPlatformId_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPlatformId.Focus();
        }

        private void txtFilterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterName.Text) && txtFilterName.Text.Length > 0)
            {
                txtBlockFilterName.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockFilterName.Visibility = Visibility.Visible;
            }
        }

        private void txtFilterUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterUrl.Text) && txtFilterUrl.Text.Length > 0)
            {
                txtBlockFilterUrl.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockFilterUrl.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockFilterUrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterUrl.Focus();
        }

        private void txtBlockFilterName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterName.Focus();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {

            var foundPlatformByFilter = bookingPlatformService.GetFilterPlatform(txtFilterName.Text, txtFilterUrl.Text);

            if (!foundPlatformByFilter.Any())
            {
                MessageBox.Show("No platforms match the filter", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            dgPlatform.ItemsSource = null;
            dgPlatform.ItemsSource = foundPlatformByFilter;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtPlatformId.Text = "";
            txtPlatformName.Text = "";
            txtPlatformUrl.Text = "";
            txtFilterName.Text = "";
            txtFilterUrl.Text = "";
            LoadPlatform();
        }
    }
}