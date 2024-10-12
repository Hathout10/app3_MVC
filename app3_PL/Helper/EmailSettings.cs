using app3.DaL.models;
using System.Net;
using System.Net.Mail;

namespace app3.PL.Helper
{
	public static class EmailSettings
	{
		//public static void sendEmail(Email email)
		//{

		//	//Mail server : gmail.com

		//	//smtp

		//	var client = new SmtpClient("smtp.gmail.com", 587);
		//	client.EnableSsl = true;

		//	client.Credentials = new NetworkCredential("medohathout55@gmail.com", "yhyqhldrkgxyabry");

		//	client.Send("medohathout55@gmail.com",email.To,email.subject,email.body);

		//}

		//public static async void SendEmailAsync(Email email)
		//{
		//    try
		//    {
		//        //var fromAddress = "medohathout55@gmail.com";
		//        //var fromPassword = "yhyqhldrkgxyabry"; // Your app password

		//        var smtpClient = new SmtpClient("smtp.gmail.com", 587)
		//        {
		//            EnableSsl = true,
		//            Credentials = new NetworkCredential("aliaatarek.route@gmail.com","Aliaa123" )
		//        };



		//        await smtpClient.SendMailAsync("medohathout55@gmail.com", email.To, email.subject, email.body);


		//        //await smtpClient.SendMailAsync(mailMessage);

		//        Console.WriteLine("Email sent successfully!");
		//}
		//    catch (SmtpException ex)
		//    {
		//        Console.WriteLine($"SmtpException: {ex.Message}");
		//        Console.WriteLine($"Status Code: {ex.StatusCode}");
		//    }
		//    catch (Exception ex)
		//    {
		//        Console.WriteLine($"Exception: {ex.Message}");
		//    }
		//}

		public static void SendEmail(Email email)
		{
			try
			{
				//var fromAddress = "medohathout55@gmail.com";
				//var fromPassword = "yhyqhldrkgxyabry"; // Your app password

				var smtpClient = new SmtpClient("smtp.gmail.com", 587);

				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = true;
				smtpClient.Credentials = new NetworkCredential("aliaatarek.route@gmail.com", "sxuvcmyketquafsj");



				//C:\Users\Options\AppData\Local\Temp\tmp9308.tmp
				smtpClient.Send("aliaatarek.route@gmail.com", email.To, email.subject, email.body);


				//await smtpClient.SendMailAsync(mailMessage);
			}

			catch (Exception ex)
			{
				Console.WriteLine($"Exception: {ex.Message}");
			}
		}


	}
}
