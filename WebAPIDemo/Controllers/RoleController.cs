using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    [ApiAuthenticationFilter]
    public class RoleController : ApiController
    {
        private readonly IRoleInterface _roleInterface;
        public RoleController(IRoleInterface roleInterface)
        {
            _roleInterface = roleInterface;
        }
        // GET: api/Role
        public HttpResponseMessage Get()
        {
            var roles = _roleInterface.GetAllRoles();
            if (roles != null)
            {
                var roleEntities = roles as List<RoleEntity> ?? roles.ToList();
                if (roleEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, roleEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "roles Not Found");
        }

        // GET: api/Role/5
        public HttpResponseMessage Get(Guid id)
        {
            var role = _roleInterface.GetRoleById(id);
            if (role != null)
                return Request.CreateResponse(HttpStatusCode.OK, role);
            return Request.CreateResponse(HttpStatusCode.OK, "No role found for given Id");
        }

        // POST: api/Role
        public Guid Post([FromBody] RoleEntity roleEntity)
        {
            return _roleInterface.CreateRole(roleEntity);
        }

       

        // PUT: api/Role/5
        public bool Put(Guid id, [FromBody]RoleEntity roleEntity)
        {
            if (id != null)
            {
                return _roleInterface.UpdateRole(id, roleEntity);
            }
            return false;
        }

        // DELETE: api/Role/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _roleInterface.DeleteRole(id);
            return false;
        }
    }
}
