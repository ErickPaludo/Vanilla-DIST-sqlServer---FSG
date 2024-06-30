using Aspose.Words;
using Microsoft.VisualBasic.ApplicationServices;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;

namespace Vanilla
{
    public class UserClass
    {
        Util util = new Util();
        Database db = new Database();
        Config config = new Config();
        private int id;
        private string name;
        private string cpf;
        private string email;
        private string tel;
        private string tel2;
        private string perm;
        private int status;
        private string status_conv;
        private string login;
        private string password;
        private int bloq_user;
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Cpf { get { return cpf; } set { cpf = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Tel { get { return tel; } set { tel = value; } }
        public string Tel2 { get { return tel2; } set { tel2 = value; } }
        public string Perm { get { return perm; } set { perm = value; } }
        public int Status { get { return status; } set { status = value; } }
        public string Status_conv { get { return status_conv; } set { status_conv = value; } }
        public string Login { get { return login; } set { login = value; } }
        public string Password { get { return password; } set { password = value; } }
        public int Bloq_user { get { return bloq_user; } set { bloq_user = value; } }
        public UserClass() { }

        public UserClass(string login)
        {
            this.login = login;
        }

        public UserClass(int id, string name, string cpf, string email, string tel, string tel2, string perm, string status_conv, string login, string password, int bloq_user)
        {
            this.id = id;
            this.name = name;
            this.cpf = cpf;
            this.email = email;
            this.tel = tel;
            this.tel2 = tel2;
            this.perm = perm;
            this.status_conv = status_conv;
            this.login = login;
            this.password = password;
            this.bloq_user = bloq_user;
        }

        public UserClass(string email, string tel, string tel2, string login, string password) : this(email)
        {
            this.tel = tel;
            this.tel2 = tel2;
            this.login = login;
            this.password = password;
        }

        public UserClass(int id, string perm, int status, string login, string password, int bloq_user, string email)
        {
            this.id = id;
            this.perm = perm;
            this.status = status;
            this.login = login;
            this.password = password;
            this.bloq_user = bloq_user;
            this.email = email;
        }

        public UserClass(int id, string login)
        {
            this.id = id;
            this.login = login;
        }

        public void Adduser(string nome, string cpf, string email, string tel, string tel2, string permissao, string status, string user, string pass, bool status_enc_email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(config.Lerdados()))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("dev.vnl_ins_user", connection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id_user", SqlDbType.Int).Value = Util.id_user;
                            cmd.Parameters.Add(new SqlParameter("@v_nome", nome));
                            cmd.Parameters.Add(new SqlParameter("@v_cpf", cpf));
                            cmd.Parameters.Add(new SqlParameter("@v_email", email));
                            cmd.Parameters.Add(new SqlParameter("@v_tel", tel));
                            cmd.Parameters.Add(new SqlParameter("@v_tel_2", tel2));
                            cmd.Parameters.Add(new SqlParameter("@v_perm", permissao));
                            cmd.Parameters.Add(new SqlParameter("@v_status", status));
                            cmd.Parameters.Add(new SqlParameter("@v_login", user));
                            cmd.Parameters.Add(new SqlParameter("@v_pass", pass));

                            cmd.ExecuteNonQuery();
                           // db.AddLog($"USUARIO: {user} | TIPO: {permissao} | FOI CADASTRADO COM SUCESSO!", Util.id_user);
                            MessageBox.Show("Usuario adicionado com sucesso!");
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (status_enc_email == true)
            {
                util.EnviarEmail("Primeiro acesso!", email, $"Olá, seja bem vindo a equipe!\nSeguem abaixo os dados de acesso ao nosso sistema:\n\nUsuário: {user}\nSenha: {pass}\n\n\nVocê pode alterar a senha do seu perfil assim que acessar o sistema, basta acessar a barra superior do menu inicial do sistema, ir no seu nome de usuário > botão direito > alterar conta. Após isso, basta sair e entrar novamente no sistema!");
            }
        }
        public void AlterUserADM(int id, string nome, string email, string tel, string tel2, string login, string pass, string perm, string status)//altera usuário por um terceiro (somente adm)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(config.Lerdados()))
                {
                    connection.Open();
                 
                        using (SqlCommand cmd = new SqlCommand("dev.vnl_edit_useradm", connection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_user", SqlDbType.Int).Value = Util.id_user;
                        cmd.Parameters.Add("v_id", SqlDbType.Int).Value = id;
                            cmd.Parameters.Add("v_nome", SqlDbType.VarChar).Value = nome;
                            ;
                            cmd.Parameters.Add("v_email", SqlDbType.VarChar).Value = email;
                            cmd.Parameters.Add("v_tel", SqlDbType.VarChar).Value = tel;
                            cmd.Parameters.Add("v_tel_2", SqlDbType.VarChar).Value = tel2;
                            cmd.Parameters.Add("v_perm", SqlDbType.VarChar).Value = perm;
                            cmd.Parameters.Add("v_status", SqlDbType.VarChar).Value = status;
                            cmd.Parameters.Add("v_login", SqlDbType.VarChar).Value = login;
                            cmd.Parameters.Add("v_pass", SqlDbType.VarChar).Value = pass;
                            cmd.ExecuteNonQuery();
                        connection.Close();
                        }                      
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ChamaView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(config.Lerdados()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From dev.view_users", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AlterarUsersGeral alteruserc = new AlterarUsersGeral();
                                alteruserc.ArmazenaDados(Convert.ToInt32(reader["id"]), reader["nome"].ToString(), reader["cpf"].ToString(), reader["email"].ToString(), reader["tel"].ToString(), reader["tel_2"].ToString(), reader["perm_form"].ToString(), reader["status_format"].ToString(), reader["login"].ToString(), reader["pass"].ToString(), Convert.ToInt32(reader["bloq"]));

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public UserClass RetornarUserC()
        {
            using (OracleConnection connection = new OracleConnection(config.Lerdados()))
            {
                try
                {
                    connection.Open();
                    using (OracleCommand cmd = new OracleCommand("vnl_pkg_users.vnl_busc_userc", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = util.Id_user;
                        cmd.Parameters.Add("r_user", OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();

                            return new UserClass(reader.GetString(0), reader.GetString(1), !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty, reader.GetString(3), reader.GetString(4));
                            


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new UserClass();
                }
            }
        }
    }
}
