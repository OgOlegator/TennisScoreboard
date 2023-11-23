using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TennisScoreboard.Application.Common.Interfaces;
using TennisScoreboard.Domain.Entities;

namespace TennisScoreboard.Application.Players
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IApplicationDbContext _context;

        public PlayerRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Player> GetById(int id)
        {
            try
            {
                return await _context.Players.FirstOrDefaultAsync(player => player.Id == id);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("Задано некорректное имя игрока");
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception("Ошибка при получении данных игрока");
            }
        }

        /// <summary>
        /// Найти игрока по имени
        /// </summary>
        /// <param name="name">Имя игрока</param>
        /// <returns>Данные игрока</returns>
        /// <exception cref="KeyNotFoundException">Игрок не найден</exception>
        /// <exception cref="Exception"></exception>
        public async Task<Player> GetByName(string name)
        {
            var result = await GetByNameOrDefaulthAsync(name);

            if (result == null)
                throw new KeyNotFoundException(" Игрок не найден");

            return result;
        }

        /// <summary>
        /// Найти или создать нового игрока по имени
        /// </summary>
        /// <param name="name">Имя игрока</param>
        /// <returns></returns>
        /// <exception cref="Exception">Ошибка при сохранении в БД</exception>
        public async Task<Player> GetByNameOrCreate(string name)
        {
            var result = await GetByNameOrDefaulthAsync(name);

            if (result != null)
                return result;

            result = new Player { Name = name };

            try
            {
                await _context.Players.AddAsync(result);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Ошибка при сохранении");
            }

            return result;
        }

        private async Task<Player> GetByNameOrDefaulthAsync(string name)
        {
            try
            {
                return await _context.Players.FirstOrDefaultAsync(player => player.Name == name);
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception("Задано некорректное имя игрока");
            }
            catch(OperationCanceledException ex)
            {
                throw new Exception("Ошибка при получении данных игрока");
            }
        }
    }
}
