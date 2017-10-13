using Data.Repository;
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

        public ListarUsuarioResponse ListarUsuario(ListarUsuariosRequest request)
        {
            var result = new ListarUsuarioResponse();
            usuarioRepositoty = new UserRepository();
            var usuarios = usuarioRepositoty.Listar(request.name, request.like);
            result.Usuarios = MapToMessage(usuarios);
            return result;
        }


        public void AdicionarUsuario(CriarUsuarioRequest request)
        {
            usuarioRepositoty = new UserRepository();
            roleRepositoty = new RoleRepository();
            var role = roleRepositoty.ObterPorId(request.Role);
            var usuario = new User() { Birth = request.Birth, Email = request.Email, Genre = request.genre, Name = request.name, Role = role};
            usuarioRepositoty.Salvar(usuario);

        }

        public void ExcluirUsuario(DeletarUsuarioRequest request)
        {
            usuarioRepositoty = new UserRepository();
            var usuario = usuarioRepositoty.ObterPorId(request.id);
            if (usuario == null)
                return;

            usuarioRepositoty.Deletar(usuario);
        }

        public void AlterarUsuario(AlterarUsuarioRequest request)
        {
            var usuario = new User() {Id = request.id, Birth = request.Birth, Email = request.Email, Genre = request.genre, Name = request.name };
            usuarioRepositoty = new UserRepository();
            usuarioRepositoty.Atualizar(usuario);
        }

        public void AdicionarDependente(AdicionarDependeteRequest request)
        {
            usuarioRepositoty = new UserRepository();
            var user = usuarioRepositoty.ObterPorId(request.IdUsuario);
            user.Dependents.Add(new Dependent() { Name = request.Name });
            usuarioRepositoty.Atualizar(user);
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
                id = usuario.Id
            };
        }
    }
}
