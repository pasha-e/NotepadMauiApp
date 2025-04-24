using NotepadMauiApp.DataSources;
using System.Collections.ObjectModel;
using NotepadMauiApp.Classes;
using NotepadMauiApp.Control;

namespace NotepadMauiApp
{
    public partial class MainPage : ContentPage
    {

        private int _selectedItemIndex;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object? sender, EventArgs e)
        {
            if(MenuColumnWidth.Width == 5)
                MenuColumnWidth.Width = 300;
            else
                MenuColumnWidth.Width = 5;
        }

        private void ListView_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
        {
            _selectedItemIndex = e.SelectedItemIndex;

            MainPageDataSource.SelectedNotepad = MainPageDataSource.NotepadsList[_selectedItemIndex];
        }

        private async void BtnAddNotepad_OnClicked(object? sender, EventArgs e)
        {
            var addNotepadPopupPage = new AddNotepadPopupPage();
            await Navigation.PushModalAsync(addNotepadPopupPage);

            // Ждём закрытия Popup и получаем данные
            addNotepadPopupPage.Disappearing += (s, e) =>
            {
                if (!addNotepadPopupPage.IsCancel)
                {
                    //добавить новый блокнот
                    MainPageDataSource.NotepadsList.Add( new NotepadInfo() {Name = addNotepadPopupPage.Name, Description = addNotepadPopupPage.Description});
                    //и сохраним
                    MainPageDataSource.SaveNotepadsList();
                }

                //string value1 = addNotepadPopupPage.Name;
                //string value2 = addNotepadPopupPage.Description;

                //if (!string.IsNullOrEmpty(value1))
                //    DisplayAlert("Результат", $"Поле 1: {value1}, Поле 2: {value2}", "OK");
            };
        }

        private async void BtnAddNote_OnClicked(object? sender, EventArgs e)
        {
            var addNotePopupPage = new AddNoteInfoPopupPage();
            await Navigation.PushModalAsync(addNotePopupPage);


            // Ждём закрытия Popup и получаем данные
            addNotePopupPage.Disappearing += (s, e) =>
            {
                if (!addNotePopupPage.IsCancel)
                {
                    //добавить информация в текущий блокнот
                    MainPageDataSource.NotepadsList[_selectedItemIndex].Notes.Add(
                        new NoteInfo()
                        {
                            NoteText = addNotePopupPage.NoteInfo,
                            IsChecked = false,
                            CreatedTime = DateTime.Now
                        });

                    //обновим вкладку
                    //MainPageDataSource.SelectedNotepad = MainPageDataSource.NotepadsList[_selectedItemIndex];

                    //и сохраним
                    MainPageDataSource.SaveNotepadsList();
                }
            };
        }
    }

}
