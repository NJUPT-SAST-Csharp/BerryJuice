﻿using Microsoft.EntityFrameworkCore;

namespace BerryJuice.Infrastructure.Persistence;

public class BerryJuiceDbContext(
    DbContextOptions<BerryJuiceDbContext> options
) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(schema: "bj_berryjuice");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BerryJuiceDbContext).Assembly);
    }
}
