using System;
using System.Linq;
using TestProjrct.Enums;
using TestProjrct.infrastructure.Services;
using TestProjrct.Models;


namespace TestProjrct
{
    public sealed class SnackMachine
    {
        private static readonly IPaymentService PaymentService = new PaymentService();
        private static readonly IPresentationService PresentationService = new PresentationService();
        private static VendingMachines VendingMachines = new VendingMachines();

        [Obsolete]
        public static void Main(string[] args)
        {
            Console.WriteLine("Vending Machines is running");
            
            while (true)
            {
                PresentationService.ViewData(VendingMachines.Snacks);
                Console.WriteLine("Lets buy something, press enter to start :)");
                string key = Console.ReadKey().Key.ToString();
                if (key == "Enter")
                {
                    while (true)
                    {
                        Console.WriteLine("choose your snack by number: ");
                        if (int.TryParse(Console.ReadLine(), out var snackId))
                        {
                            if (snackId == 0)
                            {
                                Console.WriteLine("No item with this number or all quantity sold out, try again from numbers in table");
                                continue;
                            }

                            var snack = VendingMachines.Snacks.FirstOrDefault(x=>x.Id == snackId);
                            if (snack == null)
                            {
                                Console.WriteLine("No item with this number, try again from numbers in table");
                                continue;
                            }
                            if (snack.Count == 0)
                            {
                                Console.WriteLine("all quantity sold out, try again with another snack in the table");
                                continue;
                            }

                            Console.WriteLine("Wow it is delicious choice ;)");
                            Console.WriteLine($"The Price is: {snack.Price} $, How do you want to pay? (Choose the number)");
                            Console.WriteLine("1- Coins");
                            Console.WriteLine("2- Notes");
                            Console.WriteLine("3- Card");
                            Console.WriteLine("-----------");
                            Console.WriteLine("4- Cancel");
                            if (int.TryParse(Console.ReadLine(), out int moneyType))
                            {
                                if (moneyType == 4)
                                {
                                    Console.WriteLine("The process canceled :(");
                                    System.Threading.Thread.Sleep(3000);
                                    break;
                                }
                                var isSucceeded  = PaymentService.Pay((MoneyTypes)moneyType, snack);
                                if (isSucceeded)
                                {
                                    VendingMachines.Money += snack.Price;
                                }
                                else
                                {
                                    Console.WriteLine("The process canceled :(");
                                    System.Threading.Thread.Sleep(3000);
                                    break;
                                }
                            }


                            Console.WriteLine("Bon Appetite :)");
                            System.Threading.Thread.Sleep(3000);
                            break;
                        }
                    }

                    
                }

                Console.Clear();
            }
        }
    }



}

