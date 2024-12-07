﻿using Postech.Fase3.Contatos.Delete.Application.DTO;
using Postech.Fase3.Contatos.Delete.Application.Interface;
using Postech.Fase3.Contatos.Delete.Domain.Entities;
using Postech.Fase3.Contatos.Delete.Infra.CrossCuting.Interface;
using Postech.Fase3.Contatos.Delete.Infra.CrossCuting.Model;
using Serilog;
using System.Text.Json;


namespace Postech.Fase3.Contatos.Delete.Application.Service;

public class MensagemService(IContatoService _contatoService, ILogger _logger) : IMessageProcessor
{
    public async Task<ServiceResult<bool>> ProcessMessageAsync(string message)
    {
        try
        {
            _logger.Information("Processing message: {Message}", message);
            var contatoDto = JsonSerializer.Deserialize<ContatoDto>(message);
            var result = await _contatoService.ExcluirAsync(new Contato(contatoDto!.ContatoId!.Value, contatoDto.Nome, contatoDto.Telefone, contatoDto.Email, contatoDto.DddId, contatoDto.DataInclusao));
            _logger.Information("Message processed successfully: {Message}", message);
            return result;

        }
        catch (Exception e)
        {
            _logger.Error(e, "Error processing message: {Message}", message);
            return new ServiceResult<bool>(e);
        }
    }
}