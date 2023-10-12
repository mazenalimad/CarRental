using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Employees_List : Console_Print
    {
        private void Print(string username)
        {
            WriteLine("# Admin - Employees List #");
            WriteLine("Username : " + username);
            WriteLine("ID Account : " + useSession + "");
            WriteLine("\n1- Add");
            WriteLine("2- Show");
            WriteLine("3- Edit");
            WriteLine("4- Delete");
            WriteLine("5- Back");
        }

        private void SQL()
        {
            //TODO Dalton will extarct table inquire  from sql and select ID, name, ssn, hire_date, birthday, phone, salary
            this.Close();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();


            result = userDate.SqlQuary($"SELECT * FROM employees");

            foreach (var items in result)
            {
                foreach (var item in items)
                    Write($"{item}\t");
                Write("\n");
            }
            WriteLine("\n\n\nPress any key to go back");
            ReadKey();
        }
        private void Add()
        {
            this.Close();
            WriteLine("# Add #\n");
            Employees employee = new Employees();
            int Year = 0, Month = 0, Day = 0;
            // ID set from sql last id + 1 //TODO Dalton
            Write("Name : ");
            employee.Name = ReadLine();
            Write("SSN : ");
            employee.SSN = editor(employee.SSN);
            Write("Phone : ");
            employee.Phone_num = editor(employee.Phone_num);
            Write("BirthDay : ");
            Write("\n\tYear");
            Year = editor(Year);
            Write("\n\tMonth");
            Month = editor(Month);
            Write("\n\tDay");
            Day = editor(Day);
            employee.BirthDay = new Date(Month, Day, Year);


            //HireDate dirctly to sql sysdate

            Write("Salary : ");
            employee.Salary = editor(employee.Salary);


            //TODO Dalton : Add to SQL 
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            result = userDate.SqlQuary($"INSERT INTO employees (name, ssn, hire_date, birthday, salary, phone) VALUE ({employee.Name}, {employee.SSN}, NOW(), {employee.BirthDay.Year}-{employee.BirthDay.Month}-{employee.BirthDay.Day}, {employee.Salary}, {employee.Phone_num})");

            this.Close();
            Personal_Info p = new Employees();
            Write("\n\nthe following info has been added successfully\n");
            WriteLine($"{p}");
          
            ReadKey();
        }
        private void Edit()
        {
            //TODO Dalton : Edit from & to SQL
            this.Close();
            WriteLine("# Edit #\n");
            Employees employee = new Employees();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            int Year;
            int Month;
            int Day;

            Write("\nEnter the Employee id: ");
            employee.id = editor(employee.id);

            result = userDate.SqlQuary($"SELECT id, name, ssn, DATE_FORMAT(hire_date, '%Y-%m-%d'), DATE_FORMAT(hire_date, '%Y-%m-%d'), salary, phone FROM employees WHERE id =  {employee.id}");


            foreach (var items in result)
            {
                employee.Name = items[1];
                employee.SSN = Convert.ToInt32(items[2]);


                // date format
                string str = items[4];
                char[] spearator = { '-' };

                String[] strlist = str.Split(spearator);

                Year = Convert.ToInt32(strlist[0]);
                Month = Convert.ToInt32(strlist[1]);
                Day = Convert.ToInt32(strlist[2]);

                employee.BirthDay = new Date(Month, Day, Year);


                employee.Salary = Convert.ToInt32(items[5]);
                employee.Phone_num = Convert.ToInt32(items[6]);
            }


            // ID set from sql last id + 1 //TODO Dalton
            Write("\nName : " + employee.Name);
            employee.Name = editor(employee.Name);

            Write("\nSSN : " + employee.SSN);
            employee.SSN = editor(employee.SSN);

            Write("\nPhone : " + employee.Phone_num);
            employee.Phone_num = editor(employee.Phone_num);

            Write("\nBirthDay : ");

            Write("\n\tYear" + employee.BirthDay.Year);
            Year = editor(employee.BirthDay.Year);

            Write("\n\tMonth" + employee.BirthDay.Month);
            Month = editor(employee.BirthDay.Month);

            Write("\n\tDay" + employee.BirthDay.Day);
            Day = editor(employee.BirthDay.Day);

            employee.BirthDay = new Date(Month, Day, Year);


            //HireDate dirctly to sql sysdate

            Write("\nSalary : " + employee.Salary);
            employee.Salary = editor(employee.Salary);


            result = userDate.SqlQuary($"UPDATE employees SET name = '{employee.Name}', ssn = {employee.SSN}, birthday = '{employee.BirthDay.Year}-{employee.BirthDay.Month}-{employee.BirthDay.Day}', salary = {employee.Salary}, phone = {employee.Phone_num}  WHERE id =  {employee.id}");

            this.Close();
            Write("\n\nEdition is done Successfully");
            ReadKey();

        }
        private void Delete()
        {
            //TODO Dalton : Delete from SQl
            this.Close();
            WriteLine("# Delete #\n");
            Employees employee = new Employees();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            Write("\nEnter the Car id");
            employee.id = editor(employee.id);

            result = userDate.SqlQuary($"DELETE FROM employees WHERE id =  {employee.id}");

            this.Close();
            Write("\n\nDelete is done Successfully");
            ReadKey();
        }
        private char Press() //for choise without press enter
        {
            char key = Press_check();
            if (key < '1' || key > '5')
            {
                throw new ArgumentOutOfRangeException();//exception when enter wrong will get message fo it
            }
            return key; //return it
        }

        private void Move()
        {
            if (key == '1') //here for check of choise and teleport to function of choise
            {
                this.Add();
            }
            else if (key == '2')
            {
                this.SQL();
            }
            else if (key == '3')
            {
                this.Edit();
            }
            else if (key == '4')
            {
                this.Delete();
            }
            else
            {
                reload = false; //exit from application
            }
        }

        public void Show(string username) //access to home page method
        {
            this.Admin_Page(username);
        }


        private void Admin_Page(string username)
        {
            while (reload)//here we use it if reload is false turn of this function
            {
                this.Close(); this.Print(username); // here for clear terminal then show home page this if page reload
                do
                {
                    try
                    {
                        test = false; // here we put false it means chosen succesfully
                        key = this.Press();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        test = true; // Choise is invalid
                        Write("\rChoose Again : ");
                    }
                } while (test);
                this.Move();
            }
        }
    }
}