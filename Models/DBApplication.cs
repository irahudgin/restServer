﻿using Npgsql;
using System.Data;

namespace Admin.Models
{
    public class DBApplication
    {

        public Response ProduceSaleUpdate(NpgsqlConnection con, Produce produce)
        {
            con.Open();
            Response response = new Response();
            string Query = "update produce set amount = amount - @amount where product_id = @prodId";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

            cmd.Parameters.AddWithValue("@prodId", produce.productId);
            cmd.Parameters.AddWithValue("@amount", Convert.ToDouble(produce.amount));

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                response.statusCode = 200;
                response.messageCode = "updated successfully";
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "update failed or id wasnt in right form";
            }

            con.Close();

            return response;
        }
        public Response AddSale(NpgsqlConnection con, Sales sale)
        {
            con.Open();
            Response response = new Response();
            string Query = "insert into sale values (default, @custname, @prodId, @amountBought)";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

            cmd.Parameters.AddWithValue("@custname", sale.customerName);
            cmd.Parameters.AddWithValue("@prodId", sale.productId);
            cmd.Parameters.AddWithValue("@amountBought", sale.amountBought);

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Successfully inserted";
                response.sale = sale;
                response.sales = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Something went wrong";
                response.sale = null;
                response.sales = null;
            }
            return response;
        }

        public Response GetProducebyProductId(NpgsqlConnection con, int id)
        {
            Response response = new Response();
            string Query = "select * from produce where product_id = '"+id+"'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Produce produce = new Produce();
                produce.produceId = (int)dt.Rows[0]["produce_id"]; // db column name
                produce.productId = (int)dt.Rows[0]["product_id"]; // db column name
                produce.name = (string)dt.Rows[0]["name"];
                produce.amount = (float)Convert.ToSingle(dt.Rows[0]["amount"]);
                produce.price = (float)Convert.ToSingle(dt.Rows[0]["price"]);


                // confirgure response
                response.statusCode = 200;
                response.messageCode = "Successfully retrieved";
                response.produce = produce;
                response.produces = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data couldnt be found";
                response.produces = null;
                response.produce = null;
            }

            return response;
        }

        public Response GetAllProduce(NpgsqlConnection con)
        {
            string Query = "select * from produce";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // server goingto send the retrieved data entries as response message to client

            Response response = new Response();
            List<Produce> produces = new List<Produce>();

            if (dt.Rows.Count > 0)
            {
                // if rows have values, need loop to add to student list
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Produce produce = new Produce();
                    produce.produceId = (int)dt.Rows[i]["produce_id"]; // db column name
                    produce.productId = (int)dt.Rows[i]["product_id"]; // db column name
                    produce.name = (string)dt.Rows[i]["name"];
                    produce.amount = (float)Convert.ToSingle(dt.Rows[i]["amount"]);
                    produce.price = (float)Convert.ToSingle(dt.Rows[i]["price"]);

                    produces.Add(produce);
                }
            }

            if (produces.Count > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Data retrieved successfully";
                response.produce = null;
                response.produces = produces;

            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data failed to retrieve or maybe the table is empty";
                response.produce = null;
                response.produces = null;
            }
            return response;
        }

        public Response GetProducebyId(NpgsqlConnection con, int id)
        {
            Response response = new Response();
            string Query = "select * from produce where produce_id='" + id + "'";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Produce produce = new Produce();
                produce.produceId = (int)dt.Rows[0]["produce_id"]; // db column name
                produce.productId = (int)dt.Rows[0]["product_id"]; // db column name
                produce.name = (string)dt.Rows[0]["name"];
                produce.amount = (float)Convert.ToSingle(dt.Rows[0]["amount"]);
                produce.price = (float)Convert.ToSingle(dt.Rows[0]["price"]);


                // confirgure response
                response.statusCode = 200;
                response.messageCode = "Successfully retrieved";
                response.produce = produce;
                response.produces = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data couldnt be found";
                response.produces = null;
                response.produce = null;
            }

            return response;
        }

        public Response AddProduce(NpgsqlConnection con, Produce produce)
        {
            con.Open();
            Response response = new Response();
            string Query = "INSERT INTO produce VALUES (default, @prod_name, @prod_id, @prod_amount, @prod_price)";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

            cmd.Parameters.AddWithValue("@prod_name", produce.name);
            cmd.Parameters.AddWithValue("@prod_id", produce.productId);
            cmd.Parameters.AddWithValue("@prod_amount", Convert.ToDouble(produce.amount));
            cmd.Parameters.AddWithValue("@prod_price", (produce.price).ToString());

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Successfully inserted";
                response.produce = produce;
                response.produces = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Something went wrong";
                response.produce = null;
                response.produces = null;
            }
            return response;
        }

        public Response UpdateProduce(NpgsqlConnection con, Produce produce)
        {
            con.Open();
            Response response = new Response();
            string Query = "UPDATE produce SET name = @prod_name, product_id = @prod_id, amount = @prod_amount, price = @prod_price WHERE produce_id = @ID";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

            cmd.Parameters.AddWithValue("@prod_name", produce.name.ToString());
            cmd.Parameters.AddWithValue("@prod_id", produce.productId);
            cmd.Parameters.AddWithValue("@prod_amount", Convert.ToDouble(produce.amount));
            cmd.Parameters.AddWithValue("@prod_price", (produce.price).ToString());
            cmd.Parameters.AddWithValue("@ID", produce.produceId);

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                response.statusCode = 200;
                response.messageCode = "updated successfully";
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "update failed or id wasnt in right form";
            }

            con.Close();

            return response;
        }

        public Response DeleteProducebyId(NpgsqlConnection con, int id)
        {
            con.Open();
            Response response = new Response();
            string Query = "Delete from produce where produce_id='" + id + "'";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Produce deleted successfully";

            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Produce not found, couldnt delete";
            }

            con.Close();
            return response;
        }

    }
}
