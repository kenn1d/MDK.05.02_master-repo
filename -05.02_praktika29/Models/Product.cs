using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using praktika29.ViewModels;
using System.Windows.Input;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace praktika29.Models
{
    public partial class Product : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private decimal _price;

        [Schema.NotMapped]
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnableText))]
        private bool _isEnable;

        [Schema.NotMapped]
        public string IsEnableText => IsEnable ? "Сохранить" : "Изменить";

        public class UpdateData { }
        [Schema.NotMapped]
        public ICommand OnEdit => new RelayCommand(() => {
            IsEnable = !IsEnable;
            (MainWindow.init.DataContext as VMPages)?.vm_products.dataBase.SaveChanges();
            WeakReferenceMessenger.Default.Send(new UpdateData());
        });

        [Schema.NotMapped]
        public ICommand OnDelete => new RelayCommand(() => {
            (MainWindow.init.DataContext as VMPages)?.vm_products.Products.Remove(this);
            (MainWindow.init.DataContext as VMPages)?.vm_products.dataBase.Remove(this);
            (MainWindow.init.DataContext as VMPages)?.vm_products.dataBase.SaveChanges();
            WeakReferenceMessenger.Default.Send(new UpdateData());
        });
    }
}
