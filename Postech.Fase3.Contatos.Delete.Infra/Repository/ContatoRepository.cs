using Microsoft.EntityFrameworkCore;
using Postech.Fase3.Contatos.Delete.Domain.Entities;
using Postech.Fase3.Contatos.Delete.Infra.Interface;
using Postech.Fase3.Contatos.Delete.Infra.Repository.Context;

namespace Postech.Fase3.Contatos.Delete.Infra.Repository;

public class ContatoRepository(AppDBContext context) : IContatoRepository
{
    public async Task<Contato> Excluir(Contato c)
    {
        c.DesativarContato();
        context.Contatos.Update(c);
        await context.SaveChangesAsync();
        return c;
    }

    public async Task<bool> ExisteAsync(Contato c)
    {
        return await context.Contatos.AsNoTracking().AnyAsync(contato =>
            contato.Nome.Equals(c.Nome) && contato.Telefone.Equals(c.Telefone) && contato.DddId.Equals(c.DddId));
    }
}
