using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;

namespace TopGlove.Api.Extension
{
    public static class ExcelGenerator
    {

        public static byte[] CreateExcel<T>(this List<T> list)
        {
            Type tType = typeof(T);
            var attribute = tType.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                           .Cast<DisplayNameAttribute>().FirstOrDefault();
            var sheetName = attribute == null ? "Sheet" : attribute.DisplayName;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            PropertyInfo[] headerInfo = tType.GetProperties();
            foreach (var property in headerInfo)
            {
                attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                           .Cast<DisplayNameAttribute>().FirstOrDefault();
                headers.Add(property.Name, attribute == null ? property.Name : attribute.DisplayName);
            }


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                // Add Header Columns
                var count = 0;
                foreach (var entry in headers)
                {
                    worksheet.Cell(0, count++).Value = entry.Value;
                }

                // Add Value
                for (int j = 0; j < list.Count; j++)
                {
                    var item = list[j];
                    int i = 0;
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        var y = typeof(T).InvokeMember(entry.Key.ToString(), BindingFlags.GetProperty, null, item, null);
                        worksheet.Cell(j, i++).Value = (y == null) ? "" : y.ToString();
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }
    }
}
