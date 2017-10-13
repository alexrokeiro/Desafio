using EntityService;
using Infrastructure;
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
        public ResultResponse<CriarUsuarioResponse> Create([FromBody]CriarUsuarioRequest request )
        {
            entityService = new UsuarioEntityService();
            return entityService.AdicionarUsuario(request);
            
        }

        [HttpPut]
        public ResultResponse<AlterarUsuarioResponse> Update([FromBody]AlterarUsuarioRequest request)
        {
            entityService = new UsuarioEntityService();
            return entityService.AlterarUsuario(request);
            
        }

        [HttpGet]
        public ResultResponse<ListarUsuarioResponse> Get([FromUri]ListarUsuariosRequest request)
        {
            entityService = new UsuarioEntityService();
            return entityService.ListarUsuario(request);
        }

        [HttpDelete]
        public ResultResponse<DeletarUsuarioResponse> Delete([FromBody]DeletarUsuarioRequest request)
        {
            entityService = new UsuarioEntityService();
            return entityService.ExcluirUsuario(request);
            
        }

        [HttpPost]
        [Route("dependente")]
        public ResultResponse<AdicionarDependenteResponse> CreateDepedente([FromBody]AdicionarDependeteRequest request)
        {
            entityService = new UsuarioEntityService();
            return entityService.AdicionarDependente(request);
            
        }
    }
}
