using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//The following using statments are needed so that we can connect to the database
using COMP2007_S2016_MidTerm.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // have an if statment to see if we are editing or creating
            if((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTask();
            }
        }
        
        /// <summary>
        /// This Method will get the correct task when edit is clicked on TodoList
        /// </summary>
        protected void GetTask()
        {
            int TaskID = Convert.ToInt32(Request.QueryString["TodoID"]);

            using (TodoConnection db = new TodoConnection())
            {
                // Populate the Todo Details page with the properties of the Task selected
                Todo updateTask = (from Todo in db.Todos
                                   where Todo.TodoID == TaskID
                                   select Todo).FirstOrDefault();
                if(updateTask != null)
                {
                    TodoNameTextBox.Text = updateTask.TodoName;
                    TodoNotesTextBox.Text = updateTask.TodoNotes;
                    if(updateTask.Completed == true)
                    {
                        TodoCompletedCheckBox.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// This method lets you cancel your request to either create or edit a task and brings you back to your list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //redirect to the list page
            Response.Redirect("~/TodoList.aspx");
        }

        /// <summary>
        /// This method lets you create/update your task and redirects you to the list page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // make the connection
            using (TodoConnection db = new TodoConnection())
            {
                Todo newTask = new Todo();
                int TaskID = 0;

                // checked to see if we are editing a task, if yes then update
                if(Request.QueryString.Count > 0)
                {
                    // get the ID from the url
                    TaskID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    //get the current task data
                    newTask = (from Todo in db.Todos
                                where Todo.TodoID == TaskID
                                select Todo).FirstOrDefault();
                }
                newTask.TodoName = TodoNameTextBox.Text;
                newTask.TodoNotes = TodoNameTextBox.Text;
                newTask.Completed = false;
                if(TodoCompletedCheckBox.Checked)
                {
                    newTask.Completed = true;
                }

                // if we didnt edit a task now we save the new one
                if(TaskID == 0)
                {
                    db.Todos.Add(newTask);
                }
                //otherwise we save the changes that we made
                db.SaveChanges();

                //redirect to the list page
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}