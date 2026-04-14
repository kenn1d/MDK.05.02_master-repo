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

namespace praktika7.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        public Item(object ItemData)
        {
            InitializeComponent();

            // Преобразовываем полученный объект в базовый класс
            Classes.Shop ShopData = ItemData as Classes.Shop;
            // Указываем значения в поля
            tb_Name.Content = ShopData.Name;
            tb_Price.Content = "Цена: " + ShopData.Price;
            // Проверяем можем ли преобразовать класс в детские товары
            if (ItemData is Classes.Children)
            {
                // Преобразовываем в детские товары
                Classes.Children ChildrenData = ItemData as Classes.Children;
                // Выводим характеристику
                tb_Characteristic.Content = "Возраст: " + ChildrenData.Age;
            }
            // Проверяем можем ли преобразовать класс в спорттовары
            if (ItemData is Classes.Sport)
            {
                // Преобразовываем в спорттовары
                Classes.Sport SportData = ItemData as Classes.Sport;
                // Выводим характеристику
                tb_Characteristic.Content = "Размер: " + SportData.Size;
            }

            if(ItemData is Classes.Electronics)
            {
                Classes.Electronics ElectroData = ItemData as Classes.Electronics;
                tb_Characteristic.Content = $"АКБ: {ElectroData.ValueAKB}. Скорость: {ElectroData.Speed} км/ч";
            }
        }
    }
}
