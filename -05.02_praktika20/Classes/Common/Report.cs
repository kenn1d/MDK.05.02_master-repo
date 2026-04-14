using Microsoft.Win32;
using praktika20.Pages;
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Drawing;
using System.Windows;
using Application = Microsoft.Office.Interop.Excel.Application;
using System.Net;

namespace praktika20.Classes.Common
{
    public class Report
    {
        public static void Group(int IdGroup, Main Main)
        {
            SaveFileDialog SFD = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Filter = "Excel (*.xlsx)|*.xlsx"
            };
            SFD.ShowDialog();
            if (SFD.FileName != "")
            {
                GroupContext Group = Main.AllGroups.Find(x => x.Id == IdGroup);
                var ExcelApp = new Application();
                try
                {
                    ExcelApp.Visible = false;
                    Workbook Workbook = ExcelApp.Workbooks.Add(Type.Missing);
                    Worksheet Worksheet = Workbook.ActiveSheet;
                    Worksheet.Name = $"{Group.Name}";
                    var lastSheet = Worksheet;

                    (Worksheet.Cells[1, 1] as Range).Value = $"Отчёт о группе {Group.Name}";
                    Worksheet.Range[Worksheet.Cells[1, 1], Worksheet.Cells[1, 5]].Merge();
                    Styles(Worksheet.Cells[1, 1], 18);
                    (Worksheet.Cells[3, 1] as Range).Value = $"Список группы:";
                    Worksheet.Range[Worksheet.Cells[3, 1], Worksheet.Cells[3, 5]].Merge();
                    Styles(Worksheet.Cells[3, 1], 12);
                    (Worksheet.Cells[4, 1] as Range).Value = $"ФИО";
                    Styles(Worksheet.Cells[4, 1], 12, XlHAlign.xlHAlignCenter, true);
                    (Worksheet.Cells[4, 1] as Range).ColumnWidth = 35.0f;
                    (Worksheet.Cells[4, 2] as Range).Value = $"Кол-во не сданных практических";
                    Styles(Worksheet.Cells[4, 2], 12, XlHAlign.xlHAlignCenter, true);
                    (Worksheet.Cells[4, 3] as Range).Value = $"Кол-во не сданных теоретических";
                    Styles(Worksheet.Cells[4, 3], 12, XlHAlign.xlHAlignCenter, true);
                    (Worksheet.Cells[4, 4] as Range).Value = $"Отсутствовал на паре";
                    Styles(Worksheet.Cells[4, 4], 12, XlHAlign.xlHAlignCenter, true);
                    (Worksheet.Cells[4, 5] as Range).Value = $"Опоздал";
                    Styles(Worksheet.Cells[4, 5], 12, XlHAlign.xlHAlignCenter, true);

                    int Height = 5;
                    List<StudentContext> Students = Main.AllStudents.FindAll(x => x.IdGroup == IdGroup);

                    //TODO: Инициализация коллекции лучших
                    List<PerfectStudent> perfectStudents = new List<PerfectStudent>();

                    foreach (StudentContext Student in Students)
                    {
                        List<DisciplineContext> StudentDisciplines = Main.AllDisciplines.FindAll(x => x.IdGroup == Student.IdGroup);

                        int PracticeCount = 0;
                        int TheoryCount = 0;
                        int AbsenteeismCount = 0;
                        int LateCount = 0;
                        string Work = "";
                        int Grade = 0;

                        foreach (DisciplineContext StudentDiscipline in StudentDisciplines)
                        {
                            List<WorkContext> StudentWorks = Main.AllWorks.FindAll(x => x.IdDescipline == StudentDiscipline.Id);
                            foreach (WorkContext StudentWork in StudentWorks)
                            {
                                EvaluationContext Evaluation = Main.AllEvaluations.Find(x =>
                                x.IdWork == StudentWork.Id &&
                                x.IdStudent == Student.Id);

                                if ((Evaluation != null && (Evaluation.Value.Trim() == "" || Evaluation.Value.Trim() == "2")) 
                                    || Evaluation == null)
                                {
                                    if (StudentWork.IdType == 1)
                                        PracticeCount++;
                                    else if (StudentWork.IdType == 2)
                                        TheoryCount++;
                                }
                                if (Evaluation != null && Evaluation.Lateness.Trim() != "")
                                {
                                    if (Convert.ToInt32(Evaluation.Lateness) == 90)
                                        AbsenteeismCount++;
                                    else
                                        LateCount++;
                                }
                            }
                        }

                        //TODO: Добавление студентов в коллекцию лучших
                        perfectStudents.Add(new PerfectStudent(Student, Height, PracticeCount, TheoryCount, AbsenteeismCount, LateCount));


                        (Worksheet.Cells[Height, 1] as Range).Value = $"{Student.LastName} {Student.FirstName}";
                        Styles(Worksheet.Cells[Height, 1], 12, XlHAlign.xlHAlignLeft, true);

                        (Worksheet.Cells[Height, 2] as Range).Value = PracticeCount.ToString();
                        Styles(Worksheet.Cells[Height, 2], 12, XlHAlign.xlHAlignCenter, true);

                        (Worksheet.Cells[Height, 3] as Range).Value = TheoryCount.ToString();
                        Styles(Worksheet.Cells[Height, 3], 12, XlHAlign.xlHAlignCenter, true);

                        (Worksheet.Cells[Height, 4] as Range).Value = AbsenteeismCount.ToString();
                        Styles(Worksheet.Cells[Height, 4], 12, XlHAlign.xlHAlignCenter, true);

                        (Worksheet.Cells[Height, 5] as Range).Value = LateCount.ToString();
                        Styles(Worksheet.Cells[Height, 5], 12, XlHAlign.xlHAlignCenter, true);


                        //TODO: Создание листа студента
                        Worksheet WorksheetStudent = Workbook.Worksheets.Add(After: lastSheet);
                        WorksheetStudent.Name = $"#{Student.LastName}";
                        lastSheet = WorksheetStudent;

                        (WorksheetStudent.Cells[1, 1] as Range).Value = $"Отчёт о студенте {Student.LastName} {Student.FirstName}";
                        WorksheetStudent.Range[WorksheetStudent.Cells[1, 1], WorksheetStudent.Cells[1, 4]].Merge();
                        Styles(WorksheetStudent.Cells[1, 1], 12);
                        Styles(WorksheetStudent.Cells[1, 1], Color.FromArgb(85, 250, 65), true);

                        (WorksheetStudent.Cells[2, 1] as Range).Value = $"Наименование работы";
                        WorksheetStudent.Range[WorksheetStudent.Cells[2, 1], WorksheetStudent.Cells[2, 3]].Merge();
                        Styles(WorksheetStudent.Cells[2, 1], 12, XlHAlign.xlHAlignCenter, true);
                        (WorksheetStudent.Cells[2, 4] as Range).Value = $"Оценка";
                        Styles(WorksheetStudent.Cells[2, 4], 12, XlHAlign.xlHAlignCenter, true);

                        (WorksheetStudent.Cells[3, 1] as Range).Value = Work;
                        WorksheetStudent.Range[WorksheetStudent.Cells[3, 1], WorksheetStudent.Cells[3, 3]].Merge();
                        Styles(WorksheetStudent.Cells[3, 1], 12, XlHAlign.xlHAlignCenter, true);
                        (WorksheetStudent.Cells[3, 2] as Range).Value = TheoryCount.ToString();
                        Styles(WorksheetStudent.Cells[3, 2], 12, XlHAlign.xlHAlignCenter, true);

                        Height++;
                    }

                    //TODO: Выделение лучшего студента
                    if (perfectStudents != null)
                    {
                        var sortStud = perfectStudents.OrderBy(x => x.TotalIssues).ThenBy(x => x.TheoryCount).ThenByDescending(x => x.AbsenteeismCount).ToList();
                        var bestStud = sortStud[0];

                        Range row = Worksheet.Range[
                            Worksheet.Cells[bestStud.Row, 1],
                            Worksheet.Cells[bestStud.Row, 5]];

                        Styles(row, Color.FromArgb(250, 200, 65), true);
                    }

                    Workbook.SaveAs(SFD.FileName);
                    Workbook.Close();
                }
                catch (Exception exp) {
                    MessageBox.Show($"{exp}", "Внимание, ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                ExcelApp.Quit();
            }
        }

        public static void Styles(Range Cell, int FontSize, XlHAlign Position = XlHAlign.xlHAlignCenter,
                                    bool Border = false)
        {
            Cell.Font.Name = "Bahanschrift Light Condensed";
            Cell.Font.Size = FontSize;
            Cell.HorizontalAlignment = Position;
            Cell.VerticalAlignment = XlHAlign.xlHAlignCenter;
            if (Border)
            {
                Borders border = Cell.Borders;
                border.LineStyle = XlLineStyle.xlDouble;
                border.Weight = XlBorderWeight.xlThin;
                Cell.WrapText = true;
            }
        }

        public static void Styles (Range Cell,  Color color, bool Fill = false)
        {
            if (Fill)
            {
                Cell.Interior.Color = ColorTranslator.ToOle(color);
            }
        }
    }
}
