using XMS.Core;

namespace XMS.Integration.OneC
{
    public record PaginationParameters(int? Skip = AppSettings.Default.Skip, int? Take = AppSettings.Default.Take);
}