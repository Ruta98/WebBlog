using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
    }
}
