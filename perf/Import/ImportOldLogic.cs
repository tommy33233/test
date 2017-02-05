using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using perf.Import.EF;

namespace perf.Import
{
   static class ImportOldLogic
    {
        static string path = @"D:\Test.csv";

        private static List<Perfume> GetValuesList()
        {
            List<Perfume> perfumes =new List<Perfume>();
            string[] lines = File.ReadAllLines(path);

            int counter = 1;

            foreach (var line in lines)
            {
                
               var  values = line.Replace("\"","").Split(',');
                if (values.Length < 10)
                    continue;
                
                Perfume perf=new Perfume();
                perf.LineId = counter;

                int num;
                Int32.TryParse(values[0], out num);

                if (num > 0)
                    perf.Number = num;
                else
                    perf.Number = null;

                if (values[1].Length < 201)
                    perf.Product = values[1];
                else
                    perf.Product = String.Empty;

                if (values[2].Length < 201)
                    perf.Producer = values[2];
                else
                    perf.Producer = String.Empty;

                if (values[3].Length < 11)
                    perf.Gender = values[3];
                else
                    perf.Gender = String.Empty;

                DateTime date;
                DateTime.TryParse(values[4], out date);

                if (date.Year > 1990)
                {
                    perf.Date = date;
                }
                else
                {
                    perf.Date = null;
                }

                if (values[5].Length < 2)
                    perf.IsSampleAvailable = values[5];
                else
                    perf.IsSampleAvailable=String.Empty;

                if (values[6].Length < 251)
                    perf.Description = values[6];
                else
                    perf.Description=String.Empty;
                if (values[7].Length < 2)
                    perf.IsFullSize = values[7];
                else
                    perf.IsFullSize = String.Empty;
                if (values[8].Length < 251)
                    perf.Note = values[8];
                else
                    perf.Note = String.Empty;

                if (values[9].Length < 101)
                    perf.MagDate = values[9];
                else
                    perf.MagDate=String.Empty;

                perfumes.Add(perf);
                counter++;

            }
            
            return perfumes;
        }

       public static void AddToDb()
       {
           using (PerfContext db = new PerfContext())
           {
               List<Perfume> perfumes = GetValuesList();
               foreach (var perfume in perfumes)
               {
                   db.Perfumes.Add(perfume);
               }
               db.SaveChanges();
           }
       }

    }
}
