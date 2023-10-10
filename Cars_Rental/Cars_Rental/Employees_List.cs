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
            //TODO Dalton will extarct table inquire  from sql and select ID, Brand, Car Model, Year, Status, Renter name, Lessor name
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
                       

            //TODO Dalton : Add to SQL 

        }
        private void Edit()
        {
            //TODO Dalton : Edit from & to SQL
        }
        private void Delete()
        {
            //TODO Dalton : Delete from SQl
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
            }
            else if (key == '4')
            {
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