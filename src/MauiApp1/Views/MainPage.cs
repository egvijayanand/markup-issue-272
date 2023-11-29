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
                    new Label() { Text = "Typed Binding:" }.Center(),
                    new Picker().Bind(Picker.ItemsSourceProperty, static (MainViewModel vm) => vm.Numbers)
                                .Bind(Picker.SelectedIndexProperty, static (MainViewModel vm) => vm.Index, mode: BindingMode.TwoWay),
                    new Label() { Text = "Classic Binding:" }.Center(),
                    new Picker().Bind(Picker.ItemsSourceProperty, nameof(MainViewModel.Words))
                                .Bind(nameof(MainViewModel.Index), BindingMode.TwoWay)
                }
            }.Center();
        }
    }
}
