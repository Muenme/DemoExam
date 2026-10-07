using CommunityToolkit.Mvvm.ComponentModel;

namespace Demo.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    [ObservableProperty] ViewModelBase pageSwitcher;
}
