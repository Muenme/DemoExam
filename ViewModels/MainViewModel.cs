using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo.Models;
using Demo.Views;
using Microsoft.EntityFrameworkCore;
using System;
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
            .Include(x => x.ProductOrders)
                .ThenInclude(x => x.IdOrderNavigation)
            .ToList();
        ProductCount(products);
        ProductBrush(products);
        ProductPrice(products);
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

    public static void ProductPrice(List<Product> products) //расчет скидки
    {
        DateOnly calculationDate = DateOnly.FromDateTime(DateTime.Today);//текущая дата

        DateOnly monthStart = new(calculationDate.Year, calculationDate.Month, 1);//начало месяца

        DateOnly previousMonthStart = monthStart.AddMonths(-1);//месяц назад


        foreach (Product p in products) 
        {
            bool sale = p.ProductOrders.Any(
                x => x.IdOrderNavigation.Date >= previousMonthStart &&//больше начала прошлого месяца и
                x.IdOrderNavigation.Date < monthStart);//меньше начала этого месяца

            if (!sale)//заполняем новую цену
            {
                p.priceSale = p.Price * 0.75;
            }
            else 
            {
                p.priceSale = p.Price;
            }




        }
    }
    [RelayCommand]
    public void LoginPage() //команда переключения страницы
    {
        pageSwitcher = new LoginView();
    }

    [ObservableProperty] public UserControl pageSwitcher;//переключатель страничек

    public MainViewModel() //по умолчанию открывается Home
    {
        pageSwitcher = new HomeView(); 
    }

}
