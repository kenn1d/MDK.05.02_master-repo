using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace praktika29.ViewModels
{
    public partial class VMPages
    {
        public VMClients vm_clients = new VMClients();
        public VMProducts vm_products = new VMProducts();
        public VMOrders vm_orders = new VMOrders();

        public VMPages()
        {
            MainWindow.init.frame.Navigate(new Views.Orders.MainOrders(vm_orders));
        }

        public ICommand OnCloseCommand => new RelayCommand(() => 
            MainWindow.init.Close()
        );

        public ICommand ToClientsCommand => new RelayCommand(() =>
        {
            MainWindow.init.frame.Navigate(new Views.Clients.MainClients(vm_clients));
        });

        public ICommand ToProductsCommand => new RelayCommand(() =>
        {
            MainWindow.init.frame.Navigate(new Views.Products.MainProducts(vm_products));
        });

        public ICommand ToOrdersCommand => new RelayCommand(() =>
        {
            MainWindow.init.frame.Navigate(new Views.Orders.MainOrders(vm_orders));
        });

    }
}
