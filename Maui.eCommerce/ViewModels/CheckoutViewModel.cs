using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;
using System.Windows.Input;
using Library.eCommerce.Models;
using System.Runtime.CompilerServices;



namespace Maui.eCommerce.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        private readonly ShoppingCartService _cart = ShoppingCartService.Current;

        private ObservableCollection<Item> _shoppingCart;
        public ObservableCollection<Item> ShoppingCart
        {
            get => _shoppingCart;
            set
            {
                if(_shoppingCart != null)
                {
                    _shoppingCart.CollectionChanged -= ShoppingCart_CollectionChanged;
                }

                _shoppingCart = value;
                _shoppingCart.CollectionChanged += ShoppingCart_CollectionChanged;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        public string TotalPrice => $"Total Price: ${ShoppingCart?.Sum(i => i.Product.Price  * i.Quantity.GetValueOrDefault()):F2}";

        public ICommand ConfirmPurchaseCommand { get; }
        public ICommand CancelCommand { get; }
        public CheckoutViewModel()
        {
            ShoppingCart = new ObservableCollection<Item>(_cart.CartItems);
            ConfirmPurchaseCommand = new Command(OnConfirmPurchase);
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync("//ShoppingManagement"));
        }

        public void RefreshCart()
        {
            ShoppingCart = new ObservableCollection<Item>(_cart.CartItems);
        }

            private async void OnConfirmPurchase()
        {
            await Application.Current.MainPage.DisplayAlert("Purchase Complete", "Thanks for your order!", "OK");

            _cart.CartItems.Clear();
            ShoppingCart = new ObservableCollection<Item>();

            await Shell.Current.GoToAsync("//MainPage");
        }

        private void ShoppingCart_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TotalPrice));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
