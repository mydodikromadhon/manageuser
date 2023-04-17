using Newtonsoft.Json;
using CRUD.ManagementUser.Application.Common.Constants;

namespace CRUD.ManagementUser.Bsui.Common.Extensions;

public static class StringExtensions
{
    public static string PrettifyJson(this string json)
    {
        if (!string.IsNullOrWhiteSpace(json))
        {
            try
            {
                using var stringReader = new StringReader(json);
                using var stringWriter = new StringWriter();
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
            catch
            {
                return json;
            }
        }
        else
        {
            return DefaultTextFor.NA;
        }
    }
}
