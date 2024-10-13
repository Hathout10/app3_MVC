using app3.DaL.models;
using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace app3.PL.Helper
{
	public static class EmailSettings
	{


		public static void SendEmail(Email email)
		{
			// Mail Server : gmail.com

			// Smtp
			// Note : خطوات ثابته
			var client = new SmtpClient("smtp.gmail.com", 587);

			client.EnableSsl = true; // عشان يشفر البورت ده ال 587 بس مش هيتشفر لاني مش معايا شهاده


			//client.Credentials = new NetworkCredential("mohammedatta095@gmail.com", "ecnqvfkiqraxoorz");
			client.Credentials = new NetworkCredential("medohathout55@gmail.com", "fhkhgsgdmaidbtvn");
			

			client.Send("medohathout55@gmail.com", email.To, email.subject, email.body);

		}



	}
}
