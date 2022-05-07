namespace Flux
{
  namespace PhoneticAlgorithm
  {
    //  // https://github.com/iourinski/DMSoundex/blob/master/DMSoundex.pm

    //  public class DmSoundex
    //  {
    //    System.Collections.Generic.Dictionary<string, string[]> _beginning = new System.Collections.Generic.Dictionary<string, string[]>
    //    {
    //      { "0", new string[] { "ai", "aj", "ay", "au", "a", "ei", "ej", "ey", "e", "i", "oi", "oj", "oy", "o", "ui", "uj", "uy", "u", "ue", "ай", "ау", "а", "ей", "е", "и", "ой", "о", "уй", "у", "уе", "ы" } },
    //      { "1", new string[] { "eu", "ia", "y", "io", "yu", "ya", "yo", "iu", "io", "еу ", "иа", "й", "я", "ю", "э" } },
    //      { "2", new string[] { "schtsch", "schtsh", "schtch", "scht", "schd", "sht", "schd", "stch", "stsch", "sc", "strz", "strs", "stsh", "st", "szcz", "szcs", "szt", "sht", "szd", "sd", "zdz", "zdzh", "zhdzh", "zd", "zhd", "шщ", "счш", "сч", "шт", "шд", "щт", "щд", "стч", "стш", "сц", "стрз", "стрс", "стш", "ст", "сд", "здз", "здж", "ждж", "зд", "жд" } },
    //      { "3", new string[] { "d", "dt", "th", "t", "д", "дт", "т" } },
    //      { "4", new string[] { "cz", "cs", "csz", "czs", "drs", "drz", "ds", "dsh", "dsz", "dz", "dzh", "dzs", "sch", "sh", "sz", "s", " tch", "ttch", "ttsch", "trz", "trs", "ts", "tz", "tts", "ttsz", "tc", "tsz", "zh", "zs", "zsch", "zsh", "z", "j", "ц", "ч", "дрс", "дрз", "дс", "дш", "дщ", "дж", "ж", "з" } },
    //      { "5", new string[] { "chs", "g", "h", "ks", "kh", "k", "q", "x", "х", "г", "к", "кс" } },
    //      { "6", new string[] { "m", "n", "м", "н" } },
    //      { "7", new string[] { "pf", "ph", "fb", "p", "f", "b", "v", "w", "ф", "фб", "б", "в", "п" } },
    //      { "8", new string[] { "l", "л" } },
    //      { "9", new string[] { "r", "р" } }
    //    };

    //    System.Collections.Generic.Dictionary<string, string[]> _beforeVowel = new System.Collections.Generic.Dictionary<string, string[]>
    //    {
    //      { "1", new string[] { "ai", "aj", "ay", "ei", "ej", "ey", "oj", "oy", "oi", "ui", "uj", "uy", "ай", "ей", "ой", "уй", "я", "ю" } },
    //      { "3", new string[] { "d", "dt", "th", "t", "д", "дт", "т" } },
    //      { "4", new string[] { "cz", "cs", "csz", "czs", "drz", "drs", "ds", "dsh", "dsz", "dz", "dzh", "dzs", "schtsch", "schtsh", "schtch", "sch", "shtch", "shch", "shtsh", "sh", "stch", "stsch", "sc", "strz", "strs", "stsh", "sz", "s", "tch", "sd", "ttch", "ttsch", "trz", "trs", "tsch", "tsh", "tc", "ts", "tz", "j", "ц", "ч", "шт", "щт", "стж", "тч", "сд", "тш", "тщ", "тс", "тц", "ж", "з", "зд", "здз", "дж" } },
    //      { "5", new string[] { "g", "h", "kh", "k", "q", "г", "х", "к" } },
    //      { "6", new string[] { "m", "n", "м", "н" } },
    //      { "7", new string[] { "au", "b", "fb", "f", "p", "ph", "pf", "v", "w", "ав", "ау", "фб", "ф", "п", "пф", "в", "у", "б" } },
    //      { "8", new string[] { "l", "л" } },
    //      { "9", new string[] { "r", "р" } },
    //      { "43", new string[] { "sht", "scht", "schd", "st", "szt", "shd", "szd", "sd", "zd", "zhd", "шт", "счт", "чт", "ст", "шд", "сд", "зд", "жд" } },
    //      { "54", new string[] { "chs", "ks", "x", "чс", "кс" } },
    //      { "66", new string[] { "mn", "nm", "мн", "нм" } },
    //      { "nc", new string[] { "a", "e", "ia", "i", "o", "u", "ue", "y", "а", "е", "иа", "и", "о", "у", "уе", "уй", "й" } }
    //    };

    //    System.Collections.Generic.Dictionary<string, string[]> _generic = new System.Collections.Generic.Dictionary<string, string[]>
    //    {
    //      { "1", new string[] { "я", "ю", "ё" } },
    //      { "3", new string[] { "d", "dt", "th", "t", "д", "дт", "т" } },
    //      { "4", new string[] { "cz", "cs", "csz", "czs", "drz", "rsh", "drs", "dz", "dsh", "dsz", "dzh", "dzs", "schtsch", "schtsh", "schtch", "sch", "shtch", "shch", "shtsh", "stch", "shch", "sh", "stch", "sch", "sc", "strz", "strs", "sd", "shd", "s", "tch", "ch", "trz", "trs", "tsh", "tsch", "ttch", "ttsh", "tc", "ts", "zdz", "tz", "zdzh", "zhdzh", "zh", "z", "j", "ч", "чс", "дрз", "дж", "рщ", "рш", "дщ", "дш", "шщ", "щ", "ш", "шч", "сч", "стж", "стр", "стрс", "сд", "шд", "с", "тч", "тж", "тз", "тс", "тш", "тч", "ттч", "ттс", "тц", "здз", "здж", "ждж", "ж", "з", "ц", "тц" } },
    //      { "5", new string[] { "g", "kh", "k", "q", "г", "к", "х" } },
    //      { "6", new string[] { "m", "n", "м", "н" } },
    //      { "7", new string[] { "b", "fb", "f", "p", "pf", "ph", "v", "w", "б", "фб", "ф", "п", "пф", "в" } },
    //      { "8", new string[] { "l", "л" } },
    //      { "9", new string[] { "r", "р" } },
    //      { "43", new string[] { "sht", "scht", "schd", "st", "szt", "shd", "szd", "sd", "zd", "zhd", "шт", "чт", "щт", "ст", "шд", "сд", "жд", "жт", "зд", "зт" } },
    //      { "54", new string[] { "chs", "ks", "x", "чс", "кс" } },
    //      { "66", new string[] { "mn", "nm", "мн", "нм" } },
    //      { "nc", new string[] { "ai", "aj", "ay", "au", "a", "ei", "ej", "ey", "eu", "e", "h", "ia", "ie", "io", "iu", "i", "oi", "oj", "oy", "o", "ui", "uj", "uy", "u", "ue", "y", "ай", "ау", "а", "ей", "еу", "е", "х", "иа", "ио", "иу", "и", "ой", "о", "уй", "у", "уе", "й", "я", "ю", "э", "ъ", "ь", "ы", "ё" } }
    //    };

    //    string[] _vowels = new string[] { "a", "o", "u", "i", "e", "y", "а", "е", "и", "о", "y", "э", "ю", "я", "ё" };
    //  }
  }
}
