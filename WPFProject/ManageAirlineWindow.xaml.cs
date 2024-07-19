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
        private int CurrentPage = 1;
        private int ItemsPerPage = 10;
        private int TotalPage;
        private string searchName;
        private string searchCode;
        private string searchCountry;
        private bool isSearch = false;
        private List<Airline> airlines;
        public ManageAirlineWindow()
        {
            InitializeComponent();
            airlineService = new AirlineService();
            airlines = airlineService.GetAllAirlines();
            cmbItemsPerPage.ItemsSource = new[] { 5, 10, 20, 50, 100 };
            cmbItemsPerPage.SelectedValue = 10;
            LoadAirline();

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
                TotalPage = (int)Math.Ceiling((double)airlines.Count / ItemsPerPage);
                if (CurrentPage > TotalPage)
                {
                    CurrentPage = TotalPage;
                }
                DgAirline.ItemsSource = airlines.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
                txtPageNumber.Text = CurrentPage.ToString();
                txtTotalPage.Text = "of " + TotalPage;
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
                if (string.IsNullOrWhiteSpace(txtAirlineCode.Text) ||
                    string.IsNullOrWhiteSpace(txtAirlineName.Text) ||
                    string.IsNullOrWhiteSpace(txtAirlineCountry.Text))
                {
                    MessageBox.Show("All fields are required!");
                    return;
                }
                if (txtAirlineCode.Text.Length > 3 || txtAirlineCode.Text.Length <2)
                {
                    MessageBox.Show("Airline code must be 2-3 characters!");
                    return;
                }
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
                airlines = airlineService.GetAllAirlines();
                TotalPage = (int)Math.Ceiling((double)airlines.Count / ItemsPerPage);
                CurrentPage = TotalPage;
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
                if (isSearch)
                {
                    airlines = airlineService.GetFilteredAirlines(searchCode, searchName, searchCountry).ToList();
                }
                else
                {
                    airlines = airlineService.GetAllAirlines();
                }
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
                if (isSearch)
                {
                    airlines = airlineService.GetFilteredAirlines(searchCode, searchName, searchCountry).ToList();
                }
                else
                {
                    airlines = airlineService.GetAllAirlines();
                }
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
            isSearch = false;
            airlines = airlineService.GetAllAirlines();
            LoadAirline();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string code = txtFilterCode.Text;
                string name = txtFilterName.Text;
                string country = txtFilterCountry.Text;
                searchCode = code;
                searchName = name;
                searchCountry = country;
                isSearch = true;

                var filteredAirlines = airlineService.GetFilteredAirlines(code, name, country);

                DgAirline.ItemsSource = null;
                DgAirline.ItemsSource = filteredAirlines;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadAirline();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage < TotalPage)
            {
                CurrentPage++;
                LoadAirline();
            }
        }

        private void cmbItemsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ItemsPerPage = Int32.Parse(cmbItemsPerPage.SelectedValue.ToString());
            LoadAirline();
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
                    LoadAirline();
                }

            }

        }
    }

}