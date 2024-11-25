using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using CafeOrderManager.Models;
using CafeOrderManager.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeOrderManager.Pages
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
            try
            {
                var orders = _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuDish)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();
                Orders = new ObservableCollection<Order>(orders);
                OrdersListView.ItemsSource = Orders;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заказов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OrderDialog(_context);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _context.Orders.Add(dialog.Order);
                    await _context.SaveChangesAsync();
                    LoadOrders();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Order order)
            {
                var details = string.Join("\n", order.OrderItems.Select(item => 
                    $"{item.MenuDish.Name} x{item.Quantity} = {item.Price * item.Quantity:N0} ₽"));
                
                var message = $"Заказ №{order.Id}\n" +
                             $"Дата: {order.OrderDate:dd.MM.yyyy HH:mm}\n" +
                             $"Статус: {order.Status}\n\n" +
                             $"Позиции:\n{details}\n\n" +
                             $"Итого: {order.TotalAmount:N0} ₽";
                
                MessageBox.Show(message, "Детали заказа", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Order order)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var orderToDelete = await _context.Orders
                            .Include(o => o.OrderItems)
                            .FirstOrDefaultAsync(o => o.Id == order.Id);

                        if (orderToDelete != null)
                        {
                            _context.OrderItems.RemoveRange(orderToDelete.OrderItems);
                            _context.Orders.Remove(orderToDelete);
                            await _context.SaveChangesAsync();
                            LoadOrders();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
