using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            Plik.rozdzielenieTextu(calyOdczzytanyText, ref bazaDanych);

            //for (int i = 0; i < bazaDanych.Count(); i++)
            //{
            //    Console.WriteLine("kw m " + bazaDanych[i].KW);
            //}
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

                        using (Stream s = File.Open(svd.FileName + i + ".doc", FileMode.Create))
                        using (StreamWriter sw = new StreamWriter(s, Encoding.Default))

                            try
                            {

                                try
                                {
                                   

                                    sw.WriteLine(Plik.generowanieRejestru(dokHTML, bazaTmp, bazaDanychPos, licznikNowejBazy));
                                    licznikNowejBazy = 0;

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

            Plik.rozdzielenieTextu(calyOdczzytanyText, ref bazaDanychPos, true);
            BazaDanych temporary = new BazaDanych();
            bool czyJestJednostka;
            int ileBrakujeJednostekWStarejBazie = 0;
            for (int i = 0; i < bazaDanychPos.Count(); i++)
            {
                czyJestJednostka = false;
                foreach (var item in bazaDanych)
                {
                   if((bazaDanychPos[i].NrJedn.Equals(item.NrJedn)))
                    {
                        czyJestJednostka = true;
                    }
                }
               if(!czyJestJednostka)
                {
                    ileBrakujeJednostekWStarejBazie++;
                    MessageBox.Show("Nowa jednostka po scaleniu? nr: " + bazaDanychPos[i].NrJedn);
                }

            }
            Console.WriteLine(ileBrakujeJednostekWStarejBazie + " : brak jednostek");
        }

        private void ZapisUstawien(object sender, RoutedEventArgs e)
        {
            zapisUstawienDomyslnych();
        }
    }
}
