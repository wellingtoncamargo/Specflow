using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD_SpecFlow_API.Util.Banco
{
    public class ConsultaDB
    {
        Conexao conect = new Conexao();
        SqlCommand cmd = new SqlCommand();
        public String msg = "";
        public ConsultaDB(String table, String cln, String Id)
        {
            // Comando SQL -- sqlcommand
            cmd.CommandText = $"select top 10 * from {table} where {cln}='{Id}'";

            

            // Conecta com o Banco -- Conexao
            try
            {
                cmd.Connection = conect.conectar();
                //Executa o comando
                //cmd.ExecuteNonQueryAsync();

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string id = r.GetSqlString(1).ToString();
                    string nome = r.GetSqlString(3).ToString();
                    string cpf = r.GetSqlString(6).ToString();
                    string mae = r.GetSqlString(8).ToString();
                    Console.WriteLine($"Cliente: ID: {id}, Nome: {nome}, CPF: {cpf}, Nome da Mae: {mae}");
                                       
                    //Console.WriteLine(r[table]);
                }


                // Desconecta
                conect.Desconectar();

                //mmostra a mensagem de erro ou Sucesso
                this.msg = "Consulta realizada!";
            }
            catch (SqlException e)
            {
                this.msg = "Erro ao realizar a consulta";
            }
        }
    }
}
