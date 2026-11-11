using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Sep2026.ConsoleApp4;

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
{
    DataSource = ".",
    InitialCatalog = "sep_2026",
    UserID = "sa",
    Password = "sasa@123",
    TrustServerCertificate = true 
};

SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
sqlConnection.Open();
sqlConnection.Close();

// using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
// {
//     var list = db.Query<StudentDto>("select * from Students").ToList(); 
//     foreach (var std in list)
//     {
//         Console.WriteLine($"No: {std.StudentNo} | Name: {std.Name} | ClassNo: {std.ClassNo}");
//     }
// }

// dapper -> IDbConnection
using IDbConnection db1 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
db1.Open();
// StudentDto list (type -> StudentDto)
var list1 = db1.Query<StudentDto>("select * from Students").ToList(); 

foreach (var std in list1)
{
    Console.WriteLine($"No: {std.StudentNo} | Name: {std.Name} | ClassNo: {std.ClassNo}");
}

// delete
using IDbConnection db2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
db2.Open();
int result = db2.Execute("delete from [dbo].[Students] where [Id] = @Id", new { Id = 1 });

// @ ({}) -> giving parameter 
// to prevent form sql Injection attack


