using System;
using System.Collections.Generic;
using KiraShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KiraApi2;

public partial class KiraApiDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<CarrinhoItem> CarrinhoItens { get; set; }

    public KiraApiDbContext()
    {
    }

    public KiraApiDbContext(DbContextOptions<KiraApiDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user id=root;password=;database=kira2db",
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.42-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        // CPF único
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.CPF)
            .IsUnique();

        // Usuário-Carrinho 1:1
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Carrinho)
            .WithOne(c => c.Usuario)
            .HasForeignKey<Carrinho>(c => c.UsuarioId);

        // Produto-Marca N:1
        modelBuilder.Entity<Produto>()
            .HasOne(p => p.Marca)
            .WithMany(m => m.Produtos)
            .HasForeignKey(p => p.MarcaId);

        // Produto-Categoria N:N
        modelBuilder.Entity<Produto>()
            .HasMany(p => p.Categorias)
            .WithMany(c => c.Produtos)
            .UsingEntity(j => j.ToTable("ProdutoCategoria"));

        // Carrinho-CarrinhoItem 1:N
        modelBuilder.Entity<CarrinhoItem>()
            .HasOne(ci => ci.Carrinho)
            .WithMany(c => c.Itens)
            .HasForeignKey(ci => ci.CarrinhoId);

        // Produto-CarrinhoItem 1:N
        modelBuilder.Entity<CarrinhoItem>()
            .HasOne(ci => ci.Produto)
            .WithMany()
            .HasForeignKey(ci => ci.ProdutoId);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
