namespace Betty.Wallet.Services.Wallet;

public class WalletService : IWalletService
{
    private decimal _balance = 0;

    public decimal GetBalance()
    {
        return _balance;
    }

    public void Deposit(decimal amount)
    {
        _balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        _balance -= amount;
    }
}
