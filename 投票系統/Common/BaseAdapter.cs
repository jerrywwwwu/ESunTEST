using System;
using Microsoft.Extensions.Logging;
namespace VoteOnline.Common
{
	public class BaseAdapter
	{
		protected readonly string _connectionString = null!;

		public BaseAdapter(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("VoteOnlineConnectstring") ?? throw new ArgumentNullException("Connection string not found.");
		}
	}

}
