using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Console;

namespace Cars_Rental
{
   public class Login : Console_Print
    {
        private void Print() //the label what we want to print in console
        {
            WriteLine("# Login #");
            Write("- Press \"Enter\" to start Loign or any key to Back : ");
        }

        private void Checkusers(string username, string password)
        {
            //TODO Dalton : Compaire with user enter it then take the correct user id and send to admin.show(username,id)
            // here for check from sql user and compaire with user enter it
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();


            result = userDate.SqlQuary($"SELECT id, username, hash, type FROM users WHERE username = '{username}'");

            if (result.Count > 0)
            {    
                if (ComputeHash(password) == result[0][2])
                {
                    if (result[0][3] == "Admin")
                    {
                        //Dalton : this for admin users
                        Admin admin = new Admin();
                        useSession += Convert.ToInt32(result[0][0]);
                        admin.Show(username); // id will extract from sql
                    }
                    else if (result[0][3] == "Employees")
                    {
                        Employee_Form emp_form = new Employee_Form();
                        useSession = Convert.ToInt32(result[0][0]);
                        emp_form.Show(username);
                        //Dalton : this for employees users
                        //employees 
                    }
                    else
                    {
                        WriteLine("\nLogin Failed...\nPress any key to retry"); //if enter invalid user or pass
                        ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("\n\nLogin Failed: your username or Password is wrong ...\nPress any key to retry");
                ReadKey();
            }
        }


        public string ComputeHash(string s)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
            var sb = new StringBuilder();

            foreach (var item in bytes)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }

        public void Show()
        {
            this.Login_Page(); //access to login page method
        }
        private void Login_Page()
        {
            while (reload)
            {
                this.Close(); this.Print();
                key = this.Press(); // to make choise if he start login or cancel and back tp home page
                if (key == 13)
                {
                    Write("\n\nUsername : "); //print label to know what user do
                    string username = ReadLine(); // read what user enter it and save it in string
                    Write("Password : "); 
                    string password = ""; //to start password with empty
                    do //this loop for every key user enter it add to password string
                    {
                        key = ReadKey(true).KeyChar; //here to read key the user press it and not write it in console

                        if (key == 13) //if user press "Enter" stop the loop
                        {
                            break;
                        }
                        // Backspace Should Not Work
                        else if (key != 8) //if user not use backspace to remove do this
                        {
                            password += key; // add the key user pressed to password string
                            Write("*"); //write * to know what user do
                        }
                        else
                        {
                            if (password.Length > 0) //if password length = 0 do not this
                            {
                                password = password.Remove(password.Length - 1); //remove last char in string 
                                Console.Write("\b \b"); //backspace once then replace with space then backspace again to make it like user remove chat
                            }
                        }
                    } while (key != 13); // Stops Receving Keys Once Enter is Pressed
                    this.Checkusers(username, password);

                }
                else //here if enter any key else "Enter" stop and back to home page
                {
                    reload = false;
                }
            }
        }
        private char Press() //for choise without press enter
        {
            key = Press_check(); // to check what user pressed
            return key; // then return it
        }
    }
}
