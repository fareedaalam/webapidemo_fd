using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class SchoolEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContPerson { get; set; }
        public string ContNumber { get; set; }
        public System.Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public System.Guid StateId { get; set; }
        public string StateName { get; set; }
        public string PinCode { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public List<UserEntity> SchlUser { get; set; }
    }
}
