//Using são como estiquetas que identificam as ferramentas
using MauiAppMinhasComprasNovo.Models; //pois usaremos a pasta models
using SQLite; //vem do NuGet

namespace MauiAppMinhasComprasNovo.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn; //só para leitura, é assincrona e faz a conexão com o sqlite 

        public SQLiteDatabaseHelper(string path)//é o construtor da classe, é o caminho para o arquivo
        {
            _conn = new SQLiteAsyncConnection(path);//conexão com o caminho
            _conn.CreateTableAsync<Produto>().Wait();//cria a tabela produto e espera a tarefa se concluir
        }

        public Task<int> Insert(Produto p) //insere o produto na tabela
        {
            return _conn.InsertAsync(p);//retorna uma tarefa no arquivo de texto, de forma assíncrona
        }

        public Task<List<Produto>> Update(Produto p)//atualizar
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";//é a diretiva sql

            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);//qual vai ser a tabela
        }

        public Task <int> Delete(int id)//deletar
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);//para saber quantos produtos foram excluídos na operação
        }

        public Task<List<Produto>> GetAll()//listar todos os produtos
        {
            return _conn.Table<Produto>().ToListAsync();//retorna uma array de objetos
        }

        public Task<List<Produto>> Search(string q) //pesquisar no banco de dados, pesquisa instantânea
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }

    }
}
