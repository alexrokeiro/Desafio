using EntityService;
using Message;
using Message.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Desafio.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        private UsuarioEntityService entityService;

        [HttpPost]
        public IHttpActionResult Create([FromBody]CriarUsuarioRequest request )
        {
            entityService = new UsuarioEntityService();
            entityService.AdicionarUsuario(request);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody]AlterarUsuarioRequest request)
        {
            entityService = new UsuarioEntityService();
            entityService.AlterarUsuario(request);
            return Ok();
        }

        [HttpGet]
        public ListarUsuarioResponse Get([FromUri]ListarUsuariosRequest request)
        {
            entityService = new UsuarioEntityService();
            return entityService.ListarUsuario(request);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]DeletarUsuarioRequest request)
        {
            entityService = new UsuarioEntityService();
            entityService.ExcluirUsuario(request);
            return Ok();
        }

        [HttpPost]
        [Route("dependente")]
        public IHttpActionResult CreateDepedente([FromBody]AdicionarDependeteRequest request)
        {
            entityService = new UsuarioEntityService();
            entityService.AdicionarDependente(request);
            return Ok();
        }
    }
}
