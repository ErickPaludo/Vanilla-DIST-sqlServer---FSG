﻿using Aspose.Words.XAttr;
using Oracle.ManagedDataAccess.Client;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanilla.StatusCd;

namespace Vanilla
{
    public partial class Homepage : Form
    {
        Util utilitarios = new Util();
        Database db = new Database();
        public static bool validador = false;
        private Config _config = new Config();
        public Homepage()
        {
            InitializeComponent();
            db.ValidaLicenca();
            Exibir();
        }
        public void ValidaRecebe(bool type)//atribuira true ou false para o validador
        {
            validador = type;
        }
        public void Exibir()
        {
            if (utilitarios.Permissao_user == 0)
            {
                Salterauseradm.Visible = false;
                //containerlabelcad.Visible = false;
                // containercad.Visible = false;
                Badduser.Visible = false;
                Suseron.Visible = false;
            }
            Snameuser.Text = utilitarios.Nome_user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                CadastraCNPJ transportadora = new CadastraCNPJ();
                transportadora.ShowDialog();
         
        }

        private void AbrirItem(object sender, EventArgs e)
        {        
                CadastrarItensFront cadastrarItensFront = new CadastrarItensFront();
                cadastrarItensFront.ShowDialog();       
        }

        private void button5_Click(object sender, EventArgs e)
        {          
                AdicionarUsuariosFront usuarios = new AdicionarUsuariosFront();
                usuarios.ShowDialog();
        }

        private void Homepage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (db.VerificaLogin() == true)
            {
                db.Deslog(0);
            }
            Environment.Exit(0);
        }

        private void Config_User(object sender, EventArgs e)
        {
                AlterarUserCFront altercomun = new AlterarUserCFront();
                altercomun.ExibirDados();
                altercomun.ShowDialog();
        }

        private void AlterarUser(object sender, EventArgs e)
        {      
                AlterarUserCFront altercomun = new AlterarUserCFront();
                altercomun.ExibirDados();
                altercomun.ShowDialog();
                if (validador == true)
                {
                    validador = false;
                    db.Deslog(0);
                }
        }

        private void AddCd(object sender, EventArgs e)
        {
            CadastroCd cd = new CadastroCd();
            cd.AtualizaTabelaRuas();
            cd.ShowDialog();
        }

        private void TrocarConta(object sender, EventArgs e)
        {
            if (db.VerificaLogin() == true)
            {
                db.Deslog(0);
            }
        }

        private void AlterarQualqueruser(object sender, EventArgs e)
        {
                AlterarUsersGeral users = new AlterarUsersGeral();
                users.ExibirDadosTabela();
                users.ShowDialog();
                if (validador)
                {
                    validador = false;
                    db.Deslog(0);
                }
        }

        private void Exit(object sender, EventArgs e)
        {          
                db.Deslog(0);
                Environment.Exit(0);
        }

        private void EndereçoBanco(object sender, EventArgs e)
        {        
                ConfigBank editbank = new ConfigBank();
                editbank.ShowDialog();
                if (validador == true)
                {
                    db.Deslog(0);
                }
        }

        private void VerSobre(object sender, EventArgs e)
        {
            Sobre sobre = new Sobre();
            sobre.Show();
        }

        private void VerEmpresas(object sender, EventArgs e)
        {
                Cursor = Cursors.WaitCursor;
                TabelaEmpresas emp = new TabelaEmpresas(false);
                emp.Carregar();
                emp.Show();
                Cursor = Cursors.Default;
        }

        private void VerLog(object sender, EventArgs e)
        {
                Logs log = new Logs();
                log.LimparCombo();
                db.RetornaUsuarios();
                log.ExibirlistComb();
                log.Show();
        }

        private void ConsultarItens(object sender, EventArgs e)
        {                
                TabelaItens itens = new TabelaItens(false);
                itens.AtualizarItens();
                itens.Show();    
        }

        private void LOgados(object sender, EventArgs e)
        {      
                UserOn logados = new UserOn();
                logados.AtualizaTable();
                logados.Show(); 
        }


        private void nameuser_Click(object sender, EventArgs e)
        {
         
        }

        private void AcessaTableCd(object sender, EventArgs e)
        {
          
                TabelaEnderecos itens = new TabelaEnderecos();
                //   itens.Atualizar();
                itens.Show();
        }

        private void InserirItens(object sender, EventArgs e)
        {
         
                InserirItem itens = new InserirItem();
                itens.Atualizar();
                itens.Show();
            
         
        }

        private void StatusCd(object sender, EventArgs e)
        {
           
                StatusdoCd scd = new StatusdoCd();
                scd.Mostrar();
                scd.Show();
           
        }

        private void ChamaChat(object sender, EventArgs e)
        {
           
                ViewMsg view = new ViewMsg();
                view.Visible = false;
                ModelMsg model = new ModelMsg();
                ControllerMsg controller = new ControllerMsg(view, model);
                view.ShowDialog();
         
        }
    }
}
