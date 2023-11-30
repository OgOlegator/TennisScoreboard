using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.WebApp.Models;

namespace TennisScoreboard.WebApp.Controllers
{
    public class MatchHistoryController : Controller
    {
        private readonly IApplicationDbContext _context;
        private const int _pageSize = 5;

        public MatchHistoryController(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Страница истории матчей
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="filterByPlayerName">Фильтр по имени игроков</param>
        /// <returns></returns>
        [HttpGet]
        [Route("matches")]
        public async Task<IActionResult> Get(string filterByPlayerName, int page = 1)
        {
            var source = _context.Matches
                .Include(match => match.Player1)
                .Include(match => match.Player2)
                .Where(match
                    => string.IsNullOrEmpty(filterByPlayerName)
                    || match.Player1.Name.Contains(filterByPlayerName)
                    || match.Player2.Name.Contains(filterByPlayerName))
                .OrderByDescending(match => match.Id);

            var matches = await source
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync();

            var pageInfo = new PageViewModel(await source.CountAsync(), page, _pageSize);

            var model = new MatchHistoryViewModel
            {
                Matches = matches,
                PageInfo = pageInfo,
            };

            return View(model);
        }
    }
}
