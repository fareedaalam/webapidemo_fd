using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface ILocationInterface
    {
        FunctionResponse GetLocationById(Guid locationId);
        FunctionResponse GetAllLocation();
        Guid CreateLocation(LocationEntity locationEntity);
        bool UpdateLocation(Guid locationId, LocationEntity locationEntity);
        bool DeleteLocation(Guid locationId);
        FunctionResponse GetLocationByCityId(Guid CityId);
    }
}
