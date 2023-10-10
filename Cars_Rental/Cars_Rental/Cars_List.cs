using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditChecker;
using static System.Console;

namespace Cars_Rental
{
    class Cars_List : Console_Print
    {
        private void Print(string username)
        {
            WriteLine("# Admin - Cars List #");
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
            this.Close();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();


            result = userDate.SqlQuary($"SELECT car.id, car.Brand, car.model, car.year, car.plateNumber, car.price, car.state, c1.name as renter, c2.name as lessor FROM car INNER JOIN renter ON renter_id = renter.id INNER JOIN client c1 ON renter.client_id = c1.id INNER JOIN lessor ON lessor_id = lessor.id INNER JOIN client c2 ON lessor.client_id = c2.id");

            foreach (var items in result)
            {
                foreach (var item in items)
                    Console.Write($"{item}\t");
                Console.Write("\n");
            }
            Console.WriteLine("\n\n\nPress any key to go back");
            Console.ReadKey();
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
            Write("\nLessor Valid_Regstration_Papers : ");
            car.lessor.Valid_Regstration_Papers = car.lessor.Valid_Regstration_Papers;
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
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            result = userDate.SqlQuary($"INSERT INTO client (ssn, name, phone) VALUES ({car.lessor.SSN}, {car.lessor.Name}, {car.lessor.Phone_num})");
            result = userDate.SqlQuary($"INSERT INTO lessor (commission, registation, client_id) VALUES ({car.lessor.Commission_price}, {car.lessor.Valid_Regstration_Papers}, (SELECT id FROM users WHERE ssn = {car.lessor.SSN}))");

            if (ChooseStatus != '1')
            {
                result = userDate.SqlQuary($"INSERT INTO client (ssn, name, phone) VALUES ({car.renter.SSN}, {car.renter.Name}, {car.renter.Phone_num})");
                //result = userDate.SqlQuary($"INSERT INTO renter(payment_method, drive_licanece, received_date, due_date, clint_id, users_id) VALUES (, {useSession})");
            }
            result = userDate.SqlQuary($"INSERT INTO client (ssn, name, phone) VALUES ({car.lessor.SSN}, {car.lessor.Name}, {car.lessor.Phone_num})");


        }
        private void Edit()
        {
            //TODO Dalton : Edit from & to SQL
        }
        private void Delete()
        {
            //TODO Dalton : Delete from SQl
        }


        // method to check if the number of credit card is correct or not [ VISA , MASTERCARD , AMEX ]
        private bool credit (long creditNum)
        {
            Credit Check = new Credit();
            return Check.CreditNumber(creditNum);
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
