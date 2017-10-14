using Domain.Message.Message;
using Infrastructure;

namespace Domain.Contract.EntityService.Contract
{
    public interface IEmployeeEntityService
    {

        ResultResponse<ListEmployeeResponse> ListUsuario(ListEmployeeRequest request);

        ResultResponse<CreateEmployeeResponse> AddUser(CreateEmployeeRequest request);

        ResultResponse<DeleteEmployeeResponse> DeleteUser(DeleteEmployeeRequest request);

        ResultResponse<AlterEmployeeResponse> UpdateUser(AlterEmployeeRequest request);

        ResultResponse<AddDependentResponse> AddDependent(AddDependentRequest request);

    }


}
