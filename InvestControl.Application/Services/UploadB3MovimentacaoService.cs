using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using InvestControl.Application.Services.Interfaces;
using InvestControl.Domain.Entity;
using InvestControl.Domain.Entity.Csv;
using InvestControl.Domain.Enum;
using InvestControl.Infra.Context;

namespace InvestControl.Application.Services;

public class UploadB3MovimentacaoService : IUploadB3MovimentacaoService
{
    private readonly InvestControlContext _context;
    private readonly CsvConfiguration _csvConfig;
    private readonly string _directory;
    private const string fileNomeBase = "movimentacao";

    public UploadB3MovimentacaoService(InvestControlContext context)
    {
        _context = context;
        _csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";"
        };

        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var index = currentDirectory.IndexOf("/InvestControl.API", StringComparison.Ordinal);
        _directory = $"{currentDirectory[..index]}/Negociacoes" ;
    }
    
    public void StartUpload()
    {
        // Entrada/Saída => Crédito = Compra, Débito = Venda
        // Data = Data da movimentação
        // Movimentação = Tipo da movimentação
            // Rendimento = dividendos
            // Transferência - Liquidação = compra e venda
            // Atualização = entrada de cotas da subscrição
            // Compra ou Venda = tesouro direto
            // Juros Sobre Capital Próprio = JCP de ações
            // Transferência = portabilidade de corretoras (ignorar)
            // Dividendo ou Dividendo Transferido = dividendos
        // Produto = texto com o código do ativo (0:04) ativo - (0:5) ou (0:06) com número
        // Quantidade
        // Preço unitário
        // Valor da Operação = valor total
        
        
        
        var directoryInfo = new DirectoryInfo(_directory);
        var files = directoryInfo.GetFiles();

        foreach (var fileInfo in files.OrderBy(x => x.Name))
        {
            if (fileInfo.Name.Contains(fileNomeBase))
            {
                UploadInformations(fileInfo.Name);
            }
        }

        _context.SaveChanges();
    }

    private void UploadInformations(string fileName)
    {
        using var reader = new StreamReader($"{_directory}/{fileName}");
        using var csv = new CsvReader(reader, _csvConfig);
        csv.Context.RegisterClassMap<MovimentacoesB3CsvMap>();
        var movimentacoesCsv = csv.GetRecords<MovimentacoesB3Csv>().ToList();

        //TODO: corretora temporária
        var corretora = new Corretora()
        {
            NomeFantasia = "Banco Modal",
            RazaoSocial = String.Empty,
            CNPJ = String.Empty
        };
        _context.Add(corretora);

        foreach (var movimentacaoCsv in movimentacoesCsv)
        {
            if (!MovimentacoesMapeadas().Contains(movimentacaoCsv.Movimentacao))
                continue;
            
            var codigoAtivo = movimentacaoCsv.Produto.Substring(0, 6).Trim();
            var valorDaOperacao = movimentacaoCsv.ValorDaOperacao.Substring(3).Replace(',', '.');

            var rendimento = new Rendimento()
            {
                TipoCategoria = GetTipoCategoria(codigoAtivo),
                CodigoAtivo = codigoAtivo,
                Valor = Convert.ToDecimal(valorDaOperacao),
                Data = DateTime.ParseExact(movimentacaoCsv.Data, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            
            _context.Add(rendimento);
        }
    }

    private TipoOperacao GetTipoOperacao(string movimentacaoCsvEntradaSaida) =>
        movimentacaoCsvEntradaSaida == "Credito" ? TipoOperacao.Compra : TipoOperacao.Venda;

    private TipoCategoria GetTipoCategoria(string movimentacaoCsvProduto)
    {
        return TipoCategoria.NaoDefinido;
    }

    private string[] MovimentacoesMapeadas() =>
        new string[]
        {
            "Juros Sobre Capital Próprio",
            "Juros Sobre Capital Próprio - Transferido",
            "Dividendo",
            "Dividendo - Transferido",
            "Rendimento"
        };
}