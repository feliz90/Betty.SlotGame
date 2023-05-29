// See https://aka.ms/new-console-template for more information

using Betty.Wallet.ConsoleApp.SlotGame;
using Betty.Wallet.Services.Game;
using Betty.Wallet.Services.Wallet;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddTransient<IWalletService, WalletService>()
    .AddTransient<IGameService, GameService>()
    .AddSingleton<SlotGame>()
    .BuildServiceProvider();
    
var playerWallet = serviceProvider.GetRequiredService<SlotGame>();
playerWallet.Run();   