using AutoMapper;
using BusinessEntities;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
   public static class MapperHelper    {

        public static void MapEntityInit()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<tbl_Quiz, QuizEntity>().ReverseMap();
                cfg.CreateMap<tbl_QuizDetails, QuizDetailsEntity>().ReverseMap();
                cfg.CreateMap<tbl_Solutions, SolutionsEntity>().ReverseMap();
                cfg.CreateMap<tbl_Curriculum, CurriculumEntity>().ReverseMap();
                cfg.CreateMap<tbl_CurriculumDetails, CurriculumDetailsEntity>().ReverseMap();
                cfg.CreateMap<tbl_Map_Role_User, MapRoleUserEntity>().ReverseMap();
                cfg.CreateMap<tbl_Standard, StandardEntity>().ReverseMap();
                cfg.CreateMap<tbl_Map_Role_Permission, MapRolePermissionEntity>().ReverseMap();
                cfg.CreateMap<tbl_Location, LocationEntity>().ReverseMap();
                cfg.CreateMap<tbl_City, CityEntity>().ReverseMap();
                cfg.CreateMap<tbl_State, StateEntity>().ReverseMap();
                cfg.CreateMap<tbl_Country, CountryEntity>().ReverseMap();
                cfg.CreateMap<tbl_Level, LevelEntity>().ReverseMap();
                cfg.CreateMap<tbl_User, UserEntity>().ReverseMap();
                cfg.CreateMap<tbl_Subject, SubjectEntity>().ReverseMap();
               // cfg.CreateMap<tbl_Category, CategoryEntity>().ReverseMap();
                cfg.CreateMap<tbl_Category_SubTopic, CategorySubTopicEntity>().ReverseMap();
                cfg.CreateMap<tbl_SubTopic, SubTopicEntity>().ReverseMap();               
                cfg.CreateMap<tbl_UserLog, UserLogEntity>().ReverseMap();
                cfg.CreateMap<tbl_Role, RoleEntity>().ReverseMap();
                cfg.CreateMap<tbl_Role, RoleEntity>().ReverseMap();
                cfg.CreateMap<tbl_Topic, TopicEntity>().ReverseMap();
                cfg.CreateMap<tbl_Board, BoardEntity>().ReverseMap();
                cfg.CreateMap<tbl_Question_Pattern, QuestionPatternEntity>().ReverseMap();
                cfg.CreateMap<tbl_MapTeacherStudentQuiz, MapTeacherStudentQuizEntity>().ReverseMap();
                cfg.CreateMap<tbl_QuizResponse, QuizResponseEntity>().ReverseMap();
                cfg.CreateMap<tbl_School, SchoolEntity>().ReverseMap();
                cfg.CreateMap<tbl_Map_User_Section, MapUserSectionEntity>().ReverseMap();
                cfg.CreateMap<tbl_UserDetails, UserDetailsEntity>().ReverseMap();
                //school integration code
                cfg.CreateMap<tbl_Map_School_Board, MapSchoolBoardEntity>().ReverseMap();
                cfg.CreateMap<tbl_Map_School_Standard, MapSchoolStandardEntity>().ReverseMap();
                cfg.CreateMap<tbl_Map_School_Teacher, MapSchoolTeacherEntity>().ReverseMap();
                cfg.CreateMap<tbl_Map_Teacher_Standard, MapTeacherStandardEntity>().ReverseMap();
                cfg.CreateMap<tbl_Staging, UploadEntity>().ReverseMap();
            });
        }
    }
}
