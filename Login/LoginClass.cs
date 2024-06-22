using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Vanilla
{
    public class LoginClass
    {
        public bool Login(string user, string pass)
        {
            Util util;
            Database db = new Database();
            Config config = new Config();
            using (SqlConnection connection = new SqlConnection(config.Lerdados()))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("dev.vnl_login_user", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("v_user", SqlDbType.NVarChar).Value = user;
                        cmd.Parameters.Add("v_pass", SqlDbType.NVarChar).Value = pass;
                        cmd.Parameters.Add("r_verificador", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("r_msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("r_id_user", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("r_perm", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        connection.Close();
                        string msg = cmd.Parameters["r_msg"].Value.ToString();
                        int id = (int)cmd.Parameters["r_id_user"].Value;
                        bool ver = (bool)cmd.Parameters["r_verificador"].Value;
                        int perm = (int)cmd.Parameters["r_perm"].Value;
                        string name = user;

                        util = new Util();
                        util.DadosUser(id, perm, name);

                        if (!ver)
                        {
                            MessageBox.Show(msg);
                        }

                        return ver;
                    }
                }
                catch (Exception ex)
                {
                    ErrorBox errorBox = new ErrorBox("Favor Verificar a conexao com o banco de dados!", ex.Message);
                    return false;
                }
            }
        }
    }
}
