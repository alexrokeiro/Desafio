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
    public class UsuarioEntityService
    {
        private UserRepository usuarioRepositoty;
        private RoleRepository roleRepositoty;
        private DependentRepository dependentRepository;

        public ResultResponse<ListarUsuarioResponse> ListarUsuario(ListarUsuariosRequest request)
        {
            ResultResponse<ListarUsuarioResponse> result = new ResultResponse<ListarUsuarioResponse>();
            usuarioRepositoty = new UserRepository();
            var usuarios = usuarioRepositoty.Listar(request.name, request.like);
            result.Retorno = new ListarUsuarioResponse();
            result.Retorno.Usuarios = MapToMessage(usuarios);
            return result;
        }


        public ResultResponse<CriarUsuarioResponse> AdicionarUsuario(CriarUsuarioRequest request)
        {
            ResultResponse<CriarUsuarioResponse> response = new ResultResponse<CriarUsuarioResponse>(request);
            usuarioRepositoty = new UserRepository();
            roleRepositoty = new RoleRepository();
            var role = roleRepositoty.ObterPorId(request.Role);
            var usuario = new User() { Birth = request.Birth, Email = request.Email, Genre = request.genre, Name = request.name, Role = role};
            usuarioRepositoty.Salvar(usuario);

            return response;
        }

        public ResultResponse<DeletarUsuarioResponse> ExcluirUsuario(DeletarUsuarioRequest request)
        {
            ResultResponse<DeletarUsuarioResponse> result = new ResultResponse<DeletarUsuarioResponse>(request);
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

        public ResultResponse<AlterarUsuarioResponse> AlterarUsuario(AlterarUsuarioRequest request)
        {
            ResultResponse<AlterarUsuarioResponse> result = new ResultResponse<AlterarUsuarioResponse>(request);
            var usuario = new User() {Id = request.id, Birth = request.Birth, Email = request.Email, Genre = request.genre, Name = request.name };
            usuarioRepositoty = new UserRepository();
            usuarioRepositoty.Atualizar(usuario);
            return result;
        }

        public ResultResponse<AdicionarDependenteResponse> AdicionarDependente(AdicionarDependeteRequest request)
        {
            ResultResponse<AdicionarDependenteResponse> result = new ResultResponse<AdicionarDependenteResponse>(request);
            usuarioRepositoty = new UserRepository();
            dependentRepository = new DependentRepository();
            var user = usuarioRepositoty.ObterPorId(request.IdUsuario);
            var dependet = new Dependent() { Name = request.Name, IdUser = user.Id };
            dependentRepository.Salvar(dependet);

            return result;
        }

        private List<UsuarioMessage> Create()
        {
            var list = new List<UsuarioMessage>();
            for (int i = 0; i < 9; i++)
            {
                list.Add(new UsuarioMessage() { id = 1, Birth = DateTime.Now, Email = "sdsadsa", genre = "masc", name = string.Concat("teste",i), Role = "role", QuantidadeDependentes = i });
            }
            return list;

        }

        public static List<UsuarioMessage> MapToMessage(List<User> usuarios)
        {
            var lista = new List<UsuarioMessage>();
            foreach (var usuario in usuarios)
            {
                lista.Add(MapToMessage(usuario));
            }

            return lista;
        }


        public static UsuarioMessage MapToMessage(User usuario)
        {
            return new UsuarioMessage()
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
