using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace JSRestaurantAPI.Models
{
    public class CustomerDetailsModel
    {
        #region SQL Columns
        public int customerId { get; set; }
        public string customerName { get; set; }
        public int numOfOrders { get; set; }
        public double customerBill { get; set; }
        
        SqlConnection conn = new SqlConnection("server=RONDODESKTOP\\TRAININGSERVER; database =JSP1RestaurantDB; integrated security = true;");
        #endregion

        #region Get All Customers
        public List<CustomerDetailsModel> GetCustomerList()
        {
            List<CustomerDetailsModel> cList = new List<CustomerDetailsModel>();
            SqlDataReader readAllCustomers = null;

            SqlCommand cmd_allCustomers = new SqlCommand("select * from Customers", conn);

            try
            {
                conn.Open();
                readAllCustomers = cmd_allCustomers.ExecuteReader();

                while(readAllCustomers.Read())
                {
                    cList.Add(new CustomerDetailsModel()
                    {
                        customerId = Convert.ToInt32(readAllCustomers[0]),
                        customerName = Convert.ToString(readAllCustomers[1]),
                        numOfOrders = Convert.ToInt32(readAllCustomers[2]),
                        customerBill = Math.Round(Convert.ToDouble(readAllCustomers[3]),2)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                readAllCustomers.Close();
                conn.Close();
            }
            return cList;
        }
        #endregion

        #region Search A Customer
        public List<CustomerDetailsModel> GetCustomerID(int id)
        {
            List<CustomerDetailsModel> cList = new List<CustomerDetailsModel>();
            SqlDataReader reader = null;
            SqlCommand cmd_searchCust = new SqlCommand("select * from Customers where cId=@cId", conn);
            cmd_searchCust.Parameters.AddWithValue("@cId", id);

            try
            {
                conn.Open();
                reader = cmd_searchCust.ExecuteReader();

                while (reader.Read())
                {
                    cList.Add(new CustomerDetailsModel()
                    {
                        customerId = Convert.ToInt32(reader[0]),
                        customerName = Convert.ToString(reader[1]),
                        numOfOrders = Convert.ToInt32(reader[2]),
                        customerBill = Math.Round(Convert.ToDouble(reader[3]), 2)
                    });
                }
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
            return cList;
        }
        #endregion

        #region Add Customer
        public string AddNewCustomer(CustomerDetailsModel newCustomer)
        {
            SqlCommand cmd_addCustomer = new SqlCommand("insert into Customers values (@cName, 0, 0)", conn);

            cmd_addCustomer.Parameters.AddWithValue("@cName", newCustomer.customerName);

            try
            {
                conn.Open();
                cmd_addCustomer.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return "Customer Added";
        }
        #endregion
    }
}
