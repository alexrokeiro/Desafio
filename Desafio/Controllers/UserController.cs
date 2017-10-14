using EntityService;
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
        private UserEntityService entityService;

        public UserController ()
        {
            entityService = new UserEntityService();
        }

        [HttpPost]
        public ResultResponse<CreateUserResponse> Create([FromBody]CreateUserRequest request )
        {
            return entityService.AdicionarUsuario(request);
        }

        [HttpPut]
        public ResultResponse<AlterUserResponse> Update([FromBody]AlterUserRequest request)
        {
            return entityService.AlterarUsuario(request);
        }

        [HttpGet]
        public ResultResponse<ListUserResponse> Get([FromUri]ListUserRequest request)
        {
            return entityService.ListarUsuario(request);
        }

        [HttpDelete]
        public ResultResponse<DeleteUserResponse> Delete([FromBody]DeleteUserRequest request)
        {
            return entityService.ExcluirUsuario(request);
        }

        [HttpPost]
        [Route("dependent")]
        public ResultResponse<AddDependentResponse> CreateDepedente([FromBody]AddDependentRequest request)
        {
            return entityService.AdicionarDependente(request);
        }
    }
}
