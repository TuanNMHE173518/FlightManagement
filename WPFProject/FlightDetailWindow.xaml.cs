using BussinessObjects;
using DataAccessLayer;
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
using WPFProject2;
using WPFProject3;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for FlightDetailWindow.xaml
    /// </summary>
    public partial class FlightDetailWindow : Window
    {
        private readonly int flightId;
        private readonly IBookingService bookingService;
        private readonly IFlightService flightService;
        private ObservableCollection<BookingInfoDTO> bookingInfos;
        private int currentPage = 1;
        private int itemPerPages = 50;
        public FlightDetailWindow(int flightId)
        {
            this.flightId = flightId;
            InitializeComponent();
            bookingService = new BookingService();
            flightService = new FlightService();
            lblBookinghistory.Content = $"Booking History of Flight: {flightId}";
            LoadBookingHistorys();
            LoadStatus();
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
            bookingInfos = new ObservableCollection<BookingInfoDTO>(bookingService.FindByAirlineAirportAnddateAndFlightId(null, null, null, "", "", flightId).OrderByDescending(b => b.BookingTime));
            DisplayPage(currentPage);

        }

        private void DisplayPage(int page)
        {
            int start = (page - 1) * itemPerPages;
            var bookingfound = bookingInfos.Skip(start).Take(itemPerPages).ToList();
            lvBookingHistory.ItemsSource = null;
            lvBookingHistory.ItemsSource = bookingfound;
            lblTotalPage.Content = $"of {Math.Ceiling((double)bookingInfos.Count / itemPerPages)}";
            txtPageNumber.Text = page.ToString();

        }

        private void LoadStatus()
        {
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            string name = txtName.Text;
            string status = cbStatus.SelectedValue.ToString();
            DateTime? departureDate = null;
            DateTime? arrivalDate = null;
            DateTime? bookingDate = null;
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
            var foundBooking = bookingService.FindByAirlineAirportAnddateAndFlightId(departureDate, arrivalDate, bookingDate, name, status, flightId);
            lvBookingHistory.ItemsSource = null;
            lvBookingHistory.ItemsSource = foundBooking;

        }

        private void btnSesetFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadBookingHistorys();
            dtBook.SelectedDate = null;
            dtDeparture.SelectedDate = null;
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
                if (window is BookingWindow)
                {
                    window.Show();
                }else if (window is CreateFlightWindow)
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
                Flight flight = flightService.GetFlightById(flightId);
                if (flight.ArrivalTime.Value.AddHours(1) < DateTime.Now)
                {
                    MessageBox.Show("Flight has been finished!", "Flight finished", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    UpdateBookingWindow updateBookingWindow = new UpdateBookingWindow(button.Tag.ToString());
                    updateBookingWindow.Show();
                    this.Hide();
                }
                
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

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayPage(currentPage);
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < Math.Ceiling((double)bookingInfos.Count / itemPerPages))
            {
                currentPage++;
                DisplayPage(currentPage);
            }
        }

        private void txtPageNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(Int32.TryParse(e.Text, out int pageNumber) && (pageNumber <=0 || pageNumber > Math.Ceiling((double)bookingInfos.Count / itemPerPages)))
            {
                e.Handled = true;
            }
        }

        private void txtPageNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(txtPageNumber.Text, out int pageNumber))
            {
                DisplayPage(pageNumber);
            }
        }
    }
}
