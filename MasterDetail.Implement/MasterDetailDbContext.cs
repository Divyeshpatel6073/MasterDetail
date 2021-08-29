using MasterDetail.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetail.Implement
{
    public class MasterDetailDbContext : DbContext
    {
        public MasterDetailDbContext()
          : base("name=MasterDetailConnectionString")

        {
            //Disable initializer
            Database.SetInitializer<MasterDetailDbContext>(null);

        }

        #region Tables
        public virtual DbSet<tblEmpMst> tblEmpMst { get; set; }
        public virtual DbSet<tblEmpDetail> tblEmpDetail { get; set; }
        #endregion

        #region Views

        #endregion

        #region Store Procedure

        #endregion
    }
}
