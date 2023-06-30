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
    public class SolutionsRepository : ISolutionsInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public SolutionsRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FunctionResponse Create(SolutionsEntity entity)
        {
            FunctionResponse RMsg = new FunctionResponse();
            try
            {

                if (entity != null)
                {
                    var tbl = new tbl_Solutions
                    {
                        Id = Guid.NewGuid(),
                        PatternId = entity.PatternId,
                        Solution = entity.Solution == null ? null : entity.Solution.Trim(),
                        IsImplemented = entity.IsImplemented,
                        ImplementedBy = entity.ImplementedBy,
                        IsApproved = entity.IsApproved,
                        ApprovedBy = entity.ApprovedBy,
                        CreatedOn = DateTime.Now,
                        CreatedBy = entity.CreatedBy,
                        IsActive = entity.IsActive,

                    };


                    //  var Board = _unitOfWork.BoardRepository.Get(u => u.Name == brd.Name);

                    //if (Board == null)
                    //{
                    _unitOfWork.SolutionsRepository.Insert(tbl);
                    _unitOfWork.Save();
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(tbl.Id);
                    //}
                    //else
                    //{
                    //    RMsg.Status = FunctionResponse.StatusType.ERROR;
                    //    RMsg.Message = "Duplicate";

                    //}




                    //if (_unitOfWork.Save() > 0)
                    //{                      

                    //        //Set Success
                    //        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    //        RMsg.Data.Add(brd.Id);


                    //}
                    //else
                    //{
                    //    RMsg.Message = "No_Record_Found";
                    //    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    //}

                }

            }

            catch (Exception ex)
            {
                throw;
            }
            return RMsg;
        }

        public FunctionResponse Delete(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (Id != null)
                {

                    var brd = _unitOfWork.SolutionsRepository.GetByID(Id);
                    if (brd != null)
                    {
                        _unitOfWork.SolutionsRepository.Delete(brd);

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

        public FunctionResponse GetAll()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var brds = _unitOfWork.SolutionsRepository.GetAll().ToList();
                // var brds = _unitOfWork.BoardRepository.GetBoard();
                if (brds.Any() && brds.Count > 0)
                {
                    var brdsModel = Mapper.Map<List<tbl_Solutions>, List<SolutionsEntity>>(brds);
                    //var brdsModel = brds;
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

        public FunctionResponse GetById(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.SolutionsRepository.GetByID(Id);
                if (data != null)
                {
                    var Model = Mapper.Map<tbl_Solutions, SolutionsEntity>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(Model);
                    RMsg.Message = "Success";
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FunctionResponse GetByPatternId(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.SolutionsRepository.Get().Where(x => x.PatternId == Id).ToList();
                if (data != null)
                {
                    List<SolutionsEntity> List = Mapper.Map<List<tbl_Solutions>, List<SolutionsEntity>>(data);
                   // dynamic List = Mapper.Map<List<tbl_Solutions>, List<SolutionsEntity>>(data);



                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(List);
                    RMsg.Message = "Success";
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record";
                }

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FunctionResponse Update(Guid Id, SolutionsEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (entity != null)
                {
                    var data = _unitOfWork.SolutionsRepository.GetByID(Id);
                    if (data != null)
                    {
                        data.PatternId = entity.PatternId == Guid.Empty ? data.PatternId : entity.PatternId;
                        if (!string.IsNullOrEmpty(entity.Solution))
                        {
                            data.Solution = entity.Solution.Trim();
                        }

                        data.IsImplemented = entity.IsImplemented == null ? data.IsImplemented : entity.IsImplemented;
                        data.ImplementedBy = entity.ImplementedBy == Guid.Empty ? data.ImplementedBy : entity.ImplementedBy;

                        data.IsApproved = entity.IsApproved == null ? data.IsApproved : entity.IsApproved;
                        data.ApprovedBy = entity.ApprovedBy == Guid.Empty ? data.ApprovedBy : entity.ApprovedBy;


                        data.UpdatedOn = DateTime.Now;
                        data.UpdatedBy = entity.UpdatedBy;
                        data.IsActive = entity.IsActive == null ? data.IsActive : entity.IsActive; ;

                        _unitOfWork.SolutionsRepository.Update(data);

                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Data.Add(data.Id);
                            RMsg.Message = "Success";
                        }
                        else
                        {
                            RMsg.Message = "Error";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;
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


    }
}
