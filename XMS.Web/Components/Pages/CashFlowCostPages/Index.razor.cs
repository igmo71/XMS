using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Web.Components.Pages.CashFlowCostPages
{
    public partial class Index
    {
        [Inject] public ICostCategoryService CategoryService { get; set; } = default!;
        [Inject] public ICostCategoryItemService CategodyItemService { get; set; } = default!;
        [Inject] public ICashFlowItemService CashFlowItemService { get; set; } = default!;
        [Inject] public ICashFlowCostService CashFlowCostService { get; set; } = default!;
        [Inject] public ILogger<Index> Logger { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private IReadOnlyList<CostCategory> _costCategories = [];
        private IReadOnlyList<CostCategoryItem> _costCategoryItems = [];
        private ILookup<Guid, CashFlowCost> _cashFlowCostLookup = default!;
        private IReadOnlyList<CashFlowItem> _cashFlowItems = [];
        private IReadOnlyList<TreeItemData<CashFlowItem>> _cashFlowItemsTree = [];

        private bool _isLoading;
        private bool _includeDeleted = false;

        protected override async Task OnInitializedAsync()
        {
            var startingTimestamp = Stopwatch.GetTimestamp();
            await LoadDataAsync();
            Logger.LogDebug("{Source} {Elapsed}", nameof(OnInitializedAsync), Stopwatch.GetElapsedTime(startingTimestamp));
        }

        private async Task LoadDataAsync()
        {
            var startingTimestamp = Stopwatch.GetTimestamp();

            if (_isLoading) return;

            _isLoading = true;
            try
            {
                await Task.WhenAll(
                    LoadCostCategories(),
                    LoadCostCategoryItems(),
                    LoadCashFlowItems(),
                    LoadCashFlowCosts());

                BuildCashFlowItemsTree();

                Logger.LogDebug("{Source} {Elapsed}", nameof(LoadDataAsync), Stopwatch.GetElapsedTime(startingTimestamp));
            }
            finally
            {
                _isLoading = false;

                StateHasChanged();
            }
        }

        private async Task LoadCostCategories() => _costCategories = await CategoryService.GetListAsync(_includeDeleted, _cts.Token);
        private async Task LoadCostCategoryItems() => _costCategoryItems = await CategodyItemService.GetListAsync(_cts.Token);
        private async Task LoadCashFlowItems() => _cashFlowItems = await CashFlowItemService.GetListAsync(includeDeleted: false, _cts.Token);
        private async Task LoadCashFlowCosts()
        {
            IReadOnlyList<CashFlowCost> cashFlowCostList = await CashFlowCostService.GetListAsync(_cts.Token);
            _cashFlowCostLookup = cashFlowCostList.ToLookup(e => e.CostCategoryItemId);
        }

        private void BuildCashFlowItemsTree()
        {
            _cashFlowItemsTree = ExecuteBuildTree(_cashFlowItems, Guid.Parse("00000000-0000-0000-0000-000000000000"));

            StateHasChanged();
        }

        private static List<TreeItemData<CashFlowItem>> ExecuteBuildTree(IEnumerable<CashFlowItem> allItems, Guid? parentId)
        {
            var list = allItems
                .Where(e => e.ParentId == parentId).ToList();
            var result = list
            .Select(e => new TreeItemData<CashFlowItem>
            {
                Value = e,
                Text = e.Name,
                Expanded = false,
                Children = ExecuteBuildTree(allItems, e.Id)
            })
            .ToList();

            return result;
        }

        private async Task DeleteAddCashFlowCost(Guid itemId)
        {
            await CashFlowCostService.DeleteCashFlowCostAsync(itemId, _cts.Token);
            await LoadDataAsync();
            StateHasChanged();
        }

        private async Task UpdateCashFlowCosts(List<CashFlowCost> items)
        {
            await CashFlowCostService.UpdateRangeCashFlowCostAsync(items, _cts.Token);
            await LoadDataAsync();
            StateHasChanged();
        }
    }
}
