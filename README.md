# Implementação de Sistemas

Este é um sistema para controle de gastos residenciais. Ele permite o cadastro de pessoas, transações (despesas e receitas), e consulta dos totais.

## Funcionalidades

- Cadastro de Pessoas (criação, deleção, listagem).
- Cadastro de Transações (criação e listagem).
- Consulta de totais por pessoa e no geral.

## Funcionalidades:

Cadastro de pessoas: 

Deverá ser implementado um cadastro contendo as funcionalidades básicas de gerenciamento: criação, deleção e listagem.
Em casos que se delete uma pessoa, todas a transações dessa pessoa deverão ser apagadas.
O cadastro de pessoa deverá conter:

- Identificador (deve ser um número inteiro sequencial único gerado automaticamente);
- Nome (texto);
- Idade (número inteiro);

Cadastro de transações: 

Deverá ser implementado um cadastro contendo as funcionalidades básicas de gerenciamento: criação e listagem (não é necessário implementar edição/deleção).
Caso o usuário informe um menor de idade (menor de 18), apenas despesas deverão ser aceitas.
O cadastro de transação deverá conter:

- Identificador (deve ser um número inteiro sequencial único gerado automaticamente);
- Descrição (texto);
- Valor (número decimal positivo);

Tipo (despesa/receita);
Pessoa (inteiro identificador da pessoa do cadastro anterior);
Esse valor precisa existir no cadastro de pessoa;

Consulta de totais:

Deverá listar todas as pessoas cadastradas, exibindo o total de receitas, despesas e o saldo (receita – despesa) de cada uma.
Ao final da listagem anterior, deverá exibir o total geral de todas as pessoas incluindo o total de receitas, total de despesas e o saldo líquido.

