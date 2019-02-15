
Feature: TesteAPI
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

	@Positivo
	Scenario: Add two numbers
	Given que eu use a API 'http://api.postmon.com.br/v1/cep/'
	And informo o CEP '07179260'
	When realizar um GET
	Then retorno devera ser 'Ok'
	Then as informações solicitadas

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


	Scenario: Get Dogs
	Given I Run Scenario 'Consulta de Dog'
	Given que eu use a API 'http://petstore.swagger.io/v2'
	When informando o body '"{"id": 0,"petId": 0,"quantity": 0,"shipDate": "2019-02-15T13:27:13.001Z","status": "placed","complete": false}"'
	And realizo um POST em '/store/order'
	Then retorno devera ser 'UnsupportedMediaType'
	Then as informações solicitadas