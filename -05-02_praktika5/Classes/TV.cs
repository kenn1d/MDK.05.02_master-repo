using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace praktika5.Classes
{
    public class TV
    {
        private int activeChannel;
        private int activeVolume;

        public int ActiveChannel
        {
            get
            {
                return activeChannel;
            }
            set
            {
                if (activeChannel < Channels.Count - 1) activeChannel = value;
                else activeChannel = 0;
                if (activeChannel < 0) activeChannel = Channels.Count - 1;
            }
        }

        public List<Channel> Channels = new List<Channel>();

        public TV()
        {
            Channels.Add(new Channel()
            {
                Name = "Практическое занятие №3 Объявление классов и создание объектов в С#.mp4",
                Src = @"C:/Users/"
            });
            Channels.Add(new Channel()
            {
                Name = "Практическое занятие №4 Использование методов в ООП Разницы между простыми и статическими методами.mp4",
                Src = @"C:/Users/"
            });
            Channels.Add(new Channel()
            {
                Name = "Практическое занятие №35 Конструкторы в ООП#.mp4",
                Src = @"C:/Users/"
            });
        }

        ///<summary>Метод смены канала</summary>
        ///<param name="_MediaElement">Элемент на котором воспроизводится видео</param>
        ///<param name="_NameChannel">Элемент, который отображает название анала</param>
        public void SwapChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            DoubleAnimation StartAnimation = new DoubleAnimation();
            StartAnimation.From = 1;
            StartAnimation.To = 0;
            StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
            StartAnimation.Completed += delegate
            {
                _MediaElement.Source = new Uri(this.Channels[this.ActiveChannel].Src);
                _MediaElement.Play();
                DoubleAnimation EndAnimation = new DoubleAnimation();
                EndAnimation.From = 0;
                EndAnimation.To = 1;
                EndAnimation.Duration = TimeSpan.FromSeconds(0.6);
                _MediaElement.BeginAnimation(MediaElement.OpacityProperty, EndAnimation);
            };
            _MediaElement.BeginAnimation(MediaElement.OpacityProperty, StartAnimation);
            _NameChannel.Content = this.Channels[this.ActiveChannel].Name;
        }

        ///<summary>Следующий канал</summary>
        ///<param name="_MediaElement">Элемент на котором воспроизводится видео</param>
        ///<param name="_NameChannel">Элемент, который отображает название анала</param>
        public void NextChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            ActiveChannel++;
            SwapChannel(_MediaElement, _NameChannel);
        }

        ///<summary>Предыдущий канал</summary>
        ///<param name="_MediaElement">Элемент на котором воспроизводится видео</param>
        ///<param name="_NameChannel">Элемент, который отображает название анала</param>
        public void BackChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            ActiveChannel--;
            SwapChannel(_MediaElement, _NameChannel);
        }

        ///<summary>Увеличение громкости</summary>
        public void UpSound()
        {

        }

        ///<summary>Уменьшение громкости</summary>
        public void DownSound()
        {

        }
    }
}