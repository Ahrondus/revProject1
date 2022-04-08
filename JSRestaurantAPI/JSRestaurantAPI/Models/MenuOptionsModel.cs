using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace JSRestaurantAPI.Models
{
    public class MenuOptionsModel
    {
        #region SQL Columns
        public int pId { get; set; }
        public string pName { get; set; }
        public double pPrice { get; set; }
        public string pCategory { get; set; }
        public int pQuanity { get; set; }

        SqlConnection conn = new SqlConnection("server=RONDODESKTOP\\TRAININGSERVER; database =JSP1RestaurantDB; integrated security = true;");
        #endregion

        #region Get Menu Items
        public List<MenuOptionsModel> GetProductList(string prodType)
        {
            
            List<MenuOptionsModel> pList = new List<MenuOptionsModel>();
            SqlDataReader readerAllProducts = null;
            #region Get All Products
            if (prodType == null)
            {
                SqlCommand cmd_allProducts = new SqlCommand("select * from MenuOptions", conn);
                
                try
                {
                    conn.Open();
                    readerAllProducts = cmd_allProducts.ExecuteReader();

                    while (readerAllProducts.Read())
                    {
                        pList.Add(new MenuOptionsModel()
                        {
                            pId = Convert.ToInt32(readerAllProducts[0]),
                            pName = Convert.ToString(readerAllProducts[1]),
                            pPrice = Math.Round(Convert.ToDouble(readerAllProducts[2]), 2),
                            pCategory = Convert.ToString(readerAllProducts[3]),
                            pQuanity = Convert.ToInt32(readerAllProducts[4])
                        });
                    }

                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    readerAllProducts.Close();
                    conn.Close();
                }
                return pList;
            }
            #endregion

            #region Get Products By Category
            else
            {
                SqlCommand cmd_allProducts = new SqlCommand("select * from MenuOptions where pCategory = '" + prodType + "'", conn);

                try
                {
                    conn.Open();
                    readerAllProducts = cmd_allProducts.ExecuteReader();

                    while (readerAllProducts.Read())
                    {
                        pList.Add(new MenuOptionsModel()
                        {
                            pId = Convert.ToInt32(readerAllProducts[0]),
                            pName = Convert.ToString(readerAllProducts[1]),
                            pPrice = Math.Round(Convert.ToDouble(readerAllProducts[2]), 2),
                            pCategory = Convert.ToString(readerAllProducts[3]),
                            pQuanity = Convert.ToInt32(readerAllProducts[4])
                        });
                    }

                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    readerAllProducts.Close();
                    conn.Close();
                }
                return pList;
            }
        #endregion

        }
        #endregion

        #region Add New Product
        public string AddNewProduct(string name, double price, string category, int quantitiy)
        {
            SqlCommand cmd_addProduct = new SqlCommand("insert into MenuOptions values (@pName, @pPrice, @pCategory, @pQuantity)", conn);

            cmd_addProduct.Parameters.AddWithValue("@pName", name);
            cmd_addProduct.Parameters.AddWithValue("@pPrice", price);
            cmd_addProduct.Parameters.AddWithValue("@pCategory", category);
            cmd_addProduct.Parameters.AddWithValue("@pQuantity", quantitiy);

            try
            {
                conn.Open();
                cmd_addProduct.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally 
            { 
                conn.Close(); 
            }

            return "Product Added";
        }
        #endregion

        #region Delete Product
        public string GetDeleteProduct(int pId)
        {
            SqlCommand cmd_delete = new SqlCommand("delete from MenuOptions where pId = @pId", conn);
            cmd_delete.Parameters.AddWithValue("@pId", pId);

            try
            {
                conn.Open();
                cmd_delete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return "Product Deleted Successfully";
        }
        #endregion

        #region Update Product 
        public string GetUpdateProduct(MenuOptionsModel updates)
        {
            SqlCommand cmd_update = new SqlCommand("update MenuOptions set pName=@pName, pPrice=@pPrice, pCategory=@pCategory, pQuantity=@pQuantity where pId=@pId", conn);
            cmd_update.Parameters.AddWithValue("@pName", updates.pName);
            cmd_update.Parameters.AddWithValue("@pPrice", updates.pPrice);
            cmd_update.Parameters.AddWithValue("@pCategory", updates.pCategory);
            cmd_update.Parameters.AddWithValue("@pQuantity", updates.pQuanity);
            cmd_update.Parameters.AddWithValue("@pId", updates.pId);

            try
            {
                conn.Open();
                cmd_update.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return "Product Updated";
        }
        #endregion
    }
}
