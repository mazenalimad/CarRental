using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    public class Console_Print
    {
        protected char key; //key for choise
        protected bool test; //bool for if the user choise invalid can do it again
        protected bool reload = true; //for escape from stackoverflow logic error
        protected static int useSession = 0;
        protected void Close()
        {
            Clear(); //for close page 
        }
        protected char Press_check() //for choise without press enter
        {
            return ReadKey(true).KeyChar; // true here for the key entered not be written in console
        }
        protected double editor(double edit)
        {
            char key;
            string editstring = Convert.ToString(edit);
            do //this loop for every key user enter it add to password string
            {
                key = ReadKey(true).KeyChar; //here to read key the user press it and not write it in console

                if (key == 13) //if user press "Enter" stop the loop
                {
                    break;
                }
                else if (key > '9' || key < '0')
                {
                    //if this condition is true don't do anything
                }
                // Backspace Should Not Work
                else if (key != 8) //if user not use backspace to remove do this
                {
                    if (editstring.Length >= 4)
                    {
                    }
                    else
                    {
                        editstring += key; // add the key user pressed to password string
                        Write(key);
                    }
                }
                if (key == 8)
                {
                    if (editstring.Length > 2) //if password length = 0 do not this
                    {
                        editstring = editstring.Remove(editstring.Length - 1); //remove last char in string 
                        Write("\b \b"); //backspace once then replace with space then backspace again to make it like user remove chat
                    }
                }
            } while (key != 13); // Stops Receving Keys Once Enter is Pressed
            return Convert.ToDouble(editstring);
        }
        protected int editor(int edit)
        {
            char key;
            string editstring = Convert.ToString(edit);
            do //this loop for every key user enter it add to password string
            {
                key = ReadKey(true).KeyChar; //here to read key the user press it and not write it in console

                if (key == 13) //if user press "Enter" stop the loop
                {
                    break;
                }
                else if (key > '9' || key < '0')
                {
                    //if this condition is true don't do anything
                }
                // Backspace Should Not Work
                else if (key != 8) //if user not use backspace to remove do this
                {
                    if (editstring.Length <= 9)
                    {
                        editstring += key; // add the key user pressed to password string
                        Write(key);
                    }
                }
                if (key == 8)
                {
                    if (editstring.Length > 0) //if password length = 0 do not this
                    {
                        editstring = editstring.Remove(editstring.Length - 1); //remove last char in string 
                        Write("\b \b"); //backspace once then replace with space then backspace again to make it like user remove chat
                    }
                }
            } while (key != 13); // Stops Receving Keys Once Enter is Pressed
            return Convert.ToInt32(editstring);
        }
        protected string editor(string edit)
        {
            char key;
            string editstring = Convert.ToString(edit);
            do //this loop for every key user enter it add to password string
            {
                key = ReadKey(true).KeyChar; //here to read key the user press it and not write it in console

                if (key == 13) //if user press "Enter" stop the loop
                {
                    break;
                }
                // Backspace Should Not Work
                else if (key != 8) //if user not use backspace to remove do this
                {
                    editstring += key; // add the key user pressed to password string
                    Write(key);
                }
                if (key == 8)
                {
                    if (editstring.Length > 0) //if password length = 0 do not this
                    {
                        editstring = editstring.Remove(editstring.Length - 1); //remove last char in string 
                        Write("\b \b"); //backspace once then replace with space then backspace again to make it like user remove chat
                    }
                }
            } while (key != 13); // Stops Receving Keys Once Enter is Pressed
            return editstring;
        }

        protected long editor(long edit)
        {
            char key;
            string editstring = Convert.ToString(edit);
            do //this loop for every key user enter it add to password string
            {
                key = ReadKey(true).KeyChar; //here to read key the user press it and not write it in console

                if (key == 13) //if user press "Enter" stop the loop
                {
                    break;
                }
                else if (key > '9' || key < '0')
                {
                    //if this condition is true don't do anything
                }
                // Backspace Should Not Work
                else if (key != 8) //if user not use backspace to remove do this
                {
                        editstring += key; // add the key user pressed to password string
                        Write(key);
                }
                if (key == 8)
                {
                    if (editstring.Length > 0) //if password length = 0 do not this
                    {
                        editstring = editstring.Remove(editstring.Length - 1); //remove last char in string 
                        Write("\b \b"); //backspace once then replace with space then backspace again to make it like user remove chat
                    }
                }
            } while (key != 13); // Stops Receving Keys Once Enter is Pressed
            return (long)Convert.ToDouble(editstring);
        }
    }
}
