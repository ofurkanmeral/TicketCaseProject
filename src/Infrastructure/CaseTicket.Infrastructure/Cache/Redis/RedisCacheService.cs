using CaseTicket.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Infrastructure.Cache.Redis
{
    public class RedisCacheService:IRedisCacheService
    {
        private readonly ConnectionMultiplexer redisConnection;
        private readonly IDatabase database;
        private readonly RedisCacheServiceSetting setting;
        private readonly IConfiguration _configuration;

        public RedisCacheService(IOptions<RedisCacheServiceSetting> options, IConfiguration configuration)
        {
            setting = options.Value;
            var opt = ConfigurationOptions.Parse(setting.ConnectionString);
            redisConnection = ConnectionMultiplexer.Connect(opt);
            database = redisConnection.GetDatabase();
            _configuration = configuration;
            setting.ExpireTime = _configuration.GetValue<double>("RedisCacheServiceSetting:ExpireTime");
        }

        public T GetAsync<T>(string key)
        {
            var value = database.StringGet(key);
            if (value.HasValue)
                return JsonConvert.DeserializeObject<T>(value);
            return default;
        }

        public object RemoveData(string key)
        {
            throw new NotImplementedException();
        }

        public bool SetAsync<T>(string key, T value)
        {
            TimeSpan expireTime = TimeSpan.FromMinutes(setting.ExpireTime);
            return database.StringSet(key, JsonConvert.SerializeObject(value), expireTime);
        }

    }
}
