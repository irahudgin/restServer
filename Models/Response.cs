﻿namespace Admin.Models
{
    public class Response
    {
        // response starts with code
        public int statusCode {  get; set; }

        public string messageCode {  get; set; }

        public Produce produce{ get; set; }

        public List<Produce> produces { get; set;}

        public Sales sale { get; set; }

        public List<Sales> sales { get; set; }
    }
}
