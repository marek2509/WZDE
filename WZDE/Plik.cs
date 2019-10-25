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


        public static string metodaDopisujeZera(string liczba)
        {

            if (liczba.Length - liczba.IndexOf(",") == 5)
            {

            }
            else if (liczba.Length - liczba.IndexOf(",") == 4)
            {
                liczba += "0";
            }
            else if (liczba.Length - liczba.IndexOf(",") == 3)
            {
                liczba += "00";
            }
            else if (liczba.Length - liczba.IndexOf(",") == 2)
            {
                liczba += "000";
            }
            else if (liczba.Length - liczba.IndexOf(",") == 1)
            {
                liczba += "0000";
            }
            else
            {
                liczba += ",0000";
            }


            return liczba;
        }

        public static void odczytMiedzyCudzyslowiami(string calyTextDoRozdzielenia, ref BazaDanych[] bazaDanych)
        {
            string[] lines = null;
            string[] separators = { "\n" };
            string[] separators2 = { "'", "\"" };
            string[] wartosciZTxt = null;

            try
            {
                lines = calyTextDoRozdzielenia.Split(separators, StringSplitOptions.RemoveEmptyEntries); //podzial na linie
            }
            catch
            {

            }

            bazaDanych = new BazaDanych[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                bazaDanych[i] = new BazaDanych();

                wartosciZTxt = lines[i].Split(separators2, StringSplitOptions.None);
                try
                {
                    bazaDanych[i].Obreb = wartosciZTxt[1].Trim();
                    bazaDanych[i].NrJedn = wartosciZTxt[3].Trim();
                    bazaDanych[i].Dzialka = wartosciZTxt[5].Trim();
                    //   bazaDanych[i].PowierzchniaDzialki = wartosciZTxt[7].Trim();


                    bazaDanych[i].PowierzchniaDzialki = metodaDopisujeZera(wartosciZTxt[7].Trim());
                    // Console.WriteLine("tutaj " + metodaDopisujeZera(bazaDanych[i].PowierzchniaDzialki));


                    bazaDanych[i].OFU = wartosciZTxt[9].Trim();
                    bazaDanych[i].OZU = wartosciZTxt[11].Trim();
                    bazaDanych[i].Klasa = wartosciZTxt[13].Trim();
                    //    bazaDanych[i].PowierzchniaUzytku = wartosciZTxt[15].Trim();

                    bazaDanych[i].PowierzchniaUzytku = metodaDopisujeZera(wartosciZTxt[15].Trim());
                    bazaDanych[i].Podmiot = wartosciZTxt[17].Trim();
                    bazaDanych[i].KW = wartosciZTxt[19].Trim();
                }
                catch (Exception e)
                {
                    MessageBox.Show(" PROBLEM Z TEKSTÓWKA! błędna linia: " + i, "ERROR", MessageBoxButton.OK);
                }

            }

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

        public static BazaDanych[] wyszukanieBazyPoScaleniu(BazaDanych[] bazaDanychPoScaleniu, string wyszukiwanaJednostka) //po jednostce
        {

            if (bazaDanychPoScaleniu != null)
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
            else
            {
                BazaDanych[] wyszukanaBaza = new BazaDanych[0];
                return wyszukanaBaza;
            }


        }


        public static string generowanieRejestruPoJednostce(BazaDanych[] baza, BazaDanych[] bazaDanychPoScal, int indexOstatniegoElem)
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

                Console.WriteLine("i gen " + i);


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

            string dokHTML = WczytaneTekstowki.szablon;
            dokHTML = dokHTML.Replace("bebechy", podmiankaTabela);
            //   dokHTML = dokHTML.Replace(ZnakiZastepcze.Podmiot, baza[0].Podmion);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRs, oblPowJednoskiPoUzytku(baza, indexOstatniegoElem));
            dokHTML = dokHTML.Replace(ZnakiZastepcze.JednRejS, baza[0].NrJedn);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.podmiotS, baza[0].Podmiot);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.slownieS, LiczbaNaTekst.DigitsStringToSpokenString(oblPowDoSlownejLiczby(baza, indexOstatniegoElem)));
            dokHTML = dokHTML.Replace(ZnakiZastepcze.obrebEwidencyjny, Properties.Settings.Default.Obreb);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.gmina, Properties.Settings.Default.Gmina);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.IdZglPracGeodez, Properties.Settings.Default.IdZglPrac);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.KWn, baza[0].KW);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.starostaW, Properties.Settings.Default.MiejsceStarosty);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.nrDecyzji, Properties.Settings.Default.nrDecyzjiStarosty);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.decyzjaZdnia, Properties.Settings.Default.decyzjaZdnia);

            if (bazaPoscalWyszukana.Count() > 0)
            {

                dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRn, oblPowJednoskiPoUzytku(bazaPoscalWyszukana));
                dokHTML = dokHTML.Replace(ZnakiZastepcze.jednRejesrtNowa, bazaPoscalWyszukana[0].NrJedn);
                dokHTML = dokHTML.Replace(ZnakiZastepcze.podmiotN, bazaPoscalWyszukana[0].Podmiot);
                dokHTML = dokHTML.Replace(ZnakiZastepcze.slownieN, LiczbaNaTekst.DigitsStringToSpokenString(oblPowDoSlownejLiczby(bazaPoscalWyszukana)));

            }
            else
            {
                dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRn, "");
                dokHTML = dokHTML.Replace(ZnakiZastepcze.jednRejesrtNowa, "");
                dokHTML = dokHTML.Replace(ZnakiZastepcze.podmiotN, "");
                dokHTML = dokHTML.Replace(ZnakiZastepcze.slownieN, "");
            }

            return dokHTML;
        }


        public static BazaDanych[] wyszukanieBazyPoScaleniuPoKW(BazaDanych[] bazaDanychPoScaleniu, string wyszukiwanaKW)
        {
            //foreach (var item in bazaDanychPoScaleniu)
            //{ 
            //    Console.WriteLine(" item.KW " + item.KW);
            //}

            if (bazaDanychPoScaleniu != null)
            {
                int iloscElemWyszukanych = 0;
                foreach (var item in bazaDanychPoScaleniu)
                {
                    if (item.KW.Equals(wyszukiwanaKW)) iloscElemWyszukanych++;
                }
                BazaDanych[] wyszukanaBaza = new BazaDanych[iloscElemWyszukanych];

                int iterator = 0;
                foreach (var item in bazaDanychPoScaleniu)
                {
                    if (item.KW.Equals(wyszukiwanaKW))

                        wyszukanaBaza[iterator++] = item;
                }


                return wyszukanaBaza;
            }
            else
            {
                BazaDanych[] wyszukanaBaza = new BazaDanych[0];
                return wyszukanaBaza;
            }


        }

   


        public static string generowanieRejestruPoKW(BazaDanych[] baza, BazaDanych[] bazaDanychPoScal, int indexOstatniegoElem, ref StringBuilder sb)
        {
            BazaDanych[] bazaPoscalWyszukana = wyszukanieBazyPoScaleniuPoKW(bazaDanychPoScal, baza[0].KW);

            //foreach (var item in bazaPoscalWyszukana)
            //{
            //    Console.WriteLine("wyszukana po scal : " + item.KW);
            //}

            List<string> listaLewejTaabeli = new List<string>();
            List<string> listaPrawejTaabeli = new List<string>();


            int lastIndexPrawaStrone = bazaPoscalWyszukana.Count() - 1;
            if (bazaPoscalWyszukana.Count() > 0)
                for (int i = lastIndexPrawaStrone; i >= 0; i--)
                {

                    if (lastIndexPrawaStrone == 0)
                    {
                        string txtTMP = WczytaneTekstowki.PdzialkaKW.Replace(ZnakiZastepcze.powDzialkiN, (bazaPoscalWyszukana[0].PowierzchniaDzialki));
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzNowy, bazaPoscalWyszukana[0].Dzialka);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.KWn, bazaPoscalWyszukana[0].KW);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.jednRejesrtNowa, bazaPoscalWyszukana[0].NrJedn);

                        listaPrawejTaabeli.Add(txtTMP);


                        txtTMP = WczytaneTekstowki.PuzytekKW.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[0].OZU);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[0].OFU);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy,bazaPoscalWyszukana[0].PowierzchniaUzytku);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[0].Klasa);

                        listaPrawejTaabeli.Add(txtTMP);
                    }
                    else
                    {
                        if (i == lastIndexPrawaStrone)
                        {

                            string txtTMP = WczytaneTekstowki.PdzialkaKW.Replace(ZnakiZastepcze.powDzialkiN, (bazaPoscalWyszukana[lastIndexPrawaStrone].PowierzchniaDzialki));
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzNowy, bazaPoscalWyszukana[lastIndexPrawaStrone].Dzialka);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.KWn, bazaPoscalWyszukana[lastIndexPrawaStrone].KW);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.jednRejesrtNowa, bazaPoscalWyszukana[lastIndexPrawaStrone].NrJedn);
                            listaPrawejTaabeli.Add(txtTMP);


                            txtTMP = WczytaneTekstowki.PuzytekKW.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[lastIndexPrawaStrone].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[lastIndexPrawaStrone].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy,bazaPoscalWyszukana[lastIndexPrawaStrone].PowierzchniaUzytku);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[lastIndexPrawaStrone].Klasa);
                            listaPrawejTaabeli.Add(txtTMP);


                        }
                        else if (i > 0)
                        {
                            if (!bazaPoscalWyszukana[i + 1].Dzialka.Equals(bazaPoscalWyszukana[i].Dzialka))
                            {
                                string txtTMP = WczytaneTekstowki.PdzialkaKW.Replace(ZnakiZastepcze.powDzialkiN, (bazaPoscalWyszukana[i].PowierzchniaDzialki));
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzNowy, bazaPoscalWyszukana[i].Dzialka);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.KWn, bazaPoscalWyszukana[i].KW);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.jednRejesrtNowa, bazaPoscalWyszukana[i].NrJedn);
                                listaPrawejTaabeli.Add(txtTMP);

                                txtTMP = WczytaneTekstowki.PuzytekKW.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[i].OZU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[i].OFU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy,bazaPoscalWyszukana[i].PowierzchniaUzytku);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[i].Klasa);
                                listaPrawejTaabeli.Add(txtTMP);


                            }
                            else
                            {
                                string txtTMP = WczytaneTekstowki.PuzytekKW.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[i].OZU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[i].OFU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy,bazaPoscalWyszukana[i].PowierzchniaUzytku);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[i].Klasa);
                                listaPrawejTaabeli.Add(txtTMP);
                            }
                        }
                        else if (i == 0)
                        {
                            if (!bazaPoscalWyszukana[0].Dzialka.Equals(bazaPoscalWyszukana[1].Dzialka))
                            {
                                string txtTMP = WczytaneTekstowki.PdzialkaKW.Replace(ZnakiZastepcze.powDzialkiN, bazaPoscalWyszukana[i].PowierzchniaDzialki);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzNowy, bazaPoscalWyszukana[i].Dzialka);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.KWn, bazaPoscalWyszukana[i].KW);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.jednRejesrtNowa, bazaPoscalWyszukana[i].NrJedn);
                                listaPrawejTaabeli.Add(txtTMP);

                                txtTMP = WczytaneTekstowki.PuzytekKW.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[i].OZU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUN, bazaPoscalWyszukana[i].OFU);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.PowUzytkNowy, bazaPoscalWyszukana[i].PowierzchniaUzytku);
                                txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaN, bazaPoscalWyszukana[i].Klasa);
                                listaPrawejTaabeli.Add(txtTMP);
                            }
                            else
                            {
                                string txtTMP = WczytaneTekstowki.PuzytekKW.Replace(ZnakiZastepcze.OZUN, bazaPoscalWyszukana[i].OZU);
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

                //  Console.WriteLine("i gen " + i);


                if (indexOstatniegoElem == 0)
                {
                    string txtTMP = WczytaneTekstowki.LdzialkaKW.Replace(ZnakiZastepcze.PowDzialkiS, baza[0].PowierzchniaDzialki);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[0].Dzialka);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.KWs, baza[0].KW);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.JednRejS, baza[0].NrJedn);

                    listaLewejTaabeli.Add(txtTMP);


                    txtTMP = WczytaneTekstowki.LuzytekKW.Replace(ZnakiZastepcze.OZUS, baza[0].OZU);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[0].OFU);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS,baza[0].PowierzchniaUzytku);
                    txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[0].Klasa);

                    listaLewejTaabeli.Add(txtTMP);
                }
                else
                {
                    if (i == indexOstatniegoElem)
                    {

                        string txtTMP = WczytaneTekstowki.LdzialkaKW.Replace(ZnakiZastepcze.PowDzialkiS, baza[indexOstatniegoElem].PowierzchniaDzialki);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[indexOstatniegoElem].Dzialka);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.KWs, baza[indexOstatniegoElem].KW);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.JednRejS, baza[indexOstatniegoElem].NrJedn);
                        listaLewejTaabeli.Add(txtTMP);


                        txtTMP = WczytaneTekstowki.LuzytekKW.Replace(ZnakiZastepcze.OZUS, baza[indexOstatniegoElem].OZU);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[indexOstatniegoElem].OFU);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, baza[indexOstatniegoElem].PowierzchniaUzytku);
                        txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[indexOstatniegoElem].Klasa);
                        listaLewejTaabeli.Add(txtTMP);


                    }
                    else if (i > 0)
                    {
                        if (!baza[i + 1].Dzialka.Equals(baza[i].Dzialka))
                        {
                            string txtTMP = WczytaneTekstowki.LdzialkaKW.Replace(ZnakiZastepcze.PowDzialkiS, baza[i].PowierzchniaDzialki);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[i].Dzialka);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.KWs, baza[i].KW);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.JednRejS, baza[i].NrJedn);
                            listaLewejTaabeli.Add(txtTMP);

                            txtTMP = WczytaneTekstowki.LuzytekKW.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, baza[i].PowierzchniaUzytku);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                            listaLewejTaabeli.Add(txtTMP);


                        }
                        else
                        {
                            string txtTMP = WczytaneTekstowki.LuzytekKW.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, (baza[i].PowierzchniaUzytku));
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                            listaLewejTaabeli.Add(txtTMP);
                        }
                    }
                    else if (i == 0)
                    {
                        if (!baza[0].Dzialka.Equals(baza[1].Dzialka))
                        {
                            string txtTMP = WczytaneTekstowki.LdzialkaKW.Replace(ZnakiZastepcze.PowDzialkiS, (baza[i].PowierzchniaDzialki));
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.nrDzialkiS, baza[i].Dzialka);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.KWs, baza[i].KW);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.JednRejS, baza[i].NrJedn);
                            listaLewejTaabeli.Add(txtTMP);

                            txtTMP = WczytaneTekstowki.LuzytekKW.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, (baza[i].PowierzchniaUzytku));
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                            listaLewejTaabeli.Add(txtTMP);
                        }
                        else
                        {
                            string txtTMP = WczytaneTekstowki.LuzytekKW.Replace(ZnakiZastepcze.OZUS, baza[i].OZU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.OFUS, baza[i].OFU);
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.powUzytkS, (baza[i].PowierzchniaUzytku));
                            txtTMP = txtTMP.Replace(ZnakiZastepcze.klasaS, baza[i].Klasa);
                            listaLewejTaabeli.Add(txtTMP);
                        }
                    }

                }

            }


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

            Console.WriteLine("qwerty lewa: " + listaLewejTaabeli.Count + "   prawa " + listaPrawejTaabeli.Count + " " + baza[0].KW);

            if (listaPrawejTaabeli.Count == 0)
            {
                sb.Append("\nBrak księgi w stanie po scaleniu " + baza[0].KW);
            }

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
                    podmiankaTabela += WczytaneTekstowki.LpustyKW;
                }

                if (i < listaPrawejTaabeli.Count)
                {
                    podmiankaTabela += listaPrawejTaabeli[i];
                }
                else
                {
                    podmiankaTabela += WczytaneTekstowki.PpustyKW;
                }

            }

            string dokHTML = WczytaneTekstowki.szablonKW;
            dokHTML = dokHTML.Replace("bebechy", podmiankaTabela);
            //   dokHTML = dokHTML.Replace(ZnakiZastepcze.Podmiot, baza[0].Podmion);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRs, (oblPowJednoskiPoUzytku(baza, indexOstatniegoElem)));
            // dokHTML = dokHTML.Replace(ZnakiZastepcze.JednRejS, baza[0].NrJedn);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.podmiotS, baza[0].Podmiot);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.slownieS, LiczbaNaTekst.DigitsStringToSpokenString(oblPowDoSlownejLiczby(baza, indexOstatniegoElem)));
            dokHTML = dokHTML.Replace(ZnakiZastepcze.obrebEwidencyjny, Properties.Settings.Default.Obreb);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.gmina, Properties.Settings.Default.Gmina);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.IdZglPracGeodez, Properties.Settings.Default.IdZglPrac);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.KWn, baza[0].KW);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.starostaW, Properties.Settings.Default.MiejsceStarosty);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.nrDecyzji, Properties.Settings.Default.nrDecyzjiStarosty);
            dokHTML = dokHTML.Replace(ZnakiZastepcze.decyzjaZdnia, Properties.Settings.Default.decyzjaZdnia);

            if (bazaPoscalWyszukana.Count() > 0)
            {

                dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRn, (oblPowJednoskiPoUzytku(bazaPoscalWyszukana)));
                //  dokHTML = dokHTML.Replace(ZnakiZastepcze.jednRejesrtNowa, bazaPoscalWyszukana[0].NrJedn);
                dokHTML = dokHTML.Replace(ZnakiZastepcze.podmiotN, bazaPoscalWyszukana[0].Podmiot);
                dokHTML = dokHTML.Replace(ZnakiZastepcze.slownieN, LiczbaNaTekst.DigitsStringToSpokenString(oblPowDoSlownejLiczby(bazaPoscalWyszukana)));

            }
            else
            {
                dokHTML = dokHTML.Replace(ZnakiZastepcze.pJRn, "");
                dokHTML = dokHTML.Replace(ZnakiZastepcze.jednRejesrtNowa, "");
                dokHTML = dokHTML.Replace(ZnakiZastepcze.podmiotN, "");
                dokHTML = dokHTML.Replace(ZnakiZastepcze.slownieN, "");
            }

            return dokHTML;
        }
    }
}
