using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel.UnitOfWork;

namespace BusinessServices.Repository
{
    public class DashboardRepository : IDashboardInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public DashboardRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       

        #region Student
        public FunctionResponse StudentDashboard(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                IEnumerable<Student_DashboardEntity> progress = StudentProgress(Id);
                IEnumerable<Student_GetPercentageTopicWiseEntity> Per_TopicWise = Student_GetPercentageTopicWise(Id);

                if (progress != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(progress);
                }

                if (Per_TopicWise != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(Per_TopicWise);
                }

                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IEnumerable<Student_DashboardEntity> StudentProgress(Guid Id)
        {
            string procName = "Student_Dashboard";
            var data = _unitOfWork.ExecuteReader<Student_DashboardEntity>(string.Format("{0} '{1}' ", procName, Id));
            return data;
        }
        private IEnumerable<Student_GetPercentageTopicWiseEntity> Student_GetPercentageTopicWise(Guid Id)
        {
            string procName = "Student_GetPercentageTopicWise";
            var data = _unitOfWork.ExecuteReader<Student_GetPercentageTopicWiseEntity>(string.Format("{0} '{1}' ", procName, Id));
            return data;
        }

        //private IEnumerable<DashboardStudentEntity> StudentProgress(Guid Id)
        //{
        //    string procName = "Student_Dashboard";
        //    var data = _unitOfWork.ExecuteReader<DashboardStudentEntity>(string.Format("{0} '{1}' ", procName, Id));
        //    return data;
        //}
        //private IEnumerable<TopicWisePercentage> Student_GetPercentageTopicWise(Guid Id)
        //{
        //    string procName = "Student_GetPercentageTopicWise";
        //    var data = _unitOfWork.ExecuteReader<TopicWisePercentage>(string.Format("{0} '{1}' ", procName, Id));
        //    return data;
        //}

        #endregion
        
        #region Teacher
        public FunctionResponse TeacherDashboard(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                //dashboard progress
                IEnumerable<Teacher_DashboardEntity> Levels = TeacherDash(Id);
                //recent quiz
                IEnumerable<Teacher_RecentStudentQuizEntity> RecentQuiz = Recent_Quiz(Id);
                //Top 5 student
                IEnumerable<Teacher_GetTop5StudentEntity> top5stu = GetTop5Student(Id);
                //Top 5 student
                IEnumerable<Teacher_GetChildListWithData> childListWithData = GetChildListWithData(Id);

                if (Levels != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(Levels);                   
                }
                if (RecentQuiz != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(RecentQuiz);
                }
                if (top5stu != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(top5stu);
                }
                if (childListWithData != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(childListWithData);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IEnumerable<Teacher_RecentStudentQuizEntity> Recent_Quiz(Guid Id)
        {
            string progress = "Teacher_RecentStudentQuiz";
            var RecentQuiz = _unitOfWork.ExecuteReader<Teacher_RecentStudentQuizEntity>(string.Format("{0} '{1}' ", progress, Id));
            return RecentQuiz;
        }
        private IEnumerable<Teacher_DashboardEntity> TeacherDash(Guid Id)
        {
            string procName = "Teacher_Dashboard";
            var data = _unitOfWork.ExecuteReader<Teacher_DashboardEntity>(string.Format("{0} '{1}' ", procName, Id));
            return data;
        }
        private IEnumerable<Teacher_GetTop5StudentEntity> GetTop5Student(Guid Id)
        {
            string procName = "Teacher_GetTop5Student";
            var data = _unitOfWork.ExecuteReader<Teacher_GetTop5StudentEntity>(string.Format("{0} '{1}' ", procName, Id));
            return data;
        }
        private IEnumerable<Teacher_GetChildListWithData> GetChildListWithData(Guid Id)
        {
            string procName = "Teacher_GetChildListWithData";
            var data = _unitOfWork.ExecuteReader<Teacher_GetChildListWithData>(string.Format("{0} '{1}' ", procName, Id));
            return data;
        }

        #endregion

        #region Parent
        public FunctionResponse ParentDashboard(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                
                IEnumerable<Parent_DashboardEntity> dashboard = Parent_Dashboard(Id);
              
                IEnumerable<Parent_GetPercentageTopicWiseEntity> Progress = ChildProgress(Id);

                if (dashboard != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(dashboard);
                }

                if (Progress != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(Progress);
                }                

                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
      
        private IEnumerable<Parent_DashboardEntity> Parent_Dashboard(Guid ParentId)
        {
            string procName = "Parent_Dashboard";
            var data = _unitOfWork.ExecuteReader<Parent_DashboardEntity>(string.Format("{0} '{1}' ", procName, ParentId));
            return data;
        }
        private IEnumerable<Parent_GetPercentageTopicWiseEntity> ChildProgress(Guid ParentId)
        {
            string procName = "Parent_GetPercentageTopicWise";
            var data = _unitOfWork.ExecuteReader<Parent_GetPercentageTopicWiseEntity>(string.Format("{0} '{1}' ", procName, ParentId));
            return data;
        }
        private IEnumerable<Parent_GetChildListWithData> Parent_GetChildListWithData(Guid Id)
        {
            string procName = "Parent_GetChildListWithData";
            var data = _unitOfWork.ExecuteReader<Parent_GetChildListWithData>(string.Format("{0} '{1}' ", procName, Id));
            return data;
        }
        #endregion
    }
}
