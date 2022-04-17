using InvestControl.Application.DTOs;
using InvestControl.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using InvestControl.Core.Extensions;
using InvestControl.Domain.Entity;
using InvestControl.Domain.Entity.Enums;
using InvestControl.Domain.Helpers;
using InvestControl.Domain.Repository;

namespace InvestControl.Application.Services
{
    public class ImpostoDeRendaService : IImpostoDeRendaService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IEventoRepository _eventoRepository;

        public ImpostoDeRendaService(ITransacaoRepository transacaoRepository, IEventoRepository eventoRepository)
        {
            _transacaoRepository = transacaoRepository;
            _eventoRepository = eventoRepository;
        }

        public IList<CustodiaDto> CalcularCustodiaAnual(int ano)
        {
            return CalcularImpostoAnual(ano)
                .Where(x => x.Quantidade > 0)
                .Select(x => new CustodiaDto
                {
                    Categoria = x.Categoria,
                    CodigoAtivo = x.CodigoAtivo,
                    Quantidade = x.Quantidade.ToStringPtBr(),
                    PrecoMedio = x.PrecoMedio.ToStringPtBr(),
                    ValorTotal = x.ValorTotal.ToStringPtBr()
                }).ToList();
        }

        private IList<CustodiaCompletaDto> CalcularImpostoAnual(int ano)
        {
            var transacoes = ObterTransacoes(ano);

            var lucroPrejuizoLista = new List<CustodiaCompletaDto>();

            foreach (var transacoesPorAtivo in transacoes.GroupBy(t => t.CodigoAtivo.TrimEnd('F')))
            {
                var quantidadeTotal = decimal.Zero;
                var valorTotal = decimal.Zero;
                var valorPrecoMedio = decimal.Zero;

                foreach (var operacao in transacoesPorAtivo.OrderBy(t => t.DataOperacao))
                {
                    if (operacao.TipoOperacao == TipoOperacao.Compra)
                    {
                        quantidadeTotal += operacao.Quantidade;
                        valorTotal += operacao.Quantidade * operacao.PrecoUnitario;
                        valorPrecoMedio = valorTotal / quantidadeTotal;
                    }
                    else if (operacao.TipoOperacao == TipoOperacao.Venda)
                    {
                        quantidadeTotal -= operacao.Quantidade;
                        valorTotal -= operacao.Quantidade * valorPrecoMedio;
                    }
                }

                var categoria = transacoesPorAtivo.Any()
                    ? transacoesPorAtivo.FirstOrDefault()?.TipoCategoria.ToString()
                    : "Sem Categoria";

                lucroPrejuizoLista.Add(new CustodiaCompletaDto
                {
                    Categoria = categoria,
                    CodigoAtivo = transacoesPorAtivo.Key,
                    Quantidade = quantidadeTotal,
                    ValorTotal = valorTotal.Round2Decimals(),
                    PrecoMedio = valorPrecoMedio.Round2Decimals(),
                });
            }

            return lucroPrejuizoLista
                .OrderBy(l => l.Categoria)
                .ThenBy(l => l.CodigoAtivo)
                .ToList();
        }

        public IList<LucroPrejuizoDto> CalcularLucroEPrejuizoMensal(int ano)
        {
            var transacoes = ObterTransacoes(ano);
            var lucrosEPrejuizos = MontarListaDeLucrosEPrejuizosMensais(transacoes);

            foreach (var categorias in transacoes.GroupBy(x => x.TipoCategoria))
            {
                foreach (var ativos in categorias.GroupBy(x => x.CodigoAtivo))
                {
                    var quantidadeTotal = Decimal.Zero;
                    var valorTotal = Decimal.Zero;
                    var precoMedio = Decimal.Zero;
                    
                    foreach (var transacao in ativos.OrderBy(x => x.DataOperacao))
                    {
                        if (transacao.TipoOperacao == TipoOperacao.Compra)
                        {
                            quantidadeTotal += transacao.Quantidade;
                            valorTotal += transacao.PrecoUnitario * transacao.Quantidade;
                            precoMedio = valorTotal / quantidadeTotal;
                        }

                        if (transacao.TipoOperacao == TipoOperacao.Venda)
                        {
                            quantidadeTotal -= transacao.Quantidade;
                            valorTotal -= transacao.Quantidade * precoMedio;

                            if (transacao.DataOperacao.Year == ano)
                            {
                                var lucroPrejuizo = lucrosEPrejuizos
                                    .First(x => x.Categoria.Equals(transacao.TipoCategoria.GetDescription()) &&
                                                x.Mes.Equals(transacao.DataOperacao.Month));
                                lucroPrejuizo.Total += (transacao.PrecoUnitario - precoMedio) * transacao.Quantidade;
                            }
                        }
                    }
                }
            }

            return lucrosEPrejuizos.Where(x => x.Total != Decimal.Zero).ToList();
        }

        private static List<LucroPrejuizoDto> MontarListaDeLucrosEPrejuizosMensais(List<Transacao> transacoes)
        {
            var categorias = transacoes.Select(x => x.TipoCategoria.GetDescription()).Distinct();
            var lucrosEPrejuizosMensais = new List<LucroPrejuizoDto>();

            foreach (var categoria in categorias)
            {
                for (int mes = 1; mes <= 12; mes++)
                {
                    lucrosEPrejuizosMensais.Add(new LucroPrejuizoDto()
                    {
                        Categoria = categoria,
                        Mes = mes,
                        Total = Decimal.Zero
                    });
                }
            }

            return lucrosEPrejuizosMensais;
        }

        private List<Transacao> ObterTransacoes(int ano)
        {
            if (ano <= 0)
                ano = DateTime.Today.Year;

            var eventos = _eventoRepository.ObterAteAno(ano).ToList();
            var transacoes = _transacaoRepository.ObterAteAno(ano).ToList();

            TratarBaseComEventos(eventos, transacoes);

            return transacoes;
        }

        private void TratarBaseComEventos(IList<Evento> eventos, IList<Transacao> transacoes)
        {
            foreach (var evento in eventos)
            {
                var transacoesDoEvento = transacoes
                    .Where(x => x.CodigoAtivo.ToUpper().Equals(evento.CodigoOrigem.ToUpper()) &&
                                x.DataOperacao < evento.Data).ToList();

                if(!transacoesDoEvento.Any())
                {
                    continue;
                }

                if (evento.TipoEvento == TipoEvento.Conversao)
                {
                    foreach (var transacaoDoEvento in transacoesDoEvento)
                    {
                        transacaoDoEvento.CodigoAtivo = evento.CodigoDestino;
                    }
                }

                if (evento.TipoEvento == TipoEvento.Desmembramento)
                {
                    foreach (var transacaoDoEvento in transacoesDoEvento)
                    {
                        transacaoDoEvento.Quantidade *= evento.FatorGanho;
                        transacaoDoEvento.PrecoUnitario /= evento.FatorGanho;
                    }
                }

                if (evento.TipoEvento == TipoEvento.Agrupamento)
                {
                    continue;
                }

                if (evento.TipoEvento == TipoEvento.Bonificacao)
                {
                    var transacao = transacoesDoEvento.First();
                    var totalCompra = transacoesDoEvento.Where(x => x.TipoOperacao == TipoOperacao.Compra)
                        .Sum(x => x.Quantidade);
                    var totalVenda = transacoesDoEvento.Where(x => x.TipoOperacao == TipoOperacao.Venda)
                        .Sum(x => x.Quantidade);
                    var totalAtual = totalCompra - totalVenda;
                    var totalNovosPapeis = (int)(totalAtual / evento.FatorBase) * evento.FatorGanho;
                    var transacaoDeBonificacao = new Transacao()
                    {
                        DataOperacao = evento.Data,
                        TipoCategoria = transacao.TipoCategoria,
                        CodigoAtivo = transacao.CodigoAtivo,
                        TipoOperacao = TipoOperacao.Compra,
                        Quantidade = totalNovosPapeis,
                        PrecoUnitario = evento.Valor,
                        CorretoraId = transacao.CorretoraId,
                        Corretora = transacao.Corretora
                    };
                    transacoes.Add(transacaoDeBonificacao);
                }
            }
        }
    }
}