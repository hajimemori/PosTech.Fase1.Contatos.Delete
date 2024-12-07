using Postech.Fase3.Contatos.Delete.Domain.Entities;
using Postech.Fase3.Contatos.Delete.Infra.CrossCuting.Model;

namespace Postech.Fase3.Contatos.Delete.Application.Interface;

public interface IContatoService
{
    Task<ServiceResult<bool>> ExcluirAsync(Contato contato);
}
