using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using CafeOrderManager.Models;
using CafeOrderManager.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeOrderManager
{
    public partial class MenuPage : Page
    {
        private readonly DatabaseContext _context;
        public ObservableCollection<MenuDish> MenuItems { get; set; }

        public MenuPage()
        {
            InitializeComponent();
            try
            {
                _context = new DatabaseContext();
                LoadMenuItems();
                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации базы данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadMenuItems()
        {
            try
            {
                var items = _context.MenuDishes.ToList();
                MenuItems = new ObservableCollection<MenuDish>(items);
                MenuItemsListView.ItemsSource = MenuItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке меню: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MenuItemDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var newItem = dialog.MenuDish;
                    _context.MenuDishes.Add(newItem);
                    await _context.SaveChangesAsync();
                    LoadMenuItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is MenuDish menuItem)
            {
                var dialog = new MenuItemDialog(menuItem);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        var updatedItem = dialog.MenuDish;
                        var existingItem = await _context.MenuDishes.FindAsync(updatedItem.Id);
                        if (existingItem != null)
                        {
                            _context.Entry(existingItem).CurrentValues.SetValues(updatedItem);
                            await _context.SaveChangesAsync();
                            LoadMenuItems();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при редактировании блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is MenuDish menuItem)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить это блюдо?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var itemToDelete = await _context.MenuDishes.FindAsync(menuItem.Id);
                        if (itemToDelete != null)
                        {
                            _context.MenuDishes.Remove(itemToDelete);
                            await _context.SaveChangesAsync();
                            LoadMenuItems();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
