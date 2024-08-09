using Microsoft.EntityFrameworkCore;

namespace BerryJuice.Infrastructure.Persistence;

internal class BerryJuiceDbContext(DbContextOptions<BerryJuiceDbContext> options)
    : DbContext(options) { }
