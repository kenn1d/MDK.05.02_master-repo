using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace praktika4.Elements
{
    /// <summary>
    /// Логика взаимодействия для Student.xaml
    /// </summary>
    public partial class Student : UserControl
    {
        public Student(Classes.Student student)
        {
            InitializeComponent();

            tb_info.Content = student.GetFIO();

            tb_scholarship.Content = student.Scholarship ? "Стипендия: получает" : "Стипендия: не получает";

            tb_course.Content = $"Курс: {student.Course}";

            photo.Source = new BitmapImage(new Uri(student.href, UriKind.Relative));
        }
    }
}
