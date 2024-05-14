using Microsoft.Extensions.DependencyInjection;
using Twilight.Imperium.Application.Battle;
using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Domain.Battle.Commands;
using Twilight.Imperium.Domain.Battle.Values.Root;

namespace Twilight.Imperium.Application
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddTransient<IInitialCommandUseCase<CreateBattleCommand>, CreateBattleUseCase>();
      services.AddTransient<ICommandUseCase<CreatePlayerCommand, BattleId>, CreatePlayerUseCase>();
      return services;
    }
  }
}
