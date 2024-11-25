using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using CafeOrderManager.Models;
using CafeOrderManager.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeOrderManager
{
    public partial class OrdersPage : Page
    {
        private readonly DatabaseContext _context;
        public ObservableCollection<Order> Orders { get; set; }

        public OrdersPage()
        {
            InitializeComponent();
            _context = new DatabaseContext();
            LoadOrders();
            DataContext = this;
        }

        private void LoadOrders()
        {
            Orders = new ObservableCollection<Order>(
                _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuDish)
                    .ToList());
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement new order dialog
            MessageBox.Show("Функционал добавления заказа будет реализован в следующем обновлении");
        }

        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = ((FrameworkElement)sender).DataContext as Order;
            // TODO: Implement order details dialog
            MessageBox.Show($"Детали заказа №{order.Id} будут доступны в следующем обновлении");
        }

        private async void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = ((FrameworkElement)sender).DataContext as Order;
            if (MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                LoadOrders();
            }
        }
    }
}
