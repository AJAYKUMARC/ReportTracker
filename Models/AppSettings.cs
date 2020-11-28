using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportTracker.Models
{
    public class AppSettings
    {
        public string FromAddress { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Subject { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }

}
