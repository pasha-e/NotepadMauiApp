using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotepadMauiApp.DataSources
{
    public class NotepadInfo 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //public List<NoteInfo> Notes { get; set; } = new List<NoteInfo>();
        public ObservableCollection<NoteInfo> Notes { get; set; } = new ObservableCollection<NoteInfo>();

    }
}
