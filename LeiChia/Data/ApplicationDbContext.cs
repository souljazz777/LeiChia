using System;
using System.Collections.Generic;
// 才能使用DbContext
using Microsoft.EntityFrameworkCore;
//才能知道Category是什麼
using LeiChia.Models;
using System.Diagnostics.Metrics;

namespace LeiChia.Data;

public class ApplicationDbContext : DbContext
{
    // basic config for connect the entity framework
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    // create dbset
    public DbSet<Fruit> Fruit { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Detail> Details { get; set; }
}