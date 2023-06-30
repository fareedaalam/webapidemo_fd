using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IStateInterface
    {
        IEnumerable<StateEntity> GetStateById(Guid stateId);
        IEnumerable<StateEntity> GetAllStates();
        Guid CreateState(StateEntity stateEntity);
        bool UpdateState(Guid stateId, StateEntity stateEntity);
        bool DeleteState(Guid stateId);
        FunctionResponse GetStateByCountryId(Guid id);
    }
}
