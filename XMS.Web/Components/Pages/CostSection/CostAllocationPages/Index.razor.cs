using Microsoft.AspNetCore.Components;
using MudBlazor;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Application;
using XMS.Modules.CostModule.Domain;

namespace XMS.Web.Components.Pages.CostSection.CostAllocationPages;

public partial class Index : IDisposable
{
    [Inject] public ICostAllocationService CostAllocationService { get; set; } = default!;
    [Inject] public ICityService CityService { get; set; } = default!;
    [Inject] public ILocationService LocationService { get; set; } = default!;
    [Inject] public IDepartmentService DepartmentService { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;

    private readonly CancellationTokenSource _cts = new();
    private IReadOnlyList<CostAllocation> _items = [];
    private IReadOnlyList<City> _cities = [];
    private IReadOnlyList<Location> _locations = [];
    private IReadOnlyList<Department> _departments = [];
    private bool _isLoading;
    private bool _isSaving;

    private MudDataGrid<CostAllocation>? _dataGrid;
    private CostAllocationQueryParameters _queryParameters = new() { Take = 10, IncludeDeleted = false, Type = -1 };

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
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

            await Task.WhenAll(citiesTask, locationsTask, departmentsTask);

            _cities = citiesTask.Result;
            _locations = locationsTask.Result;
            _departments = departmentsTask.Result;
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }

    private async Task<GridData<CostAllocation>> ServerReload(GridState<CostAllocation> state, CancellationToken token)
    {
        _queryParameters.Skip = state.Page * state.PageSize;
        _queryParameters.Take = state.Page;

        CostAllocationDto data = await CostAllocationService.GetListAsync(_queryParameters, _cts.Token);

        var pagedData = data.Value.ToArray();

        return new GridData<CostAllocation>
        {
            TotalItems = data.TotalItems,
            Items = pagedData
        };
    }

    private Task OnSearch()
    {
        if (_dataGrid == null)
            return Task.CompletedTask;

        return _dataGrid.ReloadServerData();
    }

    private Task ToggleIncludeDelete(bool args)
    {
        _queryParameters.IncludeDeleted = args;

        if (_dataGrid == null)
            return Task.CompletedTask;

        return _dataGrid.ReloadServerData();
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
