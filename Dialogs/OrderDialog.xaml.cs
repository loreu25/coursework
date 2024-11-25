using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CafeOrderManager.Models;
using CafeOrderManager.Data;

namespace CafeOrderManager
{
    public partial class OrderDialog : Window, INotifyPropertyChanged
    {
        private readonly DatabaseContext _context;
        private decimal _totalAmount;

        public Order Order { get; private set; }
        public ObservableCollection<OrderItem> OrderItems { get; set; }

        public decimal TotalAmount
        {
            get => _totalAmount;
            private set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public OrderDialog(DatabaseContext context)
        {
            InitializeComponent();
            _context = context;
            Order = new Order
            {
                OrderDate = DateTime.Now,
                Status = "Новый"
            };
            OrderItems = new ObservableCollection<OrderItem>();
            OrderItems.CollectionChanged += OrderItems_CollectionChanged;
            OrderItemsGrid.ItemsSource = OrderItems;
            LoadMenuItems();
            DataContext = this;
        }

        private void OrderItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = OrderItems.Sum(item => item.Total);
        }

        private void LoadMenuItems()
        {
            try
            {
                var menuItems = _context.MenuDishes.Where(m => m.IsAvailable).ToList();
                MenuItemsComboBox.ItemsSource = menuItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке меню: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MenuItemsComboBox.SelectedItem is MenuDish selectedDish)
            {
                if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
                {
                    var existingItem = OrderItems.FirstOrDefault(item => item.MenuDish.Id == selectedDish.Id);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += quantity;
                        OrderItemsGrid.Items.Refresh();
                        UpdateTotalAmount();
                    }
                    else
                    {
                        var orderItem = new OrderItem
                        {
                            MenuDish = selectedDish,
                            MenuDishId = selectedDish.Id,
                            Quantity = quantity,
                            Price = selectedDish.Price
                        };
                        OrderItems.Add(orderItem);
                    }
                    
                    // Reset inputs
                    MenuItemsComboBox.SelectedItem = null;
                    QuantityTextBox.Text = "1";
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректное количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите блюдо", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (OrderItemsGrid.SelectedItem is OrderItem selectedItem)
            {
                OrderItems.Remove(selectedItem);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (!OrderItems.Any())
            {
                MessageBox.Show("Добавьте хотя бы одно блюдо в заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Order.OrderItems = OrderItems.ToList();
            Order.TotalAmount = TotalAmount;
            DialogResult = true;
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
