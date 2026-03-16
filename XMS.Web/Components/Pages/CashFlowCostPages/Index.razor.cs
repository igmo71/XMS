using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

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
        private ILookup<Guid, CashFlowCost> _cashFlowCostLookup = default!;
        private Dictionary<(Guid CostCategoryId, Guid CostItemId), Guid> _costCategoryItemMap = [];
        private IReadOnlyList<CashFlowItem> _cashFlowItems = [];
        private IReadOnlyList<TreeItemData<CashFlowItem>> _cashFlowItemsTree = [];

        private bool _isLoading;
        private bool _includeDeleted = false;

        protected override async Task OnInitializedAsync()
        {
            var startingTimestamp = Stopwatch.GetTimestamp();

            await LoadDataAsync();
            BuildCashFlowItemsTree();

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
            }
            finally
            {
                _isLoading = false;

                //StateHasChanged();
            }

            Logger.LogDebug("{Source} {Elapsed}", nameof(LoadDataAsync), Stopwatch.GetElapsedTime(startingTimestamp));
        }

        private async Task LoadCostCategories() => _costCategories = await CategoryService.GetListAsync(_includeDeleted, _cts.Token);

        private async Task LoadCashFlowItems() => _cashFlowItems = await CashFlowItemService.GetListAsync(includeDeleted: false, _cts.Token);

        private async Task LoadCostCategoryItems()
        {
            var costCategoryItems = await CategodyItemService.GetListAsync(_cts.Token);
            _costCategoryItemMap = costCategoryItems
                .GroupBy(e => (e.CategoryId, e.ItemId))
                .ToDictionary(g => g.Key, g => g.First().Id);
        }

        private async Task LoadCashFlowCosts()
        {
            var cashFlowCostList = await CashFlowCostService.GetListAsync(_cts.Token);
            _cashFlowCostLookup = cashFlowCostList.ToLookup(e => e.CostCategoryItemId);
        }

        private void BuildCashFlowItemsTree()
        {
            var startingTimestamp = Stopwatch.GetTimestamp();

            if (_isLoading) return;

            _isLoading = true;

            ILookup<Guid?, CashFlowItem> cashFlowItemLookup = _cashFlowItems.ToLookup(e => e.ParentId);

            _cashFlowItemsTree = ExecuteBuildTree(cashFlowItemLookup, Guid.Parse("00000000-0000-0000-0000-000000000000"));

            //StateHasChanged();

            _isLoading = false;

            Logger.LogDebug("{Source} {Elapsed}", nameof(BuildCashFlowItemsTree), Stopwatch.GetElapsedTime(startingTimestamp));
        }

        private static List<TreeItemData<CashFlowItem>> ExecuteBuildTree(ILookup<Guid?, CashFlowItem> cashFlowItemLookup, Guid? parentId)
        {
            var result = cashFlowItemLookup[parentId].Select(e => new TreeItemData<CashFlowItem>
            {
                Value = e,
                Text = e.Name,
                Expanded = false,
                Children = ExecuteBuildTree(cashFlowItemLookup, e.Id)
            }).ToList();

            return result;
        }

        private async Task DeleteCashFlowCost(Guid itemId)
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
