namespace Common.Messages
{
    public class MessageType1
    {
        public int EntityId { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public string TraceId { get; set; }
        public DateTime Date { get; set; }
    }
}
