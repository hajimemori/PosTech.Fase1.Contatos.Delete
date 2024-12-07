using Microsoft.EntityFrameworkCore;
using Postech.Fase3.Contatos.Delete.Domain.Entities;
using Postech.Fase3.Contatos.Delete.Infra.Repository.Context;
using Postech.Fase3.Contatos.Delete.Infra.Repository;
using Postech.Fase3.Contatos.Delete.Integracao.Test.Fixture;
using Xunit.Extensions.Ordering;

namespace Postech.Fase3.Contatos.Delete.Integracao.Test.Infra;

[Collection(nameof(ContextDbCollection)), Order(2)]
public class ContatoRepositoryTest
{
    private readonly AppDBContext context;
    private readonly ContatoRepository repository;
    public ContatoRepositoryTest(ContextDbFixture fixture)
    {
        context = fixture.Context!;
        repository = new ContatoRepository(context);
    }

    [Fact]
    public async Task Excluir_DeveExcluirContato()
    {
        var contato = new Contato(Guid.NewGuid(), "Nome 1", "999878587", "teste@email.com.br", 11, DateTime.Now);
        context.Contatos.Add(contato);
        await context.SaveChangesAsync();
        context.Entry(contato).State = EntityState.Detached;

        var result = await repository.Excluir(contato);

        Assert.NotNull(result);
        Assert.Equal("Nome 1", result.Nome);
        Assert.Equal("999878587", result.Telefone);
        Assert.Equal(11, result.DddId);
        Assert.False(result.Ativo);
    }

    [Fact]
    public async Task Existe_DeveRetornarTrueSeContatoExiste()
    {
        var contato = new Contato(Guid.NewGuid(), "Nome 1", "999878587", "teste@email.com.br", 11, DateTime.Now);
        context.Contatos.Add(contato);
        await context.SaveChangesAsync();

        var result = await repository.ExisteAsync(contato);

        Assert.True(result);
    }
}
