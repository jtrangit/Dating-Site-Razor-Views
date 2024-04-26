using System;
using System.Net;
using System.Net.Mail;

namespace Utilities
{
    public class Email
    {
        private readonly MailMessage objMail = new MailMessage();
        private MailAddress toAddress;
        private MailAddress fromAddress;
        private MailAddress ccAddress;
        private MailAddress bccAddress;
        private string subject;
        private string messageBody;
        private bool isHTMLBody = true;
        private MailPriority priority = MailPriority.Normal;
        private string mailHost = "smtp.temple.edu";

        public void SendMail(string recipient, string sender, string subject, string body, string cc = "", string bcc = "")
        {
            try
            {
                this.Recipient = recipient;
                this.Sender = sender;
                this.Subject = subject;
                this.Message = body;

                objMail.To.Add(this.toAddress);
                objMail.From = this.fromAddress;
                objMail.Subject = this.subject;
                objMail.Body = this.messageBody;
                objMail.IsBodyHtml = this.isHTMLBody;
                objMail.Priority = this.priority;

                if (!string.IsNullOrEmpty(cc))
                {
                    this.CCAddress = cc;
                    objMail.CC.Add(this.ccAddress);
                }

                if (!string.IsNullOrEmpty(bcc))
                {
                    this.BCCAddress = bcc;
                    objMail.Bcc.Add(this.bccAddress);
                }

                using (var smtpMailClient = new SmtpClient(this.mailHost))
                {
                    smtpMailClient.Send(objMail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendMail()
        {
            try
            {
                objMail.To.Add(this.toAddress);
                objMail.From = this.fromAddress;
                objMail.Subject = this.subject;
                objMail.Body = this.messageBody;
                objMail.IsBodyHtml = this.isHTMLBody;
                objMail.Priority = this.priority;

                if (!string.IsNullOrEmpty(this.ccAddress?.Address))
                {
                    objMail.CC.Add(this.ccAddress);
                }

                if (!string.IsNullOrEmpty(this.bccAddress?.Address))
                {
                    objMail.Bcc.Add(this.bccAddress);
                }

                using (var smtpMailClient = new SmtpClient(this.mailHost))
                {
                    smtpMailClient.Send(objMail);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string Recipient
        {
            get { return this.toAddress?.Address; }
            set { this.toAddress = new MailAddress(value); }
        }

        public string Sender
        {
            get { return this.fromAddress?.Address; }
            set { this.fromAddress = new MailAddress(value); }
        }

        public string CCAddress
        {
            get { return this.ccAddress?.Address; }
            set { this.ccAddress = new MailAddress(value); }
        }

        public string BCCAddress
        {
            get { return this.bccAddress?.Address; }
            set { this.bccAddress = new MailAddress(value); }
        }

        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }

        public string Message
        {
            get { return this.messageBody; }
            set { this.messageBody = value; }
        }

        public bool HTMLBody
        {
            get { return this.isHTMLBody; }
            set { this.isHTMLBody = value; }
        }

        public MailPriority Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        public string MailHost
        {
            get { return this.mailHost; }
            set { this.mailHost = value; }
        }
    }
}
