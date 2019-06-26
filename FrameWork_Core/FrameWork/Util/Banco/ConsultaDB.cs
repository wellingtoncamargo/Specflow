using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FrameWork.Util.Banco
{
    public class ConsultaDB
    {
        Conexao conect = new Conexao();
        SqlCommand cmd = new SqlCommand();
        public String msg = "";
        public ConsultaDB(String Action, String table, String cln = null, String Id = null)
        {
            // Comando SQL -- sqlcommand
            if (Action == "select")
                cmd.CommandText = $"select * from {table} where {cln}='{Id}'";
            else if (Action == "update")
                cmd.CommandText = $"update {table} set {cln} where '{Id}'";
            else if (Action == "delete")
                cmd.CommandText = $"delete from {table} Where {cln} = '{Id}'";
            else if (Action == "proc")
                cmd.CommandText = $"{table}";



            // Conecta com o Banco -- Conexao
            try
            {
                cmd.Connection = conect.conectar();
                //Executa o comando
                //cmd.ExecuteNonQueryAsync();

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    //    //string id = r.GetSqlString(1).ToString();
                    //    //string nome = r.GetSqlString(3).ToString();
                    //    //string cpf = r.GetSqlString(6).ToString();
                    //    //string mae = r.GetSqlString(8).ToString();

                    Console.WriteLine(r[table]);
                }


                // Desconecta
                conect.Desconectar();

                //mmostra a mensagem de erro ou Sucesso
                this.msg = "Consulta realizada!";
            }
            catch
            {
                this.msg = "Erro ao realizar a consulta";
            }
        }
    }
    public class Consulta_SP
    {
        private readonly Conexao conexao;

        public Consulta_SP()
        {
            conexao = new Conexao();
        }

        public string ConsultaSaldoParcela(string idContrato, string idParcela, string idDataPgto)
        {
            var queryParameter = new DynamicParameters();

            queryParameter.Add("@STRING", idParcela, DbType.String);
            queryParameter.Add("@DATA", DateTime.ParseExact(idDataPgto, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.Date);
            var retorno = QueryProcedure<dynamic>(conexao.conectar(), "dbo.SP_Exemplo", queryParameter);
            var res = (JsonConvert.SerializeObject(retorno, Formatting.Indented));
            //Console.WriteLine(res);
            return res;
        }

        public List<T> QueryProcedure<T>(SqlConnection sqlConnection, string procedure, DynamicParameters parameters = null)
        {
            return sqlConnection.Query<T>(procedure, param: parameters, commandType: CommandType.StoredProcedure).ToList();
        }
    }
}
