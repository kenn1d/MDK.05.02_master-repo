using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TypeOperation> typeOperationList = TypeOperation.AllTypeOperation();
        public List<Format> formatsList = Format.AllFormats();

        public MainWindow()
        {
            InitializeComponent();

            LoadData();
        }

        void LoadData()
        {
            foreach (TypeOperation items in typeOperationList)
                typeOperation.Items.Add(items.name);
            foreach (Format item in formatsList)
                formats.Items.Add(item.format);
        }

        public void CostCalculations()
        {
            float price = 0;

            //проверка от вида работы
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as string == "Сканирование") price = 10;
                else if (typeOperation.SelectedItem as string == "Печать" || typeOperation.SelectedItem as string == "Копия")
                {
                    if (formats.SelectedItem as string == "А4")
                    {
                        // одна сторона
                        if (Colors.IsChecked == false)
                        {
                            // ч/б
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 4;
                                else price = 3;
                            }
                            else //цвет
                                price = 20;
                        }
                        else
                        {
                            //две стороны
                            //ч/б
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 4;
                                else price = 3;
                            }
                            else
                                price = 35;
                        }
                    }
                    else if (formats.SelectedItem as string == "А3")
                    {
                        // одна сторона
                        if (TwoSides.IsChecked == false)
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 8;
                            else price = 6;
                        }
                        else
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 12;
                            else price = 10;
                        }
                    }
                    else if (formats.SelectedItem as string == "А2")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 35;
                            else price = 50;
                        }
                        else
                        { //цвет
                            if (LotOfColor.IsChecked == false)
                                price = 120;
                            else price = 170;
                        }
                    }
                    else if (formats.SelectedItem as string == "А1")
                    {
                        if (LotOfColor.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 75;
                            else price = 120;
                        }
                        else
                        { //цвет
                            if (LotOfColor.IsChecked == false)
                                price = 170;
                            else price = 250;
                        }
                    }
                }
                else if (typeOperation.SelectedItem as string == "Ризограф")
                {
                    // одна сторона
                    if (TwoSides.IsChecked == false)
                    {
                        if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                            price = 1.40f;
                        else if (textBoxCount.Text.Length > 0 &&
                            int.Parse(textBoxCount.Text) < 200 && textBoxCount.Text.Length > 0 &&
                            int.Parse(textBoxCount.Text) >= 100)
                            price = 1.10f;
                        else price = 1;
                    }
                    else
                    { //ч/б
                        if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                            price = 1.80f;
                        else if (textBoxCount.Text.Length > 0 &&
                            int.Parse(textBoxCount.Text) < 200 &&
                            textBoxCount.Text.Length > 0 &&
                            int.Parse(textBoxCount.Text) >= 100)
                            price = 1.40f;
                        else price = 1.10f;
                    }
                }
            }

            //если кол-во != 1
            if (textBoxCount.Text.Length > 0)
                textBoxPrice.Text = (float.Parse(textBoxCount.Text) * price).ToString();
        }

        public void CalculationsAllPrice()
        {
            // Вся стоимость = 0
            float allPrice = 0;
            // перебираем все операции
            for (int i = 0; i < Operations.Items.Count; i++)
            {
                TypeOperationsWindow newTOW = Operations.Items[i] as TypeOperationsWindow;
                allPrice += newTOW.price;
            }
            labelAllPrice.Content = $"Общая сумма: {allPrice}";
        }

        private void DeleteOperation(object sender, RoutedEventArgs e)
        {
            // если выбрана операция 
            if (Operations.SelectedIndex != -1)
            {
                // удаляем её
                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]);
                // вызываем переращёт всей стоимости
                CalculationsAllPrice();
            }
            else
                MessageBox.Show("Пожалуйста, выберите операцию для удаления");
        }

        private void EditOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1) // Если выбрана операция
            {
                // создаём операцию как класс
                TypeOperationsWindow newTOW = Operations.Items[Operations.SelectedIndex] as TypeOperationsWindow;

                typeOperation.SelectedItem = typeOperationList.Find(x => x.id == newTOW.typeOperation).name; // находим тип операции
                formats.SelectedItem = formatsList.Find(x => x.id == newTOW.format).format; // Находим формат операции

                if (newTOW.side == 1) TwoSides.IsChecked = true; // если сторона 1, выключаем двойную сторону
                else if (newTOW.side == 2) TwoSides.IsChecked = true; // если сторона 2, включаем двойную печать

                Colors.IsChecked = newTOW.color; // В зависимости от операции, включаем или выключаем цветную печать

                string[] resultColor = newTOW.colorText.Split('(');
                if (resultColor.Length == 1) LotOfColor.IsChecked = false; // в зависимости от печати выключаем насыщенность
                else if (resultColor.Length == 2) LotOfColor.IsChecked = true; // в зависимости от печати включаем насыщенность

                textBoxCount.Text = newTOW.count.ToString(); // присваиваем кол-во страниц
                textBoxPrice.Text = newTOW.price.ToString(); // присваиваем цену

                addOperationButton.Content = "Изменить"; // изменяем кнопку

                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]); // удаляем операцию из списка
            }
            else MessageBox.Show("Пожалуйста, выберите операцию для редактирования"); // выводим надпись
        }

        private void SelectedType(object sender, SelectionChangedEventArgs e)
        {
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as string == "Сканирование") {
                    formats.SelectedIndex = -1;
                    TwoSides.IsChecked = false;
                    Colors.IsChecked = false;
                    LotOfColor.IsChecked = false;

                    formats.IsEnabled = false;
                    TwoSides.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }
                else if (typeOperation.SelectedItem as string == "Печать" || typeOperation.SelectedItem as string == "Копия")
                {
                    formats.IsEnabled = true;
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;

                    if (formats.SelectedItem as string == "А4") { 
                        TwoSides.IsEnabled = true;
                        Colors.IsEnabled = true;
                        LotOfColor.IsEnabled = true;
                    }
                    else if (formats.SelectedItem as string == "А3")
                    {
                        TwoSides.IsEnabled = true;
                        Colors.IsEnabled = false;
                        LotOfColor.IsEnabled = false;
                    }
                    else if (formats.SelectedItem as string == "А2" || formats.SelectedItem as string == "А1")
                    {
                        TwoSides.IsEnabled = false;
                        Colors.IsEnabled = true;
                        LotOfColor.IsEnabled = true;
                    }
                }
                else if (typeOperation.SelectedItem as string == "Ризограф")
                {
                    formats.SelectedIndex = 0;
                    formats.IsEnabled = false;
                    Colors.IsEnabled= false;
                    LotOfColor.IsEnabled= false;
                }


                // автозаполнение
                if (textBoxCount.Text.Length == 0)
                    textBoxCount.Text = "1";

                CostCalculations();
            }
        }

        private void SelectedFormat(object sender, SelectionChangedEventArgs e)
        {
            // автозаполнение на 1
            if (formats.SelectedItem as string == "А4")
            {
                TwoSides.IsEnabled = true; // Двойная сторона
                Colors.IsEnabled = true; // Цвет
                LotOfColor.IsEnabled = false; // Насыщенность
            }
            else if (formats.SelectedItem as string == "А3")
            {
                TwoSides.IsEnabled = true;
                Colors.IsEnabled = false;
                LotOfColor.IsEnabled = false;
            }
            else
            {
                TwoSides.IsEnabled = false;
                Colors.IsEnabled = true;
                LotOfColor.IsEnabled = true;
            }

            if (textBoxCount.Text.Length == 0) // Если ничего не введено
                textBoxCount.Text = "1"; // вводим 1

            CostCalculations();
        }

        private void AddOperation(object sender, RoutedEventArgs e)
        {
            TypeOperationsWindow newTOW = new TypeOperationsWindow(); // Создаём новую операцию
            newTOW.typeOperationText = typeOperation.SelectedItem as string; // присваиваем текст выбрпанной операции
            newTOW.typeOperation = typeOperationList.Find(x => x.name == newTOW.typeOperationText).id; // получаем id операции

            if (formats.SelectedIndex != -1)
            { // если выбран формат
                newTOW.formatText = formats.SelectedItem as string; // присваиваем текст формата
                newTOW.format = formatsList.Find(x => x.format == newTOW.formatText).id; // получаем id формата
            }
            if (TwoSides.IsEnabled == true) // если 2 стороны
            {
                if (TwoSides.IsChecked == false) // если не выбрана
                    newTOW.side = 1; // запоминам состояние
                else newTOW.side = 2;
            }
            if (Colors.IsChecked == false) // если включена цветная печать
            {
                newTOW.colorText = "Ч/Б";
                newTOW.color = false;

                if (LotOfColor.IsChecked == false) // если выбрана насыщенность
                {
                    newTOW.colorText += "(> 50%)";
                    newTOW.occupancy = true;
                }
            }
            else
            {
                newTOW.colorText = "ЦВ";
                newTOW.color = true;

                if (LotOfColor.IsChecked == true) // если выбрана насыщенность
                {
                    newTOW.colorText += "(> 50%)";
                    newTOW.color = true;

                    if (LotOfColor.IsChecked == true)
                    {
                        newTOW.colorText += "(> 50%)";
                        newTOW.occupancy = true;
                    }
                }
            }
            newTOW.count = int.Parse(textBoxCount.Text);
            newTOW.price = float.Parse(textBoxPrice.Text);
            addOperationButton.Content = "Добавить";
            Operations.Items.Add(newTOW);
            CostCalculations();
        }

        private void ColorsChange(object sender, RoutedEventArgs e)
        {
            CostCalculations();
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CostCalculations();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!int.TryParse(e.Text, out int x))
                MessageBox.Show("Ввод должен осуществляться цифрами!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void addTotal(object sender, RoutedEventArgs e)
        {
            for(int i = 0; Operations.Items.Count - 1 >= i; i++)
            {
                TypeOperationsWindow totalOper = new TypeOperationsWindow();

                TypeOperationsWindow newTOW = Operations.Items[i] as TypeOperationsWindow;
                totalOper.fio = usersName.Text;
                totalOper.typeOperationText = newTOW.typeOperationText;
                totalOper.typeOperation = newTOW.typeOperation;
                totalOper.formatText = newTOW.formatText;
                totalOper.format = newTOW.format;
                totalOper.side = newTOW.side;
                totalOper.colorText = newTOW.colorText;
                totalOper.color = newTOW.color;
                totalOper.occupancy = newTOW.occupancy;
                totalOper.count = newTOW.count;
                totalOper.price = newTOW.price;

                journalOperations.Items.Add(totalOper);
            }
            Operations.Items.Clear();
        }

        private void newOper(object sender, RoutedEventArgs e)
        {
            tabList.SelectedIndex = 0;
        }

        private void deleteTotalList(object sender, RoutedEventArgs e)
        {
            if (journalOperations.SelectedIndex != -1)
            {
                journalOperations.Items.Remove(journalOperations.Items[journalOperations.SelectedIndex]);
            }
            else
                MessageBox.Show("Пожалуйста, выберите операцию для удаления");
        }

        private void editOper(object sender, RoutedEventArgs e)
        {
            //if (journalOperations.SelectedIndex != -1)
            //{
            //    TypeOperationsWindow newTOW = journalOperations.Items[journalOperations.SelectedIndex] as TypeOperationsWindow;

            //    typeOperation.SelectedItem = typeOperationList.Find(x => x.id == newTOW.typeOperation).name;
            //    formats.SelectedItem = formatsList.Find(x => x.id == newTOW.format).format;

            //    if (newTOW.side == 1) TwoSides.IsChecked = true;
            //    else if (newTOW.side == 2) TwoSides.IsChecked = true;

            //    Colors.IsChecked = newTOW.color;

            //    string[] resultColor = newTOW.colorText.Split('(');
            //    if (resultColor.Length == 1) LotOfColor.IsChecked = false;
            //    else if (resultColor.Length == 2) LotOfColor.IsChecked = true;

            //    textBoxCount.Text = newTOW.count.ToString();
            //    textBoxPrice.Text = newTOW.price.ToString();

            //    addOperationButton.Content = "Изменить";

            //    journalOperations.Items.Remove(journalOperations.Items[journalOperations.SelectedIndex]);
            //}
            //else MessageBox.Show("Пожалуйста, выберите операцию для редактирования");
        }
    }
}
