using System.Data;

namespace Ids.FilesUI.Models;

public class DBFile
{
    public FileId FileId { get; set; }
    public string FileName { get; set; }
    public byte[] Data { get; set; }
    public bool HasData { get; set; }
    public bool IsValid => !string.IsNullOrWhiteSpace(FileId.Value) && !string.IsNullOrWhiteSpace(FileName) && HasData;

    public DBFile()
    {
        Data = new byte[] { };
        HasData = false;
    }

    public DBFile(FileId fileId) : this() => FileId = fileId;

    public DBFile(FileId fileId, string fileName, byte[] data)
    {
        FileId = fileId;
        FileName = fileName;
        Data = data;
        HasData = data is null ? false : data.Length > 0;
    }

    public DBFile(DBFile file)
    {
        FileId = file.FileId;
        FileName = file.FileName;
        Data = file.Data;
        HasData = file.Data is null ? false : file.Data.Length > 0;
    }

    public DBFile(string fileName, byte[] data) : this()
    {
        FileName = fileName;
        Data = data;
        HasData = data is null ? false : data.Length > 0;
    }

    public DBFile(DataRow row, bool includeData)
    {
        FileId = new FileId((string)row["FileId"]);
        FileName = (string)row["FileName"];
        HasData = ((byte[])row["Data"]).Length != 0;
        if (includeData) Data = (byte[])row["Data"];
    }

    public DBFile(DataRow row)
    {
        FileId = new((string)row["FileId"]);
        FileName = (string)row["FileName"];
        HasData = Convert.ToInt32(row["DataLen"]) != 0;
    }
}