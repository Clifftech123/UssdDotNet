namespace ussdDotNet.Models
{
    public static class UssdModel
    {


        public class UssdResponse
        {
            public string Message { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
        }

        public class UssdRequestData
        {
            public string Type { get; set; } = string.Empty;
            public string Mobile { get; set; } = string.Empty;
            public string SessionId { get; set; } = string.Empty;
            public string ServiceCode { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public string Operator { get; set; } = string.Empty;
        }
    }
}
