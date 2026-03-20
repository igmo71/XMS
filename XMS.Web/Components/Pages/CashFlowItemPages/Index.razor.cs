using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;
using XMS.Integration.OneC;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.Models;
using XMS.Web.Components.Common;

namespace XMS.Web.Components.Pages.CashFlowItemPages
{
    public partial class Index
    {
        [Inject] public IOneCUtService UtService { get; set; } = default!;
        [Inject] public ProtectedSessionStorage SessionStorage { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств> _catalogUtItems = [];
        private IReadOnlyList<TreeItemData<Catalog_СтатьиДвиженияДенежныхСредств>> _treeItems = [];
        private HashSet<Guid> _expandedCatalogUtIds = [];
        private bool _expandedAll;
        private bool _isLoading;
        private bool _isReloading;
        private CatalogQueryParameters _catalogQueryParameters = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAndBuildTreeAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var result = await SessionStorage.GetAsync<HashSet<Guid>>(nameof(_expandedCatalogUtIds));
                _expandedCatalogUtIds = result.Success ? (result.Value ?? []) : [];
                _expandedAll = _expandedCatalogUtIds.Count > 0 && _expandedCatalogUtIds.Count == _catalogUtItems.Count;
                BuildTree();
                StateHasChanged();
            }
        }

        private async Task LoadDataAndBuildTreeAsync()
        {
            await LoadDataAsync();

            BuildTree();
        }

        private async Task LoadDataAsync()
        {
            if (_isLoading) return;

            _isLoading = true;
            try
            {
                _catalogUtItems = await UtService.GetCatalog_СтатьиДвиженияДенежныхСредств_Async(_catalogQueryParameters, _cts.Token);
            }
            finally
            {
                _isLoading = false;

                StateHasChanged();
            }
        }

        private void BuildTree()
        {
            _treeItems = ExecuteBuildTree(_catalogUtItems, Guid.Empty, _expandedCatalogUtIds);

            StateHasChanged();
        }

        private static List<TreeItemData<Catalog_СтатьиДвиженияДенежныхСредств>> ExecuteBuildTree(IEnumerable<Catalog_СтатьиДвиженияДенежныхСредств> allItems, Guid? parentKey, HashSet<Guid>? _expandedIds = null)
        {
            var list = allItems
                .Where(e => e.Parent_Key == parentKey).ToList();
            var result = list
            .Select(e => new TreeItemData<Catalog_СтатьиДвиженияДенежныхСредств>
            {
                Value = e,
                Text = e.Description,
                Expanded = _expandedIds is not null && _expandedIds.Contains(e.Ref_Key),
                Children = ExecuteBuildTree(allItems, e.Ref_Key, _expandedIds)
            })
            .ToList();

            return result;
        }

        private async Task ExpandAll()
        {
            _treeItems.SetExpansion(true);
            _expandedAll = true;
            _expandedCatalogUtIds = _catalogUtItems.Select(e => e.Ref_Key).ToHashSet();
            await SessionStorage.SetAsync(nameof(_expandedCatalogUtIds), _expandedCatalogUtIds);
        }

        private async Task CollapseAll()
        {
            _treeItems.SetExpansion(false);
            _expandedAll = false;
            _expandedCatalogUtIds = [];
            await SessionStorage.SetAsync(nameof(_expandedCatalogUtIds), _expandedCatalogUtIds);
        }

        private async Task ToggleIncludeDeleted(bool args)
        {
            _catalogQueryParameters.IncludeDeleted = args;

            await LoadDataAndBuildTreeAsync();
        }

        private async Task ExpandedChanged(ITreeItemData<Catalog_СтатьиДвиженияДенежныхСредств?> node, bool expanded)
        {
            node.Expanded = expanded;

            if (node.Value is Catalog_СтатьиДвиженияДенежныхСредств cashFlowItem)

            {
                if (expanded)
                    _expandedCatalogUtIds.Add(cashFlowItem.Ref_Key);
                else
                    _expandedCatalogUtIds.Remove(cashFlowItem.Ref_Key);

                await SessionStorage.SetAsync(nameof(_expandedCatalogUtIds), _expandedCatalogUtIds);
            }
        }

        private static string GetIcon(ITreeItemData<Catalog_СтатьиДвиженияДенежныхСредств> item)
        {
            if (item.HasChildren)
                return item.Expanded ? Icons.Material.Filled.FolderOpen : Icons.Material.Filled.Folder;

            return Icons.Material.Filled.Description;
        }

        private async Task ReloadDataFromOneSAsync()
        {
            _isReloading = true;
            try
            {
                Snackbar.Add("Синхронизация с 1С...", Severity.Info);

                await UtService.ResyncAsync(_cts.Token);

                Snackbar.Add("Данные успешно синхронизированы с 1С", Severity.Success);

                await LoadDataAndBuildTreeAsync();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Ошибка при обмене с 1С: {ex.Message}", Severity.Error);
            }
            finally
            {
                _isReloading = false;
            }
        }
    }
}
