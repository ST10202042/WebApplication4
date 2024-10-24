using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

namespace WebApplication4.Migrations
{
    [DbContext(typeof(ClaimContext))]
    partial class ClaimContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication4.Models.Claim", b =>
            {
                b.Property<string>("ClaimNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("DateOfClaim")
                    .HasColumnType("datetime2");

                b.Property<string>("Department")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("LecturerName")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<byte[]>("SupportingDocuments")
                    .IsRequired()
                    .HasColumnType("varbinary(max)");

                b.HasKey("ClaimNumber");

                b.ToTable("Claims");
            });

            modelBuilder.Entity("WebApplication4.Models.Module", b =>
            {
                b.Property<string>("ModuleCode")
                    .HasMaxLength(6)
                    .HasColumnType("nvarchar(6)");

                b.Property<decimal>("ClaimAmount")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("ClaimNumber")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ClaimType")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<DateTime>("DateOfClaim")
                    .HasColumnType("datetime2");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("ModuleName")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<decimal>("RatePerHour")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("ModuleCode");

                b.HasIndex("ClaimNumber");

                b.ToTable("Modules");
            });

            modelBuilder.Entity("WebApplication4.Models.Module", b =>
            {
                b.HasOne("WebApplication4.Models.Claim", null)
                    .WithMany("Modules")
                    .HasForeignKey("ClaimNumber");
            });

            modelBuilder.Entity("WebApplication4.Models.Claim", b =>
            {
                b.Navigation("Modules");
            });
#pragma warning restore 612, 618
        }
    }
}
