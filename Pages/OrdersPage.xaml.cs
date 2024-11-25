using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
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
            catch (Exception ex)
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Order order)
            {
                var detailsWindow = new Window
                {
                    Title = $"Заказ №{order.Id}",
                    Width = 400,
                    Height = 500,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Window.GetWindow(this),
                    Background = (System.Windows.Media.Brush)FindResource("MaterialDesignPaper"),
                    Foreground = (System.Windows.Media.Brush)FindResource("MaterialDesignBody")
                };

                var mainGrid = new Grid { Margin = new Thickness(16) };
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                // Заголовок
                var headerPanel = new StackPanel { Margin = new Thickness(0, 0, 0, 16) };
                headerPanel.Children.Add(new TextBlock
                {
                    Text = $"Заказ №{order.Id}",
                    Style = (Style)FindResource("MaterialDesignHeadline6TextBlock"),
                    Margin = new Thickness(0, 0, 0, 8)
                });
                headerPanel.Children.Add(new TextBlock
                {
                    Text = $"Дата: {order.OrderDate:dd.MM.yyyy HH:mm}",
                    Style = (Style)FindResource("MaterialDesignBody1TextBlock")
                });
                headerPanel.Children.Add(new TextBlock
                {
                    Text = $"Статус: {order.Status}",
                    Style = (Style)FindResource("MaterialDesignBody1TextBlock")
                });
                Grid.SetRow(headerPanel, 0);

                // Список позиций
                var itemsListView = new ListView
                {
                    Margin = new Thickness(0, 0, 0, 16),
                    Style = (Style)FindResource("MaterialDesignListView")
                };

                var gridView = new GridView();
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Название",
                    DisplayMemberBinding = new Binding("MenuDish.Name"),
                    Width = 150
                });
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Кол-во",
                    DisplayMemberBinding = new Binding("Quantity"),
                    Width = 70
                });

                var priceColumn = new GridViewColumn
                {
                    Header = "Цена",
                    Width = 100
                };

                var priceTemplate = new DataTemplate();
                var priceTextBlock = new FrameworkElementFactory(typeof(TextBlock));
                priceTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Price") 
                { 
                    StringFormat = "{0:N0} ₽" 
                });
                priceTemplate.VisualTree = priceTextBlock;
                priceColumn.CellTemplate = priceTemplate;
                gridView.Columns.Add(priceColumn);

                itemsListView.View = gridView;
                itemsListView.ItemsSource = order.OrderItems;
                Grid.SetRow(itemsListView, 1);

                // Итого
                var totalPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 0, 0, 16)
                };
                totalPanel.Children.Add(new TextBlock
                {
                    Text = "Итого: ",
                    Style = (Style)FindResource("MaterialDesignHeadline6TextBlock")
                });
                totalPanel.Children.Add(new TextBlock
                {
                    Text = $"{order.TotalAmount:N0} ₽",
                    Style = (Style)FindResource("MaterialDesignHeadline6TextBlock")
                });
                Grid.SetRow(totalPanel, 2);

                // Кнопка закрытия
                var closeButton = new Button
                {
                    Content = "Закрыть",
                    Style = (Style)FindResource("MaterialDesignFlatButton"),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 8, 0, 0)
                };
                closeButton.Click += (s, ev) => detailsWindow.Close();
                Grid.SetRow(closeButton, 3);

                mainGrid.Children.Add(headerPanel);
                mainGrid.Children.Add(itemsListView);
                mainGrid.Children.Add(totalPanel);
                mainGrid.Children.Add(closeButton);

                detailsWindow.Content = mainGrid;
                detailsWindow.ShowDialog();
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
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
