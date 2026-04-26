using App_Compras_MAUI.Utils;

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

    private async void DeleteProduct(Model.Product product)
    {
        try
        {
            if (await DisplayAlertAsync("Atenção!", $"Realmente deseja excluir o produto '{product.Description}'?", "OK", "Cancelar"))
            {
                int rows_affected = await (new Helper.Modules.Product()).Delete(product.Id);

                products_list.Remove(product);

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

    private async void ShowProductData(Model.Product product)
    {
        try
        {
            string product_data = $"Descrição: {product.Description}\n\nQuantidade: {product.Quantity}\n\nPreço: {product.Price.ToString("C2")}\n\nTotal: {product.Total_Cost.ToString("C2")}";

            await DisplayAlertAsync($"Dados do Produto (#{product.Id})", product_data, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void UpdateProduct(Model.Product product)
    {
        try
        {
            await Navigation.PushAsync(new Form()
            {
                BindingContext = product
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

            DeleteProduct(selected_product);
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

            ShowProductData(selected_product);
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

            UpdateProduct(selected_product);
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

    private async void btn_options_Clicked(object sender, EventArgs e)
    {
        try
        {
            Button item = (Button)sender;

            Model.Product selected_product = (Model.Product)item.BindingContext;

            string choice = await DisplayActionSheetAsync($"{selected_product.Description} (#{selected_product.Id})", ItemContextActions.Cancel, ItemContextActions.Delete, ItemContextActions.Details, ItemContextActions.Edit);

            switch (choice)
            {
                case ItemContextActions.Delete:
                    DeleteProduct(selected_product);
                break;

                case ItemContextActions.Details:
                    ShowProductData(selected_product);
                break;

                case ItemContextActions.Edit:
                    UpdateProduct(selected_product);
                break;
            }
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
}