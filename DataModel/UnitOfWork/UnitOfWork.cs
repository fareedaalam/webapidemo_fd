using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using DataModel.GenericRepository;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Dynamic;

namespace DataModel.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private PCM_LearningBuddyEntities _context = null;

        private GenericRepository<tbl_Mailer> _mailerRepository;
        private GenericRepository<tbl_UrlExpire> _urlExpireRepository;
        private GenericRepository<tbl_QuizResponse> _quizResponseRepository;
        private GenericRepository<tbl_MapTeacherStudentQuiz> _mapTeacherStudentQuizRepository;
        private GenericRepository<tbl_MapTeacherChild> _mapTeacherChildRepository;
        private GenericRepository<tbl_MapParentChild> _mapParentChildRepository;
        private GenericRepository<tbl_ParamMaster> _paramMasterRepository;
        private GenericRepository<tbl_ParamDetail> _paramDetailRepository;

        private GenericRepository<tbl_ConceptMapping> _conceptMappingRepository;
        private GenericRepository<tbl_Quiz> _quizRepository;
        private GenericRepository<tbl_QuizDetails> _quizDetailsRepository;
        private GenericRepository<tbl_Solutions> _solutionsRepository;
        private GenericRepository<tbl_Curriculum> _curriculumRepository;
        private GenericRepository<tbl_CurriculumDetails> _curriculumDetailsRepository;
        private GenericRepository<tbl_Question_Pattern> _questionPatternRepository;
        private GenericRepository<tbl_Standard> _StandardRepository;
        private GenericRepository<tbl_Map_Role_Permission> _MapRolePermission;
        private GenericRepository<tbl_Map_Role_User> _MapRoleUser;
        private GenericRepository<tbl_Location> _locationRepository;
        private GenericRepository<tbl_City> _cityRepository;
        private GenericRepository<tbl_State> _stateRepository;
        private GenericRepository<tbl_Country> _countryRepository;
        private GenericRepository<tbl_Level> _levelRepository;
        private GenericRepository<tbl_Topic> _topicRepository;
        private GenericRepository<tbl_User> _userRepository;
        private GenericRepository<tbl_AccessToken> _tokenRepository;
        private GenericRepository<tbl_Role> _roleRepository;
        private GenericRepository<tbl_Permission> _permissionRepository;
        private GenericRepository<tbl_Subject> _subjectRepository;
        private GenericRepository<tbl_Category> _categoryRepository;
        private GenericRepository<tbl_SubTopic> _subTopicRepository;
        private GenericRepository<tbl_Category_SubTopic> _categorySubTopicRepository;
        private GenericRepository<tbl_UserLog> _userLogRepository;
        private GenericRepository<tbl_Map_Role_User> _mapRoleUserRepository;

        private GenericRepository<tbl_Board> _boardRepository;
        private GenericRepository<tbl_School> _schoolRepository;
        private GenericRepository<tbl_Map_User_Section> _MapUserSectionRepository;
        private GenericRepository<tbl_UserDetails> _UserDetailsRepository;

        //school integration code
        private GenericRepository<tbl_Map_School_Board> _mapSchoolBoardRepository;
        private GenericRepository<tbl_Map_School_Standard> _mapSchoolStandardRepository;
        private GenericRepository<tbl_Map_School_Teacher> _mapSchoolTeacherRepository;
        private GenericRepository<tbl_Map_Teacher_Standard> _mapTeacherStandardRepository;
        private GenericRepository<tbl_Staging> _uploadExcelRepository;

        public UnitOfWork()
        {
            _context = new PCM_LearningBuddyEntities();
        }

        public GenericRepository<tbl_Mailer> MailerRepository
        {
            get
            {
                if (this._mailerRepository == null)
                    this._mailerRepository = new GenericRepository<tbl_Mailer>(_context);
                return _mailerRepository;
            }
        }
        public GenericRepository<tbl_UrlExpire> UrlExpireRepository
        {
            get
            {
                if (this._urlExpireRepository == null)
                    this._urlExpireRepository = new GenericRepository<tbl_UrlExpire>(_context);
                return _urlExpireRepository;
            }
        }

        public GenericRepository<tbl_QuizResponse> QuizResponseRepository
        {
            get
            {
                if (this._quizResponseRepository == null)
                    this._quizResponseRepository = new GenericRepository<tbl_QuizResponse>(_context);
                return _quizResponseRepository;
            }
        }

        public GenericRepository<tbl_MapTeacherStudentQuiz> MapTeacherStudentQuizRepository
        {
            get
            {
                if (this._mapTeacherStudentQuizRepository == null)
                    this._mapTeacherStudentQuizRepository = new GenericRepository<tbl_MapTeacherStudentQuiz>(_context);
                return _mapTeacherStudentQuizRepository;
            }
        }

        public GenericRepository<tbl_School> SchoolRepository
        {
            get
            {
                if (this._schoolRepository == null)
                    this._schoolRepository = new GenericRepository<tbl_School>(_context);
                return _schoolRepository;
            }
        }
        public GenericRepository<tbl_Map_User_Section> MapUserSectionRepository
        {
            get
            {
                if (this._MapUserSectionRepository == null)
                    this._MapUserSectionRepository = new GenericRepository<tbl_Map_User_Section>(_context);
                return _MapUserSectionRepository;
            }
        }
        public GenericRepository<tbl_UserDetails> UserDetailsRepository
        {
            get
            {
                if (this._UserDetailsRepository == null)
                    this._UserDetailsRepository = new GenericRepository<tbl_UserDetails>(_context);
                return _UserDetailsRepository;
            }
        }
        public GenericRepository<tbl_MapParentChild> MapParentChildRepository
        {
            get
            {
                if (this._mapParentChildRepository == null)
                    this._mapParentChildRepository = new GenericRepository<tbl_MapParentChild>(_context);
                return _mapParentChildRepository;
            }
        }
        public GenericRepository<tbl_MapTeacherChild> MapTeacherChildRepository
        {
            get
            {
                if (this._mapTeacherChildRepository == null)
                    this._mapTeacherChildRepository = new GenericRepository<tbl_MapTeacherChild>(_context);
                return _mapTeacherChildRepository;
            }
        }
        public GenericRepository<tbl_ParamMaster> paramMasterRepository
        {
            get
            {
                if (this._paramMasterRepository == null)
                    this._paramMasterRepository = new GenericRepository<tbl_ParamMaster>(_context);
                return _paramMasterRepository;
            }
        }
        public GenericRepository<tbl_ParamDetail> paramDetailRepository
        {
            get
            {
                if (this._paramDetailRepository == null)
                    this._paramDetailRepository = new GenericRepository<tbl_ParamDetail>(_context);
                return _paramDetailRepository;
            }
        }
        public GenericRepository<tbl_ConceptMapping> ConceptMappingRepository
        {
            get
            {
                if (this._conceptMappingRepository == null)
                    this._conceptMappingRepository = new GenericRepository<tbl_ConceptMapping>(_context);
                return _conceptMappingRepository;
            }
        }
        public GenericRepository<tbl_Quiz> QuizRepository
        {
            get
            {
                if (this._quizRepository == null)
                    this._quizRepository = new GenericRepository<tbl_Quiz>(_context);
                return _quizRepository;
            }
        }
        public GenericRepository<tbl_QuizDetails> QuizDetailsRepository
        {
            get
            {
                if (this._quizDetailsRepository == null)
                    this._quizDetailsRepository = new GenericRepository<tbl_QuizDetails>(_context);
                return _quizDetailsRepository;
            }
        }
        public GenericRepository<tbl_Solutions> SolutionsRepository
        {
            get
            {
                if (this._solutionsRepository == null)
                    this._solutionsRepository = new GenericRepository<tbl_Solutions>(_context);
                return _solutionsRepository;
            }
        }
        public GenericRepository<tbl_Curriculum> CurriculumRepository
        {
            get
            {
                if (this._curriculumRepository == null)
                    this._curriculumRepository = new GenericRepository<tbl_Curriculum>(_context);
                return _curriculumRepository;
            }
        }
        public GenericRepository<tbl_CurriculumDetails> CurriculumDetailsRepository
        {
            get
            {
                if (this._curriculumDetailsRepository == null)
                    this._curriculumDetailsRepository = new GenericRepository<tbl_CurriculumDetails>(_context);
                return _curriculumDetailsRepository;
            }
        }
        public GenericRepository<tbl_Question_Pattern> QuestionPatternRepository
        {
            get
            {
                if (this._questionPatternRepository == null)
                    this._questionPatternRepository = new GenericRepository<tbl_Question_Pattern>(_context);
                return _questionPatternRepository;
            }
        }
        public GenericRepository<tbl_Standard> StandardRepository
        {
            get
            {
                if (this._StandardRepository == null)
                    this._StandardRepository = new GenericRepository<tbl_Standard>(_context);
                return _StandardRepository;
            }
        }
        public GenericRepository<tbl_Map_Role_User> MapRoleUser
        {
            get
            {
                if (this._MapRoleUser == null)
                    this._MapRoleUser = new GenericRepository<tbl_Map_Role_User>(_context);
                return _MapRoleUser;
            }
        }
        public GenericRepository<tbl_Map_Role_Permission> MapRolePermission
        {
            get
            {
                if (this._MapRolePermission == null)
                    this._MapRolePermission = new GenericRepository<tbl_Map_Role_Permission>(_context);
                return _MapRolePermission;
            }
        }
        public GenericRepository<tbl_Location> LocationRepository
        {
            get
            {
                if (this._locationRepository == null)
                    this._locationRepository = new GenericRepository<tbl_Location>(_context);
                return _locationRepository;
            }
        }
        public GenericRepository<tbl_City> CityRepository
        {
            get
            {
                if (this._cityRepository == null)
                    this._cityRepository = new GenericRepository<tbl_City>(_context);
                return _cityRepository;
            }
        }
        public GenericRepository<tbl_State> StateRepository
        {
            get
            {
                if (this._stateRepository == null)
                    this._stateRepository = new GenericRepository<tbl_State>(_context);
                return _stateRepository;
            }
        }
        public GenericRepository<tbl_Country> CountryRepository
        {
            get
            {
                if (this._countryRepository == null)
                    this._countryRepository = new GenericRepository<tbl_Country>(_context);
                return _countryRepository;
            }
        }
        public GenericRepository<tbl_Level> LevelRepository
        {
            get
            {
                if (this._levelRepository == null)
                    this._levelRepository = new GenericRepository<tbl_Level>(_context);
                return _levelRepository;
            }
        }
        public GenericRepository<tbl_Topic> TopicRepository
        {
            get
            {
                if (this._topicRepository == null)
                    this._topicRepository = new GenericRepository<tbl_Topic>(_context);
                return _topicRepository;
            }
        }
        public GenericRepository<tbl_User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<tbl_User>(_context);
                return _userRepository;
            }
        }
        public GenericRepository<tbl_Board> BoardRepository
        {
            get
            {
                if (this._boardRepository == null)
                    this._boardRepository = new GenericRepository<tbl_Board>(_context);
                return _boardRepository;
            }
        }
        public GenericRepository<tbl_AccessToken> TokenRepository
        {
            get
            {
                if (this._tokenRepository == null)
                    this._tokenRepository = new GenericRepository<tbl_AccessToken>(_context);
                return _tokenRepository;
            }
        }
        public GenericRepository<tbl_Role> RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                    this._roleRepository = new GenericRepository<tbl_Role>(_context);
                return _roleRepository;
            }
        }
        public GenericRepository<tbl_Permission> PermissionRepository
        {
            get
            {
                if (this._permissionRepository == null)
                    this._permissionRepository = new GenericRepository<tbl_Permission>(_context);
                return _permissionRepository;
            }
        }
        public GenericRepository<tbl_Subject> SubjectRepository
        {
            get
            {
                if (this._subjectRepository == null)
                    this._subjectRepository = new GenericRepository<tbl_Subject>(_context);
                return _subjectRepository;
            }
        }
        public GenericRepository<tbl_Category> CategoryRepository
        {
            get
            {
                if (this._categoryRepository == null)
                    this._categoryRepository = new GenericRepository<tbl_Category>(_context);
                return _categoryRepository;
            }
        }
        public GenericRepository<tbl_Category_SubTopic> CategorySubTopicRepository
        {
            get
            {
                if (this._categorySubTopicRepository == null)
                    this._categorySubTopicRepository = new GenericRepository<tbl_Category_SubTopic>(_context);
                return _categorySubTopicRepository;
            }
        }
        public GenericRepository<tbl_SubTopic> SubTopicRepository
        {
            get
            {
                if (this._subTopicRepository == null)
                    this._subTopicRepository = new GenericRepository<tbl_SubTopic>(_context);
                return _subTopicRepository;
            }
        }
        public GenericRepository<tbl_UserLog> UserLogRepository
        {
            get
            {
                if (this._userLogRepository == null)
                    this._userLogRepository = new GenericRepository<tbl_UserLog>(_context);
                return _userLogRepository;
            }
        }
        public GenericRepository<tbl_Map_Role_User> MapRoleUserRepository
        {
            get
            {
                if (this._mapRoleUserRepository == null)
                    this._mapRoleUserRepository = new GenericRepository<tbl_Map_Role_User>(_context);
                return _mapRoleUserRepository;
            }
        }

        //school integration code
        public GenericRepository<tbl_Map_School_Board> MapSchoolBoardRepository
        {
            get
            {
                if (this._mapSchoolBoardRepository == null)
                    this._mapSchoolBoardRepository = new GenericRepository<tbl_Map_School_Board>(_context);
                return _mapSchoolBoardRepository;
            }
        }
        public GenericRepository<tbl_Map_School_Standard> MapSchoolStandardRepository
        {
            get
            {
                if (this._mapSchoolStandardRepository == null)
                    this._mapSchoolStandardRepository = new GenericRepository<tbl_Map_School_Standard>(_context);
                return _mapSchoolStandardRepository;
            }
        }
        public GenericRepository<tbl_Map_School_Teacher> MapSchoolTeacherRepository
        {
            get
            {
                if (this._mapSchoolTeacherRepository == null)
                    this._mapSchoolTeacherRepository = new GenericRepository<tbl_Map_School_Teacher>(_context);
                return _mapSchoolTeacherRepository;
            }
        }
        public GenericRepository<tbl_Map_Teacher_Standard> MapTeacherStandardRepository
        {
            get
            {
                if (this._mapTeacherStandardRepository == null)
                    this._mapTeacherStandardRepository = new GenericRepository<tbl_Map_Teacher_Standard>(_context);
                return _mapTeacherStandardRepository;
            }
        }

        public GenericRepository<tbl_Staging> UploadExcelRepository
        {
            get
            {
                if (this._uploadExcelRepository == null)
                    this._uploadExcelRepository = new GenericRepository<tbl_Staging>(_context);
                return _uploadExcelRepository;
            }
        }

        public object Program { get; private set; }

        public Int32 Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw e;
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public IEnumerable<TEntity> ExecuteReader<TEntity>(string sql, params object[] parms)
        //{
        //    return _context.Database.SqlQuery<TEntity>(sql, parms);
        //}

        public IEnumerable<TEntity> ExecuteReader<TEntity>(string storedProcedureName, SqlParameter[] parameters = null)
        {
            if (parameters != null && parameters.Any())
            {
                var parameterBuilder = new StringBuilder();
                parameterBuilder.Append(string.Format("EXEC {0} ", storedProcedureName));

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].SqlDbType == SqlDbType.VarChar
                        || parameters[i].SqlDbType == SqlDbType.NVarChar
                        || parameters[i].SqlDbType == SqlDbType.Char
                        || parameters[i].SqlDbType == SqlDbType.NChar
                        || parameters[i].SqlDbType == SqlDbType.Text
                        || parameters[i].SqlDbType == SqlDbType.NText
                        || parameters[i].SqlDbType == SqlDbType.UniqueIdentifier)
                    {
                        parameterBuilder.Append(string.Format("{0}='{1}'", parameters[i].ParameterName,
                            string.IsNullOrEmpty(parameters[i].Value.ToString())
                            ? string.Empty : parameters[i].Value.ToString()));
                    }
                    else if (parameters[i].SqlDbType == SqlDbType.BigInt
                       || parameters[i].SqlDbType == SqlDbType.Int
                       || parameters[i].SqlDbType == SqlDbType.TinyInt
                       || parameters[i].SqlDbType == SqlDbType.Decimal
                       || parameters[i].SqlDbType == SqlDbType.Float
                       || parameters[i].SqlDbType == SqlDbType.Money
                       || parameters[i].SqlDbType == SqlDbType.SmallInt
                       || parameters[i].SqlDbType == SqlDbType.SmallMoney
                       )
                    {
                        parameterBuilder.Append(string.Format("{0}={1}", parameters[i].ParameterName
                            , parameters[i].Value));
                    }

                    else if (parameters[i].SqlDbType == SqlDbType.Bit)
                    {
                        parameterBuilder.Append(string.Format("{0}={1}", parameters[i].ParameterName,
                            Convert.ToBoolean(parameters[i].Value)));
                    }

                    if (i < parameters.Length - 1)
                    {
                        parameterBuilder.Append(",");
                    }
                }

                var query = parameterBuilder.ToString();
                // var result = _context.Database.SqlQuery<IEnumerable>(query);
                var result = _context.Database.SqlQuery<TEntity>(query);
                return result.ToList();
            }
            else
            {
                var result = _context.Database.SqlQuery<TEntity>(string.Format("EXEC {0}", storedProcedureName));

                return result.ToList();

            }
        }

        public void ExecuteSqlCommand(string storedProcedureName)
        {
            var result = _context.Database.ExecuteSqlCommand(string.Format("EXEC {0}", storedProcedureName));
            //  var result1 = _context.Database.Connection.CreateCommand()


        }

    }
}
