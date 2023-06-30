using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessServices.Repository
{
    class MapTeacherChildRepository : IMapTeacherChildInterface
    {
        private readonly UnitOfWork _unitOfWork;
        private IUserInterface _iUserInterface;
        private IEmailInterface _iEmailInterface;

        public MapTeacherChildRepository(UnitOfWork unitOfWork, IUserInterface iUserInterface, IEmailInterface iEmailInterface)
        {
            _unitOfWork = unitOfWork;
            _iUserInterface = iUserInterface;
            _iEmailInterface = iEmailInterface;
        }
        private bool CheckChildExists(tbl_MapTeacherChild entity)
        {
            bool status = false;
            try
            {
                var child = _unitOfWork.MapTeacherChildRepository.Get(x => x.ChildId == entity.ChildId && x.TeacherId == entity.TeacherId);
                if (child != null)
                    status = true;
                return status;
            }

            catch (Exception ex)
            {
                return status;
            }

        }
        public FunctionResponse AssignChildToTeacher(MapTeacherChildEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<UserEntity> User = new List<UserEntity>();
                //get Teacher 
                List<UserEntity> teacher = _iUserInterface.GetUserById(entity.TeacherId);
                //Get child Id by Email Id
                RMsg = _iUserInterface.GetUserByEmailId(entity.Email.ToString());

                if (RMsg.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    User = (List<UserEntity>)RMsg.Data[0];
                    //Check Parent himself
                    if (User[0].Id == entity.TeacherId || User[0].RoleName != "Student")
                    {
                        RMsg.Message = "No Student Registered With this Email";
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        return RMsg;
                    }

                    var mapping = new tbl_MapTeacherChild
                    {
                        TeacherId = entity.TeacherId,
                        ChildId = User[0].Id,
                        CreatedBy=entity.TeacherId,
                        CreatedOn = DateTime.Now,
                        IsActive = false,
                    };
                    // _unitOfWork.MapRolePermission.Delete(mapping);
                    if (CheckChildExists(mapping) != true)
                    {
                        _unitOfWork.MapTeacherChildRepository.Insert(mapping);
                    }

                    else
                    {
                        RMsg.Message = "Already Exists";
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;

                    }
                    if (_unitOfWork.Save() > 0)
                    {
                        string teacherName = (char.ToUpper(teacher[0].FirstName[0]) + teacher[0].FirstName.Substring(1) + " " + char.ToUpper(teacher[0].LastName[0]) + teacher[0].LastName.Substring(1) + ",");
                        string To = User[0].Email.ToString().Trim();
                        string Cc = "";
                        string Subject = teacherName + "Teacher Request";

                        StringBuilder sb = new StringBuilder("Hi ", 500);

                        sb.Append(char.ToUpper(User[0].FirstName[0]) + User[0].FirstName.Substring(1) + " " + char.ToUpper(User[0].LastName[0]) + User[0].LastName.Substring(1) + ",");

                        sb.AppendLine("<p>" +teacherName+" has requested to add you as a student. Please click ");
                        sb.Append("<a href='http://www.practice2perfection.com/#/login'> here </a>" + "to respond on it. </p>");

                        string Body = sb.ToString();

                        _iEmailInterface.SendEmail(To, Cc, Subject, Body);



                        //////////////////////////////////////////////////////////////////////////
                       // _iEmailInterface.SendEmail(User[0].Email, "", teacherName + " Send you Request", teacherName);
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                        // RMsg.Data.Add(data);
                    }
                }
                else if (RMsg.Status == FunctionResponse.StatusType.NO_RECORD)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "No Record Found";


                }
                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
            finally
            {
            }
        }
        public FunctionResponse GetAssignedChildList()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapTeacherChildEntity> data = _unitOfWork.MapTeacherChildRepository.GetTeacherChildList();
                if (data != null && data.Count > 0)
                {
                    // var mapdata = Mapper.Map<List<tbl_ParamDetail>, List<ParamDetailEntity>>(data);
                    RMsg.Message = "Success";
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public FunctionResponse GetAssignedChildListByTeacherId(Guid TeacherId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapTeacherChildEntity> data = _unitOfWork.MapTeacherChildRepository.GetTeacherChildList(TeacherId);

                //string procName = "Teacher_GetChildListWithData";
                //var data = _unitOfWork.ExecuteReader<MapTeacherChildEntity>(string.Format("{0} '{1}' ", procName, TeacherId));
                //return data;

                if (data != null)
                {
                    // var mapdata = Mapper.Map<List<tbl_ParamDetail>, List<ParamDetailEntity>>(data);
                    RMsg.Message = "Success";
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public FunctionResponse GetAssignedChildListByChildId(Guid _ChildId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapTeacherChildEntity> data = _unitOfWork.MapTeacherChildRepository.GetTeacherChildList()
                    .Where(x => x.ChildId == _ChildId).ToList<MapTeacherChildEntity>();

                if (data != null && data.Count > 0)
                {
                    // var mapdata = Mapper.Map<List<tbl_ParamDetail>, List<ParamDetailEntity>>(data);
                    RMsg.Message = "Success";
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public FunctionResponse RemoveChildToTeacher(Guid _TeacherId, Guid _childId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var mapping = new tbl_MapTeacherChild
                {
                    TeacherId = _TeacherId,
                    ChildId = _childId,

                };
                _unitOfWork.MapTeacherChildRepository.Delete(mapping);
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Something Wrong";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FunctionResponse GetAssignedChildListByTeacherIdWithQuizData(Guid teacherId)
        {
            try
            {

                FunctionResponse RMsg = new FunctionResponse();

                string procName = "Teacher_GetChildListWithData";
                var data = _unitOfWork.ExecuteReader<Teacher_GetChildListWithData>(string.Format("{0} '{1}' ", procName, teacherId));

                if (data != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
        }
        public FunctionResponse Update(MapTeacherChildEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //Get child Id by Email Id
                var data = _unitOfWork.MapTeacherChildRepository.Get(
                    x => x.TeacherId == entity.TeacherId && x.ChildId == entity.ChildId);

                if (data != null)
                {
                    data.TeacherId = entity.TeacherId;
                    data.ChildId = entity.ChildId;
                    data.UpdatedOn = DateTime.Now;
                    data.UpdatedBy = entity.ChildId;
                    data.IsActive = true;
                }
                _unitOfWork.MapTeacherChildRepository.Update(data);
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    //   RMsg.Data.Add(level.Id);
                    RMsg.Message = "Success";

                }
                else
                {
                    RMsg.Message = "Error";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                }

                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }

        }
    }

}
