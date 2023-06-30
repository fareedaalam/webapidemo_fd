using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;

using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BusinessServices.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly UnitOfWork _unitOfWork;
        // private object _IEmailInterface;

        public UserRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        // Public method to authenticate user by user name and password.
        public Guid? Authenticate(string userName, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Email == userName && u.Pwd == password);
            var userstud = _unitOfWork.UserRepository.Get(u => u.LoginId == userName && u.Pwd == password);
            //  var allrole= _unitOfWork.UserRepository.g
            //var k = new Guid("A204E098-6D2E-470B-9C9C-BA530A531B6B");
            //var RoleMap = _unitOfWork.MapRoleUserRepository.GetRefrenceID(k);

            // Load one blogs and its related posts 
            //  var blog1 = _unitOfWork.RoleRepository.GetByID(user.Id)
            //               .Include(b => b.Posts).FirstOrDefault();

            // Load one blogs and its related posts var blog1 = context.Blogs .Where(b => b.Name == "ADO.NET Blog") .Include(b => b.Posts) .FirstOrDefault(); 

            if (user != null || userstud != null)
            {
                return user.Id;
            }
            return null;
        }
        public FunctionResponse CreateUser(UserEntity userEntity)
        {
            try
            {

                FunctionResponse RMsg = new FunctionResponse();
                if (userEntity != null)
                {
                    var user = new tbl_User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = userEntity.FirstName == null ? null : userEntity.FirstName.Trim(),
                        MiddleName = userEntity.MiddleName == null ? null : userEntity.MiddleName.Trim(),
                        LastName = userEntity.LastName == null ? null : userEntity.LastName.Trim(),
                        Email = userEntity.Email == null ? null : userEntity.Email.Trim(),
                        Address1 = userEntity.Address1 == null ? null : userEntity.Address1.Trim(),
                        Address2 = userEntity.Address2 == null ? null : userEntity.Address2.Trim(),
                        Mobile = userEntity.Mobile == null ? null : userEntity.Mobile.Trim(),
                        LoginId = userEntity.LoginId == null ? null : userEntity.LoginId,
                        Pwd = userEntity.Pwd == null ? null : userEntity.Pwd.Trim(),

                        StandardId = userEntity.StandardId,
                        Qualification = userEntity.Qualification,
                        CountryId = userEntity.CountryId,
                        StateId = userEntity.StateId,
                        CityId = userEntity.CityId,
                        LocationId = userEntity.LocationId,

                        FatherName = userEntity.FatherName == null ? null : userEntity.FatherName.Trim(),
                        MotherName = userEntity.MotherName == null ? null : userEntity.MotherName.Trim(),
                        AlternatePhone = userEntity.AlternatePhone == null ? null : userEntity.AlternatePhone.Trim(),
                        AlternateEmail = userEntity.AlternateEmail == null ? null : userEntity.AlternateEmail.Trim(),

                        Occupation = userEntity.Occupation == null ? null : userEntity.Occupation.Trim(),
                        State = userEntity.State == null ? null : userEntity.State.Trim(),
                        City = userEntity.City == null ? null : userEntity.City.Trim(),
                        Location = userEntity.Location == null ? null : userEntity.Location.Trim(),
                        PinCode = userEntity.PinCode == null ? null : userEntity.PinCode.Trim(),
                        Subjects = userEntity.Subjects == null ? null : userEntity.Subjects.Trim(),
                        BoardId = userEntity.BoardId,
                        ImageB64 = userEntity.ImageB64,

                        CreatedOn = DateTime.Now,
                        CreatedBy = userEntity.CreatedBy,
                        IsActive = true
                    };

                    _unitOfWork.UserRepository.Insert(user);

                    //Add Role To User......................

                    if (userEntity.RoleId != Guid.Empty && userEntity.RoleId != null)
                    {
                        var map = new tbl_Map_Role_User
                        {
                            UserId = user.Id,
                            RoleId = userEntity.RoleId
                        };
                        _unitOfWork.MapRoleUser.Insert(map);
                    }
                    else
                    {

                        RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                        RMsg.Message = "Please Provide Role Type";
                    }

                    //Map Child to Respective Parrent
                    if (userEntity.ParentId != Guid.Empty)
                    {
                        var map = new tbl_MapParentChild
                        {
                            ParentId = userEntity.ParentId,
                            ChildId = user.Id,
                            IsActive = false,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userEntity.ParentId
                        };
                        _unitOfWork.MapParentChildRepository.Insert(map);
                    }
                    //Map Child to Respective Teacher
                    if (userEntity.TeacherId != Guid.Empty)
                    {
                        var map = new tbl_MapTeacherChild
                        {
                            TeacherId = userEntity.TeacherId,
                            ChildId = user.Id,
                            IsActive = false
                        };
                        _unitOfWork.MapTeacherChildRepository.Insert(map);
                    }


                    if (_unitOfWork.Save() > 0)
                    {

                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Data.Add(user.Id);

                        //if (userEntity.RoleId != Guid.Empty && userEntity.RoleId != null)
                        //{
                        //    var UserId = user.Id;
                        //    var RoleId = userEntity.RoleId;
                        //    MapRoleUserRepository maprole = new MapRoleUserRepository(_unitOfWork);
                        //    maprole.AssignRoleToUser(RoleId, UserId);
                        //    //Set Success
                        //    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        //    RMsg.Data.Add(user.Id);
                        //}
                        //else
                        //{

                        //    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                        //    RMsg.Message = "Please Provide Role Type";
                        //}

                        ////Map Child to Respective Parrent
                        //if (userEntity.ParentId != null)
                        //{
                        //    MapParentChildEntity MapChild = new MapParentChildEntity();
                        //    MapChild.ParentId = userEntity.ParentId;
                        //    MapChild.Email = user.Email;
                        //    _iMapParentChildInterface.AssignChildToPerent(MapChild);
                        //}
                    }
                    else
                    {
                        RMsg.Message = "No_Record_Found";
                        RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    }
                }
                return RMsg;
            }

            catch (Exception ex)
            {
                throw;
            }

        }

        public static string GeneratePassword(int lowercase, int uppercase, int numerics)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";

            Random random = new Random();

            string generated = "!";
            for (int i = 1; i <= lowercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= uppercase; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= numerics; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            return generated.Replace("!", string.Empty);

        }

        public FunctionResponse GetUserExistence(string loginid)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<UserEntity> list = _unitOfWork.UserRepository.GetUser().Where(x => x.LoginId == loginid).ToList<UserEntity>();
                //.Where(x => x.Email == EmailId.Trim()).ToList<tbl_User>();

                if (list.Count > 0)
                {
                    // var dataModel = Mapper.Map<<tbl_User>, <UserEntity>>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(list);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public FunctionResponse CreateBulkUser(List<UserEntity> userEntity)
        {
            bool userexists = false;
            try
            {

                FunctionResponse RMsg = new FunctionResponse();
                if (userEntity != null)
                {

                    foreach (var item in userEntity)
                    {
                        string pattern = null;
                        pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";



                        if (item.StudentId == null || item.FirstName == null || item.LastName == null || item.StandardId == null || item.Mobile == null || item.MotherName == null || item.FatherName == null || (item.IsMath == false && item.IsChemistry == false && item.IsPhysics == false) || Regex.IsMatch(item.Email, pattern) == false)
                        {
                            RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                            RMsg.Message = "The Excel File Uploaded is not Correct !!!";
                            break;
                        }

                        if (item.RoleName == "Student" && item.LoginId != null)
                        {
                            RMsg = this.GetUserExistence(item.LoginId.Trim());

                            if (RMsg.Status == FunctionResponse.StatusType.SUCCESS)
                            {
                                RMsg.Status = FunctionResponse.StatusType.WARNING;
                                RMsg.Message = "Duplicate Records Found !!";
                                continue;
                            }
                        }

                        char fstltr = item.FirstName.ElementAt(0);
                        char lstltr = item.LastName.ElementAt(0);
                        String randomPassword = GeneratePassword(1, 1, 1);
                        //check if the User already exists.
                        List<SubjectEntity> usersubj = new List<SubjectEntity>();
                        SubjectEntity newsubj;
                        if (item.RoleName != "Student" && item.Email != null)
                        {
                            RMsg = this.GetUserByEmailId(item.Email.Trim());

                            if (RMsg.Status == FunctionResponse.StatusType.SUCCESS)
                            {
                                userexists = true;
                            }
                        }
                        var user = new tbl_User
                        {
                            Id = Guid.NewGuid(),
                            FirstName = item.FirstName == null ? null : item.FirstName.Trim(),
                            MiddleName = item.MiddleName == null ? null : item.MiddleName.Trim(),
                            LastName = item.LastName == null ? null : item.LastName.Trim(),
                            Email = item.Email == null ? null : item.Email.Trim(),
                            Mobile = item.Mobile == null ? null : item.Mobile.Trim(),
                            StandardId = item.StandardId,

                            LoginId = item.LoginId == null ? null : item.LoginId.Trim(),
                            Pwd = item.Pwd == null ? null : fstltr + "_" + lstltr + randomPassword,


                            FatherName = item.FatherName == null ? null : item.FatherName.Trim(),
                            MotherName = item.MotherName == null ? null : item.MotherName.Trim(),

                            Subjects = item.Subjects == null ? null : item.Subjects.Trim(),
                            SchoolId = item.SchoolId == null ? null : item.SchoolId,

                            CreatedOn = DateTime.Now,
                            CreatedBy = item.CreatedBy,
                            IsActive = true
                        };


                        if (userexists == false)
                        {
                            _unitOfWork.UserRepository.Insert(user);

                            //Add Role To User......................

                            if (item.RoleId != Guid.Empty && item.RoleId != null)
                            {
                                var map = new tbl_Map_Role_User
                                {
                                    UserId = user.Id,
                                    RoleId = item.RoleId
                                };
                                _unitOfWork.MapRoleUser.Insert(map);
                            }
                            else
                            {

                                RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                                RMsg.Message = "Please Provide Role Type";
                            }


                        }
                        //Map User Section
                        if (item.SchoolId != Guid.Empty)
                        {
                            //foreach (var useritem in item.SectionDetails)
                            //{

                            var map = new tbl_Map_User_Section
                            {
                                Id = Guid.NewGuid(),
                                SectionId = item.SectionId,
                                UserId = user.Id,
                                StandardId = user.StandardId,
                                // SubjectId=user.Subjects

                                IsActive = true,
                                CreatedOn = DateTime.Now
                                //CreatedBy = item.ParentId
                            };
                            _unitOfWork.MapUserSectionRepository.Insert(map);
                            //}
                        }

                        if (item.IsMath != false)
                        {

                            var subject = _unitOfWork.SubjectRepository.Get(u => u.Name == "Mathematics");

                            newsubj = new SubjectEntity();
                            newsubj.Id = subject.Id;
                            newsubj.Code = subject.Code;
                            usersubj.Add(newsubj);

                        }
                        if (item.IsChemistry != false)
                        {
                            //foreach (var useritem in item.SectionDetails)
                            //{
                            var subject = _unitOfWork.SubjectRepository.Get(u => u.Name == "Chemistry");

                            newsubj = new SubjectEntity();
                            newsubj.Id = subject.Id;
                            newsubj.Code = subject.Code;
                            usersubj.Add(newsubj);

                        }
                        if (item.IsPhysics != false)
                        {
                            newsubj = new SubjectEntity();
                            var subject = _unitOfWork.SubjectRepository.Get(u => u.Name == "Physics");

                            newsubj.Id = subject.Id;
                            newsubj.Code = subject.Code;
                            usersubj.Add(newsubj);

                        }
                        foreach (var useritem in usersubj)
                        {
                            var map = new tbl_UserDetails
                            {
                                Id = Guid.NewGuid(),
                                //  SubjectId = item,
                                UserId = user.Id,
                                // StandardId = user.StandardId,
                                SubjectId = useritem.Id,

                                IsActive = true,
                                CreatedOn = DateTime.Now
                                //CreatedBy = item.ParentId
                            };
                            _unitOfWork.UserDetailsRepository.Insert(map);
                            //}
                        }


                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Message = "Success";
                            RMsg.Data.Add(user.Id);
                        }
                        else
                        {
                            RMsg.Message = "No_Record_Found";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;
                            RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                        }
                    }

                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Please Provide Role Type";
                }
                return RMsg;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public FunctionResponse GetUserByUserId(Guid UserId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<UserEntity> list = _unitOfWork.UserRepository.GetUser().Where(x => x.Id == UserId).ToList<UserEntity>();
                //.Where(x => x.Email == EmailId.Trim()).ToList<tbl_User>();

                if (list.Count > 0)
                {
                    // var dataModel = Mapper.Map<<tbl_User>, <UserEntity>>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(list);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public FunctionResponse DeleteUser(Guid userId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (userId != null)
                {

                    var user = _unitOfWork.UserRepository.GetByID(userId);
                    if (user != null)
                    {
                        _unitOfWork.UserRepository.Delete(user);
                        //_unitOfWork.UserRepository.DeActivateUser(userId, user);

                    }
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }

                }

                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Missing Something";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public FunctionResponse GetAllUsers()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var users = _unitOfWork.UserRepository.GetUser();
                if (users.Any() && users.Count > 0)
                {
                    //  var usersModel = Mapper.Map<List<tbl_User>, List<UserEntity>>(users);
                    var usersModel = users;
                    if (usersModel != null)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                        RMsg.Data.Add(usersModel);
                    }
                    else
                    {
                        RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                        RMsg.Message = "No Record Found";
                    }

                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public FunctionResponse GetUser(string userName, string password)
        {
            FunctionResponse RMsg = new FunctionResponse();

            try
            {
                var user = _unitOfWork.UserRepository.Get(u => (u.Email == userName || u.LoginId==userName) && u.Pwd == password);

                if (user != null)
                {
                    var userModel = Mapper.Map<tbl_User, UserEntity>(user);

                    if (userModel.IsActive == true && userModel.EmailVerified == true)
                    {
                        //remove pwd from model
                        userModel.Pwd = "***";
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "success";
                        RMsg.Data.Add(userModel);
                    }
                    else if (userModel.IsActive == true && userModel.EmailVerified != true)
                    {
                        RMsg.Status = FunctionResponse.StatusType.ERROR;
                        RMsg.Message = "UnVerified";
                        //RMsg.Data.Add(userModel);
                    }
                    else if (userModel.IsActive != true)
                    {
                        RMsg.Status = FunctionResponse.StatusType.ERROR;
                        RMsg.Message = "InActiveUser";
                        //RMsg.Data.Add(userModel);
                    }
                    //RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    //RMsg.Data.Add(userModel);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return RMsg;
        }
        public List<UserEntity> GetUserById(Guid userId)
        {
            List<UserEntity> user = _unitOfWork.UserRepository.GetUser().Where
                (x => x.Id == userId).ToList<UserEntity>();
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public FunctionResponse UpdateUser(Guid userId, UserEntity userEntity, bool? deactivation = false)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //if (deactivation == true)
                //{
                if (userEntity != null && userId != Guid.Empty)
                {
                    var user = _unitOfWork.UserRepository.GetByID(userId);

                    if (user != null)
                    {
                        if (user.IsActive == deactivation)
                        {
                            user.FirstName = userEntity.FirstName == null ? user.FirstName : userEntity.FirstName.Trim();
                            user.MiddleName = userEntity.MiddleName == null ? user.MiddleName : userEntity.MiddleName.Trim();
                            user.LastName = userEntity.LastName == null ? user.LastName : userEntity.LastName.Trim();
                            user.Email = userEntity.Email == null ? user.Email : userEntity.Email.Trim();
                            user.Address1 = userEntity.Address1 == null ? user.Address1 : userEntity.Address1.Trim();
                            user.Address2 = userEntity.Address2 == null ? user.Address2 : userEntity.Address2.Trim();
                            user.Mobile = userEntity.Mobile == null ? user.Mobile : userEntity.Mobile.Trim();
                            user.LoginId = userEntity.LoginId == null ? user.LoginId : userEntity.LoginId.Trim();
                            user.Pwd = userEntity.Pwd == null ? user.Pwd : userEntity.Pwd.Trim();
                            user.StandardId = userEntity.StandardId == null ? user.StandardId : userEntity.StandardId;
                            user.Qualification = userEntity.Qualification; /*== null ? user.Qualification : userEntity.Qualification;*/
                            user.CountryId = userEntity.CountryId == null ? user.CountryId : userEntity.CountryId;
                            user.StateId = userEntity.StateId == null ? user.StateId : userEntity.StateId;
                            user.CityId = userEntity.CityId == null ? user.CityId : userEntity.CityId;
                            user.LocationId = userEntity.LocationId == null ? user.LocationId : userEntity.LocationId;
                            user.FatherName = userEntity.FatherName == null ? user.FatherName : userEntity.FatherName.Trim();
                            user.MotherName = userEntity.MotherName == null ? user.MotherName : userEntity.MotherName.Trim();
                            user.AlternatePhone = userEntity.AlternatePhone == null ? user.AlternatePhone : userEntity.AlternatePhone.Trim();
                            user.AlternateEmail = userEntity.AlternateEmail == null ? user.AlternateEmail : userEntity.AlternateEmail.Trim();

                            user.Occupation = userEntity.Occupation == null ? user.Occupation : userEntity.Occupation;
                            user.State = userEntity.State == null ? user.State : userEntity.State.Trim();
                            user.City = userEntity.City == null ? user.City : userEntity.City.Trim();
                            user.Location = userEntity.Location == null ? user.Location : userEntity.Location.Trim();
                            user.PinCode = userEntity.PinCode == null ? user.PinCode : userEntity.PinCode.Trim();
                            user.Subjects = userEntity.Subjects == null ? user.Subjects : userEntity.Subjects;
                            user.BoardId = userEntity.BoardId;
                            user.ImageB64 = userEntity.ImageB64;

                            //CreatedOn = DateTime.Now,
                            user.UpdatedOn = DateTime.Now;
                            //CreatedBy = userEntity.CreatedBy,
                            user.UpdatedBy = userEntity.CreatedBy;
                            user.IsActive = userEntity.IsActive;

                            _unitOfWork.UserRepository.Update(user);

                            //if (_unitOfWork.UserRepository.DeActivateUser(userId, user) == true)
                            if (_unitOfWork.Save() > 0)
                            {
                                RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                RMsg.Data.Add(user.Id);
                            }
                            else
                            {
                                RMsg.Message = "No_Record_Found";
                                RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                            }
                        }
                        else
                        {
                            //Activate DeActivate User 

                            user.IsActive = deactivation;

                            if (_unitOfWork.UserRepository.DeActivateUser(userId, user) == true)
                            {
                                RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                RMsg.Data.Add(user.Id);
                            }
                            else
                            {
                                RMsg.Message = "No_Record_Found";
                                RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                            }
                        }
                    }
                    else
                    {
                        RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                        RMsg.Message = "Missing Something";
                    }
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Missing Something";
                }
                //}
                //else
                //{
                //    var user = _unitOfWork.UserRepository.GetByID(userId);
                //    user.IsActive = false;
                //    if (_unitOfWork.UserRepository.DeActivateUser(userId, user) == true)
                //    {
                //        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                //        RMsg.Data.Add(user.Id);
                //    }
                //    else
                //    {
                //        RMsg.Message = "No_Record_Found";
                //        RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                //    }
                //}
                return RMsg;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public FunctionResponse GetUserByEmailId(String EmailId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<UserEntity> list = _unitOfWork.UserRepository.GetUser().Where(x => x.Email == EmailId).ToList<UserEntity>();
                //.Where(x => x.Email == EmailId.Trim()).ToList<tbl_User>();

                if (list.Count > 0)
                {
                    // var dataModel = Mapper.Map<<tbl_User>, <UserEntity>>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(list);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }
        public FunctionResponse EmailVerification(Guid userId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (userId != Guid.Empty)
                {
                    var user = _unitOfWork.UserRepository.GetByID(userId);
                    if (user != null)
                    {
                        if (user.EmailVerified != true)
                        {
                            user.IsActive = true;
                            user.EmailVerified = true;
                            _unitOfWork.UserRepository.Update(user);


                            if (_unitOfWork.Save() > 0)
                            {
                                RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                RMsg.Data.Add(user.Id);
                            }
                        }

                        else
                        {
                            RMsg.Message = "Already Verified";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;
                        }

                    }
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "No Records Found ";
                }
                return RMsg;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public FunctionResponse SendVerificationMail(UserEntity userEntity, FunctionResponse Resp, IEmailInterface _IEmailInterface)
        {
            //GetMailer
            string mailername = ConfigurationManager.AppSettings["EmailVerificationMailer"].ToString();
            var Mailerdata = _unitOfWork.MailerRepository.Get(x => x.Name == mailername);

            StringBuilder sb = new StringBuilder();
            //sb.Append(char.ToUpper(userEntity.FirstName[0]) + userEntity.FirstName.Substring(1) + " " + char.ToUpper(userEntity.LastName[0]) + userEntity.LastName.Substring(1) + ",");
            //sb.AppendLine("<p>You Successfuly Registered On Practice2Perfection</p>");
            //sb.AppendLine("<p>Please <strong>");

            //ConfigurationManager.AppSettings["EmailVerifiyUrl"].ToString();
            //sb.Append("<html><head>");
            //sb.Append("<link rel='stylesheet' type='text/css' href='https://cdnjs.cloudflare.com/ajax/libs/quill/1.3.6/quill.core.css'>");
            //sb.Append("</head><body class='ql-editor'>");

            //add mailer here..................
            sb.AppendLine(Mailerdata.Mailer == null ? "" : Mailerdata.Mailer);
            sb.AppendLine("<p>Please  <strong><a href=");
            sb.AppendLine(ConfigurationManager.AppSettings["EmailVerifiyUrl"].ToString() + "/" + Resp.Data[0]);
            sb.AppendLine(">Validate Account !</a></strong></P>");
            //sb.Append("</body></html>");
            string Body = sb.ToString();
            //  string Body = "<a>http://localhost:51778/Api/User/Verify?Id=" + Resp.Data[0]+"</a>";
            Resp = _IEmailInterface.SendEmail(userEntity.Email, "", "Email Verification", Body);
            return Resp;
        }
        public string getpassword(Guid id)
        {
            string p = null;

            if (id != Guid.Empty)
            {
                var user = _unitOfWork.UserRepository.GetByID(id);
                if (user != null)
                {
                    p = user.Pwd;

                }
            }

            return p;
        }
        public FunctionResponse changepassword(Guid id, string oldpwd, string newpwd)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (id != Guid.Empty)
                {
                    var user = _unitOfWork.UserRepository.GetByID(id);
                    if (user != null)
                    {

                        if (user.Pwd.Trim() == oldpwd.Trim())
                        {
                            // old = user.Pwd;
                            user.Pwd = newpwd;
                            _unitOfWork.UserRepository.Update(user);

                            if (_unitOfWork.Save() > 0)
                            {
                                RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                RMsg.Data.Add(user.Id);
                            }

                        }
                        else
                        {
                            RMsg.Message = "Incorrect Password";
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        }

                    }
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Missing Something";
                }
                return RMsg;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /*
         Update pwd of user through by has code and check its expiry
             */
        public FunctionResponse Updatepassword(string hascode, string pwd)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.UrlExpireRepository.Get().Where(x => x.Hascode == hascode).FirstOrDefault();
                if (data != null && checkvalidUrl(hascode, data))
                {
                    var user = _unitOfWork.UserRepository.GetByID(data.UserId);
                    if (user != null)
                    {
                        user.Pwd = pwd;
                        _unitOfWork.UserRepository.Update(user);

                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Message = "Success";
                            RMsg.Data.Add(user.Id);
                        }
                    }
                }

                else
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Invalid URL";
                }
                return RMsg;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool checkvalidUrl(string hascode, tbl_UrlExpire data)
        {
            bool status = false;
            bool varifyHas = false;
            bool TimeExpire = false;

            //   SHA256 algorithm = SHA256.Create();

            //check hascode
            if (!string.IsNullOrEmpty(hascode))
            {

                //check hascode
                varifyHas = Utility.VerifyHash(Utility.GetAlgo(), data.UserId.ToString(), data.Hascode);
                int a = DateTime.Compare(DateTime.Now, data.Expire ?? DateTime.Now);
                if (DateTime.Compare(DateTime.Now, data.Expire ?? DateTime.Now) < 0)
                {
                    TimeExpire = true;

                }
            }

            if (varifyHas && TimeExpire) { status = true; }

            return status;


        }
        public FunctionResponse RemoveUser(UserEntity userEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (userEntity != null)
                {
                    var user = _unitOfWork.UserRepository.GetByID(userEntity.Id);
                    if (user != null)
                    {
                        //To Remove user form data base we need to remove all refrences

                        //Remove From tbl_Map_Role_User
                        // var role = _unitOfWork.MapRoleUserRepository.GetSingle(x => x.UserId == user.Id);

                        // var Rol_map = new tbl_Map_Role_User { UserId = role.UserId, RoleId = role.RoleId };
                        //_unitOfWork.MapRoleUserRepository.Delete(Rol_map);

                        //check exists
                        if (_unitOfWork.MapRoleUserRepository.Get(x => x.UserId == user.Id) != null)
                        {
                            _unitOfWork.MapRoleUserRepository.Delete(x => x.UserId == user.Id);
                        }

                        //Remove from Parent Mapping
                        // var parent = _unitOfWork.MapParentChildRepository.GetRefrenceID(user.Id);
                        //  var parent = _unitOfWork.MapParentChildRepository.GetSingle(x => x.ChildId == user.Id);

                        // var Parent_map = new tbl_MapParentChild { ChildId = parent.ChildId, ParentId = parent.ParentId };
                        //_unitOfWork.MapParentChildRepository.Delete(Parent_map);
                        //check exists
                        if (_unitOfWork.MapParentChildRepository.Get(x => x.ChildId == user.Id) != null)
                        {
                            _unitOfWork.MapParentChildRepository.Delete(x => x.ChildId == user.Id);
                        }

                        //Remove from Teacher Mapping
                        //var teacher = _unitOfWork.MapTeacherChildRepository.GetSingle(x => x.ChildId == user.Id);

                        // var teacher_map = new tbl_MapTeacherChild { ChildId = user.Id, TeacherId = teacher.TeacherId };
                        //  _unitOfWork.MapTeacherChildRepository.Delete(teacher_map);
                        //check exists
                        if (_unitOfWork.MapTeacherChildRepository.Get(x => x.ChildId == user.Id) != null)
                        {
                            _unitOfWork.MapTeacherChildRepository.Delete(x => x.ChildId == user.Id);
                        }

                        //Remove from Quiz and QuizDetails Mapping                        
                        List<QuizEntity> quiz = _unitOfWork.QuizRepository.GetQuiz().Where(x => x.UserId == user.Id).ToList<QuizEntity>();
                        foreach (var q in quiz)
                        {

                            foreach (var qd in q.QuizDetails)
                            {
                                //check exists
                                if (_unitOfWork.QuizDetailsRepository.Get(x => x.QuizId == qd.QuizId) != null)
                                {
                                    _unitOfWork.QuizDetailsRepository.Delete(x => x.QuizId == qd.QuizId);
                                }
                            }
                            if (_unitOfWork.QuizRepository.Get(x => x.Id == q.Id) != null)
                                _unitOfWork.QuizRepository.Delete(x => x.Id == q.Id);
                        }

                        _unitOfWork.UserRepository.Delete(user);


                    }
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }

                }

                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Missing Something";
                }

                return RMsg;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string SetPasswordExpireUrl(Guid UserId)
        {
            try
            {
                // SHA256 algorithm = SHA256.Create();
                string hash = Utility.GetHash(UserId.ToString(), Utility.GetAlgo());

                DateTime currentTime = DateTime.Now;
                string time = ConfigurationManager.AppSettings["ForgotPwdUrlExpireTime"].ToString();
                DateTime expireDate = currentTime.AddMinutes(Convert.ToDouble(time));


                //save detail into DB with expiry date
                var data = new tbl_UrlExpire
                {
                    UserId = UserId,
                    Hascode = hash,
                    Expire = expireDate
                };

                //check id exists
                var exist = _unitOfWork.UrlExpireRepository.Get(x => x.UserId == UserId);
                if (exist == null)
                {
                    _unitOfWork.UrlExpireRepository.Insert(data);
                }
                else
                {
                    exist.Hascode = hash;
                    exist.Expire = expireDate;
                    _unitOfWork.UrlExpireRepository.Update(exist);

                }
                _unitOfWork.Save();
                return hash;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
