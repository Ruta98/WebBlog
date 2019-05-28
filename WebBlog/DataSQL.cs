using Newtonsoft.Json;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.Models;


namespace WebBlog
{
    public class DataSQL
    {
        SQLite cSQLite;
        public DataSQL()
        {
            cSQLite = new SQLite(@"DB\blog.db");// "Data Source=C:\\WORK\\CS\\WebBlog\\WebBlog\\DB\\blog.db"
        }
        public List<Article> GetArticles(int parId=int.MaxValue,int parCount=10)
        {
            List<Article> Res = new List<Article>();
            var SQL = @"select  a.Id,a.Head,a.Body,a.AuthorId,u.NickName,a.PathPhoto,a.Time 
    from Article a
        join users u on a.AuthorId=u.Id
     where a.id<:Id
    order by a.id desc LIMIT :nn";
            var Params = new Parameter[] { new Parameter(":Id", parId), new Parameter(":nn", parCount) };
            var ReDB=cSQLite.Execute(SQL, Params);
            foreach(DataRow dr in ReDB.Rows)
            {
                var el = new Article(dr);
               // el.Body = "<pre>"+el.Body.Replace("\n", "</pre><pre>") + "</pre>";
                Res.Add(el);
            }
            string r = JsonConvert.SerializeObject(Res);
                
            return Res;
        }

        public bool UpdateArticle(Article parArticle)
        {
            if(parArticle.Id==0)
                parArticle.Id = Convert.ToInt32(cSQLite.ExecuteScalar("select max(Id)as id from Article")) + 1;
            var Params = new Parameter[] {
                    new Parameter(":Id", parArticle.Id),
                    new Parameter(":Head", parArticle.Head),
                    new Parameter(":Body", parArticle.Body),
                    new Parameter(":AuthorId", parArticle.AuthorId),
                    new Parameter(":PathPhoto",  @"Foto\10")
            };
            var SQL= "replace into Article (Id,Head,Body,AuthorId,PathPhoto) values (:Id,:Head,:Body,:AuthorId,:PathPhoto)";
            cSQLite.ExecuteNonQuery(SQL, Params);
            return true;
        }


        public bool DeleteArticle(int parIdArticle)
        {
            var Params = new Parameter[] {
                    new Parameter(":Id", parIdArticle) 
            };
            var SQL = "delete  from Article where id=:Id";
            cSQLite.ExecuteNonQuery(SQL, Params);
            return true;
        }

        public int  IsLogin(User parUser)
        {
            
            var SQL = @"select u.PassWord from  users u  where upper(u.Email)=:Email";

            var res = cSQLite.ExecuteScalar(SQL,new Parameter[] { new Parameter(":Email", parUser.Email.ToUpper()) });
            if (res == null || res == DBNull.Value)
                return -2;
            if (parUser.PassWord.Equals(res))
            {
                SQL = @"select u.Id from  users u  where upper(u.Email)=:Email";
                res = cSQLite.ExecuteScalar(SQL, new Parameter[] { new Parameter(":Email", parUser.Email.ToUpper()) });
                return Convert.ToInt32(res);
            }
            return -1;

        }

        public int AddUser(User parUser)
        {
            //існує емейл
            var SQL = @"select u.PassWord from  users u  where upper(u.Email)=:Email";
            var res = cSQLite.ExecuteScalar(SQL, new Parameter[] { new Parameter(":Email", parUser.Email.ToUpper()) });
            if (!(res == null || res == DBNull.Value))
                return -2;

            //існує нік
            SQL = @"select u.PassWord from  users u  where upper(u.NickName)=:NickName";
            res = cSQLite.ExecuteScalar(SQL, new Parameter[] { new Parameter(":NickName", parUser.NickName.ToUpper()) });
            if (!(res == null || res == DBNull.Value))
                return -1;

            parUser.Id = Convert.ToInt32(cSQLite.ExecuteScalar("select max(Id)as id from users")) + 1;
            var Params = new Parameter[] {
                    new Parameter(":Id", parUser.Id),
                    new Parameter(":Email", parUser.Email),
                    new Parameter(":PassWord", parUser.PassWord),
                    new Parameter(":NickName", parUser.NickName),
                    new Parameter(":Name", parUser.Name),
            };
            SQL = "replace into users (Id,Email,PassWord,NickName,Name) values (:Id,:Email,:PassWord,:NickName,:Name)";
            cSQLite.ExecuteNonQuery(SQL, Params);
            return parUser.Id;
        }

    }
}
