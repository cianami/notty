using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Data;
using System.IO;
using System.Data.SQLite;
using System.Data.Entity.Infrastructure;
using System.Globalization;

namespace Notty.Data
{
    internal class NoteDB
    {
        private readonly string filePath;

        public NoteDB(string filePath)
        {
            this.filePath = filePath;
            CreateAndInitDatabaseIfNotExist();
        }

        private string ConnectionString => "Data Source=" + filePath;

        private void CreateAndInitDatabaseIfNotExist()
        {
            if (File.Exists(filePath))
                return;
            SQLiteConnection.CreateFile(filePath);
            string createTableQuery = "CREATE TABLE [Notes] (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, content TEXT, title TEXT, [updated-at] TEXT);";
            ExecuteDataManipulationQuery(createTableQuery);
            Note myNote = new Note("Привет, я Ариша!", "Это мое тестовое задание на стажировку.");
            Add(myNote);
        }


        public void Add(Note note)
        {
            string query = "INSERT into Notes (content, [updated-at], title) VALUES ('" + note.Content + "', '" + FromDatetimeToSQLString(note.LastUpdateDate) + "', '" + note.Title + "')";
            ExecuteDataManipulationQuery(query);
        }

        public void Update(Note note)
        {
            string query = "UPDATE Notes SET content = '" + note.Content + "', [updated-at] = '" + FromDatetimeToSQLString(note.LastUpdateDate) + "', title = '" + note.Title + "' WHERE id = " + note.ID + "";
            ExecuteDataManipulationQuery(query);
        }

        public void Delete(Note note)
        {
            string query = "DELETE FROM Notes WHERE id = " + note.ID + "";
            ExecuteDataManipulationQuery(query);
        }

        private void ExecuteDataManipulationQuery(string query)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public List<Note> GetAll()
        {
            List<Note> allNotes = new List<Note>();
            string query = "SELECT * FROM Notes";
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Note note = new Note(reader.GetInt32(0), reader.GetString(2), reader.GetString(1), FromSQLStringToDatetime(reader.GetString(3)));
                allNotes.Add(note);
            }
            return allNotes;
        }

        private static string FromDatetimeToSQLString(DateTime lastUpdateDate)
        {
            return lastUpdateDate.ToString("yyyy-MM-dd HH:mm:ss");
        }
        
         private static DateTime FromSQLStringToDatetime(string lastUpdateDate)
        {
            return DateTime.ParseExact(lastUpdateDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static List<Note> GetNoteList()
        {
            return new List<Note> {
                new Note("cpa","acl'msd;k"),
                new Note("a;cvmv;a","a;dvnsl"), 
                new Note("aS;CLMA","AKVMALDVK"), 
                new Note("SLCM;AS","scmk")
            };
        }
    }
}
