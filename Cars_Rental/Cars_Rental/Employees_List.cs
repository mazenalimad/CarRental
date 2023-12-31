﻿using System;
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


            result = userDate.SqlQuary( $"SELECT id, name, ssn, DATE_FORMAT(hire_date, '%Y-%m-%d'), salary, phone FROM employees");
            WriteLine("{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}\n", "ID", "Name", "SSN", "Birth_Date", "salary", "phone");
            foreach (var items in result)
            {
                
                foreach (var item in items)
                    Write("{0,15}",item);
                Write("\n");
            }
            WriteLine("\n\n\nPress any key to go back");
            ReadKey();
        }
        private void Add()
        {
            // Collect the date 
            this.Close();
            WriteLine("# Add #\n");
            Employees employee = new Employees();
            
            // ID set from sql last id //TODO Dalton
            Write("Name : ");
            employee.Name = ReadLine();
            Write("SSN : ");
            employee.SSN = editor(employee.SSN);
            Write("\nPhone : ");
            employee.Phone_num = editor(employee.Phone_num);
            Write("\nBirthDay : ");
            do
            {
                try
                {
                    int Year = 0, Month = 0, Day = 0;

                    Write("\n\tYear: ");
                    Year = editor(Year);
                    Write("\n\tMonth: ");
                    Month = editor(Month);
                    Write("\n\tDay: ");
                    Day = editor(Day);
                    employee.BirthDay = new Date(Month, Day, Year);
                    test = false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    
                    Write("\nWrong Date");
                    test = true;
                }
            } while (test);

            //HireDate dirctly to sql sysdate

            Write("\nSalary : ");
            employee.Salary = editor(employee.Salary);


            //TODO Dalton : Add to SQL 
            AccessMySql userDate = new AccessMySql();

            // insert the date of employees 
            userDate.SqlQuary($"INSERT INTO employees (name, ssn, hire_date, birthday, salary, phone) VALUE ('{employee.Name}', {employee.SSN}, NOW(), '{employee.BirthDay.Year}-{employee.BirthDay.Month}-{employee.BirthDay.Day}', {employee.Salary}, {employee.Phone_num})");

            // the last msg with Elys part
            this.Close();
            Personal_Info p = employee;
            Write("\n\nthe following info has been added successfully\n");
            var header = String.Format(" {0,-10}{1,10}{2,13}{3,16}{4,17}\n",
                              "Name","SSN", "Phone", "BirthDay", "Salary");
            // WriteLine($"{"Name",10}  {"SSN",10} {"Phone",15} {"BirthDay",16} {"Salary",19}");
            WriteLine(header);
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

            // giving the date from datebase to collect it 
            result = userDate.SqlQuary($"SELECT id, name, ssn, DATE_FORMAT(hire_date, '%Y-%m-%d'), DATE_FORMAT(hire_date, '%Y-%m-%d'), salary, phone FROM employees WHERE id =  {employee.id}");

            if (result.Count > 0)
            {
                // collect the date in varabiles 
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


                // ID set from sql last id //TODO Dalton
                // make dynamic msg
                Write("\nName : " + employee.Name);
                employee.Name = editor(employee.Name);

                Write("\nSSN : " + employee.SSN);
                employee.SSN = editor(employee.SSN);

                Write("\nPhone : " + employee.Phone_num);
                employee.Phone_num = editor(employee.Phone_num);

                Write("\nBirthDay : ");
                do
                {
                    try {
                        Write("\n\tYear: " + employee.BirthDay.Year);
                        Year = editor(employee.BirthDay.Year);

                        Write("\n\tMonth: " + employee.BirthDay.Month);
                        Month = editor(employee.BirthDay.Month);

                        Write("\n\tDay: " + employee.BirthDay.Day);
                        Day = editor(employee.BirthDay.Day);

                        employee.BirthDay = new Date(Month, Day, Year);

                        test = false;
                    }catch(ArgumentOutOfRangeException)
                    {
                       
                        Write("\n \tWrong Date");
                        test = true;
                    }
                } while (test);

                //HireDate dirctly to sql sysdate

                Write("\nSalary : " + employee.Salary);
                employee.Salary = editor(employee.Salary);


                /// UPDATE the date of employees 
                result = userDate.SqlQuary($"UPDATE employees SET name = '{employee.Name}', ssn = {employee.SSN}, birthday = '{employee.BirthDay.Year}-{employee.BirthDay.Month}-{employee.BirthDay.Day}', salary = {employee.Salary}, phone = {employee.Phone_num}  WHERE id =  {employee.id}");

                this.Close();
                Write("\n\nEdition is done Successfully");
                ReadKey();
            }
            else
            {
                this.Close();
                WriteLine("\n\nThis Employee not found..Press any key to back ");
                ReadKey();
            }

        }
        private void Delete()
        {
            //TODO Dalton : Delete from SQl
            this.Close();
            WriteLine("# Delete #\n");
            Employees employee = new Employees();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            Write("\nEnter the Employee id: ");
            employee.id = editor(employee.id);

            result = userDate.SqlQuary($"SELECT id FROM employees WHERE id = {employee.id}");
            if (result.Count > 0)
            {
                result = userDate.SqlQuary($"SELECT id, name, ssn FROM car WHERE id = {employee.id}");
                WriteLine("\n{0,15}{1,15}{2,15}\n", "ID", "name", "ssn");
                foreach (var items in result)
                {

                    foreach (var item in items)
                        Write("{0,15}", item);
                    Write("\n");
                }

                Write("Are you sure delete it press enter : ");
                if (ReadKey(true).KeyChar == 13)
                {
                    /// Delete the date of employees
                    result = userDate.SqlQuary($"DELETE FROM employees WHERE id =  {employee.id}");

                    this.Close();
                    Write("\n\nDelete is done Successfully");
                    ReadKey();
                }
                else
                {
                    this.Close();
                    WriteLine("\n\nThis Employee not found..Press any key to back ");
                    ReadKey();
                }
            }
        }
        override protected char Press() //for choise without press enter
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