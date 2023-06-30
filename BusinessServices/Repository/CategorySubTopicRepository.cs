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
    public class CategorySubTopicRepository : ICategorySubTopicInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public CategorySubTopicRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<CategorySubTopicEntity> GetById(Guid id)
        {
            var cst = _unitOfWork.SubTopicRepository.GetCategoryById(id);
            if (cst != null)
            {
                IEnumerable<CategorySubTopicEntity> List = cst;
                return List;
            }
            return null;
        }

        public IEnumerable<CategorySubTopicEntity> GetAll()
        {

            var categorySubTopic = _unitOfWork.CategorySubTopicRepository.GetCategory().OrderByDescending(x => x.CreatedOn).ToList();
            if (categorySubTopic.Any())
            {
                IEnumerable<CategorySubTopicEntity> List = categorySubTopic;
                return List;
            }
            return null;
        }

        public FunctionResponse Create(CategorySubTopicEntity cstEntity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var cst = new tbl_Category_SubTopic
                {
                    Id = Guid.NewGuid(),
                    Name = cstEntity.Name,
                    Code = cstEntity.Code,
                    TopicId = cstEntity.TopicId,
                    SubTopicId = cstEntity.SubTopicId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = cstEntity.CreatedBy,
                    IsActive = cstEntity.IsActive == null ? true : cstEntity.IsActive
                };
                var stud_user = _unitOfWork.CategorySubTopicRepository.Get(s => s.Name == cst.Name || s.Code == cst.Code);
                if (stud_user == null)
                {
                    _unitOfWork.CategorySubTopicRepository.Insert(cst);
                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(cst.Id);
                }
                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "Duplicate";

                }
            }
            catch (Exception)
            {
                throw;
            }
            return Resp;

        }

        public FunctionResponse Update(Guid cstId, CategorySubTopicEntity Entity)
        {

            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (Entity != null && cstId != Guid.Empty)
                {
                    var cst = _unitOfWork.CategorySubTopicRepository.GetByID(cstId);
                    if (cst != null)
                    {
                        //check Duplicate
                        if (cst.Name == Entity.Name && cst.Code == cst.Code && cst.TopicId == Entity.TopicId &&
                            cst.SubTopicId == Entity.SubTopicId && cst.IsActive == Entity.IsActive)
                        {
                            RMsg.Message = "Duplicate";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;
                        }
                        else if (cst.IsActive != Entity.IsActive)
                        {
                            cst.IsActive = Entity.IsActive == null ? false : Entity.IsActive;
                            cst.UpdatedBy = Entity.UpdatedBy;
                            cst.UpdatedOn = DateTime.Now;
                        }
                        else
                        {

                            cst.Name = Entity.Name == null ? null : Entity.Name.Trim();
                            cst.Code = Entity.Code == null ? cst.Code : Entity.Code;
                            cst.TopicId = Entity.TopicId;
                            cst.SubTopicId = Entity.SubTopicId;
                            cst.IsActive = Entity.IsActive;
                            cst.UpdatedBy = Entity.UpdatedBy;
                            cst.UpdatedOn = DateTime.Now;
                        }
                        _unitOfWork.CategorySubTopicRepository.Update(cst);
                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Data.Add(cst.Id);
                            RMsg.Message = "Success";
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
                    var categorySubTopic = _unitOfWork.CategorySubTopicRepository.GetByID(id);

                    if (categorySubTopic != null)
                    {
                        // categorySubTopic.IsActive = categorySubTopic.IsActive == true ? false : true;                      
                        //  _unitOfWork.CategorySubTopicRepository.Update(categorySubTopic);
                        _unitOfWork.CategorySubTopicRepository.Delete(categorySubTopic);
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

        public FunctionResponse GetCategory(CategorySubTopicEntity cstEntity)
        {
            try
            {

                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.QuestionPatternRepository.GetCategory()
                    .Where(x =>
                   x.SubTopicId == (cstEntity.SubTopicId == Guid.Empty ? x.SubTopicId : cstEntity.SubTopicId)
                   || x.TopicId == (cstEntity.TopicId == Guid.Empty ? x.TopicId : cstEntity.TopicId)
                    && x.IsActive == true
                    ).OrderBy(o => o.Name);
                if (data.Any() && data.Count() > 0)
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

            catch (Exception)
            {
                throw;
            }
        }
    }

}
