using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.Interface
{
   public interface IEmailInterface
    {
       FunctionResponse SendEmail(string ToAddress, string Cc, string Subject, string Body);
        FunctionResponse SendEmailBYSMTP(string ToAddress, string Cc, string Subject, string Body);
        
    }
}
