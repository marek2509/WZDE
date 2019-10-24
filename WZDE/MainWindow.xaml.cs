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
                sb.Append( BadanieKsiagWieczystych.SprawdzCyfreKontrolna(item.KW));
            }
            textBoxBledy.Text += sb.ToString(); 
           

        }

        private void ZapiszDoPliku(object sender, RoutedEventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.DefaultExt = ".doc";
            svd.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|HTML (*.html)|*.html|doc (*.doc)|*.doc";
            if (svd.ShowDialog() == true)
            {

                BazaDanych[] bazaTmp = new BazaDanych[1000];
                string dokHTML = WczytaneTekstowki.szablon;
                int licznikNowejBazy = 0;

                // Console.WriteLine(bazaDanych.Count() + "count");
                if (bazaDanych != null)
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


                                        sw.WriteLine(Plik.generowanieRejestru(dokHTML, bazaTmp, bazaDanychPos, licznikNowejBazy));
                                        licznikNowejBazy = 0;
                                        sw.Close();
                                    }
                                    catch (Exception exc)
                                    {
                                        Console.WriteLine(exc + "  problem z plikiem");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }

                        }

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
                sb.Append(BadanieKsiagWieczystych.SprawdzCyfreKontrolna(item.KW));
            }
            textBoxBledy.Text += sb.ToString();



            bool czyJestJednostka;
            bool czyWypisacTytulBrakEjdn = true ;
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
                        if (czyWypisacTytulBrakEjdn)
                        {
                            czyWypisacTytulBrakEjdn = false;
                           sbild.Append( "\nBrak nr jednostki w pliku przed scaleniem " + bazaDanychPos[i].NrJedn + "\n");


                        }
                      ileBrakujeJednostekWStarejBazie++;


                        if (!(brakujacaJedn.Equals(bazaDanychPos[i].NrJedn)))
                        {
                           
                            textBoxBledy.Text += " " + bazaDanychPos[i].NrJedn + ",";
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
            Console.WriteLine(ileBrakujeJednostekWStarejBazie + " : brak jednostek");
        }

        private void ZapisUstawien(object sender, RoutedEventArgs e)
        {
            zapisUstawienDomyslnych();
        }

        private void RadioButtonPoJednostkach_Checked(object sender, RoutedEventArgs e)
        {
            radioButtonPoJednostkach.IsChecked = true;
            radioButtonPoKW.IsChecked = false;
        }

        private void RadioButtonPoKW_Checked(object sender, RoutedEventArgs e)
        {
            radioButtonPoJednostkach.IsChecked = false;
            radioButtonPoKW.IsChecked = true;
        }
    }
}
