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
        private readonly IJsonService _jsonService;

        public MainWindow() : this(new JsonService()) { }

        public MainWindow(IJsonService jsonService)
        {
            InitializeComponent();
            _jsonService = jsonService;
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

            _jsonService.AddAccount("Comdirect", "Girokonto", 123.45);
        }

        public void buttonBooking_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "BOOOOOOOOOOKED";

            var account = _jsonService.GetAccount("Comdirect", "Girokonto");

            if (account != null)
            {
                _jsonService.AddBooking(account, 1.00, DateTime.Now, "Something");
            }
        }

        public void buttonBookingsByAccount_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            var account = _jsonService.GetAccount("Comdirect", "Girokonto");

            if (account != null)
            {
                var bookings = _jsonService.GetAllBookingsByAccount(account);
            }
        }
    }
}
