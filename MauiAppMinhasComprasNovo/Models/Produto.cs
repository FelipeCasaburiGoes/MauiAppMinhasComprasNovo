using SQLite;

namespace MauiAppMinhasComprasNovo.Models
{
    public class Produto
    {
        string _descricao;		//VALIDA A DESCRICAO

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao; 		//É O RETORNO DA DESCRICAO
            set
            {
                if (value == null) 		//SE VALOR FOR NULO, DISPARA A EXCEÇÃO COM A MENSAGEM ABAIXO
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                _descricao = value;   //O VALOR ENTRA NA DESCRICAO
            }
        }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public double Total { get => Quantidade * Preco; }
    }
}
