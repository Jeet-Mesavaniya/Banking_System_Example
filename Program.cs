using System;

// Abstract class representing a bank account
public abstract class BankAccount
{
    // Properties
    public string AccountNumber { get; }
    public string OwnerName { get; }
    public decimal Balance { get; protected set; }

    // Constructor
    public BankAccount(string accountNumber, string ownerName)
    {
        AccountNumber = accountNumber;
        OwnerName = ownerName;
        Balance = 0;
    }

    // Abstract method for depositing funds
    public abstract void Deposit(decimal amount);

    // Abstract method for withdrawing funds
    public abstract void Withdraw(decimal amount);

    // Method to display account information
    public virtual void DisplayAccountInfo()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Owner Name: {OwnerName}");
        Console.WriteLine($"Balance: {Balance:C}");
    }
}

// Interface for accounts that can earn interest
public interface IInterestEarningAccount
{
    void CalculateInterest();
}

// SavingsAccount class implementing BankAccount and IInterestEarningAccount
public class SavingsAccount : BankAccount, IInterestEarningAccount
{
    // Constants for interest rate and minimum balance
    private const decimal InterestRate = 0.02m;
    private const decimal MinimumBalance = 100;

    // Constructor
    public SavingsAccount(string accountNumber, string ownerName) : base(accountNumber, ownerName)
    {
    }

    // Method to deposit funds
    public override void Deposit(decimal amount)
    {
        Balance += amount;
    }

    // Method to withdraw funds
    public override void Withdraw(decimal amount)
    {
        if (Balance - amount >= MinimumBalance)
        {
            Balance -= amount;
        }
        else
        {
            Console.WriteLine("Insufficient funds!");
        }
    }

    // Method to calculate interest
    public void CalculateInterest()
    {
        decimal interest = Balance * InterestRate;
        Balance += interest;
        Console.WriteLine($"Interest of {interest:C} added to the account.");
    }

    // Override DisplayAccountInfo method
    public override void DisplayAccountInfo()
    {
        base.DisplayAccountInfo();
        Console.WriteLine($"Account Type: Savings Account");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a savings account
        SavingsAccount savingsAccount = new SavingsAccount("SA123456", "John Doe");
        savingsAccount.Deposit(1000);
        savingsAccount.DisplayAccountInfo();

        // Calculate interest
        if (savingsAccount is IInterestEarningAccount interestAccount)
        {
            interestAccount.CalculateInterest();
            savingsAccount.DisplayAccountInfo();
        }

        // Withdraw funds
        savingsAccount.Withdraw(200);
        savingsAccount.DisplayAccountInfo();
    }
}