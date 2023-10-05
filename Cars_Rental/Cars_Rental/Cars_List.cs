using System;
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
            WriteLine("# Admin - Cars List #");
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
            Cars car = new Cars();
            // ID set from sql last id + 1 //TODO Dalton
            Write("Brand : ");
            car.Brand = ReadLine();
            Write("Model : ");
            car.Model = ReadLine();
            Write("Year : ");
            car.Year = editor(car.Year);
            Write("\nLicense Plate No. : ");
            car.License_plate_no = editor(car.License_plate_no);
            Write("\nPrice : ");
            car.Price = editor(car.Price);
            Write("\nLessor Name : ");
            car.lessor.Name = ReadLine();
            Write("Lessor SSN : ");
            car.lessor.SSN = editor(car.lessor.SSN);
            Write("\nLessor Phone : ");
            car.lessor.Phone_num = editor(car.lessor.Phone_num);
            Write("\nLessor Commission : " + car.lessor.Commission_price);
            car.lessor.Commission_price = editor(car.lessor.Commission_price);
            Write("\nCar Status : 1-Available 2-UnAvailable ");
            char ChooseStatus = ReadKey(true).KeyChar;
            while(ChooseStatus < '1' || ChooseStatus > '2')
            {
                ChooseStatus = ReadKey(true).KeyChar;
            }
            if (ChooseStatus == '1')
            {
                car.renter.Name = "Null";
                car.renter.SSN = 0;
                car.renter.Phone_num = 0;
                car.renter.Driver_licence = 0;
            }
            else
            {
                car.Status = "UnAvailable";

                Write("\nRenter Name : ");
                car.renter.Name = ReadLine(); ;

                Write("\nRenter SSN : ");
                car.renter.SSN = editor(car.renter.SSN);

                Write("\nRenter Phone : ");
                car.renter.Phone_num = editor(car.renter.Phone_num);

                Write("\nRenter Driver License : ");
                car.renter.Driver_licence = editor(car.renter.Driver_licence);
            }

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
                        Write("\rChoose Again : ");
                    }
                } while (test);
                this.Move();
            }
        }
    }
}
