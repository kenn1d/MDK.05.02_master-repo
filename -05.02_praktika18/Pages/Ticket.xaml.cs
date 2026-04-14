using System;
using System.Collections.Generic;
using System.Windows.Controls;
using praktika18.Classes;

namespace praktika18.Pages
{
    /// <summary>
    /// Логика взаимодействия для Ticket.xaml
    /// </summary>
    public partial class Ticket : Page
    {
        public List<TicketContext> AllTickets;
        public Ticket(string From, string To, string StartTime)
        {
            InitializeComponent();
            AllTickets = TicketContext.AllTickets().FindAll(x => (x.From == From && To == "") || (From == "" && x.To == To) || (x.From == From && x.To == To) ||
                (x.StartTime.ToString("MM.dd.yyyy") == StartTime));
            CreateUI();
        }

        public void CreateUI()
        {
            foreach (TicketContext ticket in AllTickets)
            {
                parent.Children.Add(new Element.Item(ticket));
            }
        }

        private void Create(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Create());
        }
    }
}
