using ProjectMudanza.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectMudanza.Database
{
    public class MudanzasDbContext : DbContext
    {

        public MudanzasDbContext() : base("MudanzasContext")
        {

        }

        public virtual DbSet<Mudanza> LogMudanza { get; set; }




    }
}