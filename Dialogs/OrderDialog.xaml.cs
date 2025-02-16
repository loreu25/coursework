using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CafeOrderManager.Models;
using CafeOrderManager.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeOrderManager
{
    public partial class OrderDialog : Window, INotifyPropertyChanged
    {
        private readonly DatabaseContext _context;
        private Order _order;
        private ObservableCollection<OrderItem> _orderItems;
        public decimal TotalAmount { get; private set; }

        public Order Order => _order;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrderDialog(DatabaseContext context)
        {
            InitializeComponent();
            _context = context;
            _order = new Order { OrderDate = DateTime.Now, Status = "Новый" };
            _orderItems = new ObservableCollection<OrderItem>();
            OrderItemsListView.ItemsSource = _orderItems;
            LoadMenuItems();
            DataContext = this;
        }

        private void LoadMenuItems()
        {
            try
            {
                var menuItems = _context.MenuDishes
                    .Where(m => m.IsAvailable)
                    .OrderBy(m => m.Category)
                    .ThenBy(m => m.Name)
                    .ToList();
                MenuItemsListView.ItemsSource = menuItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке меню: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuItemsListView.SelectedItem is MenuDish selectedDish)
            {
                var existingItem = _orderItems.FirstOrDefault(item => item.MenuDish.Id == selectedDish.Id);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                    existingItem.Price = existingItem.Quantity * selectedDish.Price;
                }
                else
                {
                    var orderItem = new OrderItem
                    {
                        MenuDish = selectedDish,
                        MenuDishId = selectedDish.Id,
                        Quantity = 1,
                        Price = selectedDish.Price
                    };
                    _orderItems.Add(orderItem);
                }
                UpdateTotalAmount();
            }
        }

        private void RemoveFromOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderItemsListView.SelectedItem is OrderItem selectedItem)
            {
                if (selectedItem.Quantity > 1)
                {
                    selectedItem.Quantity--;
                    selectedItem.Price = selectedItem.Quantity * selectedItem.MenuDish.Price;
                }
                else
                {
                    _orderItems.Remove(selectedItem);
                }
                UpdateTotalAmount();
            }
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = _orderItems.Sum(item => item.Price);
            OnPropertyChanged(nameof(TotalAmount));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_orderItems.Any())
            {
                MessageBox.Show("Добавьте хотя бы одно блюдо в заказ", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _order.OrderItems = _orderItems.ToList();
            _order.TotalAmount = TotalAmount;
            foreach (var item in _order.OrderItems)
            {
                item.OrderId = _order.Id;
            }

            DialogResult = true;
            Close();
        }
    }
}
