namespace Common
{
    public class ViewModelUserSettings
    {
        ///<summary> IP для прослушивания
        public string IPAddress { get; set; }
        ///<summary> Порт для прослушивания
        public string Port { get; set; }
        ///<summary> Наименование игрока
        public string Name { get; set; }
        ///<summary> ID змеи
        public int IdSnake = -1;
    }
}
