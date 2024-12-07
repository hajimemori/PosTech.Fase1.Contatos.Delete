﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Postech.Fase3.Contatos.Delete.Infra.Repository.Context;

public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
{
    public AppDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
        optionsBuilder.UseSqlServer("Server=154.38.172.208,52439;Database=ContatoFase3;User Id=sa;Password=Fiap2024$;TrustServerCertificate=True;");

        return new AppDBContext(optionsBuilder.Options);
    }
}