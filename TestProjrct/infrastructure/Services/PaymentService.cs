using System;
using TestProjrct.Enums;
using TestProjrct.Models;

namespace TestProjrct.infrastructure.Services
{
    public interface IPaymentService
    {
        bool Pay(MoneyTypes type, Snack snack);
    }

    public class PaymentService: IPaymentService
    {
        public PaymentService() { }

        public bool Pay(MoneyTypes type, Snack snack)
        {
            float amount = 0;
            Console.WriteLine($"The {type} slot opened");
            if (type == MoneyTypes.Coin)
            {
                amount = GetCoins(snack.Price * 100);

                if (amount == 0)
                    return false;// the process canceled

                float change = amount - (snack.Price * 100);

                if (change > 0)
                    ReturnTheChange(change/100);

                DispensesSnack(snack);
            }

            if (type == MoneyTypes.Note)
            {
                amount = GetNote(snack.Price);
                if (amount == 0)
                    return false;// the process canceled

                float change = amount - snack.Price;

                if (change > 0)
                    ReturnTheChange(change);

                DispensesSnack(snack);
            }

            if (type == MoneyTypes.Card)
            {
                amount = GetCard(snack.Price);

                DispensesSnack(snack);
            }

            return true;
        }

        private float GetCoins(float price)
        {
            float amount = 0;


            Console.WriteLine("We just accepts 10 cent, 20 cent, 50 cent or 1$, pls put the number of the amount of money:");
            Console.WriteLine("1- 10 cent");
            Console.WriteLine("2- 20 cent");
            Console.WriteLine("3- 50 cent");
            Console.WriteLine("4- 1$");
            Console.WriteLine("----------");
            Console.WriteLine("5- Enough");
            Console.WriteLine("6- Cancel");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int money))
                {
                    if (money == 5)
                    {
                        if (amount < price)
                        {
                            Console.WriteLine("Your credit not enough, add more");
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (money == 6)
                    {
                        Console.WriteLine($"You have {amount} cent in cash back slot, pls take it");
                        return 0;
                    }
                    Console.WriteLine("Checking the if coin is USD ... Done!");
                    Console.WriteLine("Checking the if coin is valid ... Done!");
                    System.Threading.Thread.Sleep(500);// some logic here to scan and check if the coin is counterfeit or not
                    switch (money)
                    {
                        case 1:
                            amount += 10;
                            break;
                        case 2:
                            amount += 20;
                            break;
                        case 3:
                            amount += 50;
                            break;
                        case 4:
                            amount += 100;
                            break;
                    }


                    Console.WriteLine($"Your credit: {amount/100}$");
                }
            }
            Console.WriteLine($"Cash accepted,your credit is {amount / 100}$");
            return amount;
        }

        private float GetNote(float price)
        {
            float amount = 0;


            Console.WriteLine("We just accepts 20$ or 50$, pls put the amount of money:");

            Console.WriteLine("1- 20$");
            Console.WriteLine("2- 50$");
            Console.WriteLine("----------");
            Console.WriteLine("3- Enough");
            Console.WriteLine("4- Cancel");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int money))
                {
                    if (money == 3)
                    {
                        if (amount < price)
                        {
                            Console.WriteLine("Your credit not enough, add more");
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (money == 4)
                    {
                        Console.WriteLine($"You have {amount}$ in cash back slot, pls take it");
                        return 0;
                    }
                    Console.WriteLine("Checking if the note is USD ... Done!");
                    Console.WriteLine("Checking if the note is valid ... Done!");
                    System.Threading.Thread.Sleep(500);// some logic here to scan and check if the cash is counterfeit or not
                    switch (money)
                    {
                        case 1:
                            amount += 20;
                            break;
                        case 2:
                            amount += 50;
                            break;
                    }


                    Console.WriteLine($"Your credit: {amount}$");
                }
            }
            Console.WriteLine($"Cash accepted,your credit is {amount}$");

            return amount;
        }

        private float GetCard(float price)
        {
            Console.WriteLine($"The price is {price}, pls put your bank password to dispense: ");
            var pass = Console.ReadLine();
            Console.WriteLine($"We will take {price}$, checking with bank ...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"Process succeeded :)");

            return price;
        }


        private void ReturnTheChange(float change)
        {
            Console.WriteLine($"You have {change}$ in cash back slot, pls take it");
        }

        private void DispensesSnack(Snack snack)
        {
            snack.Count--;
            Console.WriteLine($"Take your snack {snack.Name} from snack slot");
        }
    }
}
