using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Security.Cryptography;

namespace Cars_Rental
{

   public class AccessMySql
    {
        public string MySqlConnect { get; set; }
        private string filePath = "C:\\Users\\mozal\\Desktop\\carrental\\Cars_Rental\\quaryLinks.sql";


        public List<List<string>> MysqlAccessDatabase(string quary)
        {

            this.MySqlConnect = "datasource=127.0.0.1;port=3306;username=root;password=;database=carrental;";
            try
            {
                using (MySqlConnection databseConnection = new MySqlConnection(this.MySqlConnect))
                {
                    List<List<string>> result = new List<List<string>>();
                    databseConnection.Open();
                    MySqlCommand conn = new MySqlCommand(quary, databseConnection);
                    //conn.Parameters.AddWithValue("@name" , name);     SQL Injection sulotion
                    MySqlDataReader equal;


                    equal = conn.ExecuteReader();

                    if (equal.HasRows)
                    {
                        while (equal.Read())
                        {
                            List<string> items = new List<string>();
                            for (int i = 0; i < equal.FieldCount; i++)
                                items.Add(equal.GetString(i));
                            result.Add(items);
                        }
                    }
                    databseConnection.Close();
                    return result;
                }
            }
            catch
            {
                throw new ArgumentOutOfRangeException();

            }
        }

        public List<List<string>>  SqlQuary(string quary)
        {
            List<List<string>> equal = new List<List<string>>();
            try
            {
                equal = MysqlAccessDatabase(quary);
            }
            catch (ArgumentOutOfRangeException)
            {
                try
                {


                    MySqlConnect = "datasource=127.0.0.1;port=3306;username=root;password=;";
                    if (File.Exists(filePath))
                    {
                        FileInfo file = new FileInfo(filePath);
                        string script = file.OpenText().ReadToEnd();

                        using (MySqlConnection databseConnection = new MySqlConnection(MySqlConnect))
                        {
                            databseConnection.Open();
                            MySqlCommand conn = new MySqlCommand(script, databseConnection);
                            conn.ExecuteNonQuery();
                            databseConnection.Close();
                        }
                        file.OpenText().Close();
                    }
                    equal = MysqlAccessDatabase(quary);
                }
                catch (Exception)
                {
                    Console.WriteLine("\nAccess denied: Error in trying to make connection to DataBase... Press any key to exit\n");
                    Console.ReadKey();
                    Environment.Exit(0);
                }


            }

            /*if (equal != null)
            {
                Console.WriteLine("Correct");
                /*foreach (var items in equal)
                {
                    foreach (var item in items)
                        Console.Write(item + "  ");
                    Console.Write("\n");
                }
            }*/
        
            return equal;
            
        }
    }
}
