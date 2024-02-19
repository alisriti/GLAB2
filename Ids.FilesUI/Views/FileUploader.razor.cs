using Ids.FilesUI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Ids.FilesUI.Views;

public partial class FileUploader : ComponentBase
{
    [Parameter] public string DisplayedFileName { get; set; }
    [Parameter] public string UploadButtonText { get; set; } = "Sélectionnez un fichier";
    [Parameter] public string Accept { get; set; } = ".pdf";
    [Parameter] public EventCallback<DBFile> OnFileChanged { get; set; }
    [Parameter] public DBFile File { get; set; }
    [Parameter] public string CssClass { get; set; } = "text-primary";

    private string Id = $"input{new Random().Next(1, 1000)}";
    private string buttonClass => $"btn {CssClass}";
    private bool isUploading = false;
    private string uploadingText => isUploading ? "Chargement en cours!" : UploadButtonText;
    [Parameter] public int MaxSize { get; set; } = 1048576;
    private bool hasError = false;
    private string errorMessage = string.Empty;
    private string uplodadingStyle => isUploading ? "btn" : buttonClass;

    private string displayedName =>
        string.IsNullOrWhiteSpace(DisplayedFileName) ? File?.FileName : DisplayedFileName;

    private void RemoveFile()
    {
        ResetFile();
        FileChanged();
    }

    private string authorizedSize => MaxSize > 1048576 ? $"{MaxSize / 1048576} MO" : $"{MaxSize / 1024}KO";

    private void FileChanged() => OnFileChanged.InvokeAsync(File);

    private void ResetFile() => File = new DBFile(File.FileId);

    private async void HandleFile(InputFileChangeEventArgs e)
    {
        try
        {
            hasError = false;

            if (e.GetMultipleFiles()[0].Size > MaxSize)
                throw new Exception($"La taille du fichier ne doit pas dépasser {authorizedSize}.");

            isUploading = true;
            await InvokeAsync(StateHasChanged);

            if (File.FileId is null)
                File.FileId = new FileId();
            File.FileName = e.File.Name;
            MemoryStream ms = new();
            await e.File.OpenReadStream(e.File.Size).CopyToAsync(ms);
            File.Data = ms.ToArray();
            isUploading = false;
        }
        catch (Exception exception)
        {
            errorMessage = exception.Message;
            if (Accept == ".pdf")
                errorMessage += " Utilisez les compresseurs de fichier pdf pour réduire la taille.";
            hasError = true;
            StateHasChanged();
            File = new DBFile();
        }
        await InvokeAsync(StateHasChanged);
        FileChanged();
    }
}