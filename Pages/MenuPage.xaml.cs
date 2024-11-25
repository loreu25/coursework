using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using CafeOrderManager.Models;
using CafeOrderManager.Data;
using Microsoft.EntityFrameworkCore;
using MaterialDesignThemes.Wpf;

namespace CafeOrderManager.Pages
{
    public partial class MenuPage : Page
    {
        private readonly DatabaseContext _context;
        public ObservableCollection<MenuDish> MenuItems { get; set; }

        public MenuPage()
        {
            InitializeComponent();
            _context = new DatabaseContext();
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
            var dialog = new Window
            {
                Title = title,
                Width = 400,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Window.GetWindow(this),
                Background = (System.Windows.Media.Brush)FindResource("MaterialDesignPaper"),
                Foreground = (System.Windows.Media.Brush)FindResource("MaterialDesignBody")
            };

            var grid = new Grid { Margin = new Thickness(16) };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var nameBox = new TextBox
            {
                Text = menuItem.Name,
                Margin = new Thickness(0, 0, 0, 8),
                Style = (Style)FindResource("MaterialDesignOutlinedTextBox")
            };
            HintAssist.SetHint(nameBox, "Название");
            Grid.SetRow(nameBox, 0);

            var descriptionBox = new TextBox
            {
                Text = menuItem.Description,
                Margin = new Thickness(0, 0, 0, 8),
                Style = (Style)FindResource("MaterialDesignOutlinedTextBox")
            };
            HintAssist.SetHint(descriptionBox, "Описание");
            Grid.SetRow(descriptionBox, 1);

            var priceBox = new TextBox
            {
                Text = menuItem.Price.ToString(),
                Margin = new Thickness(0, 0, 0, 8),
                Style = (Style)FindResource("MaterialDesignOutlinedTextBox")
            };
            HintAssist.SetHint(priceBox, "Цена");
            Grid.SetRow(priceBox, 2);

            var categoryBox = new TextBox
            {
                Text = menuItem.Category,
                Margin = new Thickness(0, 0, 0, 8),
                Style = (Style)FindResource("MaterialDesignOutlinedTextBox")
            };
            HintAssist.SetHint(categoryBox, "Категория");
            Grid.SetRow(categoryBox, 3);

            var availableBox = new CheckBox
            {
                Content = "Доступно",
                IsChecked = menuItem.IsAvailable,
                Margin = new Thickness(0, 8, 0, 16),
                Style = (Style)FindResource("MaterialDesignCheckBox")
            };
            Grid.SetRow(availableBox, 4);

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            Grid.SetRow(buttonPanel, 5);

            var saveButton = new Button
            {
                Content = "Сохранить",
                Style = (Style)FindResource("MaterialDesignRaisedSecondaryButton"),
                Margin = new Thickness(0, 0, 8, 0)
            };
            saveButton.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(nameBox.Text))
                {
                    MessageBox.Show("Введите название блюда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(priceBox.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Введите корректную цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                menuItem.Name = nameBox.Text;
                menuItem.Description = descriptionBox.Text;
                menuItem.Price = price;
                menuItem.Category = categoryBox.Text;
                menuItem.IsAvailable = availableBox.IsChecked ?? false;

                dialog.DialogResult = true;
                dialog.Close();
            };

            var cancelButton = new Button
            {
                Content = "Отмена",
                Style = (Style)FindResource("MaterialDesignFlatButton")
            };
            cancelButton.Click += (s, e) =>
            {
                dialog.DialogResult = false;
                dialog.Close();
            };

            buttonPanel.Children.Add(cancelButton);
            buttonPanel.Children.Add(saveButton);

            grid.Children.Add(nameBox);
            grid.Children.Add(descriptionBox);
            grid.Children.Add(priceBox);
            grid.Children.Add(categoryBox);
            grid.Children.Add(availableBox);
            grid.Children.Add(buttonPanel);

            dialog.Content = grid;

            return dialog.ShowDialog() ?? false;
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = new MenuDish { IsAvailable = true };
            if (ShowMenuItemDialog(menuItem, "Добавление блюда"))
            {
                try
                {
                    _context.MenuDishes.Add(menuItem);
                    _context.SaveChanges();
                    MenuItems.Add(menuItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MenuItemsListView.SelectedItem is MenuDish selectedItem)
            {
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
                        selectedItem.Name = menuItem.Name;
                        selectedItem.Description = menuItem.Description;
                        selectedItem.Price = menuItem.Price;
                        selectedItem.Category = menuItem.Category;
                        selectedItem.IsAvailable = menuItem.IsAvailable;

                        _context.SaveChanges();
                        MenuItemsListView.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении блюда: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MenuItemsListView.SelectedItem is MenuDish selectedItem)
            {
                var result = MessageBox.Show(
                    "Вы уверены, что хотите удалить это блюдо?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

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
}
