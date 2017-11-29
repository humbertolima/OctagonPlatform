using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace OctagonPlatform.Helpers
{
    public static class Email
    {
        public static void SendNotification()   //pendiente recibir parametros para emvio de correo.
        {
            WebMail.EnableSsl = true;
            WebMail.From = "luisrafael.gamez@outlook.com";
            WebMail.SmtpPort = 25;
            WebMail.UserName = "luisrafael.gamez@outlook.com";
            WebMail.SmtpServer = "smtp.live.com";
            WebMail.Password = "Vv19477002";
            WebMail.SmtpUseDefaultCredentials = true;

            WebMail.Send("yasser.osuna@gmail.com", "Error en el Api "," Enviando notificaciones de alertas de terminal");

        }
    }
}