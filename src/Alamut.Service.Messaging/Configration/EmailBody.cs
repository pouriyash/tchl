namespace Service.Messaging.Configration
{
    public class EmailBody<TId>
    {
        public TId Id { get; set; } 
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}