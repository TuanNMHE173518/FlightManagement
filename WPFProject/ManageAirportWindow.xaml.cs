using BussinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int CurrentPage = 1;
        private int ItemsPerPage = 10;
        private int TotalPage;
        private string searchName;
        private string searchCountry;
        private string searchCity;
        private string searchState;
        private bool isSearch = false;
        private List<Airport> airports;
        public ManageAirportWindow()
        {
            InitializeComponent();
            airportService = new AirportService();
            airports = airportService.GetAllAirports();
            cmbItemsPerPage.ItemsSource = new[] { 5, 10, 20, 50, 100 };
            cmbItemsPerPage.SelectedValue = 10;
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


            TotalPage = (int)Math.Ceiling((double)airports.Count / ItemsPerPage);
            if (CurrentPage > TotalPage)
            {
                CurrentPage = TotalPage;
            }
            dgAirport.ItemsSource = airports.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
            txtPageNumber.Text = CurrentPage.ToString();
            txtTotalPage.Text = "of " + TotalPage;
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
            isSearch = false;
            airports = airportService.GetAllAirports();
            CurrentPage = 1;
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
            searchName = txtFilterName.Text;
            searchCountry = txtFilterCountry.Text;
            searchState = txtFilterState.Text;
            searchCity = txtFilterCity.Text;
            airports = airportService.FilterAirport(txtFilterName.Text, txtFilterCountry.Text, txtFilterState.Text, txtFilterCity.Text);
            isSearch = true;
            LoadAirport();
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
                    airports = airportService.GetAllAirports();
                    if (isSearch)
                    {
                        airports = airportService.FilterAirport(searchName, searchCountry, searchState, searchCity);
                    }
                    else
                    {
                        airports = airportService.GetAllAirports();
                    }
                    LoadAirport();
                    MessageBox.Show("Successfully");
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
                    airports = airportService.GetAllAirports();
                    TotalPage = (int)Math.Ceiling((double)airports.Count / ItemsPerPage);
                    CurrentPage = TotalPage;
                    LoadAirport();
                    MessageBox.Show("Successfully");
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
                        airports = airportService.GetAllAirports();
                        if (isSearch)
                        {
                            airports = airportService.FilterAirport(searchName, searchCountry, searchState, searchCity);
                        }
                        else
                        {
                            airports = airportService.GetAllAirports();
                        }
                        LoadAirport();
                        MessageBox.Show("Successfully");
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
            if (dataGrid.SelectedItem != null)
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

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadAirport();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage < TotalPage)
            {
                CurrentPage++;
                LoadAirport();
            }
        }

        private void cmbItemsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ItemsPerPage = Int32.Parse(cmbItemsPerPage.SelectedValue.ToString());
            LoadAirport();
        }

        private void txtPageNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPageNumber.Text))
            {
                if (!int.TryParse(txtPageNumber.Text, out int result))
                {
                    txtPageNumber.Text = "";
                }
                else
                {
                    CurrentPage = Int32.Parse(txtPageNumber.Text);
                    LoadAirport();
                }

            }

        }

    }
}