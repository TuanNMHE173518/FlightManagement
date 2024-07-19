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
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {

        private readonly IFlightService flightService;
        private readonly IAirportService airportService;
        private readonly IAirlineService airlineService;
        public BookingWindow()
        {
            InitializeComponent();
            flightService = new FlightService();
            airportService = new AirportService();
            airlineService = new AirlineService();
            LoadFlights();
            LoadAirLines();
            LoadAirports();
        }

        private void LoadAirLines() 
        {
            cbAirline.ItemsSource = null;
            var airlines = airlineService.GetAllAirlines();
            airlines.Add(new Airline() { Id = 0 , Name = "All" });
            cbAirline.ItemsSource = airlines.OrderBy(a => a.Id);
            cbAirline.SelectedValuePath = "Id";
            cbAirline.DisplayMemberPath = "Name";
            cbAirline.SelectedValue = 0;
        
        }


        private void LoadAirports()
        {
            cbFrom.ItemsSource = null;
            cbTo.ItemsSource = null;
            var airports = airportService.GetAllAirports();
            airports.Add(new Airport() {  Id = 0 , Name = "All" });
            cbFrom.ItemsSource = airports.OrderBy(a => a.Id);
            cbTo.ItemsSource = airports.OrderBy(a => a.Id);
            cbTo.SelectedValuePath = "Id";
            cbTo.DisplayMemberPath = "Name";
            cbTo.SelectedValue = 0;
            cbFrom.SelectedValuePath = "Id";
            cbFrom.DisplayMemberPath = "Name";
            cbFrom.SelectedValue = 0;
        }

        private void LoadFlights()
        {
            lvFligt.ItemsSource = null;
            var flights = flightService.GetAllFlights().Where(f => !(f.ArrivalTime.HasValue && f.ArrivalTime.Value.AddHours(1) < DateTime.Now)).OrderByDescending(f => f.DepartureTime);
            lvFligt.ItemsSource = flights;

            var statusOptions = new List<MappingStatus>
            {
                new MappingStatus(){ Value = true, DisplayName = "Normal"},
                new MappingStatus(){ Value = false, DisplayName = "Cancelled"}
            };
            cbStatus.ItemsSource = statusOptions;
            cbStatus.SelectedValuePath = "Value";
            cbStatus.DisplayMemberPath = "DisplayName";
            cbStatus.SelectedValue = true;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int airlineId = Int32.Parse(cbAirline.SelectedValue.ToString());
            int from = Int32.Parse(cbFrom.SelectedValue.ToString());
            int to = Int32.Parse(cbTo.SelectedValue.ToString());
            string status = cbStatus.SelectedValue.ToString();
            DateTime? departureDate = null;
            DateTime? arrivalDate = null;
            if(dtDeparture.SelectedDate != null)
            {
                departureDate = dtDeparture.SelectedDate.Value;
            }
            if (dtArrival.SelectedDate != null)
            {
                arrivalDate = dtArrival.SelectedDate.Value;
            }
            var foundFlight = flightService.FindByAirlineAirportAnddate(from,to,airlineId,departureDate, arrivalDate, status).Where(f => !(f.ArrivalTime.HasValue && f.ArrivalTime.Value.AddHours(1) < DateTime.Now)).OrderBy(f => f.DepartureTime);
            lvFligt.ItemsSource = null;
            lvFligt.ItemsSource = foundFlight;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Flight flight= flightService.GetFlightById(Int32.Parse(button.Tag.ToString()));
                if (flight.ArrivalTime.Value.AddHours(1) < DateTime.Now)
                {
                    MessageBox.Show("Flight has been finished!","Flight finished",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    BookingFlightWindow bookingFlightWindow = new BookingFlightWindow(button.Tag.ToString());
                    bookingFlightWindow.Show();
                    this.Hide();
                }

                
            }
        }

        private void btnBookingHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookingHistoryWindow bookingHistoryWindow = new BookingHistoryWindow();
            bookingHistoryWindow.Show();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            LoadAirLines();
            LoadAirports();
            LoadFlights();
            cbStatus.SelectedValue = true;
            cbFrom.SelectedValue = 0;
            cbTo.SelectedValue = 0;
            cbAirline.SelectedValue = 0;
            dtDeparture.SelectedDate = null;
        }
        private void btnCloseAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            LoadFlights();
            LoadAirLines();
            LoadAirports();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null)
            {
                Flight flight = item.DataContext as Flight;
                FlightDetailWindow flightDetailWindow = new FlightDetailWindow(flight.Id);
                flightDetailWindow.Show();
                this.Hide();
            }

        }
    }
}
