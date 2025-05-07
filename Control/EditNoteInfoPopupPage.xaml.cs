using NotepadMauiApp.DataSources;

namespace NotepadMauiApp.Control;

public partial class EditNoteInfoPopupPage : ContentPage
{
    public string NoteInfo { get; private set; }

    public bool IsCancel { get; private set; } = false;

    public EditNoteInfoPopupPage()
	{
		InitializeComponent();
	}

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        IsCancel = true;
        await Navigation.PopModalAsync(); // Закрываем Popup
    }

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        NoteInfo = EntryInfo.Text;
        IsCancel = false;
        await Navigation.PopModalAsync(); // Закрываем Popup
    }

    public void InitInfo(NoteInfo note)
    {
        EntryInfo.Text = note.NoteText;
    }
}