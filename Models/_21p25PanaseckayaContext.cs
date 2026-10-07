using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models;

public partial class _21p25PanaseckayaContext : DbContext
{
    public _21p25PanaseckayaContext()
    {
    }

    public _21p25PanaseckayaContext(DbContextOptions<_21p25PanaseckayaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacture> Manufactures { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Subcategory> Subcategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ngknn.ru;Port=5442;Database=21P_25_Panaseckaya;Username=21P2025;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("C");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("category_pkey");

            entity.ToTable("category", "MagicBoots");

            entity.HasIndex(e => e.Name, "category_name_key").IsUnique();

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Manufacture>(entity =>
        {
            entity.HasKey(e => e.IdManufacture).HasName("manufacture_pkey");

            entity.ToTable("manufacture", "MagicBoots");

            entity.HasIndex(e => e.Name, "manufacture_name_key").IsUnique();

            entity.Property(e => e.IdManufacture).HasColumnName("id_manufacture");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("order_pkey");

            entity.ToTable("order", "MagicBoots");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdUser)
                .HasColumnType("character varying")
                .HasColumnName("id_user");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("order_id_user_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("product_pkey");

            entity.ToTable("product", "MagicBoots");

            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdManufacture).HasColumnName("id_manufacture");
            entity.Property(e => e.IdSubcategory).HasColumnName("id_subcategory");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Structure)
                .HasColumnType("character varying")
                .HasColumnName("structure");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("product_id_category_fkey");

            entity.HasOne(d => d.IdManufactureNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdManufacture)
                .HasConstraintName("product_id_manufacture_fkey");

            entity.HasOne(d => d.IdSubcategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdSubcategory)
                .HasConstraintName("product_id_subcategory_fkey");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => new { e.IdProduct, e.IdOrder, e.IdSize }).HasName("product_order_pkey");

            entity.ToTable("product_order", "MagicBoots");

            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.IdSize).HasColumnName("id_size");
            entity.Property(e => e.Count).HasColumnName("count");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_order_id_order_fkey");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_order_id_product_fkey");

            entity.HasOne(d => d.IdSizeNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.IdSize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_order_id_size_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("role_pkey");

            entity.ToTable("role", "MagicBoots");

            entity.HasIndex(e => e.Name, "role_name_key").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.IdSize).HasName("size_pkey");

            entity.ToTable("size", "MagicBoots");

            entity.HasIndex(e => e.Name, "size_name_key").IsUnique();

            entity.Property(e => e.IdSize).HasColumnName("id_size");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.IdStock).HasName("stock_pkey");

            entity.ToTable("stock", "MagicBoots");

            entity.Property(e => e.IdStock).HasColumnName("id_stock");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdSize).HasColumnName("id_size");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("stock_id_product_fkey");

            entity.HasOne(d => d.IdSizeNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IdSize)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("stock_id_size_fkey");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.IdSubcategory).HasName("subcategory_pkey");

            entity.ToTable("subcategory", "MagicBoots");

            entity.HasIndex(e => e.Name, "subcategory_name_key").IsUnique();

            entity.Property(e => e.IdSubcategory).HasColumnName("id_subcategory");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("user_pkey");

            entity.ToTable("user", "MagicBoots");

            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("user_id_role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
