Feature: Cálculo do valor do IR
	Para quanto vou pagar de imposto
	Enquanto contribuinte
	Eu gostaria de calcular o IR

Scenario: Salário de 100 reais fica isento
	Given que estou na página IR 'https://www.calcule.net/trabalhista/calculo-imposto-de-renda-irrf/'
	And preencho o campo 'salario' com o valor '100.00'
	When clico em 'botao'
	Then vejo 'Imposto devido:R$ 0,00Isento'


Scenario Outline: Salário de <valor> reais fica isento
	Given que estou na página IR 'https://www.calcule.net/trabalhista/calculo-imposto-de-renda-irrf/'
	And preencho o campo 'salario' com o valor '<valor>.00'
	When clico em 'botao'
	Then vejo 'Imposto devido:R$ 0,00Isento'

Examples: 
	| valor |
	| 2000  |
	| 3000  |
	
