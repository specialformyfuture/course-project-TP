using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Course_Project_TP_6.Models;
using System.Diagnostics;

namespace Course_Project_TP_6.DAO
{
    public class RoleDAO : DAO
    {
        public Role GetRecord(int id)
        {
            Connect();
            Role role = new Role();
            try
            {
                SqlCommand command = new SqlCommand(" select * from Role where Role_Id = @Role_Id ", Connection);
                command.Parameters.Add(new SqlParameter(" @Role_Id ", id));
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                role.Role_Id = Convert.ToInt32(reader["Role_Id"]);
                role.Name = Convert.ToString(reader["Name"]);

                reader.Close();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("<GetRecord()> Ошибка при чтении записи: ", ex.Message);
                Console.ReadKey();
            }
            finally
            {
                Disconnect();
            }
            return role;
        }

        public List<Role> GetAllRecords()
        {
            Connect();
            List<Role> roleList = new List<Role>();
            try
            {
                SqlCommand command = new SqlCommand("select * from Role", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Role role = new Role();

                    role.Role_Id = Convert.ToInt32(reader["Role_Id"]);
                    role.Name = Convert.ToString(reader["Name"]);

                    roleList.Add(role);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("<GetAllRecords()> Ошибка при чтении записи: ", ex.Message);
            }
            finally
            {
                Disconnect();
            }
            return roleList;
        }

        public bool UpdateRecord(int id, Role role)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand("update Role set  " +
                                                "Name = @Name, where Role_Id = @Role_Id ", Connection);

                cmd.Parameters.Add(new SqlParameter("@Name", role.Name));
                cmd.Parameters.Add(new SqlParameter("@Role_Id", id));

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("<UpdateRecord()> Ошибка при чтении записи: ", ex.Message);
                result = false;
            }
            finally
            {
                Disconnect();
            }
            return result;
        }

        public bool AddRecord(Role role)
        {
            Connect();
            bool result = true;
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Role (Name) VALUES (@Name)", Connection);

                cmd.Parameters.Add(new SqlParameter("@Name", role.Name));

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("<AddRecord()> Ошибка при чтении записи: ", ex.Message);
                result = false;
            }
            finally
            {
                Disconnect();
            }
            return result;
        }

        public bool DeleteRecord(int id, Role role)
        {
            Connect();
            bool result = true;
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Role WHERE Role_Id = @Role_Id", Connection);
                cmd.Parameters.Add(new SqlParameter("@Role_Id", id));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("<DeleteRecord()> Ошибка при чтении записи: ", ex.Message);
                result = false;
            }
            finally
            {
                Disconnect();
            }
            return result;
        }
    }
}
