namespace DotnetCoreAngularStarter.Common.Options
{
    public class DatabaseOptions
    {
        public string SqlConnectionString { get; set; }
        public bool PluralizeColumnNames { get; set; }
        public LogTableGenerationOptions LogTableGeneration { get; set; }

        public class LogTableGenerationOptions
        {
            public bool GenerateLogTables { get; set; }
            public string[] TableNamesToInclude { get; set; }
            public string[] TableNamesToExlude { get; set; }
        }
    }
}
