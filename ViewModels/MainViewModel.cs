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
    [ObservableProperty]
    public static bool auth = false;

    [ObservableProperty]//поле для ошибок
    public static string exp;

    [ObservableProperty] public UserControl pageSwitcher;//переключатель страничек


    [ObservableProperty]  public string login;

    [ObservableProperty]  public User user; 

    [ObservableProperty]//лист продуктов с подгрузкой нужных таблиц
    static public List<Product> productsDb = ProductsList();

    [ObservableProperty]
    static public List<Product> productsDb0 = ProductsList();

    [ObservableProperty] public string selectedCost = "";
    [ObservableProperty] public string search;

    public MainViewModel() //по умолчанию открывается Home
    {
        PageSwitcher = new HomeView();
    }

    [RelayCommand]
    public void LoginPage() //команда переключения страницы на авторизацию
    {
        PageSwitcher = new LoginView();
    }

    [RelayCommand]
    public void HomePage() //команда переключения страницы на главную
    {
        PageSwitcher = new HomeView();
    }


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
        ProductTotal(products);
        return products;
    }

    public void UpdateProduct(string value) 
    {
        ProductsDb = ProductsDb0;

        ProductsDb = ProductsDb.Where(x => x.Name.Contains(value) || x.Description.Contains(value)).ToList();



    } 

    static private void ProductCount(List<Product> products) //заполнение количества
    {
        foreach (Product p in products) 
        {
            p.total = p.Stocks.Sum(x => x.Count);
        }
    }
    static private void ProductTotal(List<Product> products)//"много" или "мало"
    {
        foreach (Product p in products) 
        {
            if (p.total > 5)
            {
                p.stringTotal = "много";
            }
            else 
            {
                p.stringTotal = "мало";
            }
        }
    }

    static private void ProductBrush(List<Product> products) //заполнение поля цвета подсветки
    {
        foreach (Product p in products)
        {

            if (p.total <= 3)
            {
                p.brush = new SolidColorBrush(Color.Parse("#FF8080"));
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
                p.decoration = new TextDecorationCollection (TextDecorations.Strikethrough);   
            }
            else 
            {
                p.priceSale = null;
            }
        }
    }
    [RelayCommand]//проверка логина
    public void Enter() 
    {
        Exp = string.Empty;
        try
        {
            user = db.Users.First(x => x.Login == Login);
            Auth = true;
            HomePage();
        }
        catch (Exception)
        {
            Exp = "Неверно введен логин!";
        } 
    }
    [RelayCommand]
    public void Exit() //выйти из аккаунта
    {
        user = null;
        Auth = false;
    }

    partial void OnSelectedCostChanged(string value)
    {
        UpdateProduct(value);
    }

    partial void OnSearchChanged(string value)
    {
        UpdateProduct(value);
    }
}
