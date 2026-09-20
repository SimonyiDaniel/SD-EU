using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EU
{
    // Egy tagállam adatait tároló osztály
    class Tagallam
    {
        public string Nev { get; private set; }
        public string Datum { get; private set; }   // pl. 1995.01.01

        public int Ev { get { return int.Parse(Datum.Substring(0, 4)); } }
        public int Honap { get { return int.Parse(Datum.Substring(5, 2)); } }

        public Tagallam(string sor)
        {
            string[] adatok = sor.Split(';');
            Nev = adatok[0];
            Datum = adatok[1];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 2. feladat: állomány beolvasása
            List<Tagallam> tagok = new List<Tagallam>();
            foreach (string sor in File.ReadAllLines("EUcsatlakozas.txt", Encoding.UTF8))
            {
                if (sor.Trim() != "")
                {
                    tagok.Add(new Tagallam(sor));
                }
            }

            // 3. feladat: tagállamok száma
            Console.WriteLine("3. feladat: EU tagállamainak száma: {0} db", tagok.Count);

            // 4. feladat: 2007-ben csatlakozott országok száma
            int db2007 = tagok.Count(t => t.Ev == 2007);
            Console.WriteLine("4. feladat: 2007-ben {0} ország csatlakozott.", db2007);

            // 5. feladat: Magyarország csatlakozásának dátuma
            Tagallam mo = tagok.First(t => t.Nev == "Magyarország");
            Console.WriteLine("5. feladat: Magyarország csatlakozásának dátuma: {0}", mo.Datum);

            // 6. feladat: volt-e csatlakozás májusban
            if (tagok.Any(t => t.Honap == 5))
            {
                Console.WriteLine("6. feladat: Májusban volt csatlakozás!");
            }
            else
            {
                Console.WriteLine("6. feladat: Májusban nem volt csatlakozás!");
            }

            // 7. feladat: utoljára csatlakozott ország
            // (az yyyy.MM.dd formátum szövegként is helyesen rendezhető)
            Tagallam utolso = tagok[0];
            foreach (Tagallam t in tagok)
            {
                if (string.Compare(t.Datum, utolso.Datum, StringComparison.Ordinal) > 0)
                {
                    utolso = t;
                }
            }
            Console.WriteLine("7. feladat: Legutoljára csatlakozott ország: {0}", utolso.Nev);

            // 8. feladat: statisztika évek szerint (az első előfordulás sorrendjében)
            Console.WriteLine("8. feladat: Statisztika");
            foreach (var csoport in tagok.GroupBy(t => t.Ev))
            {
                Console.WriteLine("\t{0} - {1} ország", csoport.Key, csoport.Count());
            }

            Console.ReadKey();
        }
    }
}