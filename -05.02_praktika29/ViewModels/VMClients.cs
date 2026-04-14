using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using praktika29.Context;
using praktika29.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace praktika29.ViewModels
{
    public partial class VMClients : ObservableObject
    {
        public DBContext dataBase = new DBContext();

        [ObservableProperty]
        private ObservableCollection<Client> clients;

        public ICommand? OnAddClientCommand { get; }

        public VMClients()
        {
            dataBase.Clients.Load();
            Clients = dataBase.Clients.Local.ToObservableCollection();

            OnAddClientCommand = new RelayCommand(() =>
            {

                Client NewClient = new Client()
                {
                    FirstName = "FName",
                    LastName = "LName",
                    Email = "email@mail.com"
                };

                Clients.Add(NewClient);
                dataBase.Clients.Add(NewClient);
                dataBase.SaveChanges();
            });

        }
    }
}
