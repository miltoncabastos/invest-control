### Sistema para auxiliar a galera no imposto de renda 👋

## Preenchendo seus dados
- Existe uma pasta chamada "Negociacoes" e dentro dela existem dois arquivos Transacaoes.csv e Eventos.csv
- Transações são suas transações de compra e venda, inclusive as subscrições que vc tiver feito
- Eventos vão ter os eventos que ocorrem na B3, como Desmembramento, Agrupamento, Bonificação e Conversão
- Preencha os seus dados nos dois arquivos

## Requisitos
- Você precisa ter instalado o .net 6 em seu computador

## Usando o sistema
- Ao rodar a aplicação, você irá se deparar com a tela do Swagger, no qual deixará disponível as API existentes no sistema
- Primeiro, você precisa rodar a api UploadInformations, ela irá fazer a importação dos seus arquivos para um banco de dados em memória, você terá que rodar sempre que iniciar a aplicação.
- Agora, com sua base importada, é só escolher qual funcionalidade quer, na seção de Imposto de Renda.
- Você também pode conferir os dados que estão na base na seção de Dados, como corretoras, eventos e transações

## Stack ##
- C# 10
- .Net 6
- Entity Framework