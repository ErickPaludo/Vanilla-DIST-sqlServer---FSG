using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanilla.Backsys;


namespace Vanilla
{
    public partial class Logs : Form
    {
        private Database db = new Database();
        private static List<BackLog> logslist = new List<BackLog>();
        private static List<BackLog> listcombo = new List<BackLog>();
        public Logs()
        {
            InitializeComponent();
            if(Util.permissao_user == 1)
            {
                apagar.Visible = true;
            }
        }

        private void AtualizarViaBtn(object sender, EventArgs e)
        {
            AtualizaTable();
        }
        public void AtualizaTable()
        {
            dataGridLog.Rows.Clear();
            logslist.Clear();
            db.RetornaLogs(camppesq.Text, Convert.ToDateTime(dateinicial.Text), Convert.ToDateTime(datefinal.Text));
            foreach (BackLog log in logslist.OrderByDescending(item => item.Date))
            {
                dataGridLog.Rows.Add(log.Login, log.Log, log.Date);
            }
        }
        public void DadosnaLista(int id, string log, DateTime date, int id_user, string name_user)
        {
            logslist.Add(new BackLog(id, log, date, id_user, name_user));
        }
        public void AddnoListCombo(string user)//adiciona user no listcomb
        {
            listcombo.Add(new BackLog(user));
        }
        public void ExibirlistComb()//Exibir user no listcomb
        {

        }
        public void LimparCombo()
        {
            listcombo.Clear();

        }

        private void apagar_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("Deseja excluir os logs?","Exclusão de Logs",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dialogo == DialogResult.Yes)
            {
                Config config = new Config();
                try
                {
                    using (SqlConnection connection = new SqlConnection(config.Lerdados()))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand("dev.vnl_apagar_log", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id_user", SqlDbType.Int).Value = Util.id_user;

                            cmd.ExecuteNonQuery();
                        }
                        connection.Close();
                        AtualizaTable();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
