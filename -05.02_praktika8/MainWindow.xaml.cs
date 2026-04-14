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
using praktika8.Classes;

namespace praktika8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<object> Warriors = Classes.RepoWarriors.AllWarrior();

        public MainWindow()
        {
            InitializeComponent();
            foreach(object warrior in Warriors)
            {
                Classes.Warrior Warrior = warrior as Classes.Warrior;

                if (Warrior is Classes.WarriorEasy)
                {
                    Classes.WarriorEasy WarriorEasy = warrior as Classes.WarriorEasy;
                    playerHealth1.Content = $"Жизненные показатели: {WarriorEasy.Health}";
                }
                else if (Warrior is Classes.WarriorHard)
                {
                    Classes.WarriorHard WarriorHard = warrior as Classes.WarriorHard;
                    playerHealth2.Content = $"Жизненные показатели: {WarriorHard.Health}";
                }
            }
        }

        private void SetDamage(object sender, RoutedEventArgs e)
        {
            foreach (object warrior in Warriors)
            {
                Classes.Warrior Warrior = warrior as Classes.Warrior;

                if (Warrior is Classes.WarriorEasy)
                {
                    Classes.WarriorEasy WarriorEasy = warrior as Classes.WarriorEasy;
                    WarriorEasy.SetDamadge(100);
                    if(WarriorEasy.Health <= 0) playerHealth1.Content = $"Жизненные показатели: 0";
                    else
                        playerHealth1.Content = $"Жизненные показатели: {WarriorEasy.Health}";
                }
                else if (Warrior is Classes.WarriorHard)
                {
                    Classes.WarriorHard WarriorHard = warrior as Classes.WarriorHard;
                    WarriorHard.SetDamadge(80);
                    if (WarriorHard.Health < 40) playerHealth2.Content = $"Жизненные показатели: 0";
                    else
                        playerHealth2.Content = $"Жизненные показатели: {WarriorHard.Health}";
                }
            }
        }
    }
}
