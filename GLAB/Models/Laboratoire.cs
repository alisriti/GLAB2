namespace GLAB.Models
{
    public class Laboratoire
    {
        public string Id { get; set; }
        public string Acronyme { get; set; }
        public string Nom { get; set; }
        public byte[] Logo { get; set; } = new byte[] { };
    }
}