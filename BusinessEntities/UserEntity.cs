using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UserEntity
    {
        public System.Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Mobile { get; set; }
        public string Address2 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public string LoginId { get; set; }
        public string Pwd { get; set; }
        public Nullable<System.Guid> StandardId { get; set; }
        public Nullable<System.Guid> Qualification { get; set; }
        public Nullable<System.Guid> CountryId { get; set; }
        public Nullable<System.Guid> StateId { get; set; }
        public Nullable<System.Guid> CityId { get; set; }
        public Nullable<System.Guid> LocationId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string AlternatePhone { get; set; }
        public string AlternateEmail { get; set; }
        public Nullable<bool> EmailVerified { get; set; }
        public string Occupation { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string PinCode { get; set; }

        public string Subjects { get; set; }
        public Nullable<System.Guid> BoardId { get; set; }

        public string ImageB64 { get; set; }

        //Additionla Property added here for map role user
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        //For dynamic adding child to Parent/Teacher
        public Guid ParentId { get; set; }
        public Guid TeacherId { get; set; }

        //for School mapping
        public Nullable<System.Guid> SchoolId { get; set; }
        public string SchoolName { get; set; }

        //bulk registration
        public string StudentId { get; set; }

        public string  Hashcode { get; set; }

        public Guid SectionId { get; set; }
        public string SectionName { get; set; }
        public Nullable<bool> IsMath { get; set; }
        public Nullable<bool> IsPhysics { get; set; }
        public Nullable<bool> IsChemistry { get; set; }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MapRoleUserEntity> mapRoleUser { get; set; }
        public string Standard { get; set; }
        
        //Default value of ChildAllowd is 4
        public Nullable<int> ChildAllowed { get; set; }
    }

    //public class UserDetails
    //{
    //    public System.Guid Id { get; set; }
    //    public System.Guid UserId { get; set; }
    //    public System.DateTime CreatedOn { get; set; }
    //    public Nullable<System.Guid> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<System.Guid> UpdatedBy { get; set; }
    //    public Nullable<bool> IsActive { get; set; }
    //    public Nullable<System.Guid> SubjectId { get; set; }
    //    public Nullable<int> ChildAllowed { get; set; }
    //}


}
