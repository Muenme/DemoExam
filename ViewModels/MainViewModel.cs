using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Demo.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    static _21p25PanaseckayaContext db = new _21p25PanaseckayaContext();

    [ObservableProperty]
    static public List<Product> productsDb = db.Products.ToList();

    //static private List<Product> ProductsList() 
    //{
    //    return .Include(x => x.Name).ToList();
    //}

    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    [ObservableProperty] UserControl pageSwitcher;

    public MainViewModel() 
    {
        pageSwitcher = new HomeView(); 
    }

}
