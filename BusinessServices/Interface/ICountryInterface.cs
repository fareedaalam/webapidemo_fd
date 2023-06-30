using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface ICountryInterface
    {
        CountryEntity GetCountryById(Guid countryId);
        IEnumerable<CountryEntity> GetAllCountry();
        FunctionResponse CreateCountry(CountryEntity CountryEntity);
        FunctionResponse UpdateCountry(Guid countryId, CountryEntity countryEntity);
        bool DeleteCountry(Guid countryId);
    }
}
