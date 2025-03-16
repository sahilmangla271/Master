using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace Inventory.Management.Infrastructure
{
    public class CustomDateTimeConverter : DefaultTypeConverter
    {
        private readonly string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" };

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            throw new CsvHelperException(row.Context, $"Invalid date format: {text}");
        }

        //public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        //{
        //    if (value is DateTime date)
        //    {
        //        return date.ToString("yyyy-MM-dd"); // Ensure output format
        //    }
        //    return base.ConvertToString(value, row, memberMapData);
        //}
    }

}
