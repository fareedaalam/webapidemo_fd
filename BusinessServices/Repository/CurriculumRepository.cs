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
    public class CurriculumRepository : ICurriculumInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public CurriculumRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Master
        public FunctionResponse Create(CurriculumEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                //Check Duplicate
                var Duplicate = _unitOfWork.CurriculumRepository.Get(x => x.CountryId == entity.CountryId && x.BoardId == entity.BoardId
                && x.StandardId == entity.StandardId && x.SubjectId == entity.SubjectId);

                if (Duplicate == null)
                {
                    //insert First Data into Curriculum Master
                    var curriculum = new tbl_Curriculum
                    {
                        Id = Guid.NewGuid(),
                        Name = entity.Name,
                        CountryId = entity.CountryId,
                        BoardId = entity.BoardId,
                        StandardId = entity.StandardId,
                        SubjectId = entity.SubjectId,
                        //TopicId = entity.topicList[0].Id,
                        CreatedOn = DateTime.Now,
                        CreatedBy = entity.CreatedBy,
                        IsActive = entity.IsActive == null ? false : entity.IsActive,

                    };
                    _unitOfWork.CurriculumRepository.Insert(curriculum);
                    //if (entity.topicList.Count > 0)
                    //{
                    //    foreach (var item in entity.topicList)
                    //    {
                    //        var curriculumDetails = new tbl_CurriculumDetails
                    //        {
                    //            Id = Guid.NewGuid(),
                    //            CurriculumId = curriculum.Id,
                    //            TopicId = item.Id,
                    //        };
                    //        // var user = _unitOfWork.CurriculumRepository.Get(u => u.TopicId==entity.TopicId);

                    //        //if (user == null)
                    //        //{
                    //        _unitOfWork.CurriculumDetailsRepository.Insert(curriculumDetails);
                    //    }
                    //}
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
            catch (Exception)
            {

                throw;
            }

            return Resp;
        }
        public FunctionResponse Update(Guid Id, CurriculumEntity entity)
        {

            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (entity != null && Id != Guid.Empty)
                {
                    var que = _unitOfWork.CurriculumRepository.GetByID(Id);

                    if (que != null)
                    {
                        que.Name = entity.Name;
                        //que.CountryId = entity.CountryId == Guid.Empty ? que.CountryId : entity.CountryId;
                        //que.BoardId = entity.BoardId == Guid.Empty ? que.BoardId : entity.BoardId;
                        //que.StandardId = entity.StandardId == Guid.Empty ? que.StandardId : entity.StandardId;
                        //que.SubjectId = entity.SubjectId == Guid.Empty ? que.SubjectId : entity.SubjectId;

                        que.UpdatedOn = DateTime.Now;
                        que.UpdatedBy = entity.UpdatedBy;
                        que.IsActive = entity.IsActive;
                        //First Update Master Table
                        _unitOfWork.CurriculumRepository.Update(que);
                        //Update Details Table Means we are reinserting data hare
                        //Delete Releated Data First
                        // _unitOfWork.CurriculumDetailsRepository.Delete(x => x.CurriculumId == Id);
                        //foreach (var item in entity.topicList)
                        //{
                        //    var curriculumDetails = new tbl_CurriculumDetails
                        //    {
                        //        Id = Guid.NewGuid(),
                        //        CurriculumId = Id,
                        //        TopicId = item.Id,
                        //    };

                        //    _unitOfWork.CurriculumDetailsRepository.Insert(curriculumDetails);
                        //}

                        //foreach (var item in entity.topicList)
                        //{
                        //    var currDetails = _unitOfWork.CurriculumDetailsRepository.GetByID(item.Id);
                        //    var curriculumDetails = new tbl_CurriculumDetails
                        //    {
                        //       // Id = Guid.NewGuid(),
                        //        CurriculumId = entity.Id,
                        //        TopicId = item.Id,
                        //    };

                        //    _unitOfWork.CurriculumDetailsRepository.Update(curriculumDetails);
                        //}


                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Message = "Success";
                            RMsg.Data.Add(que.Id);
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

        public FunctionResponse Delete(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var curri = _unitOfWork.CurriculumRepository.GetByID(Id);
                if (curri != null)
                {
                    _unitOfWork.CurriculumDetailsRepository.Delete(x => x.CurriculumId == Id);
                    _unitOfWork.CurriculumRepository.Delete(Id);
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

                //var brds = _unitOfWork.BoardRepository.GetAll().ToList();
                var data = _unitOfWork.CurriculumRepository.GetCurriculum();
                if (data.Any())
                {
                    IEnumerable<CurriculumEntity> TranslationList = (IEnumerable<CurriculumEntity>)data;

                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";                  
                    RMsg.Data.Add(TranslationList);
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
        public FunctionResponse GetByUser(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                //var brds = _unitOfWork.BoardRepository.GetAll().ToList();
                var user = _unitOfWork.UserRepository.Get(x => x.Id == Id);
                var userSubjects = user.Subjects;
                
                var data = _unitOfWork.CurriculumRepository.GetCurriculum().Where(
                    x=>x.CountryId==user.CountryId && x.BoardId==user.BoardId && x.StandardId==user.StandardId
                    && x.IsActive==true
                    && userSubjects.Contains(x.SubjectId.ToString())
                    );

                if (data.Any())
                {
                    IEnumerable<CurriculumEntity> TranslationList = (IEnumerable<CurriculumEntity>)data;

                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    RMsg.Data.Add(TranslationList);
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

        public FunctionResponse GetById(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                //var brds = _unitOfWork.BoardRepository.GetAll().ToList();
                var data = _unitOfWork.CurriculumRepository.GetCurriculum().Where(x => x.Id == Id);
                if (data.Any())
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
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
        //public FunctionResponse Filter(CurriculumEntity entity)
        //{
        //    try
        //    {
        //        FunctionResponse RMsg = new FunctionResponse();

        //        //var brds = _unitOfWork.BoardRepository.GetAll().ToList();
        //        var data = _unitOfWork.CurriculumRepository.GetCurriculum().Where(x =>
        //        x.CountryId == (entity.CountryId == Guid.Empty ? x.CountryId : entity.CountryId)
        //        && x.BoardId == (entity.BoardId == Guid.Empty ? x.BoardId : entity.BoardId)
        //        && x.StandardId== (entity.StandardId == Guid.Empty ? x.StandardId : entity.StandardId)
        //      //  || SubjectList.con x.SubjectId.)                
        //        );


              
        //        if (data.Any())
        //        {
        //            IEnumerable<CurriculumEntity> TranslationList = (IEnumerable<CurriculumEntity>)data;

        //            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
        //            RMsg.Message = "Success";
        //            RMsg.Data.Add(TranslationList);
        //        }
        //        else
        //        {
        //            RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
        //            RMsg.Message = "No Record Found";
        //        }

        //        return RMsg;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion

        #region Details

        public FunctionResponse CreateDetails(CurriculumDetailsEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                foreach (var lvl in entity.LevelList)
                {
                    //Check Duplicate
                    var Duplicate = _unitOfWork.CurriculumDetailsRepository.Get(
                    x => x.CurriculumId == entity.CurriculumId
                    && x.TopicId == entity.TopicId
                    && x.SubTopicId == entity.SubTopicId
                    && x.CategoryId == entity.CategoryId
                    && x.LevelId == lvl.Id);

                    if (Duplicate == null)
                    {
                        var curriculumD = new tbl_CurriculumDetails
                        {
                            Id = Guid.NewGuid(),
                            CurriculumId = entity.CurriculumId,
                            TopicId = entity.TopicId,
                            SubTopicId = entity.SubTopicId,
                            CategoryId = entity.CategoryId,
                            LevelId = lvl.Id,
                        };
                        _unitOfWork.CurriculumDetailsRepository.Insert(curriculumD);
                        Resp.Status = FunctionResponse.StatusType.SUCCESS;
                        Resp.Message = "Success";
                    }
                    else
                    {
                        Resp.Status = FunctionResponse.StatusType.SUCCESS;
                        Resp.Message = "Duplicate";
                        break;
                    }
                }
                _unitOfWork.Save();


            }
            catch (Exception)
            {
                throw;
            }
            return Resp;
        }
        public FunctionResponse GetDetails(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.CurriculumRepository.GetCurriculumDetails().Where(x=>x.CurriculumId==Id);
                if (data.Any())
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
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
        public FunctionResponse GetDetails()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.CurriculumRepository.GetCurriculumDetails();
                if (data.Any())
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
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
        public FunctionResponse UpdateDetails(Guid Id, CurriculumDetailsEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                // var data = _unitOfWork.CurriculumDetailsRepository.GetByID(Id);
                foreach (var lvl in entity.LevelList)
                {
                    //Check Duplicate
                    var Duplicate = _unitOfWork.CurriculumDetailsRepository.Get(
                   x => x.CurriculumId == entity.CurriculumId
                   && x.TopicId == entity.TopicId
                   && x.SubTopicId == entity.SubTopicId
                   && x.CategoryId == entity.CategoryId
                   && x.LevelId == lvl.Id);

                    if (Duplicate == null)
                    {
                        //Insert new Level
                        var curriculumD = new tbl_CurriculumDetails
                        {
                            Id = Guid.NewGuid(),
                            CurriculumId = entity.CurriculumId,
                            TopicId = entity.TopicId,
                            SubTopicId = entity.SubTopicId,
                            CategoryId = entity.CategoryId,
                            LevelId = lvl.Id,
                        };
                        _unitOfWork.CurriculumDetailsRepository.Insert(curriculumD);
                        
                    }
                   


                }
                _unitOfWork.Save();
                Resp.Status = FunctionResponse.StatusType.SUCCESS;
                Resp.Message = "Success";


            }
            catch (Exception)
            {
                throw;
            }
            return Resp;
        }
        public FunctionResponse DeleteDetails(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var curri = _unitOfWork.CurriculumDetailsRepository.GetByID(Id);
                if (curri != null)
                {
                    // _unitOfWork.CurriculumDetailsRepository.Delete(x => x.CurriculumId == Id);
                    _unitOfWork.CurriculumDetailsRepository.Delete(Id);
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

       
        #endregion
    }
}
