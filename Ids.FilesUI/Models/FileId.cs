namespace Ids.FilesUI.Models;

public class FileId
{
    public string Value { get; }

    public FileId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Value = Guid.NewGuid().ToString();
            return;
        }

        try
        {
            Guid newValue = Guid.Parse(value);
            Value = newValue.ToString();
        }
        catch
        {
            throw new Exception($"Bad format: {value}");
        }
    }

    public FileId() => Value = Guid.NewGuid().ToString();

    public static bool operator ==(FileId a, FileId b) => a.Value.Equals(b.Value);

    public static bool operator !=(FileId a, FileId b) => !(a == b);
}