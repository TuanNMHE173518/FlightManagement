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
    /// Interaction logic for BookingFlightWindow.xaml
    /// </summary>
    public partial class BookingFlightWindow : Window
    {
        private readonly IBookingPlatformService bookingPlatformService;
        private readonly IBookingService bookingService;
        private readonly IPassengerService passengerService;
        private readonly string flightId;
        public BookingFlightWindow(string flightId)
        {
            InitializeComponent();
            this.flightId = flightId;
            bookingPlatformService = new BookingPlatformService();
            bookingService = new BookingService();
            passengerService = new PassengerService();
            LoadBookingPlatforms();
        }

        private void LoadBookingPlatforms()
        {
            cbBookingPlatform.ItemsSource = null;
            var bookingPlatform = bookingPlatformService.GetAll();
            cbBookingPlatform.ItemsSource = bookingPlatform;
            cbBookingPlatform.SelectedValuePath = "Id";
            cbBookingPlatform.DisplayMemberPath = "Name";
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void btnPassengerBooking_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFirstName.Text) 
                || string.IsNullOrEmpty(txtLastName.Text) 
                || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtCountry.Text)
                || dtDateofBirth.SelectedDate == null
                || cbBookingPlatform.SelectedValue == null)
            {
                MessageBox.Show("You must input all fields!");
            }
            else
            {
                if (DateTime.Today.AddYears(-2) >= dtDateofBirth.SelectedDate.Value)
                {
                    Booking booking = new Booking();
                    if (txtID.Text.Length > 0)
                    {
                        booking.PassengerId = Int32.Parse(txtID.Text);
                    }
                    else
                    {
                       
                        Passenger passenger = new Passenger();
                        passenger = passengerService.GetPassengerByEmail(txtEmail.Text);
                        if(passenger != null)
                        {
                            MessageBoxResult result = MessageBox.Show("The email already exists, do you want to save the passenger with the old information?", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                booking.PassengerId = passenger.Id;

                            }
                            else if (result == MessageBoxResult.No)
                            {
                                return;
                            }

                        }
                        else
                        {
                            Passenger passenger1 = new Passenger();

                            passenger1.FirstName = txtFirstName.Text;
                            passenger1.LastName = txtLastName.Text;
                            passenger1.Email = txtEmail.Text;
                            passenger1.Country = txtCountry.Text;
                            if (rbMale.IsChecked == true) passenger1.Gender = "Male";
                            if (rbFemale.IsChecked == true) passenger1.Gender = "Female";
                            passenger1.DateOfBirth = DateOnly.FromDateTime(dtDateofBirth.SelectedDate.Value);
                            passengerService.AddPassengers(passenger1);
                            booking.PassengerId = passenger1.Id;
                        }
                        
                        
                        
                    }
                    booking.FlightId = Int32.Parse(flightId);
                    booking.BookingPlatformId = Int32.Parse(cbBookingPlatform.SelectedValue.ToString());
                    booking.BookingTime = DateTime.Now;
                    booking.Status = true;
                    bookingService.AddBooking(booking);


                    this.Close();
                }
                else
                {
                    MessageBox.Show("Must be over 2 years old!");
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is BookingWindow)
                {
                    window.Show();
                }
            }
        }

        private void btnFindPassenger_Click(object sender, RoutedEventArgs e)
        {
            if(txtEmail.Text.Length >0)
            {
                Passenger passenger = passengerService.GetPassengerByEmail(txtEmail.Text);
                if (passenger != null)
                {
                    txtEmail.Text = passenger.Email;
                    txtFirstName.Text = passenger.FirstName;
                    txtLastName.Text = passenger.LastName;
                    txtID.Text = passenger.Id.ToString();
                    txtCountry.Text = passenger.Country;
                    dtDateofBirth.Text = passenger.DateOfBirth.ToString();
                    if(passenger.Gender.Equals("Male")) rbMale.IsChecked = true;
                    if(passenger.Gender.Equals("Female")) rbFemale.IsChecked= true;
                }
                else
                {
                    MessageBox.Show("Passenger dose not exist!");
                }
            }
            else
            {
                MessageBox.Show("Enter email to find passenger!");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = null;
            txtEmail.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;
            txtCountry.Text = null;
            cbBookingPlatform.SelectedValue = null;
            dtDateofBirth.SelectedDate = null;
        }

        private void btnCloseAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }


    
}
