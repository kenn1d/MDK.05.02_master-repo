using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace praktika13.Layouts
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public MainWindow mainWindow;

        public List<Dish> dishs = new List<Dish>();
        private Dictionary<int, CheckBox> checkBoxes = new Dictionary<int, CheckBox>();
        public Main(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;

            Dish newDish = new Dish();
            newDish.img = "img-1";
            newDish.name = "Сливочная";
            newDish.description = "Пицца - итальянское нациоальное блюдо в виде курглой открытой дрожжевой лепёшки";

            Dish.Ingredient newIngredient = new Dish.Ingredient();
            newIngredient.name = "соус «Кунжутный»";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "сыр «Моцарелла»";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "сыр «Моцарелла» мягкий";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "помидоры";
            newDish.ingredients.Add(newIngredient);


            Dish.Sizes newSize = new Dish.Sizes();
            newSize.size = 23;
            newSize.price = 380;
            newSize.wes = 530;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 30;
            newSize.price = 760;
            newSize.wes = 560;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 40;
            newSize.price = 1210;
            newSize.wes = 730;
            newDish.sizes.Add(newSize);

            dishs.Add(newDish);

            // Создаём пиццу №2

            Dish CheesePizza = new Dish();
            CheesePizza.img = "img-2";
            CheesePizza.name = "Сырная";
            CheesePizza.description = "Пицца - вид пиццы, где доминирует сыр, часто сочетая несколько его видов";

            Dish.Ingredient CheeseIngredient = new Dish.Ingredient();
            CheeseIngredient.name = "сыр «Моцарелла» мягкий";
            CheesePizza.ingredients.Add(CheeseIngredient);

            CheeseIngredient = new Dish.Ingredient();
            CheeseIngredient.name = "сыр «Пармезан»";
            CheesePizza.ingredients.Add(CheeseIngredient);

            CheeseIngredient = new Dish.Ingredient();
            CheeseIngredient.name = "сыр «Горгонзола»";
            CheesePizza.ingredients.Add(CheeseIngredient);

            CheeseIngredient = new Dish.Ingredient();
            CheeseIngredient.name = "сыр «Чеддер»";
            CheesePizza.ingredients.Add(CheeseIngredient);

            Dish.Sizes CheeseSize = new Dish.Sizes();
            CheeseSize.size = 23;
            CheeseSize.price = 480;
            CheeseSize.wes = 630;
            CheesePizza.sizes.Add(CheeseSize);

            CheeseSize = new Dish.Sizes();
            CheeseSize.size = 30;
            CheeseSize.price = 860;
            CheeseSize.wes = 760;
            CheesePizza.sizes.Add(CheeseSize);

            CheeseSize = new Dish.Sizes();
            CheeseSize.size = 40;
            CheeseSize.price = 1200;
            CheeseSize.wes = 830;
            CheesePizza.sizes.Add(CheeseSize);

            dishs.Add(CheesePizza);

            // Создаём пиццу №3

            Dish AnanasPizza = new Dish();
            AnanasPizza.img = "img-3";
            AnanasPizza.name = "Гавайская (с ананасами)";
            AnanasPizza.description = "Гавайская пицца - известная своим сочетанием сладких консервированных ананасов с курицей и сыром";

            Dish.Ingredient AnanasIngredient = new Dish.Ingredient();
            AnanasIngredient.name = "сыр «Моцарелла» мягкий";
            AnanasPizza.ingredients.Add(AnanasIngredient);

            AnanasIngredient = new Dish.Ingredient();
            AnanasIngredient.name = "Кусочки ананаса";
            AnanasPizza.ingredients.Add(AnanasIngredient);

            AnanasIngredient = new Dish.Ingredient();
            AnanasIngredient.name = "Курица";
            AnanasPizza.ingredients.Add(AnanasIngredient);

            Dish.Sizes AnanasSize = new Dish.Sizes();
            AnanasSize.size = 23;
            AnanasSize.price = 680;
            AnanasSize.wes = 630;
            AnanasPizza.sizes.Add(AnanasSize);

            AnanasSize = new Dish.Sizes();
            AnanasSize.size = 30;
            AnanasSize.price = 850;
            AnanasSize.wes = 760;
            AnanasPizza.sizes.Add(AnanasSize);

            AnanasSize = new Dish.Sizes();
            AnanasSize.size = 40;
            AnanasSize.price = 1300;
            AnanasSize.wes = 830;
            AnanasPizza.sizes.Add(AnanasSize);

            dishs.Add(AnanasPizza);

            // Создаём пиццу №4

            Dish PepperPizza = new Dish();
            PepperPizza.img = "img-4";
            PepperPizza.name = "Пепперони";
            PepperPizza.description = "Пепперони - вид американской пиццы, с салями, с добавлением паприки или перца чили";

            Dish.Ingredient PepperIngredient = new Dish.Ingredient();
            PepperIngredient.name = "Колбаски Салями";
            PepperPizza.ingredients.Add(PepperIngredient);

            PepperIngredient = new Dish.Ingredient();
            PepperIngredient.name = "Перец чилли";
            PepperPizza.ingredients.Add(PepperIngredient);

            PepperIngredient = new Dish.Ingredient();
            PepperIngredient.name = "Помидоры";
            PepperPizza.ingredients.Add(PepperIngredient);

            PepperIngredient = new Dish.Ingredient();
            PepperIngredient.name = "Говядина";
            PepperPizza.ingredients.Add(PepperIngredient);

            Dish.Sizes PepperSize = new Dish.Sizes();
            PepperSize.size = 23;
            PepperSize.price = 690;
            PepperSize.wes = 630;
            PepperPizza.sizes.Add(PepperSize);

            PepperSize = new Dish.Sizes();
            PepperSize.size = 30;
            PepperSize.price = 750;
            PepperSize.wes = 760;
            PepperPizza.sizes.Add(PepperSize);

            PepperSize = new Dish.Sizes();
            PepperSize.size = 40;
            PepperSize.price = 1100;
            PepperSize.wes = 830;
            PepperPizza.sizes.Add(PepperSize);

            dishs.Add(PepperPizza);

            // Создаём пиццу №5

            Dish DiabloPizza = new Dish();
            DiabloPizza.img = "img-5";
            DiabloPizza.name = "Дьябло";
            DiabloPizza.description = "Дьябло - это острая пицца, название которой говорит о её жгучем вкусе";

            Dish.Ingredient DiabloIngredient = new Dish.Ingredient();
            DiabloIngredient.name = "Халапеньо";
            DiabloPizza.ingredients.Add(DiabloIngredient);

            DiabloIngredient = new Dish.Ingredient();
            DiabloIngredient.name = "Моцарелла";
            DiabloPizza.ingredients.Add(DiabloIngredient);

            DiabloIngredient = new Dish.Ingredient();
            DiabloIngredient.name = "Помидоры";
            DiabloPizza.ingredients.Add(DiabloIngredient);

            DiabloIngredient = new Dish.Ingredient();
            DiabloIngredient.name = "Оливки";
            DiabloPizza.ingredients.Add(DiabloIngredient);

            DiabloIngredient = new Dish.Ingredient();
            DiabloIngredient.name = "Перец чилли";
            DiabloPizza.ingredients.Add(DiabloIngredient);

            Dish.Sizes DiabloSize = new Dish.Sizes();
            DiabloSize.size = 23;
            DiabloSize.price = 680;
            DiabloSize.wes = 630;
            DiabloPizza.sizes.Add(DiabloSize);

            DiabloSize = new Dish.Sizes();
            DiabloSize.size = 30;
            DiabloSize.price = 850;
            DiabloSize.wes = 760;
            DiabloPizza.sizes.Add(DiabloSize);

            DiabloSize = new Dish.Sizes();
            DiabloSize.size = 40;
            DiabloSize.price = 1300;
            DiabloSize.wes = 830;
            DiabloPizza.sizes.Add(DiabloSize);

            dishs.Add(DiabloPizza);

            CreatePizza();
        }

        public void CreatePizza()
        {
            for (int i = 0; i < dishs.Count; i++) // перебираем пиццы
            {
                var bc = new BrushConverter(); // создаём элемент Grid

                Grid global = new Grid(); // создаём элемент Grid
                global.Height = 100; // указываем высоту
                global.Background = (Brush)bc.ConvertFrom("#ffececec"); // указываем цвет
                global.Margin = new Thickness(0, 10, 0, 0); // задаём отступы

                Image logo = new Image();
                if (File.Exists(mainWindow.localPath + @"\image\dish\" + dishs[i].img + ".png")) // проверяем существует ли файл
                    logo.Source = new BitmapImage(new Uri(mainWindow.localPath + @"\image\dish\" + dishs[i].img + ".png")); // указываем картинку
                else
                    logo.Source = new BitmapImage(new Uri(mainWindow.localPath + @"\image\icon.png")); // указываем картинку

                logo.HorizontalAlignment = System.Windows.HorizontalAlignment.Left; // задаём привязку по горизонтали
                logo.Height = 50; // уст. высоту
                logo.Margin = new Thickness(10, 10, 0, -10); // устанавливаем отступы
                logo.VerticalAlignment = System.Windows.VerticalAlignment.Top; // устанавливаем привязку по вертикали
                logo.Width = 50; // уст. ширину
                global.Children.Add(logo); // добавляем элемент в grid

                Label name = new Label(); // создаём текст
                name.Content = dishs[i].name; // устанавливаем наименование
                name.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                name.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                name.Margin = new Thickness(65, 0, 0, 0);
                name.FontWeight = FontWeights.Bold;
                global.Children.Add(name);

                Label description = new Label(); // создаём текст
                description.Content = dishs[i].description; // уст. описание
                description.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                description.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                description.Margin = new Thickness(65, 20, 0, 0);
                global.Children.Add(description);

                if (dishs[i].ingredients.Count != 0) // если ингердиенты блюда суетсвуют
                {
                    Label ingredient = new Label(); // создаём текст
                    string str_ingredient = "";
                    for (int j = 0; j < dishs[i].ingredients.Count; j++) // перебираем ингредиенты
                    {
                        str_ingredient += dishs[i].ingredients[i].name; // запоминаем наименование ингредиаента
                        if (j != dishs[i].ingredients.Count - 1) // если это не последний ингредиаент
                            str_ingredient += ", ";
                    }

                    ingredient.Content = "Состав: " + str_ingredient; // устанавливаем описание ингредиентов
                    ingredient.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    ingredient.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    ingredient.Margin = new Thickness(65, 40, 0, 0);
                    global.Children.Add(ingredient);
                }

                Label price = new Label();
                price.Content = "Цена: " + dishs[i].sizes[0].price + " р. ";
                price.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                price.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                price.Margin = new Thickness(65, 0, 0, 10);
                global.Children.Add(price);

                Label wes = new Label();
                wes.Content = "Вес: " + dishs[i].sizes[0].wes + " г. ";
                wes.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wes.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                wes.Margin = new Thickness(236, 0, 0, 10);
                global.Children.Add(wes);

                Button button1 = new Button();
                Button button2 = new Button();
                Button button3 = new Button();

                // низ
                Button minus = new Button();
                TextBox count = new TextBox();
                Button plus = new Button();
                CheckBox order = new CheckBox();

                button1.Content = dishs[i].sizes[0].size + " см.";
                button1.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                button1.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                button1.Margin = new Thickness(0, 10, 110, 0);
                button1.Width = 45;
                button1.Background = Brushes.White;
                button1.Foreground = (Brush)bc.ConvertFrom("#ffdd3333"); // уст. цвет текста
                button1.Tag = i; // запоминаем id элемента в тэш
                button1.Click += delegate // назначем действие
                {
                    price.Content = "Цена: " + dishs[int.Parse(button1.Tag.ToString())].sizes[0].price + " р. "; // обновляем цену
                    wes.Content = "Вес: " + dishs[int.Parse(button1.Tag.ToString())].sizes[0].wes + " г. "; // обновляем вес
                    button1.Background = Brushes.White; // изменяем цвет
                    button1.Foreground = (Brush)bc.ConvertFrom("#ffdd3333");

                    button2.Background = (Brush)bc.ConvertFrom("#ffdd3333");
                    button2.Foreground = Brushes.White;
                    button3.Background = (Brush)bc.ConvertFrom("#ffdd3333");
                    button3.Foreground = Brushes.White;

                    dishs[int.Parse(button1.Tag.ToString())].activeSize = 0; // запоминаем активный размер
                    count.Text = dishs[int.Parse(button1.Tag.ToString())].sizes[0].countOrder.ToString(); // изменяем стоимость блюда
                    order.IsChecked = dishs[int.Parse(button1.Tag.ToString())].sizes[0].orders; // снимаем галочку выбора блюда
                    UpdateTotalPrice();
                };
                global.Children.Add(button1);

                button2.Content = dishs[i].sizes[1].size + " см. ";
                button2.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                button2.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                button2.Margin = new Thickness(0, 10, 60, 0);
                button2.Width = 45;
                button2.Tag = i;
                button2.Click += delegate
                {
                    price.Content = "Цена: " + dishs[int.Parse(button2.Tag.ToString())].sizes[1].price + " р. ";
                    wes.Content = "Вес: " + dishs[int.Parse(button2.Tag.ToString())].sizes[1].wes + " г. ";
                    button2.Background = Brushes.White;
                    button2.Foreground = (Brush)bc.ConvertFrom("#ffdd3333");

                    button1.Background = (Brush)bc.ConvertFrom("#ffdd3333");
                    button1.Foreground = Brushes.White;
                    button3.Background = (Brush)bc.ConvertFrom("#ffdd3333");
                    button3.Foreground = Brushes.White;

                    dishs[int.Parse(button1.Tag.ToString())].activeSize = 1; // запоминаем активный размер
                    count.Text = dishs[int.Parse(button1.Tag.ToString())].sizes[1].countOrder.ToString(); // изменяем стоимость блюда
                    order.IsChecked = dishs[int.Parse(button1.Tag.ToString())].sizes[1].orders; // снимаем галочку выбора блюда
                    UpdateTotalPrice();
                };
                global.Children.Add(button2);

                button3.Content = dishs[i].sizes[2].size + " см. ";
                button3.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                button3.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                button3.Margin = new Thickness(0, 10, 10, 0);
                button3.Width = 45;
                button3.Tag = i;
                button3.Click += delegate
                {
                    price.Content = "Цена: " + dishs[int.Parse(button3.Tag.ToString())].sizes[2].price + " р. ";
                    wes.Content = "Вес: " + dishs[int.Parse(button3.Tag.ToString())].sizes[2].wes + " г. ";
                    button3.Background = Brushes.White;
                    button3.Foreground = (Brush)bc.ConvertFrom("#ffdd3333");

                    button1.Background = (Brush)bc.ConvertFrom("#ffdd3333");
                    button1.Foreground = Brushes.White;
                    button2.Background = (Brush)bc.ConvertFrom("#ffdd3333");
                    button2.Foreground = Brushes.White;

                    dishs[int.Parse(button1.Tag.ToString())].activeSize = 2; // запоминаем активный размер
                    count.Text = dishs[int.Parse(button1.Tag.ToString())].sizes[2].countOrder.ToString(); // изменяем стоимость блюда
                    order.IsChecked = dishs[int.Parse(button1.Tag.ToString())].sizes[2].orders; // снимаем галочку выбора блюда
                    UpdateTotalPrice();
                };
                global.Children.Add(button3);




                minus.Content = "-";
                minus.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                minus.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                minus.Margin = new System.Windows.Thickness(0, 0, 103.6f, 10);
                minus.Width = 19;
                minus.Tag = i;
                minus.Click += delegate
                {
                    if (count.Text != "") // если текст не равен пустоте
                    {
                        if (int.Parse(count.Text) > 0) // если кол-во заказанных пицц больше 0
                        {
                            count.Text = (int.Parse(count.Text) - 1).ToString(); // убавляем

                            int id = int.Parse(minus.Tag.ToString()); // преобразуем ID
                            dishs[id].sizes[dishs[id].activeSize].countOrder = int.Parse(count.Text); // уменьшаем кол-во заказанных блюд
                        }
                    }
                };
                global.Children.Add(minus);




                count.Text = "0";
                count.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                count.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                count.Margin = new System.Windows.Thickness(0, 0, 33.6f, 10);
                count.TextWrapping = TextWrapping.Wrap; // вырвниваем текст
                count.TextChanged += Count_TextChanged;
                count.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                count.Width = 65;
                count.Height = 19;
                count.Tag = i;
                global.Children.Add(count);


                plus.Content = "+";
                plus.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                plus.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                plus.Margin = new System.Windows.Thickness(0, 0, 9.6f, 10);
                plus.Width = 19;
                plus.Tag = i;
                plus.Click += delegate
                {
                    if (count.Text != "") // если текст не равен пустоте
                    {
                        if (int.Parse(count.Text) < 15) // если кол-во заказанных пицц меньше 15
                        {
                            count.Text = (int.Parse(count.Text) + 1).ToString(); // прибавляем

                            int id = int.Parse(plus.Tag.ToString()); // преобразуем ID
                            dishs[id].sizes[dishs[id].activeSize].countOrder = int.Parse(count.Text); // увеличиваем кол-во заказанных блюд
                        }
                    }
                };
                global.Children.Add(plus);

                order.Content = "Выбрать";
                order.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                order.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                order.Margin = new System.Windows.Thickness(0, 0, 128, 13);
                order.Tag = i;
                checkBoxes[i] = order;
                order.Click += delegate
                {
                    int id = int.Parse(order.Tag.ToString()); // преобразуем ID
                    dishs[id].sizes[dishs[id].activeSize].orders = (bool)order.IsChecked; // увеличиваем кол-во заказанных блюд
                    UpdateTotalPrice();
                };
                global.Children.Add(order);

                parrent.Children.Add(global);
            }
        }

        private void Count_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox texBox = (TextBox)sender;
            int dishId = (int)texBox.Tag;
            
            if (!int.TryParse(texBox.Text, out int x))
            {
                texBox.Text = dishs[dishId].sizes[dishs[dishId].activeSize].countOrder.ToString();
                return;
            }

            int totalCount = int.Parse(texBox.Text.ToString());
            int dishPrice = dishs[dishId].sizes[dishs[dishId].activeSize].price;
            int exitCount = dishs[dishId].sizes[dishs[dishId].activeSize].countOrder;

            if (totalCount > 15)
            {
                MessageBox.Show("Введите количесво не более 15!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                texBox.Text = dishs[dishId].sizes[dishs[dishId].activeSize].countOrder.ToString();
                return;
            }
            else if (totalCount < 0)
            {
                MessageBox.Show("Введите количесво не менее 0!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                texBox.Text = dishs[dishId].sizes[dishs[dishId].activeSize].countOrder.ToString();
                return;
            }

            dishs[dishId].sizes[dishs[dishId].activeSize].totalPrice = dishPrice * totalCount;
            if (totalCount > 0) {
                dishs[dishId].sizes[dishs[dishId].activeSize].orders = true;
                if (checkBoxes.ContainsKey(dishId))
                {
                    checkBoxes[dishId].IsChecked = true;
                }
            }
            else {
                dishs[dishId].sizes[dishs[dishId].activeSize].orders = false;
                if (checkBoxes.ContainsKey(dishId))
                {
                    checkBoxes[dishId].IsChecked = false;
                }
            }

            dishs[dishId].sizes[dishs[dishId].activeSize].countOrder = totalCount;
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            int totalSum = 0;
            int totalBuyCount = 0;
            foreach (var dish in dishs)
            {
                foreach (var size in dish.sizes)
                {
                    if (size.orders)
                    {
                        totalBuyCount++;
                        totalSum += size.price * size.countOrder;
                    }
                }
            }
            allPrice.Content = "Сумма: " + totalSum.ToString() + " ₽";
            buyCount.Content = "Заказать (" + totalBuyCount.ToString() + ')';
        }
    }
}
