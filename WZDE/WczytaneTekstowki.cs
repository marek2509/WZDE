using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZDE
{
     public class WczytaneTekstowki
     {
      public static readonly string szablon = System.IO.File.ReadAllText(@"SZABLON.txt");
      public static readonly string wierszUzytekUzytek = System.IO.File.ReadAllText(@"wierszUzytek.txt");
      public static readonly string wierszDzialkaDzialka = System.IO.File.ReadAllText(@"wierszDzialka.txt");
      public static readonly string brakDzialkaNowa = System.IO.File.ReadAllText(@"brakDznow.txt");
      public static readonly string brakUzytekNowy = System.IO.File.ReadAllText(@"brakUzytNow.txt");
      public static readonly string dzialkaStaraBrak = System.IO.File.ReadAllText(@"wierszDzSTbrak.txt");
      public static readonly string dzialkaStarUzytekNowy= System.IO.File.ReadAllText(@"wierszDzSTUzNow.txt");
      public static readonly string uzytekStaryBrak= System.IO.File.ReadAllText(@"wierszUzStbrak.txt");
      public static readonly string uzytekStaryDzialkaNowa = System.IO.File.ReadAllText(@"wierszUzStDZnow.txt");
      public static readonly string pustyPusty = System.IO.File.ReadAllText(@"pustyPusty.txt");

      public static readonly string Ldzialka = System.IO.File.ReadAllText(@"Ldzialka.txt");
      public static readonly string Lpusty = System.IO.File.ReadAllText(@"Lpusty.txt");
      public static readonly string Luzytek = System.IO.File.ReadAllText(@"Luzytek.txt");
      public static readonly string Pdzialka = System.IO.File.ReadAllText(@"Pdzialka.txt");
      public static readonly string Ppusty = System.IO.File.ReadAllText(@"Ppusty.txt");
      public static readonly string Puzytek = System.IO.File.ReadAllText(@"Puzytek.txt");
     }
}
