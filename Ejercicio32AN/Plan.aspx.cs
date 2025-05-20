using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Ejercicio32AN
{
    public partial class Plan : System.Web.UI.Page
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
                string query = "SELECT * FROM PlanTarifa ORDER BY id_plan";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPlanes.DataSource = dt;
                gvPlanes.DataBind();
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtMinutos.Text = "";
            txtDatos.Text = "";
            txtPrecio.Text = "";
            chkRoaming.Checked = false;
            txtRenovacion.Text = "";
            gvPlanes.EditIndex = -1;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (gvPlanes.EditIndex == -1)
            {
                using (SqlConnection con = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("AgregarPlanTarifa", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@minutos", int.Parse(txtMinutos.Text.Trim()));
                        cmd.Parameters.AddWithValue("@datos", int.Parse(txtDatos.Text.Trim()));
                        cmd.Parameters.AddWithValue("@precio", decimal.Parse(txtPrecio.Text.Trim()));
                        cmd.Parameters.AddWithValue("@roaming", chkRoaming.Checked);
                        cmd.Parameters.AddWithValue("@renovacion", txtRenovacion.Text.Trim());

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                int id = Convert.ToInt32(gvPlanes.DataKeys[gvPlanes.EditIndex].Value);
                using (SqlConnection con = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("EditarPlanTarifa", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_plan", id);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@minutos", int.Parse(txtMinutos.Text.Trim()));
                        cmd.Parameters.AddWithValue("@datos", int.Parse(txtDatos.Text.Trim()));
                        cmd.Parameters.AddWithValue("@precio", decimal.Parse(txtPrecio.Text.Trim()));
                        cmd.Parameters.AddWithValue("@roaming", chkRoaming.Checked);
                        cmd.Parameters.AddWithValue("@renovacion", txtRenovacion.Text.Trim());

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                gvPlanes.EditIndex = -1;
            }

            CargarGrid();
            LimpiarFormulario();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            gvPlanes.EditIndex = -1;
            LimpiarFormulario();
            CargarGrid();
        }

        protected void gvPlanes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPlanes.EditIndex = e.NewEditIndex;
            CargarGrid();

            int id = Convert.ToInt32(gvPlanes.DataKeys[e.NewEditIndex].Value);
            using (SqlConnection con = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand("BuscarPlanTarifa", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_plan", id);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtNombre.Text = dr["nombre"].ToString();
                        txtMinutos.Text = dr["minutos"].ToString();
                        txtDatos.Text = dr["datos"].ToString();
                        txtPrecio.Text = dr["precio"].ToString();
                        chkRoaming.Checked = Convert.ToBoolean(dr["roaming"]);
                        txtRenovacion.Text = dr["renovacion"].ToString();
                    }
                }
            }
        }

        protected void gvPlanes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPlanes.EditIndex = -1;
            LimpiarFormulario();
            CargarGrid();
        }

        protected void gvPlanes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvPlanes.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand("EliminarPlanTarifa", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_plan", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LimpiarFormulario();
            CargarGrid();
        }

        protected void gvPlanes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            gvPlanes.EditIndex = -1;
            LimpiarFormulario();
            CargarGrid();
        }
    }
}
