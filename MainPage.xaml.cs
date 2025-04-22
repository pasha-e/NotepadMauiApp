using NotepadMauiApp.DataSources;
using System.Collections.ObjectModel;

namespace NotepadMauiApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        /*
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }*/
        private void Button_OnClicked(object? sender, EventArgs e)
        {
            if(MenuColumnWidth.Width == 20)
                MenuColumnWidth.Width = 300;
            else
                MenuColumnWidth.Width = 20;
        }
    }

}
