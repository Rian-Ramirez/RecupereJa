using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace RecupereJa.Data
{
    /// <summary>
    /// Fábrica usada pelo EF Core em tempo de design (migrations).
    /// Resolve o appsettings.json em caminhos comuns e faz fallback para variáveis de ambiente.
    /// </summary>
    public class RecupereJaContextFactory : IDesignTimeDbContextFactory<RecupereJaContext>
    {
        public RecupereJaContext CreateDbContext(string[] args)
        {
            var basePath = ResolveBasePath();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables() // permite ConnectionStrings__DefaultConnection
                .Build();

            // Tenta pegar da seção ConnectionStrings
            var connectionString = config.GetConnectionString("DefaultConnection");

            // Fallback para variável de ambiente (ex.: PowerShell/bash)
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"ConnectionString 'DefaultConnection' não encontrada. " +
                    $"Caminho base usado: {basePath}. " +
                    $"Crie um appsettings.json válido nesse diretório ou defina a variável de ambiente 'ConnectionStrings__DefaultConnection'.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<RecupereJaContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 39)));

            return new RecupereJaContext(optionsBuilder.Options);
        }

        private static string ResolveBasePath()
        {
            // Locais prováveis para o appsettings.json durante 'dotnet ef'
            var current = Directory.GetCurrentDirectory();
            var candidates = new List<string>
            {
                current,                                        // quando executa dentro do projeto
                Path.Combine(current, "RecupereJa"),            // quando executa na pasta da solução
                AppContext.BaseDirectory,                       // caminho do binário
                Path.GetFullPath(Path.Combine(current, "..")),  // sobe um nível
                Path.GetFullPath(Path.Combine(current, "..", "RecupereJa"))
            };

            foreach (var c in candidates)
            {
                try
                {
                    if (File.Exists(Path.Combine(c, "appsettings.json")))
                        return c;
                }
                catch { /* ignora erros de IO e continua */ }
            }

            // Se não achar, usa o diretório atual (não lança aqui; quem lança é a falta da connection string)
            return current;
        }
    }
}