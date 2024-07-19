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
        private int CurrentPage = 1;
        private int ItemsPerPage = 20;
        private int TotalPage;
        private bool isSearch = false;
        private string searchName;
        private bool searchStatus;
        private DateTime? searchDepartureDate;
        private DateTime? searchArrivalDate;
        private DateTime? searchBookingDate;
        private List<BookingInfoDTO> bookinginfor;
        public BookingHistoryWindow()
        {
            InitializeComponent();
            bookingService = new BookingService();
            airlineService = new AirlineService();
            airportService = new AirportService();
            bookinginfor = bookingService.GetBookingInfos().OrderByDescending(b => b.BookingTime).ToList();
            cmbItemsPerPage.ItemsSource = new[] { 5, 10, 20, 50, 100 };
            cmbItemsPerPage.SelectedValue = 20;
            var statusOptions = new List<MappingStatus>
            {
                new MappingStatus(){ Value = true, DisplayName = "Normal"},
                new MappingStatus(){ Value = false, DisplayName = "Cancelled"}
            };
            cbStatus.ItemsSource = statusOptions;
            cbStatus.SelectedValue = true;
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
            TotalPage = (int)Math.Ceiling((double)bookinginfor.Count / ItemsPerPage);
            if (CurrentPage > TotalPage)
            {
                CurrentPage = TotalPage;
            }
            lvBookingHistory.ItemsSource = bookinginfor.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
            txtPageNumber.Text = CurrentPage.ToString();
            txtTotalPage.Text = "of " + TotalPage;

            cbStatus.SelectedValuePath = "Value";
            cbStatus.DisplayMemberPath = "DisplayName";
        }



        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            searchName = name;
            bool status = bool.Parse(cbStatus.SelectedValue.ToString());
            searchStatus = status;
            DateTime? departureDate = null;
            DateTime? arrivalDate = null;
            DateTime? bookingDate = null;
            if (dtDeparture.SelectedDate != null)
            {
                departureDate = dtDeparture.SelectedDate.Value;
                searchDepartureDate = dtDeparture.SelectedDate.Value;
            }
            if (dtArrival.SelectedDate != null)
            {
                arrivalDate = dtArrival.SelectedDate.Value;
                searchArrivalDate = dtArrival.SelectedDate.Value;
            }
            if (dtBook.SelectedDate != null)
            {
                bookingDate = dtBook.SelectedDate.Value;
                searchBookingDate = dtBook.SelectedDate.Value;
            }
            var foundBooking = bookingService.FindByAirlineAirportAnddate(departureDate, arrivalDate, bookingDate, name, status);
            lvBookingHistory.ItemsSource = null;
            bookinginfor = foundBooking;
            CurrentPage = 1;
            LoadBookingHistorys();
            isSearch = true;
        }

        private void btnSesetFilter_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = 1;
            bookinginfor = bookingService.GetBookingInfos().OrderByDescending(b => b.BookingTime).ToList();
            LoadBookingHistorys();
            dtBook.SelectedDate = null;
            dtArrival.SelectedDate = null;
            dtDeparture.SelectedDate = null; ;
            txtName.Text = string.Empty;
            cbStatus.SelectedValue = true;
            isSearch = false;
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
                if (isSearch)
                {
                    bookinginfor = bookingService.FindByAirlineAirportAnddate(searchDepartureDate, searchArrivalDate, searchBookingDate, searchName, searchStatus);
                }
                else
                {
                    bookinginfor = bookingService.GetBookingInfos().OrderByDescending(b => b.BookingTime).ToList();
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
                if (isSearch)
                {
                    bookinginfor = bookingService.FindByAirlineAirportAnddate(searchDepartureDate, searchArrivalDate, searchBookingDate, searchName, searchStatus);
                }
                else
                {
                    bookinginfor = bookingService.GetBookingInfos().OrderByDescending(b => b.BookingTime).ToList();
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

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to change status of this booking?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int bookingId = Int32.Parse(button.Tag.ToString());
                    Booking booking = bookingService.GetBookingById(bookingId);
                    booking.Status = !booking.Status;
                    bookingService.UpdateBooking(booking);
                    bookinginfor = bookingService.GetBookingInfos().OrderByDescending(b => b.BookingTime).ToList();
                    if (isSearch)
                    {
                        bookinginfor = bookingService.FindByAirlineAirportAnddate(searchDepartureDate, searchArrivalDate, searchBookingDate, searchName, searchStatus);
                    }
                    else
                    {
                        bookinginfor = bookingService.GetBookingInfos().OrderByDescending(b => b.BookingTime).ToList();
                    }
                    LoadBookingHistorys();
                }
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadBookingHistorys();
            }
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
                    LoadBookingHistorys();
                }

            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage < TotalPage)
            {
                CurrentPage++;
                LoadBookingHistorys();
            }
        }

        private void cmbItemsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemsPerPage = Int32.Parse(cmbItemsPerPage.SelectedValue.ToString());
            LoadBookingHistorys();
        }

    }
}