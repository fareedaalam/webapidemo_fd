using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DataModel;
using DataModel.UnitOfWork;
using Resolver;
using BusinessServices.Interface;
using BusinessServices.Repository;
using BusinessEntities;

namespace BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver:IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IMailerInterface, MailerRepository>();
            registerComponent.RegisterType<IDashboardInterface, DashboardRepository>();
            registerComponent.RegisterType<IMapTeacherChildInterface, MapTeacherChildRepository>();
            registerComponent.RegisterType<IParamDetailsInterface, ParamDetailsRepository>();            
            registerComponent.RegisterType<IMapParentChildInterface, MapParentChildRepository>();
            registerComponent.RegisterType<IConceptMappingInterface, ConceptMappingRepository>();
            registerComponent.RegisterType<IQuizInterface, QuizRepository>();
            registerComponent.RegisterType<ISolutionsInterface,SolutionsRepository>();
            registerComponent.RegisterType<ICurriculumInterface, CurriculumRepository>();
            registerComponent.RegisterType<IMapRoleUserInterface, MapRoleUserRepository>();
            registerComponent.RegisterType<IEmailInterface, IEmailRepository>();
            registerComponent.RegisterType<IStandardInterface, StandardRepository>();
            registerComponent.RegisterType<IMapRolePermissionInterface, MapRolePermissionRepository>();
            registerComponent.RegisterType<ILocationInterface, LocationRepository>();
            registerComponent.RegisterType<ICityInterface, CityRepository>();
            registerComponent.RegisterType<IStateInterface, StateRepository>();
            registerComponent.RegisterType<ICountryInterface, CountryRepository>();
            registerComponent.RegisterType<IMapRoleUserInterface, MapRoleUserRepository>();
            registerComponent.RegisterType<ILevelInterface, LevelRepository>();
            registerComponent.RegisterType<ITopicInterface, TopicRepository>();
            registerComponent.RegisterType<IUserInterface, UserRepository>();
            registerComponent.RegisterType<ITokenInterface, TokenRepository>();
            registerComponent.RegisterType<IRoleInterface, RoleRepository>();
            registerComponent.RegisterType<IPermissionInterface, PermissionRepository>();
            registerComponent.RegisterType<ISubjectInterface, SubjectRepository>();
            registerComponent.RegisterType<ICategoryInterface, CategoryRepository>();
            registerComponent.RegisterType<ICategorySubTopicInterface, CategorySubTopicRepository>();
            registerComponent.RegisterType<ISubTopicInterface, SubTopicRepository>();
            registerComponent.RegisterType<IBoardInterface, BoardRepository>();
            registerComponent.RegisterType<IUserLogInterface, UserLogRepository>();
            registerComponent.RegisterType<IQustionPatternInterface, QuestionPatternRepository>();
            registerComponent.RegisterType<IMapTeacherStudentQuizInterface, MapTeacherStudentQuizRepository>();

            registerComponent.RegisterType<ISchoolInterface, SchoolRepository>();
            registerComponent.RegisterType<IMapUserSectionInterface, MapUserSectionRepository>();
            registerComponent.RegisterType<IUserDetailsInterface, UserDetailsRepository>();
            //school integration code
            registerComponent.RegisterType<IMapSchoolBoardInterface, MapSchoolBoardRepository>();
            registerComponent.RegisterType<IMapSchoolStandardInterface, MapSchoolStandardRepository>();
            registerComponent.RegisterType<IMapSchoolTeacherInterface, MapSchoolTeacherRepository>();
            registerComponent.RegisterType<IMapTeacherStandardInterface, MapTeacherStandardRepository>();
            registerComponent.RegisterType<IUploadExcelInterface, UploadExcelRepository>();
        }
    }
}
