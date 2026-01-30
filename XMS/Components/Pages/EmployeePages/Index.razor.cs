using Microsoft.AspNetCore.Components;
using MudBlazor;
using XMS.Core.Abstractions;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Components.Pages.EmployeePages
{
    public partial class Index
    {
        [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
        [Inject] public IJobTitleService JobTitleService { get; set; } = default!;
        [Inject] public IDepartmentService DepartmentService { get; set; } = default!;
        [Inject] public ICityService CityService { get; set; } = default!;
        [Inject] public ILocationService LocationService { get; set; } = default!;
        //[Inject] public ICostItemService CostItemService { get; set; } = default!;
        [Inject] public IUserAdService UserAdService { get; set; } = default!;
        [Inject] public IUserUtService UserUtService { get; set; } = default!;
        [Inject] public IEmployeeBuhService EmployeeBuhService { get; set; } = default!;
        [Inject] public IEmployeeZupService EmployeeZupService { get; set; } = default!;

        private IReadOnlyList<Employee> _employees = [];
        private IReadOnlyList<JobTitle> _jobTitles = [];
        private IReadOnlyList<City> _cities = [];
        private IReadOnlyList<Location> _locations = [];
        private IReadOnlyList<CostItem> _costItems = [];
        private IReadOnlyList<Department> _departments = [];
        private IReadOnlyList<UserUt> _usersUt = [];
        private IReadOnlyList<EmployeeBuh> _employeesBuh = []  ;
        private IReadOnlyList<EmployeeZup> _employeesZup = [];
        private IReadOnlyList<UserAd> _usersAd = [];

        private MudDataGrid<Employee> _employeeGrid = default!;

        private List<string> _events = new();

        private string? _searchString;
        private bool _isLoading;
        private bool _isEditingGrid;

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
            }
        }

        private async Task LoadEmployees() => _employees = await EmployeeService.GetListAsync();
        private async Task LoadJobTitles() => _jobTitles = await JobTitleService.GetListAsync();
        private async Task LoadDepartments() => _departments = await DepartmentService.GetFlattenedListAsync();
        private async Task LoadCities() => _cities = await CityService.GetListAsync();
        private async Task LoadLocations() => _locations = await LocationService.GetListAsync();
        private async Task LoadUsersUt() => _usersUt = await UserUtService.GetListAsync();
        private async Task LoadUsersAd() => _usersAd = await UserAdService.LoadListAsync();
        private async Task LoadEmploeesBuh() => _employeesBuh = await EmployeeBuhService.GetListAsync();
        private async Task LoadEmploeesZup() => _employeesZup = await EmployeeZupService.GetListAsync();

        private async Task NewItemAsync()
        {
            await _employeeGrid.SetEditingItemAsync(new Employee());
        }

        private async Task DeleteItem(Employee item)
        {
            if (_employees is null)
                return;

            await EmployeeService.DeleteAsync(item.Id);

            await LoadEmployees();

            await _employeeGrid.CancelEditingItemAsync();
        }

        private Task<IEnumerable<Department>> SearchDepartment(string value, CancellationToken token)
        {
            if (_departments is null)
                return Task.FromResult(Enumerable.Empty<Department>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_departments.AsEnumerable());

            return Task.FromResult(_departments
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .ToList().AsEnumerable());
        }

        private Task<IEnumerable<UserUt>> SearchUserUt(string value, CancellationToken token)
        {
            if (_usersUt is null)
                return Task.FromResult(Enumerable.Empty<UserUt>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_usersUt.AsEnumerable());

            return Task.FromResult(_usersUt
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .ToList().AsEnumerable());
        }

        private Task<IEnumerable<EmployeeBuh>> SearchEmployeeBuh(string value, CancellationToken token)
        {
            if (_employeesBuh is null)
                return Task.FromResult(Enumerable.Empty<EmployeeBuh>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_employeesBuh.AsEnumerable());

            return Task.FromResult(_employeesBuh
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .ToList().AsEnumerable());
        }

        private Task<IEnumerable<EmployeeZup>> SearchEmployeeZup(string value, CancellationToken token)
        {
            if (_employeesZup is null)
                return Task.FromResult(Enumerable.Empty<EmployeeZup>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_employeesZup.AsEnumerable());

            return Task.FromResult(_employeesZup
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .ToList().AsEnumerable());
        }

        private Task<IEnumerable<UserAd>> SearchUserAd(string value, CancellationToken token)
        {
            if (_usersAd is null)
                return Task.FromResult(Enumerable.Empty<UserAd>());

            if (string.IsNullOrEmpty(value))
                return Task.FromResult(_usersAd.AsEnumerable());

            return Task.FromResult(_usersAd
                .Where(x => x.Name != null && x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .ToList().AsEnumerable());
        }

        private string CleanName<T>(T? item) where T : IHasName
        {
            if (item == null || string.IsNullOrEmpty(item.Name))
                return string.Empty;

            return item.Name.TrimStart('└', '─', '-', ' ', '\u00A0');
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

            //if (!string.IsNullOrEmpty(e.CostItem?.Name) && e.CostItem.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            //    return true;

            return false;
        };

        private async Task CommittedItemChanges(Employee item)
        {
            if (_employees?.Any(x => x.Id == item.Id) == false)
                await EmployeeService.CreateAsync(item);
            else
                await EmployeeService.UpdateAsync(item);


            await LoadEmployees();
        }
    }
}
