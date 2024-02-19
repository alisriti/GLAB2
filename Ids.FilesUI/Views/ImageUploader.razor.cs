using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Ids.FilesUI.Views;

public partial class ImageUploader : ComponentBase
{
    [Parameter] public string EmptyImage { get; set; } = "images/uploadImage.png";
    [Parameter] public EventCallback<byte[]> OnFileChanged { get; set; }
    [Parameter] public byte[] Image { get; set; }
    [Parameter] public int Width { get; set; } = 160;
    public bool HasData => Image != null && Image.Length != 0;
    private string imageString => "data:image/png;base64," + Convert.ToBase64String(Image, 0, Image.Length);

    [Parameter] public int MaxSize { get; set; } = 102400;
    private bool hasError = false;
    private string errorMessage = string.Empty;

    private void RemoveImage()
    {
        ResetFile();
        FileChanged();
    }

    private string authorizedSize => MaxSize > 1048576 ? $"{MaxSize / 1048576} MO" : $"{MaxSize / 1024}KO";

    private void FileChanged() => OnFileChanged.InvokeAsync(Image);

    private void ResetFile() => Image = new byte[] { };

    protected override void OnInitialized()
    {
    }

    private async void HandleImage(InputFileChangeEventArgs e)
    {
        try
        {
            hasError = false;

            if (e.GetMultipleFiles()[0].Size > MaxSize)
                throw new Exception($"La taille du fichier image ne doit pas dépasser {authorizedSize}.");

            MemoryStream ms = new();
            await e.File.OpenReadStream(e.File.Size).CopyToAsync(ms);
            Image = ms.ToArray();
        }
        catch (Exception exception)
        {
            errorMessage = exception.Message;
            hasError = true;
            StateHasChanged();
            Image = new byte[] { };
        }
        StateHasChanged();
        FileChanged();
    }
}