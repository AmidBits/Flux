namespace Flux
{
  public enum UnicodeCodepoint
  {
    /// <summary>ASCII control character. The file separator FS is an interesting control code, as it gives us insight in the way that computer technology was organized in the sixties. We are now used to random access media like RAM and magnetic disks, but when the ASCII standard was defined, most data was serial. I am not only talking about serial communications, but also about serial storage like punch cards, paper tape and magnetic tapes. In such a situation it is clearly efficient to have a single control code to signal the separation of two files. The FS was defined for this purpose.</summary>
    FileSeparator = 0x001C,
    /// <summary>ASCII control character. Data storage was one of the main reasons for some control codes to get in the ASCII definition. Databases are most of the time setup with tables, containing records. All records in one table have the same type, but records of different tables can be different. The group separator GS is defined to separate tables in a serial data storage system. Note that the word table wasn’t used at that moment and the ASCII people called it a group.</summary>
    GroupSeparator = 0x001D,
    /// <summary>ASCII control character. Within a group (or table) the records are separated with RS or record separator.</summary>
    RecordSeparator = 0x001E,
    /// <summary>ASCII control character. The smallest data items to be stored in a database are called units in the ASCII definition. We would call them field now. The unit separator separates these fields in a serial data storage environment. Most current database implementations require that fields of most types have a fixed length. Enough space in the record is allocated to store the largest possible member of each field, even if this is not necessary in most cases. This costs a large amount of space in many situations. The US control code allows all fields to have a variable length. If data storage space is limited—as in the sixties—this is a good way to preserve valuable space. On the other hand is serial storage far less efficient than the table driven RAM and disk implementations of modern times. I can’t imagine a situation where modern SQL databases are run with the data stored on paper tape or magnetic reels.</summary>
    UnitSeparator = 0x001F,

    DegreeSign = 0x00B0,
    MicroSign = 0x00B5,
    QuotationMark = 0x0022,
    Apostrophe = 0x0027,

    Prime = 0x2032,
    DoublePrime = 0x2033,

    OhmSign = 0x2126,

    SymbolForFileSeparator = 0x241C,
    SymbolForGroupSeparator = 0x241D,
    SymbolForRecordSeparator = 0x241E,
    SymbolForUnitSeparator = 0x241F,

    /// <summary>Square Radian.</summary>
    SquareRad = 0x33AD,
    /// <summary>Square Bequerel.</summary>
    SquareBq = 0x33C3,
    /// <summary>Square Micro Farad.</summary>
    SquarePf = 0x338A,
    /// <summary>Square Nano Farad.</summary>
    SquareNf = 0x338B,
    /// <summary>Square Pico Farad.</summary>
    SquareMuF = 0x338C,
    /// <summary>Square K Ohm.</summary>
    SquareKOhm = 0x33C0,
    /// <summary>Square M Ohm.</summary>
    SquareMOhm = 0x33C1,
    /// <summary>Square Katal.</summary>
    SquareKt = 0x33CF,
  }
}
