using praktika15.Classes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace praktika15.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        DocumentContext Document;

        public Item(DocumentContext documentContext)
        {
            InitializeComponent();

            img.Source = new BitmapImage(new System.Uri(documentContext.Src));
            lName.Content = documentContext.Name;
            lUser.Content = "Ответственный: " + documentContext.User;
            lCode.Content = "Код документа: " + documentContext.IdDocument;
            lDate.Content = "Дата поступления: " + documentContext.Date;
            lStatus.Content = documentContext.Status == 0 ? "Статус: Входящий" : "Статус: Исходящий";
            lDirection.Content = "Направление: " + documentContext.Direction; 
        
            this.Document = documentContext;
        }

        private void EditDocument(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.init.frame.Navigate(new Pages.Add(Document));

        private void DeleteDocument(object sender, System.Windows.RoutedEventArgs e)
        {
            Document.Delete();
            MainWindow.init.AllDocuments = new DocumentContext().AllDocuments();
            MainWindow.init.frame.Navigate(new Pages.Main());
        }
    }
}
