﻿using Moq;
using Postech.Fase3.Contatos.Delete.Application.DTO;
using Postech.Fase3.Contatos.Delete.Application.Service;
using Postech.Fase3.Contatos.Delete.Domain.Entities;
using Postech.Fase3.Contatos.Delete.Infra.CrossCuting.Model;
using Postech.Fase3.Contatos.Delete.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Fase3.Contatos.Delete.Test.Application;


public class ContatoServiceTest
{
    private readonly Contato _contato;
    private readonly Mock<IContatoRepository> contatoRepository;

    public ContatoServiceTest()
    {
        _contato = new Contato(Guid.NewGuid(), "nome Teste", "963333243", "teste@email.com.br", 11, DateTime.Now);

        contatoRepository = new Mock<IContatoRepository>();

        contatoRepository
        .Setup(x => x.ExisteAsync(_contato))
        .ReturnsAsync(true);
    }

    [Fact]
    public async Task ContatoService_Exluido_ComSucesso()
    {
        //arrange
        contatoRepository
            .Setup(x => x.Excluir(_contato))
            .ReturnsAsync(_contato);

        var contatoService = new ContatoService(contatoRepository.Object);

        //act
        var contatoResult = await contatoService.ExcluirAsync(_contato);

        //assert
        Assert.True(contatoResult.IsSuccess);
        Assert.True(contatoResult.Data);
    }

    [Fact]
    public async Task ContatoService_Excluir_ComErroContatoNaoExistente()
    {
        //arrange
        contatoRepository
            .Setup(x => x.ExisteAsync(It.IsAny<Contato>()))
            .ReturnsAsync(false);

        var contatoService = new ContatoService(contatoRepository.Object);

        //act
        var contatoResult = await contatoService.ExcluirAsync(_contato);

        //assert
        Assert.False(contatoResult.IsSuccess);
        var ex = Assert.IsType<ValidacaoException>(contatoResult.Error);
        Assert.Equal("Contato não encontrado", ex.Message);
    }

    [Fact]
    public async Task ContatoService_Excluir_ComErro()
    {
        //arrange
        contatoRepository
            .Setup(x => x.Excluir(It.IsAny<Contato>()))
            .ThrowsAsync(new Exception("Erro ao Atualizar"));

        var contatoService = new ContatoService(contatoRepository.Object);

        //act
        var contatoResult = await contatoService.ExcluirAsync(_contato);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }
}
