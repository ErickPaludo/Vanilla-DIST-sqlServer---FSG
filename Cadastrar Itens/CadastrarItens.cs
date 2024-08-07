﻿using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Vanilla
{
    public class CadastrarItens : CadastraCnpjBack
    {
        Database db = new Database();
        Util utilitarios = new Util();
        Config config = new Config();
        #region Propriedades
        private string nome_item;
        private int id_item;
        private decimal preco_custo;
        private decimal lucro_porcent;
        private decimal preco_final;
        private int quantidade;
        private string codbar;
        private int status;
        private string status_conv;
        private string descricao;
        private string unmed;
        private decimal altura;
        private decimal largura;
        private decimal comprimento;
        private decimal cubagem;
        public string Nome_item
        {
            get
            {
                return nome_item;
            }
            set
            {
                nome_item = value;
            }
        }
        public int Id_item
        {
            get
            {
                return id_item;
            }
            set
            {
                id_item = value;
            }
        }
        public decimal Preco_custo
        {
            get
            {
                return preco_custo;
            }
            set
            {
                preco_custo = value;
            }
        }
        public decimal Lucro_porcent
        {
            get
            {
                return lucro_porcent;
            }
            set
            {
                lucro_porcent = value;
            }
        }
        public decimal Preco_final
        {
            get
            {
                return preco_final;
            }
            set
            {
                preco_final = value;
            }
        }
        public int Quantidade
        {
            get
            {
                return quantidade;
            }
            set
            {
                quantidade = value;
            }
        }
        public string Codbar
        {
            get
            {
                return codbar;
            }
            set
            {
                codbar = value;
            }
        }
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public string Status_conv
        {
            get
            {
                return status_conv;
            }
            set
            {
                status_conv = value;
            }
        }
        public string Descricao
        {
            get
            {
                return descricao;
            }
            set
            {
                descricao = value;
            }
        }
        public string Unmed
        {
            get
            {
                return unmed;
            }
            set
            {
                unmed = value;
            }
        }
        public decimal Altura
        {
            get
            {
                return altura;
            }
            set
            {
                altura = value;
            }
        }
        public decimal Largura
        {
            get
            {
                return largura;
            }
            set
            {
                largura = value;
            }
        }
        public decimal Comprimento
        {
            get
            {
                return comprimento;
            }
            set
            {
                comprimento = value;
            }
        }
        public decimal Cubagem
        {
            get
            {
                return cubagem;
            }
            set
            {
                cubagem = value;
            }
        }
        #endregion
        #region Construtores
        public CadastrarItens()
        {
        }

        public CadastrarItens(int id_item, int id_fornec, string nome_f, string nome_item, decimal preco_custo, decimal lucro_porcent, decimal preco_final, string codbar, string status_conv, string descricao, string unmed) : base(nome_f, id_fornec)
        {
            this.nome_item = nome_item;
            this.id_item = id_item;
            this.preco_custo = preco_custo;
            this.lucro_porcent = lucro_porcent;
            this.preco_final = preco_final;
            this.codbar = codbar;
            this.status_conv = status_conv;
            this.descricao = descricao;
            this.unmed = unmed;
        }

        public CadastrarItens(int id_item, int id_fornec, string nome_f, string nome_item, decimal preco_custo, decimal lucro_porcent, decimal preco_final, string codbar, string status_conv, string descricao, string unmed, decimal altura, decimal largura, decimal comprimento, decimal cubagem) : base(nome_f, id_fornec)
        {
            this.nome_item = nome_item;
            this.id_item = id_item;
            this.preco_custo = preco_custo;
            this.lucro_porcent = lucro_porcent;
            this.preco_final = preco_final;
            this.codbar = codbar;
            this.status_conv = status_conv;
            this.descricao = descricao;
            this.unmed = unmed;
            this.altura = altura;
            this.largura = largura;
            this.comprimento = comprimento;
            this.cubagem = cubagem;
        }
        public CadastrarItens(int id_item, int id_fornec, string nome_item, decimal preco_custo, decimal lucro_porcent, decimal preco_final , string status_conv, string descricao, string unmed, decimal altura, decimal largura, decimal comprimento, decimal cubagem) : base(id_fornec)
        {
            this.nome_item = nome_item;
            this.id_item = id_item;
            this.preco_custo = preco_custo;
            this.lucro_porcent = lucro_porcent;
            this.preco_final = preco_final;
            this.status_conv = status_conv;
            this.descricao = descricao;
            this.unmed = unmed;
            this.altura = altura;
            this.largura = largura;
            this.comprimento = comprimento;
            this.cubagem = cubagem;
        }
        public CadastrarItens(int id, string name, int quant) // contrutor da lista de fornecedores de itens
        {
            this.id_item = id;
            this.nome_item = name;
            this.quantidade = quant;
        }

        public CadastrarItens(int id_fornec, string nome, string cnpj) : base(id_fornec, nome, cnpj) // contrutor da lista de fornecedores de itens
        {
        }
        #endregion

        public string GeraCodBarrar() //gera um cod de 13 digitos
        {
            bool verific = false;
            int cod_1, cod_2;
            string cod_bar;
            Random random = new Random();
            do
            {
                cod_1 = random.Next(100000, 9999999); //gera primeira parte do codigo
                cod_2 = random.Next(100000, 999999); //gera segunda parte do codigo
                cod_bar = $"{cod_1}{cod_2}"; //gera codigo completo
                verific = db.BuscarCodBar(cod_bar); //verifica se o codigo ja existe
            } while (verific == false);
            return cod_bar; //retorna o codigo
        }

        public decimal CalculaPrecoFinal(decimal preco_custo, decimal percent_lucro) //Faz o calculo de lucro
        {
            return preco_custo + (preco_custo * (percent_lucro / 100));
        }

        public void CadastraItem(int id_fornecedor, string codigo_barras, string nome, string status, string desc,
            string und_m, decimal preco_custo, decimal margem_lucro, decimal preco_venda, int id_end, double cubagem, double altura, double largura, double comprimento) //Grava no banco de dados
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(config.Lerdados()))
                {
                    connection.Open();


                    using (SqlCommand cmd = new SqlCommand("dev.vnl_ins_item", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_user", SqlDbType.Int).Value = Util.id_user;
                        cmd.Parameters.Add("@v_id_f", SqlDbType.Int).Value = id_fornecedor;
                        cmd.Parameters.Add("@v_cubagem", SqlDbType.Decimal).Value = cubagem;
                        cmd.Parameters.Add("@v_altura", SqlDbType.Decimal).Value = altura;
                        cmd.Parameters.Add("@v_largura", SqlDbType.Decimal).Value = largura;
                        cmd.Parameters.Add("@v_comprimento", SqlDbType.Decimal).Value = comprimento;
                        cmd.Parameters.Add("@v_codbar", SqlDbType.VarChar, 15).Value = codigo_barras;
                        cmd.Parameters.Add("@v_name", SqlDbType.VarChar, 100).Value = nome;
                        cmd.Parameters.Add("@v_status", SqlDbType.VarChar, 15).Value = status;
                        cmd.Parameters.Add("@v_desc", SqlDbType.VarChar, 255).Value = desc;
                        cmd.Parameters.Add("@v_und_med", SqlDbType.VarChar, 5).Value = und_m;
                        cmd.Parameters.Add("@v_pre_c", SqlDbType.Decimal).Value = preco_custo;
                        cmd.Parameters.Add("@v_porc_l", SqlDbType.Decimal).Value = margem_lucro;
                        cmd.Parameters.Add("@v_pre_f", SqlDbType.Decimal).Value = preco_venda;
                        cmd.ExecuteNonQuery();
                        //db.AddLog($"ITEM: {nome} | STATUS: {status} | CODBAR: {codigo_barras} | FOI CADASTRADO COM SUCESSO!", Util.id_user);
                    }
                    MessageBox.Show("Item gravado com sucesso!");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EditaItens(CadastrarItens item) //Grava no banco de dados
                    {
            try
            {
                using (SqlConnection connection = new SqlConnection(config.Lerdados()))
                {
                    connection.Open();
                  
                        using (SqlCommand cmd = new SqlCommand("dev.vnl_edit_item", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_user", SqlDbType.Int).Value = Util.id_user;
                        cmd.Parameters.Add(new SqlParameter("@v_id", SqlDbType.Int)).Value = item.id_item;
                            cmd.Parameters.Add(new SqlParameter("@v_id_f", SqlDbType.Int)).Value = item.Id;
                            cmd.Parameters.Add(new SqlParameter("@v_name", SqlDbType.VarChar)).Value = item.nome_item;
                            cmd.Parameters.Add(new SqlParameter("@v_status", SqlDbType.VarChar)).Value = item.status;
                            cmd.Parameters.Add(new SqlParameter("@v_desc", SqlDbType.VarChar)).Value = item.descricao;
                            cmd.Parameters.Add(new SqlParameter("@v_und_med", SqlDbType.VarChar)).Value = item.unmed;
                            cmd.Parameters.Add(new SqlParameter("@v_pre_c", SqlDbType.Decimal)).Value = item.preco_custo;
                            cmd.Parameters.Add(new SqlParameter("@v_porc_l", SqlDbType.Decimal)).Value = item.lucro_porcent;
                            cmd.Parameters.Add(new SqlParameter("@v_pre_f", SqlDbType.Decimal)).Value = item.preco_final;
                            cmd.Parameters.Add(new SqlParameter("@v_altura", SqlDbType.Decimal)).Value = item.altura;
                            cmd.Parameters.Add(new SqlParameter("@v_largura", SqlDbType.Decimal)).Value = item.largura;
                            cmd.Parameters.Add(new SqlParameter("@v_comprimento", SqlDbType.Decimal)).Value = item.comprimento;
                            cmd.Parameters.Add(new SqlParameter("@v_cubagem", item.cubagem));

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Item gravado com sucesso!");
                        }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReturItens()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(config.Lerdados()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From dev.view_itens order by id", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                TabelaItens itens_table = new TabelaItens(true);
                                itens_table.AddNaTabelaItens(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_f"]), reader["nome_fantasia"].ToString(), reader["codbar"].ToString(), reader["nome"].ToString(), reader["descri"].ToString(), reader["und_med"].ToString(), Convert.ToDecimal(reader["preco_custo"]), Convert.ToDecimal(reader["lucro"]), Convert.ToDecimal(reader["preco_final"]), reader["status"].ToString());
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReturItensCuston(string column, string busca)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(config.Lerdados()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From dev.view_itens where {column} LIKE '%{busca}%'", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TabelaItens itens_table = new TabelaItens(true);
                                itens_table.AddNaTabelaItens(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_f"]), reader["nome_fantasia"].ToString(), reader["codbar"].ToString(), reader["nome"].ToString(), reader["descri"].ToString(), reader["und_med"].ToString(), Convert.ToDecimal(reader["preco_custo"]), Convert.ToDecimal(reader["lucro"]), Convert.ToDecimal(reader["preco_final"]), reader["status"].ToString());
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

        public double CalculaMetrosCubicos(double altura, double largura, double comprimento)
        {
            return ((altura / 100) * (largura / 100) * (comprimento / 100));
        }

        public CadastrarItens RetornarItens(int id)
        {

            using (SqlConnection connection = new SqlConnection(config.Lerdados()))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand($"select * from dev.view_itens where id = {id}", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                return new CadastrarItens(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_f"]), reader["nome_fantasia"].ToString(), reader["nome"].ToString(), Convert.ToDecimal(reader["preco_custo"]), Convert.ToDecimal(reader["lucro"]), Convert.ToDecimal(reader["preco_final"]), reader["codbar"].ToString(), reader["status"].ToString(), reader["descri"].ToString(), reader["und_med"].ToString(), Convert.ToDecimal(reader["altura"]), Convert.ToDecimal(reader["largura"]), Convert.ToDecimal(reader["comprimento"]), Convert.ToDecimal(reader["cubagem"]));
                            }
                            return new CadastrarItens();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new CadastrarItens();
                }
            }
        }
    }

}
