namespace Flux.Transcode
{
  class SimpleToon
  {
    // Encode object to TOON format
    public static string Encode(object data)
    {
      if (data == null) return "null";
      if (data is string s) return "\"" + s.Replace("\"", "\\\"") + "\"";
      if (data is bool b) return b ? "true" : "false";
      if (data is int || data is double || data is float || data is long) return data.ToString() ?? string.Empty;
      if (data is System.Collections.Generic.Dictionary<string, object> dict) return EncodeObject(dict);
      if (data is System.Collections.Generic.List<object> list) return EncodeArray(list);

      throw new NotSupportedException($"Type {data.GetType()} not supported.");
    }

    private static string EncodeObject(System.Collections.Generic.Dictionary<string, object> dict)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append("{");
      bool first = true;
      foreach (var kv in dict)
      {
        if (!first) sb.Append(";");
        first = false;
        sb.Append(kv.Key);
        sb.Append(":");
        sb.Append(Encode(kv.Value));
      }
      sb.Append("}");
      return sb.ToString();
    }

    private static string EncodeArray(System.Collections.Generic.List<object> list)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append("[");
      bool first = true;
      foreach (var item in list)
      {
        if (!first) sb.Append(",");
        first = false;
        sb.Append(Encode(item));
      }
      sb.Append("]");
      return sb.ToString();
    }

    // Decode TOON string to object
    public static object? Decode(string toon)
    {
      if (string.IsNullOrWhiteSpace(toon))
        throw new ArgumentException("TOON string cannot be null or empty.");

      toon = toon.Trim();
      if (toon.StartsWith("{") && toon.EndsWith("}"))
        return ParseObject(toon.Substring(1, toon.Length - 2));
      if (toon.StartsWith("[") && toon.EndsWith("]"))
        return ParseArray(toon.Substring(1, toon.Length - 2));
      return ParseValue(toon);
    }

    private static Dictionary<string, object?> ParseObject(string inner)
    {
      var dict = new Dictionary<string, object?>();
      foreach (var pair in SplitTopLevel(inner, ';'))
      {
        if (string.IsNullOrWhiteSpace(pair)) continue;
        var kv = pair.Split(':', 2);
        if (kv.Length != 2) throw new FormatException("Invalid key-value pair.");
        dict[kv[0].Trim()] = Decode(kv[1].Trim());
      }
      return dict;
    }

    private static List<object?> ParseArray(string inner)
    {
      var list = new List<object?>();
      foreach (var item in SplitTopLevel(inner, ','))
      {
        if (string.IsNullOrWhiteSpace(item)) continue;
        list.Add(Decode(item.Trim()));
      }
      return list;
    }

    private static object? ParseValue(string val)
    {
      if (val == "null") return null;
      if (val == "true") return true;
      if (val == "false") return false;
      if (val.StartsWith("\"") && val.EndsWith("\""))
        return val.Substring(1, val.Length - 2).Replace("\\\"", "\"");
      if (double.TryParse(val, out double num)) return num;
      throw new FormatException($"Invalid value: {val}");
    }

    // Splits a string by a delimiter at top level (ignores nested braces/brackets)
    private static List<string> SplitTopLevel(string input, char delimiter)
    {
      var parts = new List<string>();
      int depthObj = 0, depthArr = 0;
      var current = new System.Text.StringBuilder();

      foreach (char c in input)
      {
        if (c == '{') depthObj++;
        if (c == '}') depthObj--;
        if (c == '[') depthArr++;
        if (c == ']') depthArr--;

        if (c == delimiter && depthObj == 0 && depthArr == 0)
        {
          parts.Add(current.ToString());
          current.Clear();
        }
        else
        {
          current.Append(c);
        }
      }
      parts.Add(current.ToString());
      return parts;
    }
  }
}
