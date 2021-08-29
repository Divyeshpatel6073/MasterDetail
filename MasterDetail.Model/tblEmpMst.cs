using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasterDetail.Model
{
    [Table("tblEmpMst")]
    public class tblEmpMst
    {
        [Key]
        public int EmpMstId { get; set; }
        public string EmpName { get; set; }
        public string EmpMob { get; set; }

        [NotMapped]
        public List<tblEmpDetail> objtblEmpDetailList { get; set; } = new List<tblEmpDetail>();
    }
}
