using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using InvestControl.Application.Services.Interfaces;
using InvestControl.Domain.Entity;
using InvestControl.Domain.Entity.Csv;
using InvestControl.Domain.Entity.Enums;
using InvestControl.Domain.Enum;
using InvestControl.Domain.Helpers;
using InvestControl.Infra.Context;

namespace InvestControl.Application.Services
{
    public class UploadInformationsService : IUploadInformationsService
    {
        private readonly InvestControlContext _context;
        private readonly CsvConfiguration _csvConfig;
        private readonly string _rootDirectory;
        private const string NEGOCIATIONS_FOLDER = "/Negociacoes";

        public UploadInformationsService(InvestControlContext context)
        {
            _context = context;
            _csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };

            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var index = currentDirectory.IndexOf("/InvestControl.API");
            _rootDirectory = currentDirectory.Substring(0, index);
        }

        public void StartUploadInformation()
        {
            CarregarEventos();
            CarregarTransacoes();
        }

        private void CarregarEventos()
        {
            using (var reader = new StreamReader($"{_rootDirectory}{NEGOCIATIONS_FOLDER}/eventos.csv"))
            using (var csv = new CsvReader(reader, _csvConfig))
            {
                csv.Context.RegisterClassMap<EventoCsvMap>();

                var eventosCsv = csv.GetRecords<EventoCsv>().ToList();

                foreach (var eventoCsv in eventosCsv)
                {
                    var evento = new Evento()
                    {
                        CodigoOrigem = eventoCsv.CodigoOrigem,
                        CodigoDestino = eventoCsv.CodigoDestino,
                        FatorBase = Int32.Parse(eventoCsv.FatorBase),
                        FatorGanho = Int32.Parse(eventoCsv.FatorGanho),
                        Valor = ConvertToDecimal(eventoCsv.Valor),
                        Data = DateTime.ParseExact(eventoCsv.Data, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        TipoEvento = GetTipoEvento(eventoCsv.TipoEvento)
                    };
                    _context.Add(evento);
                }

                _context.SaveChanges();
            }
        }

        private void CarregarTransacoes()
        {
            using (var reader = new StreamReader($"{_rootDirectory}{NEGOCIATIONS_FOLDER}/transacoes.csv"))
            using (var csv = new CsvReader(reader, _csvConfig))
            {
                var transacoesCsv = csv.GetRecords<TransacaoCsv>().ToList();
                var corretoras = new List<Corretora>();

                foreach (var transacaoCsv in transacoesCsv)
                {
                    var corretora = corretoras.FirstOrDefault(x => x.NomeFantasia.Contains(transacaoCsv.Corretora));

                    if (corretora == null)
                    {
                        corretora = new Corretora()
                        {
                            NomeFantasia = transacaoCsv.Corretora,
                            RazaoSocial = String.Empty,
                            CNPJ = String.Empty
                        };
                        var newCorretora = _context.Add(corretora);
                        corretoras.Add(newCorretora.Entity);
                    }

                    var transacao = new Transacao()
                    {
                        DataOperacao = DateTime.ParseExact(transacaoCsv.DataOperacao, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture),
                        TipoCategoria = GetTipoCategoria(transacaoCsv.Categoria),
                        CodigoAtivo = transacaoCsv.CodigoAtivo,
                        TipoOperacao = GetTipoOperacao(transacaoCsv.Operacao),
                        Quantidade = ConvertToDecimal(transacaoCsv.Quantidade),
                        PrecoUnitario = ConvertToDecimal(transacaoCsv.PrecoUnitario),
                        Corretora = corretora
                    };
                    _context.Add(transacao);
                }

                _context.SaveChanges();
            }
        }

        private TipoEvento GetTipoEvento(string tipoEvento)
        {
            if (tipoEvento.Equals(TipoEvento.Agrupamento.GetDescription()))
                return TipoEvento.Agrupamento;

            if (tipoEvento.Equals(TipoEvento.Desmembramento.GetDescription()))
                return TipoEvento.Desmembramento;

            if (tipoEvento.Equals(TipoEvento.Bonificacao.GetDescription()))
                return TipoEvento.Bonificacao;

            if (tipoEvento.Equals(TipoEvento.Conversao.GetDescription()))
                return TipoEvento.Conversao;

            throw new ArgumentException("Tipo de evento inválido");
        }

        private TipoCategoria GetTipoCategoria(string categoria)
        {
            if (categoria.Equals(TipoCategoria.FundosImobiliarios.GetDescription()))
                return TipoCategoria.FundosImobiliarios;

            if (categoria.Equals(TipoCategoria.Acao.GetDescription()))
                return TipoCategoria.Acao;

            if (categoria.Equals(TipoCategoria.Bdr.GetDescription()))
                return TipoCategoria.Bdr;

            if (categoria.Equals(TipoCategoria.Etf.GetDescription()))
                return TipoCategoria.Etf;

            if (categoria.Equals(TipoCategoria.EtfExterior.GetDescription()))
                return TipoCategoria.EtfExterior;

            if (categoria.Equals(TipoCategoria.Stocks.GetDescription()))
                return TipoCategoria.Stocks;
            
            if (categoria.Equals(TipoCategoria.TesouroDireto.GetDescription()))
                return TipoCategoria.TesouroDireto;

            throw new ArgumentException("Tipo de categoria inválida");
        }

        private TipoOperacao GetTipoOperacao(string operacao)
        {
            if (operacao.Equals(TipoOperacao.Compra.GetDescription()))
                return TipoOperacao.Compra;

            if (operacao.Equals(TipoOperacao.Venda.GetDescription()))
                return TipoOperacao.Venda;

            throw new ArgumentException("Tipo de operação inválida");
        }

        private decimal ConvertToDecimal(string valor)
        {
            return decimal.Parse(valor.Replace(".", string.Empty).Replace(",", "."));
        }
    }
}