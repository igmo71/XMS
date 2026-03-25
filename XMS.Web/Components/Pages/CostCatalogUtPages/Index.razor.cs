using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics;
using XMS.Integration.OneC;
using XMS.Integration.OneC.Api;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Web.Components.Pages.CostCatalogUtPages
{
    public partial class Index
    {
        [Inject] public ICostCategoryService CategoryService { get; set; } = default!;
        [Inject] public ICostCategoryItemService CategodyItemService { get; set; } = default!;
        [Inject] public IOneCUtService UtService { get; set; } = default!;
        [Inject] public ICostCatalogUtService CostCatalogUtService { get; set; } = default!;
        [Inject] public ILogger<Index> Logger { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private ILookup<Guid?, CostCategory> _costCategoriesLookup = default!;
        private ILookup<Guid, CostCatalogUt> _costCatalogUtLookup = default!;
        private Dictionary<(Guid CostCategoryId, Guid CostItemId), Guid> _costCategoryItemMap = [];
        private IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств> _catalogUtItems = [];
        private IReadOnlyList<TreeItemData<Catalog_СтатьиДвиженияДенежныхСредств>> _catalogUtItemsTree = [];

        private bool _isLoading;
        private bool _includeDeleted = false;
        private CatalogQueryParameters _catalogQueryParameters = new();

        protected override async Task OnInitializedAsync()
        {
            var startingTimestamp = Stopwatch.GetTimestamp();

            await LoadDataAsync();
            BuildCostCatalogUtItemsTree();

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
                    LoadCatalogUtItems(),
                    LoadCostCatalogUt());
            }
            finally
            {
                _isLoading = false;

                //StateHasChanged();
            }

            Logger.LogDebug("{Source} {Elapsed}", nameof(LoadDataAsync), Stopwatch.GetElapsedTime(startingTimestamp));
        }

        private async Task LoadCostCategories()
        {
            var costCategories = await CategoryService.GetListAsync(_includeDeleted, _cts.Token);

            _costCategoriesLookup = costCategories.ToLookup(e => e.ParentId);
        }

        private async Task LoadCatalogUtItems() => _catalogUtItems = await UtService.GetCatalog_СтатьиДвиженияДенежныхСредств_Async(_catalogQueryParameters, _cts.Token);

        private async Task LoadCostCategoryItems()
        {
            var costCategoryItems = await CategodyItemService.GetListAsync(_cts.Token);
            _costCategoryItemMap = costCategoryItems
                .GroupBy(e => (e.CategoryId, e.ItemId))
                .ToDictionary(g => g.Key, g => g.First().Id);
        }

        private async Task LoadCostCatalogUt()
        {
            var costCatalogUtItems = await CostCatalogUtService.GetListAsync(_cts.Token);
            _costCatalogUtLookup = costCatalogUtItems.ToLookup(e => e.CostCategoryItemId);
        }

        private void BuildCostCatalogUtItemsTree()
        {
            var startingTimestamp = Stopwatch.GetTimestamp();

            if (_isLoading) return;

            _isLoading = true;

            ILookup<Guid?, Catalog_СтатьиДвиженияДенежныхСредств> catalogItemsLookup = _catalogUtItems.ToLookup(e => e.Parent_Key);

            _catalogUtItemsTree = ExecuteBuildTree(catalogItemsLookup, Guid.Empty);

            //StateHasChanged();

            _isLoading = false;

            Logger.LogDebug("{Source} {Elapsed}", nameof(BuildCostCatalogUtItemsTree), Stopwatch.GetElapsedTime(startingTimestamp));
        }

        private static List<TreeItemData<Catalog_СтатьиДвиженияДенежныхСредств>> ExecuteBuildTree(ILookup<Guid?, Catalog_СтатьиДвиженияДенежныхСредств> catalogItemsLookup, Guid? parentId)
        {
            var result = catalogItemsLookup[parentId].Select(e => new TreeItemData<Catalog_СтатьиДвиженияДенежныхСредств>
            {
                Value = e,
                Text = e.Description,
                Expanded = false,
                Children = ExecuteBuildTree(catalogItemsLookup, e.Ref_Key)
            }).ToList();

            return result;
        }

        private async Task DeleteCostCatalogUt(Guid itemId)
        {
            await CostCatalogUtService.DeleteAsync(itemId, _cts.Token);
            await LoadDataAsync();
            StateHasChanged();
        }

        private async Task UpdateCostCatalogUt(List<CostCatalogUt> items)
        {
            await CostCatalogUtService.UpdateRangeAsync(items, _cts.Token);
            await LoadDataAsync();
            StateHasChanged();
        }
    }
}
