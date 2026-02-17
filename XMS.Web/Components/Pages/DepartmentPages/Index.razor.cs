using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Abstractions;
using XMS.Domain.Models;
using XMS.Web.Components.Common;
using static MudBlazor.CategoryTypes;

namespace XMS.Web.Components.Pages.DepartmentPages
{
    public partial class Index : IDisposable
    {
        [Inject] public IDepartmentService Service { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;
        [Inject] public ProtectedSessionStorage SessionStorage { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private IReadOnlyList<Department> _departments = [];
        private List<TreeItemData<Department>> _treeItems = [];
        private bool _expandedAll;
        private HashSet<Guid> _expandedDepartmentIds = [];
        private bool _isLoading;
        private bool _isProcessing;
        private bool _includeDeleted = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAndBuildTreeAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var result = await SessionStorage.GetAsync<HashSet<Guid>>(nameof(_expandedDepartmentIds));
                _expandedDepartmentIds = result.Success ? (result.Value ?? []) : [];
                _expandedAll = _expandedDepartmentIds.Count == _departments.Count;
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
                _departments = await Service.GetListAsync(_includeDeleted, _cts.Token);
            }
            finally
            {
                _isLoading = false;

                StateHasChanged();
            }
        }

        private void BuildTree()
        {
            _treeItems = TreeHelper.BuildTree(_departments, null, _expandedDepartmentIds);
        }

        private async Task ExpandAll()
        {
            _treeItems.SetExpansion(true);
            _expandedAll = true;
            _expandedDepartmentIds = _departments.Select(e => e.Id).ToHashSet();
            await SessionStorage.SetAsync(nameof(_expandedDepartmentIds), _expandedDepartmentIds);
        }
        private async Task CollapseAll()
        {
            _treeItems.SetExpansion(false);
            _expandedAll = false;
            _expandedDepartmentIds = [];
            await SessionStorage.SetAsync(nameof(_expandedDepartmentIds), _expandedDepartmentIds);
        }

        private static string GetIcon(ITreeItemData<Department> item)
        {
            if (item.HasChildren)
                return item.Expanded ? Icons.Material.Filled.FolderOpen : Icons.Material.Filled.Folder;

            return Icons.Material.Filled.CorporateFare;
        }

        private async Task ExpandedChanged(ITreeItemData<Department?> node, bool expanded)
        {
            node.Expanded = expanded;

            if (node.Value is Department department)

            {
                if (expanded)
                    _expandedDepartmentIds.Add(department.Id);
                else
                    _expandedDepartmentIds.Remove(department.Id);

                await SessionStorage.SetAsync(nameof(_expandedDepartmentIds), _expandedDepartmentIds);
            }
        }

        private async Task NewDepartmentAsync(Department? value)
        {
            if (value is Department parent)
                await CreateOrUpdateDepartment(new Department
                {
                    ParentId = parent.Id
                });
        }

        private async Task EditDepartmentAsync(object? value)
        {
            if (value is Department department)
                await CreateOrUpdateDepartment(department);
        }

        private async Task CreateOrUpdateDepartment(Department department)
        {
            var result = await ProcessDepartmentDialog(department);

            if (result is { Canceled: false, Data: Department processedDepartment })
            {
                try
                {
                    if (_departments?.Any(e => e.Id == processedDepartment.Id) == false)
                        await Service.CreateAsync(processedDepartment, _cts.Token);
                    else
                        await Service.UpdateAsync(processedDepartment, _cts.Token);

                    await LoadDataAndBuildTreeAsync();

                    Snackbar.Add($"Успешно сохранено: {processedDepartment.Name}", Severity.Success);
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Ошибка при сохранении {processedDepartment.Name}: {ex.Message}", Severity.Error);
                }
            }
        }

        private async Task<DialogResult?> ProcessDepartmentDialog(Department department)
        {
            bool isNew = _departments?.Any(e => e.Id == department.Id) == false;

            var title = isNew ? "Создать Подразделение" : "Изменить Подразделение";

            var parameters = new DialogParameters<DepartmentDialog>
            {
                { x => x.TitleIcon, isNew ? Icons.Material.Filled.AddCircleOutline : Icons.Material.Filled.Edit },
                { x => x.Department, department },
                { x => x.Departments, _departments }
            };

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = await DialogService.ShowAsync<DepartmentDialog>(title, parameters, options);

            return await dialog.Result;
        }


        private async Task DeleteDepartmentAsync(object? value)
        {
            if (_isProcessing) return;

            if (value is Department department)
            {
                if (await ConfirmDeleteItemAsync(department))
                {
                    try
                    {
                        _isProcessing = true;

                        var result = await Service.DeleteAsync(department.Id);

                        if (result.IsSuccess)
                            Snackbar.Add($"Успешно удалено: {department.Name}", Severity.Success);
                        else
                            Snackbar.Add($"Ошибка при удалении {department.Name}: {result.Error.Description}", Severity.Error);
                    }
                    catch (Exception ex)
                    {
                        Snackbar.Add($"Ошибка при удалении {department.Name}: {ex.Message}", Severity.Error);
                    }
                    finally
                    {
                        _isProcessing = false;

                        await LoadDataAndBuildTreeAsync();
                    }
                }
            }
        }

        private async Task<bool> ConfirmDeleteItemAsync<T>(T item) where T : IHasName
        {
            var title = "Удалить Подразделение";

            var parameters = new DialogParameters<ConfirmDialog>
            {
                { x => x.TitleIcon, Icons.Material.Filled.Delete },
                { x => x.ContentText, $"Вы уверены, что хотите удалить '{item.Name}' навсегда?" },
                { x => x.ButtonText, "Да, удалить" },
                { x => x.ConfirmColor, Color.Secondary }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>(title, parameters, options);

            var result = await dialog.Result;

            return result is { Canceled: false };
        }

        private async Task RestoreDepartmentAsync(Department? value)
        {
            if (_isProcessing) return;

            if (value is Department department)
            {
                if (await ConfirmRestoreItemAsync(department))
                {
                    try
                    {
                        _isProcessing = true;

                        var result = await Service.RestoreAsync(department.Id);

                        if (result.IsSuccess)
                            Snackbar.Add($"Успешно восстановлено: {department.Name}", Severity.Success);
                        else
                            Snackbar.Add($"Ошибка при восстановлении {department.Name}: {result.Error.Description}", Severity.Error);
                    }
                    catch (Exception ex)
                    {
                        Snackbar.Add($"Ошибка при восстановлении {department.Name}: {ex.Message}", Severity.Error);
                    }
                    finally
                    {
                        _isProcessing = false;

                        await LoadDataAndBuildTreeAsync();
                    }
                }
            }
        }

        private async Task<bool> ConfirmRestoreItemAsync<T>(T item) where T : IHasName
        {
            var title = "Восстановить Подразделение";

            var parameters = new DialogParameters<ConfirmDialog>
            {
                { x => x.TitleIcon, Icons.Material.Filled.RestoreFromTrash },
                { x => x.ContentText, $"Вы уверены, что хотите восстановить '{item.Name}'?" },
                { x => x.ButtonText, "Да, восстановить" },
                { x => x.ConfirmColor, Color.Info }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>(title, parameters, options);

            var result = await dialog.Result;

            return result is { Canceled: false };
        }

        private async Task ToggleIncludeDelete(bool args)
        {
            _includeDeleted = args;

            await LoadDataAndBuildTreeAsync();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
