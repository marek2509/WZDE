using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZDE
{
    public class WczytaneTekstowki
    {

        //public static readonly string wierszUzytekUzytek = System.IO.File.ReadAllText(@"wierszUzytek.txt");
        //public static readonly string wierszDzialkaDzialka = System.IO.File.ReadAllText(@"wierszDzialka.txt");
        //public static readonly string brakDzialkaNowa = System.IO.File.ReadAllText(@"brakDznow.txt");
        //public static readonly string brakUzytekNowy = System.IO.File.ReadAllText(@"brakUzytNow.txt");
        //public static readonly string dzialkaStaraBrak = System.IO.File.ReadAllText(@"wierszDzSTbrak.txt");
        //public static readonly string dzialkaStarUzytekNowy= System.IO.File.ReadAllText(@"wierszDzSTUzNow.txt");
        //public static readonly string uzytekStaryBrak= System.IO.File.ReadAllText(@"wierszUzStbrak.txt");
        //public static readonly string uzytekStaryDzialkaNowa = System.IO.File.ReadAllText(@"wierszUzStDZnow.txt");
        //public static readonly string pustyPusty = System.IO.File.ReadAllText(@"pustyPusty.txt");

          
 	  

       
		  public static readonly string szablon;
        public static readonly string Pdzialka;
        public static readonly string Ldzialka;
        public static readonly string Lpusty;
        public static readonly string Luzytek;
        public static readonly string Ppusty;
        public static readonly string Puzytek;

        public static readonly string szablonKW;
        public static readonly string PdzialkaKW;
        public static readonly string LdzialkaKW;
        public static readonly string LpustyKW;
        public static readonly string LuzytekKW;
        public static readonly string PpustyKW;
        public static readonly string PuzytekKW;

        static WczytaneTekstowki()
        {
            try
            {
            szablon = System.IO.File.ReadAllText(@"SZABLON.txt");
            Pdzialka = System.IO.File.ReadAllText(@"Pdzialka.txt");
            Ldzialka = System.IO.File.ReadAllText(@"Ldzialka.txt");
            Lpusty = System.IO.File.ReadAllText(@"Lpusty.txt");
            Luzytek = System.IO.File.ReadAllText(@"Luzytek.txt");
            Ppusty = System.IO.File.ReadAllText(@"Ppusty.txt");
            Puzytek = System.IO.File.ReadAllText(@"Puzytek.txt");

            szablonKW = System.IO.File.ReadAllText(@"SZABLONKW.txt");
            PdzialkaKW = System.IO.File.ReadAllText(@"PdzialkaKW.txt");
            LdzialkaKW = System.IO.File.ReadAllText(@"LdzialkaKW.txt");
            LpustyKW = System.IO.File.ReadAllText(@"LpustyKW.txt");
            LuzytekKW = System.IO.File.ReadAllText(@"LuzytekKW.txt");
            PpustyKW = System.IO.File.ReadAllText(@"PpustyKW.txt");
            PuzytekKW = System.IO.File.ReadAllText(@"PuzytekKW.txt");
            }
            catch
            {
                szablon = Properties.Resources.SZABLON;
                Pdzialka = Properties.Resources.Pdzialka;
                Ldzialka = Properties.Resources.Ldzialka;
                Lpusty = Properties.Resources.Lpusty;
                Luzytek = Properties.Resources.Luzytek;
                Ppusty = Properties.Resources.Ppusty;
                Puzytek = Properties.Resources.Puzytek;

                szablonKW = Properties.Resources.SZABLONKW;
                PdzialkaKW = Properties.Resources.PdzialkaKW;
                LdzialkaKW = Properties.Resources.LdzialkaKW;
                LpustyKW = Properties.Resources.LpustyKW;
                LuzytekKW = Properties.Resources.LuzytekKW;
                PpustyKW = Properties.Resources.PpustyKW;
                PuzytekKW = Properties.Resources.PuzytekKW;
            }
        }






}
}
