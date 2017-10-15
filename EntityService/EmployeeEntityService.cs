using Data.Repository;
using Domain.Contract.EntityService.Contract;
using Domain.Message.Message;
using Domain.Model.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace Domain.Implementations.EntityService.Imp
{
    public class EmployeeEntityService : IEmployeeEntityService
    {
        private EmployeeRepository EmployeeRepositoty;
        private RoleRepository roleRepositoty;
        private DependentRepository dependentRepository;

        public EmployeeEntityService()
        {
            EmployeeRepositoty = new EmployeeRepository();
            roleRepositoty = new RoleRepository();
            dependentRepository = new DependentRepository();
        }

        public ResultResponse<ListEmployeeResponse> ListEmployee(ListEmployeeRequest request)
        {
            ResultResponse<ListEmployeeResponse> response = new ResultResponse<ListEmployeeResponse>(request);
            try
            {
                var employees = EmployeeRepositoty.List(request.name, request.like);
                response.Retorno = new ListEmployeeResponse();
                response.Retorno.Employees = MapToMessage(employees);
                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possivel listar os empregados.");
                return response;
            }

        }


        public ResultResponse<CreateEmployeeResponse> AddEmployee(CreateEmployeeRequest request)
        {
            ResultResponse<CreateEmployeeResponse> response = new ResultResponse<CreateEmployeeResponse>(request);
            try
            {
                if (request.Genre != 0 && request.Genre != 1)
                {
                    response.CreateResponseBadRequest("Gênero deve ser 1 - Masculino ou 2 - Feminino.");
                    return response;
                }

                if (!ValidateEmail(request.Email))
                {
                    response.CreateResponseBadRequest("Email inválido.");
                    return response;
                }

                if (request.Birth > DateTime.Now)
                {

                    response.CreateResponseBadRequest("Data de nascimento inválida.");
                    return response;

                }

                var role = roleRepositoty.GetById(request.Role);
                if (role == null)
                {
                    response.CreateResponseBadRequest("Cargo não encontrado.");
                    return response;
                }

                var employee = Employee.CreateEmployee(request.Name, request.Email, request.Genre, request.Birth, role);
                EmployeeRepositoty.Save(employee);
                response.Retorno = new CreateEmployeeResponse() { IdEmployee = employee.Id };
                return response;
            }
            catch (System.Exception ex)
            {
                response.CreateResponseInternalServerError("Não foi possivel adicionar o empregado.");
                return response;
            }


        }

        public ResultResponse<DeleteEmployeeResponse> DeleteEmployee(DeleteEmployeeRequest request)
        {
            ResultResponse<DeleteEmployeeResponse> response = new ResultResponse<DeleteEmployeeResponse>(request);
            try
            {
                var employee = EmployeeRepositoty.GetById(request.id, true);
                if (employee == null)
                {
                    response.CreateResponseBadRequest("Empregado não encontrado.");
                    return response;
                }
                employee.Dependents.ToList().ForEach(p => dependentRepository.Delete(p));

                var employee2 = EmployeeRepositoty.GetById(request.id);
                EmployeeRepositoty.Delete(employee2);

                return response;
            }
            catch (System.Exception ex)
            {
                response.CreateResponseInternalServerError("Não foi possível excluir o empregado.");
                return response;
            }

        }

        public ResultResponse<AlterEmployeeResponse> UpdateEmployee(AlterEmployeeRequest request)
        {
            ResultResponse<AlterEmployeeResponse> response = new ResultResponse<AlterEmployeeResponse>(request);
            try
            {
                if (request.Genre != 0 && request.Genre != 1)
                {
                    response.CreateResponseBadRequest("Gênero deve ser 1 - Masculino ou 2 - Feminino.");
                    return response;
                }

                if (!ValidateEmail(request.Email))
                {
                    response.CreateResponseBadRequest("Email inválido.");
                    return response;
                }

                if (request.Birth > DateTime.Now)
                {

                    response.CreateResponseBadRequest("Data de nascimento inválida.");
                    return response;

                }

                var employee = EmployeeRepositoty.GetById(request.Id);
                if (employee == null)
                {
                    response.CreateResponseBadRequest("Empregado não encontrado para o id informado.");
                    return response;
                }

                var role = roleRepositoty.GetById(request.Role);
                if (role == null)
                {
                    response.CreateResponseBadRequest("Cargo não encontrado.");
                    return response;
                }

                employee.UpdateEmployee(request.Name, request.Email, request.Genre, request.Birth, role);

                EmployeeRepositoty.Update(employee);
                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possível alterar o empregado.");
                return response;
            }

        }

        public ResultResponse<AddDependentResponse> AddDependent(AddDependentRequest request)
        {
            ResultResponse<AddDependentResponse> response = new ResultResponse<AddDependentResponse>(request);
            try
            {
                var employee = EmployeeRepositoty.GetById(request.IdUsuario);
                if (employee == null)
                {
                    response.CreateResponseBadRequest("Empregado não encontrado para o id informado.");
                    return response;
                }

                var dependent = new Dependent() { Name = request.Name, IdUser = employee.Id };
                dependentRepository.Save(dependent);
                response.Retorno = new AddDependentResponse() { IdDependent = dependent.Id };
                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possível adicionar o dependente.");
                return response;
            }

        }

        public ResultResponse<DeleteEmployeeResponse> DeleteDependent(DeleteEmployeeRequest request)
        {
            ResultResponse<DeleteEmployeeResponse> response = new ResultResponse<DeleteEmployeeResponse>(request);
            try
            {
                var dependent = dependentRepository.GetById(request.id);
                if (dependent == null)
                {
                    response.CreateResponseBadRequest("Dependente não encontrado.");
                    return response;
                }

                dependentRepository.Delete(dependent);

                return response;
            }
            catch (Exception)
            {
                response.CreateResponseInternalServerError("Não foi possível excluir o dependente.");
                return response;
            }
        }


        public static List<EmployeeMessage> MapToMessage(List<Employee> employees)
        {
            var lista = new List<EmployeeMessage>();
            foreach (var employee in employees)
            {
                lista.Add(MapToMessage(employee));
            }

            return lista;
        }


        public static EmployeeMessage MapToMessage(Employee employee)
        {
            return new EmployeeMessage()
            {
                Birth = employee.Birth,
                Email = employee.Email,
                genre = employee.Genre == 0 ? "Masculino" : "Feminino",
                name = employee.Name,
                id = employee.Id,
                QuantidadeDependentes = employee.Dependents.Count(),
                Role = employee.Role.Name
            };
        }

        public static bool ValidateEmail(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (String.IsNullOrEmpty(email) || rg.IsMatch(email))
                return true;

            return false;
        }
    }
}
