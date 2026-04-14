using praktika14.Classes;
using praktika14.Elements;
using System.Collections.Generic;
using System.Windows.Controls;

namespace praktika14.Pages
{
    /// <summary>
    /// Логика взаимодействия для Categories.xaml
    /// </summary>
    public partial class Categories : Page
    {
        public List<Classes.Category> categories = new List<Classes.Category>();
        public List<Classes.Item> category_1 = new List<Classes.Item>();
        public List<Classes.Item> category_2 = new List<Classes.Item>();
        public List<Classes.Item> category_3 = new List<Classes.Item>();

        public Categories()
        {
            InitializeComponent();
            // Добавляем элементы в коллекцию
            category_2.Add(new Classes.Item("Шкаф", 20000, "71euhy8b67gmcyp20f9qj8q7ytmvz8i1.jpg"));
            category_1.Add(new Classes.Item("Диван", 12000, "4488921.jpg"));
            category_1.Add(new Classes.Item("Кресло-диван", 15000, "1732024351652-800x600.jpg"));
            category_3.Add(new Classes.Item("Пуфик", 6000, "product_modal_cover_d133a33f-cea6-4472-9670-bf9ff8a270c0.jpg"));
            category_2.Add(new Classes.Item("Комод", 11000, "fe9db6540f2fd063f36b6e4d89be8720.jpg"));
            category_2.Add(new Classes.Item("Книжная полка", 20000, "denver-grafit-knizhnaya-polka-sv-mebel-krasnodar.jpg"));

            // Добавляем категории в коллекцию
            categories.Add(new Classes.Category("Диваны и кресла", category_1));
            categories.Add(new Classes.Category("Мебель хранения", category_2));
            categories.Add(new Classes.Category("Небольшая мебель", category_3));

            LoadItems();
        }

        public void LoadItems()
        {
            parent.Children.Clear(); // очищаем parent

            foreach (Classes.Category category in categories)
            {
                parent.Children.Add(new Elements.Category(category));
            }
        }
    }
}
