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
        private void Print(string username)
        {
            WriteLine("# Employee - Cars List #");
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
            Cars car = new Cars();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            WriteLine("Enter ID of car you want add renter for it : ");
            int id = 0;
            id = editor(id);

            result = userDate.SqlQuary($"SELECT state FROM car WHERE id = {id}");

            car.Status = result[0][0];

            if (car.Status == "Available") // here just example how it works
            {
                Write("\nRenter Name : ");
                car.renter.Name = ReadLine(); ;

                Write("\nRenter SSN : ");
                car.renter.SSN = editor(car.renter.SSN);

                Write("\nRenter Phone : ");
                car.renter.Phone_num = editor(car.renter.Phone_num);

                if (car.renter.GetPayment_meth() == 2)
                {
                    Write("\nRenter credit card number : ");
                    if (credit((long)Convert.ToDouble(ReadLine())))
                    {
                        car.renter.payment_method = "credit card";
                    }
                }
                else
                {
                    car.renter.payment_method = "Cash";
                }

                Write("\nRenter Driver License : ");
                car.renter.Driver_licence = editor(car.renter.Driver_licence);


                Write("\ndue_date YYYY-MM-DD : ");
                car.renter.due_date = ReadLine();


                WriteLine("\nPress Enter to confirm.. Or any key to cancel ");
                if(ReadKey(true).KeyChar == 13)
                {
                    //here send every new details of renter to car
                    car.Status = "UnAvailable";
                }

                // TODO Dalton : Add renter to the car 
                result = userDate.SqlQuary($"INSERT INTO client (ssn, name, phone) VALUES ({car.renter.SSN}, {car.renter.Name}, {car.renter.Phone_num}); INSERT INTO renter(payment_method, drive_licanece, received_date, due_date, clint_id, users_id) VALUES({ car.renter.payment_method}, { car.renter.Driver_licence}, NOW(), { car.renter.due_date}, (SELECT id FROM client WHERE ssn = { car.renter.SSN}), { useSession}); ");
                result = userDate.SqlQuary($"UPDATE car SET renter_id = (SELECT id FROM renter WHERE client_id IN (SELECT id FROM client WHERE id = {car.renter.SSN})) WHERE id = {id}");

                this.Close();
                Write("\n\nEdition is done Successfully");
                ReadKey();
            }
            else
            {
                WriteLine("This Car not found..Press any key to back ");
                ReadKey();
            }
        }
        private void Edit()
        {
            //TODO Dalton : Edit from & to SQL
            this.Close();
            WriteLine("# Edit #\n");
            Cars car = new Cars();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            WriteLine("Enter ID of car you want Edit renter for it : ");
            int id = 0;
            id = editor(id);

            result = userDate.SqlQuary($"SELECT * FROM renter INNER JOIN client ON renter.client_id = client.id WHERE renter.id IN (SELECT renter_id FROM car WHERE id = {id})");

            if(result.Count > 0)
            {
                foreach (var items in result)
                {
                    car.renter.payment_method = items[1];
                    car.renter.Driver_licence = Convert.ToInt32(items[2]);
                    car.renter.due_date = items[4];
                    car.renter.SSN = Convert.ToInt32(items[8]);
                    car.renter.Name = items[9];
                    car.renter.Phone_num = Convert.ToInt32(items[10]);


                    //TODO Dark doing dynamic display




                    result = userDate.SqlQuary($"UPDATE client SET ssn = {car.renter.SSN}, name = {car.renter.Name}, phone = {car.renter.Phone_num} WHERE ssn = {items[7]}; UPDATE renter SET payment_method = {car.renter.payment_method}, drive_licanece = {car.renter.Driver_licence}, due_date = { car.renter.due_date}, client_id = {items[7]}, users_id = {useSession} WHERE id = {items[0]}");

                    this.Close();
                    Write("\n\nEdition is done Successfully");
                    ReadKey();
                }
            }else
            {
                WriteLine("This Car not found or there are note any renter..Press any key to back ");
                ReadKey();
            }
        }
        private void Delete()
        {
            //TODO Dalton : Delete from SQl
            this.Close();
            WriteLine("# Delete #\n");
            Cars car = new Cars();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            WriteLine("Enter ID of car you want Edit renter for it : ");
            int id = 0;
            id = editor(id);


            result = userDate.SqlQuary($"DELETE FROM client WHERE id IN (SELECT client_id FROM renter WHERE id IN (SELECT renter_id FROM car WHERE id = {id})); DELETE FROM renter WHERE id IN (SELECT renter_id FROM car WHERE id = {id}));");
            result = userDate.SqlQuary($"UPDATE car SET renter = NULL WHERE id = {id}");

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
                Edit();
            }
            else if (key == '4')
            {
                Delete();
            }
            else
            {
                reload = false; //exit from application
            }
        }


        private bool credit(long creditNum)
        {
            Credit Check = new Credit();
            return Check.CreditNumber(creditNum);
        }

        public void Show(string username) //access to home page method
        {
            this.Employee_Page(username);
        }


        private void Employee_Page(string username)
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
