using App_Compras_MAUI.Model;

using System.Collections.ObjectModel;

namespace App_Compras_MAUI.View;

public partial class List : ContentPage
{
    ObservableCollection<Model.Product> products_list = new ObservableCollection<Model.Product>();

    string? search_term = null;


    public List()
	{
		InitializeComponent();

        clv_produtos.ItemsSource = products_list;
	}

    private async Task LoadTableData()
    {
        srcbar_produtos.IsEnabled = false;

        try
        {
            products_list.Clear();

            Helper.Modules.Product product_helper = new Helper.Modules.Product();

            List<Model.Product> registers = await ((String.IsNullOrWhiteSpace(this.search_term)) ? product_helper.List() : product_helper.Find(search_term));

            if (!String.IsNullOrWhiteSpace(this.search_term) && registers.Count <= 0)
            {
                throw new Exception("Nenhum registro encontrado com esse termo de pesquisa.");
            }

            registers.ForEach(register => products_list.Add(register));

            if (this.search_term == null)
            {
                srcbar_produtos.Text = String.Empty;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
        finally
        {
            this.search_term = null;

            srcbar_produtos.IsEnabled = true;

            rfsv_produtos.IsRefreshing = false;
        }
    }

    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            rfsv_produtos.IsRefreshing = true;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void swit_update_Invoked(object sender, EventArgs e)
    {
        try
        {
            SwipeItem item = (SwipeItem)sender;

            Model.Product selected_product = (Model.Product)item.BindingContext;

            await Navigation.PushAsync(new Form()
            {
                BindingContext = selected_product
            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void swit_delete_Invoked(object sender, EventArgs e)
    {
        try
        {
            SwipeItem item = (SwipeItem)sender;

            Model.Product selected_product = (Model.Product)item.BindingContext;

            if (await DisplayAlertAsync("Atenção!", $"Realmente deseja excluir o produto '{selected_product.Description}'?", "OK", "Cancelar"))
            {
                int rows_affected = await (new Helper.Modules.Product()).Delete(selected_product.Id);

                products_list.Remove(selected_product);

                if (rows_affected > 0)
                {
                    await DisplayAlertAsync("Atenção!", "Produto deletado com sucesso.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void swit_details_Invoked(object sender, EventArgs e)
    {
        try
        {
            SwipeItem item = (SwipeItem)sender;

            Model.Product selected_product = (Model.Product)item.BindingContext;

            string product_data = $"Descrição: {selected_product.Description}\n\nQuantidade: {selected_product.Quantity}\n\nPreço: {selected_product.Price.ToString("C2")}\n\nTotal: {selected_product.Total_Cost.ToString("C2")}";

            await DisplayAlertAsync($"Dados do Produto (#{selected_product.Id})", product_data, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void btn_novo_produto_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new View.Form());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void btn_total_gastos_Clicked(object sender, EventArgs e)
    {
        try
        {
            double product_total_costs_sum = this.products_list.Sum(product => product.Total_Cost);

            await DisplayAlertAsync("Atenção!", $"Quantidade de Produtos: {products_list.Count}\n\nGasto Total: {product_total_costs_sum.ToString("C2")}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void rfsv_produtos_Refreshing(object sender, EventArgs e)
    {
        // Esse evento é disparado toda vez que a propriedade "IsRefreshing" do "RefreshView" tem seu valor alterado para "True".

        try
        {
            await LoadTableData();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void srcbar_produtos_SearchButtonPressed(object sender, EventArgs e)
    {
        // Esse evento é disparado toda vez que o usuário pressiona "Enter/Lupa" no teclado ao interagir com a "SearchBar".

        try
        {
            this.search_term = srcbar_produtos.Text.Trim();

            rfsv_produtos.IsRefreshing = true;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }
}