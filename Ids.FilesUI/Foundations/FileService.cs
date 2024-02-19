using Ids.FilesUI.Exceptions;
using Ids.FilesUI.Models;
using Microsoft.Extensions.Configuration;

namespace Ids.FilesUI.Foundations;

public partial class FileService : IFileService
{
    private readonly FileStorage fileStorage;

    public FileService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("mobility") ?? string.Empty;
        fileStorage = new FileStorage(connectionString);
    }

    public async ValueTask SaveFile(FileId fileId, string fileName, byte[] data)
    {
        var fileToSave = new DBFile(fileId, fileName, data);
        await SaveFile(fileToSave);
    }

    public async ValueTask SaveFile(DBFile file)
    {
        try
        {
            ValidateFile(file);
            ValidateFileNotExists(file.FileId);
            await fileStorage.InsertFile(file);
        }
        catch (InvalidFileException exception)
        {
            throw new FileServiceException("Fichier invalide", exception);
        }
        catch (FileAlreadyExists exception)
        {
            throw new FileServiceException("Un fichier avec le même Id existe déja", exception);
        }
        catch (Exception exception)
        {
            throw new FileServiceException("Echec de la sauvegarde du fichier", exception);
        }
    }

    public bool FileExists(FileId? fileId) => fileStorage.FileExists(fileId);

    public bool FileLocked(FileId fileId) => fileStorage.FileLocked(fileId);

    public async ValueTask UpdateFile(FileId fileId, string fileName, byte[] data)
    {
        DBFile file = new(fileId, fileName, data);
        await UpdateFile(file);
    }

    public async ValueTask UpdateFile(DBFile file)
    {
        try
        {
            ValidateFile(file);

            await fileStorage.UpdateFile(file);
        }
        catch (InvalidFileException exception)
        {
            throw new FileServiceException("Fichier invalide", exception);
        }
        catch (FileNotFoundException exception)
        {
            throw new FileServiceException("Fichier inexistant", exception);
        }
        catch (Exception exception)
        {
            throw new FileServiceException("Echec de la modification du fichier", exception);
        }
    }

    public async ValueTask UpdateFile(FileId fileId, bool used = true)
    {
        try
        {
            ValidateFileExists(fileId);
            await fileStorage.UpdateFile(fileId, used);
        }
        catch (InvalidFileException exception)
        {
            throw new FileServiceException("Fichier invalide", exception);
        }
        catch (FileNotFoundException exception)
        {
            throw new FileServiceException("Fichier inexistant", exception);
        }
        catch (Exception exception)
        {
            throw new FileServiceException("Echec de la modification du fichier", exception);
        }
    }

    public async ValueTask DeleteFile(FileId fileId)
    {
        try
        {
            await fileStorage.DeleteFile(fileId);
        }
        catch (Exception exception)
        {
            throw new FileServiceException("Echec de la suppression du fichier", exception);
        }
    }

    public DBFile GetInfo(FileId fileId)
    {
        try
        {
            DBFile file = fileStorage.SelectFileInfo(fileId);

            return file;
        }
        catch (FileNotFoundException exception)
        {
            throw new FileServiceException("Fichier inexistant", exception);
        }
        catch (InvalidFileException exception)
        {
            throw new FileServiceException("Fichier invalide", exception);
        }
        catch (Exception exception)
        {
            throw new FileServiceException("Echec du chargement du fichier", exception);
        }
    }

    public async ValueTask<DBFile> LoadData(FileId fileId)
    {
        try
        {
            ValidateFileExists(fileId);
            return await fileStorage.SelectFileData(fileId);
        }
        catch (FileNotFoundException exception)
        {
            throw new FileServiceException("Fichier inexistant", exception);
        }
        catch (InvalidFileException exception)
        {
            throw new FileServiceException("Fichier invalide", exception);
        }
        catch (Exception exception)
        {
            throw new FileServiceException("Echec du chargement du fichier", exception);
        }
    }
}