using Infrastructure;
using Message;
using Message.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract.EntityService.Contract
{
    public interface IUserEntityService
    {

        ResultResponse<ListUserResponse> ListUsuario(ListUserRequest request);

        ResultResponse<CreateUserResponse> AddUser(CreateUserRequest request);

        ResultResponse<DeleteUserResponse> DeleteUser(DeleteUserRequest request);

        ResultResponse<AlterUserResponse> AlterUser(AlterUserRequest request);

        ResultResponse<AddDependentResponse> AddDependent(AddDependentRequest request);

    }


}
