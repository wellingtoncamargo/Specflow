
Feature: TesteAPI
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

	@Positivo
	Scenario: Add two numbers
	Given que eu use a API 'http://api.postmon.com.br/v1/cep/'
	And informo o CEP '07179260'
	When realizar um GET
	Then as informações solicitadas
	And retorno devera ser 'OK'
	
	@Negativo
	Scenario Outline: Aleatorios
	Given I Run Scenario '<name>'
	Given que eu use a API 'http://api.postmon.com.br/v1/cep/'
	And informo o CEP '<cep>'
	When realizar um GET
	Then as informações solicitadas
	And retorno devera ser '<status>'

	Examples: 
	| cep      | status           | name  |
	| 12345678 | NotFound         | test1 |
	| 2113     | MethodNotAllowe | test2 |
	| 81150360 | OK               | test3 |
	| 98765432 | NotFoun         | test4 |
