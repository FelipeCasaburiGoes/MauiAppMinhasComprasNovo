using MauiAppMinhasComprasNovo.Models;
using System.Collections.ObjectModel;


namespace MauiAppMinhasComprasNovo.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()		//foi adicionado um TryCatch p/ o app ñ crachear na cara do usuário.
    {
        try
        {
            lista.Clear();		//REMOVE TODOS OS ITENS, TODA VEZ QUE ABRIR A TELA DE LISTAGEM E RECARREGA A LISTA

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) //na busca, também foi adicionado o TryCatch
    {
        try
        {
            string q = e.NewTextValue;

            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)    //excluir
    {
        try
        {
            MenuItem selecinado = sender as MenuItem; //SEMPRE QUE CLICAR NO MENU ITEM, VAI CHEGAR QUAL FOI O SELECIONADO

            Produto p = selecinado.BindingContext as Produto;

            bool confirm = await DisplayAlert(  //CONFIRMA COM O USUÁRIO SE ELE QUER MESMO REMOVER O ITEM
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id); //SE A RESPOSTA FOR SIM, VAI REMOVER O PRODUTO LA DO BANCO DE DADOS
                lista.Remove(p);//REMOVE O ITEM DA LISTA QUE FOI SELECIONADO
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto; //PEGOU O PRODUTO SELECIONADO

            Navigation.PushAsync(new Views.EditarProduto //VAI MANDAR PRA TELA DE EDITAR PRODUTO	
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}


