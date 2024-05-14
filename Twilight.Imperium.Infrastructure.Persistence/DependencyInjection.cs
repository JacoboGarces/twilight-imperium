﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Twilight.Imperium.Application.Generic;

namespace Twilight.Imperium.Infrastructure.Persistence
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddPersistence( this IServiceCollection services, IConfiguration configuration )
    {

      services.AddScoped<IMongoCollection<Event>>((options) =>
      {
        var client = new MongoClient( configuration["OnlineShopDatabase:ConnectionString"] );
        var database = client.GetDatabase( configuration["OnlineShopDatabase:DatabaseName"] );
        return database.GetCollection<Event>( configuration["OnlineShopDatabase:UsersCollectionName"] );
      } );

      services.AddScoped<IEventRepository, EventsRepository>();

      return services;
    }
  }
}