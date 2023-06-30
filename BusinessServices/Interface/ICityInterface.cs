using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface ICityInterface
    {
        FunctionResponse GetcityById(Guid cityId);
        FunctionResponse GetAllcities();
        Guid CreateCity(CityEntity cityEntity);
        bool UpdateCity(Guid cityId, CityEntity cityEntity);
        bool DeleteCity(Guid cityId);
        FunctionResponse GetCityByStateId(Guid id);
    }
}
