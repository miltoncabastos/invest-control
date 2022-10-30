using System;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace InvestControl.Domain.Entity.Csv
{
    public class TransacaoCsv : ClassMap<TransacaoCsv>
    {
        [Name("Data operação")] 
        public string DataOperacao { get; set; }
        
        [Name("Categoria")] 
        public string Categoria { get; set; }
        
        [Name("Código Ativo")]
        public string CodigoAtivo { get; set; }
        
        [Name("Operação C/V")]
        public string Operacao { get; set; }

        [Name("Quantidade")]
        public string Quantidade { get; set; }
        
        [Name("Preço unitário")]
        public string PrecoUnitario { get; set; }
        
        [Name("Corretora")] 
        public string Corretora { get; set; }
    }
}