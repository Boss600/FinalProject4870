using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CheckoutView : ContentPage
{
	public CheckoutView()
	{
		InitializeComponent();
		BindingContext = new CheckoutViewModel();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        (BindingContext as CheckoutViewModel)?.RefreshCart();
    }

}