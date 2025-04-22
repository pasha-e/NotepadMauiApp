using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadMauiApp.DataSources
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<NotepadInfo> _notepadsList = new();

        public ObservableCollection<NotepadInfo> NotepadsList
        {
            get { return _notepadsList; }
        }

        public MainPageViewModel()
        {
            NotepadsList.Add(new NotepadInfo() { Name = "Private", Description = "Own messages" });
            NotepadsList.Add(new NotepadInfo() { Name = "Work", Description = "Job and tasks" });
            NotepadsList.Add(new NotepadInfo() { Name = "Kitchen", Description = "Shopping lists etc.." });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
