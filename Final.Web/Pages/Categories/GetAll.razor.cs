using Final.Core.Models;
using Final.Core.Requests.Categories;
using Final.Core.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Final.Web.Pages.Categories;

public partial class GetAllCategoriesPage : ComponentBase
{
    #region Properties   

    public bool IsBusy { get; set; } = false;
    public List<Category> Categories { get; set; } = new();

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;
    [Inject]
    public IDialogService Dialog { get; set; } = default!;
    [Inject]
    public ICategoryService Service { get; set; } = default!;
    
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        this.IsBusy = true;

        try
        {
            var request = new GetAllCategoriesRequest();

            var response = await Service.GetAllAsync(request);
            if (response.IsSuccess)
                Categories = response.Data ?? new List<Category>();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            this.IsBusy = false;
        }
    }

    #endregion

    #region Methods
    
    public async void OnDeleteClick(long id, string title)
    {
        var response = await Dialog.ShowMessageBox("Atenção", $"Ao prosseguir a categoria {title} será removida. Deseja continuar?", yesText: "Excluir", cancelText: "Cancelar");
        
        if (response is true)
            await OnDeleteAsync(id, title);

        await InvokeAsync(StateHasChanged);
    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteCategoryRequest { Id = id };

            await Service.DeleteAsync(request);

            Categories.RemoveAll(x => x.Id == id);

            Snackbar.Add($"Categoria {title} excluída com sucesso", Severity.Success);
        }   
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    #endregion
}