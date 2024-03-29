﻿// <auto-generated />
using System;
using MedbaseApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedbaseApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MedbaseApi.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("MedbaseApi.Models.Corrections", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("QuestionChild")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<bool>("SuggestedAnswer")
                        .HasColumnType("bit");

                    b.Property<string>("SuggestedExplanation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Corrections");
                });

            modelBuilder.Entity("MedbaseApi.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseRef")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("MedbaseApi.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TopicReference")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("MedbaseApi.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AnswerA")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerB")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerC")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerD")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerE")
                        .HasColumnType("bit");

                    b.Property<string>("ChildA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionMain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TopicRef")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("MedbaseApi.Models.Topic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CourseRef")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TopicRef")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });
#pragma warning restore 612, 618
        }
    }
}
