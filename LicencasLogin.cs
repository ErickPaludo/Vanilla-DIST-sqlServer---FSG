﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanilla
{
    public class LicencasLogin
    {
        Config config = new Config();
        public LicencasLogin()
        {
        }

        public async Task ValidaLicenca()
        {
            bool validador = true;
            do
            {
                await Task.Delay(1100);
                validador = VerificaLogin();
            } while (validador);

        }
        public bool VerificaLogin()
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(config.Lerdados()))
                {
                    connection.Open();

                    using (OracleCommand cmd = new OracleCommand("vnl_prc_verificar_existencia_usuario", connection))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new OracleParameter("v_valor", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = Util.id_user.ToString();
                            cmd.Parameters.Add(new OracleParameter("r_retorno", OracleDbType.Boolean, ParameterDirection.Output));

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Erro ao tentar executar a query.");
                        }

                        bool v_retorno = Convert.ToBoolean(cmd.Parameters["r_retorno"].Value.ToString());

                        if (v_retorno != true)
                        {
                            throw new Exception("Este usuário não está mais conectado na aplicação.");
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Type formTypeToKeepOpen = typeof(LoginFront);

                // Crie uma instância do formulário de login
                LoginFront login = new LoginFront();

                // Itere sobre todas as formas abertas
                foreach (Form childForm in Application.OpenForms.Cast<Form>().ToArray())
                {
                    // Verifique se a forma não é a forma que você quer manter aberta
                    if (childForm.GetType() != formTypeToKeepOpen)
                    {
                        // Se não for, feche a forma
                        childForm.Hide();
                    }
                }

                // Mostrar o formulário de login
                login.Show();
                return false;
            }
        }
    }
}