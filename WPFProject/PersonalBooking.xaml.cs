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
using WPFProject;
using WPFProject3;

namespace WPFProject2
{
    /// <summary>
    /// Interaction logic for PersonalBooking.xaml
    /// </summary>
    public partial class PersonalBooking : Window
    {
        private readonly IBookingService bookingService;
        private readonly IAirportService airportService;
        private readonly IAirlineService airlineService;
        private readonly IPassengerService passengerService;
        private readonly string passengerId;
        public PersonalBooking(string passengerId)
        {
            InitializeComponent();
            bookingService = new BookingService();
            airlineService = new AirlineService();
            airportService = new AirportService();
            passengerService = new PassengerService();
            this.passengerId = passengerId;
            Passenger passenger = passengerService.GetPassengerById(Int32.Parse(passengerId));
            lblTitle.Content = $"Booking History Of {passenger.FirstName} {passenger.LastName}({passengerId})";
            LoadBookingHistorys();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void LoadBookingHistorys()
        {
            lvBookingHistory.ItemsSource = null;
            var bookinginfor = bookingService.GetPersonalBookingInfor(Int32.Parse(passengerId)).OrderByDescending(b => b.BookingTime);
            lvBookingHistory.ItemsSource = bookinginfor;
            cbAirline.ItemsSource = null;
            cbFrom.ItemsSource = null;
            cbTo.ItemsSource = null;
            var statusOptions = new List<MappingStatus>
            {
                new MappingStatus(){ Value = true, DisplayName = "Normal"},
                new MappingStatus(){ Value = false, DisplayName = "Cancelled"}
            };
            cbStatus.ItemsSource = statusOptions;
            cbStatus.SelectedValuePath = "Value";
            cbStatus.DisplayMemberPath = "DisplayName";
            cbStatus.SelectedValue = true;
            LoadAirLines();
            LoadAirports();



        }

        private void LoadAirLines()
        {
            cbAirline.ItemsSource = null;
            var airlines = airlineService.GetAllAirlines();
            cbAirline.ItemsSource = airlines.OrderBy(a => a.Name);
            cbAirline.SelectedValuePath = "Code";
            cbAirline.DisplayMemberPath = "Name";

        }


        private void LoadAirports()
        {
            cbFrom.ItemsSource = null;
            cbTo.ItemsSource = null;
            var airports = airportService.GetAllAirports();
            cbFrom.ItemsSource = airports.OrderBy(a => a.Name);
            cbTo.ItemsSource = airports.OrderBy(a => a.Name);
            cbTo.SelectedValuePath = "Code";
            cbTo.DisplayMemberPath = "Name";
            cbFrom.SelectedValuePath = "Code";
            cbFrom.DisplayMemberPath = "Name";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string airlineId = cbAirline.SelectedValue == null ? null : cbAirline.SelectedValue.ToString();
            string from = cbFrom.SelectedValue == null ? null : cbFrom.SelectedValue.ToString();
            string to = cbTo.SelectedValue == null ? null : cbTo.SelectedValue.ToString();
            bool status = bool.Parse(cbStatus.SelectedValue.ToString());
            DateTime? departureDate = null;
            DateTime? bookingDate = null;
            DateTime? arrivalDate = null;
            if (dtDeparture.SelectedDate != null)
            {
                departureDate = dtDeparture.SelectedDate.Value;
            }
            if (dtArrival.SelectedDate != null)
            {
                arrivalDate = dtArrival.SelectedDate.Value;
            }
            if (dtBook.SelectedDate != null)
            {
                bookingDate = dtBook.SelectedDate.Value;
            }
            var foundBooking = bookingService.FindPersonalBookingByAirlineAirportAnddate(from, to, airlineId, departureDate,arrivalDate, bookingDate, status, Int32.Parse(passengerId));
            lvBookingHistory.ItemsSource = null;
            lvBookingHistory.ItemsSource = foundBooking;

        }

        private void btnSesetFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadBookingHistorys();
            dtBook.SelectedDate = null;
            dtDeparture.SelectedDate = null;
            cbAirline.SelectedValue = null;
            cbFrom.SelectedValue = null;
            cbTo.SelectedValue = null;
            cbStatus.SelectedValue = true;
        }



        private void btnManageBaggage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            this.Hide();
            BaggageWindow baggageWindow = new BaggageWindow(button.Tag.ToString());
            baggageWindow.Show();
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is ManagePassengerWindow)
                {
                    window.Show();
                }
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            LoadBookingHistorys();


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ListView listView = (ListView)lvBookingHistory;
            if (listView?.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView.SelectedItems.Count; i++)
                {
                    BookingInfoDTO bookingInfoDTO = (BookingInfoDTO)listView.SelectedItems[i];
                    if (bookingInfoDTO != null)
                    {
                        Booking booking = bookingService.GetBookingById(bookingInfoDTO.id);
                        booking.Status = false;
                        bookingService.UpdateBooking(booking);
                    }
                }

                LoadBookingHistorys();
            }
            else
            {
                MessageBox.Show("You must select at least one booking to cancel!");
            }
        }

        private void btnUpdateBooking_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                UpdateBookingWindow updateBookingWindow = new UpdateBookingWindow(button.Tag.ToString());
                updateBookingWindow.Show();
                this.Hide();
            }
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            ListView listView = (ListView)lvBookingHistory;
            if (listView?.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView.SelectedItems.Count; i++)
                {
                    BookingInfoDTO bookingInfoDTO = (BookingInfoDTO)listView.SelectedItems[i];
                    if (bookingInfoDTO != null)
                    {
                        Booking booking = bookingService.GetBookingById(bookingInfoDTO.id);
                        booking.Status = true;
                        bookingService.UpdateBooking(booking);
                    }
                }

                LoadBookingHistorys();
            }
            else
            {
                MessageBox.Show("You must select at least one booking to ACTIVATE!");
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
