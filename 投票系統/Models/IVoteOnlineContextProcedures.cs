﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using VoteOnline.Models;

namespace VoteOnline.Models
{
    public partial interface IVoteOnlineContextProcedures
    {
        Task<List<GetVoteCountsResult>> GetVoteCountsAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
