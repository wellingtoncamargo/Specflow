Feature: Cálculo do valor do IR
	Realizando Teste Web

Scenario: 01-Massa de Dados
	Given Eu salvo 'TextBox' com o valor '//h2[@class='entry-title']//a[contains(text(),'Não use assertTrue para validar textos')]'
	Given Eu salvo 'msg' com o valor '//*[@id="post-3972"]/div/div/h2[4]'


Scenario: 02-Realizando o Login
	Given que acesso a pagina 'http://www.eliasnogueira.com/'
	When clico em '{TextBox}'
	Then vejo '{msg}'