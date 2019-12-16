using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for Email
/// </summary>
public static class Email
{
    //Generates a SmtpClient connection
    private static SmtpClient getClient()
    {
        SmtpClient client = new SmtpClient("dungeonbuddy.net");
        client.Credentials = new System.Net.NetworkCredential("mailbot@dungeonbuddy.net", "Ditronian!1472");
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        return client;
    }

    //Sends the email based on the provided values.
    public static bool sendEmail(string sendAddress, string subject, string body, string sender = "Dungeon Buddy")
    {
        MailMessage message = new MailMessage("mailbot@dungeonbuddy.net", sendAddress);
        message.From = new MailAddress("mailbot@dungeonbuddy.net", sender);
        message.Subject = subject;
        message.Body = body;

        try
        {
            getClient().Send(message);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}