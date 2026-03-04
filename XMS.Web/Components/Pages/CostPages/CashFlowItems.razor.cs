using Microsoft.AspNetCore.Components;
using MudBlazor;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Web.Components.Pages.CostPages
{
    public partial class CashFlowItems
    {
        [Inject] public ICostCategoryService CategoryService { get; set; } = default!;
        [Inject] public ICostItemService ItemService { get; set; } = default!;
        [Inject] public ICostCategoryItemService CategodyItemService { get; set; } = default!;
        [Inject] public ICashFlowItemService CashFlowItemService { get; set; } = default!;
        [Inject] public IDepartmentService DepartmentService { get; set; } = default!;
        [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private IReadOnlyList<CostCategory> _costCategories = [];
        private IReadOnlyList<CostCategoryItem> _costCategoryItems = [];
        private IReadOnlyList<CashFlowItem> _cashFlowItems = [];
        private bool _isLoading;
        private bool _includeDeleted = false;
        private RecursiveTable? _recursiveTable;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            if (_isLoading) return;

            _isLoading = true;
            try
            {
                await Task.WhenAll(
                    LoadCostCategories(),
                    LoadCostCategoryItems(),
                    LoadCashFlowItems());
            }
            finally
            {
                _isLoading = false;

                StateHasChanged();
            }
        }

        private async Task LoadCostCategories() => _costCategories = await CategoryService.GetListAsync(_includeDeleted, _cts.Token);
        private async Task LoadCostCategoryItems() => _costCategoryItems = await CategodyItemService.GetListAsync(hasCashFlowOnly: false, _cts.Token);
        private async Task LoadCashFlowItems() => _cashFlowItems = await CashFlowItemService.GetListAsync(false, _cts.Token);

        private async Task AddCashFlowItemLink(CostCategoryItem args)
        {
            await CategodyItemService.AddCashFlowItemLinkAsync(args, _cts.Token);
            await LoadDataAsync();
            StateHasChanged();
        }

        private async Task DeleteCashFlowItemLink(CostCategoryItem args)
        {
            await CategodyItemService.DeleteCashFlowItemLinkAsync(args, _cts.Token);
            await LoadDataAsync();
            StateHasChanged();
        }
    }
}
