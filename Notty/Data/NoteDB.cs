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
        private const string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=NoteDB.accdb";
        public void Add(Note note) 
        {
            string query = "INSERT into Notes (content, last-update-date, title) VALUES ('" + note.Content + "', #" + note.LastUpdateDate + "#, '" + note.Title + "')";
            ExecuteDataManipulationQuery(query);
        }

        public void Update(Note note)
        {
            string query = "UPDATE Notes SET (content = '" + note.Content + "', last-update-date = #" + note.LastUpdateDate + "#, title = '" + note.Title+ "') WHERE id = '" + note.ID + "'";
            ExecuteDataManipulationQuery(query);
        }

        public void Delete(Note note)
        {
            string query = "DELETE FROM Note WHERE id = '" + note.ID + "'";
            ExecuteDataManipulationQuery(query);
        }

        public void ExecuteDataManipulationQuery( string query) 
        {
            using var connection = new OleDbConnection(connectionString);
            connection.Open();
            var command = new OleDbCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public List<Note> GetAll() 
        {
            List<Note> allNotes = new List<Note>();
            string query = "SELECT * FROM Notes";
            using var connection = new OleDbConnection(connectionString);
            connection.Open();
            var command = new OleDbCommand(query, connection);
            using OleDbDataReader reader = command.ExecuteReader(); 
            while (reader.Read())
            {
                Note note = new Note(reader.GetInt32(0), reader.GetString(2), reader.GetString(1), reader.GetDateTime(3));
                allNotes.Add(note); 
            }
            return allNotes;
        }
    }
}
