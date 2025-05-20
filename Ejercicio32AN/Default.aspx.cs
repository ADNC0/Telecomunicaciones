using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ejercicio32AN
{
    public partial class _Default : System.Web.UI.Page
    {
        private string conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
                LimpiarFormulario();
            }
        }

        private void CargarGrid()
        {
            using (SqlConnection con = new SqlConnection(conexion))
            {
                string query = "SELECT * FROM Llamada ORDER BY id_lla";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLlamadas.DataSource = dt;
                gvLlamadas.DataBind();
            }
        }

        private void LimpiarFormulario()
        {
            txtNumeroOrigen.Text = "";
            txtNumeroDestino.Text = "";
            txtDuracion.Text = "";
            txtTipo.Text = "";
            txtFecha.Text = "";
            txtCosto.Text = "";
            gvLlamadas.EditIndex = -1;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (gvLlamadas.EditIndex == -1)
            {
                using (SqlConnection con = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("AgregarLlamada", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@numero_origen", txtNumeroOrigen.Text.Trim());
                        cmd.Parameters.AddWithValue("@numero_destino", txtNumeroDestino.Text.Trim());
                        cmd.Parameters.AddWithValue("@duracion", int.Parse(txtDuracion.Text.Trim()));
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(txtFecha.Text.Trim()));
                        cmd.Parameters.AddWithValue("@costo", decimal.Parse(txtCosto.Text.Trim()));

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                int id = Convert.ToInt32(gvLlamadas.DataKeys[gvLlamadas.EditIndex].Value);
                using (SqlConnection con = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("EditarLlamada", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_lla", id);
                        cmd.Parameters.AddWithValue("@numero_origen", txtNumeroOrigen.Text.Trim());
                        cmd.Parameters.AddWithValue("@numero_destino", txtNumeroDestino.Text.Trim());
                        cmd.Parameters.AddWithValue("@duracion", int.Parse(txtDuracion.Text.Trim()));
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(txtFecha.Text.Trim()));
                        cmd.Parameters.AddWithValue("@costo", decimal.Parse(txtCosto.Text.Trim()));

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                gvLlamadas.EditIndex = -1;
            }

            CargarGrid();
            LimpiarFormulario();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            gvLlamadas.EditIndex = -1;
            LimpiarFormulario();
            CargarGrid();
        }

        protected void gvLlamadas_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvLlamadas.EditIndex = e.NewEditIndex;
            CargarGrid();

            int id = Convert.ToInt32(gvLlamadas.DataKeys[e.NewEditIndex].Value);
            using (SqlConnection con = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand("BuscarLlamada", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_lla", id);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtNumeroOrigen.Text = dr["numero_origen"].ToString();
                        txtNumeroDestino.Text = dr["numero_destino"].ToString();
                        txtDuracion.Text = dr["duracion"].ToString();
                        txtTipo.Text = dr["tipo"].ToString();
                        txtFecha.Text = Convert.ToDateTime(dr["fecha"]).ToString("yyyy-MM-dd HH:mm");
                        txtCosto.Text = dr["costo"].ToString();
                    }
                }
            }
        }

        protected void gvLlamadas_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvLlamadas.EditIndex = -1;
            LimpiarFormulario();
            CargarGrid();
        }

        protected void gvLlamadas_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            gvLlamadas.EditIndex = -1;
            LimpiarFormulario();
            CargarGrid();
        }

        protected void gvLlamadas_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvLlamadas.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand("EliminarLlamada", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_lla", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LimpiarFormulario();
            CargarGrid();
        }
    }
}
