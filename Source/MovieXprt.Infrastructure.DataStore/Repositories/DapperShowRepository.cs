namespace MovieXprt.Infrastructure.DataStore.Gateways;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieXprt.Infrastructure.DataStore.Options;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Threading;
using MovieXprt.Domain.Models;
using MovieXprt.Domain.Repositories;

public class DapperShowRepository(
    ILogger<DapperShowRepository> logger,
    IOptions<ConnectionStrings> connectionStrings
    ) : IShowRepository
{
    private readonly ILogger<DapperShowRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly string _connectionString = connectionStrings.Value.MovieXprtSql;

    public async Task AddShows(IEnumerable<Show> showsOnPage, CancellationToken ct)
    {
        var serialized = JsonSerializer.Serialize(showsOnPage);
        await StoreAsync<Show>("INSERT INTO Shows ([Data]) VALUES (@Data)", ct, new {
            Data = serialized
        });
    }

    public async Task<int> getHighestShowId()
    {
        return await QueryAsync<int>("SELECT MAX(Id) FROM Shows", CancellationToken.None);
    }

    public async Task<IEnumerable<Show>> GetShows(int page, int pageSize, CancellationToken ct)
    {
        return await QueryAsync<IEnumerable<Show>>("SELECT [data] FROM Shows ORDER BY Premiered OFFSET @Offset ROWS FETCH NEXT @PageSize", ct, new
        {
            PageSize = pageSize,
            Offset = (page - 1) * pageSize
        });
    }

    private async Task<T> QueryAsync<T>(string sql, CancellationToken ct, object? param = null)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QueryAsync<T>(sql, param);
    }

    private Task<int> StoreAsync<T>(string sql, CancellationToken ct, object? param = null)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
         
        return connection.ExecuteAsync(sql, param);
    }   
}
