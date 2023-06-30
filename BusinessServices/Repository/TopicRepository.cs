using BusinessServices;
using BusinessServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataModel.UnitOfWork;
using DataModel;
using AutoMapper;

namespace BusinessServices.Repository
{
    public class TopicRepository : ITopicInterface
    {

        private readonly UnitOfWork _unitOfWork;
        public TopicRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public Guid Create(TopicEntity topicEntity)
        //{

        //    var topic = new tbl_Topic
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = topicEntity.Name,
        //        SubjectId= topicEntity.SubjectId,  
        //        IsActive= topicEntity.IsActive,
        //        CreatedOn = DateTime.Now,
        //        CreatedBy = topicEntity.CreatedBy
        //    };
        //    _unitOfWork.TopicRepository.Insert(topic);
        //    _unitOfWork.Save();
        //    return topic.Id;
        //}

        public FunctionResponse Create(TopicEntity topicEntity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var topic = new tbl_Topic
                {
                    Id = Guid.NewGuid(),
                    Name = topicEntity.Name,
                    SubjectId = topicEntity.SubjectId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = topicEntity.CreatedBy,
                    IsActive = topicEntity.IsActive == null ? false : topicEntity.IsActive
                };

                var user = _unitOfWork.TopicRepository.Get(u => u.Name == topic.Name && u.SubjectId == topic.SubjectId);

                if (user == null)
                {
                    _unitOfWork.TopicRepository.Insert(topic);
                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(topic.Id);
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

        public bool Delete(Guid TopicId)
        {
            try
            {
                var Success = false;
                if (TopicId != null)
                {
                    var Topic = _unitOfWork.TopicRepository.GetByID(TopicId);
                    if (Topic != null)
                    {
                       // Topic.IsActive = Topic.IsActive == true ? false : true;
                       // _unitOfWork.TopicRepository.Update(Topic);
                        _unitOfWork.TopicRepository.Delete(Topic);
                        _unitOfWork.Save();
                        Success = true;
                    }
                }
                return Success;

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public IEnumerable<TopicEntity> GetAll()
        {
            var Topics = _unitOfWork.TopicRepository.GetTopic().OrderByDescending(x => x.CreatedOn).ToList();
            if (Topics.Any())
            {
                // var TopicsModel = Mapper.Map<List<tbl_Topic>, List<TopicEntity>>(Topics);
                IEnumerable<TopicEntity> List = Topics;
                return List;
            }
            return null;
        }

        public IEnumerable<TopicEntity> GetById(Guid TopicId)
        {
            var topic = _unitOfWork.TopicRepository.GetTopicById(TopicId);
            if (topic != null)
            {
                // var topicModel = Mapper.Map<tbl_Topic, TopicEntity>(topic);
                IEnumerable<TopicEntity> List = topic;
                return List;
            }
            return null;
        }

        //public bool Update(Guid TopicId, TopicEntity topicEntity)
        //{
        //    var Success = false;
        //    if (topicEntity != null)
        //    {
        //        var topic = _unitOfWork.TopicRepository.GetByID(TopicId);
        //        if (topic != null)
        //        {
        //            if (topicEntity.Name != null)
        //                topic.Name = topicEntity.Name;

        //            if (topicEntity.SubjectId != null)
        //                topic.SubjectId = topicEntity.SubjectId;

        //            if (topicEntity.IsActive != null)
        //                topic.IsActive = topicEntity.IsActive;

        //            topic.UpdatedOn = DateTime.Now;
        //        }
        //        _unitOfWork.TopicRepository.Update(topic);
        //        _unitOfWork.Save();
        //        Success = true;
        //    }
        //    return Success;

        //}

        public FunctionResponse Update(Guid TopicId, TopicEntity topicEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (topicEntity != null && TopicId != Guid.Empty)
                {
                    var top = _unitOfWork.TopicRepository.GetByID(TopicId);
                    if (top != null)
                    {
                        //check Duplicate
                        if (top.Name.Trim() == topicEntity.Name.Trim() && top.SubjectId == topicEntity.SubjectId && top.IsActive==topicEntity.IsActive)
                        {
                            RMsg.Message = "Duplicate";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;

                        }
                        else if(top.IsActive != topicEntity.IsActive)
                        {
                         
                            top.IsActive = topicEntity.IsActive == null ? false : topicEntity.IsActive;
                            top.UpdatedOn = DateTime.Now;
                            top.UpdatedBy = topicEntity.UpdatedBy;
                            _unitOfWork.TopicRepository.Update(top);

                        }
                        else
                        {
                            if (topicEntity.Name != null)
                                top.Name = topicEntity.Name == null ? null : topicEntity.Name.Trim();

                            if (topicEntity.SubjectId != null)
                                top.SubjectId = topicEntity.SubjectId;

                            if (topicEntity.IsActive != null)
                                top.IsActive = topicEntity.IsActive;

                            top.UpdatedOn = DateTime.Now;
                            top.UpdatedBy = topicEntity.UpdatedBy;

                            _unitOfWork.TopicRepository.Update(top);

                        }
                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Data.Add(top.Id);
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

        public FunctionResponse GetTopicBySubjectId(Guid SubjectId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.TopicRepository.GetMany(x => x.SubjectId == SubjectId).ToList<tbl_Topic>();

                if (data != null)
                {
                    var dataModel = Mapper.Map<List<tbl_Topic>, List<TopicEntity>>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(dataModel);
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
    }
}
