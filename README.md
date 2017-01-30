# Demonstração do uso de Sagas com NServiceBus #

Essa demonstração foi apresentada por mim no [meetup do Brasil .NET](https://www.meetup.com/pt-BR/Brasil-NET/),
na palestra *_Lidando com fluxos complexos com o uso de Sagas, NServiceBus e RabbitMQ_*.

Para executar essa demonstração na sua máquina, alguns pré-requisitos precisam ser
atendidos.

Visite e se inscreva no meu canal no YouTube ([https://youtube.com/donetbr](https://youtube.com/donetbr)) 
para assistir a palestra. 

## Pré-Requisitos ##

* SQL Server

No código-fonte, estou usando a conexão com o SQLEXPRESS, mas pode ser qualquer instância.
Defina no arquivo `app.config`.

* [RabbitMQ](https://www.rabbitmq.com/install-windows.html)

Instale o RabbitMQ na sua máquina. Se quiser abrir o painel de gerenciamento, execute
o comando:

```
rabbitmq-plugins enable rabbitmq_management
```

Mais detalhes [aqui](https://www.rabbitmq.com/management.html)

* [Particular Service Platform](https://particular.net/downloads)

A instalação da suite da Particular Software é opcional, mas necessária se você 
quiser observar as mensagens que trafegam na saga.

_OBS: se você não quiser visualizar as mensagens e o fluxo de execução da saga,_
_remova o trecho abaixo do `app.config`_

```
  <configSections>
    <section name="AuditConfig"
             type="NServiceBus.Config.AuditConfig, NServiceBus.Core"/>
  </configSections>
  <AuditConfig QueueName="audit"
               OverrideTimeToBeReceived="00:10:00"/>
  <appSettings>
    <add key="ServiceControl/Queue" value="Particular.ServiceControl"/>
  </appSettings>
```

_e o seguinte trecho do `Program.cs`:_


```
endpointConfiguration.SagaPlugin("OpenPlatform.ServiceControl");
```

## Configuração do ServiceControl ##

Para capturar as mensagens trafegadas no NserviceBus, configure o ServiceControl
da seguinte maneira:

* General/Name: mantenha `Particular.ServiceControl`
* Transport Configuration/Transport: selecione `RabbitMQ`
* Transport Configuration/Transport Connection String: `host=localhost`