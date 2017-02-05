using perf.Import;
using perf.Import.EF;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perf.ImportNL
{
     public  static  class  ImportNewLogic
    {
        static string path = @"D:\Test.csv";

        private static List<Perfume> GetValuesList()
        {
            List<Perfume> perfumes = new List<Perfume>();
            string[] lines = File.ReadAllLines(path);

            int counter = 1;
            
            for (int i=0;i<lines.Length;i++)
            {

                string workLine = lines[i];
                

                while (!workLine.EndsWith("\"") || workLine.Count(f=>f=='"')%2==1)
                {
                    workLine += lines[i+1];
                    i++;
                }

                var values = workLine.Split(new string[] { ",\"" }, StringSplitOptions.None);
                //string first = workLine.Substring(0, workLine.IndexOf(","));
                //workLine = workLine.Substring(workLine.IndexOf(",")+1, workLine.Length - workLine.IndexOf(",")-1);

            // Console.WriteLine(first);
             //   Console.WriteLine(workLine);


            

                // var values = line.Replace("\"", "").Split(',');


                Perfume perf = new Perfume();
                perf.LineId = counter;

                int num;
                Int32.TryParse(values[0].Replace("\"","").Trim(), out num);

                if (num > 0)
                    perf.Number = num;
                else
                    perf.Number = null;

                if (values[1].Replace("\"", "").Trim().Length < 201)
                    perf.Product = values[1].Replace("\"","").Trim();
                else
                   perf.Product = String.Empty;

                if (values[2].Replace("\"", "").Trim().Length < 201)
                    perf.Producer = values[2];
                else
                    perf.Producer = String.Empty;

                if (values[3].Replace("\"", "").Trim().Length < 11)
                    perf.Gender = values[3].Replace("\"", "").Trim();
                else
                    perf.Gender = String.Empty;

                DateTime date;
                DateTime.TryParse(values[4].Replace("\"", "").Trim(), out date);

                if (date.Year > 1990)
                {
                    perf.Date = date;
                }
                else
                {
                    perf.Date = null;
                }

                if (values[5].Replace("\"", "").Trim().Length < 2)
                    perf.IsSampleAvailable = values[5].Replace("\"", "").Trim();
                else
                    perf.IsSampleAvailable = String.Empty;

                if (values[6].Replace("\"", "").Trim().Length < 251)
                    perf.Description = values[6].Replace("\"", "").Trim();
                else
                    perf.Description = String.Empty;


                if (values[7].Replace("\"", "").Trim().Length < 2)
                    perf.IsFullSize = values[7].Replace("\"", "").Trim();
                else
                    perf.IsFullSize = String.Empty;


                if (values[8].Replace("\"", "").Trim().Length < 251)
                    perf.Note = values[8].Replace("\"", "").Trim();
                else
                    perf.Note = String.Empty;

                if (values[9].Replace("\"", "").Trim().Length < 101)
                    perf.MagDate = values[9].Replace("\"", "").Trim();
                else
                    perf.MagDate = String.Empty;

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
                 try
                 {


                     db.SaveChanges();

                 }

                 catch (DbEntityValidationException ex)
                 {
                     StringBuilder sb = new StringBuilder();

                     foreach (var failure in ex.EntityValidationErrors)
                     {
                         sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                         foreach (var error in failure.ValidationErrors)
                         {
                             sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                             sb.AppendLine();
                         }
                     }
                     Console.WriteLine(sb.ToString());
                     throw new DbEntityValidationException(
                         "Entity Validation Failed - errors follow:\n" +
                         sb.ToString(), ex
                         ); // Add the original exception as the innerException

                 }
             }
         }
    }
}
