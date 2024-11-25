using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using CafeOrderManager.Pages;

namespace CafeOrderManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            MainFrame.Navigate(new CafeOrderManager.Pages.MenuPage());
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CafeOrderManager.Pages.MenuPage());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CafeOrderManager.Pages.MenuPage());
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CafeOrderManager.Pages.OrdersPage());
        }

        private void ThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            
            if (ThemeToggle.IsChecked == true)
            {
                theme.SetBaseTheme(Theme.Dark);
            }
            else
            {
                theme.SetBaseTheme(Theme.Light);
            }
            
            paletteHelper.SetTheme(theme);
        }
    }
}
