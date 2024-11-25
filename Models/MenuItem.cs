using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CafeOrderManager.Models
{
    public class MenuDish : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private decimal _price;
        private string _category;
        private bool _isAvailable;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        public bool IsAvailable
        {
            get => _isAvailable;
            set
            {
                _isAvailable = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
