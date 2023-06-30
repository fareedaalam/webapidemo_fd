using BusinessEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DataModel.GenericRepository
{
    public class GenericRepository<TEntity> where TEntity : class
    {

        internal PCM_LearningBuddyEntities Context;
        internal DbSet<TEntity> DbSet;

        public object Mapper { get; private set; }

        public GenericRepository(PCM_LearningBuddyEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }
        #region TeacherChildMapping
        public List<MapTeacherChildEntity> GetTeacherChildList()
        {
            var List = (from map in Context.tbl_MapTeacherChild
                        join teacher in Context.tbl_User on map.TeacherId equals teacher.Id
                        join Child in Context.tbl_User on map.ChildId equals Child.Id

                        select new MapTeacherChildEntity
                        {
                            TeacherId = map.TeacherId,
                            TeacherName = (teacher.FirstName + " " + teacher.MiddleName + " " + teacher.LastName),
                            ChildId = map.ChildId,
                            ChildName = (Child.FirstName + " " + Child.MiddleName + " " + Child.LastName),
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapTeacherChildEntity>();

            return List;
        }
        public List<MapTeacherChildEntity> GetTeacherChildList(Guid _teacherId)
        {
            var List = (from map in Context.tbl_MapTeacherChild
                        join teacher in Context.tbl_User on map.TeacherId equals teacher.Id
                        join Child in Context.tbl_User on map.ChildId equals Child.Id
                        where map.TeacherId == _teacherId

                        select new MapTeacherChildEntity
                        {
                            TeacherId = map.TeacherId,
                            TeacherName = (teacher.FirstName + " " + teacher.MiddleName + " " + teacher.LastName),
                            ChildId = map.ChildId,
                            ChildName = (Child.FirstName + " " + Child.MiddleName + " " + Child.LastName),
                            ImageB64 = Child.ImageB64,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapTeacherChildEntity>();

            return List;

        }

        #endregion
        #region ParentChildMapping
        public List<MapParentChildEntity> GetParentChildList()
        {
            var List = (from map in Context.tbl_MapParentChild
                        join Parent in Context.tbl_User on map.ParentId equals Parent.Id
                        join Child in Context.tbl_User on map.ChildId equals Child.Id

                        select new MapParentChildEntity
                        {
                            ParentId = map.ParentId,
                            ParentName = (Parent.FirstName + " " + Parent.MiddleName + " " + Parent.LastName),
                            ChildId = map.ChildId,
                            ChildName = (Child.FirstName + " " + Child.MiddleName + " " + Child.LastName),
                            EmailVerified = Child.EmailVerified,
                            ParentEmail = Parent.Email,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapParentChildEntity>();

            return List;
        }
        public List<MapParentChildEntity> GetParentChildList(Guid _ParentId)
        {
            var List = (from map in Context.tbl_MapParentChild
                        join Parent in Context.tbl_User on map.ParentId equals Parent.Id
                        join Child in Context.tbl_User on map.ChildId equals Child.Id
                        where map.ParentId == _ParentId

                        select new MapParentChildEntity
                        {
                            ParentId = map.ParentId,
                            ParentName = (Parent.FirstName + " " + Parent.MiddleName + " " + Parent.LastName),
                            ChildId = map.ChildId,
                            ChildName = (Child.FirstName + " " + Child.MiddleName + " " + Child.LastName),
                            EmailVerified = Child.EmailVerified,
                            Email = Parent.Email,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapParentChildEntity>();

            return List;
        }
        #endregion
        #region ParamDetail
        public List<ParamDetailEntity> GetParamDetail()
        {
            var List = (from PrmD in Context.tbl_ParamDetail
                        join PrmM in Context.tbl_ParamMaster on PrmD.ParamID equals PrmM.Id


                        select new ParamDetailEntity
                        {
                            Id = PrmD.Id,
                            Name = PrmD.Name,
                            Code = PrmD.Code,

                            ParamID = PrmD.ParamID,
                            ParamName = PrmM.ParamName,

                            CreatedOn = PrmD.CreatedOn,
                            CreatedBy = PrmD.CreatedBy,
                            UpdatedOn = PrmD.UpdatedOn,
                            UpdatedBy = PrmD.UpdatedBy,
                            IsActive = PrmD.IsActive

                        }).ToList<ParamDetailEntity>();

            return List;
        }
        public List<ParamDetailEntity> GetParamDetail(Guid Id)
        {
            var List = (from PrmD in Context.tbl_ParamDetail
                        join PrmM in Context.tbl_ParamMaster on PrmD.ParamID equals PrmM.Id
                        where PrmD.Id == Id

                        select new ParamDetailEntity
                        {
                            Id = PrmD.Id,
                            Name = PrmD.Name,
                            Code = PrmD.Code,

                            ParamID = PrmD.ParamID,
                            ParamName = PrmM.ParamName,

                            CreatedOn = PrmD.CreatedOn,
                            CreatedBy = PrmD.CreatedBy,
                            UpdatedOn = PrmD.UpdatedOn,
                            UpdatedBy = PrmD.UpdatedBy,
                            IsActive = PrmD.IsActive

                        }).ToList<ParamDetailEntity>();

            return List;
        }
        public List<ParamDetailEntity> GetParamDetail(string GrpName)
        {
            var List = (from PrmD in Context.tbl_ParamDetail
                        join PrmM in Context.tbl_ParamMaster on PrmD.ParamID equals PrmM.Id
                        where PrmM.ParamName.Trim() == GrpName.Trim()

                        select new ParamDetailEntity
                        {
                            Id = PrmD.Id,
                            Name = PrmD.Name,
                            Code = PrmD.Code,
                            ParamID = PrmD.ParamID,
                            ParamName = PrmM.ParamName

                            //CreatedOn = PrmD.CreatedOn,
                            //CreatedBy = PrmD.CreatedBy,
                            //UpdatedOn = PrmD.UpdatedOn,
                            //UpdatedBy = PrmD.UpdatedBy,
                            //IsActive = PrmD.IsActive

                        }).ToList<ParamDetailEntity>();

            return List;
        }
        #endregion
        #region ConceptMapping
        public List<ConceptMappingEntity> GetConcepts()
        {
            var List = (from concept in Context.tbl_ConceptMapping
                        join topic in Context.tbl_Topic on concept.TopicId equals topic.Id
                        join category in Context.tbl_Category_SubTopic on concept.CategoryId equals category.Id

                        select new ConceptMappingEntity
                        {
                            Id = concept.Id,
                            Name = concept.Name,

                            TopicId = concept.TopicId,
                            TopicName = topic.Name,

                            CategoryId = concept.CategoryId,
                            CategoryName = category.Name,

                            Definition = concept.Definition,
                            Example = concept.Example,
                            PointsToRemember = concept.PointsToRemember,

                            CreatedOn = concept.CreatedOn,
                            CreatedBy = concept.CreatedBy,
                            UpdatedOn = concept.UpdatedOn,
                            UpdatedBy = concept.UpdatedBy,
                            IsActive = concept.IsActive

                        }).ToList<ConceptMappingEntity>();

            return List;
        }
        public List<ConceptMappingEntity> GetConceptById(Guid conceptId)
        {
            var List = (from concept in Context.tbl_ConceptMapping
                        join topic in Context.tbl_Topic on concept.TopicId equals topic.Id
                        join category in Context.tbl_Category_SubTopic on concept.CategoryId equals category.Id
                        where concept.Id == conceptId

                        select new ConceptMappingEntity
                        {
                            Id = concept.Id,
                            Name = concept.Name,

                            TopicId = concept.TopicId,
                            TopicName = topic.Name,

                            CategoryId = concept.CategoryId,
                            CategoryName = category.Name,

                            Definition = concept.Definition,
                            Example = concept.Example,
                            PointsToRemember = concept.PointsToRemember,

                            CreatedOn = concept.CreatedOn,
                            CreatedBy = concept.CreatedBy,
                            UpdatedOn = concept.UpdatedOn,
                            UpdatedBy = concept.UpdatedBy,
                            IsActive = concept.IsActive

                        }).ToList<ConceptMappingEntity>();

            return List;
        }
        #endregion
        #region Quiz
        public List<QuizEntity> GetQuiz()
        {
            var List = (from quiz in Context.tbl_Quiz
                        join standard in Context.tbl_Standard on quiz.StandardId equals standard.Id
                        join subject in Context.tbl_Subject on quiz.SubjectId equals subject.Id
                        join topic in Context.tbl_Topic on quiz.TopicId equals topic.Id
                        join subTopic in Context.tbl_SubTopic on quiz.SubTopicId equals subTopic.Id into SubTpcGrp
                        from subTopic in SubTpcGrp.DefaultIfEmpty()
                        join Catsub in Context.tbl_Category_SubTopic on quiz.CategoryId equals Catsub.Id into CatSubTpcGrp
                        from Catsub in CatSubTpcGrp.DefaultIfEmpty()
                        join user in Context.tbl_User on quiz.UserId equals user.Id into UsrGrp
                        from user in UsrGrp.DefaultIfEmpty()

                        select new QuizEntity
                        {
                            Id = quiz.Id,
                            UserId = quiz.UserId,
                            UserName = (user.FirstName + " " + user.MiddleName + " " + user.LastName),

                            StandardId = quiz.StandardId,
                            StandardName = standard.Name,

                            SubjectId = quiz.SubjectId,
                            SubjectName = subject.Name,

                            TopicId = quiz.TopicId,
                            TopicName = topic.Name,

                            SubTopicId = quiz.SubTopicId,
                            SubTopicName = subTopic.Name == null ? "N/A" : subTopic.Name,

                            CategoryId = quiz.CategoryId,
                            CategoryName = Catsub.Name == null ? "N/A" : Catsub.Name,

                            TotalQuestions = quiz.TotalQuestions,

                            Duration = quiz.Duration,

                            CreatedOn = quiz.CreatedOn,
                            CreatedBy = quiz.CreatedBy,
                            UpdatedOn = quiz.UpdatedOn,
                            UpdatedBy = quiz.UpdatedBy,
                            IsActive = quiz.IsActive,


                        }).ToList<QuizEntity>();

            List<QuizEntity> QuizEntityList = new List<QuizEntity>();

            foreach (QuizEntity Q in List)
            {
                var QuizDetailsList = (from quiz in Context.tbl_Quiz
                                       join qd in Context.tbl_QuizDetails on quiz.Id equals qd.QuizId
                                       where qd.QuizId == Q.Id

                                       select new QuizDetailsEntity
                                       {
                                           Id = qd.Id,
                                           QuizId = qd.QuizId,
                                           Question = qd.Question,
                                           CorrectAnswer = qd.CorrectAnswer,
                                           op1 = qd.op1,
                                           op2 = qd.op2,
                                           op3 = qd.op3,
                                           op4 = qd.op4

                                       }).ToList<QuizDetailsEntity>();
                Q.QuizDetails = QuizDetailsList;
                QuizEntityList.Add(Q);
            }

            return QuizEntityList;
        }
        public List<QuizEntity> GetQuizById(Guid QuizId/*, Guid studentId*/)
        {
            var List = (from quiz in Context.tbl_Quiz
                        join standard in Context.tbl_Standard on quiz.StandardId equals standard.Id
                        join subject in Context.tbl_Subject on quiz.SubjectId equals subject.Id
                        join topic in Context.tbl_Topic on quiz.TopicId equals topic.Id
                        join subTopic in Context.tbl_SubTopic on quiz.SubTopicId equals subTopic.Id into SubTpcGrp
                        from subTopic in SubTpcGrp.DefaultIfEmpty()
                        join Catsub in Context.tbl_Category_SubTopic on quiz.CategoryId equals Catsub.Id into CatSubTpcGrp
                        from Catsub in CatSubTpcGrp.DefaultIfEmpty()
                        join user in Context.tbl_User on quiz.UserId equals user.Id into UsrGrp
                        from user in UsrGrp.DefaultIfEmpty()
                        where quiz.Id == QuizId
                        //where quiz.UserId == Id
                        select new QuizEntity
                        {
                            Id = quiz.Id,
                            UserId = quiz.UserId,
                            UserName = (user.FirstName + " " + user.MiddleName + " " + user.LastName),

                            StandardId = quiz.StandardId,
                            StandardName = standard.Name,

                            SubjectId = quiz.SubjectId,
                            SubjectName = subject.Name,

                            TopicId = quiz.TopicId,
                            TopicName = topic.Name,

                            SubTopicId = quiz.SubTopicId,
                            SubTopicName = subTopic.Name == null ? "N/A" : subTopic.Name,

                            CategoryId = quiz.CategoryId,
                            CategoryName = Catsub.Name == null ? "N/A" : Catsub.Name,

                            TotalQuestions = quiz.TotalQuestions,

                            Duration = quiz.Duration,

                            CreatedOn = quiz.CreatedOn,
                            CreatedBy = quiz.CreatedBy,
                            UpdatedOn = quiz.UpdatedOn,
                            UpdatedBy = quiz.UpdatedBy,
                            IsActive = quiz.IsActive,


                        }).ToList<QuizEntity>();

            List<QuizEntity> QuizEntityList = new List<QuizEntity>();

            foreach (QuizEntity Q in List)
            {
                var QuizDetailsList = (from quiz in Context.tbl_Quiz
                                       join qd in Context.tbl_QuizDetails on quiz.Id equals qd.QuizId
                                       where quiz.Id == QuizId

                                       select new QuizDetailsEntity
                                       {
                                           Id = qd.Id,
                                           QuizId = qd.QuizId,
                                           Question = qd.Question,
                                           CorrectAnswer = qd.CorrectAnswer,
                                           op1 = qd.op1,
                                           op2 = qd.op2,
                                           op3 = qd.op3,
                                           op4 = qd.op4

                                       }).ToList<QuizDetailsEntity>();

                Q.QuizDetails = QuizDetailsList;

                QuizEntityList.Add(Q);

            }           

            return QuizEntityList;
        }
        public List<QuizEntity> GetQuizByAnyId(QuizEntity entity)
        {
            var List = (from quiz in Context.tbl_Quiz
                        join standard in Context.tbl_Standard on quiz.StandardId equals standard.Id
                        join subject in Context.tbl_Subject on quiz.SubjectId equals subject.Id
                        join topic in Context.tbl_Topic on quiz.TopicId equals topic.Id
                        join subTopic in Context.tbl_SubTopic on quiz.SubTopicId equals subTopic.Id into SubTpcGrp
                        from subTopic in SubTpcGrp.DefaultIfEmpty()
                        join Catsub in Context.tbl_Category_SubTopic on quiz.CategoryId equals Catsub.Id into CatSubTpcGrp
                        from Catsub in CatSubTpcGrp.DefaultIfEmpty()
                        join user in Context.tbl_User on quiz.UserId equals user.Id into UsrGrp
                        from user in UsrGrp.DefaultIfEmpty()
                        join map in Context.tbl_MapTeacherStudentQuiz on quiz.Id equals map.QuizId into mapTeacStuGrp
                       // from map in mapTeacStuGrp.DefaultIfEmpty()
                            // where quiz.UserId == Id
                        where quiz.Id == (entity.Id == Guid.Empty ? quiz.Id : entity.Id)
                        && quiz.UserId == (entity.UserId == Guid.Empty ? quiz.UserId : entity.UserId)
                        && quiz.StandardId == (entity.StandardId == Guid.Empty ? quiz.StandardId : entity.StandardId)
                        && quiz.SubjectId == (entity.SubjectId == Guid.Empty ? quiz.SubjectId : entity.SubjectId)
                        && quiz.TopicId == (entity.TopicId == Guid.Empty ? quiz.TopicId : entity.TopicId)
                        && quiz.IsActive == (entity.IsActive == null ? true : entity.IsActive)

                        select new QuizEntity
                        {
                            Id = quiz.Id,
                            UserId = quiz.UserId,
                            UserName = (user.FirstName + " " + user.MiddleName + " " + user.LastName),

                            StandardId = quiz.StandardId,
                            StandardName = standard.Name,

                            SubjectId = quiz.SubjectId,
                            SubjectName = subject.Name,

                            TopicId = quiz.TopicId,
                            TopicName = topic.Name,

                            SubTopicId = quiz.SubTopicId,
                            SubTopicName = subTopic.Name == null ? "N/A" : subTopic.Name,

                            CategoryId = quiz.CategoryId,
                            CategoryName = Catsub.Name == null ? "N/A" : Catsub.Name,

                            TotalQuestions = quiz.TotalQuestions,

                            Duration = quiz.Duration,
                            AssignedTo= mapTeacStuGrp.Where(map => map.TeacherId == entity.UserId).Count(),
                            AttempedBy= mapTeacStuGrp.Where(map => map.TeacherId == entity.UserId && map.Attempted==true).Count(),

                            CreatedOn = quiz.CreatedOn,
                            CreatedBy = quiz.CreatedBy,
                            UpdatedOn = quiz.UpdatedOn,
                            UpdatedBy = quiz.UpdatedBy,
                            IsActive = quiz.IsActive

                        }).ToList<QuizEntity>();

            List<QuizEntity> QuizEntityList = new List<QuizEntity>();

            foreach (QuizEntity Q in List)
            {
                var QuizDetailsList = (from quiz in Context.tbl_Quiz
                                       join qd in Context.tbl_QuizDetails on quiz.Id equals qd.QuizId
                                       where qd.QuizId == Q.Id

                                       select new QuizDetailsEntity
                                       {

                                           Id = qd.Id,
                                           QuizId = qd.QuizId,
                                           Question = qd.Question,
                                           CorrectAnswer = qd.CorrectAnswer,
                                           op1 = qd.op1,
                                           op2 = qd.op2,
                                           op3 = qd.op3,
                                           op4 = qd.op4

                                       }).ToList<QuizDetailsEntity>();

                var QuizResponse = (from qd in Context.tbl_QuizDetails
                                    join qr in Context.tbl_QuizResponse on qd.Id equals qr.QuizDetailsId
                                    where qr.QuizId == Q.Id
                                    && qr.StudentId == (entity.UserId == Guid.Empty ? entity.StudentId : Q.UserId)

                                    select new QuizResponseEntity
                                    {
                                        AnsStatus = qr.AnsStatus,
                                        CreatedBy = qr.CreatedBy,
                                        StudentId = qr.StudentId,
                                        QuizDetailsId = qr.QuizDetailsId,
                                        CreatedOn = qr.CreatedOn,
                                        QuizId = qr.QuizId,
                                        SelectedAns = qr.SelectedAns,
                                        UpdatedBy = qr.UpdatedBy,
                                        UpdatedOn = qr.UpdatedOn
                                    }
                                    ).ToList<QuizResponseEntity>();

                if (entity.QuizMapping != null)
                {
                    var quizMapping = (from quiz in Context.tbl_Quiz
                                       join mapping in Context.tbl_MapTeacherStudentQuiz on quiz.Id equals mapping.QuizId into MapGrp
                                       from map in MapGrp.DefaultIfEmpty()
                                       join user in Context.tbl_User on map.StudentId equals user.Id
                                       where quiz.Id == Q.Id
                                       && map.StudentId == entity.QuizMapping.StudentId

                                       select new MapTeacherStudentQuizEntity
                                       {
                                           StudentId = map.StudentId,
                                           Attempted = map.Attempted,
                                           QuizId = map.QuizId,
                                           CreatedBy = map.CreatedBy,
                                           CreatedOn = map.CreatedOn,
                                           StudentName = (user.FirstName + (user.MiddleName != String.Empty ? " " + user.MiddleName + " " : String.Empty) + user.LastName),
                                       }

                    ).FirstOrDefault();

                    Q.QuizMapping = quizMapping;
                }



                Q.quizResponse = QuizResponse;

                Q.QuizDetails = QuizDetailsList;


                QuizEntityList.Add(Q);

            }

            return QuizEntityList;
        }

        #endregion
        #region MapTeacherStudentQuiz
        public List<MapTeacherStudentQuizEntity> GetAssignedStudentList(Guid quizId)
        {
            var List = (from map in Context.tbl_MapTeacherStudentQuiz
                        join student in Context.tbl_User on map.StudentId equals student.Id
                        where map.QuizId == quizId

                        select new MapTeacherStudentQuizEntity
                        {
                            TeacherId = map.TeacherId,
                            QuizId = map.QuizId,
                            StudentId = map.StudentId,
                            StudentName = (student.FirstName + " " + student.MiddleName + " " + student.LastName),
                            Attempted = map.Attempted,
                            IsQuit = map.IsQuit,
                            IsTimeUp = map.IsTimeUp,
                            StartTime = map.StartTime,
                            EndTime = map.EndTime
                        }
                        ).ToList<MapTeacherStudentQuizEntity>();

            return List;
        }
        public List<QuizListModel> GetStudentQuizList(Guid studentId, Guid? teacherId)
        {
            if (teacherId == null)
            {
                var List = (from qz in Context.tbl_Quiz

                            join standard in Context.tbl_Standard on qz.StandardId equals standard.Id

                            join subject in Context.tbl_Subject on qz.SubjectId equals subject.Id

                            join topic in Context.tbl_Topic on qz.TopicId equals topic.Id

                            join subtopic in Context.tbl_SubTopic on qz.SubTopicId equals subtopic.Id into subTopic

                            from st in subTopic.DefaultIfEmpty()

                            join cat in Context.tbl_Category_SubTopic on qz.CategoryId equals cat.Id into Category

                            from c in Category.DefaultIfEmpty()

                            join map in Context.tbl_MapTeacherStudentQuiz on qz.Id equals map.QuizId into mapping

                            from m in mapping.DefaultIfEmpty()

                            join teacher in Context.tbl_User on m.TeacherId equals teacher.Id into Teacher

                            from t in Teacher.DefaultIfEmpty()

                            where qz.UserId == studentId || m.StudentId == studentId
                            && qz.IsActive == true
                            orderby qz.CreatedOn descending


                            select new QuizListModel
                            {
                                Id = qz.Id,
                                UserId = qz.UserId,
                                StandardId = qz.StandardId,
                                StandardName = standard.Name,
                                SubjectId = qz.SubjectId,
                                SubjectName = subject.Name,
                                TopicId = qz.TopicId,
                                TopicName = topic.Name,
                                SubTopicId = qz.SubTopicId,
                                SubTopicName = st.Name,
                                CategoryId = qz.CategoryId,
                                CategoryName = c.Name,
                                //StudentName = student.FirstName + (student.MiddleName != String.Empty ? student.MiddleName : String.Empty) + student.LastName,
                                TeacherId = m.TeacherId,
                                Attempted = m.Attempted,
                                StudentId = m.StudentId,
                                TeacherName = t.FirstName + (t.MiddleName != String.Empty ? t.MiddleName : String.Empty) + t.LastName,
                                CreatedOn = (DateTime)(m.CreatedOn != null ? m.CreatedOn : qz.CreatedOn)
                            }
                        ).ToList<QuizListModel>();

                foreach (QuizListModel quiz in List)
                {
                    quiz.Date = ((DateTime)quiz.CreatedOn).ToString("MM/dd/yyyy");
                }

                return List;
            }
            else
            {
                var List = (from qz in Context.tbl_Quiz

                            join standard in Context.tbl_Standard on qz.StandardId equals standard.Id

                            join subject in Context.tbl_Subject on qz.SubjectId equals subject.Id

                            join topic in Context.tbl_Topic on qz.TopicId equals topic.Id

                            join subtopic in Context.tbl_SubTopic on qz.SubTopicId equals subtopic.Id into subTopic

                            from st in subTopic.DefaultIfEmpty()

                            join cat in Context.tbl_Category_SubTopic on qz.CategoryId equals cat.Id into Category

                            from c in Category.DefaultIfEmpty()

                            join map in Context.tbl_MapTeacherStudentQuiz on qz.Id equals map.QuizId into mapping

                            from m in mapping.DefaultIfEmpty()

                            join teacher in Context.tbl_User on m.TeacherId equals teacher.Id into Teacher

                            from t in Teacher.DefaultIfEmpty()

                            where m.StudentId == studentId && m.TeacherId == teacherId
                            orderby qz.CreatedOn descending

                            select new QuizListModel
                            {
                                Id = qz.Id,
                                UserId = qz.UserId,
                                StandardId = qz.StandardId,
                                StandardName = standard.Name,
                                SubjectId = qz.SubjectId,
                                SubjectName = subject.Name,
                                TopicId = qz.TopicId,
                                TopicName = topic.Name,
                                SubTopicId = qz.SubTopicId,
                                SubTopicName = st.Name,
                                CategoryId = qz.CategoryId,
                                CategoryName = c.Name,
                                //StudentName = student.FirstName + (student.MiddleName != String.Empty ? student.MiddleName : String.Empty) + student.LastName,
                                TeacherId = m.TeacherId,
                                Attempted = m.Attempted,
                                StudentId = m.StudentId,
                                TeacherName = t.FirstName + (t.MiddleName != String.Empty ? t.MiddleName : String.Empty) + t.LastName,
                                CreatedOn = (DateTime)(m.CreatedOn != null ? m.CreatedOn : qz.CreatedOn)
                            }
                        ).ToList<QuizListModel>();

                foreach (QuizListModel quiz in List)
                {
                    quiz.Date = ((DateTime)quiz.CreatedOn).ToString("MM/dd/yyyy");
                }

                return List;
            }
        }

        #endregion
        #region Curriculum
        public List<CurriculumEntity> GetCurriculum()
        {
            var List = (from curriculum in Context.tbl_Curriculum
                        join country in Context.tbl_Country on curriculum.CountryId equals country.Id
                        join bord in Context.tbl_Board on curriculum.BoardId equals bord.Id
                        join standard in Context.tbl_Standard on curriculum.StandardId equals standard.Id
                        join sub in Context.tbl_Subject on curriculum.SubjectId equals sub.Id
                        orderby curriculum.Name
                        // join curDetails in Context.tbl_CurriculumDetails on curriculum.Id equals curDetails.CurriculumId
                        //join topic in Context.tbl_Topic on curDetails.TopicId equals topic.Id


                        select new CurriculumEntity
                        {
                            Id = curriculum.Id,
                            Name = curriculum.Name,

                            CountryId = curriculum.CountryId,
                            CountryName = country.Name,

                            BoardId = curriculum.BoardId,
                            BoardName = bord.Name,

                            StandardId = curriculum.StandardId,
                            StandardName = standard.Name,

                            SubjectId = curriculum.SubjectId,
                            SubjectName = sub.Name,

                            //  TopicId = curDetails.TopicId,
                            //TopicName = topic.Name,

                            CreatedOn = curriculum.CreatedOn,
                            CreatedBy = curriculum.CreatedBy,
                            UpdatedOn = curriculum.UpdatedOn,
                            UpdatedBy = curriculum.UpdatedBy,
                            IsActive = curriculum.IsActive,


                        }).ToList<CurriculumEntity>();

            return List;
        }
        public List<CurriculumDetailsEntity> GetCurriculumDetails()
        {
            var List = (from CurrDetails in Context.tbl_CurriculumDetails
                        join curriculum in Context.tbl_Curriculum on CurrDetails.CurriculumId equals curriculum.Id
                        join topic in Context.tbl_Topic on CurrDetails.TopicId equals topic.Id
                        join subTopic in Context.tbl_SubTopic on CurrDetails.SubTopicId equals subTopic.Id
                        join category in Context.tbl_Category_SubTopic on CurrDetails.CategoryId equals category.Id
                        join lvl in Context.tbl_Level on CurrDetails.LevelId equals lvl.Id
                        orderby curriculum.Name, topic.Name, subTopic.Name, category.Name, lvl.Name

                        select new CurriculumDetailsEntity
                        {
                            Id = CurrDetails.Id,

                            CurriculumId = CurrDetails.CurriculumId,
                            CurriculumName = curriculum.Name,

                            TopicId = CurrDetails.TopicId,
                            TopicName = topic.Name,

                            SubTopicId = CurrDetails.SubTopicId,
                            SubTopicName = subTopic.Name,

                            CategoryId = CurrDetails.CategoryId,
                            CategoryName = category.Name,

                            LevelId = CurrDetails.LevelId,
                            LevelName = lvl.Name


                        }).ToList<CurriculumDetailsEntity>();

            return List;
        }
        //public List<CurriculumEntity> GetCurriculumByUser()
        //{
        //    var List = (from curriculum in Context.tbl_Curriculum
        //                join country in Context.tbl_Country on curriculum.CountryId equals country.Id
        //                join bord in Context.tbl_Board on curriculum.BoardId equals bord.Id
        //                join standard in Context.tbl_Standard on curriculum.StandardId equals standard.Id
        //                join sub in Context.tbl_Subject on curriculum.SubjectId equals sub.Id
        //               // join curDetails in Context.tbl_CurriculumDetails on curriculum.Id equals curDetails.CurriculumId into CurDetailGroup
        //                //from CurDetailsList in CurDetailGroup.DefaultIfEmpty()
        //                // join usr in Context.tbl_User on 
        //              //  new { curriculum.CountryId, curriculum.BoardId, curriculum.StandardId } equals new { usr.CountryId, usr.BoardId, usr.StandardId }

        //              //  curriculum.CountryId equals usr.CountryId and
        //              ////  && usr.BoardId equals curriculum.bo
        //                    //curriculum.CountryId equals


        //                select new CurriculumEntity
        //                {
        //                    Id = curriculum.Id,
        //                    Name = curriculum.Name,

        //                    CountryId = curriculum.CountryId,
        //                    CountryName = country.Name,

        //                    BoardId = curriculum.BoardId,
        //                    BoardName = bord.Name,

        //                    StandardId = curriculum.StandardId,
        //                    StandardName = standard.Name,

        //                    SubjectId = curriculum.SubjectId,
        //                    SubjectName = sub.Name,


        //                    //curriculumDetails = CurDetails,

        //                    //  TopicId = curDetails.TopicId,
        //                    //TopicName = topic.Name,



        //                    CreatedOn = curriculum.CreatedOn,
        //                    CreatedBy = curriculum.CreatedBy,
        //                    UpdatedOn = curriculum.UpdatedOn,
        //                    UpdatedBy = curriculum.UpdatedBy,
        //                    IsActive = curriculum.IsActive,


        //                }).ToList<CurriculumEntity>();

        //    return List;
        //}
        #endregion
        #region QuestionPattern
        public List<QuestionPatternEntity> GetQuestionPatternsWithSolutions(Guid categoryId,Guid levelId)
        {
            var List = (from que in Context.tbl_Question_Pattern
                            // join top in Context.tbl_Topic on que.TopicId equals top.Id
                            // join sub in Context.tbl_SubTopic on que.Sub_TopicId equals sub.Id
                        join catsub in Context.tbl_Category_SubTopic on que.Category_SubTopicId equals catsub.Id
                        join lev in Context.tbl_Level on que.LevelId equals lev.Id
                        // join brd in Context.tbl_Board on que.BoardId equals brd.Id
                        //  join subject in Context.tbl_Subject on que.SubjectId equals subject.Id
                        //  join std in Context.tbl_Standard on que.StandardId equals std.Id
                        //join sol in Context.tbl_Solutions on que.Id equals sol.PatternId into solgrp
                        //from sol in solgrp.DefaultIfEmpty()
                        where que.IsActive == true 
                        && que.Category_SubTopicId== (categoryId == Guid.Empty ? que.Category_SubTopicId : categoryId)
                        && que.LevelId == (levelId == Guid.Empty ? que.LevelId : levelId)

                        select new QuestionPatternEntity
                        {
                            Id = que.Id,
                            Code = que.Code,
                            Pattern = que.Pattern,

                            ///  TopicId = top.Id,
                            //  TopicName = top.Name,

                            //   SubTopicId = sub.Id,
                            //  subTopicName = sub.Name,

                            CategorySubTopicId = catsub.Id,
                            categorySubTopicName = catsub.Name,

                            LevelId = lev.Id,
                            LevelName = lev.Name,

                            //   BoardId = brd.Id,
                            //   BoardName = brd.Name,

                            ////   SubjectId = subject.Id,
                            //  SubjectName = subject.Name,

                            //   StandardId = std.Id,
                            //   StandardName = std.Name,


                            //  CreatedOn = que.CreatedOn,
                            ////  CreatedBy = que.CreatedBy,
                            //  UpdatedOn = que.UpdatedOn,
                            //  UpdatedBy = que.UpdatedBy,
                          //  IsActive = que.IsActive,
                            SolutionsList = Context.tbl_Solutions.Where(x=>x.PatternId==que.Id).Select(x => new SolutionsEntity {
                                Id=x.Id,
                                Solution = x.Solution
                            }).ToList()
                            
                        }).ToList<QuestionPatternEntity>();

            //List<QuestionPatternEntity> QuestionPatternList = new List<QuestionPatternEntity>();

            //foreach (QuestionPatternEntity Que in List)
            //{
            //    var SolutionPatternsList = (from question in Context.tbl_Question_Pattern
            //                                join solution in Context.tbl_Solutions on question.Id equals solution.PatternId
            //                                where solution.PatternId == Que.Id

            //                                select new SolutionsEntity
            //                                {
            //                                    Id = solution.Id,
            //                                    PatternId = question.Id,
            //                                    Solution = solution.Solution,
            //                                    IsActive = solution.IsActive,
            //                                   // ApprovedBy = solution.ApprovedBy,
            //                                  //  IsApproved = solution.IsApproved,
            //                                   // ImplementedBy = solution.ImplementedBy,
            //                                   // IsImplemented = solution.IsImplemented,
            //                                   // CreatedBy = solution.CreatedBy,
            //                                   // CreatedOn = solution.CreatedOn,
            //                                   // UpdatedBy = solution.UpdatedBy,
            //                                   // UpdatedOn = solution.UpdatedOn
            //                                }).ToList<SolutionsEntity>();

            //    Que.SolutionsList = SolutionPatternsList;

            //    QuestionPatternList.Add(Que);
            //}

            return List;
        }

        public List<QuestionPatternEntity> GetQuestionPattern()
        {
            var List = (from que in Context.tbl_Question_Pattern
                        join top in Context.tbl_Topic on que.TopicId equals top.Id
                        join sub in Context.tbl_SubTopic on que.Sub_TopicId equals sub.Id
                        join catsub in Context.tbl_Category_SubTopic on que.Category_SubTopicId equals catsub.Id
                        join lev in Context.tbl_Level on que.LevelId equals lev.Id
                        //   join brd in Context.tbl_Board on que.BoardId equals brd.Id
                        join subject in Context.tbl_Subject on que.SubjectId equals subject.Id
                        //  join std in Context.tbl_Standard on que.StandardId equals std.Id

                        select new QuestionPatternEntity
                        {

                            Id = que.Id,
                            Code = que.Code,
                            Pattern = que.Pattern,

                            TopicId = top.Id,
                            TopicName = top.Name,

                            SubTopicId = sub.Id,
                            subTopicName = sub.Name,

                            CategorySubTopicId = catsub.Id,
                            categorySubTopicName = catsub.Name,

                            LevelId = lev.Id,
                            LevelName = lev.Name,

                            // BoardId = brd.Id,
                            // BoardName = brd.Name,

                            SubjectId = subject.Id,
                            SubjectName = subject.Name,

                            // StandardId = std.Id,
                            // StandardName = std.Name,


                            CreatedOn = que.CreatedOn,
                            CreatedBy = que.CreatedBy,
                            UpdatedOn = que.UpdatedOn,
                            UpdatedBy = que.UpdatedBy,
                            IsActive = que.IsActive

                        }).Distinct().ToList<QuestionPatternEntity>();

            return List;
        }
        public List<QuestionPatternEntity> GetquestionById(Guid queid)
        {
            var List = (from que in Context.tbl_Question_Pattern
                        join top in Context.tbl_Topic on que.TopicId equals top.Id
                        join sub in Context.tbl_SubTopic on que.Sub_TopicId equals sub.Id
                        join catsub in Context.tbl_Category_SubTopic on que.Category_SubTopicId equals catsub.Id
                        join lev in Context.tbl_Level on que.LevelId equals lev.Id
                        //   join brd in Context.tbl_Board on que.BoardId equals brd.Id
                        join subject in Context.tbl_Subject on que.SubjectId equals subject.Id
                        //  join std in Context.tbl_Standard on que.StandardId equals std.Id
                        where que.Id == queid
                        select new QuestionPatternEntity
                        {

                            Id = que.Id,
                            Code = que.Code,
                            Pattern = que.Pattern,

                            TopicId = top.Id,
                            TopicName = top.Name,

                            SubTopicId = sub.Id,
                            subTopicName = sub.Name,

                            CategorySubTopicId = catsub.Id,
                            categorySubTopicName = catsub.Name,

                            LevelId = lev.Id,
                            LevelName = lev.Name,

                            //   BoardId = brd.Id,
                            //  BoardName = brd.Name,

                            SubjectId = subject.Id,
                            SubjectName = subject.Name,

                            //  StandardId = std.Id,
                            //  StandardName = std.Name,



                            CreatedOn = que.CreatedOn,
                            CreatedBy = que.CreatedBy,
                            UpdatedOn = que.UpdatedOn,
                            UpdatedBy = que.UpdatedBy,
                            IsActive = que.IsActive

                        }).ToList<QuestionPatternEntity>();

            return List;
        }
        #endregion      
        #region "School"

        public List<RoleEntity> GetRoles()
        {
            var RolesList=(from rls in Context.tbl_Role
                           where rls.Name=="Student" && rls.Name=="Teacher"
                           select new RoleEntity
                           {
                               Id = rls.Id,
                               Name = rls.Name
                            }).Distinct().ToList<RoleEntity>();

            return RolesList;
        }
        public List<SchoolEntity> GetAllSchool()
        {
            var List = (from sch in Context.tbl_School
                        join cty in Context.tbl_Country on sch.CountryId equals cty.Id
                        join st in Context.tbl_State on sch.StateId equals st.Id

                        select new SchoolEntity
                        {

                            Id = sch.Id,
                            Code = sch.Code,
                            Name = sch.Name,
                            Email = sch.Email,
                            Fax = sch.Fax,
                            ContPerson = sch.ContPerson,
                            ContNumber = sch.ContNumber,
                            CountryId = cty.Id,
                            CountryName = cty.Name,
                            StateId = st.Id,
                            StateName = st.Name,
                            PinCode = sch.PinCode,
                            Remarks = sch.Remarks,
                            IsActive = sch.IsActive,
                            CreatedOn = sch.CreatedOn,
                            CreatedBy = sch.CreatedBy,
                            UpdatedOn = sch.UpdatedOn,
                            UpdatedBy = sch.UpdatedBy


                        }).Distinct().ToList<SchoolEntity>();

            return List;
        }

        public List<SchoolEntity> GetschoolById(Guid schId)
        {
            var List = (from sch in Context.tbl_School
                        join cty in Context.tbl_Country on sch.CountryId equals cty.Id
                        join st in Context.tbl_State on sch.StateId equals st.Id

                        where sch.Id == schId
                        select new SchoolEntity
                        {

                            Id = sch.Id,
                            Code = sch.Code,
                            Name = sch.Name,
                            Email = sch.Email,
                            Fax = sch.Fax,
                            ContPerson = sch.ContPerson,
                            ContNumber = sch.ContNumber,
                            CountryId = cty.Id,
                            CountryName = cty.Name,
                            StateId = st.Id,
                            StateName = st.Name,
                            PinCode = sch.PinCode,
                            Remarks = sch.Remarks,
                            IsActive = sch.IsActive,
                            CreatedOn = sch.CreatedOn,
                            CreatedBy = sch.CreatedBy,
                            UpdatedOn = sch.UpdatedOn,
                            UpdatedBy = sch.UpdatedBy

                        }).ToList<SchoolEntity>();

            return List;
        }

        public List<SchoolEntity> GetSchool_Students(Guid schId)
        {
            var SchoolList = (from sch in Context.tbl_School
                              join cty in Context.tbl_Country on sch.CountryId equals cty.Id
                              join st in Context.tbl_State on sch.StateId equals st.Id
                              join usr in Context.tbl_User on sch.Id equals usr.SchoolId into usrgrp
                              from usr in usrgrp.DefaultIfEmpty()


                              select new SchoolEntity
                              {
                                  Id = sch.Id,
                                  Code = sch.Code,
                                  Name = sch.Name,
                                  Email = sch.Email,
                                  Fax = sch.Fax,
                                  ContPerson = sch.ContPerson,
                                  ContNumber = sch.ContNumber,
                                  CountryId = cty.Id,
                                  CountryName = cty.Name,
                                  StateId = st.Id,
                                  StateName = st.Name,
                                  PinCode = sch.PinCode,
                                  Remarks = sch.Remarks,
                                  IsActive = sch.IsActive,
                                  CreatedOn = sch.CreatedOn,
                                  CreatedBy = sch.CreatedBy,
                                  UpdatedOn = sch.UpdatedOn,
                                  UpdatedBy = sch.UpdatedBy
                              }).Distinct().ToList<SchoolEntity>();

            List<UserEntity> studentList = new List<UserEntity>();

            foreach (SchoolEntity Que in SchoolList)
            {
                var studentusersList = (from sch in Context.tbl_School
                                        join usr in Context.tbl_User on sch.Id equals usr.SchoolId
                                        join std in Context.tbl_Standard on usr.StandardId equals std.Id
                                        join rolemap in Context.tbl_Map_Role_User on usr.Id equals rolemap.UserId
                                        join rol in Context.tbl_Role on rolemap.RoleId equals rol.Id

                                        where (usr.SchoolId == Que.Id && rol.Name == "Student")

                                        select new UserEntity
                                        {

                                            Id = usr.Id,
                                            FirstName = usr.FirstName,
                                            MiddleName = usr.MiddleName,
                                            LastName = usr.LastName,
                                            Email = usr.Email,
                                            Address1 = usr.Address1,
                                            Address2 = usr.Address2,
                                            Mobile = usr.Mobile,
                                            LoginId = usr.LoginId,
                                            Pwd = usr.Pwd,
                                            StandardId = usr.StandardId,
                                            Standard = std.Name,
                                            PinCode = usr.PinCode,
                                            CountryId = usr.CountryId,
                                            IsActive = sch.IsActive,
                                            CreatedOn = sch.CreatedOn,
                                            CreatedBy = sch.CreatedBy,
                                            UpdatedOn = sch.UpdatedOn,
                                            UpdatedBy = sch.UpdatedBy

                                        }).ToList<UserEntity>();

                Que.SchlUser = studentusersList;

                SchoolList.Add(Que);
            }

            return SchoolList;
        }

        public List<MapUserSectionEntity> GetUserSectionList()
        {
            var UserSectionList = (from sec in Context.tbl_Map_User_Section
                                       //join std in Context.tbl_Standard on sec.StandardId equals std.Id
                                       //join sub in Context.tbl_Subject on sec.SubjectId equals sub.Id
                                   join param in Context.tbl_ParamDetail on sec.SectionId equals param.Id


                                   select new MapUserSectionEntity
                                   {
                                       Id = sec.Id,
                                       SubjectId = sec.SubjectId,
                                       UserId = sec.UserId,
                                       //SubjectName = sub.Name,
                                       StandardId = sec.StandardId,

                                       //StandardName = std.Name,
                                       SectionId = sec.SectionId,
                                       SectionName = param.Name,
                                       IsActive = sec.IsActive,
                                       CreatedOn = sec.CreatedOn,
                                       CreatedBy = sec.CreatedBy,
                                       UpdatedOn = sec.UpdatedOn,
                                       UpdatedBy = sec.UpdatedBy
                                   }).Distinct().ToList<MapUserSectionEntity>();



            return UserSectionList;
        }

        public List<MapUserSectionEntity> GetUserSectionList(Guid userId)
        {
            var UserSectionList = (from sec in Context.tbl_Map_User_Section
                                       //join std in Context.tbl_Standard on sec.StandardId equals std.Id
                                       //join sub in Context.tbl_Subject on sec.SubjectId equals sub.Id
                                   join param in Context.tbl_ParamDetail on sec.SectionId equals param.Id
                                   where sec.UserId == userId

                                   select new MapUserSectionEntity
                                   {
                                       Id = sec.Id,
                                       SubjectId = sec.SubjectId,
                                       UserId = sec.UserId,
                                       StandardId = sec.StandardId,
                                       //StandardName = std.Name,
                                       SectionId = sec.SectionId,
                                       SectionName = param.Name,
                                       IsActive = sec.IsActive,
                                       CreatedOn = sec.CreatedOn,
                                       CreatedBy = sec.CreatedBy,
                                       UpdatedOn = sec.UpdatedOn,
                                       UpdatedBy = sec.UpdatedBy
                                   }).Distinct().ToList<MapUserSectionEntity>();



            return UserSectionList;
        }

        public List<UserDetailsEntity> GetUserDetailsListByUser(Guid userId)
        {
            var UserDetailsList = (from sec in Context.tbl_UserDetails
                                       //join std in Context.tbl_Standard on sec.StandardId equals std.Id
                                   join sub in Context.tbl_Subject on sec.SubjectId equals sub.Id
                                   where sec.UserId == userId


                                   select new UserDetailsEntity
                                   {
                                       Id = sec.Id,
                                       SubjectId = sec.SubjectId,
                                       SubjectName = sub.Name,

                                       UserId = sec.UserId,

                                       IsActive = sec.IsActive,
                                       CreatedOn = sec.CreatedOn,
                                       CreatedBy = sec.CreatedBy,
                                       UpdatedOn = sec.UpdatedOn,
                                       UpdatedBy = sec.UpdatedBy
                                   }).Distinct().ToList<UserDetailsEntity>();



            return UserDetailsList;
        }

        public List<UserDetailsEntity> GetUserDetailsList()
        {
            var UserDetailsList = (from sec in Context.tbl_UserDetails
                                       //join std in Context.tbl_Standard on sec.StandardId equals std.Id
                                   join sub in Context.tbl_Subject on sec.SubjectId equals sub.Id



                                   select new UserDetailsEntity
                                   {
                                       Id = sec.Id,
                                       SubjectId = sec.SubjectId,
                                       UserId = sec.UserId,
                                       SubjectName = sub.Name,
                                       IsActive = sec.IsActive,
                                       CreatedOn = sec.CreatedOn,
                                       CreatedBy = sec.CreatedBy,
                                       UpdatedOn = sec.UpdatedOn,
                                       UpdatedBy = sec.UpdatedBy
                                   }).Distinct().ToList<UserDetailsEntity>();



            return UserDetailsList;
        }

        public List<UserDetailsEntity> GetUserDetailsList(Guid Id)
        {
            var UserDetailsList = (from sec in Context.tbl_UserDetails
                                       //join std in Context.tbl_Standard on sec.StandardId equals std.Id
                                   join sub in Context.tbl_Subject on sec.SubjectId equals sub.Id
                                   where sec.Id == Id


                                   select new UserDetailsEntity
                                   {
                                       Id = sec.Id,
                                       SubjectId = sec.SubjectId,
                                       UserId = sec.UserId,
                                       SubjectName = sub.Name,
                                       IsActive = sec.IsActive,
                                       CreatedOn = sec.CreatedOn,
                                       CreatedBy = sec.CreatedBy,
                                       UpdatedOn = sec.UpdatedOn,
                                       UpdatedBy = sec.UpdatedBy
                                   }).Distinct().ToList<UserDetailsEntity>();



            return UserDetailsList;
        }

        #endregion
        #region User
        public bool DeActivateUser(Guid userId, tbl_User userData)
        {
            try
            {
                var user = new tbl_User()
                {
                    Id = userId,
                    IsActive = userData.IsActive,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    Email = userData.Email,
                    Mobile = userData.Mobile,
                    Pwd = userData.Pwd
                };
                //Context.tbl_User.Attach(user);

                using (var db = new PCM_LearningBuddyEntities())
                {
                    db.tbl_User.Attach(user);
                    db.Entry(user).Property(x => x.IsActive).IsModified = true;
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public List<UserEntity> GetUser()
        {
            var List = (from user in Context.tbl_User
                        join mapRole in Context.tbl_Map_Role_User on user.Id equals mapRole.UserId
                        join role in Context.tbl_Role on mapRole.RoleId equals role.Id
                        orderby user.CreatedOn descending
                        select new UserEntity
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Address1 = user.Address1,
                            Mobile = user.Mobile,
                            Address2 = user.Address2,
                            IsActive = user.IsActive,
                            CreatedOn = user.CreatedOn,
                            UpdatedOn = user.UpdatedOn,
                            CreatedBy = user.CreatedBy,
                            UpdatedBy = user.UpdatedBy,
                            LoginId = user.LoginId,
                            Pwd = user.Pwd,
                            StandardId = user.StandardId,
                            Qualification = user.Qualification,
                            CountryId = user.CountryId,
                            StateId = user.StateId,
                            CityId = user.CityId,
                            LocationId = user.LocationId,
                            FatherName = user.FatherName,
                            MotherName = user.MotherName,
                            AlternatePhone = user.AlternatePhone,
                            AlternateEmail = user.AlternateEmail,
                            EmailVerified = user.EmailVerified,
                            Occupation = user.Occupation,
                            State = user.State,
                            City = user.City,
                            Location = user.Location,
                            PinCode = user.PinCode,
                            Subjects = user.Subjects,
                            RoleId = mapRole.RoleId,
                            RoleName = role.Name,
                            BoardId = user.BoardId,
                            ImageB64 = user.ImageB64,
                            ChildAllowed=user.ChildAllowed


                        }).ToList<UserEntity>();

            return List;
        }
        //public List<UserEntity> GetUserByUserId(Guid usrid)
        //{
        //    var List = (from user in Context.tbl_User
        //                join mapRole in Context.tbl_Map_Role_User on user.Id equals mapRole.UserId
        //                join role in Context.tbl_Role on mapRole.RoleId equals role.Id
        //                where user.Id == usrid
        //                select new UserEntity
        //                {
        //                    Id = user.Id,
        //                    FirstName = user.FirstName,
        //                    MiddleName = user.MiddleName,
        //                    LastName = user.LastName,
        //                    Email = user.Email,
        //                    Address1 = user.Address1,
        //                    Mobile = user.Mobile,
        //                    Address2 = user.Address2,
        //                    IsActive = user.IsActive,
        //                    CreatedOn = user.CreatedOn,
        //                    UpdatedOn = user.UpdatedOn,
        //                    CreatedBy = user.CreatedBy,
        //                    UpdatedBy = user.UpdatedBy,
        //                    LoginId = user.LoginId,
        //                   // Pwd = user.Pwd,
        //                    StandardId = user.StandardId,
        //                    Qualification = user.Qualification,
        //                    CountryId = user.CountryId,
        //                    StateId = user.StateId,
        //                    CityId = user.CityId,
        //                    LocationId = user.LocationId,
        //                    FatherName = user.FatherName,
        //                    MotherName = user.MotherName,
        //                    AlternatePhone = user.AlternatePhone,
        //                    AlternateEmail = user.AlternateEmail,
        //                    EmailVerified = user.EmailVerified,
        //                    Occupation = user.Occupation,
        //                    State = user.State,
        //                    City = user.City,
        //                    Location = user.Location,
        //                    PinCode = user.PinCode,
        //                    Subjects = user.Subjects,
        //                    RoleId = mapRole.RoleId,
        //                    RoleName = role.Name,
        //                    BoardId = user.BoardId,
        //                    ImageB64 = user.ImageB64


        //                }).ToList<UserEntity>();

        //    return List;
        //}

        #endregion       
        #region Topic
        public List<TopicEntity> GetTopic()
        {
            var List = (from topic in Context.tbl_Topic
                        join subject in Context.tbl_Subject on topic.SubjectId equals subject.Id
                        select new TopicEntity
                        {
                            Id = topic.Id,
                            Name = topic.Name,
                            SubjectId = topic.SubjectId,
                            SubjectName = subject.Name,
                            CreatedOn = topic.CreatedOn,
                            CreatedBy = topic.CreatedBy,
                            UpdatedOn = topic.UpdatedOn,
                            UpdatedBy = topic.UpdatedBy,
                            IsActive = topic.IsActive

                        }).ToList<TopicEntity>();

            return List;
        }

        public List<TopicEntity> GetTopicById(Guid topicId)
        {
            var List = (from topic in Context.tbl_Topic
                        join subject in Context.tbl_Subject on topic.SubjectId equals subject.Id
                        where topic.Id == topicId
                        select new TopicEntity
                        {
                            Id = topic.Id,
                            Name = topic.Name,
                            SubjectId = topic.SubjectId,
                            SubjectName = subject.Name,
                            CreatedOn = topic.CreatedOn,
                            CreatedBy = topic.CreatedBy,
                            UpdatedOn = topic.UpdatedOn,
                            UpdatedBy = topic.UpdatedBy,
                            IsActive = topic.IsActive

                        }).ToList<TopicEntity>();

            return List;
        }
        #endregion      
        #region Subtopic
        public List<SubTopicEntity> GetSubTopic()
        {
            var List = (from subtopic in Context.tbl_SubTopic
                        join topic in Context.tbl_Topic on subtopic.TopicId equals topic.Id
                        join subject in Context.tbl_Subject on topic.SubjectId equals subject.Id
                        select new SubTopicEntity
                        {
                            Id = subtopic.Id,
                            Name = subtopic.Name,
                            Code = subtopic.Code,

                            SubjectId = topic.SubjectId,
                            SubjectName = subject.Name,

                            TopicId = subtopic.TopicId,
                            TopicName = topic.Name,


                            CreatedBy = subtopic.CreatedBy,
                            CreatedOn = subtopic.CreatedOn,
                            UPdatedOn = subtopic.UPdatedOn,
                            UpdatedBy = subtopic.UpdatedBy,
                            IsActive = subtopic.IsActive

                        }).ToList<SubTopicEntity>();

            return List;
        }
        public List<SubTopicEntity> GetSubTopicById(Guid SubtopicId)
        {
            var List = (from subtopic in Context.tbl_SubTopic
                            //  join category in Context.tbl_Category_SubTopic on subtopic.TypeId equals category.Id
                        join topic in Context.tbl_Topic on subtopic.TopicId equals topic.Id
                        join subject in Context.tbl_Subject on topic.SubjectId equals subject.Id
                        where subtopic.Id == SubtopicId
                        select new SubTopicEntity
                        {
                            Id = subtopic.Id,
                            Name = subtopic.Name,
                            Code = subtopic.Code,
                            SubjectId = topic.SubjectId,
                            SubjectName = subject.Name,
                            TopicId = subtopic.TopicId,
                            TopicName = topic.Name,
                            CreatedOn = subtopic.CreatedOn,
                            CreatedBy = subtopic.CreatedBy,
                            UPdatedOn = subtopic.UPdatedOn,
                            UpdatedBy = subtopic.UpdatedBy,
                            IsActive = subtopic.IsActive

                        }).ToList<SubTopicEntity>();

            return List;
        }

        #endregion
        #region Category
        public List<CategorySubTopicEntity> GetCategory()
        {
            var List = (from category in Context.tbl_Category_SubTopic
                        join subtopic in Context.tbl_SubTopic on category.SubTopicId equals subtopic.Id
                        join topic in Context.tbl_Topic on subtopic.TopicId equals topic.Id
                        join subject in Context.tbl_Subject on topic.SubjectId equals subject.Id
                        select new CategorySubTopicEntity
                        {
                            Id = category.Id,
                            Name = category.Name,
                            Code = category.Code,

                            SubjectId = topic.SubjectId,
                            SubjectName = subject.Name,

                            TopicId = category.TopicId,
                            TopicName = topic.Name,

                            SubTopicId = category.SubTopicId,
                            SubTopicName = subtopic.Name,
                            CreatedOn = category.CreatedOn,
                            CreatedBy = category.CreatedBy,
                            UpdatedOn = category.UpdatedOn,
                            UpdatedBy = category.UpdatedBy,
                            IsActive = category.IsActive

                        }).ToList<CategorySubTopicEntity>();

            return List;
        }
        public List<CategorySubTopicEntity> GetCategoryById(Guid subTopicCategoryId)
        {
            var List = (from category in Context.tbl_Category_SubTopic
                            //join category in Context.tbl_Category_SubTopic on subtopic.TypeId equals category.Id
                        join subtopic in Context.tbl_SubTopic on category.SubTopicId equals subtopic.Id
                        join topic in Context.tbl_Topic on subtopic.TopicId equals topic.Id
                        join subject in Context.tbl_Subject on topic.SubjectId equals subject.Id
                        where category.Id == subTopicCategoryId
                        select new CategorySubTopicEntity
                        {
                            Id = category.Id,
                            Name = category.Name,
                            Code = category.Code,

                            SubjectId = topic.SubjectId,
                            SubjectName = subject.Name,

                            TopicId = category.TopicId,
                            TopicName = topic.Name,

                            SubTopicId = category.SubTopicId,
                            SubTopicName = subtopic.Name,
                            CreatedOn = category.CreatedOn,
                            CreatedBy = category.CreatedBy,
                            UpdatedOn = category.UpdatedOn,
                            UpdatedBy = category.UpdatedBy,
                            IsActive = category.IsActive

                        }).ToList<CategorySubTopicEntity>();

            return List;
        }
        #endregion       
        #region State
        public List<StateEntity> GetState()
        {
            var StateList = (from state in Context.tbl_State
                             join country in Context.tbl_Country on state.CountryId equals country.Id
                             select new StateEntity
                             {
                                 Id = state.Id,
                                 CountryId = state.CountryId,
                                 CountryName = country.Name,
                                 Name = state.Name,
                                 Code = state.Code,
                                 CreatedOn = state.CreatedOn,
                                 UpdatedOn = state.UpdatedOn,
                                 UpdatedBy = state.UpdatedBy,
                                 IsActive = state.IsActive

                             }).ToList<StateEntity>();

            return StateList;
        }
        public List<StateEntity> GetStateById(Guid StateId)
        {
            var StateList = (from state in Context.tbl_State
                             join country in Context.tbl_Country on state.CountryId equals country.Id
                             where state.Id == StateId
                             select new StateEntity
                             {
                                 Id = state.Id,
                                 CountryId = state.CountryId,
                                 CountryName = country.Name,
                                 Name = state.Name,
                                 Code = state.Code,
                                 CreatedOn = state.CreatedOn,
                                 UpdatedOn = state.UpdatedOn,
                                 UpdatedBy = state.UpdatedBy,
                                 IsActive = state.IsActive

                             }).ToList<StateEntity>();

            return StateList;
        }

        public List<StateEntity> GetStateByCountryID(Guid id)
        {
            List<StateEntity> SateEntityList = new List<StateEntity>();

            try
            {
                var state = from tbl_Country in Context.tbl_Country
                            from tbl_State in tbl_Country.tbl_State
                            .Where(x => x.CountryId == id)
                            select new StateEntity
                            {
                                Id = tbl_State.Id,
                                Name = tbl_State.Name
                            };
                foreach (StateEntity n in state)
                {
                    SateEntityList.Add(n);
                }
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
            finally
            {
            }

            return SateEntityList;
        }

        #endregion        
        #region City
        public List<CityEntity> GetCity()
        {
            var List = (from city in Context.tbl_City
                        join state in Context.tbl_State on city.StateId equals state.Id
                        join country in Context.tbl_Country on state.CountryId equals country.Id
                        select new CityEntity
                        {
                            Id = city.Id,
                            Name = city.Name,
                            Code = city.Code,

                            CountryId = state.CountryId,
                            CountryName = country.Name,
                            StateId = city.StateId,
                            StateName = state.Name,

                            CreatedOn = city.CreatedOn,
                            UpdatedOn = city.UpdatedOn,
                            UpdatedBy = city.UpdatedBy,
                            IsActive = city.IsActive

                        }).ToList<CityEntity>();

            return List;
        }

        public List<CityEntity> GetCityById(Guid CityId)
        {
            var List = (from city in Context.tbl_City
                        join state in Context.tbl_State on city.StateId equals state.Id
                        join country in Context.tbl_Country on state.CountryId equals country.Id
                        where city.Id == CityId
                        select new CityEntity
                        {
                            Id = city.Id,
                            Name = city.Name,
                            Code = city.Code,

                            CountryId = state.CountryId,
                            CountryName = country.Name,
                            StateId = city.StateId,
                            StateName = state.Name,

                            CreatedOn = city.CreatedOn,
                            UpdatedOn = city.UpdatedOn,
                            UpdatedBy = city.UpdatedBy,
                            IsActive = city.IsActive

                        }).ToList<CityEntity>();

            return List;
        }
        #endregion City      
        #region Location
        public List<LocationEntity> GetLocation()
        {
            var List = (from loc in Context.tbl_Location
                        join city in Context.tbl_City on loc.CityId equals city.Id
                        join state in Context.tbl_State on city.StateId equals state.Id
                        join country in Context.tbl_Country on state.CountryId equals country.Id
                        select new LocationEntity
                        {

                            Id = loc.Id,
                            Name = loc.Name,
                            Code = loc.Code,

                            CountryId = state.CountryId,
                            CountryName = country.Name,
                            StateId = city.StateId,
                            StateName = state.Name,
                            CityId = city.Id,
                            CityName = city.Name,


                            CreatedOn = loc.CreatedOn,
                            UpdatedOn = loc.UpdatedOn,
                            UpdatedBy = loc.UpdatedBy,
                            IsActive = loc.IsActive

                        }).ToList<LocationEntity>();

            return List;
        }

        public List<LocationEntity> GetLocationById(Guid locId)
        {
            var List = (from loc in Context.tbl_Location
                        join city in Context.tbl_City on loc.CityId equals city.Id
                        join state in Context.tbl_State on city.StateId equals state.Id
                        join country in Context.tbl_Country on state.CountryId equals country.Id
                        where loc.Id == locId
                        select new LocationEntity
                        {

                            Id = loc.Id,
                            Name = loc.Name,
                            Code = loc.Code,

                            CountryId = state.CountryId,
                            CountryName = country.Name,
                            StateId = city.StateId,
                            StateName = state.Name,
                            CityId = city.Id,
                            CityName = city.Name,


                            CreatedOn = loc.CreatedOn,
                            UpdatedOn = loc.UpdatedOn,
                            UpdatedBy = loc.UpdatedBy,
                            IsActive = loc.IsActive

                        }).ToList<LocationEntity>();

            return List;
        }

        #endregion Location
        #region City
        public List<CityEntity> GetCityByStateID(Guid id)
        {
            List<CityEntity> CityEntityList = new List<CityEntity>();

            try
            {
                var city = from tbl_State in Context.tbl_State
                           from tbl_City in tbl_State.tbl_City
                           .Where(x => x.StateId == id)
                           select new CityEntity
                           {
                               Id = tbl_City.Id,
                               Name = tbl_City.Name
                           };
                foreach (CityEntity n in city)
                {
                    CityEntityList.Add(n);
                }
            }

            catch (Exception e)
            {
                throw new Exception("Error populating data, Error :" + e.Message, e);
            }
            finally
            {
            }

            return CityEntityList;
        }
        #endregion
        #region Board
        public List<BoardEntity> GetBoard()
        {
            var List = (from board in Context.tbl_Board
                        join country in Context.tbl_Country on board.CountryId equals country.Id
                        select new BoardEntity
                        {
                            Id = board.Id,
                            Name = board.Name,
                            Code = board.Code,
                            CreatedOn = board.CreatedOn,
                            CreatedBy = board.CreatedBy,
                            UpdatedOn = board.UpdatedOn,
                            UpdatedBy = board.UpdatedBy,
                            IsActive = board.IsActive,
                            CountryId = board.CountryId,
                            CountryName = country.Name
                        }).ToList<BoardEntity>();

            return List;
        }
        public List<BoardEntity> GetBoardById(Guid boardId)
        {
            var List = (from board in Context.tbl_Board
                        join country in Context.tbl_Country on board.CountryId equals country.Id
                        where board.Id == boardId
                        select new BoardEntity
                        {
                            Id = board.Id,
                            Name = board.Name,
                            Code = board.Code,
                            CreatedOn = board.CreatedOn,
                            CreatedBy = board.CreatedBy,
                            UpdatedOn = board.UpdatedOn,
                            UpdatedBy = board.UpdatedBy,
                            IsActive = board.IsActive,
                            CountryId = board.CountryId,
                            CountryName = country.Name
                        }).ToList<BoardEntity>();

            return List;
        }
        #endregion
        #region Standard+Board
        public List<StandardEntity> GetStandard()
        {
            var List = (from standard in Context.tbl_Standard
                        join bord in Context.tbl_Board on standard.BoardId equals bord.Id
                        join country in Context.tbl_Country on bord.CountryId equals country.Id
                        select new StandardEntity
                        {
                            Id = standard.Id,
                            Name = standard.Name,
                            Code = standard.Code,
                            TypeName = standard.TypeName,
                            CreatedOn = standard.CreatedOn,
                            CreatedBy = standard.CreatedBy,
                            UpdatedOn = standard.UpdatedOn,
                            UpdatedBy = standard.UpdatedBy,
                            IsActive = standard.IsActive,
                            CountryId = country.Id,
                            CountryName = country.Name,
                            BoardId = standard.BoardId,
                            BoardName = bord.Name
                        }).ToList<StandardEntity>();

            return List;
        }
        public List<StandardEntity> GetStandardById(Guid Id)
        {
            var List = (from standard in Context.tbl_Standard
                        join bord in Context.tbl_Board on standard.BoardId equals bord.Id
                        join country in Context.tbl_Country on bord.CountryId equals country.Id
                        where standard.Id == Id
                        select new StandardEntity
                        {
                            Id = standard.Id,
                            Name = standard.Name,
                            Code = standard.Code,
                            TypeName = standard.TypeName,
                            CreatedOn = standard.CreatedOn,
                            CreatedBy = standard.CreatedBy,
                            UpdatedOn = standard.UpdatedOn,
                            UpdatedBy = standard.UpdatedBy,
                            IsActive = standard.IsActive,
                            CountryId = country.Id,
                            CountryName = country.Name,
                            BoardId = standard.BoardId,
                            BoardName = bord.Name
                        }).ToList<StandardEntity>();

            return List;
        }
        #endregion
        ////Map RoleUser....................................................
        #region MapRoleUser
        //public int MapRole_User(tbl_Role role, tbl_User user)
        //{
        //    try
        //    {
        //        var firstE = Context.tbl_User
        //                     .FirstOrDefault(x => x.Id == user.Id);

        //        Context.tbl_Role.FirstOrDefault(x => x.Id == role.Id)
        //            .tbl_User.Add(firstE);
        //        //  Context.sts

        //      return Context.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public IEnumerable<MapRoleUserEntity> GetMapRole_User()
        //{


        //    List<MapRoleUserEntity> MapEntityList = new List<MapRoleUserEntity>();
        //    var MapEntity = from tbl_Role in Context.tbl_Role
        //                    from tbl_User in tbl_Role.tbl_User
        //                    select new MapRoleUserEntity
        //                    {
        //                        RoleId = tbl_Role.Id,
        //                        RoleName = tbl_Role.Name,
        //                        UserId = tbl_User.Id,
        //                        UserName = tbl_User.FirstName
        //                    };

        //    foreach (MapRoleUserEntity n in MapEntity)
        //    {
        //        MapEntityList.Add(n);
        //    }

        //    return MapEntityList;

        //}

        //public List<MapRoleUserEntity> GetMapRole_User_ById(Guid id)
        //{
        //    List<MapRoleUserEntity> MapEntityList = new List<MapRoleUserEntity>();
        //    try
        //    {
        //        var MapEntity =
        //                        from tbl_Role in Context.tbl_Role
        //                        from tbl_User in tbl_Role.tbl_User
        //                        .Where(x => x.Id == id)
        //                        select new MapRoleUserEntity
        //                        {
        //                            RoleId = tbl_Role.Id,
        //                            RoleName = tbl_Role.Name,
        //                            UserId = tbl_User.Id,
        //                            UserName = tbl_User.FirstName
        //                        };

        //        foreach (MapRoleUserEntity n in MapEntity)
        //        {
        //            MapEntityList.Add(n);
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Error populating data, Error :" + e.Message, e);
        //    }
        //    finally
        //    {


        //    }

        //    return MapEntityList;

        //}

        //public void Delete_MapRole_User(tbl_Role role, tbl_User user)
        //{

        //    var firstE = Context.tbl_User
        //                 .FirstOrDefault(x => x.Id == user.Id);

        //    Context.tbl_Role.FirstOrDefault(x => x.Id == role.Id)
        //        .tbl_User.Remove(firstE);
        //    Context.SaveChanges();


        //    //EmployeeDBContext employeeDBContext = new EmployeeDBContext();
        //    //Course WCFCourse = employeeDBContext.Courses
        //    //    .FirstOrDefault(x => x.CourseID == 4);

        //    //employeeDBContext.Students.FirstOrDefault(x => x.StudentID == 1)
        //    //    .Courses.Add(WCFCourse);
        //    //employeeDBContext.SaveChanges();


        //}
        //school integration code
        public List<MapSchoolBoardEntity> GetSchoolBoardList()
        {
            var List = (from map in Context.tbl_Map_School_Board
                        join school in Context.tbl_School on map.SchoolId equals school.Id
                        join board in Context.tbl_Board on map.BoardId equals board.Id

                        select new MapSchoolBoardEntity
                        {
                            SchoolId = map.SchoolId,
                            SchoolName = school.Name,
                            BoardId = map.BoardId,
                            BoardName = board.Name,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapSchoolBoardEntity>();

            return List;
        }
        public List<MapSchoolBoardEntity> GetSchoolBoardList(Guid _boardId, Guid _schoolId)
        {
            var List = (from map in Context.tbl_Map_School_Board
                        join school in Context.tbl_School on map.SchoolId equals school.Id
                        join board in Context.tbl_Board on map.BoardId equals board.Id
                        where map.BoardId == _boardId && map.SchoolId == _schoolId

                        select new MapSchoolBoardEntity
                        {
                            SchoolId = map.SchoolId,
                            SchoolName = school.Name,
                            BoardId = map.BoardId,
                            BoardName = board.Name,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapSchoolBoardEntity>();

            return List;

        }
        public List<MapSchoolStandardEntity> GetSchoolStandardList()
        {
            var List = (from map in Context.tbl_Map_School_Standard
                        join school in Context.tbl_School on map.SchoolId equals school.Id
                        join standard in Context.tbl_Standard on map.StandardId equals standard.Id

                        select new MapSchoolStandardEntity
                        {
                            SchoolId = map.SchoolId,
                            SchoolName = school.Name,
                            StandardId = map.StandardId,
                            StandardName = standard.Name,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapSchoolStandardEntity>();

            return List;
        }
        public List<MapSchoolStandardEntity> GetSchoolStandardList(Guid _standardId, Guid _schoolId)
        {
            var List = (from map in Context.tbl_Map_School_Standard
                        join school in Context.tbl_School on map.SchoolId equals school.Id
                        join standard in Context.tbl_Standard on map.StandardId equals standard.Id
                        where map.StandardId == _standardId && map.SchoolId == _schoolId

                        select new MapSchoolStandardEntity
                        {
                            SchoolId = map.SchoolId,
                            SchoolName = school.Name,
                            StandardId = map.StandardId,
                            StandardName = standard.Name,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapSchoolStandardEntity>();

            return List;

        }
        public List<MapSchoolTeacherEntity> GetSchoolTeacherList()
        {
            var List = (from map in Context.tbl_Map_School_Teacher
                        join school in Context.tbl_School on map.SchoolId equals school.Id
                        join teacher in Context.tbl_User on map.TeacherId equals teacher.Id

                        select new MapSchoolTeacherEntity
                        {
                            SchoolId = map.SchoolId,
                            SchoolName = school.Name,
                            TeacherId = map.TeacherId,
                            TeacherName = (teacher.FirstName + " " + teacher.MiddleName + " " + teacher.LastName),
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapSchoolTeacherEntity>();

            return List;
        }
        public List<MapSchoolTeacherEntity> GetSchoolTeacherList(Guid _teacherId, Guid _schoolId)
        {
            var List = (from map in Context.tbl_Map_School_Teacher
                        join school in Context.tbl_School on map.SchoolId equals school.Id
                        join teacher in Context.tbl_User on map.TeacherId equals teacher.Id
                        where map.TeacherId == _teacherId && map.SchoolId == _schoolId

                        select new MapSchoolTeacherEntity
                        {
                            SchoolId = map.SchoolId,
                            SchoolName = school.Name,
                            TeacherId = map.TeacherId,
                            TeacherName = (teacher.FirstName + " " + teacher.MiddleName + " " + teacher.LastName),
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive
                        }).ToList<MapSchoolTeacherEntity>();

            return List;

        }
        public List<MapTeacherStandardEntity> GetTeacherStandardList()
        {
            var List = (from map in Context.tbl_Map_Teacher_Standard
                        join teacher in Context.tbl_User on map.TeacherId equals teacher.Id
                        join standard in Context.tbl_Standard on map.StandardId equals standard.Id

                        select new MapTeacherStandardEntity
                        {
                            TeacherId = map.TeacherId,
                            TeacherName = (teacher.FirstName + " " + teacher.MiddleName + " " + teacher.LastName),
                            StandardId = map.StandardId,
                            StandardName = standard.Name,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapTeacherStandardEntity>();

            return List;
        }
        public List<MapTeacherStandardEntity> GetTeacherStandardList(Guid _standardId, Guid _teacherId)
        {
            var List = (from map in Context.tbl_Map_Teacher_Standard
                        join teacher in Context.tbl_User on map.TeacherId equals teacher.Id
                        join standard in Context.tbl_Standard on map.StandardId equals standard.Id
                        where map.StandardId == _standardId && map.TeacherId == _teacherId
                        select new MapTeacherStandardEntity
                        {
                            TeacherId = map.TeacherId,
                            TeacherName = (teacher.FirstName + " " + teacher.MiddleName + " " + teacher.LastName),
                            StandardId = map.StandardId,
                            StandardName = standard.Name,
                            CreatedOn = map.CreatedOn,
                            CreatedBy = map.CreatedBy,
                            UpdatedOn = map.UpdatedOn,
                            UpdatedBy = map.UpdatedBy,
                            IsActive = map.IsActive

                        }).ToList<MapTeacherStandardEntity>();

            return List;
        }
        #endregion MapRoleUser
        //Main Generic Repository For All..................................
        //--------------------------Genric Section-----------------------------
        #region Generic Section 

        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }
        public virtual TEntity GetByID(object id)
        {
            // var RoleID= from  in DbSet select a orderby a.LastName
            return DbSet.Find(id);
        }
        public virtual TEntity GetRefrenceID(object id)
        {
            return DbSet.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }
        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }
        public IQueryable<TEntity> GetWithInclude(
            System.Linq.Expressions.Expression<Func<TEntity,
            bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }
        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }
        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.SingleOrDefault<TEntity>(predicate);
        }
        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.FirstOrDefault<TEntity>(predicate);
        }
        #endregion  
    }
}
