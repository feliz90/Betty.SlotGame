namespace Betty.Wallet.Services.Wallet;

public interface IWalletService
{
    decimal GetBalance();
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
}