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
                int NumOfTasks = Tasks.Count();
                string showNumOfTasks = NumOfTasks.ToString();

                NumOFTasksLabel.Text = showNumOfTasks;
                // take the results and bind them to the gridview
                TodoGridView.DataSource = Tasks.AsQueryable().OrderBy(SortString).ToList();
                TodoGridView.DataBind();
            }

        }

        /// <summary>
        /// This method will delete the row when the corresponding button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // find out which row was clicked
            int SelectedRow = e.RowIndex;

            //get the Selected ID
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[SelectedRow].Values["TodoID"]);

            //find selected row and delete it
            using (TodoConnection db = new TodoConnection())
            {
                Todo deletedTask = (from Tasks in db.Todos
                                    where Tasks.TodoID == TodoID
                                    select Tasks).FirstOrDefault();
                //remove the task
                db.Todos.Remove(deletedTask);

                //save the changes 
                db.SaveChanges();

                // refresh the page 
                this.GetTask();
            }

        }

        /// <summary>
        /// This method will change the page index of the grid view listing the tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // get the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            // refresh the grid to display new rows from new page number
            this.GetTask();
        }

        /// <summary>
        /// this method will switch the sorting of the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column bu which you want to sort by
            Session["SortColumn"] = e.SortExpression;

            // refresh the grif with new sorting option
            this.GetTask();

            //if you want to toogle the direction of the sort
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DECS" : "ASC";
         }
            /// <summary>
            /// This Method will change the state of the Task (Complete or incomplete) 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        protected void Completed_CheckedChanged(object sender, EventArgs e)
        {
            // When the check box state is changed make a call to the database, and change the state in the database to what it was changed to in the gridview
            // if i get enough time do it 
        }

        /// <summary>
        /// This method lets you change the page size(amount of tasks displayed on one page)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set a new page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the view to display the change
            this.GetTask();
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(IsPostBack)
            {
                if(e.Row.RowType == DataControlRowType.Header)
                {
                    LinkButton linkbutton = new LinkButton();

                    for(int index =0; index <TodoGridView.Columns.Count -1; index++)
                    {
                        if(TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if(Session["SortDirecion"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }
}