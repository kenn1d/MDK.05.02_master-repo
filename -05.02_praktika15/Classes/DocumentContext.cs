

using praktika15.Classes.Common;
using praktika15.Interfaces;
using praktika15.Models;
using System.Collections.Generic;
using System.Data.OleDb;

namespace praktika15.Classes
{
    public class DocumentContext : Document, IDocument
    {
        public List<DocumentContext> AllDocuments()
        {
            List<DocumentContext> allDocuments = new List<DocumentContext>();

            OleDbConnection connection = DBConnection.Connection();
            OleDbDataReader dataDocuments = DBConnection.Query("SELECT * FROM [Документы]", connection);
            while (dataDocuments.Read()) {
                allDocuments.Add(new DocumentContext()
                {
                    Id = dataDocuments.GetInt32(0),
                    Src = dataDocuments.GetString(1),
                    Name = dataDocuments.GetString(2),
                    User = dataDocuments.GetString(3),
                    IdDocument = dataDocuments.GetInt32(4),
                    Date = dataDocuments.GetString(5),
                    Status = dataDocuments.GetInt32(6),
                    Direction = dataDocuments.GetString(7)
                });
            }
            DBConnection.CloseConnection(connection);

            return allDocuments;
        }

        public void Delete()
        {
            OleDbConnection connection = DBConnection.Connection();
            DBConnection.Query(
                    $"DELETE FROM [Документы] WHERE [Код] = {this.Id}", connection);
            DBConnection.CloseConnection(connection);
        }

        public void Save(bool Update = false)
        {
            OleDbConnection connection = DBConnection.Connection();
            if (Update)
            {
                DBConnection.Query(
                    $"UPDATE " + 
                        $"[Документы] " +
                    $"SET " +
                        $"[Изображение] = '{this.Src}'," +
                        $"[Наименование] = '{this.Name}', "+
                        $"[Ответственный] = '{this.User}', "+
                        $"[Код документа] = '{this.IdDocument}', " +
                        $"[Дата поступления] = '{this.Date}', " +
                        $"[Статус] = {this.Status}," +
                        $"[Направление] = '{this.Direction}' " +
                    $"WHERE " +
                        $"[Код] = {this.Id}", connection);
            }
            else
            {
                DBConnection.Query(
                    $"INSERT INTO " +
                        $"[Документы]( " +
                            $"[Изображение]," +
                            $"[Наименование]," +
                            $"[Ответственный]," +
                            $"[Код документа]," +
                            $"[Дата поступления]," +
                            $"[Статус]," +
                            $"[Направление])" +
                    $" VALUES (" +
                        $"'{this.Src}', " +
                        $"'{this.Name}', " +
                        $"'{this.User}', " +
                        $"'{this.IdDocument}', " +
                        $"'{this.Date}', " +
                        $"{this.Status}, " +
                        $"'{this.Direction}')", connection);
            }
                DBConnection.CloseConnection(connection);
        }
    }
}
