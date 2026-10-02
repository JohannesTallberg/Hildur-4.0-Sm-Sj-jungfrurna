using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Hildur4._0
{
    public class Translator
    {
        public static string Translate(string text, string to)
        {
            try
            {
                using var http = new HttpClient();
                var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={to}&dt=t&q={Uri.EscapeDataString(text)}";
                var resp = http.GetStringAsync(url).GetAwaiter().GetResult();
                var doc = JsonDocument.Parse(resp);
                var root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                {
                    var first = root[0];
                    if (first.ValueKind == JsonValueKind.Array && first.GetArrayLength() > 0)
                    {
                        var seg = first[0];
                        if (seg.ValueKind == JsonValueKind.Array && seg.GetArrayLength() > 0)
                        {
                            var translated = seg[0].GetString();
                            if (!string.IsNullOrEmpty(translated)) return translated;
                        }
                    }
                }
            }
            catch
            {
                // ignore and return original
            }
            return text;
        }
    }
}
