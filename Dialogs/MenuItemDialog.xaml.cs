using System.Windows;
using CafeOrderManager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Diagnostics;

namespace CafeOrderManager
{
    public partial class MenuItemDialog : Window, INotifyPropertyChanged
    {
        public string DishName
        {
            get { return (string)GetValue(DishNameProperty); }
            set { SetValue(DishNameProperty, value); }
        }

        public static readonly DependencyProperty DishNameProperty =
            DependencyProperty.Register("DishName", typeof(string), typeof(MenuItemDialog), new PropertyMetadata(string.Empty));

        public string DishDescription
        {
            get { return (string)GetValue(DishDescriptionProperty); }
            set { SetValue(DishDescriptionProperty, value); }
        }

        public static readonly DependencyProperty DishDescriptionProperty =
            DependencyProperty.Register("DishDescription", typeof(string), typeof(MenuItemDialog), new PropertyMetadata(string.Empty));

        public decimal DishPrice
        {
            get { return (decimal)GetValue(DishPriceProperty); }
            set { SetValue(DishPriceProperty, value); }
        }

        public static readonly DependencyProperty DishPriceProperty =
            DependencyProperty.Register("DishPrice", typeof(decimal), typeof(MenuItemDialog), new PropertyMetadata(0m));

        public string DishCategory
        {
            get { return (string)GetValue(DishCategoryProperty); }
            set { SetValue(DishCategoryProperty, value); }
        }

        public static readonly DependencyProperty DishCategoryProperty =
            DependencyProperty.Register("DishCategory", typeof(string), typeof(MenuItemDialog), new PropertyMetadata(string.Empty));

        public bool DishIsAvailable
        {
            get { return (bool)GetValue(DishIsAvailableProperty); }
            set { SetValue(DishIsAvailableProperty, value); }
        }

        public static readonly DependencyProperty DishIsAvailableProperty =
            DependencyProperty.Register("DishIsAvailable", typeof(bool), typeof(MenuItemDialog), new PropertyMetadata(true));

        public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>
        {
            "Основные блюда",
            "Салаты",
            "Супы",
            "Десерты",
            "Напитки",
            "Закуски"
        };

        public MenuDish MenuDish { get; private set; }

        public MenuItemDialog(MenuDish menuDish)
        {
            InitializeComponent();
            MenuDish = menuDish ?? new MenuDish { IsAvailable = true };

            DishName = MenuDish.Name ?? string.Empty;
            DishDescription = MenuDish.Description ?? string.Empty;
            DishPrice = MenuDish.Price;
            DishCategory = MenuDish.Category ?? Categories[0];
            DishIsAvailable = MenuDish.IsAvailable;

            Debug.WriteLine($"Constructor - DishName set to: '{DishName}'");
        }

        private void PriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out _) && e.Text != ",";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"SaveButton_Click - DishName: '{DishName}'");
            
            if (string.IsNullOrWhiteSpace(DishName))
            {
                MessageBox.Show("Пожалуйста, введите название блюда", "Ошибка");
                NameTextBox.Focus();
                return;
            }

            if (DishPrice <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную цену", "Ошибка");
                PriceTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(DishCategory))
            {
                MessageBox.Show("Пожалуйста, выберите категорию", "Ошибка");
                CategoryComboBox.Focus();
                return;
            }

            try
            {
                Debug.WriteLine("Saving menu item...");
                
                MenuDish.Name = DishName;
                MenuDish.Description = DishDescription;
                MenuDish.Price = DishPrice;
                MenuDish.Category = DishCategory;
                MenuDish.IsAvailable = DishIsAvailable;

                Debug.WriteLine($"MenuDish after save - Name: '{MenuDish.Name}'");

                DialogResult = true;
                Close();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine($"Error saving menu item: {ex}");
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
