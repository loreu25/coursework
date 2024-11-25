using System.Windows;
using CafeOrderManager.Models;

namespace CafeOrderManager
{
    public partial class MenuItemDialog : Window
    {
        public MenuDish MenuDish { get; private set; }

        public MenuItemDialog(MenuDish menuDish = null)
        {
            InitializeComponent();
            MenuDish = menuDish ?? new MenuDish { IsAvailable = true };
            DataContext = MenuDish;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MenuDish.Name))
            {
                MessageBox.Show("Пожалуйста, введите название блюда", "Ошибка");
                return;
            }

            if (MenuDish.Price <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную цену", "Ошибка");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
