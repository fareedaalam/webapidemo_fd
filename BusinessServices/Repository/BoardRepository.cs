using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Repository
{
    public class BoardRepository : IBoardInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public BoardRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<BoardEntity> GetById(Guid brdId)
        {
            //var brd = _unitOfWork.BoardRepository.GetByID(brdId);
            var brd = _unitOfWork.BoardRepository.GetBoardById(brdId);
            if (brd != null)
            {
                IEnumerable<BoardEntity> List = brd;
                // var brdModel = Mapper.Map<tbl_Board, BoardEntity>(brd);
                return List;
            }
            return null;
        }
       
        public FunctionResponse GetAll()
        {

            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                //var brds = _unitOfWork.BoardRepository.GetAll().ToList();
                var brds = _unitOfWork.BoardRepository.GetBoard().OrderBy(x => x.Name).ToList();
                if (brds.Any() && brds.Count > 0)
                {
                    //var brdsModel = Mapper.Map<List<tbl_Board>, List<BoardEntity>>(brds);
                    var brdsModel = brds;
                    if (brdsModel != null)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                        RMsg.Data.Add(brdsModel);
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

        public FunctionResponse Create(BoardEntity boardEntity)
        {
            FunctionResponse RMsg = new FunctionResponse();
            try
            {
                var brd = new tbl_Board
                {
                    Id = Guid.NewGuid(),
                    Name = boardEntity.Name == null ? null : boardEntity.Name.Trim(),
                    Code = boardEntity.Code,
                    CountryId = boardEntity.CountryId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = boardEntity.CreatedBy,
                    IsActive = boardEntity.IsActive == null ? true : boardEntity.IsActive
                };

                var brd_user = _unitOfWork.BoardRepository.Get(s => s.Name == brd.Name || s.Code == brd.Code);

                if (brd_user == null)
                {
                    _unitOfWork.BoardRepository.Insert(brd);
                    _unitOfWork.Save();
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(brd.Id);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                    RMsg.Message = "Duplicate";

                }

            }

            catch (Exception)
            {
                throw;
            }
            return RMsg;
        }

        public FunctionResponse Update(Guid brdId, BoardEntity boardEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (boardEntity != null && brdId != Guid.Empty)
                {
                    var brd = _unitOfWork.BoardRepository.GetByID(brdId);

                    if (brd != null)
                    {
                        var brd_Name = _unitOfWork.BoardRepository.Get(s => s.Name == boardEntity.Name );

                        if (brd_Name == null)
                        {
                            brd.Name = boardEntity.Name;
                            brd.UpdatedOn = DateTime.Now;
                            brd.UpdatedBy = boardEntity.UpdatedBy;
                            brd.IsActive = boardEntity.IsActive;

                            _unitOfWork.BoardRepository.Update(brd);

                            if (_unitOfWork.Save() > 0)
                            {
                                RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                RMsg.Data.Add(brd.Id);
                                RMsg.Message = "Success";
                            }
                            else
                            {
                                RMsg.Message = "Error";
                                RMsg.Status = FunctionResponse.StatusType.ERROR;
                            }
                        }
                        else
                        {
                            if (brd.IsActive != boardEntity.IsActive)
                            {
                                if (boardEntity.Name != null)
                                    brd.Name = boardEntity.Name == null ? null : boardEntity.Name.Trim();

                                if (boardEntity.CountryId != null)
                                    brd.CountryId = boardEntity.CountryId == null ? null : boardEntity.CountryId;

                                if (boardEntity.Code != null)
                                    brd.Code = boardEntity.Code;

                                if (boardEntity.IsActive != null)
                                    brd.IsActive = boardEntity.IsActive;

                                brd.UpdatedBy = boardEntity.UpdatedBy;

                                brd.UpdatedOn = DateTime.Now;

                                _unitOfWork.BoardRepository.Update(brd);
                                if (_unitOfWork.Save() > 0)
                                {
                                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                    RMsg.Data.Add(brd.Id);
                                    RMsg.Message = "Success";

                                }
                                else
                                {

                                    RMsg.Message = "Error";
                                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                                }
                            }
                            else
                            {
                                RMsg.Message = "Duplicate";
                                RMsg.Status = FunctionResponse.StatusType.ERROR;
                            }
                        }
                        

                    }
                }
                return RMsg;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public bool Delete(Guid id)
        {
            try
            {
                              var success = false;
                if (id != null)
                {
                    var brd = _unitOfWork.BoardRepository.GetByID(id);
                    if (brd != null)
                    {

                        brd.IsActive = brd.IsActive == true ? false : true;
                        //if (brd.IsActive == false)
                        //{
                        //    brd.IsActive = true;
                        //}
                        //else
                        //{
                        //    brd.IsActive = false;
                        //}
                        _unitOfWork.BoardRepository.Update(brd);
                        if (_unitOfWork.Save() > 0)
                        {
                            success = true;
                        }
                    }
                }
                return success;

            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
