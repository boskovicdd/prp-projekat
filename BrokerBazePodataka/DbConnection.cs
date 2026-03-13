using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;


namespace BrokerBazePodataka
{
    public class DbConnection
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BazaPRP;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        SqlTransaction transaction;

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

        }
        public SqlCommand CreateCommand()
        {
            return new SqlCommand("", connection, transaction);
        }
    }
}