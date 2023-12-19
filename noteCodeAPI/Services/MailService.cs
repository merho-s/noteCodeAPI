using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace noteCodeAPI.Services
{
	public class MailService
	{
		public MailService()
		{
		}

		public void SendMail(string targetEmail, string username)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("noteCode", "noreply@notecode.fr"));
			message.To.Add(new MailboxAddress(username, targetEmail));
			message.Subject = "Confirmation of your access request received";
			message.Body = new TextPart("plain")
			{
				Text = @"Hey new noteCoder,

Thank you for your confidence, you will be inform soon if your request has been accepted.

.noteCode"
			};

			using(var client = new SmtpClient())
			{
				client.Connect("smtp.ionos.fr", 465, true);
				client.Authenticate("noreply@notecode.fr", "KerdNCoussi.54");
				client.Send(message);
				client.Disconnect(true);
			}



		}
	}
}

