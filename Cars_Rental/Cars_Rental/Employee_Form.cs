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


            result = userDate.SqlQuary($"SELECT car.id, car.Brand, car.model, car.year, car.plateNumber, car.price, car.state, COALESCE(c1.name , 'Non renter') as renter, c2.name as lessor FROM car LEFT JOIN renter ON renter_id = renter.id LEFT JOIN client c1 ON renter.client_id = c1.id INNER JOIN lessor ON lessor_id = lessor.id INNER JOIN client c2 ON lessor.client_id = c2.id;");

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

            // collect the date from users into varabiles
            this.Close();
            WriteLine("# Add #\n");
            Cars car = new Cars();
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            Write("Enter ID of car you want add renter for it : ");
            car.id = editor(car.id);

            result = userDate.SqlQuary($"SELECT state FROM car WHERE id = {car.id}");

            if (result.Count > 0)
            {
                car.Status = result[0][0];

                if (car.Status == "Available") // here just example how it works
                {
                    Write("\nRenter Name : ");
                    car.renter.Name = ReadLine(); ;

                    Write("Renter SSN : ");
                    car.renter.SSN = editor(car.renter.SSN);

                    Write("\nRenter Phone : ");
                    car.renter.Phone_num = editor(car.renter.Phone_num);


                    if (car.renter.GetPayment_meth() == '2')
                    {
                        Write("\n\tRenter credit card number : ");
                        bool CCK = credit((long)Convert.ToDouble(ReadLine()));
                        while (!CCK)
                        {
                            Write("\nThe card number is invalid ...try again");
                            Write("\n\tRenter credit card number : ");
                            CCK = credit((long)Convert.ToDouble(ReadLine()));
                        }
                        car.renter.payment_method = "credit card";
                    }
                    else
                    {
                        car.renter.payment_method = "Cash";
                    }

                    Write("\nRenter Driver License : ");
                    car.renter.Driver_licence = editor(car.renter.Driver_licence);


                    int Year = 0, Month = 0, Day = 0;
                    Write("\ndue_date : ");
                    Write("\n\tYear: ");
                    Year = editor(Year);
                    Write("\n\tMonth: ");
                    Month = editor(Month);
                    Write("\n\tDay: ");
                    Day = editor(Day);
                    car.renter.due_date = new Date(Month, Day, Year);


                  
                    //here send every new details of renter to car
                    car.Status = "UnAvailable";
                    

                    // TODO Dalton : Add renter to the car 
                    // INSERT a new renter 
                    userDate.SqlQuary($"INSERT INTO client (ssn, name, phone) VALUES ({car.renter.SSN}, '{car.renter.Name}', {car.renter.Phone_num}); INSERT INTO renter(payment_method, drive_licanese, received_date, due_date, client_id, users_id) VALUES('{car.renter.payment_method}', '{car.renter.Driver_licence}', NOW(), '{car.renter.due_date.Year}-{car.renter.due_date.Month}-{car.renter.due_date.Day}' , (SELECT id FROM client WHERE ssn = {car.renter.SSN}), {useSession});");
                    // UPDATE the date of car renter to make releation between car and renter in the datebase Entity type
                    userDate.SqlQuary($"UPDATE car SET renter_id = (SELECT id FROM renter WHERE client_id IN (SELECT id FROM client WHERE ssn = {car.renter.SSN})) WHERE id = {car.id}");

                    this.Close();
                    Write("\n\nThe Following Renter has been added successfully\n");
                    WriteLine($"{"Name",10}  {"SSN",10} {"Phone",15} {"Driver_licence ", 17} {"Payment_method",19}");
                    Personal_Info p = car.renter;
                    WriteLine($"{p}");
                    ReadKey();
                }
                else
                {
                    // faild msg if the car is avaible u can't add another renter
                    this.Close(); 
                    WriteLine("\n\nThis Car is Available delete the renter first..Press any key to back ");
                    ReadKey();
                }
            }else
            {
                // faild msg if the car is not found
                this.Close(); 
                WriteLine("\n\nThis Car is not found..Press any key to back ");
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

            Write("Enter ID of car you want Edit renter for it : ");
            car.id = editor(car.id);
            int Year = 0, Month = 0, Day = 0;

            // givig the date and collect it inside varabiles 
            result = userDate.SqlQuary($"SELECT renter.id, renter.payment_method, renter.drive_licanese, DATE_FORMAT(renter.due_date, '%Y-%m-%d'), client.id ,client.SSN, client.name, client.phone  FROM renter INNER JOIN client ON renter.client_id = client.id WHERE renter.id IN (SELECT renter_id FROM car WHERE id = {car.id});");

            if(result.Count > 0)
            {
                foreach (var items in result)
                {
                    int renterID = Convert.ToInt32(items[0]);
                    car.renter.payment_method = items[1];
                    car.renter.Driver_licence = Convert.ToInt32(items[2]);

                    string str = items[3];
                    char[] spearator = { '-' };

                    String[] strlist = str.Split(spearator);

                    Year = Convert.ToInt32(strlist[0]);
                    Month = Convert.ToInt32(strlist[1]);
                    Day = Convert.ToInt32(strlist[2]);

                    car.renter.due_date = new Date(Month, Day, Year);

                    int clientID = Convert.ToInt32(items[4]);
                    car.renter.SSN = Convert.ToInt32(items[5]);
                    car.renter.Name = items[6];
                    car.renter.Phone_num = Convert.ToInt32(items[7]);


                    //TODO dalton doing dynamic display
                    // renter part
                    Write("\nrenter Name : " + car.renter.Name);
                    car.renter.Name = editor(car.renter.Name);

                    Write("\nrenter SSN : " + car.renter.SSN);
                    car.renter.SSN = editor(car.renter.SSN);

                    Write("\nrenter phone : " + car.renter.Phone_num);
                    car.renter.Phone_num = editor(car.renter.Phone_num);

                    Write("\npayment method : " + car.renter.payment_method);
                    if (car.renter.GetPayment_meth() == '2')
                    {
                        Write("\n\tRenter credit card number : ");
                        bool CCK = credit((long)Convert.ToDouble(ReadLine()));
                        while (!CCK)
                        {
                            // 4003600000000014
                            Write("\nThe card number is invalid ...try again");
                            Write("\n\tRenter credit card number : ");
                            CCK = credit((long)Convert.ToDouble(ReadLine()));
                        }
                        car.renter.payment_method = "credit card";
                    }
                    else
                    {
                        car.renter.payment_method = "Cash";
                    }

                    Write("\nDriver_licence : " + car.renter.Driver_licence);
                    car.renter.Driver_licence = editor(car.renter.Driver_licence);


                    Write("\ndue date : ");
                    Write("\n\tYear" + car.renter.due_date.Year);
                    Year = editor(car.renter.due_date.Year);

                    Write("\n\tMonth" + car.renter.due_date.Month);
                    Month = editor(car.renter.due_date.Month);

                    Write("\n\tDay" + car.renter.due_date.Day);
                    Day = editor(car.renter.due_date.Day);

                    car.renter.due_date = new Date(Month, Day, Year);

                    // UPDATE the date of renter 
                    userDate.SqlQuary($"UPDATE client SET ssn = {car.renter.SSN}, name = '{car.renter.Name}', phone = {car.renter.Phone_num} WHERE id = {clientID}; UPDATE renter SET payment_method = '{car.renter.payment_method}', drive_licanese = '{car.renter.Driver_licence}', due_date = '{car.renter.due_date.Year}-{car.renter.due_date.Month}-{car.renter.due_date.Day}', users_id = {useSession} WHERE id = {renterID}");

                    this.Close();
                    Write("\n\nEdition is done Successfully");
                    ReadKey();
                }
            }else
            {
                // faild msg that the car has'not renter to update the date 
                this.Close();
                WriteLine("\n\nThis Car not found or there are note any renter..Press any key to back ");
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

            Write("Enter ID of car you want Edit renter for it : ");
            car.id = editor(car.id);

            result = userDate.SqlQuary($"SELECT state FROM car WHERE id = {car.id}");

            if (result.Count > 0)
            {
                if (result[0][0] != "Available")
                {
                    // DELETE the date of renter and UPDATE the releation between renter and car Entity type
                    userDate.SqlQuary($"UPDATE car SET renter_id = NULL, state = 'Available'  WHERE id = {car.id}; DELETE FROM renter WHERE id IN (SELECT renter_id FROM car WHERE id = {car.id}); DELETE FROM client WHERE id IN (SELECT client_id FROM renter WHERE id IN (SELECT renter_id FROM car WHERE id = {car.id}));");

                    this.Close();
                    Write("\n\nDelete is done Successfully");
                    ReadKey();
                }
                else
                {
                    this.Close();
                    WriteLine("\n\nThis Car already Availble..Press any key to back ");
                    ReadKey();
                }
            }
            else
            {
                // faild msg that the car has'not renter to update the date 
                this.Close();
                WriteLine("\n\nThis Car not found or there are note any renter..Press any key to back ");
                ReadKey();
            }
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
