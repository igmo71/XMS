using Microsoft.AspNetCore.Components;
using MudBlazor;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Modules.CostModule.Abstractions;

namespace XMS.Web.Components.Pages.CostSection.CostAllocationPages;

public partial class Index : IDisposable
{
    [Inject] public ICostAllocationService CostAllocationService { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;

    private readonly CancellationTokenSource _cts = new();
    private IReadOnlyList<Document_РасходныйКассовыйОрдер> _expenseCashOrders = [];
    private IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств> _cashlessWriteOffs = [];
    private bool _isLoading;
    private DateTime? _from = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
    private DateTime? _to = DateTime.Today.AddDays(1);
    private string? _numberTerm;

    protected override async Task OnInitializedAsync()
    {
        await ReloadAsync();
    }

    private async Task ReloadAsync()
    {
        if (_isLoading)
            return;

        if (_from.HasValue && _to.HasValue && _from.Value >= _to.Value)
        {
            Snackbar.Add("Дата начала периода должна быть меньше даты окончания.", Severity.Warning);
            return;
        }

        _isLoading = true;

        try
        {
            var parameters = BuildParameters();

            var expenseCashOrdersTask = CostAllocationService.GetDocumentРасходныйКассовыйОрдерAsync(parameters, _cts.Token);
            var cashlessWriteOffsTask = CostAllocationService.GetDocument_СписаниеБезналичныхДенежныхСредств_Async(parameters, _cts.Token);

            await Task.WhenAll(expenseCashOrdersTask, cashlessWriteOffsTask);

            _expenseCashOrders = expenseCashOrdersTask.Result
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Number)
                .ToList();
            _cashlessWriteOffs = cashlessWriteOffsTask.Result
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Number)
                .ToList();
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Ошибка загрузки документов: {ex.Message}", Severity.Error);
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }

    private DocumentQueryParameters BuildParameters()
    {
        var numberTerm = string.IsNullOrWhiteSpace(_numberTerm)
            ? null
            : $"%{_numberTerm.Trim()}%";

        return new DocumentQueryParameters(
            NumberTerm: numberTerm,
            From: _from,
            To: _to,
            Skip: null,
            Take: null);
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
        GC.SuppressFinalize(this);
    }
}
