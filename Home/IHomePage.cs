using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanilla
{
    public interface IHomePage
    {
        void SetController(HomeController controler);
        ToolStripMenuItem UserMenu
        {
            get;set;
        }
       
        ToolStripMenuItem AlterUserAdm
        {
            get; set;
        }

      
        ToolStripMenuItem Sair
        {
            get; set;
        }
        Button CadEmpresas
        {
            get;set;
        }
        Button CadItens
        {
            get; set;
        }
        Button CadUser
        {
            get; set;
        }
        Button ViewLog
        {
            get; set;
        }
        Button ViewEmp
        {
            get; set;
        }
        Button ViewItens
        {
            get; set;
        }   
    }
}
