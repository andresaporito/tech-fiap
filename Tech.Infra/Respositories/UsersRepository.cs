﻿using Microsoft.EntityFrameworkCore;
using Tech.Domain.Entities;
using Tech.Domain.Interfaces.Repositories;
using Tech.Infra.Context;

namespace Tech.Infra.Respositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo responsavel por retornar as informacoes de um usuario
        /// com base no seu id de cadastro
        /// </summary>
        /// <param name="id">Id do usuario cadastrado na base</param>
        /// <returns>retorna um objeto do tipo Usuario com as informacoes
        /// cadastradas no BD</returns>
        public async Task<Users> GetId(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Users>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> AddUsers(Users contact)
        {
            await _context.Users.AddAsync(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Users> UpdateUsers(Users contact)
        {
            _context.Users.Update(contact);
            await _context.SaveChangesAsync();

            return contact;
        }
        public async Task<Users> DeleteUsers(Users contact)
        {
            _context.Users.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }
        public async Task<Users> GetName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task AddAsync(UserGame userGame)
        {
            await _context.UserGames.AddAsync(userGame);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByUserId(int userId)
        {
            return await _context.UserGames
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.Game)
                .ToListAsync();
        }

        public async Task RemoveAsync(int userId, int gameId)
        {
            var userGame = await _context.UserGames
                .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GameId == gameId);

            if (userGame != null)
            {
                _context.UserGames.Remove(userGame);
                await _context.SaveChangesAsync();
            }
        }

    }
}
