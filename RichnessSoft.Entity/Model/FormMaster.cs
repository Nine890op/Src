using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichnessSoft.Entity.Model
{
    public class FormMaster : BaseModel
    {
        public int companyid { get; set; } = default;
        public string code { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string Active { get; set; } 
        public Nullable<DateTime> inactivedate { get; set; }

        public virtual Company Company { get; set; }









    }
}
