namespace Flux
{
  namespace Text
  {
    /// <summary>NameGenerator based on Markov chains.</summary>
    /// <see href="https://donjon.bin.sh/code/name/"/>
    /// <see href="https://donjon.bin.sh/code/name/name_generator.js"/>
    /// <seealso href="https://donjon.bin.sh/code/name/egyptian_set.js"/>
    public class NameGenerator
    {
      public static readonly string[] Egyptian = new string[] { "Aakheperkare", "Addaya", "Ahhotpe", "Ahmes", "Ahmose", "Ahmose-saneit", "Ahmose-sipari", "Akencheres", "Akunosh", "Amenakht", "Amenakhte", "Amenemhat", "Amenemheb", "Amenemopet", "Amenhirkopshef", "Amenhirwenemef", "Amenhotpe", "Amenmesse", "Amenmose", "Amennestawy", "Amenope", "Amenophis", "Amenwahsu", "Ameny", "Amosis-ankh", "Amoy", "Amunemhat", "Amunherpanesha", "Amunhotpe", "Anen", "Ankh-Psamtek", "Ankhef", "Ankhefenamun", "Ankhefenkhons", "Ankhefenmut", "Ankhsheshonq", "Ankhtify", "Ankhtyfy", "Ankhu", "Ankhuemhesut", "Any", "Apophis", "Baba", "Baenre", "Bak", "Bakenkhons", "Bakenkhonsu", "Bakenmut", "Bakennefi", "Bakenptah", "Baky", "Bata", "Bay", "Bek", "Bengay", "Besenmut", "Butehamun", "Denger", "Deniuenkhons", "Djadjaemankh", "Djau", "Djedefhor", "Djedhor", "Djedhoriufankh", "Djedi", "Djedkhonsiufankh", "Djedkhonsuefankh", "Djedptahefankh", "Djedptahiufankh", "Djehutmose", "Djehuty", "Djehutymose", "Djenutymes", "Djeserka", "Djeserkare", "Djeserkheprure", "Djesersukhons", "Djethutmose", "Djhutmose", "Genubath", "Gua", "Haankhef", "Hapimen", "Hapu", "Hapuseneb", "Hapymen", "Haremakhet", "Haremsat", "Harkhebi", "Harkhuf", "Harmhabi", "Harnakhte", "Harsiese", "Hay", "Hemaka", "Henenu", "Henuka", "Heqaemeheh", "Heqaib", "Herenamenpenaef", "Herihor", "Hesire", "Hor", "Horapollo", "Hordedef", "Horemheb", "Hori", "Hornedjitef", "Horpais", "Horwebbefer", "Hrihor", "Hunefer", "Huy", "Huya", "Iawy", "Ibana", "Ibe", "Idy", "Ikeni", "Ikui", "Imhotep", "Inarus", "Inebni", "Ineni", "Inyotef", "Ipi", "Ipuwer", "Ipuy", "Ipy", "Ishpi", "Iu-Amun", "Iufankh", "Iufenamun", "Iunmin", "Iuseneb", "Iuwlot", "Iyerniutef", "Iyimennuef", "Iymeru", "Jarha", "Kadjadja", "Kahma", "Kaka", "Kanakht", "Karnefhere", "Katenen", "Kawab", "Kay", "Kemuny", "Kenamun", "Kenefer", "Kerasher", "Kha", "Khaemhet", "Khaemnetjeru", "Khaemwaset", "Khahor", "Khakheperraseneb", "Khay", "Khensthoth", "Kheruef", "Khety", "Khnemibre", "Khnumhotep", "Khnumhotpe", "Khons", "Khonsirdais", "Khonskhu", "Khonsuemwaset", "Khufukhaf", "Khui", "Kuenre", "Kysen", "Maakha", "Mahu", "Mahuhy", "Maiherpri", "Manakhtuf", "Manetho", "Masaharta", "May", "Maya", "Mehy", "Meketre", "Mekhu", "Men", "Menkheperraseneb", "Menkheperre", "Menmet-Ra", "Menna", "Mentuemhat", "Mentuherkhepshef", "Meremptor", "Merenamun", "Merenkhons", "Merenptah", "Mereruka", "Merka", "Mernebptah", "Mery", "Meryamun", "Meryatum", "Meryawy", "Merymose", "Meryptah", "Meryrahashtef", "Meryre", "Mes", "Min", "Minkhat", "Minmose", "Minnakht", "Mokhtar", "Montjuemhat", "Montjuhirkopshef", "Montuemhet", "Mose", "Naga-ed-der", "Nakhthorheb", "Nakhtimenwast", "Nakhtmin", "Nakhtnebef", "Naneferkeptah", "Nebamun", "Nebankh", "Nebemakst", "Nebhotep", "Nebimes", "Nebitka", "Nebmaetre", "Nebnefer", "Nebnetjeru", "Nebseni", "Nebseny", "Nebwennenef", "Nechoutes", "Neferhotep", "Neferhotpe", "Neferkheperuhersekheper", "Nefermaet", "Nefermenu", "Neferrenpet", "Neferti", "Nehasy", "Nehi", "Nekau", "Nekhwemmut", "Nendjbaendjed", "Nenedjebaendjed", "Neneferkaptah", "Nenkhefta", "Nes", "Nesamun", "Neshi", "Neshorpakhered", "Neskhons", "Nesmont", "Nespaherenhat", "Nespakashuty", "Nespatytawy", "Nespherenhat", "Nessuimenopet", "Nestanebetasheru", "Nestefnut", "Netihur", "Nigmed", "Nimlot", "Niumateped", "Pa-Siamun", "Pabasa", "Pabernefy", "Padiamenet", "Padiamenipet", "Padiamun", "Padineith", "Paheripedjet", "Pairy", "Pait", "Pakharu", "Pakhneter", "Pamont", "Pamose", "Pamu", "Panas", "Paneb", "Paneferher", "Panehesy", "Paperpa", "Paramesse", "Parennefer", "Pasebakhaenniut", "Pasekhonsu", "Paser", "Pashedbast", "Pashedu", "Pasherdjehuty", "Pawiaeadja", "Paynedjem", "Payneferher", "Pediamun", "Pediese", "Pedihor", "Penamun", "Penbuy", "Penmaat", "Pennestawy", "Pentaweret", "Pentu", "Pepynakhte", "Peraha", "Pinhasy", "Pinotmou", "Prahotpe", "Pramessu", "Preherwenemef", "Prehirwennef", "Prepayit", "Psamtek", "Psenamy", "Psenmin", "Ptahhemakhet", "Ptahhemhat-Ty", "Ptahhotep", "Ptahhudjankhef", "Ptahmose", "Ptahshepses", "Qenymin", "Rahotep", "Rahotpe", "Raia", "Ramessenakhte", "Ramessu", "Rekhmire", "Reuser", "Rewer", "Roma-Roy", "Rudamun", "Sabef", "Sabni", "Salatis", "Samut", "Sanehet", "Sasobek", "Sawesit", "Scepter", "Sekhemkare", "Sekhmire", "Seneb", "Senebtyfy", "Senemut", "Senmen", "Sennedjem", "Sennefer", "Sennufer", "Senui", "Senwosret", "Serapion", "Sese", "Setau", "Setep", "Sethe", "Sethherwenemef", "Sethhirkopshef", "Sethnakhte", "Sethnakte", "Sethy", "Setne", "Setymerenptah", "Shedsunefertum", "Shemay", "Shepenwepet", "Si-Mut", "Siamun", "Siese", "Sinuhe", "Sipair", "Sneferu", "Somtutefnakhte", "Surero", "Suty", "Sutymose", "Takairnayu", "Takany", "Tasetmerydjehuty", "Tayenimu", "Tefibi", "Tenermentu", "Teti-en", "Tetisheri", "Tjaenhebyu", "Tjahapimu", "Tjaroy", "Tjauemdi", "Tjenna", "Tjety", "To", "Tui", "Tutu", "Tymisba", "Udjahorresne", "Udjahorresneith", "Uni", "Userhet", "Usermontju", "Wadjmose", "Wahibre-Teni", "Wahka", "Webaoner", "Webensenu", "Wedjakhons", "Wenamun", "Wendjabaendjed", "Wendjebaendjed", "Weni", "Wennefer", "Wennufer", "Wepmose", "Wepwawetmose", "Werdiamenniut", "Werirenptah", "Yanhamu", "Yey", "Yii", "Yuya", "Zazamoukh" };

      private readonly System.Random m_rng = new();

      //System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> m_chain = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>();

      private readonly System.Collections.Generic.Dictionary<int, int> m_nameLengthCounts = new();
      //private readonly System.Collections.Generic.Dictionary<int, double> m_nameLengthCountsNormalized = new();

      public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> Construct_Chain(System.Collections.Generic.IList<string> list)
      {
        foreach (var n in list)
          m_nameLengthCounts[n.Length] = m_nameLengthCounts.TryGetValue(n.Length, out var count) ? count + 1 : count;

        var lengths = list.ToHistogram(k => k.Length, f => 1).ToCumulativeDistributionFunction(1.0);

        var maxLength = lengths.Keys.Max();

        var cpl = new System.Collections.Generic.SortedDictionary<int, DataStructures.CumulativeDistributionFunction<char, double>>();

        foreach (var i in Iteration.Range(0, maxLength, 1))
          cpl[(int)i + 1] = list.Where(n => i < n.Length).ToHistogram(k => k[(int)i], f => System.Globalization.CultureInfo.CurrentCulture.IsVowelOf(f[(int)i]) ? 1 : 1).ToCumulativeDistributionFunction(1.0);

        var p1 = m_rng.NextDouble();
        var length = lengths.OrderBy(kvp => kvp.Key).First(kvp => p1 <= kvp.Value);

        var letters = new System.Collections.Generic.List<char>();

        for (var index = 1; index <= length.Key; index++)
        {
          var p2 = m_rng.NextDouble();

          letters.Add(cpl[index].First(kvp => p2 <= kvp.Value).Key);
        }

        var namo = string.Concat(letters);

        var chain = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>();

        for (var i = 0; i < list.Count; i++)
        {
          var names = System.Text.RegularExpressions.Regex.Split(list[i], @"\s+");

          chain = Incr_Chain(chain, "parts", names.Length.ToString());

          for (var j = 0; j < names.Length; j++)
          {
            var name = names[j];

            chain = Incr_Chain(chain, "name_len", name.Length.ToString());

            var c = name[..1];

            chain = Incr_Chain(chain, "initial", c);

            var s = name[1..];
            var last_c = c;

            while (s.Length > 0)
            {
              c = s[..1];

              chain = Incr_Chain(chain, last_c, c);

              s = s[1..];
              last_c = c;
            }
          }
        }

        return Scale_Chain(chain);
      }

      public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> Incr_Chain(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> chain, string key, string token)
      {
        if (chain.TryGetValue(key, out var chainKey))
        //if (chain.ContainsKey(key))
        {
          if (chainKey.TryGetValue(token, out var chainKeyToken))
            //if (chain[key].ContainsKey(token))
            chainKey[token] = chainKeyToken + 1;
          else
            chainKey[token] = 1;
        }
        else
        {
          chain[key] = new()
          {
            [token] = 1
          };
        }

        return chain;
      }

      public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> Scale_Chain(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> chain)
      {
        var table_len = new System.Collections.Generic.Dictionary<string, double>();

        foreach (var key in chain.Keys)
        {
          table_len[key] = 0;

          foreach (var token in chain[key].Keys)
          {
            var count = chain[key][token];
            var weighted = System.Math.Floor(System.Math.Pow(count, 1.3));

            chain[key][token] = weighted;
            table_len[key] += weighted;
          };
        }

        chain["table_len"] = table_len;

        return chain;
      }

      public string Markov_Name(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> chain)
      {
        var parts = int.Parse(Select_Link(chain, "parts")!);
        var names = new System.Collections.Generic.List<string>();

        for (var i = 0; i < parts; i++)
        {
          var name_len = int.Parse(Select_Link(chain, "name_len")!);
          var c = Select_Link(chain, "initial");
          var name = c;
          var last_c = c;

          while (name!.Length < name_len)
          {
            c = Select_Link(chain, last_c!);
            if (c is null) break;

            name += c;
            last_c = c;
          }
          names.Add(name);
        }
        return string.Join(' ', names);
      }

      public string? Select_Link(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> chain, string key)
      {
        if (!chain["table_len"].ContainsKey(key)) return null;

        var len = chain["table_len"][key];

        var idx = System.Math.Floor(m_rng.NextDouble() * len);

        var tokens = chain[key].Keys.ToList();
        var acc = 0d;

        for (var i = 0; i < tokens.Count; i++)
        {
          var token = tokens[i];

          acc += chain[key][token];
          if (acc > idx) return token;
        }

        return null;
      }
    }
  }
}
