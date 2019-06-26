Feature: Cálculo do valor do IR
	Realizando Teste Web

Scenario: 01-Massa de Dados
	Given Eu salvo o valor 'mat-input-0' como 'user'
	Given Eu salvo o valor 'mat-input-1' como 'senha'
	Given Eu salvo o valor '//*[@role="menuitem"]' como 'BtSair'
	Given Eu salvo o valor '/html/body/app-root/app-login/div/div[2]/mat-card/form/div[2]/button' como 'BtLogin'
	Given Eu salvo o valor '/html/body/app-root/app-shell/div/mat-sidenav-container/mat-sidenav-content/app-header/mat-toolbar/button/span/mat-icon' como 'menu'
	Given Eu salvo o valor '//*[@id="mat-select-0"]/div/div[1]/span' como 'combo'
	Given Eu salvo o valor '//*[@id="mat-option-5"]/span' como 'Status'

Scenario: 02-Realizando o Login
	Given que acesso a pagina 'http://localhost:4200/login'
	When preencho o campo '{user}' com o valor 'User-Teste'
	And preencho o campo '{senha}' com o valor '12345'
	Then clico em '{BtLogin}'

	Then clico em '{menu}'
	And clico em '{BtSair}'


Scenario: 03-Selecionando Filtro
	Given que acesso a pagina 'http://localhost:4200/login'
	And  preencho o campo '{user}' com o valor 'User-Teste'
	And preencho o campo '{senha}' com o valor '12345'
	And clico em '{BtLogin}'
	
	When clico em '{combo}'
	And clico em '{Status}'
	
	Then clico em '{menu}'
	And clico em '{BtSair}'