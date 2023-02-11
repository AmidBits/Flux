using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;
using Flux;
using Flux.ApproximateEquality;
using Flux.Geometry;
using Flux.Interpolation;
using Flux.Quantities;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public class Program
  {
    private static void TimedMain(string[] args)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      foreach (var g in "egg".EnumerateTextElements())
        System.Console.WriteLine(g);

      return;

      foreach (var rune in System.Text.Unicode.UnicodeRanges.IpaExtensions.GetRunes())
        System.Console.WriteLine($"'{rune.ToUnicodeCsEscapeSequenceString(Flux.Unicode.CsEscapeSequenceFormat.UTF16)}' => new string[] {{ }},");
      return;

      var eng = new string[] { "Liam", "Noah", "Oliver", "Elijah", "James", "William", "Benjamin", "Lucas", "Henry", "Theodore", "Jack", "Levi", "Alexander", "Jackson", "Mateo", "Daniel", "Michael", "Mason", "Sebastian", "Ethan", "Logan", "Owen", "Samuel", "Jacob", "Asher", "Aiden", "John", "Joseph", "Wyatt", "David", "Leo", "Luke", "Julian", "Hudson", "Grayson", "Matthew", "Ezra", "Gabriel", "Carter", "Isaac", "Jayden", "Luca", "Anthony", "Dylan", "Lincoln", "Thomas", "Maverick", "Elias", "Josiah", "Charles", "Caleb", "Christopher", "Ezekiel", "Miles", "Jaxon", "Isaiah", "Andrew", "Joshua", "Nathan", "Nolan", "Adrian", "Cameron", "Santiago", "Eli", "Aaron", "Ryan", "Angel", "Cooper", "Waylon", "Easton", "Kai", "Christian", "Landon", "Colton", "Roman", "Axel", "Brooks", "Jonathan", "Robert", "Jameson", "Ian", "Everett", "Greyson", "Wesley", "Jeremiah", "Hunter", "Leonardo", "Jordan", "Jose", "Bennett", "Silas", "Nicholas", "Parker", "Beau", "Weston", "Austin", "Connor", "Carson", "Dominic", "Xavier", "Jaxson", "Jace", "Emmett", "Adam", "Declan", "Rowan", "Micah", "Kayden", "Gael", "River", "Ryder", "Kingston", "Damian", "Sawyer", "Luka", "Evan", "Vincent", "Legend", "Myles", "Harrison", "August", "Bryson", "Amir", "Giovanni", "Chase", "Diego", "Milo", "Jasper", "Walker", "Jason", "Brayden", "Cole", "Nathaniel", "George", "Lorenzo", "Zion", "Luis", "Archer", "Enzo", "Jonah", "Thiago", "Theo", "Ayden", "Zachary", "Calvin", "Braxton", "Ashton", "Rhett", "Atlas", "Jude", "Bentley", "Carlos", "Ryker", "Adriel", "Arthur", "Ace", "Tyler", "Jayce", "Max", "Elliot", "Graham", "Kaiden", "Maxwell", "Juan", "Dean", "Matteo", "Malachi", "Ivan", "Elliott", "Jesus", "Emiliano", "Messiah", "Gavin", "Maddox", "Camden", "Hayden", "Leon", "Antonio", "Justin", "Tucker", "Brandon", "Kevin", "Judah", "Finn", "King", "Brody", "Xander", "Nicolas", "Charlie", "Arlo", "Emmanuel", "Barrett", "Felix", "Alex", "Miguel", "Abel", "Alan", "Beckett", "Amari", "Karter", "Timothy", "Abraham", "Jesse", "Zayden", "Blake", "Alejandro", "Dawson", "Tristan", "Victor", "Avery", "Joel", "Grant", "Eric", "Patrick", "Peter", "Richard", "Edward", "Andres", "Emilio", "Colt", "Knox", "Beckham", "Adonis", "Kyrie", "Matias", "Oscar", "Lukas", "Marcus", "Hayes", "Caden", "Remington", "Griffin", "Nash", "Israel", "Steven", "Holden", "Rafael", "Zane", "Jeremy", "Kash", "Preston", "Kyler", "Jax", "Jett", "Kaleb", "Riley", "Simon", "Phoenix", "Javier", "Bryce", "Louis", "Mark", "Cash", "Lennox", "Paxton", "Malakai", "Paul", "Kenneth", "Nico", "Kaden", "Lane", "Kairo", "Maximus", "Omar", "Finley", "Atticus", "Crew", "Brantley", "Colin", "Dallas", "Walter", "Brady", "Callum", "Ronan", "Hendrix", "Jorge", "Tobias", "Clayton", "Emerson", "Damien", "Zayn", "Malcolm", "Kayson", "Bodhi", "Bryan", "Aidan", "Cohen", "Brian", "Cayden", "Andre", "Niko", "Maximiliano", "Zander", "Khalil", "Rory", "Francisco", "Cruz", "Kobe", "Reid", "Daxton", "Derek", "Martin", "Jensen", "Karson", "Tate", "Muhammad", "Jaden", "Joaquin", "Josue", "Gideon", "Dante", "Cody", "Bradley", "Orion", "Spencer", "Angelo", "Erick", "Jaylen", "Julius", "Manuel", "Ellis", "Colson", "Cairo", "Gunner", "Wade", "Chance", "Odin", "Anderson", "Kane", "Raymond", "Cristian", "Aziel", "Prince", "Ezequiel", "Jake", "Otto", "Eduardo", "Rylan", "Ali", "Cade", "Stephen", "Ari", "Kameron", "Dakota", "Warren", "Ricardo", "Killian", "Mario", "Romeo", "Cyrus", "Ismael", "Russell", "Tyson", "Edwin", "Desmond", "Nasir", "Remy", "Tanner", "Fernando", "Hector", "Titus", "Lawson", "Sean", "Kyle", "Elian", "Corbin", "Bowen", "Wilder", "Armani", "Royal", "Stetson", "Briggs", "Sullivan", "Leonel", "Callan", "Finnegan", "Jay", "Zayne", "Marshall", "Kade", "Travis", "Sterling", "Raiden", "Sergio", "Tatum", "Cesar", "Zyaire", "Milan", "Devin", "Gianni", "Kamari", "Royce", "Malik", "Jared", "Franklin", "Clark", "Noel", "Marco", "Archie", "Apollo", "Pablo", "Garrett", "Oakley", "Memphis", "Quinn", "Onyx", "Alijah", "Baylor", "Edgar", "Nehemiah", "Winston", "Major", "Rhys", "Forrest", "Jaiden", "Reed", "Santino", "Troy", "Caiden", "Harvey", "Collin", "Solomon", "Donovan", "Damon", "Jeffrey", "Kason", "Sage", "Grady", "Kendrick", "Leland", "Luciano", "Pedro", "Hank", "Hugo", "Esteban", "Johnny", "Kashton", "Ronin", "Ford", "Mathias", "Porter", "Erik", "Johnathan", "Frank", "Tripp", "Casey", "Fabian", "Leonidas", "Baker", "Matthias", "Philip", "Jayceon", "Kian", "Saint", "Ibrahim", "Jaxton", "Augustus", "Callen", "Trevor", "Ruben", "Adan", "Conor", "Dax", "Braylen", "Kaison", "Francis", "Kyson", "Andy", "Lucca", "Mack", "Peyton", "Alexis", "Deacon", "Kasen", "Kamden", "Frederick", "Princeton", "Braylon", "Wells", "Nikolai", "Iker", "Bo", "Dominick", "Moshe", "Cassius", "Gregory", "Lewis", "Kieran", "Isaias", "Seth", "Marcos", "Omari", "Shane", "Keegan", "Jase", "Asa", "Sonny", "Uriel", "Pierce", "Jasiah", "Eden", "Rocco", "Banks", "Cannon", "Denver", "Zaiden", "Roberto", "Shawn", "Drew", "Emanuel", "Kolton", "Ayaan", "Ares", "Conner", "Jalen", "Alonzo", "Enrique", "Dalton", "Moses", "Koda", "Bodie", "Jamison", "Phillip", "Zaire", "Jonas", "Kylo", "Moises", "Shepherd", "Allen", "Kenzo", "Mohamed", "Keanu", "Dexter", "Conrad", "Bruce", "Sylas", "Soren", "Raphael", "Rowen", "Gunnar", "Sutton", "Quentin", "Jaziel", "Emmitt", "Makai", "Koa", "Maximilian", "Brixton", "Dariel", "Zachariah", "Roy", "Armando", "Corey", "Saul", "Izaiah", "Danny", "Davis", "Ridge", "Yusuf", "Ariel", "Valentino", "Jayson", "Ronald", "Albert", "Gerardo", "Ryland", "Dorian", "Drake", "Gage", "Rodrigo", "Hezekiah", "Kylan", "Boone", "Ledger", "Santana", "Jamari", "Jamir", "Lawrence", "Reece", "Kaysen", "Shiloh", "Arjun", "Marcelo", "Abram", "Benson", "Huxley", "Nikolas", "Zain", "Kohen", "Samson", "Miller", "Donald", "Finnley", "Kannon", "Lucian", "Watson", "Keith", "Westin", "Tadeo", "Sincere", "Boston", "Axton", "Amos", "Chandler", "Leandro", "Raul", "Scott", "Reign", "Alessandro", "Camilo", "Derrick", "Morgan", "Julio", "Clay", "Edison", "Jaime", "Augustine", "Julien", "Zeke", "Marvin", "Bellamy", "Landen", "Dustin", "Jamie", "Krew", "Kyree", "Colter", "Johan", "Houston", "Layton", "Quincy", "Case", "Atreus", "Cayson", "Aarav", "Darius", "Harlan", "Justice", "Abdiel", "Layne", "Raylan", "Arturo", "Taylor", "Anakin", "Ander", "Hamza", "Otis", "Azariah", "Leonard", "Colby", "Duke", "Flynn", "Trey", "Gustavo", "Fletcher", "Issac", "Sam", "Trenton", "Callahan", "Chris", "Mohammad", "Rayan", "Lionel", "Bruno", "Jaxxon", "Zaid", "Brycen", "Roland", "Dillon", "Lennon", "Ambrose", "Rio", "Mac", "Ahmed", "Samir", "Yosef", "Tru", "Creed", "Tony", "Alden", "Aden", "Alec", "Carmelo", "Dario", "Marcel", "Roger", "Ty", "Ahmad", "Emir", "Landyn", "Skyler", "Mohammed", "Dennis", "Kareem", "Nixon", "Rex", "Uriah", "Lee", "Louie", "Rayden", "Reese", "Alberto", "Cason", "Quinton", "Kingsley", "Chaim", "Alfredo", "Mauricio", "Caspian", "Legacy", "Ocean", "Ozzy", "Briar", "Wilson", "Forest", "Grey", "Joziah", "Salem", "Neil", "Remi", "Bridger", "Harry", "Jefferson", "Lachlan", "Nelson", "Casen", "Salvador", "Magnus", "Tommy", "Marcellus", "Maximo", "Jerry", "Clyde", "Aron", "Keaton", "Eliam", "Lian", "Trace", "Douglas", "Junior", "Titan", "Cullen", "Cillian", "Musa", "Mylo", "Hugh", "Tomas", "Vincenzo", "Westley", "Langston", "Byron", "Kiaan", "Loyal", "Orlando", "Kyro", "Amias", "Amiri", "Jimmy", "Vicente", "Khari", "Brendan", "Rey", "Ben", "Emery", "Zyair", "Bjorn", "Evander", "Ramon", "Alvin", "Ricky", "Jagger", "Brock", "Dakari", "Eddie", "Blaze", "Gatlin", "Alonso", "Curtis", "Kylian", "Nathanael", "Devon", "Wayne", "Zakai", "Mathew", "Rome", "Riggs", "Aryan", "Avi", "Hassan", "Lochlan", "Stanley", "Dash", "Kaiser", "Benicio", "Bryant", "Talon", "Rohan", "Wesson", "Joe", "Noe", "Melvin", "Vihaan", "Zayd", "Darren", "Enoch", "Mitchell", "Jedidiah", "Brodie", "Castiel", "Ira", "Lance", "Guillermo", "Thatcher", "Ermias", "Misael", "Jakari", "Emory", "Mccoy", "Rudy", "Thaddeus", "Valentin", "Yehuda", "Bode", "Madden", "Kase", "Bear", "Boden", "Jiraiya", "Maurice", "Alvaro", "Ameer", "Demetrius", "Eliseo", "Kabir", "Kellan", "Allan", "Azrael", "Calum", "Niklaus", "Ray", "Damari", "Elio", "Jon", "Leighton", "Axl", "Dane", "Eithan", "Eugene", "Kenji", "Jakob", "Colten", "Eliel", "Nova", "Santos", "Zahir", "Idris", "Ishaan", "Kole", "Korbin", "Seven", "Alaric", "Kellen", "Bronson", "Franco", "Wes", "Larry", "Mekhi", "Jamal", "Dilan", "Elisha", "Brennan", "Kace", "Van", "Felipe", "Fisher", "Cal", "Dior", "Judson", "Alfonso", "Deandre", "Rocky", "Henrik", "Reuben", "Anders", "Arian", "Damir", "Jacoby", "Khalid", "Kye", "Mustafa", "Jadiel", "Stefan", "Yousef", "Aydin", "Jericho", "Robin", "Wallace", "Alistair", "Davion", "Alfred", "Ernesto", "Kyng", "Everest", "Gary", "Leroy", "Yahir", "Braden", "Kelvin", "Kristian", "Adler", "Avyaan", "Brayan", "Jones", "Truett", "Aries", "Joey", "Randy", "Jaxx", "Jesiah", "Jovanni", "Azriel", "Brecken", "Harley", "Zechariah", "Gordon", "Jakai", "Carl", "Graysen", "Kylen", "Ayan", "Branson", "Crosby", "Dominik", "Jabari", "Jaxtyn", "Kristopher", "Ulises", "Zyon", "Fox", "Howard", "Salvatore", "Turner", "Vance", "Harlem", "Jair", "Jakobe", "Jeremias", "Osiris", "Azael", "Bowie", "Canaan", "Elon", "Granger", "Karsyn", "Zavier", "Cain", "Dangelo", "Heath", "Yisroel", "Gian", "Shepard", "Harold", "Kamdyn", "Rene", "Rodney", "Yaakov", "Adrien", "Kartier", "Cassian", "Coleson", "Ahmir", "Darian", "Genesis", "Kalel", "Agustin", "Wylder", "Yadiel", "Ephraim", "Kody", "Neo", "Ignacio", "Osman", "Aldo", "Abdullah", "Cory", "Blaine", "Dimitri", "Khai", "Landry", "Palmer", "Benedict", "Leif", "Koen", "Maxton", "Mordechai", "Zev", "Atharv", "Bishop", "Blaise", "Davian" };

      //var boys = new string[] { "Wade", "Dave", "Seth", "Ivan", "Riley", "Gilbert", "Jorge", "Dan", "Brian", "Roberto", "Ramon", "Miles", "Liam", "Nathaniel", "Ethan", "Lewis", "Milton", "Claude", "Joshua", "Glen", "Harvey", "Blake", "Antonio", "Connor", "Julian", "Aidan", "Harold", "Conner", "Peter", "Hunter", "Eli", "Alberto", "Carlos", "Shane", "Aaron", "Marlin", "Paul", "Ricardo", "Hector", "Alexis", "Adrian", "Kingston", "Douglas", "Gerald", "Joey", "Johnny", "Charlie", "Scott", "Martin", "Tristin", "Troy", "Tommy", "Rick", "Victor", "Jessie", "Neil", "Ted", "Nick", "Wiley", "Morris", "Clark", "Stuart", "Orlando", "Keith", "Marion", "Marshall", "Noel", "Everett", "Romeo", "Sebastian", "Stefan", "Robin", "Clarence", "Sandy", "Ernest", "Samuel", "Benjamin", "Luka", "Fred", "Albert", "Greyson", "Terry", "Cedric", "Joe", "Paul", "George", "Bruce", "Christopher", "Mark", "Ron", "Craig", "Philip", "Jimmy", "Arthur", "Jaime", "Perry", "Harold", "Jerry", "Shawn", "Walter" };

      var ng = new Flux.Text.NameGenerator();

      //var chain = Flux.Text.NameGenerator.construct_chain(Flux.Text.NameGenerator.Egyptian);
      var chain = ng.construct_chain(eng);

      for (var i = 0; i < 25; i++)
      {
        var name = ng.markov_name(chain);
        System.Console.WriteLine(name);
      }

      return;

      var length = double.Sqrt(double.Pow(6, 2) + double.Pow(6, 2));

      var l = new Flux.Loops.AlternatingLoop<int>(7, 11, -1, Flux.Loops.AlternatingLoopDirection.AwayFromMean);
      //var l = new Flux.Loops.AlternatingLoop<int>(7, 11, 1, Flux.Loops.AlternatingLoopDirection.TowardsMean);
      //var l = new Flux.Loops.RangeLoop<int>(10, 3, -3);

      foreach (var i in l)
        System.Console.WriteLine(i);

      return;

      var d1 = 1.1;
      var d2 = 2.1;
      var d3 = double.Pow(d1, d2);

      var a1 = new Flux.Fraction(3, 5);
      var b1 = new Flux.Fraction(1, 2);

      var c1 = a1 % b1;

      System.Console.WriteLine(System.Numerics.BigInteger.One << 128);
      System.Console.WriteLine(System.UInt128.MaxValue);

      var ros = new char[] { 'a', 'b', 'c' }.AsSpan().AsReadOnlySpan();

      var sb = new Flux.SpanBuilder<char>();

      for (var i = 15; i > 0; i--)
        sb.Prepend(ros);



      var s = sb.ToString();

      System.Console.Write(s);

      return;

      //var t1 = Flux.ExtensionMethodsChar.GetOrdinalIndicatorSuffix('3', '0');
      //var t1c = '3'.ToStringEx();
      //System.Console.WriteLine(t1c);
      //var t1r = ((System.Text.Rune)'3').ToStringEx();
      //System.Console.WriteLine(t1r);

      //var sbc = new Flux.SpanBuilder<char>();
      //sbc.Append("hé\u0142\u0142ö");
      //sbc.Append(" 35 \U0001F469\U0001F3FD\u200D\U0001F692 ");
      //sbc.Append("wø\u024D\u0142\u0111");
      //sbc.Append('.');
      //sbc.AsSpan().ToCamelCase();
      //sbc.AsSpan().FromCamelCase();
      //sbc.InsertOrdinalIndicatorSuffix();
      //sbc.MakeNumbersFixedLength(10, '0');
      //var sbr = sbc.ToSpanBuilderRune();
      //var s = sbr.ToString();
      //var s1 = s.RemoveUnicodeMarks();
      //var s2 = s1.AsSpan().ReplaceUnicodeLatinStrokes();
      //var sh = s2.Shuffle(null);

      //var sc = s.AsSpan();
      //var sr = sc.ToSpanRune();
      //var ste = sc.ToSpanTextElement();
      //var sr1 = ste.ToSpanRune();
      //var sc1 = sr.ToSpanChar();

      //var sb = new Flux.SpanBuilder<char>();
      //for (var i = 0; i < 1000; i++)
      //  sbc.Append(s);

      //var e = rosr.EnumerateChars();

      //foreach (var t in e)
      //  System.Console.WriteLine(t);

      //System.Console.WriteLine();
      return;

      // Compute quantiles:
      {
        var aav = new int[][] { new int[] { 3, 6, 7, 8, 8, 10, 13, 15, 16, 20 }, new int[] { 3, 6, 7, 8, 8, 9, 10, 13, 15, 16, 20 } };

        foreach (var av in aav)
        {
          System.Console.WriteLine($"Sequence: [{string.Join(", ", av)}]");



          var htt = Flux.DataStructures.Histogram<int, int>.Create(av, k => k, f => 1);

          var h = Flux.DataStructures.Histogram<int, int>.Create(av, k => k, v => 1);
          System.Console.WriteLine($"HIST:{System.Environment.NewLine}{h.ToConsoleString()}{System.Environment.NewLine}");

          var v = 13;

          var pmf = Flux.DataStructures.ProbabilityMassFunction<int, double>.Create(h, 1.5d);
          var pmfv = h.ToPmfProbability(v, 1d);
          System.Console.WriteLine($"PMF:{System.Environment.NewLine}{pmf.ToConsoleString()}{System.Environment.NewLine}PV={pmfv}");

          var cmf = Flux.DataStructures.CumulativeMassFunction<int, double>.Create(h, 1.5d);
          var cmfv = h.ToCmfPercentRank(v, 1d);
          System.Console.WriteLine($"CMF:{System.Environment.NewLine}{cmf.ToConsoleString()}{System.Environment.NewLine}CV={cmfv}");

          //continue;

          foreach (QuantileAlgorithm a in System.Enum.GetValues<QuantileAlgorithm>())
          {
            var ac = av.Length;

            var qr = (ac.ComputeQuantileRank(0d, a), ac.ComputeQuantileRank(0.25, a), ac.ComputeQuantileRank(0.50, a), ac.ComputeQuantileRank(0.75, a), ac.ComputeQuantileRank(1d, a));
            var qv = (av.EstimateQuantileValue(0d, a), av.EstimateQuantileValue(0.25, a), av.EstimateQuantileValue(0.50, a), av.EstimateQuantileValue(0.75, a), av.EstimateQuantileValue(1d, a));
            //var qv = (av.EstimateQuantileValue(0.25, a), av.EstimateQuantileValue(0.50, a), av.EstimateQuantileValue(0.75, a));

            System.Console.WriteLine($"{a} : qR = {qr}, qV = {qv}");
          }

          System.Console.WriteLine();
        }

        return;
      }



      //// Compute roundings:
      //{
      //  var v = -1.5;
      //  var c = v.Round(RoundingMode.Ceiling);
      //  var e = v.Round(RoundingMode.Envelop);
      //  var f = v.Round(RoundingMode.Floor);
      //  var t = v.Round(RoundingMode.Truncate);
      //  var hafz = v.Round(RoundingMode.HalfAwayFromZero);
      //  var heven = v.Round(RoundingMode.HalfToEven);
      //  var hninf = v.Round(RoundingMode.HalfToNegativeInfinity);
      //  var hodd = v.Round(RoundingMode.HalfToOdd);
      //  var hpinf = v.Round(RoundingMode.HalfToPositiveInfinity);
      //  var htz = v.Round(RoundingMode.HalfTowardZero);
      //}
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = SetEncoding();

      SetSize(0.75);

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      ResetEncoding(originalOutputEncoding);

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();

      #region Support functions
      static void ResetEncoding(System.Text.Encoding originalOutputEncoding)
      {
        System.Console.OutputEncoding = originalOutputEncoding;
      }
      static System.Text.Encoding SetEncoding()
      {
        var originalOutputEncoding = System.Console.OutputEncoding;

        try { System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false); }
        catch { System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8; }

        System.Console.ForegroundColor = System.ConsoleColor.Blue;
        System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} {System.Console.OutputEncoding.HeaderName.ToUpper()} (code page {System.Console.OutputEncoding.CodePage})");
        System.Console.ResetColor();

        return originalOutputEncoding;
      }
      static void SetSize(double percentOfLargestWindowSize)
      {
        if (System.OperatingSystem.IsWindows())
        {
          if (percentOfLargestWindowSize < 0.1 || percentOfLargestWindowSize >= 1.0) throw new System.ArgumentOutOfRangeException(nameof(percentOfLargestWindowSize));

          var width = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowWidth * percentOfLargestWindowSize), System.Console.LargestWindowWidth), short.MaxValue);
          var height = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowHeight * percentOfLargestWindowSize), System.Console.LargestWindowHeight), short.MaxValue);

          System.Console.SetWindowSize(width, height);
        }
      }
      #endregion Support functions
    }
  }
}
