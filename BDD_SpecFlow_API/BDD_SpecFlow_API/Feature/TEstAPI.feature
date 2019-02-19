
Feature: TesteAPI
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

	@Positivo
	Scenario Outline: Add two numbers
	Given I Run Scenario '<teste>'
	Given que eu use a API 'http://api.postmon.com.br/v1/cep/'
	And informo o CEP '07179260'
	When realizar um GET
	Then retorno devera ser 'OK'
	Then as informações solicitadas
	Given Eu salvo a variavel '<var>' como 'bairro'

	Examples: 
	| teste                        | var                     |
	| Printando cep                | cep                     |
	| Printando nome               | estado_info/nome        |
	| Printando estado codigo_ibge | estado_info/codigo_ibge |
	| Printando estado codigo_ibge | estado_info/area_km2    |
	| Printando bairro             | bairro                  |
	| Printando cidade             | cidade                  |
	| Printando logradouro         | logradouro              |
	| Printando cidade codigo_ibge | cidade_info/codigo_ibge |
	| Printando cidade area_km2    | cidade_info/area_km2    |
	| Printando estado             | estado                  |


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
	When informando o body '"{"id": 334,  "username": "TEste",  "firstName": "tes",  "lastName": "ter",  "email": "string@rrr.com",  "password": "123456",  "phone": "string",  "userStatus": 1}"'
	And realizo um POST em '/user'
	Then retorno devera ser 'UnsupportedMediaType'
	Then as informações solicitadas

	@Positivo
	Scenario: Buscando Personagens de Star Wars
	Given que eu use a API 'https://swapi.co/api'
	And informo o CEP '/people/'
	When realizar um GET
	Then retorno devera ser 'OK'
	Then as informações solicitadas