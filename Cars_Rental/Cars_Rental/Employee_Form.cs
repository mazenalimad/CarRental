using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Cars_Rental
{
    public class Employee_Form : Console_Print
    {
        private void Print(string username, int id)
        {
            WriteLine("# Employee - Cars List #");
            WriteLine("Username : " + username);
            WriteLine("ID Account : " + id + "");
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

            /*
            WriteLine("Enter ID of car you want add renter for it : ");
            int ID=0;
            ID = editor(ID);
            if (ID == Cars.id && Cars.status == "Available") // here just example how it works
            {
                Write("\nRenter Name : ");
                car.renter.Name = ReadLine(); ;

                Write("\nRenter SSN : ");
                car.renter.SSN = editor(car.renter.SSN);

                Write("\nRenter Phone : ");
                car.renter.Phone_num = editor(car.renter.Phone_num);

                Write("\nRenter Driver License : ");
                car.renter.Driver_licence = editor(car.renter.Driver_licence);
                
                WriteLine("\nPress Enter to confirm.. Or any key to cancel ")
                if(ReadKey(true).KeyChar == 13)
                {
                    //here send every new details of renter to car
                    Cars.status = "UnAvailable";
                }
            }
            else
            {
                WriteLine("This Car not found..Press any key to back ");
                ReadKey();
            }

            */

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

        public void Show(string username, int id) //access to home page method
        {
            this.Employee_Page(username, id);
        }


        private void Employee_Page(string username, int id)
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
                        Write("\rChoose Again : ");
                    }
                } while (test);
                this.Move();
            }
        }
    }
}
