using StackExchange.Redis;
using System;

public class RedisService
{
    private readonly ConnectionMultiplexer _redis;
    public IDatabase db => _redis.GetDatabase();

    public RedisService(string connectionString)
    {
        _redis = ConnectionMultiplexer.Connect(connectionString); 
    }

    public void SetString(string key, string value)
    {
        db.StringSet(key, value);
    }

    public string GetString(string key)
    {
        return db.StringGet(key);
    }
}
