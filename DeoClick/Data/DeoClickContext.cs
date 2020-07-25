using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeoClick.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DeoClick.Data
{
    public class DeoClickContext : IdentityDbContext<Usuario>
    {
        public DeoClickContext (DbContextOptions<DeoClickContext> options)
            : base(options)
        {
        }

        public DbSet<Estados> Estados { get; set; }

        public DbSet<DeoClick.Models.Cidade> Cidade { get; set; }

        public DbSet<DeoClick.Models.FormaPagamento> FormaPagamento { get; set; }

        public DbSet<DeoClick.Models.Clientes> Clientes { get; set; }

        public DbSet<DeoClick.Models.Anuncio> Anuncio { get; set; }

        public DbSet<DeoClick.Models.PagamentoAnuncio> PagamentoAnuncio { get; set; }

        public DbSet<DeoClick.Models.Evento> Evento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<Usuario>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            modelBuilder.Entity<Usuario>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
          
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            // any guid
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            // any guid, but nothing is against to use the same one
            const string ROLE_ID = ADMIN_ID;
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            });

            var hasher = new PasswordHasher<Usuario>();
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = ADMIN_ID,
                Nome = "Admin",
                UserName = "deoclick@gmail.com",
                NormalizedUserName = "deoclick@gmail.com",
                Email = "deoclick@gmail.com",
                NormalizedEmail = "deoclick@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "12345678"),
                SecurityStamp = hasher.GetHashCode().ToString()
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
