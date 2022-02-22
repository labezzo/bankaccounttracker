namespace BAT.AvaloniaFrontend
{
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Interactivity;
    using Avalonia.Markup.Xaml;
    using BAT.Core.Services;
    using System;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "Hello, Avalonia!";

            IJsonService jsonService = new JsonService();
            jsonService.AddAccount("Comdirect", "Girokonto", 123.45);
        }

        public void buttonBooking_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "BOOOOOOOOOOKED";

            IJsonService jsonService = new JsonService();
            var account = jsonService.GetAccount("Comdirect", "Girokonto");

            if (account != null)
            {
                jsonService.AddBooking(account, 1.00, DateTime.Now, "Something");
            }
        }

        public void buttonBookingsByAccount_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            IJsonService jsonService = new JsonService();
            var account = jsonService.GetAccount("Comdirect", "Girokonto");

            if (account != null)
            {
                var bookings = jsonService.GetAllBookingsByAccount(account);
            }
        }
    }
}
