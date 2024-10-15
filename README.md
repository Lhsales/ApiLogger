# ApiLogger

## Serilog
[Serilog](https://serilog.net) é uma biblioteca de logs para aplicações .NET, como descrito pela própria empresa:
> Fácil de configurar, tem uma API limpa e roda em todas as plataformas recentes de .NET.

Através dele é possível registrar para vários tipos de ambiente, como console, arquivo e aplicações diversas, chamadas de Sink, tudo isso fácil de configurar e implementar.

## Meu projeto
Esse projeto foi feito para automatizar o disparo de logs via Serilog partindo de uma aplicação Web API.
Após implementado, um middleware será criado captando informações úteis de cada requisição, como tempo de execução, IP de quem requisitou, corpo do request e response, etc. e agrupando esses dados em um disparo de log ao fim da chamada, registrando assim como sua API se comportou durante a requisição.
<br><br>
Cada disparo de log dentro da aplicação será enriquecido com esses mesmos dados, podendo assim acompanhar os processos que um usuário está executando em sua API.
<br><br>
O projeto ainda está em desenvolvimento e tenho planos de algumas melhorias a serem implementadas.

## Como configurar
A configuração do ApiLogger é muito simples.
Primeiro é necessário que seja feita a padrão do Serilog em seu arquivo Program.cs, minha recomendação :
 
```csharp
//Program.cs
using ApiLogger.Serilog;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => config.Enrich.FromLogContext().WriteTo.Console();
```

Essas configurações definem onde seu log será disparado.
Observação: é importante que esteja definido que o Enrich do log possa ser feito pelo LogContext, como diz o trecho:

```
Enrich.FromLogContext()
```

Feito a configuração, antes de executar a aplicação, acrescente essa chamada:

```
app.UseApiLoggerSerilogMiddleware();
```

Com a linha de código acima, será registrado o middleware da requisição.

Pronto, você já está pronto para receber um log para cada requisição disparada, a mensagem chegará no seguinte formato:
```
[20:00:00 INF] HTTPS GET /WeatherForecast responded 200 in 0.200 seconds
```

Se implementado em uma aplicação de logs estruturados que tenha integração com o Serilog, como a ferramenta [Seq](https://datalust.co/seq), os dados mais detalhados da chamada poderão ser consultados.