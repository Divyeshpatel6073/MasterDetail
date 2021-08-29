using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetail.Model
{
    [Table("tblEmpDetail")]
    public class tblEmpDetail
    {
        [Key]
        public int EmpDetId { get; set; }
        public int RefEmpMstid { get; set; }
        public string EmpAddress { get; set; }

    }
}
