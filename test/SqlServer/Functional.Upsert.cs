using Microsoft.EntityFrameworkCore.Tests.Upsert;

namespace Microsoft.EntityFrameworkCore.Tests
{
    public class SqlServerUpsertTest : UpsertTestBase<SqlServerContextFactory<UpsertContext>>
    {
        public SqlServerUpsertTest(
            SqlServerContextFactory<UpsertContext> factory)
            : base(factory)
        {
        }

        public override void InsertIfNotExistOne()
        {
            base.InsertIfNotExistOne();

            LogSql(nameof(InsertIfNotExistOne));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING (
    VALUES
    (@__cid_1, @__teamid_2)
) AS [cte] ([Value1], [Value2])
    ON ([r].[ContestId] = [cte].[Value1]) AND ([r].[TeamId] = [cte].[Value2])
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted], [ContestId], [TeamId]) VALUES (1, 1, @__time_0, @__time_0, [cte].[Value1], [cte].[Value2]);
");
        }

        public override void InsertIfNotExistOne_CompiledQuery()
        {
            base.InsertIfNotExistOne_CompiledQuery();

            LogSql(nameof(InsertIfNotExistOne_CompiledQuery));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING (
    VALUES
    (@__cid, @__teamid)
) AS [cte] ([Value1], [Value2])
    ON ([r].[ContestId] = [cte].[Value1]) AND ([r].[TeamId] = [cte].[Value2])
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([ContestId], [TeamId], [PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted]) VALUES ([cte].[Value1], [cte].[Value2], 1, 1, @__time, @__time);
");
        }

        public override void InsertIfNotExists_AnotherTable()
        {
            base.InsertIfNotExists_AnotherTable();

            LogSql(nameof(InsertIfNotExists_AnotherTable));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING [RankSource_{{schema}}] AS [r0]
    ON ([r].[ContestId] = [r0].[ContestId]) AND ([r].[TeamId] = [r0].[TeamId])
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted], [ContestId], [TeamId]) VALUES (1, 1, [r0].[Time], [r0].[Time], [r0].[ContestId], [r0].[TeamId]);
");
        }

        public override void Translation_Parameterize()
        {
            base.Translation_Parameterize();

            LogSql(nameof(Translation_Parameterize));

            AssertSql(@"
MERGE INTO [TwoRelation_{{schema}}] AS [t]
USING (
    VALUES
    (@__p_0_0_0, @__p_0_0_1)
) AS [cte] ([aaa], [bbb])
    ON ([t].[BbbId] = @__bbb_1) AND ([t].[AaaId] = @__aaa_2)
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([BbbId], [AaaId]) VALUES (@__bbb_1, @__aaa_2);
");
        }

        public override void Upsert_AlternativeKey()
        {
            base.Upsert_AlternativeKey();

            LogSql(nameof(Upsert_AlternativeKey));

            AssertSql(@"
MERGE INTO [ThreeRelation_{{schema}}] AS [t]
USING (
    VALUES
    (@__p_0_0_0, @__p_0_0_1)
) AS [cte] ([aaa], [bbb])
    ON ([t].[BbbId] = [cte].[bbb]) AND ([t].[AaaId] = [cte].[aaa])
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([BbbId], [AaaId]) VALUES ([cte].[bbb], [cte].[aaa]);
");
        }

        public override void Upsert_AnotherTable()
        {
            base.Upsert_AnotherTable();

            LogSql(nameof(Upsert_AnotherTable));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING [RankSource_{{schema}}] AS [r0]
    ON ([r].[ContestId] = [r0].[ContestId]) AND ([r].[TeamId] = [r0].[TeamId])
WHEN MATCHED
    THEN UPDATE SET [r].[PointsPublic] = [r].[PointsPublic] + 1, [r].[TotalTimePublic] = [r].[TotalTimePublic] + [r0].[Time]
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted], [ContestId], [TeamId]) VALUES (1, 1, [r0].[Time], [r0].[Time], [r0].[ContestId], [r0].[TeamId]);
");
        }

        public override void Upsert_FromSql()
        {
            base.Upsert_FromSql();

            LogSql(nameof(Upsert_FromSql));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING (
    SELECT [r].[ContestId], [r].[TeamId], [r].[Public], [r].[Time]
    FROM [RankSource_{{schema}}] AS [r]
) AS [r0]
    ON ([r].[ContestId] = [r0].[ContestId]) AND ([r].[TeamId] = [r0].[TeamId])
WHEN MATCHED
    THEN UPDATE SET [r].[PointsPublic] = [r].[PointsPublic] + 1, [r].[TotalTimePublic] = [r].[TotalTimePublic] + [r0].[Time]
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted], [ContestId], [TeamId]) VALUES (1, 1, [r0].[Time], [r0].[Time], [r0].[ContestId], [r0].[TeamId]);
");
        }

        public override void Upsert_NewAnonymousObject()
        {
            base.Upsert_NewAnonymousObject();

            LogSql(nameof(Upsert_NewAnonymousObject));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING (
    VALUES
    (@__p_0_0_0, @__p_0_0_1, @__p_0_0_2),
    (@__p_0_1_0, @__p_0_1_1, @__p_0_1_2)
) AS [cte] ([ContestId], [TeamId], [Time])
    ON ([r].[ContestId] = [cte].[ContestId]) AND ([r].[TeamId] = [cte].[TeamId])
WHEN MATCHED
    THEN UPDATE SET [r].[PointsPublic] = [r].[PointsPublic] + 1, [r].[TotalTimePublic] = [r].[TotalTimePublic] + [cte].[Time]
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted], [ContestId], [TeamId]) VALUES (1, 1, [cte].[Time], [cte].[Time], [cte].[ContestId], [cte].[TeamId]);
");
        }

        public override void Upsert_SubSelect()
        {
            base.Upsert_SubSelect();

            LogSql(nameof(Upsert_SubSelect));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING (
    SELECT DISTINCT [r0].[ContestId], [r0].[TeamId], [r0].[Public], [r0].[Time]
    FROM [RankSource_{{schema}}] AS [r0]
) AS [t]
    ON ([r].[ContestId] = [t].[ContestId]) AND ([r].[TeamId] = [t].[TeamId])
WHEN MATCHED
    THEN UPDATE SET [r].[PointsPublic] = [r].[PointsPublic] + 1, [r].[TotalTimePublic] = [r].[TotalTimePublic] + [t].[Time]
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted], [ContestId], [TeamId]) VALUES (1, 1, [t].[Time], [t].[Time], [t].[ContestId], [t].[TeamId]);
");
        }

        public override void UpsertOne()
        {
            base.UpsertOne();

            LogSql(nameof(UpsertOne));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING (
    VALUES
    (@__cid_1, @__teamid_2)
) AS [cte] ([Value1], [Value2])
    ON ([r].[ContestId] = [cte].[Value1]) AND ([r].[TeamId] = [cte].[Value2])
WHEN MATCHED
    THEN UPDATE SET [r].[PointsPublic] = [r].[PointsPublic] + 1, [r].[TotalTimePublic] = [r].[TotalTimePublic] + @__time_0
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted], [ContestId], [TeamId]) VALUES (1, 1, @__time_0, @__time_0, [cte].[Value1], [cte].[Value2]);
");
        }

        public override void UpsertOne_CompiledQuery()
        {
            base.UpsertOne_CompiledQuery();

            LogSql(nameof(UpsertOne_CompiledQuery));

            AssertSql(@"
MERGE INTO [RankCache_{{schema}}] AS [r]
USING (
    VALUES
    (@__cid, @__teamid)
) AS [cte] ([Value1], [Value2])
    ON ([r].[ContestId] = [cte].[Value1]) AND ([r].[TeamId] = [cte].[Value2])
WHEN MATCHED
    THEN UPDATE SET [r].[PointsPublic] = [r].[PointsPublic] + 1, [r].[TotalTimePublic] = [r].[TotalTimePublic] + @__time
WHEN NOT MATCHED BY TARGET
    THEN INSERT ([ContestId], [TeamId], [PointsPublic], [PointsRestricted], [TotalTimePublic], [TotalTimeRestricted]) VALUES ([cte].[Value1], [cte].[Value2], 1, 1, @__time, @__time);
");
        }
    }
}
