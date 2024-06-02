using Final.Core.Requests.Categories;
using Final.Core.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Final.Web.Pages.Categories;

public partial class CreateCategoryPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreateCategoryRequest Request { get; set; } = new();

    #endregion

    #region Services

    [Inject]
    public ICategoryService Service { get; set; } = default!;
    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    #endregion

    #region Methods
    
    public async Task OnValidSubmitAsync()
    {
        this.IsBusy = true;

        try
        {
            var response = await Service.CreateAsync(Request);
            if (response.IsSuccess)
            {
                Snackbar.Add(response.Message, Severity.Success);
                Navigation.NavigateTo("/categorias");
            }
            else
            {
               Snackbar.Add(response.Message, Severity.Error);
            }
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
}