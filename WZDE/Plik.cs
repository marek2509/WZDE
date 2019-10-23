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
/*
        public void rozdzielenieLiniZCudzyslowami() // rozdziela linie np ' 1 ' ' 21 ' ' elo'
        {

        }
        */
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

                for (int i = 0; i < wielkTabli; i++)
                {
                    bazaDanych[i] = new BazaDanych();
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("POCZATEK ");
            
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
                        else if ((!opcjaZRowem) && (iter == 5 || iter == 6) && (tmp[4].Equals("N") || tmp[4].Equals("Wp") || tmp[4].Equals("dr") || tmp[4].Equals("Bp") || tmp[4].Equals("Bi") || tmp[4].Equals("W")))
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
                bazaDanych[licznikKlas].Podmiot = tmp[8];
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



        public static string oblPowJednoskiPoUzytku(BazaDanych[] baza, int licznik = -1)
        {
            if (licznik == -1) licznik = baza.Count() - 1;
            decimal powUzytku = 0;
            decimal powJednrej = 0;

            for (int i = 0; i <= licznik; i++)
            {

                decimal.TryParse(baza[i].PowierzchniaUzytku.Replace(".", ","), out powUzytku);
                powJednrej += powUzytku;
            }

            return powJednrej.ToString();
        }

        public static string oblPowDoSlownejLiczby(BazaDanych[] baza, int licznik = -1)
        {
            if (licznik == -1) licznik = baza.Count() - 1;
            decimal powUzytku = 0;
            decimal powJednrej = 0;

            for (int i = 0; i <= licznik; i++)
            {

                decimal.TryParse(baza[i].PowierzchniaUzytku.Replace(".", ","), out powUzytku);
                powJednrej += powUzytku;
            }

            int powWMetrach = (int)(powJednrej * 10000);
            return powWMetrach.ToString();
        }

        public static BazaDanych[] wyszukanieBazyPoScaleniu(BazaDanych[] bazaDanychPoScaleniu, string wyszukiwanaJednostka)
        {

            int iloscElemWyszukanych = 0;
            foreach (var item in bazaDanychPoScaleniu)
            {
                if (item.NrJedn.Equals(wyszukiwanaJednostka)) iloscElemWyszukanych++;
            }
            BazaDanych[] wyszukanaBaza = new BazaDanych[iloscElemWyszukanych];

            int iterator = 0;
            foreach (var item in bazaDanychPoScaleniu)
            {
                if (item.NrJedn.Equals(wyszukiwanaJednostka))

                    wyszukanaBaza[iterator++] = item;
            }

            return wyszukanaBaza;
        }


        public static string generowanieRejestru(string dokHTML, BazaDanych[] baza, BazaDanych[] bazaDanychPoScal, int indexOstatniegoElem)
        {
            BazaDanych[] bazaPoscalWyszukana = wyszukanieBazyPoScaleniu(bazaDanychPoScal, baza[0].NrJedn);

            // Console.WriteLine( "BAZA PO SCAL WYSZUKANA COUNT " +      bazaPoscalWyszukana.Count());
            List<string> listaLewejTaabeli = new List<string>();
            List<string> listaPrawejTaabeli = new List<string>();

            //   int iloscPetli = indexOstatniegoElem + 1 >= bazaPoscalWyszukana.Count() ? indexOstatniegoElem + 1 : bazaPoscalWyszukana.Count();

            // int indexOstatniegoElemTMP = indexOstatniegoElem;

            //string wierszUz = "";
            //string wierszDZUZYTK = "";
            //string wierszDzi = "";
            //string wierszUzTMP = "";
            //string wierszDzialTMP = "";
            //string wierszDzUZYTKTMP = "";

            int lastIndexPrawaStrone = bazaPoscalWyszukana.Count() - 1;
            if (bazaPoscalWyszukana.Count() > 0)
                for (int i = lastIndexPrawaStrone; i >= 0; i--) // utworzenie prawej części tabeli 
                {

                    if (lastIndexPrawaStrone == 0)
                    {
                        string txtTMP = WczytaneTekstowki.Pdzialka.Replace(ZnakiZastepcze.powDzialkiN, bazaPoscalWyszukana[0].PowierzchniaDzialki);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzNowy, bazaPoscalWyszukana[0].Dzialka);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.KWn, bazaPoscalWyszukana[0].KW);

                        listaPrawejTaabeli.Add(txtTMP);


                        txtTMP = WczytaneTekstowki.Puzytek.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[0].OZU);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[0].OFU);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy, bazaPoscalWyszukana[0].PowierzchniaUzytku);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[0].Klasa);

                        listaPrawejTaabeli.Add(txtTMP);
                    }
                    else
                    {
                        if (i == lastIndexPrawaStrone)
                        {

                            string txtTMP = WczytaneTekstowki.Pdzialka.Replace(ZnakiZastepcze.powDzialkiN, bazaPoscalWyszukana[lastIndexPrawaStrone].PowierzchniaDzialki);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzNowy, bazaPoscalWyszukana[lastIndexPrawaStrone].Dzialka);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.KWn, bazaPoscalWyszukana[lastIndexPrawaStrone].KW);

                            listaPrawejTaabeli.Add(txtTMP);


                            txtTMP = WczytaneTekstowki.Puzytek.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[lastIndexPrawaStrone].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[lastIndexPrawaStrone].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy, bazaPoscalWyszukana[lastIndexPrawaStrone].PowierzchniaUzytku);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[lastIndexPrawaStrone].Klasa);
                            listaPrawejTaabeli.Add(txtTMP);


                        }
                        else if (i > 0)
                        {
                            if (!bazaPoscalWyszukana[i + 1].Dzialka.Equals(bazaPoscalWyszukana[i].Dzialka))
                            {
                                string txtTMP = WczytaneTekstowki.Pdzialka.Replace(ZnakiZastepcze.powDzialkiN, bazaPoscalWyszukana[i].PowierzchniaDzialki);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzNowy, bazaPoscalWyszukana[i].Dzialka);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.KWn, bazaPoscalWyszukana[i].KW);
                                listaPrawejTaabeli.Add(txtTMP);

                                txtTMP = WczytaneTekstowki.Puzytek.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[i].OZU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[i].OFU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy, bazaPoscalWyszukana[i].PowierzchniaUzytku);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[i].Klasa);
                                listaPrawejTaabeli.Add(txtTMP);


                            }
                            else
                            {
                                string txtTMP = WczytaneTekstowki.Puzytek.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[i].OZU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[i].OFU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy, bazaPoscalWyszukana[i].PowierzchniaUzytku);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[i].Klasa);
                                listaPrawejTaabeli.Add(txtTMP);
                            }
                        }
                        else if (i == 0)
                        {
                            if (!bazaPoscalWyszukana[0].Dzialka.Equals(bazaPoscalWyszukana[1].Dzialka))
                            {
                                string txtTMP = WczytaneTekstowki.Pdzialka.Replace(ZnakiZastepcze.powDzialkiN, bazaPoscalWyszukana[i].PowierzchniaDzialki);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzNowy, bazaPoscalWyszukana[i].Dzialka);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.KWn, bazaPoscalWyszukana[i].KW);
                                listaPrawejTaabeli.Add(txtTMP);

                                txtTMP = WczytaneTekstowki.Puzytek.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[i].OZU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[i].OFU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy, bazaPoscalWyszukana[i].PowierzchniaUzytku);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[i].Klasa);
                                listaPrawejTaabeli.Add(txtTMP);
                            }
                            else
                            {
                                string txtTMP = WczytaneTekstowki.Puzytek.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[i].OZU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[i].OFU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy, bazaPoscalWyszukana[i].PowierzchniaUzytku);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[i].Klasa);
                                listaPrawejTaabeli.Add(txtTMP);
                            }
                        }

                    }

                }


            for (int i = indexOstatniegoElem; i >= 0; i--) // utworzenie lewej części tabeli 
            {
                /*
                wierszUzTMP = WczytaneTekstowki.wierszUzytekUzytek;
                wierszDzialTMP = WczytaneTekstowki.wierszDzialkaDzialka;

                wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[i].Dzialka);
                dokHTML = dokHTML.Replace(ZnakiZastepcze.JednRejS, baza[i].NrJedn);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);

                wierszDzialTMP = wierszDzialTMP.Replace(ZnakiZastepcze.PowDzialkiS, baza[i].PowierzchniaDzialki);

                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.powUzytkS, baza[i].PowierzchniaUzytku);
                wierszUzTMP = wierszUzTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                */
                Console.WriteLine("i gen " + i);
                //   wierszUz += wierszUzTMP;
                // wierszDzi += wierszDzialTMP;


                //   baza[i].wypiszWszystko();

                if (indexOstatniegoElem == 0)
                {
                    string txtTMP = WczytaneTekstowki.Ldzialka.Replace(ZnakiZastepcze.PowDzialkiS, baza[0].PowierzchniaDzialki);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[0].Dzialka);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.KWs, baza[0].KW);

                    listaLewejTaabeli.Add(txtTMP);


                    txtTMP = WczytaneTekstowki.Luzytek.Replace(ZnakiZastepcze.OZUS, baza[0].OZU);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[0].OFU);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, baza[0].PowierzchniaUzytku);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[0].Klasa);

                    listaLewejTaabeli.Add(txtTMP);
                }
                else
                {
                    if (i == indexOstatniegoElem)
                    {

                        string txtTMP = WczytaneTekstowki.Ldzialka.Replace(ZnakiZastepcze.PowDzialkiS, baza[indexOstatniegoElem].PowierzchniaDzialki);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[indexOstatniegoElem].Dzialka);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.KWs, baza[indexOstatniegoElem].KW);

                        listaLewejTaabeli.Add(txtTMP);


                        txtTMP = WczytaneTekstowki.Luzytek.Replace(ZnakiZastepcze.OZUS, baza[indexOstatniegoElem].OZU);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[indexOstatniegoElem].OFU);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, baza[indexOstatniegoElem].PowierzchniaUzytku);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[indexOstatniegoElem].Klasa);
                        listaLewejTaabeli.Add(txtTMP);


                    }
                    else if (i > 0)
                    {
                        if (!baza[i + 1].Dzialka.Equals(baza[i].Dzialka))
                        {
                            string txtTMP = WczytaneTekstowki.Ldzialka.Replace(ZnakiZastepcze.PowDzialkiS, baza[i].PowierzchniaDzialki);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[i].Dzialka);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.KWs, baza[i].KW);
                            listaLewejTaabeli.Add(txtTMP);

                            txtTMP = WczytaneTekstowki.Luzytek.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, baza[i].PowierzchniaUzytku);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                            listaLewejTaabeli.Add(txtTMP);


                        }
                        else
                        {
                            string txtTMP = WczytaneTekstowki.Luzytek.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, baza[i].PowierzchniaUzytku);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                            listaLewejTaabeli.Add(txtTMP);
                        }
                    }
                    else if (i == 0)
                    {
                        if (!baza[0].Dzialka.Equals(baza[1].Dzialka))
                        {
                            string txtTMP = WczytaneTekstowki.Ldzialka.Replace(ZnakiZastepcze.PowDzialkiS, baza[i].PowierzchniaDzialki);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[i].Dzialka);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.KWs, baza[i].KW);
                            listaLewejTaabeli.Add(txtTMP);

                            txtTMP = WczytaneTekstowki.Luzytek.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, baza[i].PowierzchniaUzytku);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                            listaLewejTaabeli.Add(txtTMP);
                        }
                        else
                        {
                            string txtTMP = WczytaneTekstowki.Luzytek.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, baza[i].PowierzchniaUzytku);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                            listaLewejTaabeli.Add(txtTMP);
                        }
                    }

                }

            }
            // wierszDZUZYTK += wierszUzTMP;


            //  dokHTML = dokHTML.Replace(wierszUzytku, wierszUz);
            // dokHTML = dokHTML.Replace(wierszDzialki, wierszDzi);

            //    dokHTML = dokHTML.Replace(wierszDziUzy, wierszDZUZYTK);


            string podmiankaTabela = "";
            int wiekszy = 0;
            if (listaPrawejTaabeli.Count > listaLewejTaabeli.Count)
            {
                wiekszy = listaPrawejTaabeli.Count;
            }
            else
            {
                wiekszy = listaLewejTaabeli.Count;
            }

            Console.WriteLine("qwerty lewa: " + listaLewejTaabeli.Count + "   prawa " + listaPrawejTaabeli.Count);


            listaLewejTaabeli.Reverse();
            listaPrawejTaabeli.Reverse();

            for (int i = 0; i < wiekszy; i++)
            {



                //----------------------------

                if (i < listaLewejTaabeli.Count)
                {
                    podmiankaTabela += listaLewejTaabeli[i];
                }
                else
                {
                    podmiankaTabela += WczytaneTekstowki.Lpusty;
                }

                if (i < listaPrawejTaabeli.Count)
                {
                    podmiankaTabela += listaPrawejTaabeli[i];
                }
                else
                {
                    podmiankaTabela += WczytaneTekstowki.Ppusty;
                }

            }


            dokHTML = dokHTML.Replace("bebechy", podmiankaTabela);
            //   dokHTML = dokHTML.Replace(ZnakiZastepcze.Podmiot, baza[0].Podmion);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRs, oblPowJednoskiPoUzytku(baza, indexOstatniegoElem));
            dokHTML = dokHTML.Replace(ZnakiZastepcze.JednRejS, baza[0].NrJedn);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.podmiotS, baza[0].Podmiot);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.slownieS, LiczbaNaTekst.DigitsStringToSpokenString(oblPowDoSlownejLiczby(baza,indexOstatniegoElem)));
            dokHTML = dokHTML.Replace(ZnakiZastepcze.obrebEwidencyjny, Properties.Settings.Default.Obreb);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.gmina, Properties.Settings.Default.Gmina);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.IdZglPracGeodez, Properties.Settings.Default.IdZglPrac);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.KWn, baza[0].KW);

            if (bazaPoscalWyszukana.Count() > 0)
            {
                dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRn, oblPowJednoskiPoUzytku(bazaPoscalWyszukana));
                dokHTML = dokHTML.Replace(ZnakiZastepcze.podmiotN, bazaPoscalWyszukana[0].Podmiot);
                dokHTML = dokHTML.Replace(ZnakiZastepcze.slownieN, LiczbaNaTekst.DigitsStringToSpokenString(oblPowDoSlownejLiczby(bazaPoscalWyszukana)));

            }

            return dokHTML;
        }

    }
}
