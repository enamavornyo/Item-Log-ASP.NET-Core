using System;
using ItemLog.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemLog.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions)
        { }


        //
        public DbSet<Item> Items { get; set; }
        public DbSet<Requester> Requesters { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Request> Requests { get; set; }

    }
}

