using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IUserLogInterface
    {
        UserLogEntity GetById(Guid id);
        IEnumerable<UserLogEntity> GetAll();
        Guid Create(UserLogEntity userLogEntity);
        bool Update(Guid id, UserLogEntity userLogEntity);
        bool Delete(Guid id);
    }
}
