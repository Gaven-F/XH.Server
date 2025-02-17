﻿namespace Server.Domain.ApprovedPolicy;

public class ApprovedPolicyService(DatabaseService database)
{
    private readonly SqlSugar.ISqlSugarClient _db = database.Instance;
    private static readonly char[] separator = [' ', ',', '|'];

    public long Create(EApprovedPolicy data)
    {
        return _db.InsertNav(data).Include(it => it.Conditions).ExecuteReturnEntity().Id;
    }

    public IEnumerable<EApprovedPolicy> GetPolicies(string? entityName)
    {
        return _db.Queryable<EApprovedPolicy>()
            .Includes(it => it.Conditions)
            .Where(it => !it.IsDeleted)
            .WhereIF(entityName != null, it => it.EntityName == entityName)
            .ToArray();
    }

    public EApprovedPolicy GetPolicy<T>(T data)
        where T : BasicEntity
    {
        var entityName = data.GetType().Name;
        var policies = _db.Queryable<EApprovedPolicy>()
            .Includes(it => it.Conditions)
            .Where(it => !it.IsDeleted)
            .Where(p => p.EntityName == entityName)
            .ToList();

        var policy = policies.FirstOrDefault(p =>
            p.Conditions != null && p.Conditions.All(it => it.Check(data))
        );

        policy ??= policies.FirstOrDefault(
            it => it!.IsDefault,
            new() { EntityName = entityName, IsDefault = true }
        );

        //ArgumentNullException.ThrowIfNull(policy);

        return policy;
    }

    public EApprovedPolicy GetPolicy<T>(long eId)
        where T : BasicEntity =>
        GetPolicy(_db.Queryable<T>().Where(it => !it.IsDeleted).InSingle(eId));

    public int Update(EApprovedPolicy data)
    {
        return _db.Updateable(data)
            .IgnoreColumns(p => new { p.CreateTime, p.Id })
            .ReSetValue(p =>
            {
                p.UpdateTime = DateTime.Now;
            })
            .ExecuteCommand();
    }

    public IEnumerable<string> GetApproverList<T>(T data)
        where T : BasicEntity =>
        GetPolicy(data).ApproverIds?.Split(',', StringSplitOptions.RemoveEmptyEntries)
        ?? throw new Exception("无效审核人员名单");

    public string GetNextApprover<T>(T data, string currentId)
        where T : BasicEntity
    {
        var list = GetApproverList(data).ToList();
        var currentIndex = list.IndexOf(currentId);
        return currentIndex == list.Count - 1 ? "NONE" : list[currentIndex + 1];
    }

    public void CreateApproveBasicLog<T>(T data)
        where T : BasicEntity
    {
        var approver =
            GetPolicy(data).ApproverIds?.Split(separator, StringSplitOptions.RemoveEmptyEntries)
            ?? [];

        List<EApprovalLog> logs = [];
        foreach (var item in approver.Select((val, i) => (val, i)))
        {
            logs.Add(
                new EApprovalLog()
                {
                    ApproverId = item.val,
                    Index = item.i,
                    EntityId = data.Id
                }
            );
        }
        if (logs.Count > 0)
        {
            _db.Insertable(logs).ExecuteReturnSnowflakeIdList();
        }
    }

    public int Approve(long logId, byte status, string msg = "无")
    {
        return _db.Updateable<EApprovalLog>()
            .Where(it => it.Id == logId)
            .SetColumns(it => new EApprovalLog
            {
                ApprovalStatus = status,
                UpdateTime = DateTime.Now,
                ApproveMsg = msg
            })
            .ExecuteCommand();
    }

    public EApprovalLog GetCurrentApprovalLog(long dataId)
    {
        var log = _db.Queryable<EApprovalLog>()
            .Where(it => it.EntityId == dataId)
            .OrderBy(it => it.Index)
            .First(it => it.ApprovalStatus != 1);

        return log;
    }

    public EApprovalLog GetLogById(long logId)
    {
        return _db.Queryable<EApprovalLog>().InSingle(logId);
    }

    public IEnumerable<EApprovalLog> GetLogs(long eId)
    {
        return _db.Queryable<EApprovalLog>()
            .Where(it => it.EntityId == eId)
            .OrderBy(it => it.Index)
            .ToList();
    }
}
