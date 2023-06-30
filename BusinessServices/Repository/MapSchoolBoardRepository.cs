using System;
using System.Collections.Generic;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System.Linq;
using AutoMapper;

namespace BusinessServices.Repository
{
    public class MapSchoolBoardRepository : IMapSchoolBoardInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public MapSchoolBoardRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private bool CheckSchoolBoardExists(tbl_Map_School_Board entity)
        {
            bool status = false;
            try
            {

                tbl_Map_School_Board mapschoolboard = new tbl_Map_School_Board();
                mapschoolboard = _unitOfWork.MapSchoolBoardRepository.GetFirst(x => x.BoardId == entity.BoardId && x.SchoolId == entity.SchoolId);
                if (mapschoolboard != null)
                    status = true;
                return status;
            }

            catch (Exception)
            {
                return status;
            }

        }
        public FunctionResponse AssignBoardToSchool(dynamic data)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var mapping = new tbl_Map_School_Board
                {
                    BoardId = data.BoardId,
                    SchoolId = data.SchoolId,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    IsActive = data.IsActive
                };

                if (CheckSchoolBoardExists(mapping) != true)
                {
                    _unitOfWork.MapSchoolBoardRepository.Insert(mapping);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.DUPLICATE;
                    RMsg.Message = "Duplicate";
                    return RMsg;
                }
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    // RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "Fail";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
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

        public FunctionResponse DeleteBoardSchool(Guid BordId, Guid SchoolId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var mapping = _unitOfWork.MapSchoolBoardRepository.GetFirst(x => x.BoardId == BordId && x.SchoolId == SchoolId);
                if (mapping!=null)
                {
                    _unitOfWork.MapSchoolBoardRepository.Delete(mapping);
                }
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    // RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "Fail";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
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

        public FunctionResponse GetBoardToSchool()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.MapSchoolBoardRepository.GetSchoolBoardList();

               // var data = Mapper.Map<List<tbl_Map_School_Board>, List<MapSchoolBoardEntity>>(Model);

                if (data != null && data.Count() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                }
                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }

        }

        public FunctionResponse GetBoardToSchoolById(Guid BoardId, Guid SchoolId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.MapSchoolBoardRepository.GetSchoolBoardList(BoardId, SchoolId);

               // var data = Mapper.Map<tbl_Map_School_Board, MapSchoolBoardEntity>(Model);

                if (data != null )
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                }
                return RMsg;
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }

        }

        public FunctionResponse UpdateBoardSchool(MapSchoolBoardEntity entity)
        {
            try
            {
                FunctionResponse RMgs = new FunctionResponse();
                var boardschool = _unitOfWork.MapSchoolBoardRepository.GetFirst(x => x.BoardId == entity.BoardId && x.SchoolId == entity.SchoolId);
                if (CheckSchoolBoardExists(boardschool) == true)
                {
                    boardschool.IsActive = entity.IsActive;
                    boardschool.UpdatedOn = DateTime.Now;
                    boardschool.UpdatedBy = entity.UpdatedBy;

                    _unitOfWork.MapSchoolBoardRepository.Update(boardschool);
                }
                if (_unitOfWork.Save() > 0)
                {
                    RMgs.Status = FunctionResponse.StatusType.SUCCESS;
                    RMgs.Message = "Success";
                }
                else
                {
                    RMgs.Status = FunctionResponse.StatusType.ERROR;
                    RMgs.Message = "Fail";
                }
                return RMgs;
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
        }
    }
}
