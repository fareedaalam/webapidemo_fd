using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IStandardInterface
    {
        FunctionResponse GetById(Guid Id);
        FunctionResponse GetAll();
        FunctionResponse Create(StandardEntity entity);
        FunctionResponse Update(Guid Id, StandardEntity entity);
        FunctionResponse Delete(Guid Id);
    }
}
