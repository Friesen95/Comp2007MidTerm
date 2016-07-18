using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//The following using statments are needed so that we can connect to the database
using COMP2007_S2016_MidTerm.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016_MidTerm
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
         //populate the gridview if is nota a postback
         if(!IsPostBack)
            {
                //give it a defaut soting
                Session["SortColumn"] = "TodoName";
                //give a direction for the sorting
                Session["SortDirection"] = "ASC";
                this.GetTask();
            }
               
        }
        /// <summary>
        /// This method will populate the gridview with all the necessary tasks
        /// </summary>
        /// 
        /// @method GetTasks
        /// @returns {void}
        protected void GetTask()
        {
            using (TodoConnection db = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                //with the using statments that we added query the tasks table
                var Tasks = (from allTasks in db.Todos
                             select allTasks);

                // take the results and bind them to the gridview
                ToDoGridView.DataSource = Tasks.AsQueryable().OrderBy(SortString).ToList();
                ToDoGridView.DataBind();
            }
        }

        /// <summary>
        /// This method will delete the row when the corresponding button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        /// <summary>
        /// This method will change the page index of the grid view listing the tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        /// <summary>
        /// this method will switch the sorting of the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {

        }


        protected void ToDoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Completed_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}