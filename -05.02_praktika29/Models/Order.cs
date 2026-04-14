using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using static praktika29.ViewModels.VMClients;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace praktika29.Models
{
    public partial class Order : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private int? _client_id;

        [ObservableProperty]
        private int? _product_id;

        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private int _count;

        [Schema.NotMapped]
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnableText))]
        private bool _isEnable;

        [Schema.NotMapped]
        public string IsEnableText => IsEnable ? "Сохранить" : "Изменить";

        [Schema.NotMapped]
        public ICommand OnEdit => new RelayCommand(() => {
            IsEnable = !IsEnable;
            (MainWindow.init.DataContext as ViewModels.VMPages)?.vm_orders.dataBase.SaveChanges();
        });

        [Schema.NotMapped]
        public ICommand OnDelete => new RelayCommand(() => {
            (MainWindow.init.DataContext as ViewModels.VMPages)?.vm_orders.Orders.Remove(this);
            (MainWindow.init.DataContext as ViewModels.VMPages)?.vm_orders.dataBase.Remove(this);
            (MainWindow.init.DataContext as ViewModels.VMPages)?.vm_orders.dataBase.SaveChanges();
        });
    }
}
