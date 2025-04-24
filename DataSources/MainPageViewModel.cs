using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotepadMauiApp.Classes;

namespace NotepadMauiApp.DataSources
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<NotepadInfo> _notepadsList = new();

        private NotepadInfo _selectedNotepad;
        
        public ObservableCollection<NotepadInfo> NotepadsList
        {
            get { return _notepadsList; }
        }

        public NotepadInfo SelectedNotepad
        {
            get { return _selectedNotepad; }
            set
            {
                if (_selectedNotepad != value ) // тут надо проверку нормальную написать на сравнение элементов чтобы каждый раз не обновлять
                {
                    _selectedNotepad = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNotepad"));
                }
            }
        }

        public MainPageViewModel()
        {
            RestoreNotepadsList();
        }


        /// <summary>
        /// смотрим есть у нас сохраннёные данные
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void RestoreNotepadsList()
        {
            var restoreData = DataStorage.Restore();

            if (restoreData != null)
            {
                foreach (var data in restoreData)
                {
                    NotepadsList.Add(new NotepadInfo(){Name = data.Name, Description = data.Description, Id = data.Id, Notes = data.Notes});
                }
            }
            else // если пустой - создадим чтото первоначальное
            {
                NotepadsList.Add(new NotepadInfo() { Name = "Личный", Description = "заметки, записи и тд." });
                NotepadsList.Add(new NotepadInfo() { Name = "Рабочий", Description = "расписание, встречи ..." });
                //NotepadsList.Add(new NotepadInfo() { Name = "Kitchen", Description = "Shopping lists etc.." });
            }
        }

        public void SaveNotepadsList()
        {
            DataStorage.Save(NotepadsList.ToList());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
