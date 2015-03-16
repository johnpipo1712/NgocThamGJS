using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using W_GJS.Models;
namespace W_GJS.General
{
    public class Process
    {
        public static class ProcessSendMail
        {
            public static bool SendMail_Contract(ContractModel contract)
            {
                string Subject = "Thông tin đơn hàng gửi công ty TNHH Ngọc Thẫm";


                string str = "<html>"
                           + "Xin chào,Chúng tôi xin gửi thông tin đơn đặt hàng theo yêu cầu từ khách hàng như sau :"

                           + "<P>Tên người gửi : " + contract.Name + "</P></br>"

                           + "<P>Tên công ty : " + contract.Company + "</P></br>"

                           + "<P>Địa chỉ : " + contract.Address + "</P></br>"

                           + "<P>Số điện thoại : " + contract.Phone + "</P></br>"

                           + "<P>Email : " + contract.Email + "</P></br>"

                           + "<P>Nội dung đặt hàng : " + contract.Note + "</P></br>"

                           + "</html>";

                MailMessage mail = new MailMessage(MailGmail, "ngocthamso1@yahoo.com", Subject, str);
                mail.IsBodyHtml = true;
                try
                {
                    SmtpClient client = new SmtpClient();
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    // setup Smtp authentication
                    NetworkCredential credentials = new NetworkCredential(MailGmail, Pass);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;
                    client.Send(mail);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }


            public static readonly string MailGmail = "ngocthamgold@gmail.com";
            public static readonly string Pass = "john@0938613461";

        }

        public static bool CheckExtensionImg(string extension)
        {
            string[] extensions = { "jpg", "jpeg", "gif", "png", "bmp", "jpe", "jfif", "tif", "JPG", "JPEG", "GIF", "PNG", "BMP", "JPE", "JFIF", "TIF" };

            int count = 0;
            foreach (string item in extensions)
            {
                if (extension == item)
                    count++;
            }
            if (count == 1)
                return true;

            return false;
        }
    }
}