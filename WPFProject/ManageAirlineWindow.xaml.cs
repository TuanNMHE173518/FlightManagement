using BussinessObjects;
using DataAccessLayer;
using Repositories;
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
    /// Interaction logic for ManageAirlineWindow.xaml
    /// </summary>
    public partial class ManageAirlineWindow : Window
    {
        private readonly IAirlineService airlineService;
        public ManageAirlineWindow()
        {
            InitializeComponent();
            airlineService = new AirlineService();
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
        private void LoadAirline()
        {
            try
            {
                DgAirline.ItemsSource = null;
                var airline = airlineService.GetAllAirlines();
                DgAirline.ItemsSource = airline;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgAirline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgAirline.SelectedItem is Airline airline)
            {
                txtAirlineId.Text = airline.Id.ToString();
                txtAirlineCode.Text = airline.Code.ToString();
                txtAirlineName.Text = airline.Name.ToString();
                txtAirlineCountry.Text = airline.Country.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAirline();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Airline airline = new Airline();
                airline.Code = txtAirlineCode.Text;
                airline.Name = txtAirlineName.Text;
                airline.Country = txtAirlineCountry.Text;
                AirlineDAO.AddAirline(airline);
                MessageBox.Show("Airline add successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadAirline();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txtAirlineId.Text.Length > 0)
                {
                    Airline airline = new Airline();
                    airline.Id = Int32.Parse(txtAirlineId.Text);
                    airline.Code = txtAirlineCode.Text;
                    airline.Name = txtAirlineName.Text;
                    airline.Country = txtAirlineCountry.Text;
                    AirlineDAO.UpdateAirline(airline);
                }
                else
                {
                    MessageBox.Show("Please select a Airline");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadAirline();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtAirlineId.Text.Length > 0)
                {
                    Airline airline = new Airline();
                    airline.Id = Int32.Parse(txtAirlineId.Text);
                    airline.Code = txtAirlineCode.Text;
                    airline.Name = txtAirlineName.Text;
                    airline.Country = txtAirlineCountry.Text;
                    AirlineDAO.DeleteAirline(airline);
                }
                else
                {
                    MessageBox.Show("Please select a Airline");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadAirline();
            }
        }

        private void txtAirlineId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAirlineId.Text) && txtAirlineId.Text.Length > 0)
            {
                txtBlockAirlineID.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockAirlineID.Visibility = Visibility.Visible;
            }
        }

        private void txtAirlineCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAirlineCode.Text) && txtAirlineCode.Text.Length > 0)
            {
                txtBlockAirlineCode.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtBlockAirlineCode.Visibility = Visibility.Visible;
            }
        }

        private void txtAirlineName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAirlineName.Text) && txtAirlineName.Text.Length > 0)
            {
                txtBlockAirlineName.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtBlockAirlineName.Visibility = Visibility.Visible;
            }
        }

        private void txtAirlineCountry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAirlineCountry.Text) && txtAirlineCountry.Text.Length > 0)
            {
                txtBlockAirlineCountry.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtBlockAirlineCountry.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockAirlineCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtAirlineCode.Focus();
        }

        private void txtBlockAirlineName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtAirlineName.Focus();
        }

        private void txtBlockAirlineCountry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtAirlineCountry.Focus();
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

        private void txtFilterCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterCode.Text) && txtFilterCode.Text.Length > 0)
            {
                txtBlockFilterCode.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockFilterCode.Visibility = Visibility.Visible;
            }
        }
        private void txtFilterCountry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterCountry.Text) && txtFilterCountry.Text.Length > 0)
            {
                txtBlockFilterCountry.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockFilterCountry.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockFilterCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterCode.Focus();
        }

        private void txtBlockFilterName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterName.Focus();
        }

        private void txtBlockFilterCountry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterCountry.Focus();
        }

        private void btnreset_Click(object sender, RoutedEventArgs e)
        {
            txtAirlineId.Text = "";
            txtAirlineCode.Text = "";
            txtAirlineName.Text = "";
            txtAirlineCountry.Text = "";
            txtFilterCode.Text = "";
            txtFilterName.Text = "";
            txtFilterCountry.Text = "";
            LoadAirline();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string code = txtFilterCode.Text;
                string name = txtFilterName.Text;
                string country = txtFilterCountry.Text;

                var filteredAirlines = airlineService.GetFilteredAirlines(code, name, country);

                DgAirline.ItemsSource = null;
                DgAirline.ItemsSource = filteredAirlines;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

}