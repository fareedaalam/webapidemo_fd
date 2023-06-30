using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IMailerInterface
    {
        FunctionResponse GetById(Guid Id);
        FunctionResponse GetByName(string Name);
        FunctionResponse GetAll();
        FunctionResponse Create(MailerEntity entity);
        FunctionResponse Update(Guid Id, MailerEntity entity);
        FunctionResponse Delete(Guid Id);
    }
}
