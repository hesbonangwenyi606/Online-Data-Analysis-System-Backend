using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FOS.Setup
{
    //public class connection
    //{
    //    private static SqlConnection objconn;
    //    private static SqlDataAdapter oda;
    //    private static SqlCommand cmd;
    //    private static SqlTransaction tran;
    //    private static DataSet ds;

    //    private static string ConnectionString = "Data Source=SHAHZAD-LAPTOP\SQLEXPRESS2012;;initial catalog=FOS_WallCoat; integrated security=True;";

    //    private static void CreatdbConnection()
    //    {
    //        objconn = new SqlConnection(ConnectionString);   //Function of Connection
    //        objconn.Open();
    //    }

    //    private static void ClosedbConnection()
    //    {
    //        objconn.Close();
    //        objconn.Dispose();
    //    }

    //    public static bool SelectTable(DataTable tbl, string query)
    //    {
    //        SqlCommand objcomm = new SqlCommand();
    //        SqlDataAdapter objadapter = new SqlDataAdapter();
    //        try
    //        {
    //            //DataSet ds = new DataSet();
    //            objconn = new SqlConnection(ConnectionString);
    //            objconn.Open();
    //            objcomm.Connection = objconn;
    //            objcomm.CommandText = query;
    //            objadapter.SelectCommand = objcomm;
    //            objadapter.Fill(tbl);
    //            return true;
    //        }
    //        catch (Exception ex) { return false; }
    //        finally
    //        {
    //            objconn.Close();
    //            objconn.Dispose();
    //            objcomm.Clone();
    //            objcomm.Dispose();
    //            objadapter.Dispose();
    //        }
    //    }
    //}
}