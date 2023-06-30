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
    public class ConceptMappingRepository : IConceptMappingInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public ConceptMappingRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FunctionResponse Create(ConceptMappingEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                //Check Duplicate
                var Duplicate = _unitOfWork.ConceptMappingRepository.Get(x => x.TopicId == entity.TopicId && x.CategoryId == entity.CategoryId);

                if (Duplicate == null)
                {
                    //insert First Data into Curriculum Master
                    var concept = new tbl_ConceptMapping
                    {
                        Id = Guid.NewGuid(),
                        Name = entity.Name,
                        CategoryId = entity.CategoryId,
                        TopicId = entity.TopicId,
                        Definition = entity.Definition,
                        Example = entity.Example,
                        PointsToRemember = entity.PointsToRemember,
                        CreatedOn = DateTime.Now,
                        CreatedBy = entity.CreatedBy,
                        IsActive = entity.IsActive == null ? true : entity.IsActive,
                    };
                    _unitOfWork.ConceptMappingRepository.Insert(concept);

                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                }
                else
                {
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Duplicate";

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Resp;
        }

        public FunctionResponse Delete(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var con = _unitOfWork.ConceptMappingRepository.GetByID(Id);
                if (con != null)
                {
                    _unitOfWork.ConceptMappingRepository.Delete(Id);
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }
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

        public FunctionResponse GetAll()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var conc = _unitOfWork.ConceptMappingRepository.GetConcepts();

                if (conc.Any() && conc != null)
                {
                    //var conceptModel = Mapper.Map<List<tbl_ConceptMapping>, List<ConceptMappingEntity>>(conc);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(conc);
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

        public FunctionResponse GetConcepts(ConceptMappingEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var conc = _unitOfWork.ConceptMappingRepository.GetConcepts()
                    .Where(x =>
                   x.CategoryId == (entity.CategoryId == Guid.Empty ? x.CategoryId : entity.CategoryId)
                   && x.TopicId == (entity.TopicId == Guid.Empty ? x.TopicId : entity.TopicId)
                   && x.Id == (entity.Id == Guid.Empty ? x.Id : entity.Id)
                   && x.IsActive == true
                    );

                if (conc.Any() && conc.Count() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(conc);
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

        public FunctionResponse GetConceptsById(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var concept = _unitOfWork.ConceptMappingRepository.GetConceptById(Id);

                if (concept != null)
                {
                    //var conceptModel = Mapper.Map<tbl_ConceptMapping, ConceptMappingEntity>(concept);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(concept);
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

        public FunctionResponse Update(Guid Id, ConceptMappingEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (entity != null && Id != Guid.Empty)
                {
                    var concept = _unitOfWork.ConceptMappingRepository.GetByID(Id);

                    if (concept != null)
                    {
                        concept.Name = entity.Name == String.Empty ? concept.Name : entity.Name;
                        concept.TopicId = entity.TopicId == Guid.Empty ? concept.TopicId : entity.TopicId;
                        concept.CategoryId = entity.CategoryId == Guid.Empty ? concept.CategoryId : entity.CategoryId;
                        concept.Definition = entity.Definition == String.Empty ? concept.Definition : entity.Definition;
                        concept.Example = entity.Example == String.Empty ? concept.Example : entity.Example;
                        concept.PointsToRemember = entity.PointsToRemember == String.Empty ? concept.PointsToRemember : entity.PointsToRemember;
                        concept.UpdatedOn = DateTime.Now;
                        concept.UpdatedBy = entity.UpdatedBy;
                        concept.IsActive = entity.IsActive == null ? concept.IsActive : entity.IsActive;
                        //First Update Master Table
                        _unitOfWork.ConceptMappingRepository.Update(concept);

                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Message = "Success";
                            RMsg.Data.Add(concept.Id);
                        }
                        else
                        {
                            RMsg.Message = "Error";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;
                        }
                    }

                    else
                    {
                        RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                        RMsg.Message = "Missing Something";
                    }

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
