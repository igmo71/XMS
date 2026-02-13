using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Abstractions;
using XMS.Domain.Models;
using XMS.Web.Components.Common;

namespace XMS.Web.Components.Pages.CostPages
{
    public partial class Index : IDisposable
    {
        [Inject] public ICostCategoryService CategoryService { get; set; } = default!;
        [Inject] public ICostItemService ItemService { get; set; } = default!;
        [Inject] public IDepartmentService DepartmentService { get; set; } = default!;
        [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;
        [Inject] private ProtectedSessionStorage SessionStorage { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private MudDataGrid<CostItem> _costItemGrid = default!;
        private IReadOnlyList<CostItem> _costItems = [];
        private IReadOnlyList<CostCategory> _costCategories = [];
        private IReadOnlyList<TreeItemData<object?>> _costTree = [];
        private IReadOnlyList<Department> _departments = [];
        private IReadOnlyList<Employee> _employees = [];
        private bool _expandedAll;
        private bool _isLoading;
        private bool _isProcessing;
        private HashSet<Guid> _expandedCategoryIds = [];

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAndBuildTreeAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var result = await SessionStorage.GetAsync<HashSet<Guid>>(nameof(_expandedCategoryIds));
                _expandedCategoryIds = result.Success ? (result.Value ?? []) : [];
                _expandedAll = _expandedCategoryIds.Count == _costCategories.Count;
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
                await Task.WhenAll(LoadCostCategories(),
                    LoadCostItems(),
                    LoadDepartments(),
                    LoadEmployees());
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task LoadCostCategories() => _costCategories = await CategoryService.GetListAsync(_cts.Token);
        private async Task LoadCostItems() => _costItems = await ItemService.GetListAsync(_cts.Token);
        private async Task LoadDepartments() => _departments = await DepartmentService.GetListAsync(_cts.Token);
        private async Task LoadEmployees() => _employees = await EmployeeService.GetListAsync(_cts.Token);

        private void BuildTree()
        {
            var lookup = _costCategories.OrderBy(e => e.Name).ToLookup(e => e.ParentId);
            _costTree = BuildTreeRecursive(lookup, null);
        }

        private List<TreeItemData<object?>> BuildTreeRecursive(ILookup<Guid?, CostCategory> lookup, Guid? parentId)
        {
            var nodes = new List<TreeItemData<object?>>();

            foreach (var category in lookup[parentId])
            {
                var childrenList = BuildTreeRecursive(lookup, category.Id);

                if (category.Items?.Count > 0)
                {
                    childrenList.AddRange(category.Items.OrderBy(e => e.Name).Select(e => new TreeItemData<object?> { Value = e }));
                }

                nodes.Add(new TreeItemData<object?>
                {
                    Value = category,
                    Children = childrenList,
                    Expanded = _expandedCategoryIds.Contains(category.Id)
                });
            }
            return nodes;
        }

        private async Task ExpandedChanged(ITreeItemData<object?> node, bool expanded)
        {
            node.Expanded = expanded;

            if (node.Value is CostCategory category)
            {
                if (expanded)
                    _expandedCategoryIds.Add(category.Id);
                else
                    _expandedCategoryIds.Remove(category.Id);

                await SessionStorage.SetAsync(nameof(_expandedCategoryIds), _expandedCategoryIds);
            }
        }

        private async Task ExpandAll()
        {
            _expandedAll = true;
            _costTree.SetExpansion(true);
            _expandedCategoryIds = _costCategories.Select(e => e.Id).ToHashSet();
            await SessionStorage.SetAsync(nameof(_expandedCategoryIds), _expandedCategoryIds);
        }

        private async Task CollapseAll()
        {
            _expandedAll = false;
            _costTree.SetExpansion(false);
            _expandedCategoryIds = [];
            await SessionStorage.SetAsync(nameof(_expandedCategoryIds), _expandedCategoryIds);
        }

        // CostCategory Operations //

        private async Task NewCategoryAsync(object? value)
        {
            if (value is CostCategory parent)
                await CreateOrUpdateCategory(new CostCategory
                {
                    ParentId = parent.Id
                });
        }

        private async Task EditCategoryAsync(object? value)
        {
            if (value is CostCategory category)
                await CreateOrUpdateCategory(category);
        }

        private async Task CreateOrUpdateCategory(CostCategory category)
        {
            var result = await ProcessCategoryDialog(category);

            if (result is { Canceled: false, Data: CostCategory costCategory })
            {
                try
                {
                    if (_costCategories?.Any(e => e.Id == costCategory.Id) == false)
                        await CategoryService.CreateAsync(costCategory, _cts.Token);
                    else
                        await CategoryService.UpdateAsync(costCategory, _cts.Token);

                    await LoadDataAndBuildTreeAsync();

                    Snackbar.Add($"Успешно сохранено: {costCategory.Name}", Severity.Success);
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Ошибка при сохранении {costCategory.Name}: {ex.Message}", Severity.Error);
                }
            }
        }

        private async Task<DialogResult?> ProcessCategoryDialog(CostCategory costCategory)
        {
            bool isNew = _costCategories?.Any(e => e.Id == costCategory.Id) == false;

            var title = isNew ? "Создать Категорию" : "Изменить Категорию";

            var parameters = new DialogParameters<CategoryDialog>
            {
                { x => x.TitleIcon, isNew ? Icons.Material.Filled.AddCircleOutline : Icons.Material.Filled.Edit },
                { x => x.CostCategory, costCategory },
                { x => x.CostItems, _costItems },
                { x => x.CostCategories, _costCategories },
                { x => x.Departments, _departments },
                { x => x.Employees, _employees }
            };

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };

            var dialog = await DialogService.ShowAsync<CategoryDialog>(title, parameters, options);

            return await dialog.Result;
        }

        private async Task DeleteCategoryAsync(object? value)
        {
            if (_isProcessing) return;

            if (value is CostCategory category)
            {
                if (await ConfirmDeleteItemAsync(category))
                {
                    try
                    {
                        _isProcessing = true;

                        var result = await CategoryService.DeleteAsync(category.Id);

                        if (result.IsSuccess)
                            Snackbar.Add($"Успешно удалено: {category.Name}", Severity.Success);
                        else
                            Snackbar.Add($"Ошибка при удалении {category.Name}: {result.Error.Description}", Severity.Error);
                    }
                    catch (Exception ex)
                    {
                        Snackbar.Add($"Ошибка при удалении {category.Name}: {ex.Message}", Severity.Error);
                    }
                    finally
                    {
                        _isProcessing = false;

                        await LoadDataAndBuildTreeAsync();
                    }
                }
            }
        }

        // CostItem Operations //

        private async Task NewItemAsync(MouseEventArgs args)
        {
            var item = new CostItem();

            await _costItemGrid.SetEditingItemAsync(item);
        }

        private async Task<DataGridEditFormAction> CommittedItemChanges(CostItem item)
        {
            try
            {
                if (!_costItems.Any(x => x.Id == item.Id))
                    await ItemService.CreateAsync(item);
                else
                    await ItemService.UpdateAsync(item);

                Snackbar.Add($"Успешно сохранено: {item.Name}", Severity.Success);

                await LoadDataAndBuildTreeAsync();

                return DataGridEditFormAction.Close;
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Ошибка при сохранении {item.Name}: {ex.Message}", Severity.Error);

                return DataGridEditFormAction.KeepOpen;
            }
        }

        private async Task DeleteItemAsync(CostItem item)
        {
            if (_isProcessing) return;

            if (await ConfirmDeleteItemAsync(item))
            {
                try
                {
                    _isProcessing = true;

                    var result = await ItemService.DeleteAsync(item.Id);

                    if (result.IsSuccess)
                        Snackbar.Add($"Успешно удалено: {item.Name}", Severity.Success);
                    else
                        Snackbar.Add($"Ошибка при удалении {item.Name}: {result.Error.Description}", Severity.Error);
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Ошибка при удалении {item.Name}: {ex.Message}", Severity.Error);
                }
                finally
                {
                    _isProcessing = false;

                    await LoadDataAndBuildTreeAsync();
                }
            }
        }

        ////

        private async Task<bool> ConfirmDeleteItemAsync<T>(T item) where T : IHasName
        {
            var title = "Удалить Категорию Затрат";

            var parameters = new DialogParameters<ConfirmDialog>
            {
                { x => x.TitleIcon, Icons.Material.Filled.DeleteForever },
                { x => x.ContentText, $"Вы уверены, что хотите удалить '{item.Name}' навсегда?" },
                { x => x.ButtonText, "Да, удалить" },
                { x => x.ConfirmColor, Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>(title, parameters, options);

            var result = await dialog.Result;

            return result is { Canceled: false };
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
