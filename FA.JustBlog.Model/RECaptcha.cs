using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
{
    public class RECaptcha
    {
        public string Key = "<RECaptcha Site Key>";

        public string Secret = "<RECaptcha Secret Key>";
        public string Response { get; set; }
    }
}
