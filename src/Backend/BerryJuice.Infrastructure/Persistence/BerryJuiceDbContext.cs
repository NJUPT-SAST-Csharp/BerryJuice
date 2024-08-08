using Microsoft.EntityFrameworkCore;

namespace BerryJuice.Persistence;

internal class BerryJuiceDbContext(DbContextOptions<BerryJuiceDbContext> options)
    : DbContext(options) { }
