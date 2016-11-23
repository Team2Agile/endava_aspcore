using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SampleProject.Data;

namespace SampleProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161123113821_ChangedPriceType")]
    partial class ChangedPriceType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SampleProject.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });
        }
    }
}
