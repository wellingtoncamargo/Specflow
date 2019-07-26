Feature: TesteAPI
	Realizando Teste Api

	@Teste
Scenario: Consulta Cep
	Given que eu use a BaseURL 'BaseUrl'
	And Eu use a api '/cep/80240260'
	When realizo um GET
	Then retorno devera ser 200
	And as informações solicitadas

	Given Eu salvo a variavel 'bairro' como '12'
	Then comparo se '{12}' e 'Batel' sao iguais
