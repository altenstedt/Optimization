using System.Data.Entity;

using Data.Migrations;

namespace Data
{
    internal class ContextInitializer : MigrateDatabaseToLatestVersion<OptimizationContext, Configuration>
    {
    }
}