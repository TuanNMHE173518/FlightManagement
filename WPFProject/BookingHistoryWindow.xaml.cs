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
using WPFProject2;
using WPFProject3;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for BookingHistoryWindow.xaml
    /// </summary>
    public partial class BookingHistoryWindow : Window
    {
        private readonly IBookingService bookingService;
        private readonly IAirportService airportService;
        private readonly IAirlineService airlineService;
        public BookingHistoryWindow()
        {
            InitializeComponent();
            bookingService = new BookingService();
            airlineService = new AirlineService();
            airportService = new AirportService();
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
            var bookinginfor = bookingService.GetBookingInfos().OrderByDescending(b => b.BookingTime).Take(100);
            lvBookingHistory.ItemsSource = bookinginfor;
            
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
            bool status = bool.Parse(cbStatus.SelectedValue.ToString());
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
            var foundBooking = bookingService.FindByAirlineAirportAnddate(departureDate, arrivalDate, bookingDate,name,status);
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
                for(int i = 0; i < listView.SelectedItems.Count; i++)
                {
                    BookingInfoDTO bookingInfoDTO = (BookingInfoDTO)listView.SelectedItems[i];
                    if(bookingInfoDTO != null)
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
