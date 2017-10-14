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
        private UserRepository usuarioRepositoty;
        private RoleRepository roleRepositoty;
        private DependentRepository dependentRepository;

        public ResultResponse<ListUserResponse> ListarUsuario(ListUserRequest request)
        {
            ResultResponse<ListUserResponse> result = new ResultResponse<ListUserResponse>();
            usuarioRepositoty = new UserRepository();
            var usuarios = usuarioRepositoty.Listar(request.name, request.like);
            result.Retorno = new ListUserResponse();
            result.Retorno.Usuarios = MapToMessage(usuarios);
            return result;
        }


        public ResultResponse<CreateUserResponse> AdicionarUsuario(CreateUserRequest request)
        {
            ResultResponse<CreateUserResponse> response = new ResultResponse<CreateUserResponse>(request);
            usuarioRepositoty = new UserRepository();
            roleRepositoty = new RoleRepository();
            var role = roleRepositoty.ObterPorId(request.Role);
            var usuario = new User() { Birth = request.Birth, Email = request.Email, Genre = request.genre, Name = request.name, Role = role};
            usuarioRepositoty.Salvar(usuario);

            return response;
        }

        public ResultResponse<DeleteUserResponse> ExcluirUsuario(DeleteUserRequest request)
        {
            ResultResponse<DeleteUserResponse> result = new ResultResponse<DeleteUserResponse>(request);
            usuarioRepositoty = new UserRepository();
            var usuario = usuarioRepositoty.ObterPorId(request.id);
            if (usuario == null)
            {
                result.CreateResponseBadRequest("Usuário não encontrado");
                return result;
            }

            usuarioRepositoty.Deletar(usuario);

            return result;
        }

        public ResultResponse<AlterUserResponse> AlterarUsuario(AlterUserRequest request)
        {
            ResultResponse<AlterUserResponse> result = new ResultResponse<AlterUserResponse>(request);
            usuarioRepositoty = new UserRepository();
            var user = usuarioRepositoty.ObterPorId(request.id);
            user.Birth = request.Birth;
            user.Email = request.Email;
            user.Genre = request.genre;
            user.Name = request.name;
            
            usuarioRepositoty.Atualizar(user);
            return result;
        }

        public ResultResponse<AddDependentResponse> AdicionarDependente(AddDependentRequest request)
        {
            ResultResponse<AddDependentResponse> result = new ResultResponse<AddDependentResponse>(request);
            usuarioRepositoty = new UserRepository();
            dependentRepository = new DependentRepository();
            var user = usuarioRepositoty.ObterPorId(request.IdUsuario);
            var dependet = new Dependent() { Name = request.Name, IdUser = user.Id };
            dependentRepository.Salvar(dependet);

            return result;
        }

        private List<UserMessage> Create()
        {
            var list = new List<UserMessage>();
            for (int i = 0; i < 9; i++)
            {
                list.Add(new UserMessage() { id = 1, Birth = DateTime.Now, Email = "sdsadsa", genre = "masc", name = string.Concat("teste",i), Role = "role", QuantidadeDependentes = i });
            }
            return list;

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
