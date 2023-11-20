using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.Domain.Entities;
using TennisScoreboard.Infrastructure.Persistence;
using TennisScoreboard.Infrastructure.Services;
using TennisScoreboard.WebApp.Models;

namespace TennisScoreboard.WebApp.Controllers
{
    public class CreateMatchController : Controller
    {
        private readonly IPlayerRepository _playerRep;

        public CreateMatchController(IPlayerRepository playerRepository)
        {
            _playerRep = playerRepository;
        }

        [HttpGet]
        [Route("new-match")]
        public IActionResult Create()
        {
            return View();
        }

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

                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(createViewModel);
            }



            return View();
        }
    }
}
