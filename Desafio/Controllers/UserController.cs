using EntityService;
using EntityService.Contract;
using Infrastructure;
using Message;
using Message.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return userEntityService.AdicionarUsuario(request);
        }

        [HttpPut]
        public ResultResponse<AlterUserResponse> Update([FromBody]AlterUserRequest request)
        {
            return userEntityService.AlterarUsuario(request);
        }

        [HttpGet]
        public ResultResponse<ListUserResponse> Get([FromUri]ListUserRequest request)
        {
            return userEntityService.ListarUsuario(request);
        }

        [HttpDelete]
        public ResultResponse<DeleteUserResponse> Delete([FromBody]DeleteUserRequest request)
        {
            return userEntityService.ExcluirUsuario(request);
        }

        [HttpPost]
        [Route("dependent")]
        public ResultResponse<AddDependentResponse> CreateDepedente([FromBody]AddDependentRequest request)
        {
            return userEntityService.AdicionarDependente(request);
        }
    }
}
