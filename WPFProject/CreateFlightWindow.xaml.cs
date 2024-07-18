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
        private ObservableCollection<Flight> flights;
        private int currentPage;
        private int itemsPerPage;
        public CreateFlightWindow()
        {
            InitializeComponent();
            flightService = new FlightService();
            airportService = new AirportService();
            airlineService = new AirlineService();
            flights = new ObservableCollection<Flight>();
            currentPage = 1;
            itemsPerPage = 50;
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
            
            flights = new ObservableCollection<Flight>(flightService.FindByAirlineAirportAnddate(0,0,0,null,null,"All"));
            DisplayPage(currentPage);

            
           
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


        private void DisplayPage(int pageNumber)
        {
            int start = (pageNumber - 1) * itemsPerPage;
            var flightsfound = flights.Skip(start).Take(itemsPerPage).ToList();
            lvFlight.ItemsSource = flightsfound;
            txtPageInfo.Text = $"Page {currentPage} of {Math.Ceiling((double)flights.Count / itemsPerPage)}";
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
                if (dtDeparture.SelectedDate.HasValue &&
                    timeDeparture.SelectedTime.HasValue &&
                    dtArrival.SelectedDate.HasValue &&
                    timeArrival.SelectedTime.HasValue &&
                    cbFrom.SelectedValue != null &&
                    cbTo.SelectedValue != null &&
                    cbAirline.SelectedValue != null)
                {
                    Flight flight = flightService.GetFlightById(Int32.Parse(txtID.Text));
                    if (flight != null)
                    {
                        flight.DepartingGate = txtDepartureGate.Text;
                        flight.ArrivingGate = txtArrivalGate.Text;
                        flight.NumberPassengers = Int32.Parse(txtNumberPassengers.Text);
                        flight.DepartureTime = dtDeparture.SelectedDate.Value.AddHours(timeDeparture.SelectedTime.Value.Hour).AddMinutes(timeDeparture.SelectedTime.Value.Minute);
                        flight.ArrivalTime = dtArrival.SelectedDate.Value.AddHours(timeArrival.SelectedTime.Value.Hour).AddMinutes(timeArrival.SelectedTime.Value.Minute);
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
                        MessageBox.Show("Flight does not exist!");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill in all required fields.");
                }

            }
            else
            {
                MessageBox.Show("You must select a Flight to update!");

            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dtDeparture.SelectedDate.HasValue &&
                    timeDeparture.SelectedTime.HasValue &&
                    dtArrival.SelectedDate.HasValue &&
                    timeArrival.SelectedTime.HasValue &&
                    cbFrom.SelectedValue != null &&
                    cbTo.SelectedValue != null &&
                    cbAirline.SelectedValue != null)
            {
                Flight flight = new Flight();
                flight.DepartingGate = txtDepartureGate.Text;
                flight.ArrivingGate = txtArrivalGate.Text;
                flight.NumberPassengers = Int32.Parse(txtNumberPassengers.Text);
                flight.DepartureTime = dtDeparture.SelectedDate.Value.AddHours(timeDeparture.SelectedTime.Value.Hour).AddMinutes(timeDeparture.SelectedTime.Value.Minute);
                flight.ArrivalTime = dtArrival.SelectedDate.Value.AddHours(timeArrival.SelectedTime.Value.Hour).AddMinutes(timeArrival.SelectedTime.Value.Minute);
                flight.DepartingAirport = Int32.Parse(cbFrom.SelectedValue.ToString());
                flight.ArrivingAirport = Int32.Parse(cbTo.SelectedValue.ToString());
                flight.AirlineId = Int32.Parse(cbAirline.SelectedValue.ToString());
                flight.Status = true;
                flightService.AddFlight(flight);
                LoadFlights();
                LoadAirLines();
                LoadAirports();
            }
            else
            {
                MessageBox.Show("Please fill in all required fields.");
            }

           
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
                    txtNumberPassengers.Text = flight.NumberPassengers.ToString();
                    cbFrom.SelectedValue = flight.DepartingAirport;
                    cbTo.SelectedValue = flight.ArrivingAirport;
                    cbAirline.SelectedValue = flight.AirlineId;
                    txtArrivalGate.Text = flight.ArrivingGate;
                    txtDepartureGate.Text = flight.DepartingGate;
                    dtDeparture.SelectedDate = flight.DepartureTime;
                    if (flight.DepartureTime.HasValue)
                    {
                        dtDeparture.Text = flight.DepartureTime.Value.ToString("dd/MM/yyyy");
                    }

                   
                    dtArrival.SelectedDate = flight.ArrivalTime;
                    if (flight.ArrivalTime.HasValue)
                    {
                        dtArrival.Text = flight.ArrivalTime.Value.ToString("dd/MM/yyyy");
                    }

                    if (flight.DepartureTime.HasValue)
                    {
                        timeDeparture.SelectedTime = flight.DepartureTime;
                    }

                    if (flight.ArrivalTime.HasValue)
                    {
                        timeArrival.SelectedTime = flight.ArrivalTime;
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int airlineId = Int32.Parse(cbSearchAirline.SelectedValue.ToString());
            int from = Int32.Parse(cbSearchFrom.SelectedValue.ToString());
            int to = Int32.Parse(cbSearchTo.SelectedValue.ToString());
            

            DateTime? departureDate = null;
            DateTime? arrivalDate = null;
            if (dtSearchDepature.SelectedDate != null)
            {
                departureDate = dtSearchDepature.SelectedDate.Value;
            }
            if (dtSearchArrival.SelectedDate != null)
            {
                arrivalDate = dtSearchArrival.SelectedDate.Value;
            }
            flights = new ObservableCollection<Flight>(flightService.FindByAirlineAirportAnddate(from, to, airlineId, departureDate, arrivalDate, cbSearchStatus.SelectedValue.ToString()));
            currentPage = 1;
            DisplayPage(currentPage);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            LoadFlights();
            LoadAirLines();
            LoadAirports();
            txtID.Text = "";
            txtNumberPassengers.Text = "";
            cbAirline.SelectedValue = null;
            cbFrom.SelectedValue = null;
            cbTo.SelectedValue = null;
            dtArrival.SelectedDate = null;
            dtDeparture.SelectedDate = null;
            timeArrival.SelectedTime = null;
            timeDeparture.SelectedTime = null;
            txtArrivalGate.Text = null;
            txtDepartureGate.Text = null;
            cbSearchFrom.SelectedValue = 0;
            cbSearchFrom.SelectedValue = 0;
            cbSearchStatus.SelectedValue = true;
            cbSearchAirline.SelectedValue = 0;
            dtSearchDepature.SelectedDate = null;


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

        

        private void txtPageNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPageNumber.Text) && txtPageNumber.Text.Length > 0)
            {
                txtBlockPageNumber.Visibility = Visibility.Collapsed;

            }
            else
            {
                txtBlockPageNumber.Visibility = Visibility.Visible;
            }
        }

        private void txtBlockPageNumber_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPageNumber.Focus();
        }

        
        

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < Math.Ceiling((double)flights.Count / itemsPerPage))
            {
                currentPage++;
                DisplayPage(currentPage);
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if(currentPage > 1)
            {
                currentPage--;
                DisplayPage(currentPage);
            }
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(txtPageNumber.Text, out int pagenum))
            {
                int totalPages = (int)Math.Ceiling((double)flights.Count / itemsPerPage);
                if(pagenum >0 && pagenum < totalPages)
                {
                    currentPage = pagenum;
                    DisplayPage(currentPage);

                }
                else
                {
                    MessageBox.Show($"Please enter a valid page number between 1 and {totalPages}.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid page number.");
            }
        }

        private void txtNumberPassengers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!Int32.TryParse(e.Text, out int num))
            {
                e.Handled = true;
            }
        }
    }
}
