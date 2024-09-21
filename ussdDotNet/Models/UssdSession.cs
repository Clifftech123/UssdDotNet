using System.ComponentModel.DataAnnotations;
using ussdDotNet.Contracts;
using ussdDotNet.DBContext;

namespace ussdDotNet.Models
{
    public class UssdSession
    {
    
        public UssdSession()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Type = string.Empty;
            SessionId = string.Empty;
            Tag = string.Empty;
            Mobile = string.Empty;
            ServiceCode = string.Empty;
            Message = string.Empty;
            Operator = string.Empty;
            MessageDescription = string.Empty;
            MerchantName = string.Empty;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        [StringLength(300)]
        public string SessionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string ServiceCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Message { get; set; }

        [StringLength(255)]
        public string MessageDescription { get; set; }

        [StringLength(100)]
        public string Tag { get; set; }

        [StringLength(50)]
        public string Operator { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public long? MerchantId { get; set; }

        [StringLength(100)]
        public string MerchantName { get; set; }


        //  Save session method
        public static void SaveSession(UssdDBAppContext context, AppSettings appSettings, string requestType, string sessionId, string mobileNumber, string network, string message, string tag, string description)
        {
            var dateTimeNow = DateTime.Now;

            var usd = new UssdSession
            {
                Type = requestType,
                SessionId = sessionId,
                Mobile = mobileNumber,
                ServiceCode = appSettings.ShortCode,
                Message = message,
                MessageDescription = description ?? string.Empty,
                Operator = network,
                CreatedAt = dateTimeNow,
                UpdatedAt = dateTimeNow,
                Tag = tag,
            };

            context.UssdSessions.Add(usd);
            context.SaveChanges();
        }
    }
}
