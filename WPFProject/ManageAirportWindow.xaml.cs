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
    /// Interaction logic for ManageAirportWindow.xaml
    /// </summary>
    public partial class ManageAirportWindow : Window
    {
        private readonly IAirportService airportService;
        public ManageAirportWindow()
        {
            InitializeComponent();
            airportService = new AirportService();
            LoadAirport();
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

        public void LoadAirport()
        {
            dgAirport.ItemsSource = null;
            var airports = airportService.GetAllAirports();
            dgAirport.ItemsSource = airports;
        }

        public void Reset()
        {
            txtId.Text = "";
            txtCode.Text = "";
            txtName.Text = "";
            txtCountry.Text = "";
            txtState.Text = "";
            txtCity.Text = "";
            txtFilterName.Text = "";
            txtFilterCountry.Text = "";
            txtFilterCity.Text = "";
            txtFilterState.Text = "";
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && txtName.Text.Length > 0)
            {
                txtBlockName.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockName.Visibility = Visibility.Visible;
            }
        }

        private void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text) && txtCode.Text.Length > 0)
            {
                txtBlockCode.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockCode.Visibility = Visibility.Visible;
            }
        }

        private void txtCountry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCountry.Text) && txtCountry.Text.Length > 0)
            {
                txtBlockCountry.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockCountry.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockCountry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCountry.Focus();
        }

        private void txtBlockState_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtState.Focus();
        }

        private void txtState_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtState.Text) && txtState.Text.Length > 0)
            {
                txtBlockState.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockState.Visibility = Visibility.Visible;
            }
        }

        private void txtCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCity.Text) && txtCity.Text.Length > 0)
            {
                txtBlockCity.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockCity.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockCity_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCity.Focus();
        }

        private void txtBlockName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtName.Focus();
        }

        private void txtBlockCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCode.Focus();
        }

        private void txtBlockFilterName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterName.Focus();
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

        private void txtStateFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterState.Text) && txtFilterState.Text.Length > 0)
            {
                txtBlockStateFilter.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockStateFilter.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockCityFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterCity.Focus();
        }

        private void txtCityFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterCity.Text) && txtFilterCity.Text.Length > 0)
            {
                txtBlockCityFilter.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockCityFilter.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockStateFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterState.Focus();
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

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            dgAirport.ItemsSource = airportService.FilterAirport(txtFilterName.Text, txtFilterCountry.Text, txtFilterState.Text, txtFilterCity.Text);
        }

        private void btnreset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            LoadAirport();
        }

        private void txtBlockFilterCountry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterCountry.Focus();
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
                Airport airport = airportService.GetAirportById(Int32.Parse(txtId.Text));
                if (airport != null)
                {
                    airport.Name = txtName.Text;
                    airport.Code = txtCode.Text;
                    airport.Country = txtCountry.Text;
                    airport.State = txtState.Text;
                    airport.City = txtCity.Text;
                    airportService.UpdateAirport(airport);
                    dgAirport.ItemsSource = airportService.GetAllAirports();
                }
                else
                {
                    MessageBox.Show("This Airport doesn't exist");
                }
            }
            else
            {
                MessageBox.Show("You must select a Airport to Update!");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Airport airport = new Airport();
            if (string.IsNullOrEmpty(txtCode.Text)
                || string.IsNullOrEmpty(txtName.Text)
                || string.IsNullOrEmpty(txtCountry.Text)
                || (string.IsNullOrEmpty(txtState.Text) && string.IsNullOrEmpty(txtCity.Text)))
            {
                MessageBox.Show("You need to fill in all information fields.\nNote: For State and City you can fill in either or both.");
            }
            else
            {
                if (airportService.GetAirportByCode(txtCode.Text) != null)
                {
                    MessageBox.Show("This airport code is already in use");
                }
                else
                {
                    airport.Code = txtCode.Text;
                    airport.Name = txtName.Text;
                    airport.Country = txtCountry.Text;
                    airport.State = txtState.Text;
                    airport.City = txtCity.Text;
                    airportService.CreateAirport(airport);
                    dgAirport.ItemsSource = airportService.GetAllAirports();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
                Airport airport = airportService.GetAirportById(Int32.Parse(txtId.Text));
                if (airport != null)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to delete this Airport?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        airportService.DeleteAirport(airport);
                        dgAirport.ItemsSource = airportService.GetAllAirports();
                    }
                }
                else
                {
                    MessageBox.Show("This Airport doesn't exist");
                }
            }
            else
            {
                MessageBox.Show("You must select a Airport to Delete!");
            }
        }

        private void dgAirport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            if (dataGrid.SelectedItems.Count > 0)
            {
                Airport airport = dataGrid.SelectedItem as Airport;
                txtId.Text = airport.Id.ToString();
                txtCode.Text = airport.Code;
                txtName.Text = airport.Name;
                txtCountry.Text = airport.Country;
                txtState.Text = airport.State;
                txtCity.Text = airport.City;
            }
        }
    }
}