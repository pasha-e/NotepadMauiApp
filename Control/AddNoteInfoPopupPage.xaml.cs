namespace NotepadMauiApp.Control;

public partial class AddNoteInfoPopupPage : ContentPage
{
    public string NoteInfo { get; private set; }

    public bool IsCancel { get; private set; } = false;

    public AddNoteInfoPopupPage()
	{
		InitializeComponent();
	}

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        NoteInfo = EntryInfo.Text;
        IsCancel = false;
        await Navigation.PopModalAsync(); // Закрываем Popup
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        IsCancel = true;
        await Navigation.PopModalAsync(); // Закрываем Popup
    }
}