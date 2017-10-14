using Data.Repository;
using Infrastructure;
using Message;
using Message.Response;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService
{
    public class UserEntityService
    {
        private UserRepository userRepositoty;
        private RoleRepository roleRepositoty;
        private DependentRepository dependentRepository;

        public UserEntityService()
        {
            userRepositoty = new UserRepository();
            roleRepositoty = new RoleRepository();
            dependentRepository = new DependentRepository();
        }

        public ResultResponse<ListUserResponse> ListarUsuario(ListUserRequest request)
        {
            ResultResponse<ListUserResponse> response = new ResultResponse<ListUserResponse>();
            try
            {
                var usuarios = userRepositoty.Listar(request.name, request.like);
                response.Retorno = new ListUserResponse();
                response.Retorno.Usuarios = MapToMessage(usuarios);
                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possivel listar os usuários");
                return response;
            }
            
        }


        public ResultResponse<CreateUserResponse> AdicionarUsuario(CreateUserRequest request)
        {
            ResultResponse<CreateUserResponse> response = new ResultResponse<CreateUserResponse>(request);
            try
            {
                var role = roleRepositoty.ObterPorId(request.Role);
                if (role == null)
                {
                    response.CreateResponseBadRequest("Role não existe.");
                    return response;
                }

                var usuario = new User() { Birth = request.Birth, Email = request.Email, Genre = request.genre, Name = request.name, Role = role };
                userRepositoty.Salvar(usuario);

                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possivel adicionar o usuário.");
                return response;
            }
            
            
        }

        public ResultResponse<DeleteUserResponse> ExcluirUsuario(DeleteUserRequest request)
        {
            ResultResponse<DeleteUserResponse> response = new ResultResponse<DeleteUserResponse>(request);
            try
            {
                var user = userRepositoty.ObterPorId(request.id);
                if (user == null)
                {
                    response.CreateResponseBadRequest("Usuário não encontrado");
                    return response;
                }
                user.Dependents.ToList().ForEach(p => dependentRepository.Delete(p));

                var user2 = userRepositoty.ObterPorId(request.id);
                userRepositoty.Deletar(user2);

                return response;
            }
            catch (System.Exception ex)
            {
                response.CreateResponseInternalServerError("Não foi possível excluir o usuário");
                return response;
            }
            
        }

        public ResultResponse<AlterUserResponse> AlterarUsuario(AlterUserRequest request)
        {
            ResultResponse<AlterUserResponse> response = new ResultResponse<AlterUserResponse>(request);
            try
            {
                var user = userRepositoty.ObterPorId(request.id);
                if (user == null)
                {
                    response.CreateResponseBadRequest("Usuário não encontrado para o id informado.");
                    return response;
                }

                user.Birth = request.Birth;
                user.Email = request.Email;
                user.Genre = request.genre;
                user.Name = request.name;

                userRepositoty.Atualizar(user);
                return response;
            }
            catch (System.Exception)
            {
                response.CreateResponseInternalServerError("Não foi possível alterar o usuário");
                return response;
            }
            
        }

        public ResultResponse<AddDependentResponse> AdicionarDependente(AddDependentRequest request)
        {
            ResultResponse<AddDependentResponse> response = new ResultResponse<AddDependentResponse>(request);
            try
            {
                var user = userRepositoty.ObterPorId(request.IdUsuario);
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

       
        public static List<UserMessage> MapToMessage(List<User> usuarios)
        {
            var lista = new List<UserMessage>();
            foreach (var usuario in usuarios)
            {
                lista.Add(MapToMessage(usuario));
            }

            return lista;
        }


        public static UserMessage MapToMessage(User usuario)
        {
            return new UserMessage()
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
