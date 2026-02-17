using Microsoft.AspNetCore.Components;
using MudBlazor;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;
using XMS.Web.Components.Common;

namespace XMS.Web.Components.Pages.EmployeePages
{
    public partial class Index : IDisposable
    {
        [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
        [Inject] public IJobTitleService JobTitleService { get; set; } = default!;
        [Inject] public IDepartmentService DepartmentService { get; set; } = default!;
        [Inject] public ICityService CityService { get; set; } = default!;
        [Inject] public ILocationService LocationService { get; set; } = default!;
        [Inject] public IUserAdService UserAdService { get; set; } = default!;
        [Inject] public IUserUtService UserUtService { get; set; } = default!;
        [Inject] public IEmployeeBuhService EmployeeBuhService { get; set; } = default!;
        [Inject] public IEmployeeZupService EmployeeZupService { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();
        private IReadOnlyList<Employee> _employees = [];
        private IReadOnlyList<JobTitle> _jobTitles = [];
        private IEnumerable<City> _cities = [];
        private IReadOnlyList<Location> _locations = [];
        private IReadOnlyList<Department> _departments = [];
        private IReadOnlyList<UserUt> _usersUt = [];
        private IReadOnlyList<EmployeeBuh> _employeesBuh = [];
        private IReadOnlyList<EmployeeZup> _employeesZup = [];
        private IReadOnlyList<UserAd> _usersAd = [];

        private MudDataGrid<Employee> _employeeGrid = default!;

        private string? _searchString;
        private bool _isLoading;
        private bool _isProcessing;
        private bool _isEditingGrid;
        private bool _includeDeleted = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataFromDb();
        }

        private async Task LoadDataFromDb()
        {
            if (_isLoading) return;

            _isLoading = true;

            try
            {
                await Task.WhenAll(
                    LoadEmployees(),
                    LoadJobTitles(),
                    LoadDepartments(),
                    LoadCities(),
                    LoadLocations(),
                    LoadUsersUt(),
                    LoadUsersAd(),
                    LoadEmploeesBuh(),
                    LoadEmploeesZup());
            }
            finally
            {
                _isLoading = false;

                StateHasChanged();
            }
        }

        private async Task LoadEmployees() => _employees = await EmployeeService.GetListAsync(_includeDeleted, _cts.Token);
        private async Task LoadJobTitles() => _jobTitles = await JobTitleService.GetListAsync(false, _cts.Token);
        private async Task LoadDepartments()
        {
            var list = await DepartmentService.GetListAsync(false, _cts.Token);
            _departments = TreeHelper.BuildFlattenedTree(list);
        }
        private async Task LoadCities() => _cities = await CityService.GetListAsync(false, _cts.Token);
        private async Task LoadLocations() => _locations = await LocationService.GetListAsync(false, _cts.Token);
        private async Task LoadUsersUt() => _usersUt = await UserUtService.GetListAsync(false, _cts.Token);
        private async Task LoadUsersAd() => _usersAd = await UserAdService.GetListAsync(_cts.Token);
        private async Task LoadEmploeesBuh() => _employeesBuh = await EmployeeBuhService.GetListAsync(false, _cts.Token);
        private async Task LoadEmploeesZup() => _employeesZup = await EmployeeZupService.GetListAsync(false, _cts.Token);

        private async Task NewItemAsync()
        {
            await _employeeGrid.SetEditingItemAsync(new Employee());
        }

        private async Task<DataGridEditFormAction> CommittedItemChanges(Employee item)
        {
            try
            {
                if (_employees?.Any(x => x.Id == item.Id) == false)
                    await EmployeeService.CreateAsync(item, _cts.Token);
                else
                    await EmployeeService.UpdateAsync(item, _cts.Token);

                Snackbar.Add($"Успешно сохранено: {item.Name}", Severity.Success);

                await LoadEmployees();

                return DataGridEditFormAction.Close;
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Ошибка при сохранении {item.Name}: {ex.Message}", Severity.Error);

                return DataGridEditFormAction.KeepOpen;
            }
        }

        private async Task DeleteItemAsync(Employee item)
        {
            if (_isProcessing) return;

            if (await ConfirmDeleteItemAsync(item))
            {
                try
                {
                    _isProcessing = true;

                    var result = await EmployeeService.DeleteAsync(item.Id, _cts.Token);

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

                    await LoadEmployees();
                }
            }

            await _employeeGrid.CancelEditingItemAsync();
        }

        private async Task<bool> ConfirmDeleteItemAsync(Employee item)
        {
            var title = "Удалить Сотрудника";

            var parameters = new DialogParameters<ConfirmDialog>
            {
                { x => x.TitleIcon, Icons.Material.Filled.Delete },
                { x => x.ContentText, $"Вы уверены, что хотите удалить '{item.Name}'?" },
                { x => x.ButtonText, "Да, удалить" },
                { x => x.ConfirmColor, Color.Secondary }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>(title, parameters, options);

            var result = await dialog.Result;

            return result is { Canceled: false };
        }

        private async Task RestoreItemAsync(Employee item)
        {
            if (_isProcessing) return;

            if (await ConfirmRestoreItemAsync(item))
            {
                try
                {
                    _isProcessing = true;

                    var result = await EmployeeService.RestoreAsync(item.Id);

                    if (result.IsSuccess)
                        Snackbar.Add($"Успешно восстановлено: {item.Name}", Severity.Success);
                    else
                        Snackbar.Add($"Ошибка при восстановлении {item.Name}: {result.Error.Description}", Severity.Error);
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Ошибка при восстановлении {item.Name}: {ex.Message}", Severity.Error);
                }
                finally
                {
                    _isProcessing = false;

                    await LoadEmployees();
                }
            }

            await _employeeGrid.CancelEditingItemAsync();
        }

        private async Task<bool> ConfirmRestoreItemAsync(Employee item)
        {
            var title = "Восстановить Сотрудника";

            var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.TitleIcon, Icons.Material.Filled.RestoreFromTrash },
            { x => x.ContentText, $"Вы уверены, что хотите восстановить '{item.Name}'?" },
            { x => x.ButtonText, "Да, восстановить" },
            { x => x.ConfirmColor, Color.Info }
        };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>(title, parameters, options);

            var result = await dialog.Result;

            return result is { Canceled: false };
        }

        private void ToggleEditingGrid(bool args)
        {
            _isEditingGrid = args;
        }

        private Func<Employee, bool> QuickFilter => e =>
        {
            if (string.IsNullOrEmpty(_searchString))
                return true;

            if (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (!string.IsNullOrEmpty(e.JobTitle?.Name) && e.JobTitle.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (!string.IsNullOrEmpty(e.Department?.Name) && e.Department.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private Task<IEnumerable<Guid?>> SearchUserUtIds(string value, CancellationToken token)
        {
            if (_usersUt is null)
                return Task.FromResult(Enumerable.Empty<Guid?>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_usersUt.Select(e => e.Id as Guid?));

            return Task.FromResult(_usersUt
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => e.Id as Guid?));
        }

        private string? GetUserUtName(Guid? id)
        {
            return _usersUt.FirstOrDefault(e => e.Id == id)?.Name;
        }

        private Task<IEnumerable<Guid?>> SearchEmployeeBuhIds(string value, CancellationToken token)
        {
            if (_employeesBuh is null)
                return Task.FromResult(Enumerable.Empty<Guid?>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_employeesBuh.Select(e => e.Id as Guid?));

            return Task.FromResult(_employeesBuh
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => e.Id as Guid?));
        }

        private string? GetEmployeeBuhLabel(Guid? id)
        {
            var item = _employeesBuh.FirstOrDefault(e => e.Id == id);
            return item is null ? string.Empty : $"{item?.Name} ({item?.Code})";
        }

        private Task<IEnumerable<Guid?>> SearchEmployeeZupIds(string value, CancellationToken token)
        {
            if (_employeesZup is null)
                return Task.FromResult(Enumerable.Empty<Guid?>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_employeesZup.Select(e => e.Id as Guid?));

            return Task.FromResult(_employeesZup
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => e.Id as Guid?));
        }

        private string? GetEmployeeZupLabel(Guid? id)
        {
            var item = _employeesZup.FirstOrDefault(e => e.Id == id);
            return item is null ? string.Empty : $"{item?.Name} ({item?.Code})";
        }

        private Task<IEnumerable<string>> SearchUserAdIds(string value, CancellationToken token)
        {
            if (_usersAd is null)
                return Task.FromResult(Enumerable.Empty<string>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_usersAd.Select(e => e.Sid));

            return Task.FromResult(_usersAd
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => e.Sid));
        }

        private string? GetUserAdName(string id)
        {
            return _usersAd.FirstOrDefault(e => e.Sid == id)?.Name;
        }

        private Task<IEnumerable<Guid?>> SearchEmployeeIds(string value, CancellationToken token)
        {
            if (_employees is null)
                return Task.FromResult(Enumerable.Empty<Guid?>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_employees.Select(e => e.Id as Guid?));

            return Task.FromResult(_employees
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => e.Id as Guid?));
        }

        private string? GetEmployeeName(Guid? id)
        {
            return _employees.FirstOrDefault(e => e.Id == id)?.Name;
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
