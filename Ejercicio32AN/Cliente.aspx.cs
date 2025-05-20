using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejercicio32AN
{
    public partial class Cliente : Page
    {
        string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ClienteTelco", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvClientes.DataSource = dt;
                gvClientes.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                SqlCommand cmd;

                if (string.IsNullOrEmpty(hfIdTec.Value)) // Insertar
                {
                    cmd = new SqlCommand("AgregarClienteTelco", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else // Editar
                {
                    cmd = new SqlCommand("EditarClienteTelco", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_tec", int.Parse(hfIdTec.Value));
                }

                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@plan", txtPlan.Text);
                cmd.Parameters.AddWithValue("@numero", txtNumero.Text);
                cmd.Parameters.AddWithValue("@ciudad", txtCiudad.Text);
                cmd.Parameters.AddWithValue("@estado", txtEstado.Text);
                cmd.Parameters.AddWithValue("@consumo_mensual", decimal.Parse(txtConsumo.Text));

                cmd.ExecuteNonQuery();
            }

            LimpiarFormulario();
            CargarClientes();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hfIdTec.Value = "";
            txtNombre.Text = "";
            txtPlan.Text = "";
            txtNumero.Text = "";
            txtCiudad.Text = "";
            txtEstado.Text = "";
            txtConsumo.Text = "";
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                using (SqlConnection con = new SqlConnection(cadena))
                {
                    SqlCommand cmd = new SqlCommand("BuscarClienteTelco", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_tec", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        hfIdTec.Value = reader["id_tec"].ToString();
                        txtNombre.Text = reader["nombre"].ToString();
                        txtPlan.Text = reader["plan"].ToString();
                        txtNumero.Text = reader["numero"].ToString();
                        txtCiudad.Text = reader["ciudad"].ToString();
                        txtEstado.Text = reader["estado"].ToString();
                        txtConsumo.Text = reader["consumo_mensual"].ToString();
                    }
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                using (SqlConnection con = new SqlConnection(cadena))
                {
                    SqlCommand cmd = new SqlCommand("EliminarClienteTelco", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_tec", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                CargarClientes();
            }
        }
    }
}
