using Ids.FilesUI.Exceptions;
using Ids.FilesUI.Models;

namespace Ids.FilesUI.Foundations;

public partial class FileService
{
    private void ValidateFileNotExists(FileId fileId)
    {
        if (FileExists(fileId))
            throw new FileAlreadyExists(fileId.Value);
    }

    private void ValidateFileExists(FileId fileId)
    {
        if (!FileExists(fileId))
            throw new FileNotFoundException();
    }

    private void ValidateFileNotLocked(FileId fileId)
    {
        if (fileStorage.FileLocked(fileId))
            throw new FileLockedException(fileId.Value);
    }

    private static void ValidateFile(DBFile file)
    {
        if (string.IsNullOrWhiteSpace(file.FileId.Value) ||
            string.IsNullOrWhiteSpace(file.FileName) ||
            file.Data is null || file.Data.Length == 0)
        {
            throw new InvalidFileException();
        }
    }
}