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

namespace BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver:IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IProductInterface, ProductRepository>();
            registerComponent.RegisterType<IUserInterface, UserRepository>();
            registerComponent.RegisterType<ITokenInterface, TokenRepository>();
            registerComponent.RegisterType<IRoleInterface, RoleRepository>();
        }
    }
}
