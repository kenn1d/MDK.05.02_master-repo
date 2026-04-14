using praktika2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using System.Windows.Threading;

namespace praktika2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Данные игрока и их копия
        public Classes.PersonInfo Player = new Classes.PersonInfo("Student", 100, 10, 1, 0, 0, 5, "/Hero 1.png");
        public Classes.PersonInfo PlayerCopy;

        ///<summary>Коллекция противников
        public List<Classes.PersonInfo> Enemys = new List<Classes.PersonInfo>();

        // Данный объект будет содержать в себе определённого противника в определенный момент
        public Classes.PersonInfo Enemy;

        public MainWindow()
        {
            InitializeComponent();
            //Повышаем уровень персонажа и обновляем данные на UI
            UserInfoPlayer();

            //Добавляем данные о противниках в коллекцию
            Enemys.Add(new Classes.PersonInfo("Название врага №1", 100, 20, 1, 15, 5, 20, "/Hero 2.png"));
            Enemys.Add(new Classes.PersonInfo("Название врага №2", 20, 5, 1, 5, 2, 5, "/Hero 3.png"));
            Enemys.Add(new Classes.PersonInfo("Название врага №3", 50, 3, 1, 10, 10, 15, "/Hero 4.png"));

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            // Задаем насройки для таймера
            dispatcherTimer.Tick += AttackPlayer;
            // Задаем интервал с которым выполняется таймер
            dispatcherTimer.Interval = new System.TimeSpan(0, 0, 10);
            // Запускаем таймер
            dispatcherTimer.Start();

            // Заполнем копию игрока данными
            PlayerCopy = new Classes.PersonInfo(Player.Name,
                Player.Health,
                Player.Armor,
                Player.Level,
                Player.Glasses,
                Player.Money,
                Player.Damage,
                Player.Photo);

            // Вызываем метод случайного противника
            SelectEnemy();
        }

        ///<summary> Выбор случайного противника
        public void SelectEnemy()
        {
            // Выбирваем случайный индекс противника
            int Id = new Random().Next(0, Enemys.Count);
            // Создаем экземпляр с данными противника
            Enemy = new Classes.PersonInfo(
                Enemys[Id].Name,
                Enemys[Id].Health,
                Enemys[Id].Armor,
                Enemys[Id].Level,
                Enemys[Id].Glasses,
                Enemys[Id].Money,
                Enemys[Id].Damage,
                Enemys[Id].Photo);

            // Смена изображения врага
            emptyImage.Source = new BitmapImage(new Uri(Enemy.Photo, UriKind.RelativeOrAbsolute));

            // Выводим характеристики
            emptyHealth.Content = "Жизненные показаели: " + Enemy.Health;
            emptyArmor.Content = "Броня: " + Enemy.Armor;
        }

        ///<summary> Метод, который наносит переодический урон игроку
        private void AttackPlayer(object sender, System.EventArgs e)
        { 
            // Наносим урон в процентном соотношении имеющейся брони
            Player.Health -= Convert.ToInt32(Player.Damage * 100f / (100f - Player.Armor));

            // TODO: Скрытие элементов при смерти
            if (Player.Health <= 0)
            {
                HeroBorder.Visibility = Visibility.Hidden;
                EnemyBorder.Visibility = Visibility.Hidden;

                playerDied.Visibility = Visibility.Visible;
                Revive.Visibility = Visibility.Visible;
            }

            // Обновляем характеристики персонажа
            UserInfoPlayer();
        }

        ///<summary> Метод, который наносит переодический урон врагу
        private void AttackEnemy(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Наношение урона
            Random rnd = new Random();
            int dmg = 0;
            for (int i = 0; i < 1;)
            {
                dmg = rnd.Next(2);
                i++;
            }
            if (dmg == 0)
            {
                // Наносим урон в процентном соотношении имеющейся брони
                Enemy.Health -= Convert.ToInt32(Player.Damage * 100f / (100f - Enemy.Armor));
                Damage.Content = "";
            }
            else
            {
                // TODO: Наносим урон минуя броню
                Enemy.Health -= Convert.ToInt32(Player.Damage * 2);
                Damage.Content = "Игрок нанёс удар минуя броню!";
            }

            // TODO: Контратака
            if (Classes.CounterAttack.P() <= 0.2)
            {
                Player.Health -= Convert.ToInt32(Player.Damage);
                if (Player.Health <= 0)
                {
                    HeroBorder.Visibility = Visibility.Hidden;
                    EnemyBorder.Visibility = Visibility.Hidden;

                    playerDied.Visibility = Visibility.Visible;
                    Revive.Visibility = Visibility.Visible;
                    return;
                }
                CounterAttack.Content = "Враг нанёс контратаку!";
            }
            else CounterAttack.Content = "";

            // Если жизненные показаели меньше или равны 0
            if (Enemy.Health <= 0)
            {
                // Увеличиваем очки персонажа
                Player.Glasses += Enemy.Glasses;
                PlayerCopy.Glasses += Enemy.Glasses;
                // Увеличиваем монеты персонажа
                Player.Money += Enemy.Money;
                PlayerCopy.Money += Enemy.Money;
                // Обновляем информацию на UI
                UserInfoPlayer();
                // Выбираем нового противника
                SelectEnemy();
            }
            else
            {
                // Обновляем UI персонажа
                emptyHealth.Content = "Жизненные показаели: " + Enemy.Health;
                emptyArmor.Content = "Броня: " + Enemy.Armor;
            }
        }


        ///<summary> Повышение уровня и обновления данных на UI
        public void UserInfoPlayer()
        {
            // TODO: Проверка для кнопки возрождения
            if (Player.Health <= 0)
            {
                Player.Level = PlayerCopy.Level;
                Player.Glasses = PlayerCopy.Glasses;
                Player.Health = PlayerCopy.Health;
                Player.Damage = PlayerCopy.Damage;
                Player.Armor = PlayerCopy.Armor;
            }

            // Если опыт персонажа больше чем 100 * уровень персонажа
            else if (Player.Glasses > 100 * Player.Level)
            {
                // Увеличиваем уровень на 1
                Player.Level++;
                PlayerCopy.Level++;
                // Обновляем очки уровня
                Player.Glasses = 0;
                PlayerCopy.Glasses = 0;
                // Увеличиваем здоровье на 100
                Player.Health += 100;
                PlayerCopy.Health += 100;
                // Увеличиваем урон на 1
                Player.Damage++;
                PlayerCopy.Damage++;
                //Увеличиваем броню на 1
                Player.Armor++;
                PlayerCopy.Armor++;
            }

            // выводим данные на экран
            playerHealth.Content = "Жизненные показатели: " + Player.Health;
            playerArmor.Content = "Броня: " + Player.Armor;
            playerLevel.Content = "Уровень: " + Player.Level;
            playerGlasses.Content = "Опыт: " + Player.Glasses;
            playerMoney.Content = "Монеты: " + Player.Money;
        }

        // TODO: Кнопка возрождения
        private void btnRevive(object sender, RoutedEventArgs e)
        {
            // Заносим данные игрока из копии
            Player = new Classes.PersonInfo(
                PlayerCopy.Name,
                PlayerCopy.Health,
                PlayerCopy.Armor,
                PlayerCopy.Level,
                PlayerCopy.Glasses,
                PlayerCopy.Money,
                PlayerCopy.Damage,
                PlayerCopy.Photo);

            UserInfoPlayer();
            SelectEnemy();

            playerDied.Visibility = Visibility.Hidden;
            Revive.Visibility = Visibility.Hidden;

            HeroBorder.Visibility = Visibility.Visible;
            EnemyBorder.Visibility = Visibility.Visible;

            CounterAttack.Content = "";
            Damage.Content = "";
        }
    }
}
