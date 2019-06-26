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
            conn.ConnectionString = "Data Source=10.55.234.4;Initial Catalog=db_sicred_h2h_acoescriticas;Persist Security Info=True;User ID=sicred;Password=ps&sicred";
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
