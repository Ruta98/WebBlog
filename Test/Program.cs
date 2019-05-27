
using System;
using System.Data.SQLite;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var connection = new SQLiteConnection(@"Data Source=C:\WORK\CS\WebBlog\WebBlog\DB\blog.db"))/* "" +
    new SqliteConnectionStringBuilder
    {
        DataSource = @"C:\WORK\CS\WebBlog\WebBlog\DB\blog.db"
    }))*/
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                  /*  var insertCommand = connection.CreateCommand();
                    insertCommand.Transaction = transaction;
                    insertCommand.CommandText = "INSERT INTO message ( text ) VALUES ( $text )";
                    insertCommand.Parameters.AddWithValue("$text", "Hello, World!");
                    insertCommand.ExecuteNonQuery();*/

                    var selectCommand = connection.CreateCommand();
                    selectCommand.Transaction = transaction;
                    selectCommand.CommandText = @"select  a.*,u.NickName from Article a
join users u on a.AuthorId=u.Id
order by id";
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = reader.GetString(0);
                            Console.WriteLine(message);
                        }
                    }

                    transaction.Commit();
                }
            }
        }
    }
}
