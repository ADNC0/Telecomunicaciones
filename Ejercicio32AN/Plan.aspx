<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="Ejercicio32AN.Plan" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Planes de Tarifa</h2>
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
            border: 1px solid #ccc;
            padding: 14px 16px;
            text-align: center;
            background-color: white;
        }
        .table-style th {
            background-color: black;
            color: white;
            font-weight: bold;
        }
        .table-style tr:hover td {
            background-color: #d0e7ff;
        }
    </style>

    <asp:Panel ID="pnlForm" runat="server">
        <table>
            <tr>
                <td><asp:Label ID="lblNombre" runat="server" Text="Nombre:" /></td>
                <td><asp:TextBox ID="txtNombre" runat="server" MaxLength="50" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblMinutos" runat="server" Text="Minutos:" /></td>
                <td><asp:TextBox ID="txtMinutos" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblDatos" runat="server" Text="Datos (MB):" /></td>
                <td><asp:TextBox ID="txtDatos" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblPrecio" runat="server" Text="Precio:" /></td>
                <td><asp:TextBox ID="txtPrecio" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblRoaming" runat="server" Text="Roaming:" /></td>
                <td><asp:CheckBox ID="chkRoaming" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblRenovacion" runat="server" Text="Renovación:" /></td>
                <td><asp:TextBox ID="txtRenovacion" runat="server" MaxLength="30" /></td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top:10px;">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:GridView ID="gvPlanes" runat="server" AutoGenerateColumns="False" 
        CssClass="table-style"
        DataKeyNames="id_plan"
        OnRowEditing="gvPlanes_RowEditing"
        OnRowCancelingEdit="gvPlanes_RowCancelingEdit"
        OnRowDeleting="gvPlanes_RowDeleting"
        OnRowUpdating="gvPlanes_RowUpdating">
        <Columns>
            <asp:BoundField DataField="id_plan" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="minutos" HeaderText="Minutos" />
            <asp:BoundField DataField="datos" HeaderText="Datos (MB)" />
            <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C2}" />
            <asp:CheckBoxField DataField="roaming" HeaderText="Roaming" ReadOnly="True" />
            <asp:BoundField DataField="renovacion" HeaderText="Renovación" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" HeaderText="Acciones" />
        </Columns>
    </asp:GridView>
</asp:Content>
