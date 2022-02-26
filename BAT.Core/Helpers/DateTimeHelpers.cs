namespace BAT.Core.Helpers
{
    using System;

    internal static class DateTimeHelpers
    {
        internal static string GetBookingsJsonFileName(this DateTime date)
        {
            var fileName = date.ToString(Consts.BookingFileNameDateFormat) + ".json";
            return fileName;
        }
    }
}
