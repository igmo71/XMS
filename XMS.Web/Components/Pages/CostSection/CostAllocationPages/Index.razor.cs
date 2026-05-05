using Microsoft.AspNetCore.Components;
using MudBlazor;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Application;
using XMS.Modules.CostModule.Domain;
using XMS.Web.Components.Common;

namespace XMS.Web.Components.Pages.CostSection.CostAllocationPages;

public partial class Index : IDisposable
{
    [Inject] public ICostAllocationService CostAllocationService { get; set; } = default!;
    [Inject] public ICityService CityService { get; set; } = default!;
    [Inject] public ILocationService LocationService { get; set; } = default!;
    [Inject] public IDepartmentService DepartmentService { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;
    [Inject] public IWebUserAccessor UserAccessor { get; set; } = default!;
    [Inject] public ICostCategoryService CostCategoryService { get; set; } = default!;

    private readonly CancellationTokenSource _cts = new();
    //private IReadOnlyList<CostAllocation> _items = [];
    private IReadOnlyList<City> _cities = [];
    private IReadOnlyList<Location> _locations = [];
    private IReadOnlyList<Department> _departments = [];
    private IEnumerable<Department> _departmentsFlattenedTree = [];
    private IReadOnlyList<Employee> _managers = [];
    private bool _isLoading;
    private bool _isSaving;

    private MudDataGrid<CostAllocation>? _dataGrid;
    private CostAllocationQueryParameters _queryParameters = new();
    private bool _isManagerLoaded = false;

    protected override async Task OnInitializedAsync()
    {
        var currentManager = await UserAccessor.GetCurrentEmployeeAsync();

        _queryParameters.ManagerId = currentManager?.Id;

        _isManagerLoaded = true;

        if (_dataGrid != null)
            await _dataGrid.ReloadServerData();

        await LoadDataAsync();

        _departmentsFlattenedTree = TreeHelper.BuildFlattenedTree(_departments);
    }

    private async Task LoadDataAsync()
    {
        if (_isLoading)
            return;

        _isLoading = true;

        try
        {
            var citiesTask = CityService.GetListAsync(false, _cts.Token);
            var locationsTask = LocationService.GetListAsync(false, _cts.Token);
            var departmentsTask = DepartmentService.GetListAsync(false, _cts.Token);
            var managersTask = CostCategoryService.GetManagers(_cts.Token);

            await Task.WhenAll(citiesTask, locationsTask, departmentsTask, managersTask);

            _cities = await citiesTask;
            _locations = await locationsTask;
            _departments = await departmentsTask;
            _managers = await managersTask;
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }

    private async Task<GridData<CostAllocation>> LoadCostAllocation(GridState<CostAllocation> state, CancellationToken token)
    {
        if (!_isManagerLoaded)
            return new GridData<CostAllocation> { TotalItems = 0, Items = [] };

        _queryParameters.Skip = state.Page * state.PageSize;
        _queryParameters.Take = state.PageSize;

        CostAllocationDto data = await CostAllocationService.GetListAsync(_queryParameters, _cts.Token);

        var pagedData = data.Value.ToArray();

        return new GridData<CostAllocation>
        {
            TotalItems = data.TotalItems,
            Items = pagedData
        };
    }

    private async Task OnSearch()
    {
        if (_dataGrid is null)
            return;

        await _dataGrid.ReloadServerData();
    }

    private async Task ToggleAllocatedShow(bool args)
    {
        _queryParameters.IncludeAllocated = args;

        await OnSearch();
    }

    private async Task OnDocTypeSearch(int? docType)
    {
        _queryParameters.DocType = docType;

        await OnSearch();
    }

    private async Task OnDateFromSearch(DateTime? from)
    {
        _queryParameters.From = from;

        await OnSearch();
    }

    private async Task OnDateToSearch(DateTime? to)
    {
        _queryParameters.To = to;

        await OnSearch();
    }


    private async Task OnNumberSearch(string? searchTerm)
    {
        _queryParameters.SearchTerm = searchTerm;

        await OnSearch();
    }

    private async Task OnManagerSearch(Guid? managerId)
    {
        _queryParameters.ManagerId = managerId;

        await OnSearch();
    }

    private async Task OnCityChangedAsync(CostAllocation item, Guid? cityId)
    {
        item.CityId = cityId;
        item.City = _cities.FirstOrDefault(x => x.Id == cityId);

        await SaveAllocationAsync(item);
    }

    private async Task OnLocationChangedAsync(CostAllocation item, Guid? locationId)
    {
        item.LocationId = locationId;
        item.Location = _locations.FirstOrDefault(x => x.Id == locationId);

        await SaveAllocationAsync(item);
    }

    private async Task OnDepartmentChangedAsync(CostAllocation item, Guid? departmentId)
    {
        item.DepartmentId = departmentId;
        item.Department = _departments.FirstOrDefault(x => x.Id == departmentId);

        await SaveAllocationAsync(item);
    }

    private async Task SaveAllocationAsync(CostAllocation item)
    {
        if (_isSaving)
            return;

        _isSaving = true;

        try
        {
            await CostAllocationService.UpdateAsync(item, _cts.Token);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Ошибка при сохранении документа '{item.Number}': {ex.Message}", Severity.Error);
            await LoadDataAsync();
        }
        finally
        {
            _isSaving = false;

            if (_dataGrid != null)
                await _dataGrid.ReloadServerData();
        }
    }

    private static bool HasMissingRequiredLinks(CostAllocation item)
    {
        return item.CityId is null || item.LocationId is null || item.DepartmentId is null;
    }

    private static string GetCellClass(CostAllocation item)
    {
        var result = HasMissingRequiredLinks(item) ? "mud-theme-warning" : string.Empty;
        return result;
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
        GC.SuppressFinalize(this);
    }
}
