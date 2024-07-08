using BussinessObjects;
using CsvHelper;
using Microsoft.Win32;
using OfficeOpenXml;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using Path = System.IO.Path;

namespace WPFProject2
{
    /// <summary>
    /// Interaction logic for ManagePassengerWindow.xaml
    /// </summary>
    public partial class ManagePassengerWindow : Window
    {

        private readonly IPassengerService passengerService;
        public ManagePassengerWindow()
        {
            InitializeComponent();
            passengerService = new PassengerService();
            LoadPassenger();
        }

        private void LoadPassenger()
        {
            dgPassenger.ItemsSource = null;
            var passengers = passengerService.GetAll();
            dgPassenger.ItemsSource = passengers;

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

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void txtBlockFilterName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterName.Focus();
        }

       


        private void reset_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = null;
            txtEmail.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;
            txtCountry.Text = null;
            dtDate.SelectedDate = null;
            txtAgeFrom.Text = null;
            txtAgeTo.Text = null;
            txtFilterEmail.Text = null;
            txtFilterName.Text = null;
            LoadPassenger();
        }

        private void txtBlockEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtBlockFirstName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFirstName.Focus();
        }

        private void txtBlockLastName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLastName.Focus();
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

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFirstName.Text) && txtFirstName.Text.Length > 0)
            {
                txtBlockFirstName.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockFirstName.Visibility = Visibility.Visible;
            }
        }

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text.Length > 0)
            {
                txtBlockLastName.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockLastName.Visibility = Visibility.Visible;
            }
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

        

        private void txtBlockCountry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCountry.Focus();
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

        private void dgPassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            if(dataGrid.SelectedItems.Count > 0)
            {
                Passenger passenger = dataGrid.SelectedItem as Passenger;
                txtId.Text = passenger.Id.ToString();
                txtFirstName.Text = passenger.FirstName;
                txtLastName.Text = passenger.LastName;
                txtCountry.Text = passenger.Country;
                txtEmail.Text = passenger.Email;
                dtDate.Text = passenger.DateOfBirth.ToString();
                if (passenger.Gender.Equals("Male")) rbMale.IsChecked = true;
                if (passenger.Gender.Equals("Female")) rbFemale.IsChecked = true;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text.Length > 0)
            {

                if (string.IsNullOrEmpty(txtFirstName.Text)
                 || string.IsNullOrEmpty(txtLastName.Text)
                 || string.IsNullOrEmpty(txtEmail.Text)
                 || string.IsNullOrEmpty(txtCountry.Text)
                 || dtDate.SelectedDate == null)
                {
                    MessageBox.Show("You must input all fields!");
                }
                else
                {
                    if (DateTime.Today.AddYears(-2) >= dtDate.SelectedDate.Value)
                    {
                        Passenger passenger = passengerService.GetPassengerById(Int32.Parse(txtId.Text));
                        if (passenger != null)
                        {
                            passenger.Email = txtEmail.Text;
                            passenger.FirstName = txtFirstName.Text;
                            passenger.LastName = txtLastName.Text;
                            passenger.Country = txtCountry.Text;
                            passenger.DateOfBirth = DateOnly.FromDateTime(dtDate.SelectedDate.Value);
                            if (rbFemale.IsChecked == true) passenger.Gender = "Female";
                            if (rbMale.IsChecked == true) passenger.Gender = "Male";
                            passengerService.UpdatePassenger(passenger);
                            LoadPassenger();
                        }
                        else
                        {
                            MessageBox.Show("Passenger does not exist!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Must be over 2 years old!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose a passenger!");
            }
        }

        private void btnBookingInfor_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text.Length > 0)
            {
                PersonalBooking personalBooking = new PersonalBooking(txtId.Text);
                personalBooking.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Choose a passenger!");
            }
            
        }

        private void txtFilterEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilterEmail.Text) && txtFilterEmail.Text.Length > 0)
            {
                txtBlockFilterEmail.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockFilterEmail.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockFilterEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFilterEmail.Focus();
        }

        private void txtBlockAgeFrom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtAgeFrom.Focus();
        }

        private void txtBlockAgeTo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtAgeTo.Focus();
        }

        private void txtAgeTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgeTo.Text) && txtAgeTo.Text.Length > 0)
            {
                txtBlockAgeTo.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockAgeTo.Visibility = Visibility.Visible;
            }
        }

        private void txtAgeFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgeFrom.Text) && txtAgeFrom.Text.Length > 0)
            {
                txtBlockAgeFrom.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockAgeFrom.Visibility = Visibility.Visible;
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            int ageFrom = 0;
            int ageTo = 0;
            bool ageFromProvided = false;
            bool ageToProvided = false;

            if (!string.IsNullOrEmpty(txtAgeFrom.Text))
            {
                if (Int32.TryParse(txtAgeFrom.Text, out ageFrom))
                {
                    if (ageFrom < 0)
                    {
                        MessageBox.Show("Age(From) must be a positive integer!");
                        return;
                    }
                    ageFromProvided = true;
                }
                else
                {
                    MessageBox.Show("You must enter Age(From) as an integer!");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtAgeTo.Text))
            {
                if (Int32.TryParse(txtAgeTo.Text, out ageTo))
                {
                    if (ageTo < 0)
                    {
                        MessageBox.Show("Age(To) must be a positive integer!");
                        return;
                    }
                    ageToProvided = true;
                }
                else
                {
                    MessageBox.Show("You must enter Age(To) as an integer!");
                    return;
                }
            }

            if (ageFromProvided && ageToProvided && ageFrom > ageTo)
            {
                MessageBox.Show("Age(From) must be less than or equal to Age(To)!");
                return;
            }


            string name = txtFilterName.Text;
            string email = txtFilterEmail.Text;
            var foundPassenger = passengerService.FindPassengersByNameEmailAndAge(name, email,ageFrom,ageTo);
            dgPassenger.ItemsSource = null;
            dgPassenger.ItemsSource = foundPassenger;

        }

        private void btnCloseAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnExportData_Click(object sender, RoutedEventArgs e)
        {
            var savefileDialog = new SaveFileDialog();
            savefileDialog.Filter = "JSON File (*.json)|*.json";
            savefileDialog.DefaultExt = "*.json";
            List<Passenger> passengers = dgPassenger.ItemsSource as List<Passenger>;
            if(savefileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = savefileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();
                    switch (extension)
                    {
                        case ".json":
                            string jsonData = JsonSerializer.Serialize<List<Passenger>>(passengers);
                            File.WriteAllText(filePath, jsonData);
                            break;
                      
                        default:
                            throw new Exception("Unsupported file format");

                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //private void btnImportData_Click(object sender, RoutedEventArgs e)
        //{
        //    var dialog =  new OpenFileDialog();
        //    dialog.Filter = "JSON Files (*.json)|*.json";
        //    dialog.DefaultExt = "*.json";

        //    bool? result = dialog.ShowDialog();
        //    if (result == true)
        //    {
        //        try
        //        {
        //            string filePath = dialog.FileName;
        //            string extension = Path.GetExtension(filePath).ToLower();
        //            List<Passenger> passengers = new List<Passenger>();

        //            switch (extension)
        //            {
        //                case ".json":
        //                    string jsonData = File.ReadAllText(filePath);
        //                    passengers = JsonSerializer.Deserialize<List<Passenger>>(jsonData);
        //                    break;
        //                default:
        //                    throw new Exception("Unsupported file format");
        //            }

        //            dgPassenger.ItemsSource = passengers;
        //            lblChoosen.Content = filePath;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}

        //private void btnSavetoDtb_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult result = MessageBox.Show("Do you want to save data to database?", "Save data", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        var passengers = dgPassenger.ItemsSource as List<Passenger>;
        //        if (passengers != null)
        //        {
        //            passengerService.AddRange(passengers);
        //            MessageBox.Show("Successfully!");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Don't have data to save!");
        //        }
        //    }
            
        //}
    }
}
