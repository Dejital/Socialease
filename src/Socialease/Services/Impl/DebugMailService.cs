using System.Diagnostics;

namespace Socialease.Services.Impl
{
    internal class DebugMailService : IMailService
    {
        public bool SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail: To {to}, Subject: {subject}");
            return true;
        }
    }
}