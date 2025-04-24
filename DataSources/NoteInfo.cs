using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadMauiApp.DataSources
{
    public class NoteInfo
    {
        public string NoteText { get; set; }

        public DateTime CreatedTime { get; set; }

        public bool  IsChecked { get; set; }
    }
}
