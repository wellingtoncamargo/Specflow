using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FrameWork.Util.Banco
{
    public class Conexao 
    {
        SqlConnection conn = new SqlConnection();
        public Conexao()
        {
            conn.ConnectionString = "Conection_String";
        }

        public SqlConnection conectar()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn;
        }

        public void Desconectar()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
