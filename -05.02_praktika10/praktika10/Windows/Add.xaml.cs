using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace praktika10.Windows
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        Classes.Passport EditPassport;

        public Add(Classes.Passport EditPassport)
        {
            InitializeComponent();
            if (EditPassport != null )
            {
                Name.Text = EditPassport.Name;
                FirstName.Text = EditPassport.FirstName;
                LastName.Text = EditPassport.LastName;
                Issued.Text = EditPassport.Issued;
                DateOfIssued.Text = EditPassport.DateOfIssued;
                DepartmentCode.Text = EditPassport.DepartmentCode;
                SeriesAndNumber.Text = EditPassport.SeriesAndNumber;
                DateOfBirth.Text = EditPassport.DateOfBirth;
                PlaceOfBirth.Text = EditPassport.PlaceOfBirth;
                PhotoPassport.Text = EditPassport.PhotoPassport;
                BthAdd.Content = "Сохранить";
                this.EditPassport = EditPassport;
            }
        }

        private void AddPassport(object sender, RoutedEventArgs e)
        {
            // Если поле имени пустое или не соответствует регулярному выражению
            if (string.IsNullOrEmpty(Name.Text) || !Classes.Common.CheckRegex.Match(@"^[а-яА-я]*$", Name.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указано имя пользователя");
                return;
            }
            if (string.IsNullOrEmpty(FirstName.Text) || !Classes.Common.CheckRegex.Match(@"^[а-яА-я]*$", FirstName.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указана фамилия пользователя");
                return;
            }
            if (string.IsNullOrEmpty(LastName.Text) || !Classes.Common.CheckRegex.Match(@"^[а-яА-я]*$", LastName.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указано отчество пользователя");
                return;
            }
            if (string.IsNullOrEmpty(Issued.Text) || !Classes.Common.CheckRegex.Match(@"^([А-ЯЁ]+\s){2,}[А-ЯЁ]+$", Issued.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указано уполномоченное лицо выдачи");
                return;
            }
            if (string.IsNullOrEmpty(DateOfIssued.Text) || !Classes.Common.CheckRegex.Match(@"^([012][\d]|[3][01])\.([0][\d]|[1][0-2])\.(20[\d][\d])$", DateOfIssued.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указана дата выдачи");
                return;
            }
            if (string.IsNullOrEmpty(DepartmentCode.Text) || !Classes.Common.CheckRegex.Match(@"^[\d]{3}\-[\d]{3}$", DepartmentCode.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указан код подразделения");
                return;
            }
            if (string.IsNullOrEmpty(SeriesAndNumber.Text) || !Classes.Common.CheckRegex.Match(@"^[\d]{2}\s[\d]{2}\s[\d]{6}$", SeriesAndNumber.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указана серия и номер");
                return;
            }
            if (string.IsNullOrEmpty(DateOfBirth.Text) || !Classes.Common.CheckRegex.Match(@"^([012][\d]|[3][01])\.([0][\d]|[1][0-2])\.((19\d\d)|(200\d)|(201[01]))$", DateOfBirth.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указана дата рождения");
                return;
            }
            if (string.IsNullOrEmpty(PlaceOfBirth.Text) || !Classes.Common.CheckRegex.Match(@"^Г\.\s[А-ЯЁ][А-ЯЁ\s\-]+\s([А-ЯЁ][А-ЯЁ\s\-]+){2,}$", PlaceOfBirth.Text))
            {
                // Выводим сообщение и останавливаем выполнение
                MessageBox.Show("Не правильно указано место рождения");
                return;
            }
            if (!string.IsNullOrEmpty(PhotoPassport.Text) && !System.IO.File.Exists(PhotoPassport.Text))
            {
                MessageBox.Show($"Файл не найден: {PhotoPassport.Text}");
                return;
            }

            // Если паспорт не редактируется, а создаётся новый
            if (EditPassport == null)
            {
                EditPassport = new Classes.Passport();
                MainWindow.init.Passports.Add(EditPassport);
            }

            EditPassport.Name = Name.Text;
            EditPassport.FirstName = FirstName.Text;
            EditPassport.LastName = LastName.Text;
            EditPassport.Issued = Issued.Text;
            EditPassport.DateOfIssued = DateOfIssued.Text;
            EditPassport.DepartmentCode = DepartmentCode.Text;
            EditPassport.SeriesAndNumber = SeriesAndNumber.Text;
            EditPassport.DateOfBirth = DateOfBirth.Text;
            EditPassport.PlaceOfBirth = PlaceOfBirth.Text;
            EditPassport.PhotoPassport = PhotoPassport.Text;

            // Вызываем метод загрузки паспортов в интерфейс программы
            MainWindow.init.LoadPassports();
            this.Close();
        }
    }
}
