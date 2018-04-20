using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevHubWeb.Data.Entities
{
    public partial class DevHubContext : DbContext
    {
        public virtual DbSet<Amenities> Amenities { get; set; }
        public virtual DbSet<AmenityRates> AmenityRates { get; set; }
        public virtual DbSet<BillingOverallTransaction> BillingOverallTransaction { get; set; }
        public virtual DbSet<BillingTransMiscellaneous> BillingTransMiscellaneous { get; set; }
        public virtual DbSet<BillingTransOthers> BillingTransOthers { get; set; }
        public virtual DbSet<BookingType> BookingType { get; set; }
        public virtual DbSet<BookLog> BookLog { get; set; }
        public virtual DbSet<ClientMaster> ClientMaster { get; set; }
        public virtual DbSet<EmailRecipients> EmailRecipients { get; set; }
        public virtual DbSet<Frequencies> Frequencies { get; set; }
        public virtual DbSet<InvAddProducts> InvAddProducts { get; set; }
        public virtual DbSet<InvProductCategories> InvProductCategories { get; set; }
        public virtual DbSet<InvProducts> InvProducts { get; set; }
        public virtual DbSet<InvQuickInventory> InvQuickInventory { get; set; }
        public virtual DbSet<InvTransactionOthers> InvTransactionOthers { get; set; }
        public virtual DbSet<InvUnitOfMeasure> InvUnitOfMeasure { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Space> Space { get; set; }
        public virtual DbSet<TimeTrackingLogger> TimeTrackingLogger { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public DevHubContext(DbContextOptions<DevHubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amenities>(entity =>
            {
                entity.HasKey(e => e.AmenityId);

                entity.Property(e => e.AmenityId).HasColumnName("AmenityID");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.AmenityDescription)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.AmenityName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AmenityRates>(entity =>
            {
                entity.HasKey(e => e.RateId);

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.AmenityId).HasColumnName("AmenityID");

                entity.Property(e => e.Capacity)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FrequencyId).HasColumnName("FrequencyID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<BillingOverallTransaction>(entity =>
            {
                entity.HasKey(e => e.BillingId);

                entity.ToTable("billing_OverallTransaction");

                entity.Property(e => e.BillingId).HasColumnName("BillingID");

                entity.Property(e => e.AmountPaid).HasDefaultValueSql("((0))");

                entity.Property(e => e.BillingDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CashierUser)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.TotalBill).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<BillingTransMiscellaneous>(entity =>
            {
                entity.HasKey(e => e.TranMiscId);

                entity.ToTable("billing_TransMiscellaneous");

                entity.Property(e => e.TranMiscId).HasColumnName("TranMisc_Id");

                entity.Property(e => e.TranMiscDescription)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BillingTransOthers>(entity =>
            {
                entity.HasKey(e => e.TranOtherId);

                entity.ToTable("billing_TransOthers");

                entity.Property(e => e.TranOtherId).HasColumnName("TranOtherID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Srp).HasColumnName("SRP");
            });

            modelBuilder.Entity<BookingType>(entity =>
            {
                entity.Property(e => e.BookingTypeId).HasColumnName("BookingTypeID");

                entity.Property(e => e.BookingTypeDesc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookLog>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.AddedBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.AmenityId).HasColumnName("AmenityID");

                entity.Property(e => e.BookingRefCode)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BookingTypeId).HasColumnName("BookingTypeID");

                entity.Property(e => e.ClientId)
                    .HasColumnName("ClientID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeOfArrival).HasColumnType("datetime");

                entity.Property(e => e.DateTimeOfDeparture).HasColumnType("datetime");

                entity.Property(e => e.FrequencyId).HasColumnName("FrequencyID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.RateId).HasColumnName("RateID");
            });

            modelBuilder.Entity<ClientMaster>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.AddedBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Profession)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailRecipients>(entity =>
            {
                entity.HasKey(e => e.RecipientId);

                entity.Property(e => e.RecipientId)
                    .HasColumnName("RecipientID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Frequencies>(entity =>
            {
                entity.HasKey(e => e.FrequencyId);

                entity.Property(e => e.FrequencyId)
                    .HasColumnName("FrequencyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FrequencyDescription)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.FrequencyName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvAddProducts>(entity =>
            {
                entity.HasKey(e => e.RecId);

                entity.ToTable("inv_AddProducts");

                entity.Property(e => e.AddedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            modelBuilder.Entity<InvProductCategories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("inv_ProductCategories");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryDesc)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvProducts>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("inv_Products");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Srp).HasColumnName("SRP");

                entity.Property(e => e.UomId).HasColumnName("uom_Id");
            });

            modelBuilder.Entity<InvQuickInventory>(entity =>
            {
                entity.HasKey(e => e.QuickInvId);

                entity.ToTable("inv_QuickInventory");

                entity.Property(e => e.QuickInvId).HasColumnName("quick_inv_ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            modelBuilder.Entity<InvTransactionOthers>(entity =>
            {
                entity.HasKey(e => e.RecId);

                entity.ToTable("inv_TransactionOthers");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<InvUnitOfMeasure>(entity =>
            {
                entity.HasKey(e => e.UomId);

                entity.ToTable("inv_UnitOfMeasure");

                entity.Property(e => e.UomId)
                    .HasColumnName("uom_Id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UomDesc)
                    .IsRequired()
                    .HasColumnName("uom_Desc")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Photos>(entity =>
            {
                entity.HasKey(e => e.PhotoId);

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.PhotoUrl).HasColumnName("PhotoURL");

                entity.Property(e => e.PublicId).HasColumnName("PublicID");

                entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");
            });

            modelBuilder.Entity<Space>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<TimeTrackingLogger>(entity =>
            {
                entity.HasKey(e => e.TimeTrackerId);

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.LoggedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoggedOutDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");

                entity.Property(e => e.UserLoggedBy)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordResetReason)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole).HasDefaultValueSql("((0))");
            });
        }
    }
}
