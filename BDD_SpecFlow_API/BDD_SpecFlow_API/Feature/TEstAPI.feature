#language: pt-br
Funcionalidade: TesteAPI
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Cenario: Add two numbers
	Dado que eu use a API 'http://api.postmon.com.br/v1/cep/'
	E informo o CEP '07179260'
	Quando realizar um GET
	Entao as informações solicitadas
	E retorno devera ser 'OK'