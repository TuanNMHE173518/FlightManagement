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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using WPFProject;

namespace WPFProject2
{
    /// <summary>
    /// Interaction logic for CreateFlightWindow.xaml
    /// </summary>
    public partial class CreateFlightWindow : Window
    {
        private readonly IFlightService flightService;
        private readonly IAirportService airportService;
        private readonly IAirlineService airlineService;
        public CreateFlightWindow()
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
            var SearchAirlines = airlineService.GetAllAirlines();
            SearchAirlines.Add(new Airline() { Id = 0 , Name="All"});
            cbAirline.ItemsSource = airlines;
            cbAirline.SelectedValuePath = "Id";
            cbAirline.DisplayMemberPath = "Name";
            cbAirline.SelectedValue = null;
            cbSearchAirline.ItemsSource = null;
            cbSearchAirline.ItemsSource = SearchAirlines.OrderBy(a => a.Name);
            cbSearchAirline.SelectedValuePath = "Id";
            cbSearchAirline.DisplayMemberPath = "Name";
            cbSearchAirline.SelectedValue = 0;

        }


        private void LoadAirports()
        {
            cbFrom.ItemsSource = null;
            cbTo.ItemsSource = null;
            var airports = airportService.GetAllAirports();
            var SearchAirports = airportService.GetAllAirports();
            SearchAirports.Add(new Airport() { Id = 0, Name = "All" });
            cbFrom.ItemsSource = airports;
            cbTo.ItemsSource = airports;
            cbTo.SelectedValuePath = "Id";
            cbTo.DisplayMemberPath = "Name";
            cbTo.SelectedValue = null;
            cbFrom.SelectedValuePath = "Id";
            cbFrom.DisplayMemberPath = "Name";
            cbFrom.SelectedValue = null;
            cbSearchFrom.ItemsSource = null;
            cbSearchFrom.ItemsSource = SearchAirports.OrderBy(a => a.Name);
            cbSearchFrom.SelectedValuePath = "Id";
            cbSearchFrom.DisplayMemberPath = "Name";
            cbSearchFrom.SelectedValue = 0;
            cbSearchTo.ItemsSource = null;
            cbSearchTo.ItemsSource = SearchAirports.OrderBy(a => a.Name);
            cbSearchTo.SelectedValuePath = "Id";
            cbSearchTo.DisplayMemberPath = "Name";
            cbSearchTo.SelectedValue = 0;
        }

        private void LoadFlights()
        {
            lvFlight.ItemsSource = null;
            var flights = flightService.GetAllFlights().OrderByDescending(f => f.DepartureTime);
            lvFlight.ItemsSource = flights;
            dtDeparture.Value = null;
            dtArrival.Value = null;
            var statusOptions = new List<MappingStatus>
            {
                new MappingStatus(){ Value = true, DisplayName = "Normal"},
                new MappingStatus(){ Value = false, DisplayName = "Cancelled"}
            };
            cbSearchStatus.ItemsSource = statusOptions;
            cbSearchStatus.SelectedValuePath = "Value";
            cbSearchStatus.DisplayMemberPath = "DisplayName";
            cbSearchStatus.SelectedValue = true;
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(txtID.Text.Length > 0)
            {
                Flight flight = flightService.GetFlightById(Int32.Parse(txtID.Text));
                if (flight != null) {
                    flight.DepartingGate = txtDepartureGate.Text;
                    flight.ArrivingGate = txtArrivalGate.Text;
                    flight.DepartureTime = dtDeparture.Value;
                    flight.ArrivalTime = dtArrival.Value;
                    flight.DepartingAirport = Int32.Parse(cbFrom.SelectedValue.ToString());
                    flight.ArrivingAirport = Int32.Parse(cbTo.SelectedValue.ToString());
                    flight.AirlineId = Int32.Parse(cbAirline.SelectedValue.ToString());
                    flightService.UpdateFlight(flight);
                    LoadFlights();
                    LoadAirLines();
                    LoadAirports();
                }
                else
                {
                    MessageBox.Show("Flight dose not exist!");
                }

            }
            else
            {
                MessageBox.Show("You must select a Flight to update!");

            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Flight flight = new Flight();
            flight.DepartingGate = txtDepartureGate.Text;
            flight.ArrivingGate = txtArrivalGate.Text;
            flight.DepartureTime = dtDeparture.Value;
            flight.ArrivalTime = dtArrival.Value;
            flight.DepartingAirport = Int32.Parse(cbFrom.SelectedValue.ToString());
            flight.ArrivingAirport = Int32.Parse(cbTo.SelectedValue.ToString());
            flight.AirlineId = Int32.Parse(cbAirline.SelectedValue.ToString());
            flight.Status = true;
            flightService.AddFlight(flight);
            LoadFlights();
            LoadAirLines();
            LoadAirports();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListView listView = (ListView)lvFlight;
            if (listView?.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to cancel these flights?", "Cancle Flight", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    for (int i = 0; i < listView.SelectedItems.Count; i++)
                    {
                        Flight flight = listView.SelectedItems[i] as Flight;
                        if (flight != null)
                        {

                            flightService.UpdateFlightStatus(flight, false);

                        }
                    }

                    LoadFlights();
                    LoadAirLines();
                    LoadAirports();
                }
               
            }
            else
            {
                MessageBox.Show("You must select at least one flight to cancel!");
            }


            
        }

        private void lvFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listFlight = sender as ListView;
            if(listFlight.SelectedItem != null)
            {
                Flight flight = listFlight.SelectedItem as Flight;
                if (flight != null)
                {
                    txtID.Text = flight.Id.ToString();
                    cbFrom.SelectedValue = flight.DepartingAirport;
                    cbTo.SelectedValue = flight.ArrivingAirport;
                    cbAirline.SelectedValue = flight.AirlineId;
                    txtArrivalGate.Text = flight.ArrivingGate;
                    txtDepartureGate.Text = flight.DepartingGate;
                    dtDeparture.Value = flight.DepartureTime;
                    dtArrival.Value = flight.ArrivalTime;
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int airlineId = Int32.Parse(cbSearchAirline.SelectedValue.ToString());
            int from = Int32.Parse(cbSearchFrom.SelectedValue.ToString());
            int to = Int32.Parse(cbSearchTo.SelectedValue.ToString());
            bool status = bool.Parse(cbSearchStatus.SelectedValue.ToString());

            DateTime? departureDate = null;
            DateTime? arrivalDate = null;
            if (dtSearchDepature.Value != null)
            {
                departureDate = dtSearchDepature.Value;
            }
            if (dtSearchArrival.Value != null)
            {
                arrivalDate = dtSearchArrival.Value;
            }
            var foundFlight = flightService.FindByAirlineAirportAnddate(from,to, airlineId, departureDate, arrivalDate,status).OrderBy(d => d.DepartureTime);
            lvFlight.ItemsSource = null;
            lvFlight.ItemsSource = foundFlight;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            LoadFlights();
            LoadAirLines();
            LoadAirports();
            txtID.Text = "";
            cbAirline.SelectedValue = null;
            cbFrom.SelectedValue = null;
            cbTo.SelectedValue = null;
            dtArrival.Value = null;
            dtDeparture.Value = null;
            txtArrivalGate.Text = null;
            txtDepartureGate.Text = null;
            cbSearchFrom.SelectedValue = 0;
            cbSearchFrom.SelectedValue = 0;
            cbSearchStatus.SelectedValue = true;
            cbSearchAirline.SelectedValue = 0;
            dtSearchDepature.Value = null;


        }

        private void btnActive_Click(object sender, RoutedEventArgs e)
        {
            ListView listView = (ListView)lvFlight;
            if (listView?.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to activate these flights?", "Cancle Flight", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    for (int i = 0; i < listView.SelectedItems.Count; i++)
                    {
                        Flight flight = listView.SelectedItems[i] as Flight;
                        if (flight != null)
                        {
                            
                            flightService.UpdateFlightStatus(flight, true);
                        }
                    }

                    LoadFlights();
                    LoadAirLines();
                    LoadAirports();
                }

            }
            else
            {
                MessageBox.Show("You must select at least one flight to activate!");
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
