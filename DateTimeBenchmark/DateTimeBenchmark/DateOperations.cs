using System;

namespace DateTimeBenchmark
{
    public class DateOperations
    {
        internal int GetYearFromDateTime(string dateString)
        {
            var dateObj = DateTime.Parse(dateString);
            return dateObj.Year;
        }

        internal int GetYearFromSubstring(string dateString)
        {
            var index = dateString.IndexOf('-');
            return int.Parse(dateString.Substring(0, index));
        }

        internal int GetYearFromSplit(string dateString)
        {
            var splittedArray = dateString.Split('-');
            return int.Parse(splittedArray[0]);
        }

        internal int GetYearFromReadOnlySpan(ReadOnlySpan<char> dateString)
        {
            var index = dateString.IndexOf('-');
            return int.Parse(dateString.Slice(0, index));
        }
    }
}
