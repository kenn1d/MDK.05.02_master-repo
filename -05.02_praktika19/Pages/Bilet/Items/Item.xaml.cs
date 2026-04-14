using System.Collections.Generic;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.Bilet.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        List<KinoteatrContext> AllKinoteatrs = KinoteatrContext.Select();
        List<AfishaContext> AllAfishas = AfishaContext.Select();

        public Item(BiletContext item)
        {
            InitializeComponent();

            kinoteatrs.Text = AllKinoteatrs.Find(x => x.Id == AllAfishas.Find(y => y.Id == item.idAfisha).IdKinoteatr).Name;
            name.Text = AllAfishas.Find(x => x.Id == item.idAfisha).Name;
            date.Text = AllAfishas.Find(x => x.Id == item.idAfisha).Time.ToString("yyyy-MM-dd");
            time.Text = AllAfishas.Find(x => x.Id == item.idAfisha).Time.ToString("HH:mm");
            price.Text = AllAfishas.Find(x => x.Id == item.idAfisha).Price.ToString();
        }
    }
}
