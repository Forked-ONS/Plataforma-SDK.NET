namespace ONS.PlataformaSDK.Domain
{
    public class Metadata
    {
        public string Branch { get; set; }
        public string Type { get; set; }
        public string ChangeTrack { get; set; }

        public Metadata(string branch, string type, string changeTrack)
        {
            this.Branch = branch;
            this.Type = type;
            this.ChangeTrack = changeTrack;            
        }
    }
}