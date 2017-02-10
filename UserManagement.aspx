<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="UserManagement" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .Company-ListView {
            width: 100%;
            height: 150px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" Runat="Server">

    <table>
        <tr>
            <td style="width: 260px;">
                <asp:SqlDataSource ID="DS_UnBound_Companies" runat="server" ConnectionString="<%$ ConnectionStrings:Trax %>"  
                    SelectCommand="select Company_Id, Company_Display_Name from tb_Companies where Company_Id Not In ( select User_Comp_Comp from tb_User_Comp where User_Comp_User = @UserId and User_Comp_Active = 1)" >
                    <SelectParameters>
                        <asp:Parameter Name="UserId" DbType="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:ListBox ID="LB_UnBound_Companies" runat="server" CssClass="Company-ListView" DataSourceID="DS_UnBound_Companies" DataTextField="Company_Display_Name" DataValueField="Company_Id" /> 
            </td>
            <td style="width: 260px;">                
                <asp:SqlDataSource ID="DS_Bound_Companies" runat="server" ConnectionString="<%$ ConnectionStrings:Trax %>"
                    SelectCommand="select User_Comp_Id, Company_Id, Company_Display_Name from tb_User_Comp join tb_Companies on Company_Id = User_Comp_Comp where User_Comp_User = @UserId and User_Comp_Active = 1;">
                    <SelectParameters>
                        <asp:Parameter Name="UserId" DbType="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:ListBox ID="LB_Bound_Companies" runat="server" CssClass="Company-ListView" DataSourceID="DS_Bound_Companies" DataTextField="Company_Display_Name" DataValueField="Company_Id" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="B_Bind_Selected_Company" runat="server" Text="Bind Selected Company To User ->" Width="100%" />
            </td>
            <td>
                <asp:Button ID="B_UBind_Selected_Company" runat="server" Text="UnBind Selected Company From User ->" Width="100%" />

            </td>
        </tr>
    
    </table>

</asp:Content>

