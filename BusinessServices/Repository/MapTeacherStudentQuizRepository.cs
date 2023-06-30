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
    class MapTeacherStudentQuizRepository : IMapTeacherStudentQuizInterface
    {
        private readonly UnitOfWork _unitOfWork;
        private IUserInterface _iUserInterface;

        public MapTeacherStudentQuizRepository(UnitOfWork unitOfWork, IUserInterface iUserInterface)
        {
            _unitOfWork = unitOfWork;
            _iUserInterface = iUserInterface;
        }

        public FunctionResponse AssignQuizToStudent(List<MapTeacherStudentQuizEntity> mapTeacherStudentQuiz)
        {
            FunctionResponse Resp = new FunctionResponse();

            try
            {
                foreach (var item in mapTeacherStudentQuiz)
                {
                    var quiz = new tbl_MapTeacherStudentQuiz
                    {
                        TeacherId = item.TeacherId,
                        StudentId = item.StudentId,
                        QuizId = item.QuizId,
                        Attempted = item.Attempted == null ? false : item.Attempted,
                        IsQuit = item.IsQuit == null ? null : item.IsQuit,
                        IsTimeUp = item.IsTimeUp == null ? null : item.IsTimeUp,

                        CreatedOn = DateTime.Now,
                        CreatedBy = item.CreatedBy
                    };

                    //Add Master Data First
                    _unitOfWork.MapTeacherStudentQuizRepository.Insert(quiz);
                }

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
            catch (Exception)
            {
                throw;

            }
            return Resp;
        }
        /// <summary>
        /// Author:Asif 
        /// </summary>
        /// <param name="QuizId"></param>
        /// <returns></returns>
        public FunctionResponse GetAssignedStudentListByQuizId(Guid QuizId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<MapTeacherStudentQuizEntity> data = _unitOfWork.MapTeacherStudentQuizRepository.GetAssignedStudentList(QuizId);
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

        public FunctionResponse GetStudentQuizList(Guid StudentId, Guid? TeacherId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                List<QuizListModel> data = _unitOfWork.MapTeacherStudentQuizRepository.GetStudentQuizList(StudentId, TeacherId);
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


        public FunctionResponse Update(Guid Id, List<MapTeacherStudentQuizEntity> entity)
        {
            try
            {
                FunctionResponse Resp = new FunctionResponse();

                var values = _unitOfWork.MapTeacherStudentQuizRepository.Get().Where(x => x.QuizId == Id && x.TeacherId == entity[0].TeacherId).ToList();

                if (values.Count < 1)
                {
                    return AssignQuizToStudent(entity);
                }
                else if (values != null)
                {
                    var delete = values.Where(p => !entity.Any(s =>
                      s.StudentId == p.StudentId
                    )).ToList();

                    if (delete.Count > 0)
                    {
                        _unitOfWork.MapTeacherStudentQuizRepository.Delete(x => x.TeacherId == entity[0].TeacherId && x.QuizId == Id && x.StudentId == delete[0].StudentId);
                    }

                    var insert = entity.Where(p => !values.Any(s =>
                      s.StudentId == p.StudentId
                    )).ToList();

                    if (insert.Count > 0)
                    {
                        foreach (var item in insert)
                        {
                            var quiz = new tbl_MapTeacherStudentQuiz
                            {
                                TeacherId = item.TeacherId,
                                StudentId = item.StudentId,
                                QuizId = item.QuizId,
                                Attempted = item.Attempted == null ? false : item.Attempted,
                                IsQuit = item.IsQuit == null ? null : item.IsQuit,
                                IsTimeUp = item.IsTimeUp == null ? null : item.IsTimeUp,

                                CreatedOn = DateTime.Now,
                                CreatedBy = item.CreatedBy
                            };

                            //Add Master Data First
                            _unitOfWork.MapTeacherStudentQuizRepository.Insert(quiz);
                        }
                    }

                }

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

                return Resp;
            }

            catch (Exception)
            {
                throw;

            }
        }

        public FunctionResponse saveQuizResponse(MapTeacherStudentQuizEntity entity)
        {
            try
            {

                FunctionResponse RMsg = new FunctionResponse();
                if (entity != null)
                {
                    var mapping = new tbl_MapTeacherStudentQuiz
                    {
                        TeacherId = entity.TeacherId,
                        StudentId = entity.StudentId,
                        QuizId = entity.QuizId,
                        Attempted = entity.Attempted,
                        IsTimeUp = entity.IsTimeUp,
                        IsQuit = entity.IsQuit,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime,
                        UpdatedBy = entity.StudentId,
                        UpdatedOn = DateTime.Now
                    };

                    _unitOfWork.MapTeacherStudentQuizRepository.Update(mapping);

                    //Add Role To User......................

                    if (entity.quizResponses.Count > 0)
                    {
                        foreach (var item in entity.quizResponses)
                        {
                            var response = new tbl_QuizResponse
                            {
                                QuizId = entity.QuizId,
                                QuizDetailsId = item.QuizDetailsId,
                                StudentId = item.StudentId,
                                SelectedAns = item.SelectedAns,
                                AnsStatus = item.AnsStatus,
                                CreatedOn = DateTime.Now,
                                CreatedBy = item.StudentId
                            };

                            _unitOfWork.QuizResponseRepository.Insert(response);
                        }
                    }
                    else
                    {
                        RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                        RMsg.Message = "Please Provide Role Type";
                    }

                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        //RMsg.Data.Add(user.Id);                        
                    }
                    else
                    {
                        RMsg.Message = "No_Record_Found";
                        RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    }
                }
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }

        //public FunctionResponse GetAll(Guid TeacherId)
        //{
        //    try
        //    {
        //        FunctionResponse RMsg = new FunctionResponse();
        //      var data = _unitOfWork.MapTeacherStudentQuizRepository.GetAll().Where(x => x.Attempted == true && x.TeacherId == TeacherId).ToList();
        //        if (data != null)
        //        {
        //            var mapdata = Mapper.Map<List<tbl_MapTeacherStudentQuiz>, List<MapTeacherStudentQuizEntity>>(data);
        //            RMsg.Message = "Success";
        //            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
        //            RMsg.Data.Add(data);
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
    }
}
