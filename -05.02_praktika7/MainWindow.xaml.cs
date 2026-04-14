using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

namespace praktika7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<object> AllItems = Classes.RepoItems.AllItems();

        public MainWindow()
        {
            InitializeComponent();
            // Метод создания
            CreateUI();
        }

        public void CreateUI()
        {
            // перебираем объекты
            foreach (object Item in AllItems)
            {
                // Добавляем в UI
                parent.Children.Add(new Elements.Item(Item));
            }
        }

        private void find(object sender, RoutedEventArgs e)
        {
            if(find_.Text == "")
            {
                parent.Children.Clear();
                foreach (object Item in AllItems)
                {
                    parent.Children.Add(new Elements.Item(Item));
                }
                return;
            }

            try {
                if (double.TryParse(find_.Text, out double x))
                {
                    int i = 0;
                    foreach (object Item in AllItems)
                    {
                        Classes.Shop myTovar = Item as Classes.Shop;
                        if (myTovar is Classes.Children)
                        {
                            Classes.Children children = myTovar as Classes.Children;
                            if(children.Age == x)
                            {
                                if(i == 0) parent.Children.Clear();
                                parent.Children.Add(new Elements.Item(Item));
                                i++;
                            }
                        }
                        if (myTovar is Classes.Electronics)
                        {
                            Classes.Electronics electronic = myTovar as Classes.Electronics;
                            if(electronic.ValueAKB == x || electronic.Speed == x)
                            {
                                if(i == 0) parent.Children.Clear();
                                parent.Children.Add(new Elements.Item(Item));
                                i++;
                            }
                        }

                    }
                }
                else
                {
                    int i = 0;
                    foreach(object Item in AllItems)
                    {
                        Classes.Shop myTovar = Item as Classes.Shop;
                        if(myTovar is Classes.Sport)
                        {
                            Classes.Sport sport = myTovar as Classes.Sport;
                            if(sport.Size == find_.Text)
                            {
                                if(i == 0) parent.Children.Clear();
                                parent.Children.Add(new Elements.Item(Item));
                                i++;
                            }
                        }
                    }
                }
            }
            catch {
                MessageBox.Show($"Произошла непредвиденная ошибка. Проверьте корректность данных.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
