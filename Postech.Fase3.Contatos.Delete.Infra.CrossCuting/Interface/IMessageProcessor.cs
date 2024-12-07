using Postech.Fase3.Contatos.Delete.Infra.CrossCuting.Model;

namespace Postech.Fase3.Contatos.Delete.Infra.CrossCuting.Interface;

public interface IMessageProcessor
{
    Task<ServiceResult<bool>> ProcessMessageAsync(string message);
}
