using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public string NickName { get; set; }
        public string Time { get; set; }
        public string PathPhoto { get; set; }
        public Article() { }
        public Article(DataRow varDr)
        {
            Id = Convert.ToInt32(varDr["Id"]);
            Head= Convert.ToString(varDr["Head"]);
            Body= Convert.ToString(varDr["Body"]);
            AuthorId = Convert.ToInt32(varDr["AuthorId"]);
            NickName = Convert.ToString( varDr["NickName"]);
            // Time = (string)varDr["Id"];
            if(varDr["PathPhoto"] != null && varDr["PathPhoto"] != DBNull.Value)
                PathPhoto = (string)varDr["PathPhoto"];


        }
    }
}
