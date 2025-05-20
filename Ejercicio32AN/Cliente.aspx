<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cliente.aspx.cs" Inherits="Ejercicio32AN.Cliente" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-color: #E6E6FA !important; 
        }
        .table-style {
            border-collapse: separate;
            border-spacing: 0 8px;
            width: 100%;
            max-width: 900px;
            font-family: Arial, sans-serif;
            margin-top: 20px;
            margin-bottom: 20px;
        }
        .table-style th, 
        .table-style td {
            border: 1px solid #4CAF50; /* verde */
            padding: 12px 16px;
            text-align: center;
            background-color: white;
        }
        .table-style th {
            background-color: #4CAF50; /* verde */
            color: white;
            font-weight: bold;
        }
        .table-style tr:hover td {
            background-color: #C8E6C9; 
        }
    </style>

    <h2>Formulario Cliente Telco</h2>

    <asp:HiddenField ID="hfIdTec" runat="server" />

    <table>
        <tr>
            <td><asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label></td>
            <td><asp:TextBox ID="txtNombre" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPlan" runat="server" Text="Plan:"></asp:Label></td>
            <td><asp:TextBox ID="txtPlan" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblNumero" runat="server" Text="Número:"></asp:Label></td>
            <td><asp:TextBox ID="txtNumero" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCiudad" runat="server" Text="Ciudad:"></asp:Label></td>
            <td><asp:TextBox ID="txtCiudad" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label></td>
            <td><asp:TextBox ID="txtEstado" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblConsumo" runat="server" Text="Consumo Mensual:"></asp:Label></td>
            <td><asp:TextBox ID="txtConsumo" runat="server" /></td>
        </tr>
    </table>

    <br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />

    <hr />

    <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" OnRowCommand="gvClientes_RowCommand"
        CssClass="table-style">
        <Columns>
            <asp:BoundField DataField="id_tec" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="plan" HeaderText="Plan" />
            <asp:BoundField DataField="numero" HeaderText="Número" />
            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
            <asp:BoundField DataField="estado" HeaderText="Estado" />
            <asp:BoundField DataField="consumo_mensual" HeaderText="Consumo" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id_tec") %>' Text="Editar" />
                    <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id_tec") %>' Text="Eliminar" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
