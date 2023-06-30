using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class LocationEntity
    {
        public System.Guid Id { get; set; }
      
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }


        public Nullable<System.Guid> CountryId { get; set; }
        public string CountryName { get; set; }

        public Nullable<System.Guid> StateId { get; set; }
        public string StateName { get; set; }

        public Nullable<System.Guid> CityId { get; set; }
        public string CityName { get; set; }




    }
}
