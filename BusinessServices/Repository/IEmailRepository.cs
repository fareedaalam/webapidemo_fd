using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Configuration;
using System.Net.Mail;

using System.Runtime.InteropServices;
//using Outlook = Microsoft.Office.Interop.Outlook;

namespace BusinessServices.Repository
{
    class IEmailRepository : IEmailInterface
    {
        public enum MyMail
        {
            MailFromAddress,
            MailFromPwd,
            MailToAddress,
            MailCc,
            MailSubject,
            MailBody,
            MailAttachment            
        }
        //public FunctionResponse SendEmail(string ToAddress, string Cc, string Subject, string Body)
        //{
        //    FunctionResponse RMsg = new FunctionResponse();
        //    try
        //    {

        //        // Create the Outlook application.
        //        Outlook.Application oApp = new Outlook.Application();
        //        // Create a new mail item.
        //        Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
        //        // Set HTMLBody. 
        //        //add the body of the email
        //        if (!string.IsNullOrWhiteSpace(Body))
        //        {
        //            oMsg.HTMLBody = Body;
        //        }
        //        //Add an attachment.
        //        String sDisplayName = "MyAttachment";
        //        int iPosition = (int)oMsg.Body.Length + 1;
        //        int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
        //        //now attached the file
        //        //Outlook.Attachment oAttach = oMsg.Attachments.Add
        //        //                             (@"C:\\fileName.jpg", iAttachType, iPosition, sDisplayName);
        //        //Subject line
        //        if (!string.IsNullOrWhiteSpace(Subject))
        //        {
        //            oMsg.Subject = Subject;
        //        }

        //        //Add CC
        //        if (!string.IsNullOrWhiteSpace(Cc))
        //        {
        //            oMsg.CC = Cc;
        //        }
        //        // Add a recipient.

        //        Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;

        //        if (!string.IsNullOrWhiteSpace(ToAddress))

        //        {
        //            string[] arrAddTos = ToAddress.Split(new char[] { ';', ',' });

        //            foreach (string strAddr in arrAddTos)
        //            {

        //                if (!string.IsNullOrWhiteSpace(strAddr) && strAddr.IndexOf('@') != -1)
        //                {
        //                    // Change the recipient in the next line if necessary.
        //                    Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(strAddr.Trim());
        //                    oRecip.Resolve();

        //                }
        //                else
        //                {

        //                    throw new Exception("Bad to-address: " + ToAddress);
        //                }

        //            }

        //        }

        //        else
        //        {

        //            throw new Exception("Must specify to-address");
        //        }




        //        // Send.
        //        oMsg.Send();
        //        // Clean up.
        //        // oRecip = null;
        //        oRecips = null;
        //        oMsg = null;
        //        oApp = null;

        //        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
        //        RMsg.Message = "Success";

        //    }//end of try block
        //    catch (Exception)
        //    {
        //        RMsg.Status = FunctionResponse.StatusType.ERROR;
        //        RMsg.Message = ex.ToString();

        //    }//end of catch

        //    return RMsg;
        //}

        public FunctionResponse SendEmail(string ToAddress, string Cc, string Subject, string Body)
        {
            FunctionResponse RMsg = new FunctionResponse();
            RMsg.Message = "Fail";
            try
            {
                //Create Mail Message
                MailMessage mail = new MailMessage();
                mail.To.Add(ToAddress);               
                mail.From = new MailAddress(ConfigurationManager.AppSettings["P2PMailId"].ToString().Trim());
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                //SMTP Client SetUP
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.office365.com"; 
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential
                     (ConfigurationManager.AppSettings["P2PMailId"].ToString().Trim(), ConfigurationManager.AppSettings["P2PMailPwd"].ToString().Trim()); // ***use valid credentials***
                           
                smtp.Send(mail);

                RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                RMsg.Message = "Success";

            }//end of try block
            catch (Exception ex)
            {
                RMsg.Status = FunctionResponse.StatusType.ERROR;
                RMsg.Message = ex.ToString();

            }//end of catch

            return RMsg;
        }

        public FunctionResponse SendEmailBYSMTP(string ToAddress, string Cc, string Subject, string Body)
        {
            throw new NotImplementedException();
        }
    }
}
