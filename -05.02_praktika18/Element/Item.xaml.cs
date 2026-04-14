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
using praktika18.Classes;

namespace praktika18.Element
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        public Item(TicketContext Ticket)
        {
            InitializeComponent();

            lPrice.Content = Ticket.Price + " ₽";
            fromTime.Content = Ticket.StartTime.ToString("HH:mm");
            fromDate.Content = Ticket.StartTime.ToString("MM.dd.yyyy");
            from.Content = Ticket.From;
            toTime.Content = Ticket.EndTime.ToString("HH:mm");
            toDate.Content = Ticket.EndTime.ToString("MM.dd.yyyy");
            to.Content = Ticket.To;

            TimeSpan WayTime = Ticket.EndTime.Subtract(Ticket.StartTime);
            string sHours = WayTime.Hours.ToString();
            if(WayTime.Hours < 10) sHours = "0" + WayTime.Hours;
            string sMin = WayTime.Minutes.ToString();
            if (WayTime.Minutes < 10) sMin = "0" + WayTime.Minutes;
            way.Content = "в пути: " + sHours + ":" + sMin;
        }
    }
}
