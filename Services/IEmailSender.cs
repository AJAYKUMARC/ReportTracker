using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportTracker.Services
{
    public interface IEmailSender
    {
        public bool SendConformationEmail(string toAddress, string htmlBody);
    }
}
