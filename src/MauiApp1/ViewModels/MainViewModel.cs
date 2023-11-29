using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title = "Home";

        [ObservableProperty]
        private int index = 3;

        public ObservableCollection<string> Numbers => ["1", "2", "3", "4", "5"];

        public ObservableCollection<string> Words => ["One", "Two", "Three", "Four", "Five"];
    }
}
