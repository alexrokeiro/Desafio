using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ResultResponse<TResponseMessage> : IHttpActionResult where TResponseMessage : BaseResponse
    {
        private BaseRequest requestMessage;

        public ResultResponse()
        {
            CreateResponseOk();
        }

        /// <summary>
        /// Construtor criado com o  HttpStatusCode 200 como default. Utilizado para todas as situações de sucesso.
        /// </summary>
        /// <param name="requestMessage">Request no qual será referenciado o response</param>
        public ResultResponse(BaseRequest requestMessage)
            : this()
        {
            SetRequestMessage(requestMessage);
        }

        public void SetRequestMessage(BaseRequest requestMessage)
        {
            if (requestMessage == null)
                throw new ArgumentNullException("O requestMessage não pode ser nulo.");

            this.requestMessage = requestMessage;
        }

        [DataMember(Name = "retorno")]
        public TResponseMessage Retorno { get; set; }

        [DataMember(Name = "mensagem")]
        public string Mensagem { get; set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public ResultResponse<TNewResponseMessage> GetNewResultMessage<TNewResponseMessage>(BaseRequest requestMessage)
            where TNewResponseMessage : BaseResponse
        {
            return new ResultResponse<TNewResponseMessage>(requestMessage)
            {
                HttpStatusCode = HttpStatusCode,
                Mensagem = Mensagem
            };
        }
        
        /// <summary>
        /// 200 OK - Utilizado para todas as situações de sucesso.
        /// </summary>
        public void CreateResponseOk()
        {
            Mensagem = "Sucesso.";
            HttpStatusCode = (HttpStatusCode)200;
        }

        /// <summary>
        /// 201 Created - Utilizado para criação de registro no banco<c>Criar carrinho.</c>
        /// </summary>
        public void CreateResponseCreated()
        {
            Mensagem = "Criado com sucesso.";
            HttpStatusCode = (HttpStatusCode)201;
        }

        /// <summary>
        /// 400 Bad Request - Utilizado para a maioria dos erros de request.
        /// <c>Campos obrigatórios no request ou erro na camada de serviço.</c>
        /// </summary>
        /// <param name="message"></param>
        public void CreateResponseBadRequest(string message)
        {
            Mensagem = "Validação" + (string.IsNullOrWhiteSpace(message) ? "" : " - " + message);
            HttpStatusCode = (HttpStatusCode)400;
        }

        /// <summary>
        /// 500 Internal Server Error - Utilizado para erros internos no servidor ou exceptions.
        /// <c>Utilizado principalmente pelos conectores.</c>
        /// </summary>
        public void CreateResponseInternalServerError(string message)
        {
            Mensagem = message;
            HttpStatusCode = (HttpStatusCode)500;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage = requestMessage.HttpRequestMessage.CreateResponse(HttpStatusCode, this);
            return Task.FromResult(HttpResponseMessage);
        }

        /// <summary>
        /// Verifica que foi configurado algum erro ou validação de negócio no response.
        /// </summary>
        /// <returns>verdadeiro para HttpStatusCode maior ou igual 400 e falso para HttpStatusCode menor que 400</returns>
        public bool IsHttpStatusCodeError()
        {
            int errorCode = (int)HttpStatusCode;
            return errorCode >= 400;
        }
    }
}
