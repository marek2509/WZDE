using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace WZDE
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BazaDanych[] bazaDanych;
        public BazaDanych[] bazaDanychPos;

        public void wczytanieUstawienDomyślnych()
        {
            try
            {
                textBoxIdZglPrac.Text = Properties.Settings.Default.IdZglPrac;
                textBoxObręb.Text = Properties.Settings.Default.Obreb;
                textBoxGmina.Text = Properties.Settings.Default.Gmina;
                textBoxNrDecyzji.Text = Properties.Settings.Default.nrDecyzjiStarosty;
                textBoxMiejsceStarosty.Text = Properties.Settings.Default.MiejsceStarosty;
                textBoxdecyzjaZdnia.Text = Properties.Settings.Default.decyzjaZdnia;
                radioButtonPoJednBezKWZUsunieciemNiezmienionychDzialek.IsChecked = Properties.Settings.Default.radioButtonPoJednBezKWZUsunieciemNiezmienionychDzialek;
                radioButtonPoJednostkach.IsChecked = Properties.Settings.Default.radioButtonPoJednostkach;
                radioButtonPoJednostkachBezKW.IsChecked = Properties.Settings.Default.radioButtonPoJednostkachBezKW;
                radioButtonPoKW.IsChecked = Properties.Settings.Default.radioButtonPoKW;
                comboBox0Metry1Ary.SelectedIndex = Properties.Settings.Default.czyAry ? 1 : 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("bł wczytanie ustawien domyslnych" + e);
            }
        }

        public void ZapisUstawienDomyslnych()
        {
            try
            {
                Properties.Settings.Default.IdZglPrac = textBoxIdZglPrac.Text;
                Properties.Settings.Default.Gmina = textBoxGmina.Text;
                Properties.Settings.Default.Obreb = textBoxObręb.Text;
                Properties.Settings.Default.nrDecyzjiStarosty = textBoxNrDecyzji.Text;
                Properties.Settings.Default.MiejsceStarosty = textBoxMiejsceStarosty.Text;
                Properties.Settings.Default.decyzjaZdnia = textBoxdecyzjaZdnia.Text;
                Properties.Settings.Default.radioButtonPoJednBezKWZUsunieciemNiezmienionychDzialek = (bool)radioButtonPoJednBezKWZUsunieciemNiezmienionychDzialek.IsChecked;
                Properties.Settings.Default.radioButtonPoJednostkach = (bool)radioButtonPoJednostkach.IsChecked;
                Properties.Settings.Default.radioButtonPoJednostkachBezKW = (bool)radioButtonPoJednostkachBezKW.IsChecked;
                Properties.Settings.Default.radioButtonPoKW = (bool)radioButtonPoKW.IsChecked;
                Properties.Settings.Default.czyAry = comboBox0Metry1Ary.SelectedIndex == 1 ? true : false;
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("bł zapisu ustawien domyslnych" + e);
            }
        }



        public MainWindow()
        {
            InitializeComponent();
            try
            {

                Title = "inż. Marek Wojciechowicz    Wersja nr " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch
            {
                Console.WriteLine("błąd odczytu versji");
            }
            wczytanieUstawienDomyślnych();

            Console.WriteLine("Plik.metodaDopisujeZera(\"2132\")); + "+Plik.metodaDopisujeZera("2132"));
            Console.WriteLine("Plik.metodaDopisujeZera(\"3213,\")); + "+Plik.metodaDopisujeZera("3213,"));
            Console.WriteLine("Plik.metodaDopisujeZera(\"231312,2\")); + "+Plik.metodaDopisujeZera("231312,2"));
            Console.WriteLine("Plik.metodaDopisujeZera(\"231312,32\")); + "+Plik.metodaDopisujeZera("231312,32"));
            Console.WriteLine("Plik.metodaDopisujeZera(\"123,312\")); + "+Plik.metodaDopisujeZera("123,312"));
            Console.WriteLine("Plik.metodaDopisujeZera(\"123, 3122\")); + "+Plik.metodaDopisujeZera("123,3122"));
        }

        private void OtworzZPliku(object sender, RoutedEventArgs e)
        {
            string calyOdczzytanyText = "";
        poczatek:
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";

            dlg.Filter = "All files(*.*) | *.*|TXT Files (*.txt)|*.txt| CSV(*.csv)|*.csv";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                // textBox1.Text = filename;
                try
                {

                    calyOdczzytanyText = Plik.odczytZPliku(filename);
                }
                catch
                {
                    goto poczatek;
                }
            }
            //Console.WriteLine(calyOdczzytanyText);
            Console.WriteLine("x");
            Console.WriteLine("x");
            Console.WriteLine("x");
            Console.WriteLine("x");
            Console.WriteLine("x");
            Console.WriteLine("x");


            //string[] separators = { "\n" };
            //int iloscLinii = calyOdczzytanyText.Split(separators, StringSplitOptions.RemoveEmptyEntries).Count();
            //bazaDanych = new BazaDanych[iloscLinii];
            //for (int i = 0; i < iloscLinii; i++)
            //{
            //    bazaDanych[i] = new BazaDanych();
            //}

            //  Plik.rozdzielenieTextu(calyOdczzytanyText, ref bazaDanych);
            Plik.odczytMiedzyCudzyslowiami(calyOdczzytanyText, ref bazaDanych);
            textBoxBledy.Text += "\nPlik przed scaleniem błędy:\n ";
            StringBuilder sb = new StringBuilder();
            foreach (var item in bazaDanych)
            {
                sb.Append(BadanieKsiagWieczystych.SprawdzCyfreKontrolna(item.KW, item.Dzialka));
            }
            textBoxBledy.Text += sb.ToString();


        }



        private void ZapiszDoPliku(object sender, RoutedEventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.DefaultExt = "";
            svd.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|HTML (*.html)|*.html|doc (*.doc)|*.doc";
            if (svd.ShowDialog() == true)
            {

                BazaDanych[] bazaTmp = new BazaDanych[1000];
                int licznikNowejBazy = 0;

                // Console.WriteLine(bazaDanych.Count() + "count");

                if (bazaDanych != null)
                {
                    if (radioButtonPoJednostkach.IsChecked == true)
                    {
                        for (int i = 0; i < bazaDanych.Count(); i++)
                        {
                            bazaTmp[licznikNowejBazy] = bazaDanych[i];

                            if ((i < bazaDanych.Count() - 1) && (bazaDanych[i + 1].NrJedn.Equals(bazaDanych[i].NrJedn)))
                            {
                                // Console.WriteLine("       if (bazaDanych[i + 1].NrJedn.Equals(bazaDanych[i].NrJedn))");
                                bazaTmp[++licznikNowejBazy] = bazaDanych[i];
                            }
                            else
                            {
                                Console.WriteLine("else");

                                using (Stream s = File.Open(svd.FileName + bazaTmp[0].NrJedn + ".doc", FileMode.Create))
                                using (StreamWriter sw = new StreamWriter(s, Encoding.Default))

                                    try
                                    {
                                        try
                                        {
                                            sw.WriteLine(Plik.generowanieRejestruPoJednostce(bazaTmp, bazaDanychPos, licznikNowejBazy));
                                            licznikNowejBazy = 0;
                                            sw.Close();
                                        }
                                        catch (Exception exc)
                                        {
                                            MessageBox.Show(exc.ToString() + "  problem z plikiem");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                            }

                        }
                    }
                    else if (radioButtonPoKW.IsChecked == true)
                    {
                        //-----------------------------------------------------poczatek odczytu Po KW
                        StringBuilder stringBuilder = new StringBuilder();
                        // Console.WriteLine(" PO KW ");
                        for (int i = 0; i < bazaDanych.Count(); i++)
                        {
                            bazaTmp[licznikNowejBazy] = bazaDanych[i];

                            if ((i < bazaDanych.Count() - 1) && (bazaDanych[i + 1].KW.Equals(bazaDanych[i].KW)))
                            {

                                // Console.WriteLine("       if (bazaDanych[i + 1].NrJedn.Equals(bazaDanych[i].NrJedn))");
                                bazaTmp[++licznikNowejBazy] = bazaDanych[i];

                            }
                            else
                            {
                                // Console.WriteLine("else");
                                string nazwaPliku = bazaTmp[0].KW.Replace("/", "_");
                                using (Stream s = File.Open(svd.FileName + nazwaPliku + ".doc", FileMode.Create))
                                using (StreamWriter sw = new StreamWriter(s, Encoding.Default))

                                    try
                                    {

                                        try
                                        {
                                            sw.WriteLine(Plik.generowanieRejestruPoKW(bazaTmp, bazaDanychPos, licznikNowejBazy, ref stringBuilder));
                                            licznikNowejBazy = 0;
                                            sw.Close();
                                        }
                                        catch (Exception exc)
                                        {
                                            MessageBox.Show(exc + "  problem z plikiem");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }

                            }

                        }

                        textBoxBledy.Text += stringBuilder.ToString();
                        //-------------------------------------------------------koniec


                    }

                    else if (radioButtonPoJednostkachBezKW.IsChecked == true)
                    {
                        //-----------------------------------------------------poczatek odczytu Po KW
                        StringBuilder stringBuilder = new StringBuilder();
                        // Console.WriteLine(" PO KW ");
                        for (int i = 0; i < bazaDanych.Count(); i++)
                        {
                            bazaTmp[licznikNowejBazy] = bazaDanych[i];

                            if ((i < bazaDanych.Count() - 1) && (bazaDanych[i + 1].NrJedn.Equals(bazaDanych[i].NrJedn)))
                            {
                                bazaTmp[++licznikNowejBazy] = bazaDanych[i];
                            }
                            else
                            {

                                string nazwaPliku = bazaTmp[0].NrJedn;
                                using (Stream s = File.Open(svd.FileName + nazwaPliku + ".doc", FileMode.Create))
                                using (StreamWriter sw = new StreamWriter(s, Encoding.Default))
                                    try
                                    {
                                        try
                                        {
                                            sw.WriteLine(Plik.generowanieRejestruPoJednostceBezKW(bazaTmp, bazaDanychPos, licznikNowejBazy));
                                            licznikNowejBazy = 0;
                                            sw.Close();
                                        }
                                        catch (Exception exc)
                                        {
                                            MessageBox.Show(exc + "  problem z plikiem");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                            }
                        }

                        textBoxBledy.Text += stringBuilder.ToString();
                        //-------------------------------------------------------koniec


                    }
                    else if (radioButtonPoJednBezKWZUsunieciemNiezmienionychDzialek.IsChecked == true)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        StringBuilder sbWszystkieDokWjednym = new StringBuilder();
                        for (int i = 0; i < bazaDanych.Count(); i++)
                        {
                            bazaTmp[licznikNowejBazy] = bazaDanych[i];

                            if ((i < bazaDanych.Count() - 1) && (bazaDanych[i + 1].NrJedn.Equals(bazaDanych[i].NrJedn)))
                            {
                                bazaTmp[++licznikNowejBazy] = bazaDanych[i];
                            }
                            else
                            {
                                Console.WriteLine("jestem przed tym1 ELSE");
                                string nazwaPliku = bazaTmp[0].NrJedn;


                                //-----------------------------------------------------------------------------------------------

                                List<string> dzialkiDoUsuniecia = new List<string>();
                                BazaDanych[] bazaPRZEDWyszukana = new BazaDanych[licznikNowejBazy + 1];

                                for (int k = 0; k <= licznikNowejBazy; k++)
                                {
                                    bazaPRZEDWyszukana[k] = bazaTmp[k];
                                }

                                BazaDanych[] bazaPoScalDoJednoski = Plik.wyszukanieBazyPoScaleniu(bazaDanychPos, bazaPRZEDWyszukana[0].NrJedn);
                                for (int l = 0; l < bazaPRZEDWyszukana.Count(); l++)
                                {
                                    if (l == 0)
                                    {
                                        BazaDanych[] tymczasPO = Plik.wyszukanieBazyPoDziałkach(bazaPoScalDoJednoski, bazaPRZEDWyszukana[l].Dzialka);
                                        BazaDanych[] tymczasPRZED = Plik.wyszukanieBazyPoDziałkach(bazaPRZEDWyszukana, bazaPRZEDWyszukana[l].Dzialka);

                                        if (Plik.czyTakaSamaZawartosc(tymczasPRZED, tymczasPO))
                                        {
                                            dzialkiDoUsuniecia.Add(bazaPRZEDWyszukana[l].Dzialka);
                                        }
                                    }
                                    else if (!bazaPRZEDWyszukana[l - 1].Dzialka.Equals(bazaPRZEDWyszukana[l].Dzialka))
                                    {
                                        BazaDanych[] tymczasPO = Plik.wyszukanieBazyPoDziałkach(bazaPoScalDoJednoski, bazaPRZEDWyszukana[l].Dzialka);
                                        BazaDanych[] tymczasPRZED = Plik.wyszukanieBazyPoDziałkach(bazaPRZEDWyszukana, bazaPRZEDWyszukana[l].Dzialka);
                                        if (Plik.czyTakaSamaZawartosc(tymczasPRZED, tymczasPO))
                                        {
                                            dzialkiDoUsuniecia.Add(bazaPRZEDWyszukana[l].Dzialka);
                                        }
                                    }
                                }



                                BazaDanych[] BdPRZED = null;
                                BazaDanych[] BdPO = null;
                                BdPRZED = Plik.usunDzialkiZListy(bazaPRZEDWyszukana, dzialkiDoUsuniecia);
                                BdPO = Plik.usunDzialkiZListy(bazaPoScalDoJednoski, dzialkiDoUsuniecia);



                                if (BdPO == null && BdPRZED == null)
                                {
                                    licznikNowejBazy = 0;
                                    continue;
                                }

                                //Console.WriteLine("-*-**--**-***-*-**-*-*-*-*--*-*-**--*-**-*--*--*-*-*-*-**-");
                                //foreach (var item in bazaPRZEDWyszukana)
                                //{
                                //    Console.Write(item.Dzialka + " ");
                                //}

                                //if (BdPRZED != null)
                                //{
                                //    foreach (var item in BdPRZED)
                                //    {
                                //        Console.Write(item.NrJedn + "<>" + item.Dzialka + "   ");
                                //    }
                                //}
                                //else
                                //{
                                //    Console.WriteLine("NULL BdPRZED");
                                //    //BdPRZED = new BazaDanych[1];
                                //}
                                //Console.WriteLine("------------stan po---------------");

                                //if (BdPO != null)
                                //{



                                //    Console.WriteLine("BdPO (przed petla)");
                                //    foreach (var item in BdPO)
                                //    {
                                //        Console.Write("BdPO (w trakcie)>>>");
                                //        Console.Write(item.NrJedn + "<>" + item.Dzialka + "   ");
                                //    }
                                //}
                                //else
                                //{
                                //    Console.WriteLine("NULL BdPO");
                                //    //BdPO = new BazaDanych[1];
                                //}
                                //--------------------------------------------------------------------------------------------------


                                if (BdPRZED == null)
                                {
                                    Console.WriteLine(">=0");
                                }
                                else
                                {
                                    Console.WriteLine("0<");
                                }

                                using (Stream s = File.Open(svd.FileName + nazwaPliku + ".doc", FileMode.Create))
                                using (StreamWriter sw = new StreamWriter(s, Encoding.Default))
                                {
                                    try
                                    {
                                        Console.WriteLine("BdPRZED    <   >   BdPO");
                                        Console.WriteLine(BdPRZED == null);
                                        Console.WriteLine(BdPO == null);
                                        int countBdPrzed = BdPRZED == null ? 0 : BdPRZED.Count();

                                        string dokument = Plik.generowanieRejestruPoJednostceBezKW(BdPRZED, BdPO, (countBdPrzed - 1));
                                        Console.WriteLine("---------");






                                        //Console.WriteLine(BdPRZED.Count() + " " + BdPO.Count() + "o zesz");
                                        sw.WriteLine(dokument);
                                        licznikNowejBazy = 0;
                                        sw.Close();

                                        if (i == 0)
                                        {
                                            dokument = dokument.Replace("</body>", "<div>");
                                            dokument = dokument.Replace("</html>", "<div>");
                                        }
                                        else if (i == bazaDanych.Count() - 1)
                                        {
                                            dokument = dokument.Replace("<head>", "<div>");
                                            dokument = dokument.Replace("</head>", "</div>");
                                            dokument = dokument.Replace("<body>", "</div>");
                                            dokument = dokument.Replace("<html>", "</div>");
                                        }
                                        else
                                        {
                                            dokument = dokument.Replace("<html>", "<div>");
                                            dokument = dokument.Replace("</html>", "</div>");
                                            dokument = dokument.Replace("<head>", "<div>");
                                            dokument = dokument.Replace("</head>", "</div>");
                                            dokument = dokument.Replace("<body>", "<div>");
                                            dokument = dokument.Replace("</body>", "</div>");
                                        }



                                        sbWszystkieDokWjednym.Append(dokument);
                                        string HTML_PodzialSekcjiNaStronieNieparzystej = "<span style='font-size:12.0pt;font-family:\"Times New Roman\",\"serif\";mso-fareast-font-family: \"Times New Roman\";mso-ansi-language:PL;mso-fareast-language:PL;mso-bidi-language: AR-SA'><br clear=all style='page-break-before:right;mso-break-type:section-break'></span>";
                                        sbWszystkieDokWjednym.Append(HTML_PodzialSekcjiNaStronieNieparzystej);
                                        Console.WriteLine("---KONIEC^^^^^^^^^^------");
                                    }
                                    catch (Exception exc)
                                    {
                                        MessageBox.Show(exc + "  problem z plikiem \n" + BdPRZED);
                                    }
                                }
                            }

                        }


                        using (Stream s = File.Open(svd.FileName + ".doc", FileMode.Create))
                        using (StreamWriter sw = new StreamWriter(s, Encoding.Default))
                        {
                            try
                            {
                                sw.WriteLine(sbWszystkieDokWjednym.ToString());
                                sw.Close();

                            }
                            catch
                            {

                            }
                        }



                        textBoxBledy.Text += stringBuilder.ToString();
                        //-------------------------------------------------------koniec


                    }
                }
            }
        }


        private void OtworzStanPo(object sender, RoutedEventArgs e)
        {
            string calyOdczzytanyText = "";
        poczatek:
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";

            dlg.Filter = "All files(*.*) | *.*|TXT Files (*.txt)|*.txt| CSV(*.csv)|*.csv";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                // textBox1.Text = filename;
                try
                {

                    calyOdczzytanyText = Plik.odczytZPliku(filename);
                }
                catch
                {
                    goto poczatek;
                }
            }

            // Plik.rozdzielenieTextu(calyOdczzytanyText, ref bazaDanychPos, true);
            Plik.odczytMiedzyCudzyslowiami(calyOdczzytanyText, ref bazaDanychPos);

            BazaDanych temporary = new BazaDanych();
            //textBoxBledy.Visibility = Visibility.Hidden;
            textBoxBledy.Text += "\nPlik po scaleniu błędy: \n";
            StringBuilder sb = new StringBuilder();
            foreach (var item in bazaDanychPos)
            {
                sb.Append(BadanieKsiagWieczystych.SprawdzCyfreKontrolna(item.KW, item.Dzialka));
            }
            textBoxBledy.Text += sb.ToString();



            bool czyJestJednostka;
            bool czyWypisacTytulBrakEjdn = true;
            int ileBrakujeJednostekWStarejBazie = 0;
            string brakujacaJedn = "";



            if (bazaDanych != null)
            {
                StringBuilder sbild = new StringBuilder();
                for (int i = 0; i < bazaDanychPos.Count(); i++)
                {

                    czyJestJednostka = false;

                    for (int n = 0; n < bazaDanych.Count(); n++)
                    {
                        if ((bazaDanychPos[i].NrJedn.Equals(bazaDanych[n].NrJedn)))
                        {
                            czyJestJednostka = true;
                        }

                    }

                    if (!czyJestJednostka)
                    {
                        ileBrakujeJednostekWStarejBazie++;
                        if (czyWypisacTytulBrakEjdn)
                        {
                            czyWypisacTytulBrakEjdn = false;
                            sbild.Append("\nBrak nr jednostki w pliku przed scaleniem \n" + bazaDanychPos[i].NrJedn);


                        }
                        else


                        if (!(brakujacaJedn.Equals(bazaDanychPos[i].NrJedn)))
                        {

                            sbild.Append("\n " + bazaDanychPos[i].NrJedn + ",");
                            brakujacaJedn = bazaDanychPos[i].NrJedn;
                            // MessageBoxResult mbr = MessageBox.Show("Nowa jednostka po scaleniu? nr: " + bazaDanychPos[i].NrJedn);
                        }
                    }


                }
                textBoxBledy.Text += sbild.ToString();
            }
            else
            {
                MessageBox.Show("OTWÓRZ NAJPIERW STAN PRZED SCALENIEM!");
            }
            // Console.WriteLine(ileBrakujeJednostekWStarejBazie + " : brak jednostek");
        }

        private void ZapisUstawien(object sender, RoutedEventArgs e)
        {
            ZapisUstawienDomyslnych();
        }

        private void comboBox0Metry1Ary_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.czyAry = comboBox0Metry1Ary.SelectedIndex == 1 ? true : false;
            Properties.Settings.Default.Save();
        }


        //private void RadioButtonPoJednostkach_Checked(object sender, RoutedEventArgs e)
        //{
        //    radioButtonPoJednostkach.IsChecked = true;
        //    radioButtonPoKW.IsChecked = false;
        //}

        //private void RadioButtonPoKW_Checked(object sender, RoutedEventArgs e)
        //{
        //    radioButtonPoJednostkach.IsChecked = false;
        //    radioButtonPoKW.IsChecked = true;
        //}
    }
}
