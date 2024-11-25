using System.Windows;
using System.Windows.Controls;

namespace CafeOrderManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MenuPage());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MenuPage());
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrdersPage());
        }
    }
}
