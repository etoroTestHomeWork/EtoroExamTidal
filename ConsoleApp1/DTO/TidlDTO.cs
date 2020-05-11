using OpenTidl.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.DTO
{
    public class TidlDTO
    {

        public OpenTidlSession OpenTidlSession { get; set; }
        public string TidlErrorMessage { get; set; }
        public int? TidlErrorCode { get; set; }

    }
}
