

using System;

namespace praktika15.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Name { get; set; }
        public string User {  get; set; }
        public int IdDocument { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
        public string Direction { get; set; }
    }
}
