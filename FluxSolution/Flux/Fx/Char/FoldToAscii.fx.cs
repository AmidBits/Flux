namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Folds runes representing characters above ASCII as a reasonable ASCII equivalence. Only characters from certain blocks are converted.</para>
    /// </summary>
    public static string FoldToAscii(this char source)
      => ((System.Text.Rune)source).FoldToAscii();
  }
}

//namespace Flux
//{
//  public static partial class Unicode
//  {
//    /// <summary>Folds runes representing characters above ASCII as a reasonable ASCII equivalence. Only characters from certain blocks are converted.</summary>
//    private static System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<char>> m_asciiFolding =
//    new() {
//      { "A",
//        new() {
//          '\u00C0', // [LATIN CAPITAL LETTER A WITH GRAVE]
//        	'\u00C1', // [LATIN CAPITAL LETTER A WITH ACUTE]
//        	'\u00C2', // [LATIN CAPITAL LETTER A WITH CIRCUMFLEX]
//        	'\u00C3', // [LATIN CAPITAL LETTER A WITH TILDE]
//        	'\u00C4', // [LATIN CAPITAL LETTER A WITH DIAERESIS]
//        	'\u00C5', // [LATIN CAPITAL LETTER A WITH RING ABOVE]
//        	'\u0100', // [LATIN CAPITAL LETTER A WITH MACRON]
//        	'\u0102', // [LATIN CAPITAL LETTER A WITH BREVE]
//        	'\u0104', // [LATIN CAPITAL LETTER A WITH OGONEK]
//        	'\u018F', // [LATIN CAPITAL LETTER SCHWA]
//        	'\u01CD', // [LATIN CAPITAL LETTER A WITH CARON]
//        	'\u01DE', // [LATIN CAPITAL LETTER A WITH DIAERESIS AND MACRON]
//        	'\u01E0', // [LATIN CAPITAL LETTER A WITH DOT ABOVE AND MACRON]
//        	'\u01FA', // [LATIN CAPITAL LETTER A WITH RING ABOVE AND ACUTE]
//        	'\u0200', // [LATIN CAPITAL LETTER A WITH DOUBLE GRAVE]
//        	'\u0202', // [LATIN CAPITAL LETTER A WITH INVERTED BREVE]
//        	'\u0226', // [LATIN CAPITAL LETTER A WITH DOT ABOVE]
//        	'\u023A', // [LATIN CAPITAL LETTER A WITH STROKE]
//        	'\u1D00', // [LATIN LETTER SMALL CAPITAL A]
//        	'\u1E00', // [LATIN CAPITAL LETTER A WITH RING BELOW]
//        	'\u1EA0', // [LATIN CAPITAL LETTER A WITH DOT BELOW]
//        	'\u1EA2', // [LATIN CAPITAL LETTER A WITH HOOK ABOVE]
//        	'\u1EA4', // [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND ACUTE]
//        	'\u1EA6', // [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND GRAVE]
//        	'\u1EA8', // [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE]
//        	'\u1EAA', // [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND TILDE]
//        	'\u1EAC', // [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND DOT BELOW]
//        	'\u1EAE', // [LATIN CAPITAL LETTER A WITH BREVE AND ACUTE]
//        	'\u1EB0', // [LATIN CAPITAL LETTER A WITH BREVE AND GRAVE]
//        	'\u1EB2', // [LATIN CAPITAL LETTER A WITH BREVE AND HOOK ABOVE]
//        	'\u1EB4', // [LATIN CAPITAL LETTER A WITH BREVE AND TILDE]
//        	'\u1EB6', // [LATIN CAPITAL LETTER A WITH BREVE AND DOT BELOW]
//        	'\u24B6', // [CIRCLED LATIN CAPITAL LETTER A]
//        	'\uFF21', // [FULLWIDTH LATIN CAPITAL LETTER A]
//        }
//      },
//      { "a",
//        new() {
//          '\u00E0', // [LATIN SMALL LETTER A WITH GRAVE]
//        	'\u00E1', // [LATIN SMALL LETTER A WITH ACUTE]
//        	'\u00E2', // [LATIN SMALL LETTER A WITH CIRCUMFLEX]
//        	'\u00E3', // [LATIN SMALL LETTER A WITH TILDE]
//        	'\u00E4', // [LATIN SMALL LETTER A WITH DIAERESIS]
//        	'\u00E5', // [LATIN SMALL LETTER A WITH RING ABOVE]
//        	'\u0101', // [LATIN SMALL LETTER A WITH MACRON]
//        	'\u0103', // [LATIN SMALL LETTER A WITH BREVE]
//        	'\u0105', // [LATIN SMALL LETTER A WITH OGONEK]
//        	'\u01CE', // [LATIN SMALL LETTER A WITH CARON]
//        	'\u01DF', // [LATIN SMALL LETTER A WITH DIAERESIS AND MACRON]
//        	'\u01E1', // [LATIN SMALL LETTER A WITH DOT ABOVE AND MACRON]
//        	'\u01FB', // [LATIN SMALL LETTER A WITH RING ABOVE AND ACUTE]
//        	'\u0201', // [LATIN SMALL LETTER A WITH DOUBLE GRAVE]
//        	'\u0203', // [LATIN SMALL LETTER A WITH INVERTED BREVE]
//        	'\u0227', // [LATIN SMALL LETTER A WITH DOT ABOVE]
//        	'\u0250', // [LATIN SMALL LETTER TURNED A]
//        	'\u0259', // [LATIN SMALL LETTER SCHWA]
//        	'\u025A', // [LATIN SMALL LETTER SCHWA WITH HOOK]
//        	'\u1D8F', // [LATIN SMALL LETTER A WITH RETROFLEX HOOK]
//        	'\u1D95', // [LATIN SMALL LETTER SCHWA WITH RETROFLEX HOOK]
//        	'\u1E01', // [LATIN SMALL LETTER A WITH RING BELOW]
//        	'\u1E9A', // [LATIN SMALL LETTER A WITH RIGHT HALF RING]
//        	'\u1EA1', // [LATIN SMALL LETTER A WITH DOT BELOW]
//        	'\u1EA3', // [LATIN SMALL LETTER A WITH HOOK ABOVE]
//        	'\u1EA5', // [LATIN SMALL LETTER A WITH CIRCUMFLEX AND ACUTE]
//        	'\u1EA7', // [LATIN SMALL LETTER A WITH CIRCUMFLEX AND GRAVE]
//        	'\u1EA9', // [LATIN SMALL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE]
//        	'\u1EAB', // [LATIN SMALL LETTER A WITH CIRCUMFLEX AND TILDE]
//        	'\u1EAD', // [LATIN SMALL LETTER A WITH CIRCUMFLEX AND DOT BELOW]
//        	'\u1EAF', // [LATIN SMALL LETTER A WITH BREVE AND ACUTE]
//        	'\u1EB1', // [LATIN SMALL LETTER A WITH BREVE AND GRAVE]
//        	'\u1EB3', // [LATIN SMALL LETTER A WITH BREVE AND HOOK ABOVE]
//        	'\u1EB5', // [LATIN SMALL LETTER A WITH BREVE AND TILDE]
//        	'\u1EB7', // [LATIN SMALL LETTER A WITH BREVE AND DOT BELOW]
//        	'\u2090', // [LATIN SUBSCRIPT SMALL LETTER A]
//        	'\u2094', // [LATIN SUBSCRIPT SMALL LETTER SCHWA]
//        	'\u24D0', // [CIRCLED LATIN SMALL LETTER A]
//        	'\u2C65', // [LATIN SMALL LETTER A WITH STROKE]
//        	'\u2C6F', // [LATIN CAPITAL LETTER TURNED A]
//        	'\uFF41', // [FULLWIDTH LATIN SMALL LETTER A]
//        }
//      },
//      { "AA",
//        new() {
//          '\uA732', // [LATIN CAPITAL LETTER AA]
//        }
//      },
//      { "AE",
//        new() {
//          '\u00C6', // [LATIN CAPITAL LETTER AE]
//        	'\u01E2', // [LATIN CAPITAL LETTER AE WITH MACRON]
//        	'\u01FC', // [LATIN CAPITAL LETTER AE WITH ACUTE]
//        	'\u1D01', // [LATIN LETTER SMALL CAPITAL AE]
//        }
//      },
//      { "AO",
//        new() {
//          '\uA734', // [LATIN CAPITAL LETTER AO]
//        }
//      },
//      { "AU",
//        new() {
//          '\uA736', // [LATIN CAPITAL LETTER AU]
//        }
//      },
//      { "AV",
//        new() {
//          '\uA738', // [LATIN CAPITAL LETTER AV]
//          '\uA73A', // [LATIN CAPITAL LETTER AV WITH HORIZONTAL BAR]
//        }
//      },
//      { "AY",
//        new() {
//          '\uA73C', // [LATIN CAPITAL LETTER AY]
//        }
//      },
//      { "(a)",
//        new() {
//          '\u249C', // [PARENTHESIZED LATIN SMALL LETTER A]
//        }
//      },
//      { "aa",
//        new() {
//          '\uA733', // [LATIN SMALL LETTER AA]
//        }
//      },
//      { "ae",
//        new() {
//          '\u00E6', // [LATIN SMALL LETTER AE]
//          '\u01E3', // [LATIN SMALL LETTER AE WITH MACRON]
//          '\u01FD', // [LATIN SMALL LETTER AE WITH ACUTE]
//          '\u1D02', // [LATIN SMALL LETTER TURNED AE]
//        }
//      },
//      { "ao",
//        new() {
//          '\uA735', // [LATIN SMALL LETTER AO]
//        }
//      },
//      { "au",
//        new() {
//          '\uA737', // [LATIN SMALL LETTER AU]
//        }
//      },
//      { "av",
//        new() {
//          '\uA739', // [LATIN SMALL LETTER AV]
//          '\uA73B', // [LATIN SMALL LETTER AV WITH HORIZONTAL BAR]
//        }
//      },
//      { "ay",
//        new() {
//          '\uA73D', // [LATIN SMALL LETTER AY]
//        }
//      },
//      { "B",
//        new() {
//          '\u0181', // [LATIN CAPITAL LETTER B WITH HOOK]
//        	'\u0182', // [LATIN CAPITAL LETTER B WITH TOPBAR]
//        	'\u0243', // [LATIN CAPITAL LETTER B WITH STROKE]
//        	'\u0299', // [LATIN LETTER SMALL CAPITAL B]
//        	'\u1D03', // [LATIN LETTER SMALL CAPITAL BARRED B]
//        	'\u1E02', // [LATIN CAPITAL LETTER B WITH DOT ABOVE]
//        	'\u1E04', // [LATIN CAPITAL LETTER B WITH DOT BELOW]
//        	'\u1E06', // [LATIN CAPITAL LETTER B WITH LINE BELOW]
//        	'\u24B7', // [CIRCLED LATIN CAPITAL LETTER B]
//        	'\uFF22', // [FULLWIDTH LATIN CAPITAL LETTER B]
//        }
//      },
//      { "b",
//        new() {
//          '\u0180', // [LATIN SMALL LETTER B WITH STROKE]
//        	'\u0183', // [LATIN SMALL LETTER B WITH TOPBAR]
//        	'\u0253', // [LATIN SMALL LETTER B WITH HOOK]
//        	'\u1D6C', // [LATIN SMALL LETTER B WITH MIDDLE TILDE]
//        	'\u1D80', // [LATIN SMALL LETTER B WITH PALATAL HOOK]
//        	'\u1E03', // [LATIN SMALL LETTER B WITH DOT ABOVE]
//        	'\u1E05', // [LATIN SMALL LETTER B WITH DOT BELOW]
//        	'\u1E07', // [LATIN SMALL LETTER B WITH LINE BELOW]
//        	'\u24D1', // [CIRCLED LATIN SMALL LETTER B]
//        	'\uFF42', // [FULLWIDTH LATIN SMALL LETTER B]
//        }
//      },
//      { "(b)",
//        new() {
//          '\u249D', // [PARENTHESIZED LATIN SMALL LETTER B]
//        }
//      },
//      { "C",
//        new() {
//          '\u00C7', // [LATIN CAPITAL LETTER C WITH CEDILLA]
//        	'\u0106', // [LATIN CAPITAL LETTER C WITH ACUTE]
//        	'\u0108', // [LATIN CAPITAL LETTER C WITH CIRCUMFLEX]
//        	'\u010A', // [LATIN CAPITAL LETTER C WITH DOT ABOVE]
//        	'\u010C', // [LATIN CAPITAL LETTER C WITH CARON]
//        	'\u0187', // [LATIN CAPITAL LETTER C WITH HOOK]
//        	'\u023B', // [LATIN CAPITAL LETTER C WITH STROKE]
//        	'\u0297', // [LATIN LETTER STRETCHED C]
//        	'\u1D04', // [LATIN LETTER SMALL CAPITAL C]
//        	'\u1E08', // [LATIN CAPITAL LETTER C WITH CEDILLA AND ACUTE]
//        	'\u24B8', // [CIRCLED LATIN CAPITAL LETTER C]
//        	'\uFF23', // [FULLWIDTH LATIN CAPITAL LETTER C]
//        }
//      },
//      { "c",
//        new() {
//          '\u00E7', // [LATIN SMALL LETTER C WITH CEDILLA]
//        	'\u0107', // [LATIN SMALL LETTER C WITH ACUTE]
//        	'\u0109', // [LATIN SMALL LETTER C WITH CIRCUMFLEX]
//        	'\u010B', // [LATIN SMALL LETTER C WITH DOT ABOVE]
//        	'\u010D', // [LATIN SMALL LETTER C WITH CARON]
//        	'\u0188', // [LATIN SMALL LETTER C WITH HOOK]
//        	'\u023C', // [LATIN SMALL LETTER C WITH STROKE]
//        	'\u0255', // [LATIN SMALL LETTER C WITH CURL]
//        	'\u1E09', // [LATIN SMALL LETTER C WITH CEDILLA AND ACUTE]
//        	'\u2184', // [LATIN SMALL LETTER REVERSED C]
//        	'\u24D2', // [CIRCLED LATIN SMALL LETTER C]
//        	'\uA73E', // [LATIN CAPITAL LETTER REVERSED C WITH DOT]
//        	'\uA73F', // [LATIN SMALL LETTER REVERSED C WITH DOT]
//        	'\uFF43', // [FULLWIDTH LATIN SMALL LETTER C]
//        }
//      },
//      { "(c)",
//        new() {
//          '\u249E', // [PARENTHESIZED LATIN SMALL LETTER C]
//        }
//      },
//      { "D",
//        new() {
//          '\u00D0', // [LATIN CAPITAL LETTER ETH]
//        	'\u010E', // [LATIN CAPITAL LETTER D WITH CARON]
//        	'\u0110', // [LATIN CAPITAL LETTER D WITH STROKE]
//        	'\u0189', // [LATIN CAPITAL LETTER AFRICAN D]
//        	'\u018A', // [LATIN CAPITAL LETTER D WITH HOOK]
//        	'\u018B', // [LATIN CAPITAL LETTER D WITH TOPBAR]
//        	'\u1D05', // [LATIN LETTER SMALL CAPITAL D]
//        	'\u1D06', // [LATIN LETTER SMALL CAPITAL ETH]
//        	'\u1E0A', // [LATIN CAPITAL LETTER D WITH DOT ABOVE]
//        	'\u1E0C', // [LATIN CAPITAL LETTER D WITH DOT BELOW]
//        	'\u1E0E', // [LATIN CAPITAL LETTER D WITH LINE BELOW]
//        	'\u1E10', // [LATIN CAPITAL LETTER D WITH CEDILLA]
//        	'\u1E12', // [LATIN CAPITAL LETTER D WITH CIRCUMFLEX BELOW]
//        	'\u24B9', // [CIRCLED LATIN CAPITAL LETTER D]
//        	'\uA779', // [LATIN CAPITAL LETTER INSULAR D]
//        	'\uFF24', // [FULLWIDTH LATIN CAPITAL LETTER D]
//        }
//      },
//      { "d",
//        new() {
//          '\u00F0', // [LATIN SMALL LETTER ETH]
//        	'\u010F', // [LATIN SMALL LETTER D WITH CARON]
//        	'\u0111', // [LATIN SMALL LETTER D WITH STROKE]
//        	'\u018C', // [LATIN SMALL LETTER D WITH TOPBAR]
//        	'\u0221', // [LATIN SMALL LETTER D WITH CURL]
//        	'\u0256', // [LATIN SMALL LETTER D WITH TAIL]
//        	'\u0257', // [LATIN SMALL LETTER D WITH HOOK]
//        	'\u1D6D', // [LATIN SMALL LETTER D WITH MIDDLE TILDE]
//        	'\u1D81', // [LATIN SMALL LETTER D WITH PALATAL HOOK]
//        	'\u1D91', // [LATIN SMALL LETTER D WITH HOOK AND TAIL]
//        	'\u1E0B', // [LATIN SMALL LETTER D WITH DOT ABOVE]
//        	'\u1E0D', // [LATIN SMALL LETTER D WITH DOT BELOW]
//        	'\u1E0F', // [LATIN SMALL LETTER D WITH LINE BELOW]
//        	'\u1E11', // [LATIN SMALL LETTER D WITH CEDILLA]
//        	'\u1E13', // [LATIN SMALL LETTER D WITH CIRCUMFLEX BELOW]
//        	'\u24D3', // [CIRCLED LATIN SMALL LETTER D]
//        	'\uA77A', // [LATIN SMALL LETTER INSULAR D]
//        	'\uFF44', // [FULLWIDTH LATIN SMALL LETTER D]
//        }
//      },
//      { "DZ",
//        new() {
//          '\u01C4', // [LATIN CAPITAL LETTER DZ WITH CARON]
//        	'\u01F1', // [LATIN CAPITAL LETTER DZ]
//        }
//      },
//      { "Dz",
//        new() {
//          '\u01C5', // [LATIN CAPITAL LETTER D WITH SMALL LETTER Z WITH CARON]
//        	'\u01F2', // [LATIN CAPITAL LETTER D WITH SMALL LETTER Z]
//        }
//      },
//      { "(d)",
//        new() {
//          '\u249F', // [PARENTHESIZED LATIN SMALL LETTER D]
//        }
//      },
//      { "db",
//        new() {
//          '\u0238', // [LATIN SMALL LETTER DB DIGRAPH]
//        }
//      },
//      { "dz",
//        new() {
//          '\u01C6', // [LATIN SMALL LETTER DZ WITH CARON]
//        	'\u01F3', // [LATIN SMALL LETTER DZ]
//        	'\u02A3', // [LATIN SMALL LETTER DZ DIGRAPH]
//        	'\u02A5', // [LATIN SMALL LETTER DZ DIGRAPH WITH CURL]
//        }
//      },
//      { "E",
//        new() {
//          '\u00C8', // [LATIN CAPITAL LETTER E WITH GRAVE]
//        	'\u00C9', // [LATIN CAPITAL LETTER E WITH ACUTE]
//        	'\u00CA', // [LATIN CAPITAL LETTER E WITH CIRCUMFLEX]
//        	'\u00CB', // [LATIN CAPITAL LETTER E WITH DIAERESIS]
//        	'\u0112', // [LATIN CAPITAL LETTER E WITH MACRON]
//        	'\u0114', // [LATIN CAPITAL LETTER E WITH BREVE]
//        	'\u0116', // [LATIN CAPITAL LETTER E WITH DOT ABOVE]
//        	'\u0118', // [LATIN CAPITAL LETTER E WITH OGONEK]
//        	'\u011A', // [LATIN CAPITAL LETTER E WITH CARON]
//        	'\u018E', // [LATIN CAPITAL LETTER REVERSED E]
//        	'\u0190', // [LATIN CAPITAL LETTER OPEN E]
//        	'\u0204', // [LATIN CAPITAL LETTER E WITH DOUBLE GRAVE]
//        	'\u0206', // [LATIN CAPITAL LETTER E WITH INVERTED BREVE]
//        	'\u0228', // [LATIN CAPITAL LETTER E WITH CEDILLA]
//        	'\u0246', // [LATIN CAPITAL LETTER E WITH STROKE]
//        	'\u1D07', // [LATIN LETTER SMALL CAPITAL E]
//        	'\u1E14', // [LATIN CAPITAL LETTER E WITH MACRON AND GRAVE]
//        	'\u1E16', // [LATIN CAPITAL LETTER E WITH MACRON AND ACUTE]
//        	'\u1E18', // [LATIN CAPITAL LETTER E WITH CIRCUMFLEX BELOW]
//        	'\u1E1A', // [LATIN CAPITAL LETTER E WITH TILDE BELOW]
//        	'\u1E1C', // [LATIN CAPITAL LETTER E WITH CEDILLA AND BREVE]
//        	'\u1EB8', // [LATIN CAPITAL LETTER E WITH DOT BELOW]
//        	'\u1EBA', // [LATIN CAPITAL LETTER E WITH HOOK ABOVE]
//        	'\u1EBC', // [LATIN CAPITAL LETTER E WITH TILDE]
//        	'\u1EBE', // [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND ACUTE]
//        	'\u1EC0', // [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND GRAVE]
//        	'\u1EC2', // [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE]
//        	'\u1EC4', // [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND TILDE]
//        	'\u1EC6', // [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND DOT BELOW]
//        	'\u24BA', // [CIRCLED LATIN CAPITAL LETTER E]
//        	'\u2C7B', // [LATIN LETTER SMALL CAPITAL TURNED E]
//        	'\uFF25', // [FULLWIDTH LATIN CAPITAL LETTER E]
//        }
//      },
//      { "e",
//        new() {
//          '\u00E8', // [LATIN SMALL LETTER E WITH GRAVE]
//        	'\u00E9', // [LATIN SMALL LETTER E WITH ACUTE]
//        	'\u00EA', // [LATIN SMALL LETTER E WITH CIRCUMFLEX]
//        	'\u00EB', // [LATIN SMALL LETTER E WITH DIAERESIS]
//        	'\u0113', // [LATIN SMALL LETTER E WITH MACRON]
//        	'\u0115', // [LATIN SMALL LETTER E WITH BREVE]
//        	'\u0117', // [LATIN SMALL LETTER E WITH DOT ABOVE]
//        	'\u0119', // [LATIN SMALL LETTER E WITH OGONEK]
//        	'\u011B', // [LATIN SMALL LETTER E WITH CARON]
//        	'\u01DD', // [LATIN SMALL LETTER TURNED E]
//        	'\u0205', // [LATIN SMALL LETTER E WITH DOUBLE GRAVE]
//        	'\u0207', // [LATIN SMALL LETTER E WITH INVERTED BREVE]
//        	'\u0229', // [LATIN SMALL LETTER E WITH CEDILLA]
//        	'\u0247', // [LATIN SMALL LETTER E WITH STROKE]
//        	'\u0258', // [LATIN SMALL LETTER REVERSED E]
//        	'\u025B', // [LATIN SMALL LETTER OPEN E]
//        	'\u025C', // [LATIN SMALL LETTER REVERSED OPEN E]
//        	'\u025D', // [LATIN SMALL LETTER REVERSED OPEN E WITH HOOK]
//        	'\u025E', // [LATIN SMALL LETTER CLOSED REVERSED OPEN E]
//        	'\u029A', // [LATIN SMALL LETTER CLOSED OPEN E]
//        	'\u1D08', // [LATIN SMALL LETTER TURNED OPEN E]
//        	'\u1D92', // [LATIN SMALL LETTER E WITH RETROFLEX HOOK]
//        	'\u1D93', // [LATIN SMALL LETTER OPEN E WITH RETROFLEX HOOK]
//        	'\u1D94', // [LATIN SMALL LETTER REVERSED OPEN E WITH RETROFLEX HOOK]
//        	'\u1E15', // [LATIN SMALL LETTER E WITH MACRON AND GRAVE]
//        	'\u1E17', // [LATIN SMALL LETTER E WITH MACRON AND ACUTE]
//        	'\u1E19', // [LATIN SMALL LETTER E WITH CIRCUMFLEX BELOW]
//        	'\u1E1B', // [LATIN SMALL LETTER E WITH TILDE BELOW]
//        	'\u1E1D', // [LATIN SMALL LETTER E WITH CEDILLA AND BREVE]
//        	'\u1EB9', // [LATIN SMALL LETTER E WITH DOT BELOW]
//        	'\u1EBB', // [LATIN SMALL LETTER E WITH HOOK ABOVE]
//        	'\u1EBD', // [LATIN SMALL LETTER E WITH TILDE]
//        	'\u1EBF', // [LATIN SMALL LETTER E WITH CIRCUMFLEX AND ACUTE]
//        	'\u1EC1', // [LATIN SMALL LETTER E WITH CIRCUMFLEX AND GRAVE]
//        	'\u1EC3', // [LATIN SMALL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE]
//        	'\u1EC5', // [LATIN SMALL LETTER E WITH CIRCUMFLEX AND TILDE]
//        	'\u1EC7', // [LATIN SMALL LETTER E WITH CIRCUMFLEX AND DOT BELOW]
//        	'\u2091', // [LATIN SUBSCRIPT SMALL LETTER E]
//        	'\u24D4', // [CIRCLED LATIN SMALL LETTER E]
//        	'\u2C78', // [LATIN SMALL LETTER E WITH NOTCH]
//        	'\uFF45', // [FULLWIDTH LATIN SMALL LETTER E]
//        }
//      },
//      { "(e)",
//        new() {
//          '\u24A0', // [PARENTHESIZED LATIN SMALL LETTER E]
//        }
//      },
//      { "F",
//        new() {
//          '\u0191', // [LATIN CAPITAL LETTER F WITH HOOK]
//        	'\u1E1E', // [LATIN CAPITAL LETTER F WITH DOT ABOVE]
//        	'\u24BB', // [CIRCLED LATIN CAPITAL LETTER F]
//        	'\uA730', // [LATIN LETTER SMALL CAPITAL F]
//        	'\uA77B', // [LATIN CAPITAL LETTER INSULAR F]
//        	'\uA7FB', // [LATIN EPIGRAPHIC LETTER REVERSED F]
//        	'\uFF26', // [FULLWIDTH LATIN CAPITAL LETTER F]
//        }
//      },
//      { "f",
//        new() {
//          '\u0192', // [LATIN SMALL LETTER F WITH HOOK]
//        	'\u1D6E', // [LATIN SMALL LETTER F WITH MIDDLE TILDE]
//        	'\u1D82', // [LATIN SMALL LETTER F WITH PALATAL HOOK]
//        	'\u1E1F', // [LATIN SMALL LETTER F WITH DOT ABOVE]
//        	'\u1E9B', // [LATIN SMALL LETTER LONG S WITH DOT ABOVE]
//        	'\u24D5', // [CIRCLED LATIN SMALL LETTER F]
//        	'\uA77C', // [LATIN SMALL LETTER INSULAR F]
//        	'\uFF46', // [FULLWIDTH LATIN SMALL LETTER F]
//        }
//      },
//      { "(f)",
//        new() {
//          '\u24A1', // [PARENTHESIZED LATIN SMALL LETTER F]
//        }
//      },
//      { "ff",
//        new() {
//          '\uFB00', // [LATIN SMALL LIGATURE FF]
//        }
//      },
//      { "ffi",
//        new() {
//          '\uFB03', // [LATIN SMALL LIGATURE FFI]
//        }
//      },
//      { "ffl",
//        new() {
//          '\uFB04', // [LATIN SMALL LIGATURE FFL]
//        }
//      },
//      { "fi",
//        new() {
//          '\uFB01', // [LATIN SMALL LIGATURE FI]
//        }
//      },
//      { "fl",
//        new() {
//          '\uFB02', // [LATIN SMALL LIGATURE FL]
//        }
//      },
//      { "G",
//        new() {
//          '\u011C', // [LATIN CAPITAL LETTER G WITH CIRCUMFLEX]
//        	'\u011E', // [LATIN CAPITAL LETTER G WITH BREVE]
//        	'\u0120', // [LATIN CAPITAL LETTER G WITH DOT ABOVE]
//        	'\u0122', // [LATIN CAPITAL LETTER G WITH CEDILLA]
//        	'\u0193', // [LATIN CAPITAL LETTER G WITH HOOK]
//        	'\u01E4', // [LATIN CAPITAL LETTER G WITH STROKE]
//        	'\u01E5', // [LATIN SMALL LETTER G WITH STROKE]
//        	'\u01E6', // [LATIN CAPITAL LETTER G WITH CARON]
//        	'\u01E7', // [LATIN SMALL LETTER G WITH CARON]
//        	'\u01F4', // [LATIN CAPITAL LETTER G WITH ACUTE]
//        	'\u0262', // [LATIN LETTER SMALL CAPITAL G]
//        	'\u029B', // [LATIN LETTER SMALL CAPITAL G WITH HOOK]
//        	'\u1E20', // [LATIN CAPITAL LETTER G WITH MACRON]
//        	'\u24BC', // [CIRCLED LATIN CAPITAL LETTER G]
//        	'\uA77D', // [LATIN CAPITAL LETTER INSULAR G]
//        	'\uA77E', // [LATIN CAPITAL LETTER TURNED INSULAR G]
//        	'\uFF27', // [FULLWIDTH LATIN CAPITAL LETTER G]
//        }
//      },
//      { "g",
//        new() {
//          '\u011D', // [LATIN SMALL LETTER G WITH CIRCUMFLEX]
//        	'\u011F', // [LATIN SMALL LETTER G WITH BREVE]
//        	'\u0121', // [LATIN SMALL LETTER G WITH DOT ABOVE]
//        	'\u0123', // [LATIN SMALL LETTER G WITH CEDILLA]
//        	'\u01F5', // [LATIN SMALL LETTER G WITH ACUTE]
//        	'\u0260', // [LATIN SMALL LETTER G WITH HOOK]
//        	'\u0261', // [LATIN SMALL LETTER SCRIPT G]
//        	'\u1D77', // [LATIN SMALL LETTER TURNED G]
//        	'\u1D79', // [LATIN SMALL LETTER INSULAR G]
//        	'\u1D83', // [LATIN SMALL LETTER G WITH PALATAL HOOK]
//        	'\u1E21', // [LATIN SMALL LETTER G WITH MACRON]
//        	'\u24D6', // [CIRCLED LATIN SMALL LETTER G]
//        	'\uA77F', // [LATIN SMALL LETTER TURNED INSULAR G]
//        	'\uFF47', // [FULLWIDTH LATIN SMALL LETTER G]
//        }
//      },
//      { "(g)",
//        new() {
//          '\u24A2', // [PARENTHESIZED LATIN SMALL LETTER G]
//        }
//      },
//      { "H",
//        new() {
//          '\u0124', // [LATIN CAPITAL LETTER H WITH CIRCUMFLEX]
//        	'\u0126', // [LATIN CAPITAL LETTER H WITH STROKE]
//        	'\u021E', // [LATIN CAPITAL LETTER H WITH CARON]
//        	'\u029C', // [LATIN LETTER SMALL CAPITAL H]
//        	'\u1E22', // [LATIN CAPITAL LETTER H WITH DOT ABOVE]
//        	'\u1E24', // [LATIN CAPITAL LETTER H WITH DOT BELOW]
//        	'\u1E26', // [LATIN CAPITAL LETTER H WITH DIAERESIS]
//        	'\u1E28', // [LATIN CAPITAL LETTER H WITH CEDILLA]
//        	'\u1E2A', // [LATIN CAPITAL LETTER H WITH BREVE BELOW]
//        	'\u24BD', // [CIRCLED LATIN CAPITAL LETTER H]
//        	'\u2C67', // [LATIN CAPITAL LETTER H WITH DESCENDER]
//        	'\u2C75', // [LATIN CAPITAL LETTER HALF H]
//        	'\uFF28', // [FULLWIDTH LATIN CAPITAL LETTER H]
//        }
//      },
//      { "h",
//        new() {
//          '\u0125', // [LATIN SMALL LETTER H WITH CIRCUMFLEX]
//        	'\u0127', // [LATIN SMALL LETTER H WITH STROKE]
//        	'\u021F', // [LATIN SMALL LETTER H WITH CARON]
//        	'\u0265', // [LATIN SMALL LETTER TURNED H]
//        	'\u0266', // [LATIN SMALL LETTER H WITH HOOK]
//        	'\u02AE', // [LATIN SMALL LETTER TURNED H WITH FISHHOOK]
//        	'\u02AF', // [LATIN SMALL LETTER TURNED H WITH FISHHOOK AND TAIL]
//        	'\u1E23', // [LATIN SMALL LETTER H WITH DOT ABOVE]
//        	'\u1E25', // [LATIN SMALL LETTER H WITH DOT BELOW]
//        	'\u1E27', // [LATIN SMALL LETTER H WITH DIAERESIS]
//        	'\u1E29', // [LATIN SMALL LETTER H WITH CEDILLA]
//        	'\u1E2B', // [LATIN SMALL LETTER H WITH BREVE BELOW]
//        	'\u1E96', // [LATIN SMALL LETTER H WITH LINE BELOW]
//        	'\u24D7', // [CIRCLED LATIN SMALL LETTER H]
//        	'\u2C68', // [LATIN SMALL LETTER H WITH DESCENDER]
//        	'\u2C76', // [LATIN SMALL LETTER HALF H]
//        	'\uFF48', // [FULLWIDTH LATIN SMALL LETTER H]
//        }
//      },
//      { "HV",
//        new() {
//          '\u01F6', // [LATIN CAPITAL LETTER HWAIR]
//        }
//      },
//      { "(h)",
//        new() {
//          '\u24A3', // [PARENTHESIZED LATIN SMALL LETTER H]
//        }
//      },
//      { "hv",
//        new() {
//          '\u0195', // [LATIN SMALL LETTER HV]
//        }
//      },
//      { "I",
//        new() {
//          '\u00CC', // [LATIN CAPITAL LETTER I WITH GRAVE]
//        	'\u00CD', // [LATIN CAPITAL LETTER I WITH ACUTE]
//        	'\u00CE', // [LATIN CAPITAL LETTER I WITH CIRCUMFLEX]
//        	'\u00CF', // [LATIN CAPITAL LETTER I WITH DIAERESIS]
//        	'\u0128', // [LATIN CAPITAL LETTER I WITH TILDE]
//        	'\u012A', // [LATIN CAPITAL LETTER I WITH MACRON]
//        	'\u012C', // [LATIN CAPITAL LETTER I WITH BREVE]
//        	'\u012E', // [LATIN CAPITAL LETTER I WITH OGONEK]
//        	'\u0130', // [LATIN CAPITAL LETTER I WITH DOT ABOVE]
//        	'\u0196', // [LATIN CAPITAL LETTER IOTA]
//        	'\u0197', // [LATIN CAPITAL LETTER I WITH STROKE]
//        	'\u01CF', // [LATIN CAPITAL LETTER I WITH CARON]
//        	'\u0208', // [LATIN CAPITAL LETTER I WITH DOUBLE GRAVE]
//        	'\u020A', // [LATIN CAPITAL LETTER I WITH INVERTED BREVE]
//        	'\u026A', // [LATIN LETTER SMALL CAPITAL I]
//        	'\u1D7B', // [LATIN SMALL CAPITAL LETTER I WITH STROKE]
//        	'\u1E2C', // [LATIN CAPITAL LETTER I WITH TILDE BELOW]
//        	'\u1E2E', // [LATIN CAPITAL LETTER I WITH DIAERESIS AND ACUTE]
//        	'\u1EC8', // [LATIN CAPITAL LETTER I WITH HOOK ABOVE]
//        	'\u1ECA', // [LATIN CAPITAL LETTER I WITH DOT BELOW]
//        	'\u24BE', // [CIRCLED LATIN CAPITAL LETTER I]
//        	'\uA7FE', // [LATIN EPIGRAPHIC LETTER I LONGA]
//        	'\uFF29', // [FULLWIDTH LATIN CAPITAL LETTER I]
//        }
//      },
//      { "i",
//        new() {
//          '\u00EC', // [LATIN SMALL LETTER I WITH GRAVE]
//        	'\u00ED', // [LATIN SMALL LETTER I WITH ACUTE]
//        	'\u00EE', // [LATIN SMALL LETTER I WITH CIRCUMFLEX]
//        	'\u00EF', // [LATIN SMALL LETTER I WITH DIAERESIS]
//        	'\u0129', // [LATIN SMALL LETTER I WITH TILDE]
//        	'\u012B', // [LATIN SMALL LETTER I WITH MACRON]
//        	'\u012D', // [LATIN SMALL LETTER I WITH BREVE]
//        	'\u012F', // [LATIN SMALL LETTER I WITH OGONEK]
//        	'\u0131', // [LATIN SMALL LETTER DOTLESS I]
//        	'\u01D0', // [LATIN SMALL LETTER I WITH CARON]
//        	'\u0209', // [LATIN SMALL LETTER I WITH DOUBLE GRAVE]
//        	'\u020B', // [LATIN SMALL LETTER I WITH INVERTED BREVE]
//        	'\u0268', // [LATIN SMALL LETTER I WITH STROKE]
//        	'\u1D09', // [LATIN SMALL LETTER TURNED I]
//        	'\u1D62', // [LATIN SUBSCRIPT SMALL LETTER I]
//        	'\u1D7C', // [LATIN SMALL LETTER IOTA WITH STROKE]
//        	'\u1D96', // [LATIN SMALL LETTER I WITH RETROFLEX HOOK]
//        	'\u1E2D', // [LATIN SMALL LETTER I WITH TILDE BELOW]
//        	'\u1E2F', // [LATIN SMALL LETTER I WITH DIAERESIS AND ACUTE]
//        	'\u1EC9', // [LATIN SMALL LETTER I WITH HOOK ABOVE]
//        	'\u1ECB', // [LATIN SMALL LETTER I WITH DOT BELOW]
//        	'\u2071', // [SUPERSCRIPT LATIN SMALL LETTER I]
//        	'\u24D8', // [CIRCLED LATIN SMALL LETTER I]
//        	'\uFF49', // [FULLWIDTH LATIN SMALL LETTER I]
//        }
//      },
//      { "IJ",
//        new() {
//          '\u0132', // [LATIN CAPITAL LIGATURE IJ]
//        }
//      },
//      { "(i)",
//        new() {
//          '\u24A4', // [PARENTHESIZED LATIN SMALL LETTER I]
//        }
//      },
//      { "ij",
//        new() {
//          '\u0133', // [LATIN SMALL LIGATURE IJ]
//        }
//      },
//      { "J",
//        new() {
//          '\u0134', // [LATIN CAPITAL LETTER J WITH CIRCUMFLEX]
//        	'\u0248', // [LATIN CAPITAL LETTER J WITH STROKE]
//        	'\u1D0A', // [LATIN LETTER SMALL CAPITAL J]
//        	'\u24BF', // [CIRCLED LATIN CAPITAL LETTER J]
//        	'\uFF2A', // [FULLWIDTH LATIN CAPITAL LETTER J]
//        }
//      },
//      { "j",
//        new() {
//          '\u0135', // [LATIN SMALL LETTER J WITH CIRCUMFLEX]
//        	'\u01F0', // [LATIN SMALL LETTER J WITH CARON]
//        	'\u0237', // [LATIN SMALL LETTER DOTLESS J]
//        	'\u0249', // [LATIN SMALL LETTER J WITH STROKE]
//        	'\u025F', // [LATIN SMALL LETTER DOTLESS J WITH STROKE]
//        	'\u0284', // [LATIN SMALL LETTER DOTLESS J WITH STROKE AND HOOK]
//        	'\u029D', // [LATIN SMALL LETTER J WITH CROSSED-TAIL]
//        	'\u24D9', // [CIRCLED LATIN SMALL LETTER J]
//        	'\u2C7C', // [LATIN SUBSCRIPT SMALL LETTER J]
//        	'\uFF4A', // [FULLWIDTH LATIN SMALL LETTER J]
//        }
//      },
//      { "(j)",
//        new() {
//          '\u24A5', // [PARENTHESIZED LATIN SMALL LETTER J]
//        }
//      },
//      { "K",
//        new() {
//          '\u0136', // [LATIN CAPITAL LETTER K WITH CEDILLA]
//        	'\u0198', // [LATIN CAPITAL LETTER K WITH HOOK]
//        	'\u01E8', // [LATIN CAPITAL LETTER K WITH CARON]
//        	'\u1D0B', // [LATIN LETTER SMALL CAPITAL K]
//        	'\u1E30', // [LATIN CAPITAL LETTER K WITH ACUTE]
//        	'\u1E32', // [LATIN CAPITAL LETTER K WITH DOT BELOW]
//        	'\u1E34', // [LATIN CAPITAL LETTER K WITH LINE BELOW]
//        	'\u24C0', // [CIRCLED LATIN CAPITAL LETTER K]
//        	'\u2C69', // [LATIN CAPITAL LETTER K WITH DESCENDER]
//        	'\uA740', // [LATIN CAPITAL LETTER K WITH STROKE]
//        	'\uA742', // [LATIN CAPITAL LETTER K WITH DIAGONAL STROKE]
//        	'\uA744', // [LATIN CAPITAL LETTER K WITH STROKE AND DIAGONAL STROKE]
//        	'\uFF2B', // [FULLWIDTH LATIN CAPITAL LETTER K]
//        }
//      },
//      { "k",
//        new() {
//          '\u0137', // [LATIN SMALL LETTER K WITH CEDILLA]
//        	'\u0199', // [LATIN SMALL LETTER K WITH HOOK]
//        	'\u01E9', // [LATIN SMALL LETTER K WITH CARON]
//        	'\u029E', // [LATIN SMALL LETTER TURNED K]
//        	'\u1D84', // [LATIN SMALL LETTER K WITH PALATAL HOOK]
//        	'\u1E31', // [LATIN SMALL LETTER K WITH ACUTE]
//        	'\u1E33', // [LATIN SMALL LETTER K WITH DOT BELOW]
//        	'\u1E35', // [LATIN SMALL LETTER K WITH LINE BELOW]
//        	'\u24DA', // [CIRCLED LATIN SMALL LETTER K]
//        	'\u2C6A', // [LATIN SMALL LETTER K WITH DESCENDER]
//        	'\uA741', // [LATIN SMALL LETTER K WITH STROKE]
//        	'\uA743', // [LATIN SMALL LETTER K WITH DIAGONAL STROKE]
//        	'\uA745', // [LATIN SMALL LETTER K WITH STROKE AND DIAGONAL STROKE]
//        	'\uFF4B', // [FULLWIDTH LATIN SMALL LETTER K]
//        }
//      },
//      { "(k)",
//        new() {
//          '\u24A6', // [PARENTHESIZED LATIN SMALL LETTER K]
//        }
//      },
//      { "L",
//        new() {
//          '\u0139', // [LATIN CAPITAL LETTER L WITH ACUTE]
//        	'\u013B', // [LATIN CAPITAL LETTER L WITH CEDILLA]
//        	'\u013D', // [LATIN CAPITAL LETTER L WITH CARON]
//        	'\u013F', // [LATIN CAPITAL LETTER L WITH MIDDLE DOT]
//        	'\u0141', // [LATIN CAPITAL LETTER L WITH STROKE]
//        	'\u023D', // [LATIN CAPITAL LETTER L WITH BAR]
//        	'\u029F', // [LATIN LETTER SMALL CAPITAL L]
//        	'\u1D0C', // [LATIN LETTER SMALL CAPITAL L WITH STROKE]
//        	'\u1E36', // [LATIN CAPITAL LETTER L WITH DOT BELOW]
//        	'\u1E38', // [LATIN CAPITAL LETTER L WITH DOT BELOW AND MACRON]
//        	'\u1E3A', // [LATIN CAPITAL LETTER L WITH LINE BELOW]
//        	'\u1E3C', // [LATIN CAPITAL LETTER L WITH CIRCUMFLEX BELOW]
//        	'\u24C1', // [CIRCLED LATIN CAPITAL LETTER L]
//        	'\u2C60', // [LATIN CAPITAL LETTER L WITH DOUBLE BAR]
//        	'\u2C62', // [LATIN CAPITAL LETTER L WITH MIDDLE TILDE]
//        	'\uA746', // [LATIN CAPITAL LETTER BROKEN L]
//        	'\uA748', // [LATIN CAPITAL LETTER L WITH HIGH STROKE]
//        	'\uA780', // [LATIN CAPITAL LETTER TURNED L]
//        	'\uFF2C', // [FULLWIDTH LATIN CAPITAL LETTER L]
//        }
//      },
//      { "l",
//        new() {
//          '\u013A', // [LATIN SMALL LETTER L WITH ACUTE]
//        	'\u013C', // [LATIN SMALL LETTER L WITH CEDILLA]
//        	'\u013E', // [LATIN SMALL LETTER L WITH CARON]
//        	'\u0140', // [LATIN SMALL LETTER L WITH MIDDLE DOT]
//        	'\u0142', // [LATIN SMALL LETTER L WITH STROKE]
//        	'\u019A', // [LATIN SMALL LETTER L WITH BAR]
//        	'\u0234', // [LATIN SMALL LETTER L WITH CURL]
//        	'\u026B', // [LATIN SMALL LETTER L WITH MIDDLE TILDE]
//        	'\u026C', // [LATIN SMALL LETTER L WITH BELT]
//        	'\u026D', // [LATIN SMALL LETTER L WITH RETROFLEX HOOK]
//        	'\u1D85', // [LATIN SMALL LETTER L WITH PALATAL HOOK]
//        	'\u1E37', // [LATIN SMALL LETTER L WITH DOT BELOW]
//        	'\u1E39', // [LATIN SMALL LETTER L WITH DOT BELOW AND MACRON]
//        	'\u1E3B', // [LATIN SMALL LETTER L WITH LINE BELOW]
//        	'\u1E3D', // [LATIN SMALL LETTER L WITH CIRCUMFLEX BELOW]
//        	'\u24DB', // [CIRCLED LATIN SMALL LETTER L]
//        	'\u2C61', // [LATIN SMALL LETTER L WITH DOUBLE BAR]
//        	'\uA747', // [LATIN SMALL LETTER BROKEN L]
//        	'\uA749', // [LATIN SMALL LETTER L WITH HIGH STROKE]
//        	'\uA781', // [LATIN SMALL LETTER TURNED L]
//        	'\uFF4C', // [FULLWIDTH LATIN SMALL LETTER L]
//        }
//      },
//      { "LJ",
//        new() {
//          '\u01C7', // [LATIN CAPITAL LETTER LJ]
//        }
//      },
//      { "LL",
//        new() {
//          '\u1EFA', // [LATIN CAPITAL LETTER MIDDLE-WELSH LL]
//        }
//      },
//      { "Lj",
//        new() {
//          '\u01C8', // [LATIN CAPITAL LETTER L WITH SMALL LETTER J]
//        }
//      },
//      { "(l)",
//        new() {
//          '\u24A7', // [PARENTHESIZED LATIN SMALL LETTER L]
//        }
//      },
//      { "lj",
//        new() {
//          '\u01C9', // [LATIN SMALL LETTER LJ]
//        }
//      },
//      { "ll",
//        new() {
//          '\u1EFB', // [LATIN SMALL LETTER MIDDLE-WELSH LL]
//        }
//      },
//      { "ls",
//        new() {
//          '\u02AA', // [LATIN SMALL LETTER LS DIGRAPH]
//        }
//      },
//      { "lz",
//        new() {
//          '\u02AB', // [LATIN SMALL LETTER LZ DIGRAPH]
//        }
//      },
//      { "M",
//        new() {
//          '\u019C', // [LATIN CAPITAL LETTER TURNED M]
//        	'\u1D0D', // [LATIN LETTER SMALL CAPITAL M]
//        	'\u1E3E', // [LATIN CAPITAL LETTER M WITH ACUTE]
//        	'\u1E40', // [LATIN CAPITAL LETTER M WITH DOT ABOVE]
//        	'\u1E42', // [LATIN CAPITAL LETTER M WITH DOT BELOW]
//        	'\u24C2', // [CIRCLED LATIN CAPITAL LETTER M]
//        	'\u2C6E', // [LATIN CAPITAL LETTER M WITH HOOK]
//        	'\uA7FD', // [LATIN EPIGRAPHIC LETTER INVERTED M]
//        	'\uA7FF', // [LATIN EPIGRAPHIC LETTER ARCHAIC M]
//        	'\uFF2D', // [FULLWIDTH LATIN CAPITAL LETTER M]
//        }
//      },
//      { "m",
//        new() {
//          '\u026F', // [LATIN SMALL LETTER TURNED M]
//        	'\u0270', // [LATIN SMALL LETTER TURNED M WITH LONG LEG]
//        	'\u0271', // [LATIN SMALL LETTER M WITH HOOK]
//        	'\u1D6F', // [LATIN SMALL LETTER M WITH MIDDLE TILDE]
//        	'\u1D86', // [LATIN SMALL LETTER M WITH PALATAL HOOK]
//        	'\u1E3F', // [LATIN SMALL LETTER M WITH ACUTE]
//        	'\u1E41', // [LATIN SMALL LETTER M WITH DOT ABOVE]
//        	'\u1E43', // [LATIN SMALL LETTER M WITH DOT BELOW]
//        	'\u24DC', // [CIRCLED LATIN SMALL LETTER M]
//        	'\uFF4D', // [FULLWIDTH LATIN SMALL LETTER M]
//        }
//      },
//      { "(m)",
//        new() {
//          '\u24A8', // [PARENTHESIZED LATIN SMALL LETTER M]
//        }
//      },
//      { "N",
//        new() {
//          '\u00D1', // [LATIN CAPITAL LETTER N WITH TILDE]
//        	'\u0143', // [LATIN CAPITAL LETTER N WITH ACUTE]
//        	'\u0145', // [LATIN CAPITAL LETTER N WITH CEDILLA]
//        	'\u0147', // [LATIN CAPITAL LETTER N WITH CARON]
//        	'\u014A', // [LATIN CAPITAL LETTER ENG]
//        	'\u019D', // [LATIN CAPITAL LETTER N WITH LEFT HOOK]
//        	'\u01F8', // [LATIN CAPITAL LETTER N WITH GRAVE]
//        	'\u0220', // [LATIN CAPITAL LETTER N WITH LONG RIGHT LEG]
//        	'\u0274', // [LATIN LETTER SMALL CAPITAL N]
//        	'\u1D0E', // [LATIN LETTER SMALL CAPITAL REVERSED N]
//        	'\u1E44', // [LATIN CAPITAL LETTER N WITH DOT ABOVE]
//        	'\u1E46', // [LATIN CAPITAL LETTER N WITH DOT BELOW]
//        	'\u1E48', // [LATIN CAPITAL LETTER N WITH LINE BELOW]
//        	'\u1E4A', // [LATIN CAPITAL LETTER N WITH CIRCUMFLEX BELOW]
//        	'\u24C3', // [CIRCLED LATIN CAPITAL LETTER N]
//        	'\uFF2E', // [FULLWIDTH LATIN CAPITAL LETTER N]
//        }
//      },
//      { "n",
//        new() {
//          '\u00F1', // [LATIN SMALL LETTER N WITH TILDE]
//        	'\u0144', // [LATIN SMALL LETTER N WITH ACUTE]
//        	'\u0146', // [LATIN SMALL LETTER N WITH CEDILLA]
//        	'\u0148', // [LATIN SMALL LETTER N WITH CARON]
//        	'\u0149', // [LATIN SMALL LETTER N PRECEDED BY APOSTROPHE]
//        	'\u014B', // [LATIN SMALL LETTER ENG]
//        	'\u019E', // [LATIN SMALL LETTER N WITH LONG RIGHT LEG]
//        	'\u01F9', // [LATIN SMALL LETTER N WITH GRAVE]
//        	'\u0235', // [LATIN SMALL LETTER N WITH CURL]
//        	'\u0272', // [LATIN SMALL LETTER N WITH LEFT HOOK]
//        	'\u0273', // [LATIN SMALL LETTER N WITH RETROFLEX HOOK]
//        	'\u1D70', // [LATIN SMALL LETTER N WITH MIDDLE TILDE]
//        	'\u1D87', // [LATIN SMALL LETTER N WITH PALATAL HOOK]
//        	'\u1E45', // [LATIN SMALL LETTER N WITH DOT ABOVE]
//        	'\u1E47', // [LATIN SMALL LETTER N WITH DOT BELOW]
//        	'\u1E49', // [LATIN SMALL LETTER N WITH LINE BELOW]
//        	'\u1E4B', // [LATIN SMALL LETTER N WITH CIRCUMFLEX BELOW]
//        	'\u207F', // [SUPERSCRIPT LATIN SMALL LETTER N]
//        	'\u24DD', // [CIRCLED LATIN SMALL LETTER N]
//        	'\uFF4E', // [FULLWIDTH LATIN SMALL LETTER N]
//        }
//      },
//      { "NJ",
//        new() {
//          '\u01CA', // [LATIN CAPITAL LETTER NJ]
//        }
//      },
//      { "Nj",
//        new() {
//          '\u01CB', // [LATIN CAPITAL LETTER N WITH SMALL LETTER J]
//        }
//      },
//      { "(n)",
//        new() {
//          '\u24A9', // [PARENTHESIZED LATIN SMALL LETTER N]
//        }
//      },
//      { "nj",
//        new() {
//          '\u01CC', // [LATIN SMALL LETTER NJ]
//        }
//      },
//      { "O",
//        new() {
//          '\u00D2', // [LATIN CAPITAL LETTER O WITH GRAVE]
//        	'\u00D3', // [LATIN CAPITAL LETTER O WITH ACUTE]
//        	'\u00D4', // [LATIN CAPITAL LETTER O WITH CIRCUMFLEX]
//        	'\u00D5', // [LATIN CAPITAL LETTER O WITH TILDE]
//        	'\u00D6', // [LATIN CAPITAL LETTER O WITH DIAERESIS]
//        	'\u00D8', // [LATIN CAPITAL LETTER O WITH STROKE]
//        	'\u014C', // [LATIN CAPITAL LETTER O WITH MACRON]
//        	'\u014E', // [LATIN CAPITAL LETTER O WITH BREVE]
//        	'\u0150', // [LATIN CAPITAL LETTER O WITH DOUBLE ACUTE]
//        	'\u0186', // [LATIN CAPITAL LETTER OPEN O]
//        	'\u019F', // [LATIN CAPITAL LETTER O WITH MIDDLE TILDE]
//        	'\u01A0', // [LATIN CAPITAL LETTER O WITH HORN]
//        	'\u01D1', // [LATIN CAPITAL LETTER O WITH CARON]
//        	'\u01EA', // [LATIN CAPITAL LETTER O WITH OGONEK]
//        	'\u01EC', // [LATIN CAPITAL LETTER O WITH OGONEK AND MACRON]
//        	'\u01FE', // [LATIN CAPITAL LETTER O WITH STROKE AND ACUTE]
//        	'\u020C', // [LATIN CAPITAL LETTER O WITH DOUBLE GRAVE]
//        	'\u020E', // [LATIN CAPITAL LETTER O WITH INVERTED BREVE]
//        	'\u022A', // [LATIN CAPITAL LETTER O WITH DIAERESIS AND MACRON]
//        	'\u022C', // [LATIN CAPITAL LETTER O WITH TILDE AND MACRON]
//        	'\u022E', // [LATIN CAPITAL LETTER O WITH DOT ABOVE]
//        	'\u0230', // [LATIN CAPITAL LETTER O WITH DOT ABOVE AND MACRON]
//        	'\u1D0F', // [LATIN LETTER SMALL CAPITAL O]
//        	'\u1D10', // [LATIN LETTER SMALL CAPITAL OPEN O]
//        	'\u1E4C', // [LATIN CAPITAL LETTER O WITH TILDE AND ACUTE]
//        	'\u1E4E', // [LATIN CAPITAL LETTER O WITH TILDE AND DIAERESIS]
//        	'\u1E50', // [LATIN CAPITAL LETTER O WITH MACRON AND GRAVE]
//        	'\u1E52', // [LATIN CAPITAL LETTER O WITH MACRON AND ACUTE]
//        	'\u1ECC', // [LATIN CAPITAL LETTER O WITH DOT BELOW]
//        	'\u1ECE', // [LATIN CAPITAL LETTER O WITH HOOK ABOVE]
//        	'\u1ED0', // [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND ACUTE]
//        	'\u1ED2', // [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND GRAVE]
//        	'\u1ED4', // [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE]
//        	'\u1ED6', // [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND TILDE]
//        	'\u1ED8', // [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND DOT BELOW]
//        	'\u1EDA', // [LATIN CAPITAL LETTER O WITH HORN AND ACUTE]
//        	'\u1EDC', // [LATIN CAPITAL LETTER O WITH HORN AND GRAVE]
//        	'\u1EDE', // [LATIN CAPITAL LETTER O WITH HORN AND HOOK ABOVE]
//        	'\u1EE0', // [LATIN CAPITAL LETTER O WITH HORN AND TILDE]
//        	'\u1EE2', // [LATIN CAPITAL LETTER O WITH HORN AND DOT BELOW]
//        	'\u24C4', // [CIRCLED LATIN CAPITAL LETTER O]
//        	'\uA74A', // [LATIN CAPITAL LETTER O WITH LONG STROKE OVERLAY]
//        	'\uA74C', // [LATIN CAPITAL LETTER O WITH LOOP]
//        	'\uFF2F', // [FULLWIDTH LATIN CAPITAL LETTER O]
//        }
//      },
//      { "o",
//        new() {
//          '\u00F2', // [LATIN SMALL LETTER O WITH GRAVE]
//        	'\u00F3', // [LATIN SMALL LETTER O WITH ACUTE]
//        	'\u00F4', // [LATIN SMALL LETTER O WITH CIRCUMFLEX]
//        	'\u00F5', // [LATIN SMALL LETTER O WITH TILDE]
//        	'\u00F6', // [LATIN SMALL LETTER O WITH DIAERESIS]
//        	'\u00F8', // [LATIN SMALL LETTER O WITH STROKE]
//        	'\u014D', // [LATIN SMALL LETTER O WITH MACRON]
//        	'\u014F', // [LATIN SMALL LETTER O WITH BREVE]
//        	'\u0151', // [LATIN SMALL LETTER O WITH DOUBLE ACUTE]
//        	'\u01A1', // [LATIN SMALL LETTER O WITH HORN]
//        	'\u01D2', // [LATIN SMALL LETTER O WITH CARON]
//        	'\u01EB', // [LATIN SMALL LETTER O WITH OGONEK]
//        	'\u01ED', // [LATIN SMALL LETTER O WITH OGONEK AND MACRON]
//        	'\u01FF', // [LATIN SMALL LETTER O WITH STROKE AND ACUTE]
//        	'\u020D', // [LATIN SMALL LETTER O WITH DOUBLE GRAVE]
//        	'\u020F', // [LATIN SMALL LETTER O WITH INVERTED BREVE]
//        	'\u022B', // [LATIN SMALL LETTER O WITH DIAERESIS AND MACRON]
//        	'\u022D', // [LATIN SMALL LETTER O WITH TILDE AND MACRON]
//        	'\u022F', // [LATIN SMALL LETTER O WITH DOT ABOVE]
//        	'\u0231', // [LATIN SMALL LETTER O WITH DOT ABOVE AND MACRON]
//        	'\u0254', // [LATIN SMALL LETTER OPEN O]
//        	'\u0275', // [LATIN SMALL LETTER BARRED O]
//        	'\u1D16', // [LATIN SMALL LETTER TOP HALF O]
//        	'\u1D17', // [LATIN SMALL LETTER BOTTOM HALF O]
//        	'\u1D97', // [LATIN SMALL LETTER OPEN O WITH RETROFLEX HOOK]
//        	'\u1E4D', // [LATIN SMALL LETTER O WITH TILDE AND ACUTE]
//        	'\u1E4F', // [LATIN SMALL LETTER O WITH TILDE AND DIAERESIS]
//        	'\u1E51', // [LATIN SMALL LETTER O WITH MACRON AND GRAVE]
//        	'\u1E53', // [LATIN SMALL LETTER O WITH MACRON AND ACUTE]
//        	'\u1ECD', // [LATIN SMALL LETTER O WITH DOT BELOW]
//        	'\u1ECF', // [LATIN SMALL LETTER O WITH HOOK ABOVE]
//        	'\u1ED1', // [LATIN SMALL LETTER O WITH CIRCUMFLEX AND ACUTE]
//        	'\u1ED3', // [LATIN SMALL LETTER O WITH CIRCUMFLEX AND GRAVE]
//        	'\u1ED5', // [LATIN SMALL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE]
//        	'\u1ED7', // [LATIN SMALL LETTER O WITH CIRCUMFLEX AND TILDE]
//        	'\u1ED9', // [LATIN SMALL LETTER O WITH CIRCUMFLEX AND DOT BELOW]
//        	'\u1EDB', // [LATIN SMALL LETTER O WITH HORN AND ACUTE]
//        	'\u1EDD', // [LATIN SMALL LETTER O WITH HORN AND GRAVE]
//        	'\u1EDF', // [LATIN SMALL LETTER O WITH HORN AND HOOK ABOVE]
//        	'\u1EE1', // [LATIN SMALL LETTER O WITH HORN AND TILDE]
//        	'\u1EE3', // [LATIN SMALL LETTER O WITH HORN AND DOT BELOW]
//        	'\u2092', // [LATIN SUBSCRIPT SMALL LETTER O]
//        	'\u24DE', // [CIRCLED LATIN SMALL LETTER O]
//        	'\u2C7A', // [LATIN SMALL LETTER O WITH LOW RING INSIDE]
//        	'\uA74B', // [LATIN SMALL LETTER O WITH LONG STROKE OVERLAY]
//        	'\uA74D', // [LATIN SMALL LETTER O WITH LOOP]
//        	'\uFF4F', // [FULLWIDTH LATIN SMALL LETTER O]
//        }
//      },
//      { "OE",
//        new() {
//          '\u0152', // [LATIN CAPITAL LIGATURE OE]
//        	'\u0276', // [LATIN LETTER SMALL CAPITAL OE]
//        }
//      },
//      { "OO",
//        new() {
//          '\uA74E', // [LATIN CAPITAL LETTER OO]
//        }
//      },
//      { "OU",
//        new() {
//          '\u0222', // [LATIN CAPITAL LETTER OU]
//        	'\u1D15', // [LATIN LETTER SMALL CAPITAL OU]
//        }
//      },
//      { "(o)",
//        new() {
//          '\u24AA', // [PARENTHESIZED LATIN SMALL LETTER O]
//        }
//      },
//      { "oe",
//        new() {
//          '\u0153', // [LATIN SMALL LIGATURE OE]
//        	'\u1D14', // [LATIN SMALL LETTER TURNED OE]
//        }
//      },
//      { "oo",
//        new() {
//          '\uA74F', // [LATIN SMALL LETTER OO]
//        }
//      },
//      { "ou",
//        new() {
//          '\u0223', // [LATIN SMALL LETTER OU]
//        }
//      },
//      { "P",
//        new() {
//          '\u01A4', // [LATIN CAPITAL LETTER P WITH HOOK]
//        	'\u1D18', // [LATIN LETTER SMALL CAPITAL P]
//        	'\u1E54', // [LATIN CAPITAL LETTER P WITH ACUTE]
//        	'\u1E56', // [LATIN CAPITAL LETTER P WITH DOT ABOVE]
//        	'\u24C5', // [CIRCLED LATIN CAPITAL LETTER P]
//        	'\u2C63', // [LATIN CAPITAL LETTER P WITH STROKE]
//        	'\uA750', // [LATIN CAPITAL LETTER P WITH STROKE THROUGH DESCENDER]
//        	'\uA752', // [LATIN CAPITAL LETTER P WITH FLOURISH]
//        	'\uA754', // [LATIN CAPITAL LETTER P WITH SQUIRREL TAIL]
//        	'\uFF30', // [FULLWIDTH LATIN CAPITAL LETTER P]
//        }
//      },
//      { "p",
//        new() {
//          '\u01A5', // [LATIN SMALL LETTER P WITH HOOK]
//        	'\u1D71', // [LATIN SMALL LETTER P WITH MIDDLE TILDE]
//        	'\u1D7D', // [LATIN SMALL LETTER P WITH STROKE]
//        	'\u1D88', // [LATIN SMALL LETTER P WITH PALATAL HOOK]
//        	'\u1E55', // [LATIN SMALL LETTER P WITH ACUTE]
//        	'\u1E57', // [LATIN SMALL LETTER P WITH DOT ABOVE]
//        	'\u24DF', // [CIRCLED LATIN SMALL LETTER P]
//        	'\uA751', // [LATIN SMALL LETTER P WITH STROKE THROUGH DESCENDER]
//        	'\uA753', // [LATIN SMALL LETTER P WITH FLOURISH]
//        	'\uA755', // [LATIN SMALL LETTER P WITH SQUIRREL TAIL]
//        	'\uA7FC', // [LATIN EPIGRAPHIC LETTER REVERSED P]
//        	'\uFF50', // [FULLWIDTH LATIN SMALL LETTER P]
//        }
//      },
//      { "(p)",
//        new() {
//          '\u24AB', // [PARENTHESIZED LATIN SMALL LETTER P]
//        }
//      },
//      { "Q",
//        new() {
//          '\u024A', // [LATIN CAPITAL LETTER SMALL Q WITH HOOK TAIL]
//        	'\u24C6', // [CIRCLED LATIN CAPITAL LETTER Q]
//        	'\uA756', // [LATIN CAPITAL LETTER Q WITH STROKE THROUGH DESCENDER]
//        	'\uA758', // [LATIN CAPITAL LETTER Q WITH DIAGONAL STROKE]
//        	'\uFF31', // [FULLWIDTH LATIN CAPITAL LETTER Q]
//        }
//      },
//      { "q",
//        new() {
//          '\u0138', // [LATIN SMALL LETTER KRA]
//        	'\u024B', // [LATIN SMALL LETTER Q WITH HOOK TAIL]
//        	'\u02A0', // [LATIN SMALL LETTER Q WITH HOOK]
//        	'\u24E0', // [CIRCLED LATIN SMALL LETTER Q]
//        	'\uA757', // [LATIN SMALL LETTER Q WITH STROKE THROUGH DESCENDER]
//        	'\uA759', // [LATIN SMALL LETTER Q WITH DIAGONAL STROKE]
//        	'\uFF51', // [FULLWIDTH LATIN SMALL LETTER Q]
//        }
//      },
//      { "(q)",
//        new() {
//          '\u24AC', // [PARENTHESIZED LATIN SMALL LETTER Q]
//        }
//      },
//      { "qp",
//        new() {
//          '\u0239', // [LATIN SMALL LETTER QP DIGRAPH]
//        }
//      },
//      { "R",
//        new() {
//          '\u0154', // [LATIN CAPITAL LETTER R WITH ACUTE]
//        	'\u0156', // [LATIN CAPITAL LETTER R WITH CEDILLA]
//        	'\u0158', // [LATIN CAPITAL LETTER R WITH CARON]
//        	'\u0210', // [LATIN CAPITAL LETTER R WITH DOUBLE GRAVE]
//        	'\u0212', // [LATIN CAPITAL LETTER R WITH INVERTED BREVE]
//        	'\u024C', // [LATIN CAPITAL LETTER R WITH STROKE]
//        	'\u0280', // [LATIN LETTER SMALL CAPITAL R]
//        	'\u0281', // [LATIN LETTER SMALL CAPITAL INVERTED R]
//        	'\u1D19', // [LATIN LETTER SMALL CAPITAL REVERSED R]
//        	'\u1D1A', // [LATIN LETTER SMALL CAPITAL TURNED R]
//        	'\u1E58', // [LATIN CAPITAL LETTER R WITH DOT ABOVE]
//        	'\u1E5A', // [LATIN CAPITAL LETTER R WITH DOT BELOW]
//        	'\u1E5C', // [LATIN CAPITAL LETTER R WITH DOT BELOW AND MACRON]
//        	'\u1E5E', // [LATIN CAPITAL LETTER R WITH LINE BELOW]
//        	'\u24C7', // [CIRCLED LATIN CAPITAL LETTER R]
//        	'\u2C64', // [LATIN CAPITAL LETTER R WITH TAIL]
//        	'\uA75A', // [LATIN CAPITAL LETTER R ROTUNDA]
//        	'\uA782', // [LATIN CAPITAL LETTER INSULAR R]
//        	'\uFF32', // [FULLWIDTH LATIN CAPITAL LETTER R]
//        }
//      },
//      { "r",
//        new() {
//          '\u0155', // [LATIN SMALL LETTER R WITH ACUTE]
//        	'\u0157', // [LATIN SMALL LETTER R WITH CEDILLA]
//        	'\u0159', // [LATIN SMALL LETTER R WITH CARON]
//        	'\u0211', // [LATIN SMALL LETTER R WITH DOUBLE GRAVE]
//        	'\u0213', // [LATIN SMALL LETTER R WITH INVERTED BREVE]
//        	'\u024D', // [LATIN SMALL LETTER R WITH STROKE]
//        	'\u027C', // [LATIN SMALL LETTER R WITH LONG LEG]
//        	'\u027D', // [LATIN SMALL LETTER R WITH TAIL]
//        	'\u027E', // [LATIN SMALL LETTER R WITH FISHHOOK]
//        	'\u027F', // [LATIN SMALL LETTER REVERSED R WITH FISHHOOK]
//        	'\u1D63', // [LATIN SUBSCRIPT SMALL LETTER R]
//        	'\u1D72', // [LATIN SMALL LETTER R WITH MIDDLE TILDE]
//        	'\u1D73', // [LATIN SMALL LETTER R WITH FISHHOOK AND MIDDLE TILDE]
//        	'\u1D89', // [LATIN SMALL LETTER R WITH PALATAL HOOK]
//        	'\u1E59', // [LATIN SMALL LETTER R WITH DOT ABOVE]
//        	'\u1E5B', // [LATIN SMALL LETTER R WITH DOT BELOW]
//        	'\u1E5D', // [LATIN SMALL LETTER R WITH DOT BELOW AND MACRON]
//        	'\u1E5F', // [LATIN SMALL LETTER R WITH LINE BELOW]
//        	'\u24E1', // [CIRCLED LATIN SMALL LETTER R]
//        	'\uA75B', // [LATIN SMALL LETTER R ROTUNDA]
//        	'\uA783', // [LATIN SMALL LETTER INSULAR R]
//        	'\uFF52', // [FULLWIDTH LATIN SMALL LETTER R]
//        }
//      },
//      { "(r)",
//        new() {
//          '\u24AD', // [PARENTHESIZED LATIN SMALL LETTER R]
//        }
//      },
//      { "S",
//        new() {
//          '\u015A', // [LATIN CAPITAL LETTER S WITH ACUTE]
//        	'\u015C', // [LATIN CAPITAL LETTER S WITH CIRCUMFLEX]
//        	'\u015E', // [LATIN CAPITAL LETTER S WITH CEDILLA]
//        	'\u0160', // [LATIN CAPITAL LETTER S WITH CARON]
//        	'\u0218', // [LATIN CAPITAL LETTER S WITH COMMA BELOW]
//        	'\u1E60', // [LATIN CAPITAL LETTER S WITH DOT ABOVE]
//        	'\u1E62', // [LATIN CAPITAL LETTER S WITH DOT BELOW]
//        	'\u1E64', // [LATIN CAPITAL LETTER S WITH ACUTE AND DOT ABOVE]
//        	'\u1E66', // [LATIN CAPITAL LETTER S WITH CARON AND DOT ABOVE]
//        	'\u1E68', // [LATIN CAPITAL LETTER S WITH DOT BELOW AND DOT ABOVE]
//        	'\u24C8', // [CIRCLED LATIN CAPITAL LETTER S]
//        	'\uA731', // [LATIN LETTER SMALL CAPITAL S]
//        	'\uA785', // [LATIN SMALL LETTER INSULAR S]
//        	'\uFF33', // [FULLWIDTH LATIN CAPITAL LETTER S]
//        }
//      },
//      { "s",
//        new() {
//          '\u015B', // [LATIN SMALL LETTER S WITH ACUTE]
//        	'\u015D', // [LATIN SMALL LETTER S WITH CIRCUMFLEX]
//        	'\u015F', // [LATIN SMALL LETTER S WITH CEDILLA]
//        	'\u0161', // [LATIN SMALL LETTER S WITH CARON]
//        	'\u017F', // [LATIN SMALL LETTER LONG S]
//        	'\u0219', // [LATIN SMALL LETTER S WITH COMMA BELOW]
//        	'\u023F', // [LATIN SMALL LETTER S WITH SWASH TAIL]
//        	'\u0282', // [LATIN SMALL LETTER S WITH HOOK]
//        	'\u1D74', // [LATIN SMALL LETTER S WITH MIDDLE TILDE]
//        	'\u1D8A', // [LATIN SMALL LETTER S WITH PALATAL HOOK]
//        	'\u1E61', // [LATIN SMALL LETTER S WITH DOT ABOVE]
//        	'\u1E63', // [LATIN SMALL LETTER S WITH DOT BELOW]
//        	'\u1E65', // [LATIN SMALL LETTER S WITH ACUTE AND DOT ABOVE]
//        	'\u1E67', // [LATIN SMALL LETTER S WITH CARON AND DOT ABOVE]
//        	'\u1E69', // [LATIN SMALL LETTER S WITH DOT BELOW AND DOT ABOVE]
//        	'\u1E9C', // [LATIN SMALL LETTER LONG S WITH DIAGONAL STROKE]
//        	'\u1E9D', // [LATIN SMALL LETTER LONG S WITH HIGH STROKE]
//        	'\u24E2', // [CIRCLED LATIN SMALL LETTER S]
//        	'\uA784', // [LATIN CAPITAL LETTER INSULAR S]
//        	'\uFF53', // [FULLWIDTH LATIN SMALL LETTER S]
//        }
//      },
//      { "SS",
//        new() {
//          '\u1E9E', // [LATIN CAPITAL LETTER SHARP S]
//        }
//      },
//      { "(s)",
//        new() {
//          '\u24AE', // [PARENTHESIZED LATIN SMALL LETTER S]
//        }
//      },
//      { "ss",
//        new() {
//          '\u00DF', // [LATIN SMALL LETTER SHARP S]
//        }
//      },
//      { "st",
//        new() {
//          '\uFB06', // [LATIN SMALL LIGATURE ST]
//        }
//      },
//      { "T",
//        new() {
//          '\u0162', // [LATIN CAPITAL LETTER T WITH CEDILLA]
//        	'\u0164', // [LATIN CAPITAL LETTER T WITH CARON]
//        	'\u0166', // [LATIN CAPITAL LETTER T WITH STROKE]
//        	'\u01AC', // [LATIN CAPITAL LETTER T WITH HOOK]
//        	'\u01AE', // [LATIN CAPITAL LETTER T WITH RETROFLEX HOOK]
//        	'\u021A', // [LATIN CAPITAL LETTER T WITH COMMA BELOW]
//        	'\u023E', // [LATIN CAPITAL LETTER T WITH DIAGONAL STROKE]
//        	'\u1D1B', // [LATIN LETTER SMALL CAPITAL T]
//        	'\u1E6A', // [LATIN CAPITAL LETTER T WITH DOT ABOVE]
//        	'\u1E6C', // [LATIN CAPITAL LETTER T WITH DOT BELOW]
//        	'\u1E6E', // [LATIN CAPITAL LETTER T WITH LINE BELOW]
//        	'\u1E70', // [LATIN CAPITAL LETTER T WITH CIRCUMFLEX BELOW]
//        	'\u24C9', // [CIRCLED LATIN CAPITAL LETTER T]
//        	'\uA786', // [LATIN CAPITAL LETTER INSULAR T]
//        	'\uFF34', // [FULLWIDTH LATIN CAPITAL LETTER T]
//        }
//      },
//      { "t",
//        new() {
//          '\u0163', // [LATIN SMALL LETTER T WITH CEDILLA]
//        	'\u0165', // [LATIN SMALL LETTER T WITH CARON]
//        	'\u0167', // [LATIN SMALL LETTER T WITH STROKE]
//        	'\u01AB', // [LATIN SMALL LETTER T WITH PALATAL HOOK]
//        	'\u01AD', // [LATIN SMALL LETTER T WITH HOOK]
//        	'\u021B', // [LATIN SMALL LETTER T WITH COMMA BELOW]
//        	'\u0236', // [LATIN SMALL LETTER T WITH CURL]
//        	'\u0287', // [LATIN SMALL LETTER TURNED T]
//        	'\u0288', // [LATIN SMALL LETTER T WITH RETROFLEX HOOK]
//        	'\u1D75', // [LATIN SMALL LETTER T WITH MIDDLE TILDE]
//        	'\u1E6B', // [LATIN SMALL LETTER T WITH DOT ABOVE]
//        	'\u1E6D', // [LATIN SMALL LETTER T WITH DOT BELOW]
//        	'\u1E6F', // [LATIN SMALL LETTER T WITH LINE BELOW]
//        	'\u1E71', // [LATIN SMALL LETTER T WITH CIRCUMFLEX BELOW]
//        	'\u1E97', // [LATIN SMALL LETTER T WITH DIAERESIS]
//        	'\u24E3', // [CIRCLED LATIN SMALL LETTER T]
//        	'\u2C66', // [LATIN SMALL LETTER T WITH DIAGONAL STROKE]
//        	'\uFF54', // [FULLWIDTH LATIN SMALL LETTER T]
//        }
//      },
//      { "TH",
//        new() {
//          '\u00DE', // [LATIN CAPITAL LETTER THORN]
//        	'\uA766', // [LATIN CAPITAL LETTER THORN WITH STROKE THROUGH DESCENDER]
//        }
//      },
//      { "TZ",
//        new() {
//          '\uA728', // [LATIN CAPITAL LETTER TZ]
//        }
//      },
//      { "(t)",
//        new() {
//          '\u24AF', // [PARENTHESIZED LATIN SMALL LETTER T]
//        }
//      },
//      { "tc",
//        new() {
//          '\u02A8', // [LATIN SMALL LETTER TC DIGRAPH WITH CURL]
//        }
//      },
//      { "th",
//        new() {
//          '\u00FE', // [LATIN SMALL LETTER THORN]
//        	'\u1D7A', // [LATIN SMALL LETTER TH WITH STRIKETHROUGH]
//        	'\uA767', // [LATIN SMALL LETTER THORN WITH STROKE THROUGH DESCENDER]
//        }
//      },
//      { "ts",
//        new() {
//          '\u02A6', // [LATIN SMALL LETTER TS DIGRAPH]
//        }
//      },
//      { "tz",
//        new() {
//          '\uA729', // [LATIN SMALL LETTER TZ]
//        }
//      },
//      { "U",
//        new() {
//          '\u00D9', // [LATIN CAPITAL LETTER U WITH GRAVE]
//        	'\u00DA', // [LATIN CAPITAL LETTER U WITH ACUTE]
//        	'\u00DB', // [LATIN CAPITAL LETTER U WITH CIRCUMFLEX]
//        	'\u00DC', // [LATIN CAPITAL LETTER U WITH DIAERESIS]
//        	'\u0168', // [LATIN CAPITAL LETTER U WITH TILDE]
//        	'\u016A', // [LATIN CAPITAL LETTER U WITH MACRON]
//        	'\u016C', // [LATIN CAPITAL LETTER U WITH BREVE]
//        	'\u016E', // [LATIN CAPITAL LETTER U WITH RING ABOVE]
//        	'\u0170', // [LATIN CAPITAL LETTER U WITH DOUBLE ACUTE]
//        	'\u0172', // [LATIN CAPITAL LETTER U WITH OGONEK]
//        	'\u01AF', // [LATIN CAPITAL LETTER U WITH HORN]
//        	'\u01D3', // [LATIN CAPITAL LETTER U WITH CARON]
//        	'\u01D5', // [LATIN CAPITAL LETTER U WITH DIAERESIS AND MACRON]
//        	'\u01D7', // [LATIN CAPITAL LETTER U WITH DIAERESIS AND ACUTE]
//        	'\u01D9', // [LATIN CAPITAL LETTER U WITH DIAERESIS AND CARON]
//        	'\u01DB', // [LATIN CAPITAL LETTER U WITH DIAERESIS AND GRAVE]
//        	'\u0214', // [LATIN CAPITAL LETTER U WITH DOUBLE GRAVE]
//        	'\u0216', // [LATIN CAPITAL LETTER U WITH INVERTED BREVE]
//        	'\u0244', // [LATIN CAPITAL LETTER U BAR]
//        	'\u1D1C', // [LATIN LETTER SMALL CAPITAL U]
//        	'\u1D7E', // [LATIN SMALL CAPITAL LETTER U WITH STROKE]
//        	'\u1E72', // [LATIN CAPITAL LETTER U WITH DIAERESIS BELOW]
//        	'\u1E74', // [LATIN CAPITAL LETTER U WITH TILDE BELOW]
//        	'\u1E76', // [LATIN CAPITAL LETTER U WITH CIRCUMFLEX BELOW]
//        	'\u1E78', // [LATIN CAPITAL LETTER U WITH TILDE AND ACUTE]
//        	'\u1E7A', // [LATIN CAPITAL LETTER U WITH MACRON AND DIAERESIS]
//        	'\u1EE4', // [LATIN CAPITAL LETTER U WITH DOT BELOW]
//        	'\u1EE6', // [LATIN CAPITAL LETTER U WITH HOOK ABOVE]
//        	'\u1EE8', // [LATIN CAPITAL LETTER U WITH HORN AND ACUTE]
//        	'\u1EEA', // [LATIN CAPITAL LETTER U WITH HORN AND GRAVE]
//        	'\u1EEC', // [LATIN CAPITAL LETTER U WITH HORN AND HOOK ABOVE]
//        	'\u1EEE', // [LATIN CAPITAL LETTER U WITH HORN AND TILDE]
//        	'\u1EF0', // [LATIN CAPITAL LETTER U WITH HORN AND DOT BELOW]
//        	'\u24CA', // [CIRCLED LATIN CAPITAL LETTER U]
//        	'\uFF35', // [FULLWIDTH LATIN CAPITAL LETTER U]
//        }
//      },
//      { "u",
//        new() {
//          '\u00F9', // [LATIN SMALL LETTER U WITH GRAVE]
//        	'\u00FA', // [LATIN SMALL LETTER U WITH ACUTE]
//        	'\u00FB', // [LATIN SMALL LETTER U WITH CIRCUMFLEX]
//        	'\u00FC', // [LATIN SMALL LETTER U WITH DIAERESIS]
//        	'\u0169', // [LATIN SMALL LETTER U WITH TILDE]
//        	'\u016B', // [LATIN SMALL LETTER U WITH MACRON]
//        	'\u016D', // [LATIN SMALL LETTER U WITH BREVE]
//        	'\u016F', // [LATIN SMALL LETTER U WITH RING ABOVE]
//        	'\u0171', // [LATIN SMALL LETTER U WITH DOUBLE ACUTE]
//        	'\u0173', // [LATIN SMALL LETTER U WITH OGONEK]
//        	'\u01B0', // [LATIN SMALL LETTER U WITH HORN]
//        	'\u01D4', // [LATIN SMALL LETTER U WITH CARON]
//        	'\u01D6', // [LATIN SMALL LETTER U WITH DIAERESIS AND MACRON]
//        	'\u01D8', // [LATIN SMALL LETTER U WITH DIAERESIS AND ACUTE]
//        	'\u01DA', // [LATIN SMALL LETTER U WITH DIAERESIS AND CARON]
//        	'\u01DC', // [LATIN SMALL LETTER U WITH DIAERESIS AND GRAVE]
//        	'\u0215', // [LATIN SMALL LETTER U WITH DOUBLE GRAVE]
//        	'\u0217', // [LATIN SMALL LETTER U WITH INVERTED BREVE]
//        	'\u0289', // [LATIN SMALL LETTER U BAR]
//        	'\u1D64', // [LATIN SUBSCRIPT SMALL LETTER U]
//        	'\u1D99', // [LATIN SMALL LETTER U WITH RETROFLEX HOOK]
//        	'\u1E73', // [LATIN SMALL LETTER U WITH DIAERESIS BELOW]
//        	'\u1E75', // [LATIN SMALL LETTER U WITH TILDE BELOW]
//        	'\u1E77', // [LATIN SMALL LETTER U WITH CIRCUMFLEX BELOW]
//        	'\u1E79', // [LATIN SMALL LETTER U WITH TILDE AND ACUTE]
//        	'\u1E7B', // [LATIN SMALL LETTER U WITH MACRON AND DIAERESIS]
//        	'\u1EE5', // [LATIN SMALL LETTER U WITH DOT BELOW]
//        	'\u1EE7', // [LATIN SMALL LETTER U WITH HOOK ABOVE]
//        	'\u1EE9', // [LATIN SMALL LETTER U WITH HORN AND ACUTE]
//        	'\u1EEB', // [LATIN SMALL LETTER U WITH HORN AND GRAVE]
//        	'\u1EED', // [LATIN SMALL LETTER U WITH HORN AND HOOK ABOVE]
//        	'\u1EEF', // [LATIN SMALL LETTER U WITH HORN AND TILDE]
//        	'\u1EF1', // [LATIN SMALL LETTER U WITH HORN AND DOT BELOW]
//        	'\u24E4', // [CIRCLED LATIN SMALL LETTER U]
//        	'\uFF55', // [FULLWIDTH LATIN SMALL LETTER U]
//        }
//      },
//      { "(u)",
//        new() {
//          '\u24B0', // [PARENTHESIZED LATIN SMALL LETTER U]
//        }
//      },
//      { "ue",
//        new() {
//          '\u1D6B', // [LATIN SMALL LETTER UE]
//        }
//      },
//      { "V",
//        new() {
//          '\u01B2', // [LATIN CAPITAL LETTER V WITH HOOK]
//        	'\u0245', // [LATIN CAPITAL LETTER TURNED V]
//        	'\u1D20', // [LATIN LETTER SMALL CAPITAL V]
//        	'\u1E7C', // [LATIN CAPITAL LETTER V WITH TILDE]
//        	'\u1E7E', // [LATIN CAPITAL LETTER V WITH DOT BELOW]
//        	'\u1EFC', // [LATIN CAPITAL LETTER MIDDLE-WELSH V]
//        	'\u24CB', // [CIRCLED LATIN CAPITAL LETTER V]
//        	'\uA75E', // [LATIN CAPITAL LETTER V WITH DIAGONAL STROKE]
//        	'\uA768', // [LATIN CAPITAL LETTER VEND]
//        	'\uFF36', // [FULLWIDTH LATIN CAPITAL LETTER V]
//        }
//      },
//      { "v",
//        new() {
//          '\u028B', // [LATIN SMALL LETTER V WITH HOOK]
//        	'\u028C', // [LATIN SMALL LETTER TURNED V]
//        	'\u1D65', // [LATIN SUBSCRIPT SMALL LETTER V]
//        	'\u1D8C', // [LATIN SMALL LETTER V WITH PALATAL HOOK]
//        	'\u1E7D', // [LATIN SMALL LETTER V WITH TILDE]
//        	'\u1E7F', // [LATIN SMALL LETTER V WITH DOT BELOW]
//        	'\u24E5', // [CIRCLED LATIN SMALL LETTER V]
//        	'\u2C71', // [LATIN SMALL LETTER V WITH RIGHT HOOK]
//        	'\u2C74', // [LATIN SMALL LETTER V WITH CURL]
//        	'\uA75F', // [LATIN SMALL LETTER V WITH DIAGONAL STROKE]
//        	'\uFF56', // [FULLWIDTH LATIN SMALL LETTER V]
//        }
//      },
//      { "VY",
//        new() {
//          '\uA760', // [LATIN CAPITAL LETTER VY]
//        }
//      },
//      { "(v)",
//        new() {
//          '\u24B1', // [PARENTHESIZED LATIN SMALL LETTER V]
//        }
//      },
//      { "vy",
//        new() {
//          '\uA761', // [LATIN SMALL LETTER VY]
//        }
//      },
//      { "W",
//        new() {
//          '\u0174', // [LATIN CAPITAL LETTER W WITH CIRCUMFLEX]
//        	'\u01F7', // [LATIN CAPITAL LETTER WYNN]
//        	'\u1D21', // [LATIN LETTER SMALL CAPITAL W]
//        	'\u1E80', // [LATIN CAPITAL LETTER W WITH GRAVE]
//        	'\u1E82', // [LATIN CAPITAL LETTER W WITH ACUTE]
//        	'\u1E84', // [LATIN CAPITAL LETTER W WITH DIAERESIS]
//        	'\u1E86', // [LATIN CAPITAL LETTER W WITH DOT ABOVE]
//        	'\u1E88', // [LATIN CAPITAL LETTER W WITH DOT BELOW]
//        	'\u24CC', // [CIRCLED LATIN CAPITAL LETTER W]
//        	'\u2C72', // [LATIN CAPITAL LETTER W WITH HOOK]
//        	'\uFF37', // [FULLWIDTH LATIN CAPITAL LETTER W]
//        }
//      },
//      { "w",
//        new() {
//          '\u0175', // [LATIN SMALL LETTER W WITH CIRCUMFLEX]
//        	'\u01BF', // [LATIN LETTER WYNN]
//        	'\u028D', // [LATIN SMALL LETTER TURNED W]
//        	'\u1E81', // [LATIN SMALL LETTER W WITH GRAVE]
//        	'\u1E83', // [LATIN SMALL LETTER W WITH ACUTE]
//        	'\u1E85', // [LATIN SMALL LETTER W WITH DIAERESIS]
//        	'\u1E87', // [LATIN SMALL LETTER W WITH DOT ABOVE]
//        	'\u1E89', // [LATIN SMALL LETTER W WITH DOT BELOW]
//        	'\u1E98', // [LATIN SMALL LETTER W WITH RING ABOVE]
//        	'\u24E6', // [CIRCLED LATIN SMALL LETTER W]
//        	'\u2C73', // [LATIN SMALL LETTER W WITH HOOK]
//        	'\uFF57', // [FULLWIDTH LATIN SMALL LETTER W]
//        }
//      },
//      { "(w)",
//        new() {
//          '\u24B2', // [PARENTHESIZED LATIN SMALL LETTER W]
//        }
//      },
//      { "X",
//        new() {
//          '\u1E8A', // [LATIN CAPITAL LETTER X WITH DOT ABOVE]
//        	'\u1E8C', // [LATIN CAPITAL LETTER X WITH DIAERESIS]
//        	'\u24CD', // [CIRCLED LATIN CAPITAL LETTER X]
//        	'\uFF38', // [FULLWIDTH LATIN CAPITAL LETTER X]
//        }
//      },
//      { "x",
//        new() {
//          '\u1D8D', // [LATIN SMALL LETTER X WITH PALATAL HOOK]
//        	'\u1E8B', // [LATIN SMALL LETTER X WITH DOT ABOVE]
//        	'\u1E8D', // [LATIN SMALL LETTER X WITH DIAERESIS]
//        	'\u2093', // [LATIN SUBSCRIPT SMALL LETTER X]
//        	'\u24E7', // [CIRCLED LATIN SMALL LETTER X]
//        	'\uFF58', // [FULLWIDTH LATIN SMALL LETTER X]
//        }
//      },
//      { "(x)",
//        new() {
//          '\u24B3', // [PARENTHESIZED LATIN SMALL LETTER X]
//        }
//      },
//      { "Y",
//        new() {
//          '\u00DD', // [LATIN CAPITAL LETTER Y WITH ACUTE]
//        	'\u0176', // [LATIN CAPITAL LETTER Y WITH CIRCUMFLEX]
//        	'\u0178', // [LATIN CAPITAL LETTER Y WITH DIAERESIS]
//        	'\u01B3', // [LATIN CAPITAL LETTER Y WITH HOOK]
//        	'\u0232', // [LATIN CAPITAL LETTER Y WITH MACRON]
//        	'\u024E', // [LATIN CAPITAL LETTER Y WITH STROKE]
//        	'\u028F', // [LATIN LETTER SMALL CAPITAL Y]
//        	'\u1E8E', // [LATIN CAPITAL LETTER Y WITH DOT ABOVE]
//        	'\u1EF2', // [LATIN CAPITAL LETTER Y WITH GRAVE]
//        	'\u1EF4', // [LATIN CAPITAL LETTER Y WITH DOT BELOW]
//        	'\u1EF6', // [LATIN CAPITAL LETTER Y WITH HOOK ABOVE]
//        	'\u1EF8', // [LATIN CAPITAL LETTER Y WITH TILDE]
//        	'\u1EFE', // [LATIN CAPITAL LETTER Y WITH LOOP]
//        	'\u24CE', // [CIRCLED LATIN CAPITAL LETTER Y]
//        	'\uFF39', // [FULLWIDTH LATIN CAPITAL LETTER Y]
//        }
//      },
//      { "y",
//        new() {
//          '\u00FD', // [LATIN SMALL LETTER Y WITH ACUTE]
//        	'\u00FF', // [LATIN SMALL LETTER Y WITH DIAERESIS]
//        	'\u0177', // [LATIN SMALL LETTER Y WITH CIRCUMFLEX]
//        	'\u01B4', // [LATIN SMALL LETTER Y WITH HOOK]
//        	'\u0233', // [LATIN SMALL LETTER Y WITH MACRON]
//        	'\u024F', // [LATIN SMALL LETTER Y WITH STROKE]
//        	'\u028E', // [LATIN SMALL LETTER TURNED Y]
//        	'\u1E8F', // [LATIN SMALL LETTER Y WITH DOT ABOVE]
//        	'\u1E99', // [LATIN SMALL LETTER Y WITH RING ABOVE]
//        	'\u1EF3', // [LATIN SMALL LETTER Y WITH GRAVE]
//        	'\u1EF5', // [LATIN SMALL LETTER Y WITH DOT BELOW]
//        	'\u1EF7', // [LATIN SMALL LETTER Y WITH HOOK ABOVE]
//        	'\u1EF9', // [LATIN SMALL LETTER Y WITH TILDE]
//        	'\u1EFF', // [LATIN SMALL LETTER Y WITH LOOP]
//        	'\u24E8', // [CIRCLED LATIN SMALL LETTER Y]
//        	'\uFF59', // [FULLWIDTH LATIN SMALL LETTER Y]
//        }
//      },
//      { "(y)",
//        new() {
//          '\u24B4', // [PARENTHESIZED LATIN SMALL LETTER Y]
//        }
//      },
//      { "Z",
//        new() {
//          '\u0179', // [LATIN CAPITAL LETTER Z WITH ACUTE]
//        	'\u017B', // [LATIN CAPITAL LETTER Z WITH DOT ABOVE]
//        	'\u017D', // [LATIN CAPITAL LETTER Z WITH CARON]
//        	'\u01B5', // [LATIN CAPITAL LETTER Z WITH STROKE]
//        	'\u021C', // [LATIN CAPITAL LETTER YOGH]
//        	'\u0224', // [LATIN CAPITAL LETTER Z WITH HOOK]
//        	'\u1D22', // [LATIN LETTER SMALL CAPITAL Z]
//        	'\u1E90', // [LATIN CAPITAL LETTER Z WITH CIRCUMFLEX]
//        	'\u1E92', // [LATIN CAPITAL LETTER Z WITH DOT BELOW]
//        	'\u1E94', // [LATIN CAPITAL LETTER Z WITH LINE BELOW]
//        	'\u24CF', // [CIRCLED LATIN CAPITAL LETTER Z]
//        	'\u2C6B', // [LATIN CAPITAL LETTER Z WITH DESCENDER]
//        	'\uA762', // [LATIN CAPITAL LETTER VISIGOTHIC Z]
//        	'\uFF3A', // [FULLWIDTH LATIN CAPITAL LETTER Z]
//        }
//      },
//      { "z",
//        new() {
//          '\u017A', // [LATIN SMALL LETTER Z WITH ACUTE]
//        	'\u017C', // [LATIN SMALL LETTER Z WITH DOT ABOVE]
//        	'\u017E', // [LATIN SMALL LETTER Z WITH CARON]
//        	'\u01B6', // [LATIN SMALL LETTER Z WITH STROKE]
//        	'\u021D', // [LATIN SMALL LETTER YOGH]
//        	'\u0225', // [LATIN SMALL LETTER Z WITH HOOK]
//        	'\u0240', // [LATIN SMALL LETTER Z WITH SWASH TAIL]
//        	'\u0290', // [LATIN SMALL LETTER Z WITH RETROFLEX HOOK]
//        	'\u0291', // [LATIN SMALL LETTER Z WITH CURL]
//        	'\u1D76', // [LATIN SMALL LETTER Z WITH MIDDLE TILDE]
//        	'\u1D8E', // [LATIN SMALL LETTER Z WITH PALATAL HOOK]
//        	'\u1E91', // [LATIN SMALL LETTER Z WITH CIRCUMFLEX]
//        	'\u1E93', // [LATIN SMALL LETTER Z WITH DOT BELOW]
//        	'\u1E95', // [LATIN SMALL LETTER Z WITH LINE BELOW]
//        	'\u24E9', // [CIRCLED LATIN SMALL LETTER Z]
//        	'\u2C6C', // [LATIN SMALL LETTER Z WITH DESCENDER]
//        	'\uA763', // [LATIN SMALL LETTER VISIGOTHIC Z]
//        	'\uFF5A', // [FULLWIDTH LATIN SMALL LETTER Z]
//        }
//      },
//      { "(z)",
//        new() {
//          '\u24B5', // [PARENTHESIZED LATIN SMALL LETTER Z]
//        }
//      },
//      { "0",
//        new() {
//          '\u2070', // [SUPERSCRIPT ZERO]
//        	'\u2080', // [SUBSCRIPT ZERO]
//        	'\u24EA', // [CIRCLED DIGIT ZERO]
//        	'\u24FF', // [NEGATIVE CIRCLED DIGIT ZERO]
//        	'\uFF10', // [FULLWIDTH DIGIT ZERO]
//        }
//      },
//      { "1",
//        new() {
//          '\u00B9', // [SUPERSCRIPT ONE]
//        	'\u2081', // [SUBSCRIPT ONE]
//        	'\u2460', // [CIRCLED DIGIT ONE]
//        	'\u24F5', // [DOUBLE CIRCLED DIGIT ONE]
//        	'\u2776', // [DINGBAT NEGATIVE CIRCLED DIGIT ONE]
//        	'\u2780', // [DINGBAT CIRCLED SANS-SERIF DIGIT ONE]
//        	'\u278A', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT ONE]
//        	'\uFF11', // [FULLWIDTH DIGIT ONE]
//        }
//      },
//      { "1.",
//        new() {
//          '\u2488', // [DIGIT ONE FULL STOP]
//        }
//      },
//      { "(1)",
//        new() {
//          '\u2474', // [PARENTHESIZED DIGIT ONE]
//        }
//      },
//      { "2",
//        new() {
//          '\u00B2', // [SUPERSCRIPT TWO]
//        	'\u2082', // [SUBSCRIPT TWO]
//        	'\u2461', // [CIRCLED DIGIT TWO]
//        	'\u24F6', // [DOUBLE CIRCLED DIGIT TWO]
//        	'\u2777', // [DINGBAT NEGATIVE CIRCLED DIGIT TWO]
//        	'\u2781', // [DINGBAT CIRCLED SANS-SERIF DIGIT TWO]
//        	'\u278B', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT TWO]
//        	'\uFF12', // [FULLWIDTH DIGIT TWO]
//        }
//      },
//      { "2.",
//        new() {
//          '\u2489', // [DIGIT TWO FULL STOP]
//        }
//      },
//      { "(2)",
//        new() {
//          '\u2475', // [PARENTHESIZED DIGIT TWO]
//        }
//      },
//      { "3",
//        new() {
//          '\u00B3', // [SUPERSCRIPT THREE]
//        	'\u2083', // [SUBSCRIPT THREE]
//        	'\u2462', // [CIRCLED DIGIT THREE]
//        	'\u24F7', // [DOUBLE CIRCLED DIGIT THREE]
//        	'\u2778', // [DINGBAT NEGATIVE CIRCLED DIGIT THREE]
//        	'\u2782', // [DINGBAT CIRCLED SANS-SERIF DIGIT THREE]
//        	'\u278C', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT THREE]
//        	'\uFF13', // [FULLWIDTH DIGIT THREE]
//        }
//      },
//      { "3.",
//        new() {
//          '\u248A', // [DIGIT THREE FULL STOP]
//        }
//      },
//      { "(3)",
//        new() {
//          '\u2476', // [PARENTHESIZED DIGIT THREE]
//        }
//      },
//      { "4",
//        new() {
//          '\u2074', // [SUPERSCRIPT FOUR]
//        	'\u2084', // [SUBSCRIPT FOUR]
//        	'\u2463', // [CIRCLED DIGIT FOUR]
//        	'\u24F8', // [DOUBLE CIRCLED DIGIT FOUR]
//        	'\u2779', // [DINGBAT NEGATIVE CIRCLED DIGIT FOUR]
//        	'\u2783', // [DINGBAT CIRCLED SANS-SERIF DIGIT FOUR]
//        	'\u278D', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT FOUR]
//        	'\uFF14', // [FULLWIDTH DIGIT FOUR]
//        }
//      },
//      { "4.",
//        new() {
//          '\u248B', // [DIGIT FOUR FULL STOP]
//        }
//      },
//      { "(4)",
//        new() {
//          '\u2477', // [PARENTHESIZED DIGIT FOUR]
//        }
//      },
//      { "5",
//        new() {
//          '\u2075', // [SUPERSCRIPT FIVE]
//        	'\u2085', // [SUBSCRIPT FIVE]
//        	'\u2464', // [CIRCLED DIGIT FIVE]
//        	'\u24F9', // [DOUBLE CIRCLED DIGIT FIVE]
//        	'\u277A', // [DINGBAT NEGATIVE CIRCLED DIGIT FIVE]
//        	'\u2784', // [DINGBAT CIRCLED SANS-SERIF DIGIT FIVE]
//        	'\u278E', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT FIVE]
//        	'\uFF15', // [FULLWIDTH DIGIT FIVE]
//        }
//      },
//      { "5.",
//        new() {
//          '\u248C', // [DIGIT FIVE FULL STOP]
//        }
//      },
//      { "(5)",
//        new() {
//          '\u2478', // [PARENTHESIZED DIGIT FIVE]
//        }
//      },
//      { "6",
//        new() {
//          '\u2076', // [SUPERSCRIPT SIX]
//        	'\u2086', // [SUBSCRIPT SIX]
//        	'\u2465', // [CIRCLED DIGIT SIX]
//        	'\u24FA', // [DOUBLE CIRCLED DIGIT SIX]
//        	'\u277B', // [DINGBAT NEGATIVE CIRCLED DIGIT SIX]
//        	'\u2785', // [DINGBAT CIRCLED SANS-SERIF DIGIT SIX]
//        	'\u278F', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT SIX]
//        	'\uFF16', // [FULLWIDTH DIGIT SIX]
//        }
//      },
//      { "6.",
//        new() {
//          '\u248D', // [DIGIT SIX FULL STOP]
//        }
//      },
//      { "(6)",
//        new() {
//          '\u2479', // [PARENTHESIZED DIGIT SIX]
//        }
//      },
//      { "7",
//        new() {
//          '\u2077', // [SUPERSCRIPT SEVEN]
//        	'\u2087', // [SUBSCRIPT SEVEN]
//        	'\u2466', // [CIRCLED DIGIT SEVEN]
//        	'\u24FB', // [DOUBLE CIRCLED DIGIT SEVEN]
//        	'\u277C', // [DINGBAT NEGATIVE CIRCLED DIGIT SEVEN]
//        	'\u2786', // [DINGBAT CIRCLED SANS-SERIF DIGIT SEVEN]
//        	'\u2790', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT SEVEN]
//        	'\uFF17', // [FULLWIDTH DIGIT SEVEN]
//        }
//      },
//      { "7.",
//        new() {
//          '\u248E', // [DIGIT SEVEN FULL STOP]
//        }
//      },
//      { "(7)",
//        new() {
//          '\u247A', // [PARENTHESIZED DIGIT SEVEN]
//        }
//      },
//      { "8",
//        new() {
//          '\u2078', // [SUPERSCRIPT EIGHT]
//        	'\u2088', // [SUBSCRIPT EIGHT]
//        	'\u2467', // [CIRCLED DIGIT EIGHT]
//        	'\u24FC', // [DOUBLE CIRCLED DIGIT EIGHT]
//        	'\u277D', // [DINGBAT NEGATIVE CIRCLED DIGIT EIGHT]
//        	'\u2787', // [DINGBAT CIRCLED SANS-SERIF DIGIT EIGHT]
//        	'\u2791', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT EIGHT]
//        	'\uFF18', // [FULLWIDTH DIGIT EIGHT]
//        }
//      },
//      { "8.",
//        new() {
//          '\u248F', // [DIGIT EIGHT FULL STOP]
//        }
//      },
//      { "(8)",
//        new() {
//          '\u247B', // [PARENTHESIZED DIGIT EIGHT]
//        }
//      },
//      { "9",
//        new() {
//          '\u2079', // [SUPERSCRIPT NINE]
//        	'\u2089', // [SUBSCRIPT NINE]
//        	'\u2468', // [CIRCLED DIGIT NINE]
//        	'\u24FD', // [DOUBLE CIRCLED DIGIT NINE]
//        	'\u277E', // [DINGBAT NEGATIVE CIRCLED DIGIT NINE]
//        	'\u2788', // [DINGBAT CIRCLED SANS-SERIF DIGIT NINE]
//        	'\u2792', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT NINE]
//        	'\uFF19', // [FULLWIDTH DIGIT NINE]
//        }
//      },
//      { "9.",
//        new() {
//          '\u2490', // [DIGIT NINE FULL STOP]
//        }
//      },
//      { "(9)",
//        new() {
//          '\u247C', // [PARENTHESIZED DIGIT NINE]
//        }
//      },
//      { "10",
//        new() {
//          '\u2469', // [CIRCLED NUMBER TEN]
//        	'\u24FE', // [DOUBLE CIRCLED NUMBER TEN]
//        	'\u277F', // [DINGBAT NEGATIVE CIRCLED NUMBER TEN]
//        	'\u2789', // [DINGBAT CIRCLED SANS-SERIF NUMBER TEN]
//        	'\u2793', // [DINGBAT NEGATIVE CIRCLED SANS-SERIF NUMBER TEN]
//        }
//      },
//      { "10.",
//        new() {
//          '\u2491', // [NUMBER TEN FULL STOP]
//        }
//      },
//      { "(10)",
//        new() {
//          '\u247D', // [PARENTHESIZED NUMBER TEN]
//        }
//      },
//      { "11",
//        new() {
//          '\u246A', // [CIRCLED NUMBER ELEVEN]
//        	'\u24EB', // [NEGATIVE CIRCLED NUMBER ELEVEN]
//        }
//      },
//      { "11.",
//        new() {
//          '\u2492', // [NUMBER ELEVEN FULL STOP]
//        }
//      },
//      { "(11)",
//        new() {
//          '\u247E', // [PARENTHESIZED NUMBER ELEVEN]
//        }
//      },
//      { "12",
//        new() {
//          '\u246B', // [CIRCLED NUMBER TWELVE]
//        	'\u24EC', // [NEGATIVE CIRCLED NUMBER TWELVE]
//        }
//      },
//      { "12.",
//        new() {
//          '\u2493', // [NUMBER TWELVE FULL STOP]
//        }
//      },
//      { "(12)",
//        new() {
//          '\u247F', // [PARENTHESIZED NUMBER TWELVE]
//        }
//      },
//      { "13",
//        new() {
//          '\u246C', // [CIRCLED NUMBER THIRTEEN]
//        	'\u24ED', // [NEGATIVE CIRCLED NUMBER THIRTEEN]
//        }
//      },
//      { "13.",
//        new() {
//          '\u2494', // [NUMBER THIRTEEN FULL STOP]
//        }
//      },
//      { "(13)",
//        new() {
//          '\u2480', // [PARENTHESIZED NUMBER THIRTEEN]
//        }
//      },
//      { "14",
//        new() {
//          '\u246D', // [CIRCLED NUMBER FOURTEEN]
//        	'\u24EE', // [NEGATIVE CIRCLED NUMBER FOURTEEN]
//        }
//      },
//      { "14.",
//        new() {
//          '\u2495', // [NUMBER FOURTEEN FULL STOP]
//        }
//      },
//      { "(14)",
//        new() {
//          '\u2481', // [PARENTHESIZED NUMBER FOURTEEN]
//        }
//      },
//      { "15",
//        new() {
//          '\u246E', // [CIRCLED NUMBER FIFTEEN]
//        	'\u24EF', // [NEGATIVE CIRCLED NUMBER FIFTEEN]
//        }
//      },
//      { "15.",
//        new() {
//          '\u2496', // [NUMBER FIFTEEN FULL STOP]
//        }
//      },
//      { "(15)",
//        new() {
//          '\u2482', // [PARENTHESIZED NUMBER FIFTEEN]
//        }
//      },
//      { "16",
//        new() {
//          '\u246F', // [CIRCLED NUMBER SIXTEEN]
//        	'\u24F0', // [NEGATIVE CIRCLED NUMBER SIXTEEN]
//        }
//      },
//      { "16.",
//        new() {
//          '\u2497', // [NUMBER SIXTEEN FULL STOP]
//        }
//      },
//      { "(16)",
//        new() {
//          '\u2483', // [PARENTHESIZED NUMBER SIXTEEN]
//        }
//      },
//      { "17",
//        new() {
//          '\u2470', // [CIRCLED NUMBER SEVENTEEN]
//        	'\u24F1', // [NEGATIVE CIRCLED NUMBER SEVENTEEN]
//        }
//      },
//      { "17.",
//        new() {
//          '\u2498', // [NUMBER SEVENTEEN FULL STOP]
//        }
//      },
//      { "(17)",
//        new() {
//          '\u2484', // [PARENTHESIZED NUMBER SEVENTEEN]
//        }
//      },
//      { "18",
//        new() {
//          '\u2471', // [CIRCLED NUMBER EIGHTEEN]
//        	'\u24F2', // [NEGATIVE CIRCLED NUMBER EIGHTEEN]
//        }
//      },
//      { "18.",
//        new() {
//          '\u2499', // [NUMBER EIGHTEEN FULL STOP]
//        }
//      },
//      { "(18)",
//        new() {
//          '\u2485', // [PARENTHESIZED NUMBER EIGHTEEN]
//        }
//      },
//      { "19",
//        new() {
//          '\u2472', // [CIRCLED NUMBER NINETEEN]
//        	'\u24F3', // [NEGATIVE CIRCLED NUMBER NINETEEN]
//        }
//      },
//      { "19.",
//        new() {
//          '\u249A', // [NUMBER NINETEEN FULL STOP]
//        }
//      },
//      { "(19)",
//        new() {
//          '\u2486', // [PARENTHESIZED NUMBER NINETEEN]
//        }
//      },
//      { "20",
//        new() {
//          '\u2473', // [CIRCLED NUMBER TWENTY]
//        	'\u24F4', // [NEGATIVE CIRCLED NUMBER TWENTY]
//        }
//      },
//      { "20.",
//        new() {
//          '\u249B', // [NUMBER TWENTY FULL STOP]
//        }
//      },
//      { "(20)",
//        new() {
//          '\u2487', // [PARENTHESIZED NUMBER TWENTY]
//        }
//      },
//      { "\"",
//        new() {
//          '\u00AB', // [LEFT-POINTING DOUBLE ANGLE QUOTATION MARK]
//        	'\u00BB', // [RIGHT-POINTING DOUBLE ANGLE QUOTATION MARK]
//        	'\u201C', // [LEFT DOUBLE QUOTATION MARK]
//        	'\u201D', // [RIGHT DOUBLE QUOTATION MARK]
//        	'\u201E', // [DOUBLE LOW-9 QUOTATION MARK]
//        	'\u2033', // [DOUBLE PRIME]
//        	'\u2036', // [REVERSED DOUBLE PRIME]
//        	'\u275D', // [HEAVY DOUBLE TURNED COMMA QUOTATION MARK ORNAMENT]
//        	'\u275E', // [HEAVY DOUBLE COMMA QUOTATION MARK ORNAMENT]
//        	'\u276E', // [HEAVY LEFT-POINTING ANGLE QUOTATION MARK ORNAMENT]
//        	'\u276F', // [HEAVY RIGHT-POINTING ANGLE QUOTATION MARK ORNAMENT]
//        	'\uFF02', // [FULLWIDTH QUOTATION MARK]
//        }
//      },
//      { "'",
//        new() {
//          '\u2018', // [LEFT SINGLE QUOTATION MARK]
//        	'\u2019', // [RIGHT SINGLE QUOTATION MARK]
//        	'\u201A', // [SINGLE LOW-9 QUOTATION MARK]
//        	'\u201B', // [SINGLE HIGH-REVERSED-9 QUOTATION MARK]
//        	'\u2032', // [PRIME]
//        	'\u2035', // [REVERSED PRIME]
//        	'\u2039', // [SINGLE LEFT-POINTING ANGLE QUOTATION MARK]
//        	'\u203A', // [SINGLE RIGHT-POINTING ANGLE QUOTATION MARK]
//        	'\u275B', // [HEAVY SINGLE TURNED COMMA QUOTATION MARK ORNAMENT]
//        	'\u275C', // [HEAVY SINGLE COMMA QUOTATION MARK ORNAMENT]
//        	'\uFF07', // [FULLWIDTH APOSTROPHE]
//        }
//      },
//      { "-",
//        new() {
//          '\u2010', // [HYPHEN]
//        	'\u2011', // [NON-BREAKING HYPHEN]
//        	'\u2012', // [FIGURE DASH]
//        	'\u2013', // [EN DASH]
//        	'\u2014', // [EM DASH]
//        	'\u207B', // [SUPERSCRIPT MINUS]
//        	'\u208B', // [SUBSCRIPT MINUS]
//        	'\uFF0D', // [FULLWIDTH HYPHEN-MINUS]
//        }
//      },
//      { "[",
//        new() {
//          '\u2045', // [LEFT SQUARE BRACKET WITH QUILL]
//        	'\u2772', // [LIGHT LEFT TORTOISE SHELL BRACKET ORNAMENT]
//        	'\uFF3B', // [FULLWIDTH LEFT SQUARE BRACKET]
//        }
//      },
//      { "]",
//        new() {
//          '\u2046', // [RIGHT SQUARE BRACKET WITH QUILL]
//        	'\u2773', // [LIGHT RIGHT TORTOISE SHELL BRACKET ORNAMENT]
//        	'\uFF3D', // [FULLWIDTH RIGHT SQUARE BRACKET]
//        }
//      },
//      { "(",
//        new() {
//          '\u207D', // [SUPERSCRIPT LEFT PARENTHESIS]
//        	'\u208D', // [SUBSCRIPT LEFT PARENTHESIS]
//        	'\u2768', // [MEDIUM LEFT PARENTHESIS ORNAMENT]
//        	'\u276A', // [MEDIUM FLATTENED LEFT PARENTHESIS ORNAMENT]
//        	'\uFF08', // [FULLWIDTH LEFT PARENTHESIS]
//        }
//      },
//      { "((",
//        new() {
//          '\u2E28', // [LEFT DOUBLE PARENTHESIS]
//        }
//      },
//      { ")",
//        new() {
//          '\u207E', // [SUPERSCRIPT RIGHT PARENTHESIS]
//        	'\u208E', // [SUBSCRIPT RIGHT PARENTHESIS]
//        	'\u2769', // [MEDIUM RIGHT PARENTHESIS ORNAMENT]
//        	'\u276B', // [MEDIUM FLATTENED RIGHT PARENTHESIS ORNAMENT]
//        	'\uFF09', // [FULLWIDTH RIGHT PARENTHESIS]
//        }
//      },
//      { "))",
//        new() {
//          '\u2E29', // [RIGHT DOUBLE PARENTHESIS]
//        }
//      },
//      { "<",
//        new() {
//          '\u276C', // [MEDIUM LEFT-POINTING ANGLE BRACKET ORNAMENT]
//        	'\u2770', // [HEAVY LEFT-POINTING ANGLE BRACKET ORNAMENT]
//        	'\uFF1C', // [FULLWIDTH LESS-THAN SIGN]
//        }
//      },
//      { ">",
//        new() {
//          '\u276D', // [MEDIUM RIGHT-POINTING ANGLE BRACKET ORNAMENT]
//        	'\u2771', // [HEAVY RIGHT-POINTING ANGLE BRACKET ORNAMENT]
//        	'\uFF1E', // [FULLWIDTH GREATER-THAN SIGN]
//        }
//      },
//      { "{",
//        new() {
//          '\u2774', // [MEDIUM LEFT CURLY BRACKET ORNAMENT]
//        	'\uFF5B', // [FULLWIDTH LEFT CURLY BRACKET]
//        }
//      },
//      { "}",
//        new() {
//          '\u2775', // [MEDIUM RIGHT CURLY BRACKET ORNAMENT]
//        	'\uFF5D', // [FULLWIDTH RIGHT CURLY BRACKET]
//        }
//      },
//      { "+",
//        new() {
//          '\u207A', // [SUPERSCRIPT PLUS SIGN]
//        	'\u208A', // [SUBSCRIPT PLUS SIGN]
//        	'\uFF0B', // [FULLWIDTH PLUS SIGN]
//        }
//      },
//      { "=",
//        new() {
//          '\u207C', // [SUPERSCRIPT EQUALS SIGN]
//        	'\u208C', // [SUBSCRIPT EQUALS SIGN]
//        	'\uFF1D', // [FULLWIDTH EQUALS SIGN]
//        }
//      },
//      { "!",
//        new() {
//          '\uFF01', // [FULLWIDTH EXCLAMATION MARK]
//        }
//      },
//      { "!!",
//        new() {
//          '\u203C', // [DOUBLE EXCLAMATION MARK]
//        }
//      },
//      { "!?",
//        new() {
//          '\u2049', // [EXCLAMATION QUESTION MARK]
//        }
//      },
//      { "#",
//        new() {
//          '\uFF03', // [FULLWIDTH NUMBER SIGN]
//        }
//      },
//      { "$",
//        new() {
//          '\uFF04', // [FULLWIDTH DOLLAR SIGN]
//        }
//      },
//      { "%",
//        new() {
//          '\u2052', // [COMMERCIAL MINUS SIGN]
//        	'\uFF05', // [FULLWIDTH PERCENT SIGN]
//        }
//      },
//      { "&",
//        new() {
//          '\uFF06', // [FULLWIDTH AMPERSAND]
//        }
//      },
//      { "*",
//        new() {
//          '\u204E', // [LOW ASTERISK]
//        	'\uFF0A', // [FULLWIDTH ASTERISK]
//        }
//      },
//      { ",",
//        new() {
//          '\uFF0C', // [FULLWIDTH COMMA]
//        }
//      },
//      { ".",
//        new() {
//          '\uFF0E', // [FULLWIDTH FULL STOP]
//        }
//      },
//      { "/",
//        new() {
//          '\u2044', // [FRACTION SLASH]
//        	'\uFF0F', // [FULLWIDTH SOLIDUS]
//        }
//      },
//      { ",",
//        new() {
//          '\uFF1A', // [FULLWIDTH COLON]
//        }
//      },
//      { ";",
//        new() {
//          '\u204F', // [REVERSED SEMICOLON]
//        	'\uFF1B', // [FULLWIDTH SEMICOLON]
//        }
//      },
//      { "?",
//        new() {
//          '\uFF1F', // [FULLWIDTH QUESTION MARK]
//        }
//      },
//      { "??",
//        new() {
//          '\u2047', // [DOUBLE QUESTION MARK]
//        }
//      },
//      { "?!",
//        new() {
//          '\u2048', // [QUESTION EXCLAMATION MARK]
//        }
//      },
//      { "@",
//        new() {
//          '\uFF20', // [FULLWIDTH COMMERCIAL AT]
//        }
//      },
//      { "\\",
//        new() {
//          '\uFF3C', // [FULLWIDTH REVERSE SOLIDUS]
//        }
//      },
//      { "^",
//        new() {
//          '\u2038', // [CARET]
//        	'\uFF3E', // [FULLWIDTH CIRCUMFLEX ACCENT]
//        }
//      },
//      { "_",
//        new() {
//          '\uFF3F', // [FULLWIDTH LOW LINE]
//        }
//      },
//      { "~",
//        new() {
//          '\u2053', // [SWUNG DASH]
//        	'\uFF5E', // [FULLWIDTH TILDE]
//        }
//      },
//    };
//  }
//}
