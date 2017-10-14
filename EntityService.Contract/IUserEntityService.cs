using Infrastructure;
using Message;
using Message.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService.Contract
{
    public interface IUserEntityService
    {

        ResultResponse<ListUserResponse> ListarUsuario(ListUserRequest request);

        ResultResponse<CreateUserResponse> AdicionarUsuario(CreateUserRequest request);

        ResultResponse<DeleteUserResponse> ExcluirUsuario(DeleteUserRequest request);

        ResultResponse<AlterUserResponse> AlterarUsuario(AlterUserRequest request);

        ResultResponse<AddDependentResponse> AdicionarDependente(AddDependentRequest request);

    }


}
