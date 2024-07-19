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
using WPFProject;
using WPFProject2;

namespace WPFProject3
{
    /// <summary>
    /// Interaction logic for UpdateBookingWindow.xaml
    /// </summary>
    public partial class UpdateBookingWindow : Window
    {
        private readonly string bookingId;
        private readonly IBookingPlatformService bookingPlatformService;
        private readonly IBookingService bookingService;
        public UpdateBookingWindow(string bookingId)
        {
            InitializeComponent();
            bookingPlatformService = new BookingPlatformService();
            bookingService = new BookingService();
            this.bookingId = bookingId;
            LoadBookingPlatform();
            LoadStatus();
            LoadBooking();
        }


        private void LoadBooking()
        {
            Booking booking = bookingService.GetBookingById(Int32.Parse(bookingId));
            txtId.Text = bookingId;
            cbBookingPlatform.SelectedValue = booking.BookingPlatformId;
            cbStatus.SelectedValue = booking.Status;
        }

        private void LoadBookingPlatform()
        {
            var bookingPlatform = bookingPlatformService.GetAll();
            cbBookingPlatform.ItemsSource = bookingPlatform.OrderBy(b => b.Name);
            cbBookingPlatform.SelectedValuePath = "Id";
            cbBookingPlatform.DisplayMemberPath = "Name";


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
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = bookingService.GetBookingById(Int32.Parse(bookingId));
            booking.BookingPlatformId = Int32.Parse(cbBookingPlatform.SelectedValue.ToString());
            booking.Status = bool.Parse(cbStatus.SelectedValue.ToString());
            bookingService.UpdateBooking(booking);
            this.Close();
            MessageBox.Show("Update successful!");
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is BookingHistoryWindow)
                {
                    window.Show();
                    return;
                }
                else if(window is PersonalBooking)
                {
                    window.Show();
                    return;
                }else if(window is FlightDetailWindow)
                {
                    window.Show();
                }
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
