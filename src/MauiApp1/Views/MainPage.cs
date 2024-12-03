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
                    // Typed binding
                    
                    new Label() { Text = "Typed Binding:" },
                    // ItemSource uses the package provided method
                    // SelectedIndex uses local definition
                    new Picker().Bind(Picker.ItemsSourceProperty, static (MainViewModel vm) => vm.Numbers)
                                .BindV2(static (MainViewModel vm) => vm.Index),

                    // String-based

                    new Label() { Text = "Classic Binding:" },
                    new Picker().Bind(Picker.ItemsSourceProperty, nameof(MainViewModel.Words))
                                .Bind(nameof(MainViewModel.Index)) // It's two-way by definition
                }
            }.Center();
        }
    }
}
