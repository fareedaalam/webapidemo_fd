using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.Repository
{
    class MapParentChildRepository : IMapParentChildInterface
    {
        private readonly UnitOfWork _unitOfWork;
        private IUserInterface _iUserInterface;

        public MapParentChildRepository(UnitOfWork unitOfWork, IUserInterface iUserInterface)
        {
            _unitOfWork = unitOfWork;
            _iUserInterface = iUserInterface;
        }

        private bool CheckMapParentChildExists(tbl_MapParentChild entity)
        {
            bool status = false;
            try
            {
                var child = _unitOfWork.MapParentChildRepository.Get(x => x.ChildId == entity.ChildId);
                if (child != null)
                    status = true;
                return status;
            }

            catch (Exception ex)
            {
                return status;
            }

        }

        public FunctionResponse AssignChildToPerent(MapParentChildEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //Check limit of child
                if (checkChildLimit(entity.ParentId))
                {

                    //Get child Id by Email Id
                    RMsg = _iUserInterface.GetUserByEmailId(entity.Email.Trim());
                    if (RMsg.Status == FunctionResponse.StatusType.SUCCESS)
                    {
                        List<UserEntity> User = (List<UserEntity>)RMsg.Data[0];

                        //Check Parent himself
                        if (User[0].Id == entity.ParentId || User[0].RoleName != "Student")
                        {
                            RMsg.Message = "You can not add yourself!";
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            return RMsg;
                        }

                        var mapping = new tbl_MapParentChild
                        {
                            ParentId = entity.ParentId,
                            ChildId = User[0].Id,
                            CreatedOn = DateTime.Now,
                            IsActive = false,
                        };
                        // _unitOfWork.MapRolePermission.Delete(mapping);
                        if (CheckMapParentChildExists(mapping) != true)
                        {
                            _unitOfWork.MapParentChildRepository.Insert(mapping);
                        }
                        else
                        {
                            RMsg.Message = "Already Exists";
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;

                        }
                        if (_unitOfWork.Save() > 0)
                        {
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
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Chilld Limit Exceed!";
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

        private bool checkChildLimit(Guid ParentId)
        {
            bool status = false;
            List<UserEntity> data = _iUserInterface.GetUserById(ParentId);
            List<MapParentChildEntity> AssignedChild = _unitOfWork.MapParentChildRepository.GetParentChildList(ParentId);
            if (data != null && AssignedChild != null)
            {
                if (AssignedChild.Count()+1 <= data[0].ChildAllowed)
                {
                    status = true;
                }
            }

            return status;

        }
        public FunctionResponse GetAssignedChildList()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapParentChildEntity> data = _unitOfWork.MapParentChildRepository.GetParentChildList();
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

        public FunctionResponse GetAssignedChildListByParentId(Guid ParentId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapParentChildEntity> data = _unitOfWork.MapParentChildRepository.GetParentChildList(ParentId);
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

        public FunctionResponse GetAssignedChildListByChildId(Guid _childId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapParentChildEntity> data = _unitOfWork.MapParentChildRepository.GetParentChildList()
                    .Where(x => x.ChildId == _childId).ToList<MapParentChildEntity>();
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

        public FunctionResponse RemoveChildToPerent(Guid _parentId, Guid _childId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //var data = _unitOfWork.MapParentChildRepository.Get(x => x.ChildId == _childId && x.ParentId==_parentId);
                //if (data != null)
                //{
                var mapping = new tbl_MapParentChild
                {
                    ParentId = _parentId,
                    ChildId = _childId,

                };
                _unitOfWork.MapParentChildRepository.Delete(mapping);
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                }
                // }

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

        public FunctionResponse GetAssignedChildListByParentIdWithQuizData(Guid parentId)
        {
            try
            {

                FunctionResponse RMsg = new FunctionResponse();

                string procName = "Parent_GetChildListWithData";
                var data = _unitOfWork.ExecuteReader<Teacher_GetChildListWithData>(string.Format("{0} '{1}' ", procName, parentId));

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


        public FunctionResponse Update(MapParentChildEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //Get child Id by Email Id
                var data = _unitOfWork.MapParentChildRepository.Get(
                    x => x.ParentId == entity.ParentId && x.ChildId == entity.ChildId);

                if (data != null)
                {
                    data.ParentId = entity.ParentId;
                    data.ChildId = entity.ChildId;
                    data.UpdatedOn = DateTime.Now;
                    data.UpdatedBy = entity.ChildId;
                    data.IsActive = true;
                }
                _unitOfWork.MapParentChildRepository.Update(data);
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
