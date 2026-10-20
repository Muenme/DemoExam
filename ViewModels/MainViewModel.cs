using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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
        List<Product> products = db.Products
            .Include(x => x.IdManufactureNavigation)
            .Include(x => x.IdCategoryNavigation)
            .Include(x => x.Stocks)
            .ToList();
        ProductCount(products);
        ProductBrush(products);
        return products;
    }

    static private void ProductCount(List<Product> products) //заполнение количества
    {
        foreach (Product p in products) 
        {
            p.total = p.Stocks.Sum(x => x.Count);
        }
    }

    static private void ProductBrush(List<Product> products) //заполнение поля цвета подсветки
    {
        foreach (Product p in products)
        {

            if (p.total <= 3)
            {
                p.brush = Brushes.LightCoral;
            }
            else
            {
                p.brush = Brushes.DarkGray;
            }

        }
    }


    [ObservableProperty] UserControl pageSwitcher;//переключатель страничек

    public MainViewModel() //по умолчанию открывается Home
    {
        pageSwitcher = new HomeView(); 
    }

}
