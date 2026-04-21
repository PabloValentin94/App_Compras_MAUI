using App_Compras_MAUI.Model;
using App_Compras_MAUI.Helper;

namespace App_Compras_MAUI.View;

public partial class Form : ContentPage
{
	public Form()
	{
		InitializeComponent();
	}

    private async void btn_voltar_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void btn_salvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            Model.Product? context = (Model.Product?)this.BindingContext;

            Model.Product product = new Model.Product()
            {
                Id = (context != null && context.Id > 0) ? context.Id : 0,
                Description = txt_descricao.Text,
                Quantity = int.Parse(txt_quantidade.Text),
                Price = double.Parse(txt_preco.Text)
            };

            Helper.Modules.Product products_manager = new Helper.Modules.Product();

            if (await products_manager.Save(product) > 0)
            {
                await DisplayAlertAsync("Atenção!", "Produto salvo com sucesso.", "OK");

                await Navigation.PopAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }
}