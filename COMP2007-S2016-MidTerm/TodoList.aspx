<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
         <div class="row">
             <div class="col-md-offset-2 col-md-8">
                 <h1>To Do List</h1>
                 <a href="TodoDetails.aspx" class="btn btn-success btn-md"><i class="fa fa-plus"></i>Add Task</a>

                 <div>
                     <label for="PageSizeDropDownList">Records Per Page: </label>
                     <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                         AutoPostBack="true" CssClass="btn btn-default btn-sm dropdown-toggle"
                          OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                         <asp:ListItem
                     </asp:DropDownList>
                 </div>

                 <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover"
                     ID="ToDoGridView" AutoGenerateColumns="false" DataKeyNames="TodoID"
                     OnRowDeleting="ToDoGridView_RowDeleting" AllowPaging="true" PageSize="3"
                     OnPageIndexChanging="ToDoGridView_PageIndexChanging" AllowSorting="true"
                     OnSorting="ToDoGridView_Sorting" OnRowDataBound="ToDoGridView_RowDataBound"
                     PagerStyle-CssClass="pagination-ys">
                     <Columns>
                         <asp:BoundField DataField="TodoID" HeaderText="ToDo ID" visible="false" />
                         <asp:BoundField DataField="TodoName" HeaderText="Task" Visible="true" SortExpression="TodoName" />
                         <asp:BoundField DataField="TodoNotes" HeaderText="Notes" Visible="true" SortExpression="TodoNotes" />
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:CheckBox ID="Completed" runat="server" AutoPostBack="true" OnCheckedChanged="Completed_CheckedChanged"/>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:HyperLinkField Headertext="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit"
                             NavigateUrl="~/TodoDetails.aspx" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                              DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}" />
                         <asp:TemplateField ShowHeader="false">
                             <ItemTemplate>
                                 <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" 
                                     OnClientClick="return confirm('Are you sure you want to delete this task?');" 
                                     CssClass="btn btn-danger btn-sm"><i class='fa fa-trash-o fa-lg'></i> Delete</asp:LinkButton>
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </div>
         </div>
     </div>
</asp:Content>
