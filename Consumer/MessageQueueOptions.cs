namespace Consumer
{
    public class MessageQueueOptions
    {
        public const string Section = "MessageQueue";

        public bool UseAzureBus { get; set; }
        public string? ConnectionString { get; set; }
    }
}
