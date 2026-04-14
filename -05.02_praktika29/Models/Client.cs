using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using praktika29.ViewModels;
using System.Windows.Input;
using static praktika29.Models.Order;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace praktika29.Models
{
    public partial class Client : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string? _firstName;

        [ObservableProperty]
        private string? _lastName;

        [ObservableProperty]
        private string? _email;

        [Schema.NotMapped]
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof (IsEnableText))]
        private bool _isEnable;

        [Schema.NotMapped]
        public string IsEnableText => IsEnable ? "Сохранить" : "Изменить";

        public class UpdateData { }
        [Schema.NotMapped]
        public ICommand OnEdit => new RelayCommand(() => {
            IsEnable = !IsEnable;
            (MainWindow.init.DataContext as VMPages)?.vm_clients.dataBase.SaveChanges();
            WeakReferenceMessenger.Default.Send(new UpdateData());
        });

        [Schema.NotMapped]
        public ICommand OnDelete => new RelayCommand(() => {
            (MainWindow.init.DataContext as VMPages)?.vm_clients.Clients.Remove(this);
            (MainWindow.init.DataContext as VMPages)?.vm_clients.dataBase.Remove(this);
            (MainWindow.init.DataContext as VMPages)?.vm_clients.dataBase.SaveChanges();
            WeakReferenceMessenger.Default.Send(new UpdateData());
        });
    }
}
