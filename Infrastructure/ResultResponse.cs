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
    class ResultResponse
    {
    }

    public class ResultResponse<TResponseMessage> : IHttpActionResult where TResponseMessage : BaseResponse
    {
        private BaseRequest requestMessage;

        public ResultResponse()
        {
            Validacoes = new List<ValidationMessage>();
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

        /// <summary>
        /// Utilizado para retornar mensagens de validações. Ex: new ValidationMessage { Atributo = "Cep", Mensagem = "Campo obrigatório." }
        /// </summary>
        [DataMember(Name = "validacoes")]
        public List<ValidationMessage> Validacoes { get; set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public ResultResponse<TNewResponseMessage> GetNewResultMessage<TNewResponseMessage>(BaseRequest requestMessage)
            where TNewResponseMessage : BaseResponse
        {
            return new ResultResponse<TNewResponseMessage>(requestMessage)
            {
//                Protocolo = Protocolo,
                HttpStatusCode = HttpStatusCode,
                Mensagem = Mensagem,
                Validacoes = Validacoes
            };
        }

        public void MapResultResponseMessage(int httpStatusCode, string message, Guid? protocolo = null, List<ValidationMessage> validacoes = null)
        {
            Validacoes = validacoes;

            switch (httpStatusCode)
            {
                case 422:
                    CreateResponseUnprocessableEntity(message);
                    break;
                case 200:
                    CreateResponseOk();
                    break;
                case 201:
                    CreateResponseCreated();
                    break;
                case 202:
                    CreateResponseAccepted();
                    break;
                case 400:
                    CreateResponseBadRequest(message);
                    break;
                case 500:
                    CreateResponseInternalServerError(message);
                    break;
                case 403:
                    CreateResponseForbidden();
                    break;
                case 404:
                    CreateResponseNotFound();
                    break;
                case 503:
                    CreateResponseServiceUnavailable();
                    break;
                case 401:
                    CreateResponseUnauthorized();
                    break;
                case 406:
                    CreateResponseNotAcceptable();
                    break;
                default:
                    CreateResponseInternalServerError();
                    break;
            }
        }

        /// <summary>
        /// 422 Unprocessable Entity - Utilizado para validações de negócio ou alguma informação necessária para uma ação.
        /// <c>Quando necessário uma origem para criar um carrinho e a mesma não é encontrada. Utilize esse tipo de retorno.</c>
        /// </summary>
        /// <param name="message"></param>
        public void CreateResponseUnprocessableEntity(string message)
        {
            Mensagem = "Validação" + (string.IsNullOrWhiteSpace(message) ? "" : " - " + message);
            HttpStatusCode = (HttpStatusCode)422;
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
        /// 202 Accepted - Utilizado para chamadas async ou adicionar algum item em fila de processamento.
        /// </summary>
        public void CreateResponseAccepted()
        {
            Mensagem = "Em fila de processamento.";
            HttpStatusCode = (HttpStatusCode)202;
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
        public void CreateResponseInternalServerError(string message = null)
        {
            Mensagem = string.IsNullOrWhiteSpace(message) ? "Erro na camada de serviço." : message;
            HttpStatusCode = (HttpStatusCode)500;
        }

        /// <summary>
        /// 403 Forbidden - Utilizado para requisições rejeitadas pelo servidor.
        /// <c>Utilizado principalmente pelos conectores.</c>
        /// </summary>
        public void CreateResponseForbidden()
        {
            Mensagem = "Requisição proibida - O servidor recusou a requisição.";
            HttpStatusCode = (HttpStatusCode)403;
        }

        /// <summary>
        /// 404 Not Found - Utilizado para informar rota inválida.
        /// <c>Utilizado principalmente pelos conectores.</c>
        /// </summary>
        public void CreateResponseNotFound()
        {
            Mensagem = "Recurso não encontrado no servidor.";
            HttpStatusCode = (HttpStatusCode)404;
        }

        /// <summary>
        /// 503 Service Unavailable - Utilizado para informar serviço indisponível.
        /// <c>Utilizado principalmente pelos conectores.</c>
        /// </summary>
        public void CreateResponseServiceUnavailable()
        {
            Mensagem = "Serviço indisponível.";
            HttpStatusCode = (HttpStatusCode)503;
        }

        /// <summary>
        /// 401 Unauthorized - Utilizado para token inválido ou restrição de acesso.
        /// </summary>
        public void CreateResponseUnauthorized()
        {
            Mensagem = "Validação - Não autorizado o acesso ao recurso.";
            HttpStatusCode = (HttpStatusCode)401;
        }

        /// <summary>
        /// 406 NotAcceptable - Utilizado para informar que existe informações que devem ser passadas no header. 
        /// <c>CodigoOperadora obrigatório</c>
        /// </summary>    
        public void CreateResponseNotAcceptable()
        {
            Mensagem = "Validação - Informação no header é obrigatório.";
            HttpStatusCode = (HttpStatusCode)406;
        }

        /// <summary>
        /// Adiciona um erro de validação.
        /// </summary>
        /// <param name="atributo">Campo com erro de validação. Ex: "Cep".</param>
        /// <param name="mensagem">Mensagem para a validação do campo. Ex: "Campo obrigatório".</param>
        public void AdicionarValidacao(string atributo, string mensagem)
        {
            Validacoes.Add(new ValidationMessage { Atributo = atributo, Mensagem = mensagem });
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
