using System;
using System.Collections.Generic;
using System.Linq;
using DotnetCoreAngularStarter.Models.EntityFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCoreAngularStarter.Models.EntityFramework.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                                 .GetAppliedMigrations()
                                 .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                               .Migrations
                               .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void Seed(this DotnetCoreAngularStarterDbContext context)
        {
            if (!context.Notes.Any())
            {
                var notes = new List<Note>
                            {
                                new Note {Id = Guid.NewGuid(), Text = "HardCodedNote1", CreationDate = DateTime.UtcNow,},
                                new Note {Id = Guid.NewGuid(), Text = "HardCodedNote2", CreationDate = DateTime.UtcNow,},
                                new Note {Id = Guid.NewGuid(), Text = "HardCodedNote3", CreationDate = DateTime.UtcNow,},
                            };
                context.AddRange(notes);
                context.SaveChanges();
            }
        }

        public static void UseTableNamePluralization(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }
        }

        public static void GenerateLogTables(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                var logTableName = entity.DisplayName() + "Log";
                //modelBuilder.Model.AddEntityType()
            }
        }
    }
}
