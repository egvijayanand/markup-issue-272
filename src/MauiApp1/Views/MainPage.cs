using MauiApp1.Extensions;

namespace MauiApp1.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainViewModel();
            this.Bind(TitleProperty, nameof(MainViewModel.Title));
            Content = new VerticalStackLayout()
            {
                Children =
                {
                    new Label() { Text = "Typed Binding:" },
                    new Picker().Bindv2(Picker.ItemsSourceProperty, static (MainViewModel vm) => vm.Numbers)
                                .Bindv2(static (MainViewModel vm) => vm.Index),
                    new Label() { Text = "Classic Binding:" },
                    new Picker().Bind(Picker.ItemsSourceProperty, nameof(MainViewModel.Words))
                                .Bind(nameof(MainViewModel.Index), BindingMode.TwoWay)
                }
            }.Center();
        }
    }
}
