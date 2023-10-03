﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Cars_List : Console_Print
    {
        private void Print(string username, int id)
        {
            WriteLine("# Admin #");
            WriteLine("Username : " + username);
            WriteLine("ID Account : " + id + "\n\n");
            this.SQL();
            WriteLine("\n1- Add");
            WriteLine("2- Edit");
            WriteLine("3- Delete");
        }

        private void SQL()
        {
            //TODO Dalton will extarct table inquire  from sql and select ID, Brand, Car Model, Year, Status, Renter name, Lessor name
        }
        private void Add()
        {
            this.Close();
            WriteLine("# Add #\n");
            Cars car = new Cars();
            // ID set from sql last id + 1 //TODO Dalton
            Write("Brand : ");
            car.Brand = ReadLine();
            WriteLine("\n new : "+car.Brand);
            Write("Model : ");
            car.Model = ReadLine();
            Write("Year : ");
            car.Year = editor(car.Year);
            Write("License Plate No. : ");
            car.License_plate_no = Convert.ToInt32(ReadLine());
            Write("Price : ");
            car.Price = Convert.ToInt32(ReadLine());
            Write("Lessor Name : ");
            car.lessor.Name = ReadLine();
            Write("Lessor SSN : ");
            car.lessor.SSN = Convert.ToInt32(ReadLine());
            Write("Lessor Phone : ");
            car.lessor.Phone_num = Convert.ToInt32(ReadLine());
            Write("Lessor Commission : ");
            car.lessor.Commission_price = float.Parse(ReadLine());
            Write("Renter Name : ");
            car.renter.Name = ReadLine();
            Write("Renter SSN : ");
            car.renter.SSN = Convert.ToInt32(ReadLine());
            Write("Renter Phone : ");
            car.renter.Phone_num = Convert.ToInt32(ReadLine());
            Write("Renter Driver License : ");
            car.renter.Driver_licence = Int32.Parse(ReadLine());

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
            if (key < '1' || key > '3')
            {
                throw new ArgumentOutOfRangeException();//exception when enter wrong will get message fo it
            }
            return key; //return it
        }

        private void Move()
        {
            if (key == '1') //here for check of choise and teleport to function of choise
            {
                Add();
            }
            else if (key == '2')
            {
                // this.Close();
                // Login login = new Login();//create object to access to login page
                // login.Show(); //to start show login page
            }
            else
            {
                reload = false; //exit from application
            }
        }

        public void Show(string username, int id) //access to home page method
        {
            this.Admin_Page(username, id);
        }


        private void Admin_Page(string username, int id)
        {
            while (reload)//here we use it if reload is false turn of this function
            {
                this.Close(); this.Print(username, id); // here for clear terminal then show home page this if page reload
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
                        Write("\nChoose Again : ");
                    }
                } while (test);
                this.Move();
            }
        }
    }
}