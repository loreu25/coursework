using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using CafeOrderManager.Models;
using CafeOrderManager.Data;
using Microsoft.EntityFrameworkCore;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using System.Diagnostics;

namespace CafeOrderManager.Pages
{
    public partial class MenuPage : Page
    {
        private readonly DatabaseContext _context;
        public ObservableCollection<MenuDish> MenuItems { get; set; }

        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public MenuPage()
        {
            InitializeComponent();
            _context = new DatabaseContext();
            
            EditCommand = new RelayCommand(EditMenuItem);
            DeleteCommand = new RelayCommand(DeleteMenuItem);
            
            LoadMenuItems();
            DataContext = this;
        }

        private void LoadMenuItems()
        {
            try
            {
                var items = _context.MenuDishes
                    .OrderBy(m => m.Category)
                    .ThenBy(m => m.Name)
                    .ToList();
                MenuItems = new ObservableCollection<MenuDish>(items);
                MenuItemsListView.ItemsSource = MenuItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке меню: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ShowMenuItemDialog(MenuDish menuItem, string title)
        {
            Debug.WriteLine($"ShowMenuItemDialog - menuItem.Name: '{menuItem.Name}'");
            
            var dialog = new MenuItemDialog(menuItem);
            dialog.Title = title;
            dialog.Owner = Window.GetWindow(this);
            var result = dialog.ShowDialog() ?? false;
            
            Debug.WriteLine($"Dialog result: {result}, menuItem.Name after dialog: '{menuItem.Name}'");
            return result;
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("AddMenuItem_Click");
            var menuItem = new MenuDish { IsAvailable = true };
            
            if (ShowMenuItemDialog(menuItem, "Добавление блюда"))
            {
                try
                {
                    Debug.WriteLine($"Adding menu item to database: '{menuItem.Name}'");
                    _context.MenuDishes.Add(menuItem);
                    _context.SaveChanges();
                    MenuItems.Add(menuItem);
                    Debug.WriteLine("Menu item added successfully");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding menu item: {ex}");
                    MessageBox.Show($"Ошибка при добавлении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditMenuItem(object parameter)
        {
            if (parameter is MenuDish selectedItem)
            {
                Debug.WriteLine($"EditMenuItem - selectedItem.Name: '{selectedItem.Name}'");
                
                var menuItem = new MenuDish
                {
                    Id = selectedItem.Id,
                    Name = selectedItem.Name,
                    Description = selectedItem.Description,
                    Price = selectedItem.Price,
                    Category = selectedItem.Category,
                    IsAvailable = selectedItem.IsAvailable
                };

                if (ShowMenuItemDialog(menuItem, "Редактирование блюда"))
                {
                    try
                    {
                        Debug.WriteLine($"Updating menu item in database: '{menuItem.Name}'");
                        selectedItem.Name = menuItem.Name;
                        selectedItem.Description = menuItem.Description;
                        selectedItem.Price = menuItem.Price;
                        selectedItem.Category = menuItem.Category;
                        selectedItem.IsAvailable = menuItem.IsAvailable;

                        _context.Entry(selectedItem).State = EntityState.Modified;
                        _context.SaveChanges();
                        MenuItemsListView.Items.Refresh();
                        Debug.WriteLine("Menu item updated successfully");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error updating menu item: {ex}");
                        MessageBox.Show($"Ошибка при обновлении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void DeleteMenuItem(object parameter)
        {
            if (parameter is MenuDish selectedItem)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить это блюдо?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.MenuDishes.Remove(selectedItem);
                        _context.SaveChanges();
                        MenuItems.Remove(selectedItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
