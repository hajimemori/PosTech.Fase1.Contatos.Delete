using Postech.Fase3.Contatos.Delete.Application.Interface;
using Postech.Fase3.Contatos.Delete.Domain.Entities;
using Postech.Fase3.Contatos.Delete.Infra.CrossCuting.Model;
using Postech.Fase3.Contatos.Delete.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Fase3.Contatos.Delete.Application.Service;

public class ContatoService(IContatoRepository _contatoRepository) : IContatoService
{
    public async Task<ServiceResult<bool>> ExcluirAsync(Contato contato)
    {
        try
        {
            if (!await _contatoRepository.ExisteAsync(contato))
                return new ServiceResult<bool>(new ValidacaoException("Contato não encontrado"));

            await _contatoRepository.Excluir(contato);

            return new ServiceResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool>(ex);
        }
    }
}