﻿using Domain.Contract.EntityService.Contract;
using Domain.Message.Message;
using Infrastructure;
using System.Web.Http;

namespace Desafio.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeEntityService userEntityService;

        public EmployeeController (IEmployeeEntityService userEntityService)
        {
            this.userEntityService = userEntityService;
        }

        [HttpPost]
        public ResultResponse<CreateEmployeeResponse> Create([FromBody]CreateEmployeeRequest request )
        {
            return userEntityService.AddUser(request);
        }

        [HttpPut]
        public ResultResponse<AlterEmployeeResponse> Update([FromBody]AlterEmployeeRequest request)
        {
            return userEntityService.UpdateUser(request);
        }

        [HttpGet]
        public ResultResponse<ListEmployeeResponse> Get([FromUri]ListEmployeeRequest request)
        {
            return userEntityService.ListUsuario(request);
        }

        [HttpDelete]
        public ResultResponse<DeleteEmployeeResponse> Delete([FromBody]DeleteEmployeeRequest request)
        {
            return userEntityService.DeleteUser(request);
        }

        [HttpPost]
        [Route("dependent")]
        public ResultResponse<AddDependentResponse> CreateDepedente([FromBody]AddDependentRequest request)
        {
            return userEntityService.AddDependent(request);
        }

        [HttpDelete]
        [Route("dependent")]
        public ResultResponse<DeleteEmployeeResponse> DeleteDepedente([FromBody]DeleteEmployeeRequest request)
        {
            return userEntityService.DeleteDependent(request);
        }
    }
}
