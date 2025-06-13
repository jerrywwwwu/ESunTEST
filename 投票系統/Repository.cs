using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using VoteOnline.Common;
using VoteOnline.Controllers;
using VoteOnline.Models;

namespace VoteOnline.Models
{
    public class RepositoryAdapter : BaseAdapter
    {
		private readonly ILogger<RepositoryAdapter> _logger;
        public RepositoryAdapter(IConfiguration configuration, ILogger<RepositoryAdapter> logger) : base(configuration) 
        {
			_logger = logger;
		}


		public GetVotePage GetUsersFromSP(out string O_CHR_MESSAGE)
        {
            GetVotePage votePage = new(); // 單一物件
            O_CHR_MESSAGE = "";

            try
            {
                _logger.LogInformation("這是Serilog紀錄訊息");

				using SqlConnection conn = new(_connectionString);
                conn.Open();
                using SqlCommand cmd = new("GETVOTEPAGE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@O_CHR_MESSAGE", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // 第一個結果集：讀取所有 UserName
                    while (reader.Read())
                    {
                        votePage.UserNames.Add(reader["UserName"].ToString());
                    }

                    // 移動到第二個結果集
                    if (reader.NextResult())
                    {
                        while (reader.Read())
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
                O_CHR_MESSAGE = cmd.Parameters["@O_CHR_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                O_CHR_MESSAGE = $"錯誤: {ex.Message}";
            }

            return votePage;
        }

        public void SubmitVote(string UserName, List<VoteItemDto> VoteItems, out string O_CHR_MESSAGE) {
            O_CHR_MESSAGE = "";
            try
            {
                using SqlConnection conn = new(_connectionString);
                conn.Open();
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
                    cmd.ExecuteNonQuery();

                    // 取得 OUTPUT 參數的值
                    int voteId = (voteIdParam.Value != DBNull.Value) ? Convert.ToInt32(voteIdParam.Value) : 0;
                    O_CHR_MESSAGE = cmd.Parameters["@O_CHR_MESSAGE"].Value.ToString();

                    // 若 `voteId == 0`，表示沒有插入（可能已投過），可根據需求記錄 log
                    if (voteId == 0)
                    {
                        O_CHR_MESSAGE = ($"使用者 {UserName} 已對項目 {voteItem.ItemName} 投過票");
                    }
                }
            }
            catch (Exception ex)
            {
				O_CHR_MESSAGE = $"錯誤: {ex.Message}";
            }
        }
    }
}
