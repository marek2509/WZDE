using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZDE
{
     public class BazaDanych
    {
        public string Obreb { get; set; }
        public string NrJedn { get; set; }
        public string Dzialka { get; set; }
        public string PowierzchniaDzialki { get; set; }
        public string OFU { get; set; }
        public string OZU { get; set; }
        public string Klasa { get; set; }    
        public string PowierzchniaUzytku { get; set; }

        public void wypiszWszystko()
        {
           //Console.WriteLine("O " + Obreb + "N " + NrJedn + "D " + Dzialka + "PD " + PowierzchniaDzialki + "OFU " + OFU + "OZU " + OZU + "K " + Klasa + "PU " + PowierzchniaUzytku);
            Console.WriteLine(" " + Obreb + " " + NrJedn + " " + Dzialka + " " + PowierzchniaDzialki + " " + OFU + " " + OZU + " " + Klasa + " " + PowierzchniaUzytku);
        }
    }

    
}
