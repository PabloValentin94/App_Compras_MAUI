# Aplicativo de Lista de Compras (**Windows**)

O objetivo desse aplicativo é disponibilizar uma lista de compras gerenciável (CRUD).

## Persistência de Dados

Os dados salvos são armazenados dentro de um arquivo **SQLite** (**.db3**), que simula o funcionamento de bancos de dados, como o MySQL, por exemplo.

## Funcionalidades

Dentre os recursos implementados, estão:

- **Cadastro de Itens**: é possível fornecer dados como descrição, quantidade e preço unitário para criar um item.

- **Edição de Itens**: é possível atualizar dados como descrição, quantidade e preço unitário para editar um item.

- **Deleção de Itens**: é possível excluir um item específico selecionado. É exigida uma confirmação antes de executar essa ação.

- **Visualização Específica**: é possível visualizar dados específicos de um item da lista de compras, que são sua descrição, quantidade comprada, preço unitário e valor total a ser pago por ele individualmente.

- **Visualização Geral**:  é possível visualizar dados gerais da lista de compras, que são a quantidade de itens da lista e valor total a ser pago por tudo nela.

- **Listagem Geral**: é possível visualizar todos os itens salvos no SQLite através de uma listagem geral.

- **Pesquisa de Itens**: é possível pesquisar itens da lista de compras com base em parte de sua descrição. Para resetar a listagem geral, basta apertar o botão de pesquisa com o campo em branco.

## Diferencial

Essa versão foi pensada para funcionar sem recursos do Android, como o "SwipeItem" e o "RefreshView". Veja como as limitações foram contornadas a seguir:

- **SwipeItem**: como o Windows não permite a implementação desse recurso, foi adicionado um botão de opções para cada item que exibe as ações disponíveis.

- **RefreshView**: como esse recurso não é acionado pelo Windows, a listagem é resetada de outra forma, como foi especificado anteriormente.
