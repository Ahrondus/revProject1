using System;
using System.Data.SqlClient;

namespace JSRestaurantAPI.Models
{
    public class OrderItemsModel
    {
        #region SQL Columns
        public int oLineId { get; set; }
        public int orderId { get; set; }
        public int itemId { get; set; }

        SqlConnection conn = new SqlConnection("server=RONDODESKTOP\\TRAININGSERVER; database =JSP1RestaurantDB; integrated security = true;");
        #endregion

        #region Add Items to Order
        public string AddItemsToOrder(int orderID, int itemID)
        {
            SqlCommand cmd_addItems = new SqlCommand("insert into OrderItems values (@oRefId,@pRefId)", conn);
            SqlCommand cmd_execute = new SqlCommand("exec prc_Sale " + itemID + ", " + orderID, conn);
            cmd_addItems.Parameters.AddWithValue("@oRefId", orderID);
            cmd_addItems.Parameters.AddWithValue("@pRefId", itemID);

            try
            {
                conn.Open();
                cmd_addItems.ExecuteNonQuery();
                cmd_execute.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return "Items Added to Order";
        }
        #endregion
    }
}
