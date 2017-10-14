using Domain.Contract.EntityService.Contract;
using Infrastructure;
using Message;
using Message.Response;
using System.Web.Http;

namespace Desafio.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserEntityService userEntityService;

        public UserController (IUserEntityService userEntityService)
        {
            this.userEntityService = userEntityService;
        }

        [HttpPost]
        public ResultResponse<CreateUserResponse> Create([FromBody]CreateUserRequest request )
        {
            return userEntityService.AddUser(request);
        }

        [HttpPut]
        public ResultResponse<AlterUserResponse> Update([FromBody]AlterUserRequest request)
        {
            return userEntityService.UpdateUser(request);
        }

        [HttpGet]
        public ResultResponse<ListUserResponse> Get([FromUri]ListUserRequest request)
        {
            return userEntityService.ListUsuario(request);
        }

        [HttpDelete]
        public ResultResponse<DeleteUserResponse> Delete([FromBody]DeleteUserRequest request)
        {
            return userEntityService.DeleteUser(request);
        }

        [HttpPost]
        [Route("dependent")]
        public ResultResponse<AddDependentResponse> CreateDepedente([FromBody]AddDependentRequest request)
        {
            return userEntityService.AddDependent(request);
        }
    }
}
