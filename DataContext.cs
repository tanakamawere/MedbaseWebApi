﻿using MedbaseComponents.Models;
using Microsoft.EntityFrameworkCore;

namespace MedbaseApi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}
        public DbSet<Question> Questions { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Corrections> Corrections {get; set;}
        public DbSet<Course> Courses { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
