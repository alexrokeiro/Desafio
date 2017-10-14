using Domain.Message.Message;
using Infrastructure;

namespace Domain.Contract.EntityService.Contract
{
    public interface IUserEntityService
    {

        ResultResponse<ListUserResponse> ListUsuario(ListUserRequest request);

        ResultResponse<CreateUserResponse> AddUser(CreateUserRequest request);

        ResultResponse<DeleteUserResponse> DeleteUser(DeleteUserRequest request);

        ResultResponse<AlterUserResponse> UpdateUser(AlterUserRequest request);

        ResultResponse<AddDependentResponse> AddDependent(AddDependentRequest request);

    }


}
