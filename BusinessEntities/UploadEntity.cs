using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    public class UploadEntity
    {
        public System.Guid Id { get; set; }
        [Required]
        public string RegistNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Standard { get; set; }
        [Required]
        public string Section { get; set; }
        public Nullable<System.Guid> SchoolId { get; set; }
        public string Subjects { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public Nullable<System.DateTime> SubsciptionStartDate { get; set; }
        public Nullable<System.DateTime> SubscriptionEndDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
    public class ExcelData
    {
        [Required]
        public Nullable<System.Guid> SchoolId { get; set; }
        [Required]
        public string UserType { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public List<UploadEntity> excelData { get; set; }
    }
}