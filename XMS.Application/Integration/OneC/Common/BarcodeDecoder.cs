using System.Globalization;
using System.Numerics;

namespace XMS.Integrations.OneC.Common;

internal class BarcodeDecoder
{
    public static Guid ToRefKey(string barcode)
    {
        var number = BigInteger.Parse(barcode, CultureInfo.InvariantCulture);

        var hex = number.ToString("x").PadLeft(32, '0');

        var guidText =
            $"{hex[..8]}-{hex.Substring(8, 4)}-{hex.Substring(12, 4)}-{hex.Substring(16, 4)}-{hex.Substring(20, 12)}";

        return Guid.Parse(guidText);
    }

    public static string FromRefKey(Guid refKey)
    {
        var hex = refKey.ToString("N");

        var number = BigInteger.Parse("0" + hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }
}
