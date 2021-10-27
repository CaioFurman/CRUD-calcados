using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MySQL_CRUD
{
    public partial class Form1 : Form
    {
        
        MySqlConnection cn;
        MySqlCommand cmd;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;

        public Form1()
        {
            InitializeComponent();
            
        }

        //INSERIR
        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new MySqlConnection(@"server=localhost;user id=root;password=1234;database=loja_calcados;");
                strSQL = "insert into loja_calcados (Nome, Estoque, Valor, Tipo) values (@Nome, @Estoque, @Valor, @Tipo)";
                cmd = new MySqlCommand(strSQL, cn);

                cmd.Parameters.AddWithValue("@Nome", tbNome.Text);
                cmd.Parameters.AddWithValue("@Estoque", tbEstoque.Text);
                cmd.Parameters.AddWithValue("@Valor", tbValor.Text);
                cmd.Parameters.AddWithValue("@Tipo", cbTipo.Text);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                cn = null;
                cmd = null;
            }

            Limpar();
        }
        
        //ALTERAR
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new MySqlConnection(@"server=localhost;user id=root;password=1234;database=loja_calcados;");
                strSQL = "update loja_calcados set Nome = @Nome, Estoque = @Estoque, Valor = @Valor, Tipo = @Tipo where ID = @ID";
                cmd = new MySqlCommand(strSQL, cn);

                cmd.Parameters.AddWithValue("@ID", tbID.Text);
                cmd.Parameters.AddWithValue("@Nome", tbNome.Text);
                cmd.Parameters.AddWithValue("@Estoque", tbEstoque.Text);
                cmd.Parameters.AddWithValue("@Valor", tbValor.Text);
                cmd.Parameters.AddWithValue("@Tipo", cbTipo.Text);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                cn = null;
                cmd = null;
            }

            Limpar();
        }

        //DELETAR
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new MySqlConnection(@"server=localhost;user id=root;password=1234;database=loja_calcados;");
                strSQL = "delete from loja_calcados where ID = @ID";
                cmd = new MySqlCommand(strSQL, cn);

                cmd.Parameters.AddWithValue("@ID", tbID.Text);
                cmd.Parameters.AddWithValue("@Nome", tbNome.Text);
                cmd.Parameters.AddWithValue("@Estoque", tbEstoque.Text);
                cmd.Parameters.AddWithValue("@Valor", tbValor.Text);
                cmd.Parameters.AddWithValue("@Tipo", cbTipo.Text);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                cn = null;
                cmd = null;
            }

            Limpar();
        }

        //CONSULTAR
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new MySqlConnection(@"server=localhost;user id=root;password=1234;database=loja_calcados;");
                strSQL = "select * from loja_calcados where ID = @ID";
                cmd = new MySqlCommand(strSQL, cn);

                cmd.Parameters.AddWithValue("@ID", tbID.Text);
                cmd.Parameters.AddWithValue("@Nome", tbNome.Text);
                cmd.Parameters.AddWithValue("@Estoque", tbEstoque.Text);
                cmd.Parameters.AddWithValue("@Valor", tbValor.Text);
                cmd.Parameters.AddWithValue("@Tipo", cbTipo.Text);

                cn.Open();

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tbNome.Text = Convert.ToString(dr["Nome"]);
                    tbEstoque.Text = Convert.ToString(dr["Estoque"]);
                    tbValor.Text = Convert.ToString(dr["Valor"]);
                    cbTipo.Text = Convert.ToString(dr["Tipo"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                cn = null;
                cmd = null;
            }

        }

        //EXIBIR
        private void btnExibir_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new MySqlConnection(@"server=localhost;user id=root;password=1234;database=loja_calcados;");
                strSQL = "select * from loja_calcados";

                da = new MySqlDataAdapter(strSQL, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgTabela.DataSource = dt;
                
                ConfigurarTabela();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                cn = null;
                cmd = null;
            }
        }

        private void ConfigurarTabela()
        {
            dgTabela.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12F);
            dgTabela.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14F);
            dgTabela.RowHeadersWidth = 25;

            //ID
            dgTabela.Columns["ID"].HeaderText = "ID";
            dgTabela.Columns["ID"].Width = 50;
            dgTabela.Columns["ID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgTabela.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Nome
            dgTabela.Columns["Nome"].HeaderText = "Nome";
            dgTabela.Columns["Nome"].Width = 325;

            //Tipo
            dgTabela.Columns["Tipo"].HeaderText = "Tipo";
            dgTabela.Columns["Tipo"].Width = 150;

            //Preço
            dgTabela.Columns["Valor"].HeaderText = "Preço";
            dgTabela.Columns["Valor"].Width = 100;
            dgTabela.Columns["Valor"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgTabela.Columns["Valor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Estoque
            dgTabela.Columns["Estoque"].HeaderText = "Estoque";
            dgTabela.Columns["Estoque"].Width = 100;
            dgTabela.Columns["Estoque"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgTabela.Columns["Estoque"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void Limpar()
        {
            tbEstoque.Clear();
            tbID.Clear();
            tbNome.Clear();
            tbValor.Clear();
        }

    }
}
