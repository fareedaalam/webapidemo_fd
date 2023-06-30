using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessServices.Repository
{
    class QuestionPatternRepository : IQustionPatternInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public QuestionPatternRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FunctionResponse Createquestion(QuestionPatternEntity questionEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (questionEntity != null)
                {
                    var que = new tbl_Question_Pattern
                    {
                        Id = Guid.NewGuid(),
                        Code = questionEntity.Code,
                        Pattern = questionEntity.Pattern,
                        TopicId = questionEntity.TopicId,
                        Sub_TopicId = questionEntity.SubTopicId,
                        Category_SubTopicId = questionEntity.CategorySubTopicId,
                        LevelId = questionEntity.LevelId,
                        BoardId = questionEntity.BoardId,
                        SubjectId = questionEntity.SubjectId,
                        StandardId = questionEntity.StandardId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = questionEntity.CreatedBy,
                        IsActive = questionEntity.IsActive
                    };

                    //_unitOfWork.QuestionPatternRepository.Insert(que);

                    //var Question = _unitOfWork.QuestionPatternRepository.Get(u =>  u.Pattern == que.Pattern);

                    var Question = _unitOfWork.QuestionPatternRepository.Get(P => P.BoardId == que.BoardId && P.StandardId == que.StandardId && P.SubjectId == que.SubjectId && P.TopicId == que.TopicId && P.Sub_TopicId == que.Sub_TopicId && P.Category_SubTopicId == que.Category_SubTopicId && P.LevelId == que.LevelId && P.Pattern == que.Pattern);


                    if (Question == null)
                    {
                        _unitOfWork.QuestionPatternRepository.Insert(que);
                        _unitOfWork.Save();
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                        RMsg.Data.Add(que.Id);
                    }
                    else
                    {
                        RMsg.Status = FunctionResponse.StatusType.ERROR;
                        RMsg.Message = "Duplicate";

                    }
                }
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public FunctionResponse Deletequestion(Guid queId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (queId != null)
                {

                    var que = _unitOfWork.QuestionPatternRepository.GetByID(queId);
                    if (que != null)
                    {
                      //  que.IsActive = que.IsActive == true ? false : true;
                       // _unitOfWork.QuestionPatternRepository.Update(que);

                        _unitOfWork.QuestionPatternRepository.Delete(que);

                    }
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }
                    //Generate_Code_Delete(brd.Code);

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

        public FunctionResponse GetAllquestion()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var ques = _unitOfWork.QuestionPatternRepository.GetQuestionPattern().OrderByDescending(x => x.CreatedOn).ToList();

                //  IEnumerable<QuestionPatternEntity> ques = _unitOfWork.QuestionPatternRepository.GetQuestionPattern();

                if (ques.Any() && ques.Count() > 0)
                {
                    //  var quesModel = Mapper.Map<List<tbl_Question_Pattern>, List<QuestionPatternEntity>>(ques);

                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;

                    RMsg.Data.Add(ques);

                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                //}

                return RMsg;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public FunctionResponse GetquestionById(Guid quesdId)
        {
            FunctionResponse RMsg = new FunctionResponse();

            try
            {

                // var que_get = _unitOfWork.QuestionPatternRepository.GetByID(quesdId);

                IEnumerable<QuestionPatternEntity> ques_get = _unitOfWork.QuestionPatternRepository.GetquestionById(quesdId);

                if (ques_get != null && ques_get.Count() > 0)
                {

                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;

                    RMsg.Data.Add(ques_get);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.ERROR;
                    RMsg.Message = "No Record Found";
                }
                return RMsg;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FunctionResponse Updatequestion(Guid queId, QuestionPatternEntity questionEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (questionEntity != null && queId != Guid.Empty)
                {
                    var que = _unitOfWork.QuestionPatternRepository.GetByID(queId);

                    if (que != null)
                    {

                        var patternname = _unitOfWork.QuestionPatternRepository.Get(
                            P => P.BoardId == questionEntity.BoardId
                            && P.StandardId == questionEntity.StandardId
                            && P.SubjectId == questionEntity.SubjectId
                            && P.TopicId == questionEntity.TopicId
                            && P.Sub_TopicId == questionEntity.SubTopicId
                            && P.Category_SubTopicId == questionEntity.CategorySubTopicId
                            && P.LevelId == questionEntity.LevelId
                            && P.Pattern == questionEntity.Pattern
                            && P.IsActive == questionEntity.IsActive);
                        //  var patternname = _unitOfWork.QuestionPatternRepository.Get(P => P.Pattern == que.Pattern);

                        if (patternname == null)
                        {
                            que.Code = questionEntity.Code == null ? null : questionEntity.Code.Trim();
                            que.Pattern = questionEntity.Pattern == null ? null : questionEntity.Pattern.Trim();
                            que.TopicId = questionEntity.TopicId;
                            que.Sub_TopicId = questionEntity.SubTopicId;
                            que.Category_SubTopicId = questionEntity.CategorySubTopicId;
                            que.LevelId = questionEntity.LevelId;
                            que.BoardId = questionEntity.BoardId;
                            que.SubjectId = questionEntity.SubjectId;
                            que.StandardId = questionEntity.StandardId;

                            que.UpdatedOn = DateTime.Now;
                            que.UpdatedBy = questionEntity.UpdatedBy;
                            que.IsActive = questionEntity.IsActive;

                            _unitOfWork.QuestionPatternRepository.Update(que);

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
                            if (patternname.Id == questionEntity.Id)
                            {
                                que.Code = questionEntity.Code == null ? null : questionEntity.Code.Trim();
                                que.Pattern = questionEntity.Pattern == null ? null : questionEntity.Pattern.Trim();
                                que.TopicId = questionEntity.TopicId;
                                que.Sub_TopicId = questionEntity.SubTopicId;
                                que.Category_SubTopicId = questionEntity.CategorySubTopicId;
                                que.LevelId = questionEntity.LevelId;
                                que.BoardId = questionEntity.BoardId;
                                que.SubjectId = questionEntity.SubjectId;
                                que.StandardId = questionEntity.StandardId;
                                que.UpdatedOn = DateTime.Now;
                                que.UpdatedBy = questionEntity.UpdatedBy;
                                que.IsActive = questionEntity.IsActive;

                                _unitOfWork.QuestionPatternRepository.Update(que);

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
                                RMsg.Message = "Duplicate";
                                RMsg.Status = FunctionResponse.StatusType.ERROR;
                            }
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
        public FunctionResponse GetQuestionPattern(QuestionPatternEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var ques = _unitOfWork.QuestionPatternRepository.GetQuestionPattern()
                    .Where(x =>
                    // x.BoardId == (entity.BoardId == null ? x.BoardId : entity.BoardId)
                    x.SubjectId == (entity.SubjectId == Guid.Empty ? x.SubjectId : entity.SubjectId)
                   //&& x.StandardId == (entity.StandardId == null ? x.StandardId : entity.StandardId)
                   && x.TopicId == (entity.TopicId == Guid.Empty ? x.TopicId : entity.TopicId)
                   && x.SubTopicId == (entity.SubTopicId == Guid.Empty ? x.SubTopicId : entity.SubTopicId)
                   && x.CategorySubTopicId == (entity.CategorySubTopicId == Guid.Empty ? x.CategorySubTopicId : entity.CategorySubTopicId)
                   && x.LevelId == (entity.LevelId == Guid.Empty ? x.LevelId : entity.LevelId)
                   && x.IsActive == true
                    );

                if (ques.Count() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(ques);
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
        //Get category by ...................
        public FunctionResponse GetQuestionPattern_Category(QuestionPatternEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var ques = _unitOfWork.QuestionPatternRepository.GetQuestionPattern()
                   .Where(x =>
                    //  x.BoardId == (entity.BoardId ==null ? x.BoardId : entity.BoardId)
                    x.SubjectId == (entity.SubjectId == Guid.Empty ? x.SubjectId : entity.SubjectId)
                   //&& x.StandardId == (entity.StandardId == null ? x.StandardId : entity.StandardId)
                   && x.TopicId == (entity.TopicId == Guid.Empty ? x.TopicId : entity.TopicId)
                   && x.SubTopicId == (entity.SubTopicId == Guid.Empty ? x.SubTopicId : entity.SubTopicId)
                   && x.CategorySubTopicId == (entity.CategorySubTopicId == Guid.Empty ? x.CategorySubTopicId : entity.CategorySubTopicId)
                   && x.LevelId == (entity.LevelId == Guid.Empty ? x.LevelId : entity.LevelId)
                   && x.IsActive == true
                    );

                if (ques.Count() > 0)
                {
                    List<QuestionPatternEntity> myList = new List<QuestionPatternEntity>();
                    myList = ques.ToList();

                    var result = RemoveDuplicatesIterative(myList);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(result);
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

        public static List<QuestionPatternEntity> RemoveDuplicatesIterative(List<QuestionPatternEntity> items)
        {
            List<QuestionPatternEntity> result = new List<QuestionPatternEntity>();
            for (int i = 0; i < items.Count; i++)
            {
                // Assume not duplicate.
                bool duplicate = false;
                for (int z = 0; z < i; z++)
                {
                    if (items[z].CategorySubTopicId == items[i].CategorySubTopicId)
                    {
                        // This is a duplicate.
                        duplicate = true;
                        break;
                    }
                }
                // If not duplicate, add to result.
                if (!duplicate)
                {
                    result.Add(items[i]);
                }
            }
            return result;
        }

        public FunctionResponse GetQuestionPatternSolution(QuestionPatternEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var ques = _unitOfWork.QuestionPatternRepository.GetQuestionPatternsWithSolutions(entity.CategorySubTopicId, entity.LevelId);
                 //  .Where(x =>
                 //   // x.BoardId == (entity.BoardId == null ? x.BoardId : entity.BoardId)
                 //  // x.SubjectId == (entity.SubjectId == Guid.Empty ? x.SubjectId : entity.SubjectId)
                 //  //  && x.StandardId == (entity.StandardId == null ? x.StandardId : entity.StandardId)
                 ////  && x.TopicId == (entity.TopicId == Guid.Empty ? x.TopicId : entity.TopicId)
                 ////  && x.SubTopicId == (entity.SubTopicId == Guid.Empty ? x.SubTopicId : entity.SubTopicId)
                 //  x.CategorySubTopicId == (entity.CategorySubTopicId == Guid.Empty ? x.CategorySubTopicId : entity.CategorySubTopicId)
                 //  && x.LevelId == (entity.LevelId == Guid.Empty ? x.LevelId : entity.LevelId)
                 ////&& x.IsActive == true
                 //   );

                if (ques.Count() > 0)
                {
                    List<QuestionPatternEntity> myList = new List<QuestionPatternEntity>();
                    myList = ques.ToList();

                    //var result = RemoveDuplicatesIterative(myList);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(myList);
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
