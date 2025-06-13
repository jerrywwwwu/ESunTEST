using System;
using Microsoft.Extensions.Logging;
namespace VoteOnline.Common
{
    public class BaseAdapter
    {
        protected readonly string _connectionString;

        public BaseAdapter(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("VoteOnlineConnectstring");
        }
    }
}
