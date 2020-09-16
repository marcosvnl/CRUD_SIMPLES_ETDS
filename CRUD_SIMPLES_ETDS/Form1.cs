using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUD_SIMPLES_ETDS
{
    public partial class Form1 : Form
    {
        SqlConnection conexao;
        SqlCommand comandoSLQ;
        SqlDataReader dr;
        SqlDataAdapter da;
        string strSQL;
        // Priximo passo criar a estaciação da conexão com o banco de dados no evento do Botão "Novo".
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(); // podemos escrever a string de conexão dentro dos ().
                conexao.ConnectionString = Properties.Settings.Default.CTS; // Busca a string de conexão app.config
                strSQL = "INSERT INTO tb_usuario(nome, numero) VALUES (@nome, @numero)";
                comandoSLQ = new SqlCommand(strSQL, conexao);
                comandoSLQ.Parameters.AddWithValue("@nome", txtNome.Text);
                comandoSLQ.Parameters.AddWithValue("@numero", txtNumero.Text);

                conexao.Open();
                comandoSLQ.ExecuteNonQuery(); // executar os comandos
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comandoSLQ = null;
            }
        }

        private void btnExibir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(); // podemos escrever a string de conexão dentro dos ().
                conexao.ConnectionString = Properties.Settings.Default.CTS; // Busca a string de conexão app.config
                strSQL = "SELECT * FROM tb_usuario";
                DataSet dados = new DataSet();
                da = new SqlDataAdapter(strSQL, conexao);
                conexao.Open();
                da.Fill(dados);
                dgvDados.DataSource = dados.Tables[0];
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
            }
        }

        private void btnConsultra_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(); // podemos escrever a string de conexão dentro dos ().
                conexao.ConnectionString = Properties.Settings.Default.CTS; // Busca a string de conexão app.config
                strSQL = "SELECT * FROM tb_usuario WHERE id = @id";
                comandoSLQ = new SqlCommand(strSQL, conexao);
                comandoSLQ.Parameters.AddWithValue("@id", txtId.Text);
                conexao.Open();
                dr = comandoSLQ.ExecuteReader();
                while(dr.Read())
                {
                    txtNome.Text = Convert.ToString(dr["nome"]);
                    txtNumero.Text = Convert.ToString(dr["numero"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comandoSLQ = null;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(); // podemos escrever a string de conexão dentro dos ().
                conexao.ConnectionString = Properties.Settings.Default.CTS; // Busca a string de conexão app.config
                strSQL = "UPDATE tb_usuario SET nome = @nome, numero = @numero WHERE id = @id";
                comandoSLQ = new SqlCommand(strSQL, conexao);
                comandoSLQ.Parameters.AddWithValue("@id", txtId.Text);
                comandoSLQ.Parameters.AddWithValue("@nome", txtNome.Text);
                comandoSLQ.Parameters.AddWithValue("@numero", txtNumero.Text);
                conexao.Open();
                comandoSLQ.ExecuteNonQuery(); // executar os comandos
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comandoSLQ = null;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection(); // podemos escrever a string de conexão dentro dos ().
                conexao.ConnectionString = Properties.Settings.Default.CTS; // Busca a string de conexão app.config
                strSQL = "DELETE tb_usuario WHERE id = @id";
                comandoSLQ = new SqlCommand(strSQL, conexao);
                comandoSLQ.Parameters.AddWithValue("@ID", txtId.Text);
                conexao.Open();
                comandoSLQ.ExecuteNonQuery(); // executar os comandos
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comandoSLQ = null;
            }
        }
    }
}
