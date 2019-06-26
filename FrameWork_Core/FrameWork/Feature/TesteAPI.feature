Feature: TesteAPI
	Realizando Teste Api

	@Teste
Scenario: Consulta Cep
	Given que eu use a BaseURL 'BaseUrl'
	And Eu use a api '/cep/07179260'
	When realizo um GET
	Then retorno devera ser 200
	And as informações solicitadas
