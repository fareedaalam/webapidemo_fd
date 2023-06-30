using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BusinessServices.Repository
{
    public class QuizRepository : IQuizInterface
    {
        private readonly IEmailInterface _IEmailInterface;
        private readonly IMapParentChildInterface _iMapParentChildInterface;

        private readonly UnitOfWork _unitOfWork;
        public QuizRepository(UnitOfWork unitOfWork, IMapParentChildInterface iMapParentChildInterface, IEmailInterface iEmailInterface)
        {
            _unitOfWork = unitOfWork;
            _iMapParentChildInterface = iMapParentChildInterface;
            _IEmailInterface = iEmailInterface;
        }
        public FunctionResponse Create(QuizEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();

            try
            {
                var quiz = new tbl_Quiz
                {
                    Id = Guid.NewGuid(),
                    UserId = entity.UserId,
                    StandardId = entity.StandardId,
                    SubjectId = entity.SubjectId,
                    TopicId = entity.TopicId,
                    SubTopicId = entity.SubTopicId,
                    CategoryId = entity.CategoryId,

                    TotalQuestions = entity.TotalQuestions,
                    Duration = entity.Duration,

                    CreatedOn = DateTime.Now,
                    CreatedBy = entity.CreatedBy,
                    IsActive = entity.IsActive == null ? true : entity.IsActive,

                };

                //Add Master Data First
                _unitOfWork.QuizRepository.Insert(quiz);

                for (var i = 0; i < entity.QuizDetails.Count; i++)
                {
                    var quizDetails = new tbl_QuizDetails
                    {
                        Id = Guid.NewGuid(),
                        QuizId = quiz.Id,
                        Question = entity.QuizDetails[i].Question,
                        CorrectAnswer = entity.QuizDetails[i].CorrectAnswer,
                        op1 = entity.QuizDetails[i].op1,
                        op2 = entity.QuizDetails[i].op2,
                        op3 = entity.QuizDetails[i].op3,
                        op4 = entity.QuizDetails[i].op4,
                        CreatedOn = DateTime.Now,
                        CreatedBy = quiz.CreatedBy,
                    };

                    _unitOfWork.QuizDetailsRepository.Insert(quizDetails);


                    if (entity.quizResponse != null && entity.quizResponse.Count > 0)
                    {
                        var response = new tbl_QuizResponse
                        {
                            QuizId = quiz.Id,
                            QuizDetailsId = quizDetails.Id,
                            StudentId = entity.quizResponse[i].StudentId,
                            SelectedAns = entity.quizResponse[i].SelectedAns != String.Empty ? entity.quizResponse[i].SelectedAns : null,
                            AnsStatus = entity.quizResponse[i].AnsStatus,
                            CreatedOn = DateTime.Now,
                            CreatedBy = entity.quizResponse[i].StudentId
                        };

                        _unitOfWork.QuizResponseRepository.Insert(response);

                    }

                }

                //Add Detail Data with master Id
                //foreach (var item in entity.QuizDetails)
                //{
                //    var quizDetails = new tbl_QuizDetails
                //    {
                //        Id = Guid.NewGuid(),
                //        QuizId = quiz.Id,
                //        Question = item.Question,
                //        CorrectAnswer = item.CorrectAnswer,
                //        op1 = item.op1,
                //        op2 = item.op2,
                //        op3 = item.op3,
                //        op4 = item.op4,
                //        CreatedOn = DateTime.Now,
                //        CreatedBy = quiz.CreatedBy,
                //    };

                //    _unitOfWork.QuizDetailsRepository.Insert(quizDetails);

                //    if (entity.quizResponse.Count > 0)
                //    {
                //        foreach (var reponse in entity.quizResponse)
                //        {
                //            var response = new tbl_QuizResponse
                //            {
                //                QuizId = quiz.Id,
                //                QuizDetailsId = quizDetails.Id,
                //                StudentId = reponse.StudentId,
                //                SelectedAns = reponse.SelectedAns != String.Empty ? reponse.SelectedAns : null,
                //                AnsStatus = reponse.AnsStatus,
                //                CreatedOn = DateTime.Now,
                //                CreatedBy = reponse.StudentId
                //            };

                //            _unitOfWork.QuizResponseRepository.Insert(response);
                //        }
                //    }
                //}



                if (_unitOfWork.Save() > 0)
                {
                    //Send Report Mail to Respictive Parent
                    SendReportMail(entity);
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(quiz.Id);
                }
                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "Missing Reference";

                }
            }

            catch (Exception)
            {
                throw;

            }
            return Resp;
        }
        public FunctionResponse GetAll()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                //var brds = _unitOfWork.BoardRepository.GetAll().ToList();
                var data = _unitOfWork.QuizRepository.GetQuiz();

                if (data.Any())
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Message = "Success";
                    //  var k = Mapper.Map<List<tbl_Quiz>, List<QuizEntity>>(data);
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

        public FunctionResponse Update(Guid Id, QuizEntity entity)
        {

            try
            {
                FunctionResponse Resp = new FunctionResponse();
                if (entity != null && Id != Guid.Empty)
                {
                    var quiz = _unitOfWork.QuizRepository.GetByID(Id);

                    if (quiz != null)
                    {
                        if (quiz.IsActive != entity.IsActive)
                        {
                            quiz.IsActive = entity.IsActive;
                            quiz.UpdatedOn = DateTime.Now;
                            if (entity.UpdatedBy != null)
                                quiz.UpdatedBy = entity.UpdatedBy;
                        }
                        //quiz.StandardId = entity.StandardId == null ? quiz.StandardId : entity.StandardId;
                        //quiz.SubjectId = entity.SubjectId == null ? quiz.SubjectId : entity.SubjectId;
                        //quiz.TopicId = entity.TopicId == null ? quiz.TopicId : entity.TopicId;
                        //quiz.SubTopicId = entity.SubTopicId == null ? quiz.SubTopicId : entity.SubTopicId;
                        //quiz.CategoryId = entity.CategoryId == null ? quiz.CategoryId : entity.CategoryId;

                        //quiz.TotalQuestions = entity.TotalQuestions == null ? quiz.TotalQuestions : entity.TotalQuestions;
                        //quiz.Duration = entity.Duration == null ? quiz.Duration : entity.Duration;

                        //quiz.UpdatedOn = DateTime.Now;
                        //quiz.UpdatedBy = entity.UpdatedBy == null ? quiz.CreatedBy : entity.UpdatedBy;
                        //quiz.IsActive = entity.IsActive == null ? quiz.IsActive : entity.IsActive;
                    }

                    //Update
                    _unitOfWork.QuizRepository.Update(quiz);

                    //Delete Releated Data First
                    //_unitOfWork.QuizDetailsRepository.Delete(x => x.QuizId == Id);

                    ////Add Detail Data with master Id
                    //foreach (var item in entity.QuizDetails)
                    //{
                    //    var quizDetails = new tbl_QuizDetails
                    //    {
                    //        Id = Guid.NewGuid(),
                    //        QuizId = quiz.Id,
                    //        Question = item.Question,
                    //        CorrectAnswer = item.CorrectAnswer,
                    //        op1 = item.op1,
                    //        op2 = item.op2,
                    //        op3 = item.op3,
                    //        op4 = item.op4,
                    //        IsActive = true,
                    //        CreatedOn = DateTime.Now,
                    //        CreatedBy = quiz.CreatedBy,
                    //    };

                    //    _unitOfWork.QuizDetailsRepository.Insert(quizDetails);
                    //}


                    if (_unitOfWork.Save() > 0)
                    {
                        Resp.Status = FunctionResponse.StatusType.SUCCESS;
                        Resp.Message = "Success";
                    }
                    else
                    {
                        Resp.Status = FunctionResponse.StatusType.ERROR;
                        Resp.Message = "Missing Reference";

                    }
                }
                return Resp;
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
                var quiz = _unitOfWork.QuizRepository.GetByID(Id);
                if (quiz != null)
                {
                    _unitOfWork.QuizDetailsRepository.Delete(x => x.QuizId == Id);
                    _unitOfWork.MapTeacherStudentQuizRepository.Delete(x => x.QuizId == Id);
                    _unitOfWork.QuizRepository.Delete(Id);
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
        public FunctionResponse GetById(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.QuizRepository.GetQuizById(Id);
                // var data = _unitOfWork.QuizRepository.GetQuiz().Where(q => q.UserId == Id);

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

        private void SendReportMail(QuizEntity user)
        {
            FunctionResponse resp = new FunctionResponse();
            // Guid defaultId = Guid.NewGuid();
            //Guid UserId = new Guid();
            //var UserId = user.UserId ?? Guid.NewGuid();

            var UserId = user.UserId;

            if (Guid.Empty != UserId)
            {
                resp = _iMapParentChildInterface.GetAssignedChildListByChildId(UserId);
                if (resp.Data.Count > 0)
                {
                    // string tbl = Utility.GenerateHtmlTable(4, 1);
                    List<MapParentChildEntity> child = (List<MapParentChildEntity>)resp.Data[0];
                    string EmailTo = child[0].ParentEmail;

                    StringBuilder sb = new StringBuilder("Hi ", 500);
                    sb.Append(child[0].ParentName);
                    sb.AppendLine("<p> Your Child ");
                    sb.AppendLine(child[0].ChildName + " " + " Participated On Practice2Perfection with given bellow details.</p> ");
                    sb.AppendLine("<table border='1'>");
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<th>" + "Total Question" + "</th>");
                    sb.AppendLine("<th>" + "Duration" + "</th>");
                    sb.AppendLine("<th>" + "Result" + "</th>");
                    sb.AppendLine("</tr>");
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<td>" + user.TotalQuestions + "</td>");
                    sb.AppendLine("<td>" + user.Duration + " min</td>");
                    sb.AppendLine("<td>");
                    ///parent/report
                    sb.AppendLine("<p>Please <strong>");
                    sb.AppendLine("<a href=");
                    sb.AppendLine(ConfigurationManager.AppSettings["BaseUrl"].ToString() + "parent/report");
                    sb.AppendLine(">Click Here</a></strong> </P>");
                    sb.AppendLine("</td>");

                    sb.AppendLine("</tr>");
                    sb.AppendLine("</table>");

                    
                    //  sb.AppendLine(tbl);
                    string Body = sb.ToString();

                    ////  string Body = "<a>http://localhost:51778/Api/User/Verify?Id=" + Resp.Data[0]+"</a>";

                    _IEmailInterface.SendEmail(EmailTo, "", "Report" + DateTime.Now.ToShortDateString(), Body);
                    //return Resp;
                }

            }
        }

        public FunctionResponse GetQuizByUser(QuizEntity quiz)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.QuizRepository.GetQuizByAnyId(quiz);
                // var data = _unitOfWork.QuizRepository.GetQuiz().Where(q => q.UserId == Id);

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


        public FunctionResponse saveQuizResponse(QuizEntity entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                //var quiz = _unitOfWork.QuizRepository.GetQuizByAnyId(entity).ToList();

                if (entity.quizResponse.Count > 0)
                {
                    for (var i = 0; i < entity.QuizDetails.Count; i++)
                    {
                        if (entity.quizResponse != null && entity.quizResponse.Count > 0)
                        {
                            var response = new tbl_QuizResponse
                            {
                                QuizId = entity.Id,
                                QuizDetailsId = entity.QuizDetails[i].Id,
                                StudentId = entity.quizResponse[i].StudentId,
                                SelectedAns = entity.quizResponse[i].SelectedAns != String.Empty ? entity.quizResponse[i].SelectedAns : null,
                                AnsStatus = entity.quizResponse[i].AnsStatus,
                                CreatedOn = DateTime.Now,
                                CreatedBy = entity.quizResponse[i].StudentId
                            };

                            _unitOfWork.QuizResponseRepository.Insert(response);
                        }
                    }
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Please Provide Role Type";
                }

                //get entity with 3 id
                //vat k
                var map = _unitOfWork.MapTeacherStudentQuizRepository.Get(id => id.QuizId == entity.Id && id.TeacherId == entity.UserId && id.StudentId == entity.QuizMapping.StudentId);

                if (entity.QuizMapping != null)
                {
                    if (map != null)
                    {
                        map.Attempted = true;
                        map.IsTimeUp = entity.QuizMapping.IsTimeUp;
                        map.IsQuit = entity.QuizMapping.IsQuit;
                        map.StartTime = entity.QuizMapping.StartTime != null ? entity.QuizMapping.StartTime : null;
                        map.EndTime = entity.QuizMapping.EndTime != null ? entity.QuizMapping.EndTime : null;
                        map.UpdatedBy = entity.QuizMapping.StudentId;
                        map.UpdatedOn = DateTime.Now;
                    }
                    _unitOfWork.MapTeacherStudentQuizRepository.Update(map);
                }

                //if (entity.QuizMapping != null)
                //{
                //    var mapping = new tbl_MapTeacherStudentQuiz
                //    {
                //        Attempted = entity.QuizMapping.Attempted,
                //        IsTimeUp = entity.QuizMapping.IsTimeUp,
                //        IsQuit = entity.QuizMapping.IsQuit,
                //        StartTime = entity.QuizMapping.StartTime != null ? entity.QuizMapping.StartTime : null,
                //        EndTime = entity.QuizMapping.EndTime != null ? entity.QuizMapping.EndTime : null,
                //        UpdatedBy = entity.QuizMapping.StudentId,
                //        UpdatedOn = DateTime.Now
                //    };

                //    _unitOfWork.MapTeacherStudentQuizRepository.Update(mapping);

                //}
                //Add Role To User......................

                //if (entity.quizResponse.Count > 0)
                //{
                //    foreach (var item in entity.quizResponse)
                //    {
                //        var response = new tbl_QuizResponse
                //        {
                //            QuizId = entity.Id,
                //            QuizDetailsId = item.QuizDetailsId,
                //            StudentId = item.StudentId,
                //            SelectedAns = item.SelectedAns != String.Empty ? item.SelectedAns : null,
                //            AnsStatus = item.AnsStatus,
                //            CreatedOn = DateTime.Now,
                //            CreatedBy = item.StudentId                            
                //        };

                //        _unitOfWork.QuizResponseRepository.Insert(response);
                //    }
                //}
                //else
                //{
                //    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                //    RMsg.Message = "Please Provide Role Type";
                //}

                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Message = "Success";
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    //RMsg.Data.Add(user.Id);                        
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
