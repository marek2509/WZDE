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
                       Console.WriteLine(line);
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



        public static void rozdzielenieTextu(string calyTextDoRozdzielenia, ref BazaDanych[] bazaDanych, bool opcjaZRowem = false) //odczyt z pliku z wyjatkami niepowodzenia należy podać ścieżkę, zwraca tablicę odczytaną z pliku
        {
            string[] lines = null;
            string[] separators = { "\n" };
            string[] wartosciZTxt = null;

            try
            {

                lines = calyTextDoRozdzielenia.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int n = 0;
                foreach (var item in lines)
                {

                    if (item.Trim() == "") continue;
                    n++;

                }

                wartosciZTxt = new string[n];
                int k = 0;
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (!(lines[i].Trim() == ""))
                    {
                        wartosciZTxt[k++] = lines[i].Trim();
                    }
                }

                Console.WriteLine("KONIEC09  " + wartosciZTxt.Count());

                int wielkTabli = wartosciZTxt.Count();
                Console.WriteLine("wielkosc tabl " + wielkTabli);
                bazaDanych = new BazaDanych[wielkTabli];

                for (int i = 0; i <= wielkTabli; i++)
                {
                    bazaDanych[i] = new BazaDanych();
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("POCZATEK ");
            int licznik = 0;
            int licznikKlas = 0;

            foreach (string a in wartosciZTxt)
            {

                // string tmp = a.Trim('\"', ' ', '\t');
                string[] separatory = { "\"", "'", "\'" };
                string[] tmp2 = a.Split(separatory, StringSplitOptions.RemoveEmptyEntries);
                string[] tmp = new string[10];
                int iter = 0;
                foreach (var item1 in tmp2)
                {

                    if (!(item1.Trim().Equals("")))
                    {
                        if (iter == 10) break;

                        if ((opcjaZRowem) && (iter == 5 || iter == 6) && (tmp[4].Equals("N") || tmp[4].Equals("Wp") || tmp[4].Equals("dr") || tmp[4].Equals("Bp") || tmp[4].Equals("Bi")))
                        {
                            tmp[iter++] = "";
                            tmp[iter++] = "";
                            tmp[iter++] = item1.Trim();
                        }
                        else if ((!opcjaZRowem)&&(iter == 5 || iter == 6) && (tmp[4].Equals("N") || tmp[4].Equals("Wp") || tmp[4].Equals("dr") || tmp[4].Equals("Bp") || tmp[4].Equals("Bi") || tmp[4].Equals("W")))
                        {
                            tmp[iter++] = "";
                            tmp[iter++] = "";
                            tmp[iter++] = item1.Trim();
                        }
                        else
                        {
                            tmp[iter++] = item1.Trim();
                        }

                        // Console.WriteLine(item1);
                    }
                }
                bazaDanych[licznikKlas].Obreb = tmp[0];
                bazaDanych[licznikKlas].NrJedn = tmp[1];
                bazaDanych[licznikKlas].Dzialka = tmp[2];
                bazaDanych[licznikKlas].PowierzchniaDzialki = tmp[3];
                bazaDanych[licznikKlas].OFU = tmp[4];
                bazaDanych[licznikKlas].OZU = tmp[5];
                bazaDanych[licznikKlas].Klasa = tmp[6];
                bazaDanych[licznikKlas].PowierzchniaUzytku = tmp[7];
                bazaDanych[licznikKlas].Podmion = tmp[8];
                bazaDanych[licznikKlas].KW = tmp[9];
                licznikKlas++;


                /*
                switch (licznik % 10)
                {

                    case 0:
                        Console.WriteLine("OBR" + tmp);
                        bazaDanych[licznikKlas].Obreb = tmp[0];
                        break;
                    case 1:
                        Console.WriteLine("NR JEDN" + tmp);
                        bazaDanych[licznikKlas].NrJedn = tmp[1];
                        break;
                    case 2:
                        Console.WriteLine("DZIALKA" + tmp);
                        bazaDanych[licznikKlas].Dzialka = tmp[2];
                        break;
                    case 3:
                        Console.WriteLine("Pow dzialki" + tmp);
                        bazaDanych[licznikKlas].PowierzchniaDzialki = tmp[3];
                        break;
                    case 4:
                        Console.WriteLine("OFU" + tmp);
                        bazaDanych[licznikKlas].OFU = tmp[4];
                        break;
                    case 5:
                        Console.WriteLine("OZU" + tmp);
                        bazaDanych[licznikKlas].OZU = tmp[5];
                        break;
                    case 6:
                        Console.WriteLine("Klasa" + tmp);
                        bazaDanych[licznikKlas].Klasa = tmp[6];
                        break;
                    case 7:
                        Console.WriteLine("pow uzytku" + tmp);
                        bazaDanych[licznikKlas].PowierzchniaUzytku = tmp[7];
                        break;
                    case 8:
                        Console.WriteLine("pow podmiot " + tmp);
                        bazaDanych[licznikKlas].Podmion = tmp[8];
                        break;
                    case 9:
                        Console.WriteLine(licznikKlas + "KW " + tmp);
                        bazaDanych[licznikKlas].KW = tmp[9];

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

                /*
                if (++licznik % 10 == 0)
                    licznikKlas++;/*/
            }
            Console.WriteLine("koniec ");
        }



        public static string oblPowJednoskiPoUzytku(BazaDanych[] baza, int licznik)
        {
            decimal powUzytku = 0;
            decimal powJednrej = 0;

            for (int i = 0; i <= licznik; i++)
            {

                decimal.TryParse(baza[i].PowierzchniaUzytku.Replace(".", ","), out powUzytku);
                powJednrej += powUzytku;
            }

            return powJednrej.ToString();
        }



        public static string generowanieRejestru(string dokHTML, BazaDanych[] baza, int licznik, string wierszDziUzy)
        {
            float pJRs = 0;// suma powierzchni działek

            for (int i = 0; i <= licznik; i++)
            {
                Console.WriteLine("WYPISUJE: --------------");
                Console.WriteLine(baza[i].Dzialka);
            }

            string wierszUz = "";
            string wierszDZUZYTK = "";
            string wierszDzi = "";
            string wierszUzTMP = "";
            string wierszDzialTMP = "";
            string wierszDzUZYTKTMP = "";
            for (int i = licznik; i >= 0; i--)
            {
                wierszUzTMP = WczytaneTekstowki.wierszUzytekUzytek;
                wierszDzialTMP = WczytaneTekstowki.wierszDzialkaDzialka;

                wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[i].Dzialka);
                dokHTML = dokHTML.Replace(ZnakiZastepcze.JednRejS, baza[i].NrJedn);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);

                wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.PowDzialkiS, baza[i].PowierzchniaDzialki);

                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.powUzytkS, baza[i].PowierzchniaUzytku);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);

                Console.WriteLine("i gen " + i);
                wierszUz += wierszUzTMP;
                // wierszDzi += wierszDzialTMP;


                baza[i].wypiszWszystko();

                if (licznik == 0)
                {

                    wierszDZUZYTK = wierszUzTMP + wierszDzialTMP;

                }
                else
                {
                    if (i == licznik)
                    {
                        // wierszDZUZYTK = wierszUzTMP + wierszDzialTMP + wierszDZUZYTK;

                        wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.KWs, baza[i].KW);
                        wierszDZUZYTK = wierszUzTMP + wierszDzialTMP;
                        //vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

                    }
                    else if (i > 0)
                    {
                        if (!baza[i + 1].Dzialka.Equals(baza[i].Dzialka))
                        {
                            wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.KWs, baza[i].KW);
                            wierszDZUZYTK = wierszUzTMP + wierszDzialTMP + wierszDZUZYTK;

                        }
                        else
                        {
                            wierszDZUZYTK = wierszUzTMP + wierszDZUZYTK;
                        }
                    }
                    else if (i == 0)
                    {
                        if (!baza[0].Dzialka.Equals(baza[1].Dzialka))
                        {
                            wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.KWs, baza[i].KW);
                            wierszDZUZYTK = wierszUzTMP + wierszDzialTMP + wierszDZUZYTK;
                        }
                        else
                        {
                            wierszDZUZYTK = wierszUzTMP + wierszDZUZYTK;
                        }
                    }

                }

            }
            // wierszDZUZYTK += wierszUzTMP;


            //  dokHTML = dokHTML.Replace(wierszUzytku, wierszUz);
            // dokHTML = dokHTML.Replace(wierszDzialki, wierszDzi);

            dokHTML = dokHTML.Replace(wierszDziUzy, wierszDZUZYTK);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.Podmiot, baza[0].Podmion);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRs, oblPowJednoskiPoUzytku(baza, licznik));

            return dokHTML;
        }

    }
}
