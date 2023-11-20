using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.Application.Players;

namespace TennisScoreboard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            return services;
        }
    }
}
