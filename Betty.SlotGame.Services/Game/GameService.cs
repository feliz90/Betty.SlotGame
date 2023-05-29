namespace Betty.Wallet.Services.Game;

public class GameService : IGameService
{
    private readonly Random _random = new();

    public decimal Play(decimal betAmount)
    {
        decimal winAmount = 0;
        var winPercentage = _random.Next(1, 101);

        switch (winPercentage)
        {
            case <= 50:
                Console.WriteLine("No luck this time.");
                break;
            case <= 90:
                winAmount = betAmount * 2;
                Console.WriteLine($"Congratulations! You won up to x2 the bet amount: ${winAmount}");
                break;
            default:
                winAmount = _random.Next((int)(betAmount * 2), (int)(betAmount * 11));
                Console.WriteLine($"Congratulations! You won: ${winAmount}");
                break;
        }

        return winAmount;
    }
}