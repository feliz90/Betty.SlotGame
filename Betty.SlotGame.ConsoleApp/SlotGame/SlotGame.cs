using Betty.Wallet.Services;
using Betty.Wallet.Services.Game;
using Betty.Wallet.Services.Wallet;

namespace Betty.Wallet.ConsoleApp.SlotGame;

public class SlotGame
{
    private readonly IWalletService _walletService;
    private readonly IGameService _gameService;
    
    public SlotGame(IWalletService walletService, IGameService gameService)
    {
        _walletService = walletService;
        _gameService = gameService;
    }

    public void Run()
    {
        var isRunning = true;
        
        Console.WriteLine("Welcome to the Player Wallet!");

        while (isRunning)
        {
            Console.WriteLine("Please submit the action: ");

            var action = Console.ReadLine();
            var actionArray = action.Split(' ');

            if (actionArray.Length >= 2 && TryGetActionName(actionArray[0], out var method))
            {
                if (decimal.TryParse(actionArray[1], out var amount))
                {
                    method(amount);
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please try again.");
                }
            }
            else if (action == "exit")
            {
                isRunning = false;
                Console.WriteLine("Thank you for playing! Hope to see you soon again");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            Console.WriteLine();
        }
    }
    private void DepositFunds(decimal amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Invalid amount. Deposit failed.");
            return;
        }

        _walletService.Deposit(amount);
        Console.WriteLine($"Deposit successful. Your current balance is: ${_walletService.GetBalance()}");
    }
    
    private void WithdrawFunds(decimal amount)
    {
        var currentBalance = _walletService.GetBalance();

        if (amount < 0)
        {
            Console.WriteLine("Invalid amount. Withdrawal failed.");
            return;
        }

        if (amount > currentBalance)
        {
            Console.WriteLine("Insufficient balance. Withdrawal failed.");
            return;
        }

        _walletService.Withdraw(amount);
        Console.WriteLine($"Withdrawal successful. Your current balance is: ${_walletService.GetBalance()}");
    }
    
    private void PlayGame(decimal betAmount)
    {
        var balance = _walletService.GetBalance();

        if (betAmount > balance)
        {
            Console.WriteLine("Your balance is less than your bet amount.");
            return;
        }

        if (betAmount is < 1 or > 10)
        {
            Console.WriteLine("Invalid bet amount. Game round failed.");
            return;
        }

        _walletService.Withdraw(betAmount);
        var winAmount = _gameService.Play(betAmount);
        _walletService.Deposit(winAmount);
        Console.WriteLine($"Your current balance is: ${_walletService.GetBalance()}");
    }
    
    private bool TryGetActionName(string action, out Action<decimal>? method)
    {
        var actionMethods = new Dictionary<string, Action<decimal>?>
        {
            { "deposit", DepositFunds },
            { "withdraw", WithdrawFunds },
            { "bet", PlayGame }
        };

        return actionMethods.TryGetValue(action, out method);
    }
}