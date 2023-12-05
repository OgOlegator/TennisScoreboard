using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.Domain.Entities;
using TennisScoreboard.Infrastructure.Persistence;
using TennisScoreboard.Infrastructure.Services;
using TennisScoreboard.WebApp.Models;

namespace TennisScoreboard.WebApp.Controllers
{
    public class MatchController : Controller
    {
        private readonly IPlayerRepository _playerRep;
        private readonly IMatchRepository _matchRep;
        private readonly IMemoryCache _cache;

        public MatchController(IPlayerRepository playerRepository, IMemoryCache cache, IMatchRepository matchRep)
        {
            _playerRep = playerRepository;
            _cache = cache;
            _matchRep = matchRep;
        }

        [HttpGet]
        [Route("new-match")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Создание нового матча
        /// </summary>
        /// <param name="createViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("new-match")]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
        {
            if(!ModelState.IsValid)
                return View(createViewModel);

            try
            {
                var player1 = await _playerRep.GetByNameOrCreate(createViewModel.NamePlayer1);
                var player2 = await _playerRep.GetByNameOrCreate(createViewModel.NamePlayer2);

                var newMatch = new MatchService(player1.Id, player2.Id);

                var newMatchId = MatchService.GetNewMatchId();

                _cache.Set(newMatchId, newMatch);

                return RedirectToAction(nameof(MatchScore), new { uuid = newMatchId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(createViewModel);
            }
        }

        /// <summary>
        /// Страница матча
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("match-score")]
        public async Task<IActionResult> MatchScore(string uuid)
        {
            if (!_cache.TryGetValue(uuid, out MatchService match))
                return NotFound();

            return View(await GetMatchScoreViewModel(match));
        }

        [HttpPost]
        [Route("match-score")]
        public async Task<IActionResult> MatchScore(MatchScoreViewModel viewModel, string uuid)
        {
            if (!_cache.TryGetValue(uuid, out MatchService match))
                return NotFound();

            match.AddPointForPlayer(match.GetWinPlayerById(viewModel.IdPointWinner));

            if(!match.IsFinished)
                return RedirectToAction(nameof(MatchScore), new { uuid = uuid });

            await _matchRep.AddAsync(match.IdPlayer1, match.IdPlayer2, match.WinnerId);
            
            await Task.Factory.StartNew(() 
                => _cache.Remove(uuid));

            return View(await GetMatchScoreViewModel(match));
        }

        private async Task<MatchScoreViewModel> GetMatchScoreViewModel(MatchService match)
            => new MatchScoreViewModel
            {
                Match = match,
                Player1 = await _playerRep.GetById(match.IdPlayer1),
                Player2 = await _playerRep.GetById(match.IdPlayer2),
            };
    }
}
