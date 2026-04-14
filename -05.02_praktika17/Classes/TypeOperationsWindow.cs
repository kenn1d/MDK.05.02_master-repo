using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1.Classes
{
    public class TypeOperationsWindow
    {
        public string fio { get; set; }
        public string typeOperationText {  get; set; }
        public string formatText { get; set; }
        public string colorText { get; set; }
        public int typeOperation {  get; set; }
        public int format { get; set; }
        public int side { get; set; }
        public bool color { get; set; }
        public bool occupancy { get; set; }
        public int count { get; set; }
        public float price { get; set; }
        public string sign { get; set; } = "Images/image.png";
    }
}
