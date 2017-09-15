using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_test_player_TD.Controllers;
using UI_test_player_TD.DB;

namespace UI_test_player_TD.Model
{
    public class Tela
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Sistema SistemaPai { get; set; }

        private ObservableCollection<AcaoDyn> _acoesPossiveis { get; set; }
        public ObservableCollection<AcaoDyn> acoesPossiveis 
        { 
            get 
            {
                if (_acoesPossiveis == null)
                {
                    _acoesPossiveis = new ObservableCollection<AcaoDyn>(AcaoDyn_DAO.getAllActionsFromTela(this));
                }
                return _acoesPossiveis;
            }
        }

        public Tela(string Nome, Sistema SistemaPai)
        {
            this.Nome = Nome;
            this.SistemaPai = SistemaPai;

            //Salvar No Banco E Receber Novo Id
        }

        public void Salvar()
        {
            Tela_DAO.Salvar(this);
        }

        public void Deletar()
        {
            Tela_DAO.Deletar(this);
        }

        public void Alterar()
        {
            Tela_DAO.Alterar(this);
        }
    }
}
