using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Course_Project_TP_6.Models;
using System.Diagnostics;

namespace Course_Project_TP_6.DAO
{
    public class UsersDAO : DAO
    {
        public Users GetRecord(int id)
        {
            Connect();
            Users users = new Users();
            try
            {
                SqlCommand command = new SqlCommand(" select * from Users where User_Id = @User_Id ", Connection);
                command.Parameters.Add(new SqlParameter(" @User_Id ", id));
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                users.User_Id = Convert.ToInt32(reader["User_Id"]);
                users.UserName = Convert.ToString(reader["UserName"]);
                users.UserLastName = Convert.ToString(reader["UserLastName"]);
                users.UserPatronymic = Convert.ToString(reader["UserPatronymic"]);
                users.CityOfBirth = Convert.ToString(reader["CityOfBirth"]);
                users.UserDateOfBirth = Convert.ToDateTime(reader["UserDateOfBirth"]);
                users.Email = Convert.ToString(reader["Email"]);
                users.Password = Convert.ToString(reader["Password"]);
                users.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);

                reader.Close();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("<GetRecord()> Ошибка при чтении записи: ", ex.Message);
            }
            finally
            {
                Disconnect();
            }
            return users;
        }

        public Users GetRecordId(string email)
        {
            Connect();
            Users users = new Users();
            try
            {
                SqlCommand command = new SqlCommand(" select User_Id from Users where Email = @Email ", Connection);
                command.Parameters.Add(new SqlParameter("@Email", email));
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                users.User_Id = Convert.ToInt32(reader["User_Id"]);
                reader.Close();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("<GetRecord()> Ошибка при чтении записи: ", ex.Message);
            }
            finally
            {
                Disconnect();
            }
            return users;
        }

        public List<Users> GetAllRecords()
        {
            Connect();
            List<Users> usersList = new List<Users>();
            try
            {
                SqlCommand command = new SqlCommand("select * from Users", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Users users = new Users();

                    users.User_Id = Convert.ToInt32(reader["User_Id"]);
                    users.UserName = Convert.ToString(reader["UserName"]);
                    users.UserLastName = Convert.ToString(reader["UserLastName"]);
                    users.UserPatronymic = Convert.ToString(reader["UserPatronymic"]);
                    users.CityOfBirth = Convert.ToString(reader["CityOfBirth"]);
                    users.UserDateOfBirth = Convert.ToDateTime(reader["UserDateOfBirth"]);
                    users.Email = Convert.ToString(reader["Email"]);
                    users.Password = Convert.ToString(reader["Password"]);
                    users.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);

                    usersList.Add(users);
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
            return usersList;
        }

        public bool UpdateRecord(int id, Users users)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand("update Users set  " +
                                                "UserName = @UserName, " +
                                                "UserLastName = @UserLastName, " +
                                                "UserPatronymic = @UserPatronymic, " +
                                                "CityOfBirth = @CityOfBirth, " +
                                                "UserDateOfBirth = @UserDateOfBirth, " +
                                                "Email = @Email, " +
                                                "Password = @Password, " +
                                                "PhoneNumber = @PhoneNumber, " +
                                                "where User_Id = @User_Id ", Connection);

                cmd.Parameters.Add(new SqlParameter("@UserName", users.UserName));
                cmd.Parameters.Add(new SqlParameter("@UserLastName", users.UserLastName));
                cmd.Parameters.Add(new SqlParameter("@UserPatronymic", users.UserPatronymic));
                cmd.Parameters.Add(new SqlParameter("@CityOfBirth", users.CityOfBirth));
                cmd.Parameters.Add(new SqlParameter("@UserDateOfBirth", users.UserDateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@Email", users.Email));
                cmd.Parameters.Add(new SqlParameter("@Password", users.Password));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", users.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@User_Id", id));

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

        public bool AddRecord(Users users)
        {
            Connect();
            bool result = true;
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (UserName, UserLastName, UserPatronymic, CityOfBirth, UserDateOfBirth, Email, Password, PhoneNumber, Gender_Id)" +
                    "VALUES (@UserName, @UserLastName, @UserPatronymic, @CityOfBirth, @UserDateOfBirth, @Email, @Password,  @PhoneNumber, @Gender_Id)", Connection);

                cmd.Parameters.Add(new SqlParameter("@UserName", users.UserName));
                cmd.Parameters.Add(new SqlParameter("@UserLastName", users.UserLastName));
                cmd.Parameters.Add(new SqlParameter("@UserPatronymic", users.UserPatronymic));
                cmd.Parameters.Add(new SqlParameter("@CityOfBirth", users.CityOfBirth));
                cmd.Parameters.Add(new SqlParameter("@UserDateOfBirth", users.UserDateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@Email", users.Email));
                cmd.Parameters.Add(new SqlParameter("@Password", users.Password));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", users.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Gender_Id", users.Gender_Id));
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

        public bool DeleteRecord(int id, Users users)
        {
            Connect();
            bool result = true;
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE User_Id = @User_Id", Connection);
                cmd.Parameters.Add(new SqlParameter("@User_Id", id));
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