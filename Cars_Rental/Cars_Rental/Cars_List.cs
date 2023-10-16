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
            this.Close();
            WriteLine("# Add #\n");
            Cars car = new Cars();
            // ID set from sql last id//TODO Dalton

            // collect the date 
            Write("Brand : ");
            car.Brand = ReadLine();
            Write("Model : ");
            car.Model = ReadLine();
            Write("Year : ");
            car.Year = editor(car.Year);
            Write("\nLicense Plate No. : ");
            car.License_plate_no = ReadLine();
            Write("Price : ");
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
            car.lessor.Valid_Regstration_Papers = editor(car.lessor.Valid_Regstration_Papers);
            Write("\nCar Status : 1-Available 2-UnAvailable ");
            char ChooseStatus = ReadKey(true).KeyChar;
            while (ChooseStatus < '1' || ChooseStatus > '2')
            {
                ChooseStatus = ReadKey(true).KeyChar;
            }
            if (ChooseStatus == '1')
            {
                car.Status = "Available";
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
            } 

            //TODO Dalton : Add to SQL 
            AccessMySql userDate = new AccessMySql();

            // insert the date of lessor
            userDate.SqlQuary($"INSERT INTO client (ssn, name, phone) VALUES ({car.lessor.SSN}, '{car.lessor.Name}', {car.lessor.Phone_num}); INSERT INTO lessor (commission, registation_paper, client_id) VALUES ({car.lessor.Commission_price}, {car.lessor.Valid_Regstration_Papers}, (SELECT id FROM client WHERE ssn = {car.lessor.SSN}));");


            // insert the date of renter that is avalible 
            if (ChooseStatus == '2')
            {
                userDate.SqlQuary($"INSERT INTO client (ssn, name, phone) VALUES ({car.renter.SSN}, '{car.renter.Name}', {car.renter.Phone_num}); INSERT INTO renter(payment_method, drive_licanese, received_date, due_date, client_id, users_id) VALUES('{car.renter.payment_method}', '{ car.renter.Driver_licence}', NOW(), '{car.renter.due_date.Year}-{car.renter.due_date.Month}-{car.renter.due_date.Day}', (SELECT id FROM client WHERE ssn = { car.renter.SSN}), { useSession}); ");
                userDate.SqlQuary($"INSERT INTO car (brand, model, year, plateNumber, price, state, renter_id, lessor_id) VALUES ('{car.Brand}', '{car.Model}', {car.Year}, '{car.License_plate_no}', {car.Price}, '{car.Status}', (SELECT id FROM renter WHERE client_id IN (SELECT id From client WHERE ssn = {car.renter.SSN})),  (SELECT id From lessor where client_id IN (SELECT id From client WHERE ssn = {car.lessor.SSN})))");
            }
            else
            {
                // insert the date of car and check if it has a renter or not and then make the link 
                userDate.SqlQuary($"INSERT INTO car (brand, model, year, plateNumber, price, state, renter_id, lessor_id) VALUES ('{car.Brand}', '{car.Model}', {car.Year}, '{car.License_plate_no}', {car.Price}, '{car.Status}', NULL,  (SELECT id From lessor where client_id IN (SELECT id From client WHERE ssn = {car.lessor.SSN})))");
            }

            // print the last msg with MazenElays part
            this.Close();
            Write("\n\nThe Following Lessor has been added Successfully\n");
            Personal_Info p = car.lessor;
            WriteLine($"{p}");
            ReadKey();

        }
        private void Edit()
        {
            //TODO Dalton : Edit from & to SQL
            this.Close();
            WriteLine("# Edit #\n");
            Cars car = new Cars();
            Write("\nEnter the Car id: ");
            car.id = editor(car.id);

            int selector = 0;
            int Year = 0, Month = 0, Day = 0;

            // SQL 
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();

            if ((userDate.SqlQuary($"SELECT id FROM car WHERE id = {car.id}")).Count > 0)
            {


                // giving the date from the datebase in collect it in the object varabiles 
                if ((userDate.SqlQuary($"SELECT id FROM car WHERE id = {car.id} AND renter_id IS NOT NULL")).Count < 1)
                {
                    result = userDate.SqlQuary($"SELECT car.Brand, car.model, car.year, car.plateNumber, car.price, car.state, lessor.commission, lessor.registation_paper, c1.ssn as 'lessor SSN', c1.name as 'lessor name', c1.phone as 'lessor phone' FROM car INNER JOIN lessor ON lessor_id = lessor.id INNER JOIN client c1 ON lessor.client_id = c1.id WHERE car.id = {car.id}");
                    foreach (var items in result)
                    {
                        car.Brand = items[0];
                        car.Model = items[1];
                        car.Year = Convert.ToInt32(items[2]);
                        car.License_plate_no = items[3];
                        car.Price = Convert.ToInt32(items[4]);
                        car.Status = items[5];
                        car.lessor.Commission_price = Convert.ToDouble(items[6]);
                        car.lessor.Valid_Regstration_Papers = Convert.ToInt32(items[7]);
                        car.lessor.SSN = Convert.ToInt32(items[8]);
                        car.lessor.Name = items[9];
                        car.lessor.Phone_num = Convert.ToInt32(items[10]);
                    }
                }
                else
                {
                    result = userDate.SqlQuary($"SELECT car.Brand, car.model, car.year, car.plateNumber, car.price, car.state, renter.payment_method, renter.drive_licanese, renter.received_date, DATE_FORMAT(renter.due_date, '%Y-%m-%d'), lessor.commission, lessor.registation_paper, c1.ssn as 'lessor SSN', c1.name as 'lessor name', c1.phone as 'lessor phone', c2.ssn as 'renter SSN', c2.name as 'renter name', c2.phone as 'renter phone' FROM car INNER JOIN lessor ON lessor_id = lessor.id INNER JOIN renter ON renter_id = renter.id INNER JOIN client c1 ON lessor.client_id = c1.id INNER JOIN client c2 ON renter.client_id = c2.id WHERE car.id = {car.id}");
                    foreach (var items in result)
                    {
                        car.Brand = items[0];
                        car.Model = items[1];
                        car.Year = Convert.ToInt32(items[2]);
                        car.License_plate_no = items[3];
                        car.Price = Convert.ToInt32(items[4]);
                        car.Status = items[5];
                        car.renter.payment_method = items[6];
                        car.renter.Driver_licence = Convert.ToInt32(items[7]);

                        string str = items[9];
                        char[] spearator = { '-' };

                        String[] strlist = str.Split(spearator);

                        Year = Convert.ToInt32(strlist[0]);
                        Month = Convert.ToInt32(strlist[1]);
                        Day = Convert.ToInt32(strlist[2]);

                        car.renter.due_date = new Date(Month, Day, Year);

                        car.lessor.Commission_price = Convert.ToDouble(items[10]);
                        car.lessor.Valid_Regstration_Papers = Convert.ToInt32(items[11]);
                        car.lessor.SSN = Convert.ToInt32(items[12]);
                        car.lessor.Name = items[13];
                        car.lessor.Phone_num = Convert.ToInt32(items[14]);
                        car.renter.SSN = Convert.ToInt32(items[15]);
                        car.renter.Name = items[16];
                        car.renter.Phone_num = Convert.ToInt32(items[17]);
                    }
                }


                // TODO Dalton to display the options with value dynamicly 
                Write("\nBrand : " + car.Brand);
                car.Brand = editor(car.Brand);

                Write("\nModel : " + car.Model);
                car.Model = editor(car.Model);

                Write("\nYear : " + car.Year);
                car.Year = editor(car.Year);

                Write("\nLicense plate no : " + car.License_plate_no);
                car.License_plate_no = editor(car.License_plate_no);

                Write("\nPrice : " + car.Price);
                car.Price = editor(car.Price);


                Write("\nLessor Name : " + car.lessor.Name);
                car.lessor.Name = editor(car.lessor.Name);

                Write("\nLessor SSN : " + car.lessor.SSN);
                car.lessor.SSN = editor(car.lessor.SSN);

                Write("\nLessor phone : " + car.lessor.Phone_num);
                car.lessor.Phone_num = editor(car.lessor.Phone_num);

                Write("\nLessor Commission : " + car.lessor.Commission_price);
                car.lessor.Commission_price = editor(car.lessor.Commission_price);

                Write("\nLessor Valid_Regstration_Papers : " + car.lessor.Valid_Regstration_Papers);
                car.lessor.Valid_Regstration_Papers = editor(car.lessor.Valid_Regstration_Papers);

                Write("\nStatus : " + car.Status);
                Write("\n\tCar Status : 1-Available 2-UnAvailable ");
                char ChooseStatus = ReadKey(true).KeyChar;
                while (ChooseStatus < '1' || ChooseStatus > '2')
                {
                    ChooseStatus = ReadKey(true).KeyChar;
                }
                if (ChooseStatus == '2' && car.Status != "Available")
                {
                    selector = 1;
                    car.Status = "UNAvailable";
                    // renter part
                    Write("\nrenter Name : " + car.renter.Name);
                    car.renter.Name = editor(car.renter.Name);

                    Write("\nrenter SSN : " + car.renter.SSN);
                    car.renter.SSN = editor(car.renter.SSN);

                    Write("\nrenter phone : " + car.renter.Phone_num);
                    car.renter.Phone_num = car.renter.Phone_num;

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


                    Write("\ndue date : " + car.renter.due_date);
                    Write("\n\tYear" + car.renter.due_date.Year);
                    Year = editor(car.renter.due_date.Year);

                    Write("\n\tMonth" + car.renter.due_date.Month);
                    Month = editor(car.renter.due_date.Month);

                    Write("\n\tDay" + car.renter.due_date.Day);
                    Day = editor(car.renter.due_date.Day);

                    car.renter.due_date = new Date(Month, Day, Year);
                }
                else if (ChooseStatus == '2' && car.Status == "Available")
                {
                    selector = 2;
                    car.Status = "UnAvailable";

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

                    Write("\nRenter Driver License : ");
                    car.renter.Driver_licence = editor(car.renter.Driver_licence);

                    Year = 0;
                    Month = 0;
                    Day = 0;
                    Write("\ndue_date : ");
                    Write("\n\tYear: ");
                    Year = editor(Year);
                    Write("\n\tMonth: ");
                    Month = editor(Month);
                    Write("\n\tDay: ");
                    Day = editor(Day);
                    car.renter.due_date = new Date(Month, Day, Year);
                }
                else
                {
                    car.Status = "Available";
                    userDate.SqlQuary($"UPDATE car SET renter_id = NULL WHERE id = {car.id}; DELETE FROM renter WHERE id IN(SELECT renter_id FROM car WHERE id = {car.id}); DELETE FROM client WHERE id IN(SELECT client_id FROM renter WHERE id IN(SELECT renter_id FROM car WHERE id = {car.id}))");

                }


                // UPDATE the date of lessor 
                userDate.SqlQuary($"UPDATE client SET ssn = {car.lessor.SSN}, name = '{car.lessor.Name}', phone = {car.lessor.Phone_num} WHERE id IN (SELECT client_id FROM lessor WHERE id IN (SELECT lessor_id FROM car WHERE id = {car.id})); UPDATE lessor SET commission = {car.lessor.Commission_price}, registation_paper = {car.lessor.Valid_Regstration_Papers} WHERE id IN (SELECT lessor_id FROM car WHERE id = {car.id});");

                // UPDATE the date of renter 
                if (selector == 1)
                {
                    userDate.SqlQuary($"UPDATE client SET ssn = {car.renter.SSN}, name = '{car.renter.Name}', phone = {car.renter.Phone_num} WHERE id IN (SELECT client_id FROM renter WHERE id IN (SELECT renter_id FROM car WHERE id = {car.id})); UPDATE renter SET payment_method = '{car.renter.payment_method}', drive_licanese = '{ car.renter.Driver_licence}', due_date = '{car.renter.due_date.Year}-{car.renter.due_date.Month}-{car.renter.due_date.Day}', users_id = { useSession} WHERE id IN (SELECT renter_id FROM car WHERE id = {car.id});");
                }
                // INTERT the date of renter if the car has new information about renter 
                else if (selector == 2)
                {
                    userDate.SqlQuary($"INSERT INTO client (ssn, name, phone) VALUES ({car.renter.SSN}, '{car.renter.Name}', {car.renter.Phone_num}); INSERT INTO renter(payment_method, drive_licanese, received_date, due_date, client_id, users_id) VALUES('{car.renter.payment_method}', '{ car.renter.Driver_licence}', NOW(), '{car.renter.due_date.Year}-{car.renter.due_date.Month}-{car.renter.due_date.Day}', (SELECT id FROM client WHERE ssn = { car.renter.SSN}), { useSession}); UPDATE car SET renter_id = (SELECT id FROM renter WHERE client_id IN (SELECT id FROM client WHERE ssn = {car.renter.SSN})) WHERE id = {car.id}");
                }
                // UPATE the car info
                userDate.SqlQuary($"UPDATE car SET brand = '{car.Brand}', model = '{car.Model}', year = {car.Year}, plateNumber = '{car.License_plate_no}', price = {car.Price}, state = '{car.Status}' WHERE id = {car.id};");

                Write("\n\nEdit is done Successfully");
                ReadKey();
            }
            else
            {
                this.Close();
                WriteLine("\n\nThis Car not found..Press any key to back ");
                ReadKey();
            }
        }
        private void Delete()
        {
            //TODO Dalton : Delete from SQl
            this.Close();
            WriteLine("# Delete #\n");
            Cars car = new Cars();
            Write("\nEnter the Car id: ");
            car.id = editor(car.id);

            // SQL 
            AccessMySql userDate = new AccessMySql();

            if ((userDate.SqlQuary($"SELECT id FROM car WHERE id = {car.id}")).Count > 0)
            {
                // Select the ssn of renter or lessor 
                car.renter.SSN = Convert.ToInt32(userDate.SqlQuary($"SELECT ssn FROM client WHERE id IN (SELECT client_id FROM renter WHERE id IN (SELECT renter_id FROM car WHERE id= {car.id}))")[0][0]);
                car.lessor.SSN = Convert.ToInt32(userDate.SqlQuary($"SELECT ssn FROM client WHERE id IN (SELECT client_id FROM lessor WHERE id IN (SELECT lessor_id FROM car WHERE id= {car.id}))")[0][0]);


                // delete the date of cline lessor and renter and the car information
                userDate.SqlQuary($"DELETE FROM car WHERE id = 2; DELETE FROM lessor WHERE client_id IN(SELECT id FROM client WHERE ssn = { car.lessor.SSN});DELETE FROM client WHERE ssn = { car.lessor.SSN};DELETE FROM renter WHERE client_id IN (SELECT id FROM client WHERE ssn = { car.renter.SSN}); DELETE FROM client WHERE ssn = { car.renter.SSN};");

                Write("\n\nDelete is done Successfully");
                ReadKey();
            }
            else
            {
                this.Close();
                WriteLine("\n\nThis Car not found..Press any key to back ");
                ReadKey();
            }
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
