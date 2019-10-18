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

        string szablon = System.IO.File.ReadAllText(@"C:\Users\User\source\repos\WZDE\WZDE\SZABLON.txt");
        string wierszUzytku = System.IO.File.ReadAllText(@"C:\Users\User\source\repos\WZDE\WZDE\wierszUzytek.txt");
        string wierszDzialki = System.IO.File.ReadAllText(@"C:\Users\User\source\repos\WZDE\WZDE\wierszDzialka.txt");
        string wierszDziUzy = System.IO.File.ReadAllText(@"C:\Users\User\source\repos\WZDE\WZDE\wierszDziUzy.txt");


        public MainWindow()
        {
            InitializeComponent();

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


            string[] separators = { "\n" };
            int iloscLinii = calyOdczzytanyText.Split(separators, StringSplitOptions.RemoveEmptyEntries).Count();
            bazaDanych = new BazaDanych[iloscLinii];
            for (int i = 0; i < iloscLinii; i++)
            {
                bazaDanych[i] = new BazaDanych();
            }

            Plik.rozdzielenieTextu(calyOdczzytanyText, ref bazaDanych);

            for (int i = 0; i < bazaDanych.Count(); i++)
            {
                bazaDanych[i].wypiszWszystko();
            }
        }

        private void ZapiszDoPliku(object sender, RoutedEventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.DefaultExt = ".html";
            svd.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|HTML (*.html)|*.html";
            if (svd.ShowDialog() == true)
            {
                //using (Stream s = File.Open(svd.FileName, FileMode.Create))

                //using (StreamWriter sw = new StreamWriter(s, Encoding.Default))

                //    try
                //    {

                //        try
                //        {
                BazaDanych[] bazaTmp = new BazaDanych[100];
                string dokHTML = szablon;
                int licznikNowejBazy = 0;

                for (int i = 0; i < bazaDanych.Count(); i++)
                {
                  
                    bazaTmp[licznikNowejBazy] = bazaDanych[i];
                 
                    if (!(i < bazaDanych.Count() - 1)) break;

                    if (bazaDanych[i + 1].NrJedn.Equals(bazaDanych[i].NrJedn))
                    {
                        ++licznikNowejBazy;
                        // bazaTmp[++licznikNowejBazy] = bazaDanych[i];
                        Console.WriteLine(licznikNowejBazy);
                    }
                    else
                    {
                        using (Stream s = File.Open(svd.FileName + i + ".html", FileMode.Create))
                        using (StreamWriter sw = new StreamWriter(s, Encoding.Default))

                            try
                            {
                               
                                try
                                {
                                    
                          
                                    sw.WriteLine(Plik.generowanieRejestru(dokHTML, bazaTmp, licznikNowejBazy, wierszUzytku, wierszDzialki, wierszDziUzy));
                                    licznikNowejBazy = 0;
                                }
                                catch (Exception exc)
                                {
                                    Console.WriteLine(exc + "  problem z html");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }

                    }


                }
                //    catch (Exception exc)
                //    {
                //        Console.WriteLine(exc + "  problem z html");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
            }
        }
    }
}
