<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ejercicio32AN._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
            background-color: #007ACC;
            color: white;
            font-weight: bold;
        }
        .table-style tr:hover td {
            background-color: #d0e7ff;
        }
    </style>

    <h2>Formulario con Calendario</h2>
    <asp:Panel ID="pnlForm" runat="server">
        <table>
            <tr>
                <td><asp:Label ID="lblNumeroOrigen" runat="server" Text="Número Origen:" /></td>
                <td><asp:TextBox ID="txtNumeroOrigen" runat="server" MaxLength="15" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblNumeroDestino" runat="server" Text="Número Destino:" /></td>
                <td><asp:TextBox ID="txtNumeroDestino" runat="server" MaxLength="15" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblDuracion" runat="server" Text="Duración (min):" /></td>
                <td><asp:TextBox ID="txtDuracion" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblTipo" runat="server" Text="Tipo:" /></td>
                <td><asp:TextBox ID="txtTipo" runat="server" MaxLength="30" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFecha" runat="server" Text="Fecha:" /></td>
                <td>
                    <asp:TextBox ID="txtFecha" runat="server" Width="150px" placeholder="yyyy-MM-dd HH:mm" />
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblCosto" runat="server" Text="Costo:" /></td>
                <td><asp:TextBox ID="txtCosto" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top:10px;">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:GridView ID="gvLlamadas" runat="server" AutoGenerateColumns="False"
        CssClass="table-style"
        OnRowEditing="gvLlamadas_RowEditing" 
        OnRowCancelingEdit="gvLlamadas_RowCancelingEdit" 
        OnRowUpdating="gvLlamadas_RowUpdating" 
        OnRowDeleting="gvLlamadas_RowDeleting" 
        DataKeyNames="id_lla">
        <Columns>
            <asp:BoundField DataField="id_lla" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="numero_origen" HeaderText="Número Origen" />
            <asp:BoundField DataField="numero_destino" HeaderText="Número Destino" />
            <asp:BoundField DataField="duracion" HeaderText="Duración" />
            <asp:BoundField DataField="tipo" HeaderText="Tipo" />
            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField DataField="costo" HeaderText="Costo" DataFormatString="{0:C2}" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" HeaderText="Acciones" />
        </Columns>
    </asp:GridView>
</asp:Content>
