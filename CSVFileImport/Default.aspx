<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CSVFileImport._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <h3>Import or Export Database Data <==> CSV/XML/EXCEL Format.</h3>
   <div>
       <h3>Import or Export Data from csv.</h3>
   <div> 
         <table>
               <tr>
                   <td>Select File : </td>
                   <td>
                       <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                   <td>
                       <asp:Button ID="btnImportFromCSV" runat="server" Text="Import Data to Database" OnClick="btnImportFromCSV_Click" />
                   </td>
               </tr>
         </table>
        <div>
            <br />
             <asp:Label ID="lblMessage" runat="server" Font-Bold="true" />
                <br />
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false">
                    <EmptyDataTemplate>
                        <div style="padding:10px;">No Data Found!</div>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="Account ID" DataField="AccountID" />
                        <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" />
                        <asp:BoundField HeaderText="Account Number" DataField="AccountNumber" />
                        <asp:BoundField HeaderText="Date Of Purchase" DataField="DateOfPurchase" />
                        <asp:BoundField HeaderText="Customer Address" DataField="CustomerAddress" />
                        <asp:BoundField HeaderText="Transaction Values" DataField="TransactionValues" />
                    </Columns>
                </asp:GridView>
               <br />
            <asp:Button ID="btnExportToCSV" runat="server" Text="Export Data CSV" />
            </div>
        </div>
    </div>
</asp:Content>
   
