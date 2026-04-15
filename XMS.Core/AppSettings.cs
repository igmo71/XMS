using System.Text.Encodings.Web;
using System.Text.Json;

namespace XMS.Core;

public static class AppSettings
{
    public static class Default
    {
        public const int Skip = 0;
        public const int Take = 1000;
        public const int MaxTake = 1000;
    }

    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        //WriteIndented = false // JSON с отступами
    };
}
