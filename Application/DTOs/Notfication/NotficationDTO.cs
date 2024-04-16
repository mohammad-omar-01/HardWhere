namespace Application.DTOs.Notfication
{
    public class NotficationDTO
    {
        public int userId { get; set; }
        public string NotficationTitle { get; set; } = string.Empty;
        public string NotficationBody { get; set; } = string.Empty;
        public string NotficationType { get; set; } = string.Empty;
    }
}
