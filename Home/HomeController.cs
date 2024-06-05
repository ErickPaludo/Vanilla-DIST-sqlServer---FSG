using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vanilla
{
    public class HomeController
    {
        IHomePage viewhome;
        HomeModel model;
        Database db = new Database();
        private CadastrarItensFront itensForm;
        public HomeController()
        {
        }
        public HomeController(IHomePage viewhome, HomeModel model)
        {
            this.viewhome = viewhome;
            this.model = model;
            viewhome.SetController(this);
        }

        #region Menu
        public void UsersOn()
        {
            UserOn logados = new UserOn();
            logados.AtualizaTable();
            logados.Show();
        }
        public void AlterUserComplet()
        {
            AlterarUsersGeral users = new AlterarUsersGeral();
            users.ExibirDadosTabela();
            users.ShowDialog();
        }
        public void AlterUser()
        {
            AlterarUserCFront altercomun = new AlterarUserCFront();
            altercomun.ExibirDados();
            altercomun.Show();
        }
        public void TrocarConta()
        {
            db.Deslog(0);
        }
        public void Sair()
        {
            db.Deslog(0);
            Environment.Exit(0);
        }
        #endregion

        #region Cadastros
        public void StartCadEmp()
        {
            CadastraCNPJ emp = new CadastraCNPJ();
            viewhome.CadEmpresas.Enabled = false;
            model.AddJanela("Cadastra Empresa");
       
            emp.Show();

            emp.FormClosed += (s, args) =>
            {
                viewhome.CadEmpresas.Enabled = true;
                model.RemJanela("Cadastra Empresa");
            
            };

        }
        public void StartCadItens()
        {
            CadastrarItensFront cadastrarItensFront = new CadastrarItensFront();
            viewhome.CadItens.Enabled = false;
            model.AddJanela("Cadastra Itens");
         
            cadastrarItensFront.Show();
            cadastrarItensFront.FormClosed += (s, args) =>
            {
                viewhome.CadItens.Enabled = true;
                model.RemJanela("Cadastra Itens");
             
            };

        }
        public void StartCadUser()
        {
            AdicionarUsuariosFront usuarios = new AdicionarUsuariosFront();
            viewhome.CadUser.Enabled = false;
            model.AddJanela("Cadastra Usuários");
       
            usuarios.Show();
            usuarios.FormClosed += (s, args) =>
            {
                viewhome.CadUser.Enabled = true;
                model.RemJanela("Cadastra Usuários");
              
            };
        }
        #endregion

        #region Visualização

        public void StartViewLogs()
        {
            Logs log = new Logs();
            log.LimparCombo();
            db.RetornaUsuarios();
            log.ExibirlistComb();
            log.Show();
        }
        public void StartViewEmps()
        {
            TabelaEmpresas emp = new TabelaEmpresas(false);
            emp.Carregar();
            emp.Show();
        }
        public void StartViewItens()
        {
            TabelaItens itens = new TabelaItens(false);
            itens.AtualizarItens();
            itens.Show();
        }
        #endregion

     

    }
}
