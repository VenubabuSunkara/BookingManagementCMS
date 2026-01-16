namespace Booking.Application.DTOs
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = "";
        public int Port { get; set; }
        public string SenderName { get; set; } = "";
        public string SenderEmail { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
    public class GoogleSettings
    {
        public string PlacesApiKey { get; set; } = string.Empty;
    }

    public class SendGridSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
    }

}
