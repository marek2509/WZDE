using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
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

            }
            catch (Exception e)
            {
                Console.WriteLine("bł wczytanie ustawien domyslnych" + e);
            }
        }

        public void zapisUstawienDomyslnych()
        {
            try
            {
                Properties.Settings.Default.IdZglPrac = textBoxIdZglPrac.Text;
                Properties.Settings.Default.Gmina = textBoxGmina.Text;
                Properties.Settings.Default.Obreb = textBoxObręb.Text;
                Properties.Settings.Default.nrDecyzjiStarosty = textBoxNrDecyzji.Text;
                Properties.Settings.Default.MiejsceStarosty = textBoxMiejsceStarosty.Text;
                Properties.Settings.Default.decyzjaZdnia = textBoxdecyzjaZdnia.Text;
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




            wczytanieUstawienDomyślnych();

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
                    else if(radioButtonPoKW.IsChecked == true)
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
                                /*            else
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
                            }*/
                                // Console.WriteLine("else");
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
            zapisUstawienDomyslnych();
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
