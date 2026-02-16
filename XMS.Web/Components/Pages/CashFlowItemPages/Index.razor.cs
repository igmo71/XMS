using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;
using XMS.Web.Components.Common;

namespace XMS.Web.Components.Pages.CashFlowItemPages
{
    public partial class Index
    {
        [Inject] public ICashFlowItemService Service { get; set; } = default!;
        [Inject] public ProtectedSessionStorage SessionStorage { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private IReadOnlyList<CashFlowItem> _cashFlowItems = [];
        private List<TreeItemData<CashFlowItem>> _treeItems = [];
        private bool _expandedAll;
        private HashSet<Guid> _expandedCashFlowItemIds = [];
        private bool _isLoading;
        //private bool _isProcessing;
        private bool _ignoreQueryFilters;
        private bool _isReloading;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAndBuildTreeAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var result = await SessionStorage.GetAsync<HashSet<Guid>>(nameof(_expandedCashFlowItemIds));
                _expandedCashFlowItemIds = result.Success ? (result.Value ?? []) : [];
                _expandedAll = _expandedCashFlowItemIds.Count == _cashFlowItems.Count;
                await BuildTreeAsync();
                StateHasChanged();
            }
        }

        private async Task LoadDataAndBuildTreeAsync()
        {
            await LoadDataAsync();

            await BuildTreeAsync();
        }

        private async Task LoadDataAsync()
        {
            if (_isLoading) return;

            _isLoading = true;
            try
            {
                _cashFlowItems = await Service.GetListAsync(_ignoreQueryFilters, _cts.Token);
            }
            finally
            {
                _isLoading = false;

                StateHasChanged();
            }
        }

        private async Task BuildTreeAsync() => _treeItems = BuildTree(_cashFlowItems, Guid.Parse("00000000-0000-0000-0000-000000000000"), _expandedCashFlowItemIds);

        private static List<TreeItemData<CashFlowItem>> BuildTree(IEnumerable<CashFlowItem> allItems, Guid? parentId, HashSet<Guid>? _expandedIds = null)
        {
            var list = allItems
                .Where(e => e.ParentId == parentId).ToList();
            var result = list
            .Select(e => new TreeItemData<CashFlowItem>
            {
                Value = e,
                Text = e.Name,
                Expanded = _expandedIds is not null && _expandedIds.Contains(e.Id),
                Children = BuildTree(allItems, e.Id, _expandedIds)
            })
            .ToList();

            return result;
        }

        private async Task ExpandAll()
        {
            _treeItems.SetExpansion(true);
            _expandedAll = true;
            _expandedCashFlowItemIds = _cashFlowItems.Select(e => e.Id).ToHashSet();
            await SessionStorage.SetAsync(nameof(_expandedCashFlowItemIds), _expandedCashFlowItemIds);
        }
        private async Task CollapseAll()
        {
            _treeItems.SetExpansion(false);
            _expandedAll = false;
            _expandedCashFlowItemIds = [];
            await SessionStorage.SetAsync(nameof(_expandedCashFlowItemIds), _expandedCashFlowItemIds);
        }

        private async Task ToggleQueryFilters(bool args)
        {
            _ignoreQueryFilters = args;

            await LoadDataAndBuildTreeAsync();
        }

        private async Task ExpandedChanged(ITreeItemData<CashFlowItem?> node, bool expanded)
        {
            node.Expanded = expanded;

            if (node.Value is CashFlowItem cashFlowItem)

            {
                if (expanded)
                    _expandedCashFlowItemIds.Add(cashFlowItem.Id);
                else
                    _expandedCashFlowItemIds.Remove(cashFlowItem.Id);

                await SessionStorage.SetAsync(nameof(_expandedCashFlowItemIds), _expandedCashFlowItemIds);
            }
        }

        private static string GetIcon(ITreeItemData<CashFlowItem> item)
        {
            if (item.HasChildren)
                return item.Expanded ? Icons.Material.Filled.FolderOpen : Icons.Material.Filled.Folder;

            return Icons.Material.Filled.Description; //Icons.Material.Filled.CurrencyRuble;
        }

        private async Task ReloadDataFromOneSAsync()
        {
            _isReloading = true;
            try
            {

                Snackbar.Add("Начало синхронизации с 1С...", Severity.Info);

                await Service.ReloadListAsync(_cts.Token);

                Snackbar.Add("Данные успешно синхронизированы с 1С", Severity.Success);

                await LoadDataAsync();
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
