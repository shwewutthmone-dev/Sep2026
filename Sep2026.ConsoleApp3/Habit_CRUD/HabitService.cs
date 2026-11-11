using System;
using MySql.Data.MySqlClient;
using System.Data;
using Sep2026.ConsoleApp3.CommonFunction;

namespace Sep2026.ConsoleApp3.Habit_CRUD;

public class HabitService
{

    // MySQL connection string builder for connecting to the database
    private MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder()
    {
        Server = "localhost",
        Database = "azurelifestyles",
        UserID = "appuser",
        Password = "P@ssw0rd",
    };

    public void ShowAll()
    {
        try
        {
            using var connection = new MySqlConnection(mySqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            using var command =new MySqlCommand("SELECT ComponentTypeCode, ComponentTypeDescription FROM ComponentType", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Code: {reader["ComponentTypeCode"]}, Description: {reader["ComponentTypeDescription"]}");
            }
        }
        catch (MySqlException ex)
        {
            ConsoleHelper.ShowErrorMessage($"MySQL connection failed: {ex.Message}");
        }
    }  
}
