using MauiAppMinhasComprasNovo.Models;

namespace MauiAppMinhasComprasNovo.Views;

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try � � � � �			//TAMB�M TEM O TRYCATCH
� � � � {
            Produto produto_anexado = BindingContext as Produto;	//PRODUTO NA PR�PRIA TELA

� � � � � � Produto p = new Produto			//TA PEGANDO DA INTERFAE GRAFICA
� � � � � � {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            await App.Db.Update(p);						//SE TRATA DA ATUALIZA��O
� � � � � � await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");	//REGISTRO ATUALIZADO
� � � � � � await Navigation.PopAsync();					//REGRESSA A TELA DE ORIGEM
� � � � }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}