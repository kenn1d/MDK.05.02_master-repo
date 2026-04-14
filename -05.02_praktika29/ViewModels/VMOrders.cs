using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using praktika29.Context;
using praktika29.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace praktika29.ViewModels
{
    public partial class VMOrders : ObservableObject
    {
        public DBContext dataBase = new DBContext();

        [ObservableProperty]
        private ObservableCollection<Order> orders;

        [ObservableProperty]
        private ObservableCollection<Client> clients;

        [ObservableProperty]
        private ObservableCollection<Product> products;

        public ICommand? OnAddOrderCommand { get; }

        public VMOrders()
        {
            dataBase = new DBContext();
            dataBase.Orders.Load();
            dataBase.Clients.Load();
            dataBase.Products.Load();
            Orders = new ObservableCollection<Order>(
                dataBase.Orders.ToList()
            );
            Clients = new ObservableCollection<Client>(
                dataBase.Clients.ToList()
            );
            Products = new ObservableCollection<Product>(
                dataBase.Products.ToList()
            );

            OnAddOrderCommand = new RelayCommand(() =>
            {
                Order NewOrder = new Order()
                {
                    Date = DateTime.Now,
                    Count = 0
                };
                Orders.Add(NewOrder);
                dataBase.Orders.Add(NewOrder);
                dataBase.SaveChanges();
            });

            WeakReferenceMessenger.Default.Register<Client.UpdateData>(this, (r, m) =>
            {
                Refresh();
            });
        }

        public void Refresh()
        {
            dataBase.ChangeTracker.Clear();
            Orders = new ObservableCollection<Order>(
                dataBase.Orders.ToList()
            );
            Clients = new ObservableCollection<Client>(
                dataBase.Clients.ToList()
            );
            Products = new ObservableCollection<Product>(
                dataBase.Products.ToList()
            );
        }
    }
}
