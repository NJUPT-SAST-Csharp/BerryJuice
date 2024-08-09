using Microsoft.EntityFrameworkCore;

namespace BerryJuice.Infrastructure.Persistence;

public class BerryJuiceDbContext(DbContextOptions<BerryJuiceDbContext> options)
    : DbContext(options) { }
