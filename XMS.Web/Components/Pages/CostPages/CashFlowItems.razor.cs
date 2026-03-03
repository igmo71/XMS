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
        private IEnumerable<CostItem> _costItems = [];
        private IReadOnlyList<CostCategory> _costCategories = [];
        private IReadOnlyList<CostCategoryItem> _costCategoryItems = [];
        private IReadOnlyList<CashFlowItem> _сashFlowItems = [];
        private IEnumerable<Department> _departments = [];
        private IEnumerable<Employee> _employees = [];
        private bool _isLoading;
        private bool _includeDeleted = false;

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
                    LoadCostItems(),
                    LoadCostCategoryItems(),
                    LoadCashFlowItems(),
                    LoadDepartments(),
                    LoadEmployees());
            }
            finally
            {
                _isLoading = false;

                StateHasChanged();
            }
        }


        private async Task LoadCostCategories() => _costCategories = await CategoryService.GetListAsync(_includeDeleted, _cts.Token);
        private async Task LoadCostItems() => _costItems = await ItemService.GetListAsync(true, _cts.Token);
        private async Task LoadCostCategoryItems() => _costCategoryItems = await CategodyItemService.GetListAsync(hasCashFlowOnly: false, _cts.Token);
        private async Task LoadCashFlowItems() => _сashFlowItems = await CashFlowItemService.GetListAsync(false, _cts.Token);
        private async Task LoadDepartments() => _departments = await DepartmentService.GetListAsync(false, _cts.Token);
        private async Task LoadEmployees() => _employees = await EmployeeService.GetListAsync(false, _cts.Token);
    }
}
