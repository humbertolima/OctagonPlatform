namespace OctagonPlatform.Helpers
{
    public class KeyManager
    {
        public int Id { get; set; }
        public string Zmk { get; set; }
        public string Checksum { get; set; }
        public string TerminalId { get; set; }
        public int Idk1 { get; set; }
        public int Idk2 { get; set; }
        public string Pwk { get; set; }
        public K1 K1 { get; set; }
        public K2 K2 { get; set; }
    }
}