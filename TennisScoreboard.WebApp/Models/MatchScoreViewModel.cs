using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.Domain.Entities;
using TennisScoreboard.Infrastructure.Services;

namespace TennisScoreboard.WebApp.Models
{
    public class MatchScoreViewModel 
    {
        public int IdPointWinner { get; set; }
    }
}
