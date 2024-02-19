using Ids.FilesUI.Models;

namespace Ids.FilesUI.Foundations;

public interface IFileService
{
    ValueTask SaveFile(DBFile file);

    ValueTask SaveFile(FileId fileId, string fileName, byte[] data);

    ValueTask UpdateFile(DBFile file);

    ValueTask UpdateFile(FileId fileId, string fileName, byte[] data);

    ValueTask UpdateFile(FileId fileId, bool used = true);

    ValueTask DeleteFile(FileId fileId);

    ValueTask<DBFile> LoadData(FileId fileId);

    DBFile GetInfo(FileId fileId);

    bool FileExists(FileId? fileId);
}