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

    [ObservableProperty]//лист продуктов с подгрузкой нужных таблиц
    static public List<Product> productsDb = ProductsList();

    static private List<Product> ProductsList()//подгрузка данных из связанных таблиц с продукт
    {
        return db.Products.Include(x => x.IdManufactureNavigation).Include(x => x.IdCategoryNavigation).Include(x => x.Stocks).ToList();
    }

    [ObservableProperty] UserControl pageSwitcher;//переключатель страничек

    public MainViewModel() //по умолчанию открывается Home
    {
        pageSwitcher = new HomeView(); 
    }

}
