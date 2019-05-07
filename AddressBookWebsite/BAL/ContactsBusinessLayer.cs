using AddressBookWebsite.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookWebsite.BAL
{
    public class ContactsBusinessLayer
    {
        SqlConnection conn = new SqlConnection();
        public void SaveContactDetails(ContactViewModel contacts)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ContactsDbContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sproc_UpdateContacts", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@avatar", contacts.Avatar);
                cmd.Parameters.AddWithValue("@fullName", contacts.FullName);
                cmd.Parameters.AddWithValue("@age", contacts.Age);
                cmd.Parameters.AddWithValue("@gender", contacts.Gender);
                cmd.Parameters.AddWithValue("@addressOne", contacts.AddressOne);
                cmd.Parameters.AddWithValue("@addressTwo", contacts.AddressTwo);
                cmd.Parameters.AddWithValue("@phone", contacts.Phone);
                cmd.Parameters.AddWithValue("@mobile", contacts.Mobile);
                cmd.Parameters.AddWithValue("@email", contacts.Email);
                cmd.Parameters.AddWithValue("@contactID", contacts.ContactID);
                conn.ConnectionString = connectionString;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}