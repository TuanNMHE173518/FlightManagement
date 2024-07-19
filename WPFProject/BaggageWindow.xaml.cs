using BussinessObjects;
using Microsoft.IdentityModel.Tokens;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using WPFProject;

namespace WPFProject2
{
    /// <summary>
    /// Interaction logic for BaggageWindow.xaml
    /// </summary>
    public partial class BaggageWindow : Window
    {
        private readonly IBaggageService baggageService;
        private readonly string bookingId;
        private readonly IBookingService bookingService;
        public BaggageWindow(string bookingId)
        {
            InitializeComponent();
            baggageService = new BaggageService();
            bookingService = new BookingService();
            this.bookingId = bookingId;
            LoadBaggage();
        }

        private void LoadBaggage()
        {
            lvBaggage.ItemsSource = null;
            var foundBagge = baggageService.GetBaggagesByBookingID(Int32.Parse(bookingId));
            lvBaggage.ItemsSource = foundBagge;
            Booking booking = bookingService.GetBookingById(Int32.Parse(bookingId));
            //txtBooking.Text = bookingId;
            //txtPassenger.Text = booking.Passenger.FirstName + " "+ booking.Passenger.LastName;
            
            if (foundBagge.IsNullOrEmpty()) {
                lvBaggage.Visibility = Visibility.Collapsed;
            }
            else
            {
                lvBaggage.Visibility = Visibility.Visible;
            }
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = bookingService.GetBookingById(Int32.Parse(bookingId));
            if(booking.Status == false) {
                MessageBox.Show($"Booking with ID: {bookingId} has been cancelled!");
                return;
            }else if (booking.Flight.ArrivalTime.Value.AddHours(1) < DateTime.Now)
            {
                MessageBox.Show("Flight has been finished!", "Flight finished", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(cbBaggageType.SelectedValue != null)
            {
                Decimal weight = 0;
                Decimal baggageType = 0;
                foreach (ComboBoxItem cb in cbBaggageType.Items)
                {
                    if (cb.IsSelected == true)
                    {
                        baggageType = Decimal.Parse(cb.Tag.ToString());
                    }
                }
                if (Decimal.TryParse(txtWeight.Text, out weight) && weight > 0 && weight <= baggageType)
                {
                    Decimal totalBaggage = baggageService.GetBaggagesByBookingID(Int32.Parse(bookingId)).Sum(b => b.WeightInKg).Value;
                    if ((totalBaggage + weight) <= 50)
                    {

                        Baggage baggage = new Baggage();
                        baggage.WeightInKg = weight;
                        baggage.BookingId = Int32.Parse(bookingId);

                        baggageService.AddBaggage(baggage);
                        LoadBaggage();
                    }
                    else
                    {
                        MessageBox.Show($"The total baggage weight of this booking is greater than 50 kg!");
                    }
                }
                else
                {
                    MessageBox.Show($"You must input a number > 0 and < {baggageType}!");

                }
            }
            else
            {
                MessageBox.Show("You must choose baggage type!");

            }


        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = bookingService.GetBookingById(Int32.Parse(bookingId));
            
            if (booking.Status == false)
            {
                MessageBox.Show($"Booking with ID: {bookingId} has been cancelled!");
                return;
            }
            else if (booking.Flight.ArrivalTime.Value.AddHours(1) < DateTime.Now)
            {
                MessageBox.Show("Flight has been finished!", "Flight finished", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtBaggage.Text.Length > 0)
            {
                if (cbBaggageType.SelectedValue != null)
                {
                    int id = Int32.Parse(txtBaggage.Text);
                    Decimal weight = 0;
                    Decimal baggageType = 0;
                    foreach (ComboBoxItem cb in cbBaggageType.Items)
                    {
                        if (cb.IsSelected == true)
                        {
                            baggageType = Decimal.Parse(cb.Tag.ToString());
                        }
                    }

                    if (Decimal.TryParse(txtWeight.Text, out weight) && weight > 0 && weight <= baggageType)
                    {
                        Decimal totalBaggage = baggageService.GetBaggagesByBookingID(Int32.Parse(bookingId)).Sum(b => b.WeightInKg).Value;
                        if ((totalBaggage + weight ) <= 50) {

                            Baggage baggage = baggageService.GetBaggageById(id);
                            baggage.WeightInKg = weight;
                            baggageService.UpdateBaggage(baggage);
                            LoadBaggage();
                        }
                        else
                        {
                            MessageBox.Show($"The total baggage weight of this booking is greater than 50 kg!");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show($"You must input a number > 0 and < {baggageType}!");
                    }
                }
                else
                {
                    MessageBox.Show("You must choose baggage type!");

                }

            }
            else
            {
                MessageBox.Show("Choose an baggage!");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is BookingHistoryWindow)
                {
                    window.Show();
                }
                else if (window is PersonalBooking) 
                {
                    window.Show();
                }else if (window is FlightDetailWindow)
                {
                    window.Show();
                }
            }
        }

        private void txtBlockBaggageId_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtBaggage.Focus();
        }

        private void txtBlockBooking_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtBooking.Focus();
        }

        private void txtBlockPassenger_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassenger.Focus();
        }

        private void txtBlockWeight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtWeight.Focus();
        }

        private void txtWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight.Text) && txtWeight.Text.Length > 0)
            {
                txtBlockWeight.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockWeight.Visibility = Visibility.Visible;
            }
        }

        private void txtPassenger_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassenger.Text) && txtPassenger.Text.Length > 0)
            {
                txtBlockPassenger.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockPassenger.Visibility = Visibility.Visible;
            }
        }

        private void txtBooking_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBooking.Text) && txtBooking.Text.Length > 0)
            {
                txtBlockBooking.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockBooking.Visibility = Visibility.Visible;
            }
        }

        private void txtBaggage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBaggage.Text) && txtBaggage.Text.Length > 0)
            {
                txtBlockBaggageId.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockBaggageId.Visibility = Visibility.Visible;
            }
        }

        private void lvBaggage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            if(listView?.SelectedItems != null)
            {
                BaggageDTO baggageDTO = listView.SelectedItem as BaggageDTO;   
                if(baggageDTO != null)
                {
                    txtBaggage.Text = baggageDTO.Id.ToString();
                    txtBooking.Text = baggageDTO.BookingId.ToString();
                    txtPassenger.Text = baggageDTO.PassengerName;
                    txtWeight.Text = baggageDTO.WeightInKg.ToString();

                }
            }
        }

        private void btnDeleteBaggage_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = bookingService.GetBookingById(Int32.Parse(bookingId));
            if (booking.Status == false)
            {
                MessageBox.Show($"Booking with ID: {bookingId} has been cancelled!");
                return;
            }
            else if (booking.Flight.ArrivalTime.Value.AddHours(1) < DateTime.Now)
            {
                MessageBox.Show("Flight has been finished!", "Flight finished", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Button button = (Button)sender;
            MessageBoxResult result = MessageBox.Show("Do you want to delete this baggage?", "Delete Baggage", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                Baggage baggage = baggageService.GetBaggageById(Int32.Parse(button.Tag.ToString()));
                baggageService.DeleteBaggage(baggage);
                LoadBaggage();
            }
                
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
