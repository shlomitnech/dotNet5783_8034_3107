using Dal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static DO.Enums;

using DO;
namespace Dal;

internal class DalTest
{
    static void Main()
    {
        Product product = new Product();
        int num1, num2;
        string response1, response2;

            Console.WriteLine("To edit the products, press 1 \n"
                + "To edit orders, press 2 \n"
                + "To edit order items press 3 \n"
                + "To exit the program, press 0"
                );
        response1 = Console.ReadLine();
        num1 = Convert.ToInt32(response1);
        while (num1 > 3)
            Console.WriteLine("Please click a number 0-3");
        Enums.Type type = (Enums.Type)num1; 
        while (num1 != 0) //while user input is not 0
        {
            Console.WriteLine("To add, press 1 \n"
              + "To view one, press 2 \n"
              + "to view the whole list, press 3 \n"
              + "to delete, press 4 \n"
              + "to exit, press 0 \n"
              );


            if (num2 ==0)
            Enums.Action action = (Enums.Action)num2;

            switch (type, action)
            {
                case(Enums.Type)

            }
        }
            


    }
}