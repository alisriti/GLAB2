namespace Ids.FilesUI.Exceptions;

public class FileServiceException : Exception
{
    public FileServiceException(string message, Exception innerException) : base(message, innerException)
    { }

    public FileServiceException(string message) : base(message)
    { }
}