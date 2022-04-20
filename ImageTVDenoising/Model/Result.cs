using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageTVDenoising.Model
{
    class Result
    {
        public bool Succes {get; set;}
        public string Message { get; set; }

        public Result()
        {
            Succes = true;
            Message = "";
        }
    }
}
