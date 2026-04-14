using System.Windows.Controls;

namespace praktika27.View
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        // получаем контекст, от команд создания и изменения
        public Add(object Context)
        {
            InitializeComponent();
            // Указываем контекст
            DataContext = new
            {
                item = Context, // контекст item
                categorys = new ViewModell.VMCategorys() // контекст категорий
            };
        }
    }
}
