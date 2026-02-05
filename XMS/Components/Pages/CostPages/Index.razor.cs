using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using XMS.Components.Common;
using XMS.Core.Abstractions;
using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Domain;

namespace XMS.Components.Pages.CostPages
{
    public partial class Index : IDisposable
    {
        [Inject] public ICostCategoryService CategoryService { get; set; } = default!;
        [Inject] public ICostItemService ItemService { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private MudDataGrid<CostItem> _itemGrid = default!;
        private IReadOnlyList<CostItem> _items = [];
        private IReadOnlyList<CostCategory> _categoriesList = [];
        private IReadOnlyList<CostCategory> _categoriesFullList = [];
        private IReadOnlyList<CostCategory> _categoriesFlattenedTree = [];
        private IReadOnlyList<TreeItemData<object?>> _costTree = [];
        private bool _expandedAll;
        private bool _isLoading;
        private bool _isProcessing;

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
                _items = await ItemService.GetListAsync(_cts.Token);

                _categoriesList = await CategoryService.GetListAsync(_cts.Token);

                _categoriesFlattenedTree = TreeHelper.BuildFlattenedTree(_categoriesList);

                _categoriesFullList = await CategoryService.GetFullListAsync(_cts.Token);

                _costTree = BuildTree(_categoriesFullList, null);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private static List<TreeItemData<object?>> BuildTree(IEnumerable<CostCategory> categories, Guid? parentId)
        {
            var lookup = categories.OrderBy(e => e.Name).ToLookup(e => e.ParentId);
            return BuildTreeRecursive(lookup, parentId);
        }

        private static List<TreeItemData<object?>> BuildTreeRecursive(ILookup<Guid?, CostCategory> lookup, Guid? parentId)
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
                    Expanded = false
                });
            }
            return nodes;
        }

        private void ExpandAll()
        {
            _costTree.SetExpansion(true);
            _expandedAll = true;
        }
        private void CollapseAll()
        {
            _costTree.SetExpansion(false);
            _expandedAll = false;
        }

        // CostItem Operations //

        private async Task NewItemAsync(MouseEventArgs args)
        {
            var item = new CostItem();

            await _itemGrid.SetEditingItemAsync(item);
        }

        private async Task CommittedItemChanges(CostItem item)
        {
            try
            {
                if (!_items.Any(x => x.Id == item.Id))
                    await ItemService.CreateAsync(item);
                else
                    await ItemService.UpdateAsync(item);

                Snackbar.Add($"Успешно сохранено: {item.Name}", Severity.Success);

                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Ошибка при сохранении {item.Name}: {ex.Message}", Severity.Error);
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

                    await ItemService.DeleteAsync(item.Id);

                    await LoadDataAsync();

                    Snackbar.Add($"Успешно удалено: {item.Name}", Severity.Success);
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Ошибка при удалении {item.Name}: {ex.Message}", Severity.Error);
                }
                finally
                {
                    _isProcessing = false;
                }
            }
        }

        // CostCategory Operations //

        private async Task NewCategoryAsync(object? value)
        {
            if (value is CostCategory parent)
                await CreareOrUpdateCategory(new CostCategory
                {
                    ParentId = parent.Id
                });
        }

        private async Task EditCategoryAsync(object? value)
        {
            if (value is CostCategory category)
                await CreareOrUpdateCategory(category);
        }

        private async Task CreareOrUpdateCategory(CostCategory category)
        {
            var result = await ProcessCategoryDialog(category);

            if (result is { Canceled: false, Data: CostCategory costCategory })
            {
                try
                {
                    await CategoryService.CreateOrUpdateAsync(costCategory, _cts.Token);

                    await LoadDataAsync();

                    Snackbar.Add($"Успешно сохранено: {costCategory.Name}", Severity.Success);
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Ошибка при сохранении {costCategory.Name}: {ex.Message}", Severity.Error);

                    throw;
                }
            }
        }

        private async Task<DialogResult?> ProcessCategoryDialog(CostCategory category)
        {
            var parameters = new DialogParameters<CategoryDialog>
            {
                { x => x.Model, category },
                { x => x.AllItems, _items },
                {x => x.CategoriesFlattenedTree, _categoriesFlattenedTree }
            };

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var title = category.Id == Guid.Empty ? "Создание Категории" : "Редактирование Категории";

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

                        await CategoryService.DeleteAsync(category.Id);

                        await LoadDataAsync();

                        Snackbar.Add($"Успешно удалено: {category.Name}", Severity.Success);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("SAME TABLE REFERENCE"))
                            Snackbar.Add($"Ошибка при удалении {category.Name}: Категория содержит другие категории", Severity.Error);
                        else
                            Snackbar.Add($"Ошибка при удалении {category.Name}: {ex.Message}", Severity.Error);
                    }
                    finally
                    {
                        _isProcessing = false;
                    }
                }
            }
        }

        ////

        private async Task<bool> ConfirmDeleteItemAsync<T>(T item) where T : IHasName
        {
            var parameters = new DialogParameters<ConfirmDialog>
            {
                { x => x.ContentText, $"Вы уверены, что хотите удалить '{item.Name}' навсегда?" },
                { x => x.ButtonText, "Да, удалить" },
                { x => x.ButtonColor, Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Удаление", parameters, options);

            var result = await dialog.Result;

            return result is { Canceled: false };
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
