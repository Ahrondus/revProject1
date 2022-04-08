using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace JSRestaurantAPI.Models
{
    public class OrdersModel
    {
        #region SQL Columns
        public int oId { get; set; }
        public int custOrderId { get; set; }
        public double orderTotal { get; set; }

        SqlConnection conn = new SqlConnection("server=RONDODESKTOP\\TRAININGSERVER; database =JSP1RestaurantDB; integrated security = true;");
        #endregion

        #region Add Order
        public string AddNewOrder(int custID)
        {
            SqlCommand cmd_newOrder = new SqlCommand("insert into Orders values (@oCId, @oTotal)", conn);
            cmd_newOrder.Parameters.AddWithValue("@oCId", custID);
            cmd_newOrder.Parameters.AddWithValue("@oTotal", orderTotal);

            try
            {
                conn.Open();
                cmd_newOrder.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return "Order Added";
        }
        #endregion

        #region List All Orders
        public List<OrdersModel> GetOrderList()
        {
            List<OrdersModel> oList = new List<OrdersModel>();
            SqlDataReader reader = null;

            SqlCommand cmd_listOrders = new SqlCommand("select * from Orders", conn);

            try
            {
                conn.Open();
                reader = cmd_listOrders.ExecuteReader();

                while (reader.Read())
                {
                    oList.Add(new OrdersModel()
                    {
                        oId = Convert.ToInt32(reader[0]),
                        custOrderId = Convert.ToInt32(reader[1]),
                        orderTotal = Math.Round(Convert.ToDouble(reader[2]),2)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
            return oList;
        }
        #endregion

        #region Find an Order
        public List<OrdersModel> FindAnOrder(int id)
        {
            List<OrdersModel> oList = new List<OrdersModel>();
            SqlDataReader reader = null;

            SqlCommand cmd_findOrder = new SqlCommand("select * from Orders where oId=@oId", conn);
            cmd_findOrder.Parameters.AddWithValue("@oId", id);

            try
            {
                conn.Open();
                reader = cmd_findOrder.ExecuteReader();

                while(reader.Read())
                {
                    oList.Add(new OrdersModel()
                    {
                        oId = Convert.ToInt32(reader[0]),
                        custOrderId = Convert.ToInt32(reader[1]),
                        orderTotal = Math.Round(Convert.ToDouble(reader[2]), 2)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
            return oList;
        }
        #endregion

        #region Find All Customer Orders
        public List<OrdersModel> FindCustomerOrders(int id)
        {
            List<OrdersModel> oList = new List<OrdersModel>();
            SqlDataReader reader = null;

            SqlCommand cmd_findOrder = new SqlCommand("select * from Orders where oCId=@oCId", conn);
            cmd_findOrder.Parameters.AddWithValue("@oCId", id);

            try
            {
                conn.Open();
                reader = cmd_findOrder.ExecuteReader();

                while (reader.Read())
                {
                    oList.Add(new OrdersModel()
                    {
                        oId = Convert.ToInt32(reader[0]),
                        custOrderId = Convert.ToInt32(reader[1]),
                        orderTotal = Math.Round(Convert.ToDouble(reader[2]), 2)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
            return oList;
        }
        #endregion
    }
}
