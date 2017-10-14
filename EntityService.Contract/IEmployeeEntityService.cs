using Domain.Message.Message;
using Infrastructure;

namespace Domain.Contract.EntityService.Contract
{
    public interface IEmployeeEntityService
    {

        ResultResponse<ListEmployeeResponse> ListEmployee(ListEmployeeRequest request);

        ResultResponse<CreateEmployeeResponse> AddEmployee(CreateEmployeeRequest request);

        ResultResponse<DeleteEmployeeResponse> DeleteEmployee(DeleteEmployeeRequest request);

        ResultResponse<AlterEmployeeResponse> UpdateEmployee(AlterEmployeeRequest request);

        ResultResponse<AddDependentResponse> AddDependent(AddDependentRequest request);

        ResultResponse<DeleteEmployeeResponse> DeleteDependent(DeleteEmployeeRequest request);

    }


}
