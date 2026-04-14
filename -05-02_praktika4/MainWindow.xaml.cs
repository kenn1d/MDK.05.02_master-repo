using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace praktika4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Список студентов, которых будем обрабатывать
        public List<Classes.Student> AllStudent = Classes.RepoStudents.AllStudent();

        /// <summary> Количество записей для разовой подгрузки
        public int Count = 10;
        /// <summary> Шаг, на котором находится пользователь
        public int Step = 0;
        public int StepAdded = 0;

        /// <summary> Превысили ли мы лимит репозитория 
        public int CheckLimit = 0;


        public MainWindow()
        {
            InitializeComponent();
            // Создание студентов
            CreateStudentRepo(Step, Count);
        }

        /// <summary> Методы создания студентов
        public void CreateStudentRepo(int Step, int Count)
        {
            for (int iStudent = Step; iStudent < Step + Count; iStudent++)
                if (AllStudent.Count > iStudent)
                    parent.Children.Add(new Elements.Student(AllStudent[iStudent]));
            this.Step += Count;
        }
        public void CreateStudentAdded(int StepAdded, int Count)
        {
            for (int iStudent = StepAdded; iStudent < StepAdded + Count; iStudent++)
                if (Classes.RepoStudents.AddedStudent.Count > iStudent)
                    parent.Children.Add(new Elements.Student(Classes.RepoStudents.AddedStudent[iStudent]));
            this.StepAdded += Count;
        }

        /// <summary> Пролистывание списка
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scroll = sender as ScrollViewer;
            double ParentHeight = parent.ActualHeight;
            double WindowHeight = scroll.ActualHeight - 20;
            double DeltaHeight = ParentHeight - WindowHeight;
            if (DeltaHeight - scroll.VerticalOffset < 140)
            {
                if(Step < AllStudent.Count) CreateStudentRepo(Step, Count);
                else CreateStudentAdded(StepAdded, Count);
            } 
        }

        /// <summary> Добавление записи
        public void NewStudent(string Firstname,  string Lastname, string Surname, bool Scholarship, int Course)
        {
            var Add = new Classes.RepoStudents();
            Add.NewStud(Firstname, Lastname, Surname, Scholarship, Course);
        }

        private void btn_upload(object sender, RoutedEventArgs e)
        {
            string[] FIO = tb_info.Text.Split(' ');
            string Scholarship = tb_scholarship.Text;
            bool check = true;
            if(Scholarship != "получает") check = false;
            NewStudent(FIO[0], FIO[1], FIO[2], check, int.Parse(tb_course.Text));
        }

        private void btn_search(object sender, RoutedEventArgs e)
        {
            Elements.FindWindow win = new Elements.FindWindow();

            for (int i = 0; i < AllStudent.Count; i++)
            {
                List<string> FIO = new List<string>();
                FIO.Add(AllStudent[i].GetFIO());
                if (FIO.Contains(tb_info.Text))
                {
                    win.inf_fio.Content = $"ФИО: {AllStudent[i].GetFIO()}";
                    if (AllStudent[i].Scholarship == true) win.inf_step.Content = $"Степендия: получает";
                    else win.inf_step.Content = $"Степендия: не получает";
                    win.inf_course.Content = $"Курс: {AllStudent[i].Course}";
                    win.Show();
                    return;
                }
            }

            List<Classes.Student> AddedStudent = Classes.RepoStudents.AddedStudent;
            for (int i = 0; i < AddedStudent.Count; i++)
            {
                List<string> FIO = new List<string>();
                FIO.Add(AddedStudent[i].GetFIO());
                if (FIO.Contains(tb_info.Text))
                {
                    win.inf_fio.Content = $"ФИО: {AddedStudent[i].GetFIO()}";
                    if (AddedStudent[i].Scholarship == true) win.inf_step.Content = $"Степендия: получает";
                    else win.inf_step.Content = $"Степендия: не получает";
                    win.inf_course.Content = $"Курс: {AddedStudent[i].Course}";
                    win.Show();
                    return;
                }
            }

            MessageBox.Show("Студент не найден, проверьте корректность данных.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
