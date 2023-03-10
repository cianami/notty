using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notty.Data
{
    internal class Note
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public Note(int iD, string title, string content, DateTime lastUpdateDate):this(title, content, lastUpdateDate)
        {
            ID = iD;
        }

        public Note(string title, string content, DateTime lastUpdateDate)
        {
            Title = title;
            Content = content;
            LastUpdateDate = lastUpdateDate;
        }
    }
}
