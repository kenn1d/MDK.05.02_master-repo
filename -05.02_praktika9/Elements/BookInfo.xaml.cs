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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace praktika9.Elements
{
    /// <summary>
    /// Логика взаимодействия для BookInfo.xaml
    /// </summary>
    public partial class BookInfo : UserControl
    {
        public BookInfo(Classes.Book Book)
        {
            InitializeComponent();

            tbName.Text = $"Наименование: {Book.Name} ({Book.Year} г.)";

            tbGenre.Text = $"Жанр: {Book.ToGenres()}";

            tbAuthor.Text = $"Автор: {Book.ToAuthors()}";
        }
    }
}
