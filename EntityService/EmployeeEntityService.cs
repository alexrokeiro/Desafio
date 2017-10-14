using Data.Repository;
using Domain.Contract.EntityService.Contract;
using Domain.Message.Message;
using Domain.Model.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Domain.Implementations.EntityService.Imp
{
    public class EmployeeEntityService : IEmployeeEntityService
    {
        private EmployeeRepository userRepositoty;
        private RoleRepository roleRepositoty;
        private DependentRepository dependentRepository;

        public EmployeeEntityService()
        {
            userRepositoty = new EmployeeRepository();
            roleRepositoty = new RoleRepository();
            dependentRepository = new DependentRepository();
        }

        public ResultResponse<ListEmployeeResponse> ListUsuario(ListEmployeeRequest request)
        {
            ResultResponse<ListEmployeeResponse> response = new ResultResponse<ListEmployeeResponse>(request);
            try
            {
                var usuarios = userRepositoty.List(request.name, request.like);
                response.Retorno = new ListEmployeeResponse();
                response.Retorno.Usuarios = MapToMessage(usuarios);
                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possivel listar os usuários");
                return response;
            }
            
        }


        public ResultResponse<CreateEmployeeResponse> AddUser(CreateEmployeeRequest request)
        {
            ResultResponse<CreateEmployeeResponse> response = new ResultResponse<CreateEmployeeResponse>(request);
            try
            {
                var role = roleRepositoty.GetById(request.Role);
                if (role == null)
                {
                    response.CreateResponseBadRequest("Role não existe.");
                    return response;
                }

                var usuario = Employee.CreateEmployee(request.Name, request.Email, request.Genre, request.Birth, role);
                userRepositoty.Save(usuario);
                
                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possivel adicionar o usuário.");
                return response;
            }


        }

        public ResultResponse<DeleteEmployeeResponse> DeleteUser(DeleteEmployeeRequest request)
        {
            ResultResponse<DeleteEmployeeResponse> response = new ResultResponse<DeleteEmployeeResponse>(request);
            try
            {
                var user = userRepositoty.GetById(request.id,true);
                if (user == null)
                {
                    response.CreateResponseBadRequest("Usuário não encontrado");
                    return response;
                }
                user.Dependents.ToList().ForEach(p => dependentRepository.Delete(p));

                var user2 = userRepositoty.GetById(request.id);
                userRepositoty.Delete(user2);

                return response;
            }
            catch (System.Exception ex)
            {
                response.CreateResponseInternalServerError("Não foi possível excluir o usuário");
                return response;
            }
            
        }

        public ResultResponse<AlterEmployeeResponse> UpdateUser(AlterEmployeeRequest request)
        {
            ResultResponse<AlterEmployeeResponse> response = new ResultResponse<AlterEmployeeResponse>(request);
            try
            {
                var user = userRepositoty.GetById(request.id);
                if (user == null)
                {
                    response.CreateResponseBadRequest("Usuário não encontrado para o id informado.");
                    return response;
                }

                var role = roleRepositoty.GetById(request.Role);
                if (role == null)
                {
                    response.CreateResponseBadRequest("Role não encontrada");
                    return response;
                }

                user.UpdateEmployee(request.name, request.Email, request.genre, request.Birth, role);
                
                userRepositoty.Update(user);
                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possível alterar o usuário");
                return response;
            }
            
        }

        public ResultResponse<AddDependentResponse> AddDependent(AddDependentRequest request)
        {
            ResultResponse<AddDependentResponse> response = new ResultResponse<AddDependentResponse>(request);
            try
            {
                var user = userRepositoty.GetById(request.IdUsuario);
                if (user == null)
                {
                    response.CreateResponseBadRequest("Usuário não encontrado para o id informado.");
                    return response;
                }

                var dependet = new Dependent() { Name = request.Name, IdUser = user.Id };
                dependentRepository.Save(dependet);

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


        public static List<EmployeeMessage> MapToMessage(List<Employee> usuarios)
        {
            var lista = new List<EmployeeMessage>();
            foreach (var usuario in usuarios)
            {
                lista.Add(MapToMessage(usuario));
            }

            return lista;
        }


        public static EmployeeMessage MapToMessage(Employee usuario)
        {
            return new EmployeeMessage()
            {
                Birth = usuario.Birth,
                Email = usuario.Email,
                genre = usuario.Genre,
                name = usuario.Name,
                id = usuario.Id,
                QuantidadeDependentes = usuario.Dependents.Count(),
                Role = usuario.Role.Name
            };
        }

    }
}
