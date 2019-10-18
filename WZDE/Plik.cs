using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WZDE
{
    class Plik
    {
        public static string odczytZPliku(string a) //odczyt z pliku z wyjatkami niepowodzenia należy podać ścieżkę, zwraca tablicę odczytaną z pliku
        {
            string all = "";
            //  string[] lines = null;
            try
            {
                // lines = System.IO.File.ReadAllLines(a);
                /*  foreach (string line in lines)
                  {
                      // Console.WriteLine(line);
                  }*/
                all = System.IO.File.ReadAllText(a);
            }
            catch (Exception e)
            {
                Console.WriteLine("Do dupy: {0}", e.Message);
                MessageBox.Show("Błąd odcztu pliku txt lub csv.\nUpewnij się, że plik, \nktóry chcesz otworzyć jest zamknięty!", "ERROR", MessageBoxButton.OK);
            }
            return all;
        }

        public static void rozdzielenieTextu(string calyTextDoRozdzielenia, ref BazaDanych[] bazaDanych) //odczyt z pliku z wyjatkami niepowodzenia należy podać ścieżkę, zwraca tablicę odczytaną z pliku
        {
            string[] lines = null;
            string[] separators = { "\n", "\t", " " };
            try
            {
                lines = calyTextDoRozdzielenia.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            int licznik = 0;
            int licznikKlas = 0;
            foreach (string a in lines)
            {
                string tmp = a.Trim('\'', ' ');

                switch (licznik % 8)
                {

                    case 0:
                        //Console.WriteLine("OBR" + tmp);
                        bazaDanych[licznikKlas].Obreb = tmp;
                        break;
                    case 1:
                        //Console.WriteLine("NR JEDN" + tmp);
                        bazaDanych[licznikKlas].NrJedn = tmp;
                        break;
                    case 2:
                        //Console.WriteLine("DZIALKA" + tmp);
                        bazaDanych[licznikKlas].Dzialka = tmp;
                        break;
                    case 3:
                        //Console.WriteLine("Pow dzialki" + tmp);
                        bazaDanych[licznikKlas].PowierzchniaDzialki = tmp;
                        break;
                    case 4:
                        //Console.WriteLine("OFU" + tmp);
                        bazaDanych[licznikKlas].OFU = tmp;
                        break;
                    case 5:
                        //Console.WriteLine("OZU" + tmp);
                        bazaDanych[licznikKlas].OZU = tmp;
                        break;
                    case 6:
                        //Console.WriteLine("Klasa" + tmp);
                        bazaDanych[licznikKlas].Klasa = tmp;
                        break;
                    case 7:
                        //Console.WriteLine("pow uzytku" + tmp);
                        bazaDanych[licznikKlas].PowierzchniaUzytku = tmp;
                        break;

                    default:
                        Console.WriteLine("-----------------------------------------------Default case----------------------------------------------------");
                        break;
                }

                //   Console.WriteLine(licznik + "  licznik " + bazaDanych[licznikKlas].NrJedn + " " + bazaDanych[licznikKlas].Dzialka);
                /*
                bazaDanych.NrJedn = a;
                bazaDanych.Dzialka = a;
                bazaDanych.PowierzchniaDzialki = a;
                bazaDanych.OFU = a;
                bazaDanych.OZU = a;
                bazaDanych.Klasa = a;
                bazaDanych.PowierzchniaUzytku = a;
                */


                if (++licznik % 8 == 0)
                    licznikKlas++;
            }
        }

        public static string generowanieRejestru(string dokHTML, BazaDanych[] baza, int licznik, string wierszUzytku, string wierszDzialki, string wierszDziUzy)
        {


            string wierszUz = "";
            string wierszDZUZYTK = "";
            string wierszDzi = "";
            string wierszUzTMP = "";
            string wierszDzialTMP = "";
            string wierszDzUZYTKTMP = "";
            for (int i = 0; i <= licznik; i++)
            {
                wierszUzTMP = wierszUzytku;
                wierszDzialTMP = wierszDzialki;

                wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[i].Dzialka);
                dokHTML = dokHTML.Replace(ZnakiZastepcze.JednRejS, baza[i].NrJedn);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.PowDzialkiS, baza[i].PowierzchniaDzialki);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.powUzytkS, baza[i].PowierzchniaUzytku);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);

                Console.WriteLine("i gen " + i);
                wierszUz += wierszUzTMP;
                wierszDzi += wierszDzialTMP;
                try
                {
                   if (baza[i + 1].Dzialka.Equals(baza[i].Dzialka))
                    {
                       wierszDzialTMP = "";
                    }
                    
                        
                        }catch(Exception e)
                    {
                }
                 
                wierszDZUZYTK += wierszUzTMP + wierszDzialTMP; 

            }

//  dokHTML = dokHTML.Replace(wierszUzytku, wierszUz);
// dokHTML = dokHTML.Replace(wierszDzialki, wierszDzi);
dokHTML = dokHTML.Replace(wierszDziUzy, wierszDZUZYTK);
         
            return dokHTML;
        }

    }
}
