using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using VoteOnline.Common;
using VoteOnline.Controllers;
using VoteOnline.Models;
using static VoteOnline.Models.RepositoryAdapter;

namespace VoteOnline.Models
{
    public interface IRepositoryAdapter
	{
		Task<GetVotePage> GetUsersFromSP ();
		Task<string> SubmitVote(string UserName, List<VoteItemDto> VoteItems);

	}
    public interface ITEST//介面注入測試
	{
        void TESTFun();
	}

	public class RepositoryAdapter : BaseAdapter,IRepositoryAdapter
    {
		private readonly ILogger<IRepositoryAdapter> _logger;
        public RepositoryAdapter(IConfiguration configuration, ILogger<IRepositoryAdapter> logger) : base(configuration) 
        {
			_logger = logger;
		}

		public async Task<GetVotePage> GetUsersFromSP()
		{
			var votePage = new GetVotePage();

            try
            {
                _logger.LogInformation("這是Ilog紀錄訊息");
                //_logger.LogInformation("這是Serilog紀錄訊息");


                using SqlConnection conn = new(_connectionString);
                await conn.OpenAsync();
                using SqlCommand cmd = new("GETVOTEPAGE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@O_CHR_MESSAGE", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    // 第一個結果集：讀取所有 UserName
                    while (await reader.ReadAsync())
                    {
                        votePage.UserNames.Add(reader["UserName"].ToString());
                    }

                    // 移動到第二個結果集
                    if (await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            votePage.VoteItemCounts.Add(new VoteItemCount
                            {
                                VoteItemId = Convert.ToInt32(reader["VoteItemId"]),
                                ItemName = reader["ItemName"].ToString(),
                                VoteCount = Convert.ToInt32(reader["VoteCount"])
                            });
                        }
                    }
                }

                // 取得 OUTPUT 參數的值
                votePage.Message = cmd.Parameters["@O_CHR_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
				votePage.Message = $"錯誤: {ex.Message}";
            }

            return votePage;
        }

        public async Task<string>SubmitVote(string UserName, List<VoteItemDto> VoteItems) {
            string O_CHR_MESSAGE = "";
            try
            {
                using SqlConnection conn = new(_connectionString);
				await conn.OpenAsync();
				foreach (var voteItem in VoteItems)
                {
                    using SqlCommand cmd = new("INSERTVOTERECORD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 輸入參數
                    cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                    cmd.Parameters.Add(new SqlParameter("@VoteItemId", voteItem.VoteItemId));
                    cmd.Parameters.Add(new SqlParameter("@ItemName", voteItem.ItemName));

                    // OUTPUT 參數
                    var voteIdParam = new SqlParameter("@VoteId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(voteIdParam);
                    cmd.Parameters.Add(new SqlParameter("@O_CHR_MESSAGE", SqlDbType.NVarChar, 200)).Direction = ParameterDirection.Output;
                    //var messageParam = new SqlParameter("@O_CHR_MESSAGE", SqlDbType.NVarChar, 200)
                    //{
                    //    Direction = ParameterDirection.Output
                    //};
                    //cmd.Parameters.Add(messageParam);

                    // 執行 Stored Procedure
                    await cmd.ExecuteNonQueryAsync();

                    // 取得 OUTPUT 參數的值
                    int voteId = (voteIdParam.Value != DBNull.Value) ? Convert.ToInt32(voteIdParam.Value) : 0;
                    O_CHR_MESSAGE = cmd.Parameters["@O_CHR_MESSAGE"].Value.ToString()??"";

                    // 若 `voteId == 0`，表示沒有插入（可能已投過），可根據需求記錄 log
                    if (voteId == 0)
                    {
                        O_CHR_MESSAGE = ($"使用者 {UserName} 已對項目 {voteItem.ItemName} 投過票");
                    }
                    
                }
				return O_CHR_MESSAGE;
			}
            catch (Exception ex)
            {
				O_CHR_MESSAGE = $"錯誤: {ex.Message}";
				return O_CHR_MESSAGE;
			}
        }
    }

    public partial class TEST: ITEST//介面注入測試(在Homecontroll實利)
	{ 
       public void TESTFun() 
       {
            Debug.WriteLine("tests");
       }
    }
    public class TEST2
    {
		public void TESTFun2()
		{
			Debug.WriteLine("test2");
		}
	}
}
