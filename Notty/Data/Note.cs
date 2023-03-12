using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notty.Data
{
    public class Note
    {
        private string title = null!;
        private string content = null!;

        public int ID { get; }
        public string Title
        { 
            get => title;
            set
            {
                title = value;
                LastUpdateDate = DateTime.Now;  
            }
            
        }
        public string Content 
        { 
            get => content;
            set
            {
                content = value;
                LastUpdateDate = DateTime.Now;
            }
            
        }
        public DateTime LastUpdateDate { get; set; }

        public Note(int iD, string title, string content, DateTime lastUpdateDate) : this(title, content)
        {
            ID = iD;
            LastUpdateDate = lastUpdateDate;
        }

        public Note(string title, string content)
        {
            Title = title;
            Content = content;
            LastUpdateDate = DateTime.Now;
        }
        public override string ToString()
        {
            return Title + ": " + Content;
        }
    }
}
