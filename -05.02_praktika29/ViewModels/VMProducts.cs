using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using praktika29.Context;
using praktika29.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace praktika29.ViewModels
{
    public partial class VMProducts : ObservableObject
    {
        public DBContext dataBase = new DBContext();

        [ObservableProperty]
        private ObservableCollection<Product> products;

        public ICommand? OnAddProductCommand {  get; }

        public VMProducts()
        {
            dataBase.Products.Load();
            Products = dataBase.Products.Local.ToObservableCollection();

            OnAddProductCommand = new RelayCommand(() =>
            {
                Product NewProduct = new Product()
                {
                    Name = "Name",
                    Price = 0
                };
                Products.Add(NewProduct);
                dataBase.Products.Add(NewProduct);
                dataBase.SaveChanges();
            });
        }
    }
}
