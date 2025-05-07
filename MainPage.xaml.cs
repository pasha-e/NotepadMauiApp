using NotepadMauiApp.DataSources;
using System.Collections.ObjectModel;
using NotepadMauiApp.Classes;
using NotepadMauiApp.Control;

namespace NotepadMauiApp
{
    public partial class MainPage : ContentPage
    {

        private int _selectedItemIndex;

        private int _selectedNoteIndex;

        private double _widthInDp = 300;

        public MainPage()
        {
            InitializeComponent();
            this.Disappearing += OnMainPageDisappearing; // обработка закрытия окна

#if ANDROID
            // Получаем информацию о дисплее
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;

            // Ширина экрана в пикселях (например, 1080)
            double screenWidth = displayInfo.Width;

            // Плотность пикселей (например, 2.75 для 440 DPI)
            double density = displayInfo.Density;

            // Ширина в независимых от устройства единицах (dp)
            _widthInDp = screenWidth / density;

            MenuColumnWidth.Width = _widthInDp;
#endif

        }


        private void OnMainPageDisappearing(object? sender, EventArgs e)
        {
            //сброс выделения
            foreach (var notepad in MainPageDataSource.NotepadsList)
            {
                foreach (var noteInfo in notepad.Notes)
                {
                    noteInfo.IsSelected = false;
                }

                notepad.IsSelected = false;
            }

            //сохранение
            MainPageDataSource.SaveNotepadsList();
        }

        private void Button_OnClicked(object? sender, EventArgs e)
        {
            if(MenuColumnWidth.Width == 5)
                MenuColumnWidth.Width = _widthInDp;
            else
                MenuColumnWidth.Width = 5;
        }

        private void ListView_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
        {
            _selectedItemIndex = e.SelectedItemIndex;

            MainPageDataSource.SelectedNotepad = MainPageDataSource.NotepadsList[_selectedItemIndex];

            if (e.SelectedItem is NotepadInfo selectedItem)
            {
                // Сбрасываем предыдущий выбор
                foreach (var item in ListViewNotepads.ItemsSource.Cast<NotepadInfo>())
                {
                    item.IsSelected = false;
                }

                // Устанавливаем новый выбранный элемент
                selectedItem.IsSelected = true;
            }
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
        

        private async void BtnDeleteNote_OnClicked(object? sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Предупреждение!", "Вы хотите удалить заметку", "OK", "Отмена");

            if (answer)
            {
                MainPageDataSource.NotepadsList[_selectedItemIndex].Notes.RemoveAt(_selectedNoteIndex);

                //и сохраним
                MainPageDataSource.SaveNotepadsList();
            }
        }

        private async void BtnEditNote_OnClicked(object? sender, EventArgs e)
        {
            var editNotePopupPage = new EditNoteInfoPopupPage();
            editNotePopupPage.InitInfo(MainPageDataSource.NotepadsList[_selectedItemIndex].Notes[_selectedNoteIndex]);
            await Navigation.PushModalAsync(editNotePopupPage);

            // Ждём закрытия Popup и получаем данные
            editNotePopupPage.Disappearing += (s, e) =>
            {
                if (!editNotePopupPage.IsCancel)
                {
                    //изменить информацию
                    MainPageDataSource.NotepadsList[_selectedItemIndex].Notes[_selectedNoteIndex].NoteText = editNotePopupPage.NoteInfo;
                    MainPageDataSource.NotepadsList[_selectedItemIndex].Notes[_selectedNoteIndex].CreatedTime = DateTime.Now;
                    

                    //и сохраним
                    MainPageDataSource.SaveNotepadsList();
                }
            };

            MainPageDataSource.NotepadsList[_selectedItemIndex].Notes[_selectedNoteIndex].IsSelected = true;
        }

        private void ListViewNotes_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
        {
            //MainPageDataSource.NotepadsList[_selectedItemIndex].Notes[e.SelectedItemIndex];

            _selectedNoteIndex = e.SelectedItemIndex;

            if (e.SelectedItem is NoteInfo selectedItem)
            {
                // Сбрасываем предыдущий выбор
                foreach (var item in ListViewNotes.ItemsSource.Cast<NoteInfo>())
                {
                    item.IsSelected = false;
                }

                // Устанавливаем выбранный элемент
                selectedItem.IsSelected = true;
            }
        }
    }

}
