using Postech.Fase3.Contatos.Delete.Domain.Entities;


namespace Postech.Fase3.Contatos.Delete.Infra.Interface;

public interface IContatoRepository
{
    Task<Contato> Excluir(Contato c);
    Task<bool> ExisteAsync(Contato c);
}
