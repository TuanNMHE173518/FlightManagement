using BussinessObjects;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFProject2;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AccountMember accountMember;
        public MainWindow(AccountMember account)
        {   
            this.accountMember = account;
            InitializeComponent();
            AuthorizationUser(account);
            lblProfile.Content = account.Email;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LoginWindow)
                {
                    window.Show();
                }
            }


        }

        private void btnManageAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AccountWindow accountWindow = new AccountWindow(accountMember);
            accountWindow.Show();
            
        }

        private void AuthorizationUser(AccountMember account)
        {
            if(account.Role.Equals("Admin") || account.Role.Equals("Super Admin"))
            {
                ((Button)btnManageAccount).Visibility = Visibility.Visible;
                ((Button)btnCreateFlight).Visibility = Visibility.Visible;
                
            }
            else
            {
                ((Button)btnManageAccount).Visibility = Visibility.Collapsed;
                ((Button)btnCreateFlight).Visibility = Visibility.Collapsed;
                

            }
        }

        private void btnManageBooking_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.Show();
        }

        private void btnCreateFlight_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreateFlightWindow createFlightWindow = new CreateFlightWindow();
            createFlightWindow.Show();
        }

        private void btnManagePassenger_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManagePassengerWindow managePassengerWindow = new ManagePassengerWindow();
            managePassengerWindow.Show();
        }

        private void btnManageAirport_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageAirportWindow manageAirportWindow = new ManageAirportWindow();
            manageAirportWindow.Show();
        }

        private void btnManageAirLine_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageAirlineWindow manageAirlineWindow = new ManageAirlineWindow();
            manageAirlineWindow.Show();
        }

        private void btnManagePlatform_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManagePlatformWindow managePlatformWindow = new ManagePlatformWindow();
            managePlatformWindow.Show();
        }

        
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            if (image != null && image.ContextMenu != null)
            {
                var contextMenu = image.ContextMenu;

                Point relativePoint = image.TransformToAncestor(this)
                                  .Transform(new Point(0, 0));
                contextMenu.HorizontalOffset = relativePoint.X;
                contextMenu.VerticalOffset = relativePoint.Y + image.ActualHeight;
                contextMenu.Placement = PlacementMode.RelativePoint;
                contextMenu.PlacementTarget = this;
                contextMenu.IsOpen = true;


            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(accountMember);
            changePasswordWindow.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow(accountMember);
            profileWindow.Show();
            this.Hide();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to logout?", "Logout!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

      

        
    }
}