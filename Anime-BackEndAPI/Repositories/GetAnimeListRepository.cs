﻿using Anime_BackEndAPI.DTOs;
using Anime_BackEndAPI.Interfaces;
using Dapper;
using System.Data;

namespace Anime_BackEndAPI.Repositories
{
    public class GetAnimeListRepository : IGetAnimeListRepository 
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetAnimeListRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        
        public async Task<IEnumerable<MinimalAnimeDTO>> GetAnimeListRepositoryAsync()
        {
            using(IDbConnection connection = _dbConnectionFactory.ConnectToDataBase())
            {
                var animeList = await connection.QueryAsync<MinimalAnimeDTO>("GetAnimeList",commandType:CommandType.StoredProcedure);
                return animeList;
            }
        }
        public async Task<IEnumerable<MinimalAnimeDTO>> GetAnimeListByUserIdAsync(int IdUser)
        {
            using(IDbConnection connection = _dbConnectionFactory.ConnectToDataBase())
            {
                var parameters = new DynamicParameters();   
                parameters.Add("@UserID", IdUser);
                var animeList = await connection.QueryAsync<MinimalAnimeDTO>("GetAnimeListForUser", parameters,commandType:CommandType.StoredProcedure);
                return animeList;
            }
        }
    }
}
