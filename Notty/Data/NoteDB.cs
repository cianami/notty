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

namespace Notty.Data
{
    internal class NoteDB
    {
        private readonly string filePath;

        public NoteDB(string filePath)
        {
            this.filePath = filePath;
        }

        private string ConnectionString => "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath; 


        public void Add(Note note) 
        {
            string query = "INSERT into Notes (content, [updated-at], title) VALUES ('" + note.Content + "', #" + ToSQLDateFormat(note.LastUpdateDate) + "#, '" + note.Title + "')";
            ExecuteDataManipulationQuery(query);
        }

        public void Update(Note note)
        {
            string query = "UPDATE Notes SET content = '" + note.Content + "', [updated-at] = #" + ToSQLDateFormat(note.LastUpdateDate) + "#, title = '" + note.Title+ "' WHERE id = " + note.ID + "";
            ExecuteDataManipulationQuery(query);
        }

        public void Delete(Note note)
        {
            string query = "DELETE FROM Notes WHERE id = " + note.ID + "";
            ExecuteDataManipulationQuery(query);
        }

        private void ExecuteDataManipulationQuery(string query) 
        {
            using var connection = new OleDbConnection(ConnectionString);
            connection.Open();
            var command = new OleDbCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public List<Note> GetAll() 
        {
            List<Note> allNotes = new List<Note>();
            string query = "SELECT * FROM Notes";
            using var connection = new OleDbConnection(ConnectionString);
            connection.Open();
            var command = new OleDbCommand(query, connection);
            using OleDbDataReader reader = command.ExecuteReader(); 
            while (reader.Read())
            {
                Note note = new Note(reader.GetInt32(0), reader.GetString(3), reader.GetString(1), reader.GetDateTime(2));
                allNotes.Add(note); 
            }
            return allNotes;
        }

        private static string ToSQLDateFormat(DateTime lastUpdateDate) 
        {
            return lastUpdateDate.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
