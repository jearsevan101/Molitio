using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Molitio.MVVM.View;
using Molitio.View.UserControls;
using Npgsql;

namespace Molitio
{
    public class ConnectionToDB
    {
        private NpgsqlConnection conn;
        public event EventHandler DataUpdated;
        //string connstring = "Host=localhost; Port=5432; Username=postgres; Password=postgresql1Evan; Database=MolitioDatabase";
        string connstring = "Host=rain.db.elephantsql.com; Port=5432; Username=vsuvdxqk; Password=N0RNrStZzPjdawPEwqNnA5NtGhhZO-Da; Database=vsuvdxqk;";


        public ConnectionToDB()
        {
            conn = new NpgsqlConnection(connstring);
        }
        private void OnDataUpdated()
        {
            DataUpdated?.Invoke(this, EventArgs.Empty);
        }
        public List<DailyTask> GetDailyTasks()
        {
            List<DailyTask> tasks = new List<DailyTask>();
            try
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM dailyTask_select()", conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id_dailytask"));
                            string time = reader["tasktime"].ToString();
                            string title = reader["taskname"].ToString();
                            bool isDone = reader.GetBoolean(reader.GetOrdinal("isDone"));
                            tasks.Add(new DailyTask { Id = id, Time = time, Title = title, isDone = isDone }) ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw custom exception)
                throw new Exception($"Error retrieving daily tasks: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return tasks;
        }

        public List<NoteItem> GetNoteItemsFromDatabase()
        {
            List<NoteItem> item = new List<NoteItem>();
            try
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Notes_select()", conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id_notes"));
                            string title = reader["notetitle"].ToString();
                            string desc = reader["notedesc"].ToString();
                            string date = reader["notedate"].ToString();
                            

                            item.Add(new NoteItem {NoteTitle = title,NoteDesc = desc, NoteDate = date, NotesID = id });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw custom exception)
                throw new Exception($"Error retrieving daily tasks: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return item;
        }

        public List<ToDoList> GetToDoList()
        {
            List<ToDoList> toDo = new List<ToDoList>();
            try
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM toDoList_select()", conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id_todolist"));
                            string date = reader["taskdate"].ToString();
                            string title = reader["taskName"].ToString();
                            string desc = reader["taskDesc"].ToString();
                            bool isDone = reader.GetBoolean(reader.GetOrdinal("isDone"));
                            toDo.Add(new ToDoList {Id = id, Time = date, Title = title, Description = desc, isDone = isDone });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                // Handle exceptions (e.g., log, throw custom exception)
                throw new Exception($"Error retrieving todolist: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return toDo;
        }
        public void AddTask(string taskName, string taskTime)
        {
            try
            {
                conn.Open();
                TimeSpan timeOfDay = TimeSpan.Parse(taskTime);
                string insertQuery = "INSERT INTO DailyTask (taskName, taskTime, isDone) VALUES (@taskName, @taskTime, false)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@taskName", taskName);
                    cmd.Parameters.AddWithValue("@taskTime", timeOfDay);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Insert successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                OnDataUpdated();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, show error message)
                MessageBox.Show($"Error inserting daily task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        public void AddTask(string taskName, string taskDesc, DateTime taskDate)
        {
            try
            {
                conn.Open();
                string insertQuery = "INSERT INTO ToDoList (taskname, taskdesc, datetask, isdone) VALUES (@taskName, @taskDesc, @taskDate, false)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@taskName", taskName);
                    cmd.Parameters.AddWithValue("@taskDesc", taskDesc);
                    cmd.Parameters.AddWithValue("@taskDate", taskDate);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Insert successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                OnDataUpdated();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, show error message)
                MessageBox.Show($"Error inserting daily task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        public void DeleteDailyTask(int taskId)
        {
            try
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT dailyTask_delete(@taskId)", conn))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    int result = (int)cmd.ExecuteScalar();

                    // Check the result to see if the deletion was successful
                    if (result == 1)
                    {
                        // Notify that data has been updated
                        OnDataUpdated();
                        MessageBox.Show("daily task deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Handle the case where the task was not found (result == 0)
                        MessageBox.Show("Task not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, show error message)
                MessageBox.Show($"Error deleting daily task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        public void DeleteToDoListTask(int taskId)
        {
            try
            {
                conn.Open();
                string deleteQuery = "SELECT todolist_delete(@taskId)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    int result = (int)cmd.ExecuteScalar();
                    if (result == 1)
                    {
                        // Notify that data has been updated
                        OnDataUpdated();
                        MessageBox.Show("todolist task deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Handle the case where the task was not found (result == 0)
                        MessageBox.Show("Task not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw custom exception)
                throw new Exception($"Error deleting ToDoList task: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateDailyTask(int taskId, string newTaskName, string newTaskTime, bool newIsDone)
        {
            try
            {
                conn.Open();
                TimeSpan timeOfDay = TimeSpan.Parse(newTaskTime);
                string updateQuery = "UPDATE dailytask SET taskname = @newTaskName, tasktime = @newTaskTime, isdone = @newIsDone WHERE id_dailytask = @taskId";
                using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@newTaskName", newTaskName);
                    cmd.Parameters.AddWithValue("@newTaskTime", timeOfDay);
                    cmd.Parameters.AddWithValue("@newIsDone", newIsDone);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Daily task updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        OnDataUpdated();
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated. Task not found or no changes made.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw custom exception)
                throw new Exception($"Error updating DailyTask: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateToDoList(int taskId, string newTaskName, string newTaskDesc, DateTime newTaskTime, bool newIsDone)
        {
            try
            {
                conn.Open();
                string updateQuery = "UPDATE todolist SET taskname = @newTaskName, taskdesc = @newTaskDesc, datetask = @newTaskTime, isdone = @newIsDone WHERE id_todolist = @taskId";
                using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@newTaskName", newTaskName);
                    cmd.Parameters.AddWithValue("@newTaskDesc", newTaskDesc);
                    cmd.Parameters.AddWithValue("@newTaskTime", newTaskTime);
                    cmd.Parameters.AddWithValue("@newIsDone", newIsDone);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("ToDoList task updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        OnDataUpdated();
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated. Task not found or no changes made.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw custom exception)
                throw new Exception($"Error updating ToDoList task: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateIsDoneStatus(int taskId, bool newIsDone, bool isTodolist)
        {
            try
            {
                conn.Open();
                string updateQuery;
                if (isTodolist)
                {
                    updateQuery = "UPDATE todolist SET isdone = @newIsDone WHERE id_todolist = @taskId"; ;
                }
                else
                {
                    updateQuery = "UPDATE dailytask SET isdone = @newIsDone WHERE id_dailytask = @taskId";
                }
                using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@newIsDone", newIsDone);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("IsDone status updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        OnDataUpdated();
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated. Data with the specified ID may not exist.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw custom exception)
                MessageBox.Show($"Error updating IsDone status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        public void DeleteNote(int noteId)
        {
            try
            {
                conn.Open();
                string deleteQuery = "SELECT notes_delete(@noteId)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@noteId", noteId);
                    int result = (int)cmd.ExecuteScalar();
                    if (result == 1)
                    {
                        // Notify that data has been updated
                        OnDataUpdated();
                        MessageBox.Show("Note deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Handle the case where the note was not found (result == 0)
                        MessageBox.Show("Note not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw custom exception)
                throw new Exception($"Error deleting note: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }


    }
}
