namespace NotepadMauiApp.Control;

public partial class AddNotepadPopupPage : ContentPage
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public bool IsCancel { get; private set; } = false;

    public AddNotepadPopupPage()
	{
		InitializeComponent();
	}

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        Name = EntryName.Text;
        Description = EntryDescription.Text;
        IsCancel = false;
        await Navigation.PopModalAsync(); // Закрываем Popup
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        IsCancel = true;
        await Navigation.PopModalAsync(); // Закрываем Popup
    }
}