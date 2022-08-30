using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace vlapp.Control
{
    internal class Database_Functions
    {
        public static MySqlConnection con;
        public static string connectDb() {
            try
            {
                string cs = @"server=192.168.1.71;userid=testpro;password=1234567890;database=testpro";

                con = new MySqlConnection(cs);
                openDb(con);
                string version = con.ServerVersion;
                closeDb(con); 
                return version;
            }
            catch (Exception)
            {
                return "cant connect";
            }
        }

        public static string closeDb(MySqlConnection exCon) {
            try
            {
                exCon.Close();
                return "db closed";
            }
            catch (Exception)
            {
                return "db failed to close";
            }
        }

        public static string openDb(MySqlConnection exCon) {
            try
            {
                exCon.Open();
                return "db is opened";
            }
            catch (Exception)
            {
                return "db failed to open";
            }
        }

        MySqlDataReader excecuteQuery(string query) {
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader;
            openDb(con);
            reader = command.ExecuteReader();
            return reader;
        }

        public string insertData(string tbl, string[] fields, string[] values) {
            if (fields.Length != values.Length)
            {
                return "fields and values must be the same number";
            }
            else {
                try
                {
                    //string query = "INSERT INTO tbl_node(ip,blind_num) VALUES('192.168.1.1','1');";
                    StringBuilder sbQuery = new StringBuilder();
                    sbQuery.Append("INSERT INTO ");
                    sbQuery.Append(tbl);
                    sbQuery.Append('(');
                    for (int x = 0; x < fields.Length; x++)
                    {
                        sbQuery.Append(fields[x]);
                        if (x <= fields.Length - 2)
                        {
                            sbQuery.Append('\u002C'); //comma
                        }
                    }
                    sbQuery.Append(") VALUES(");
                    for (int y = 0; y < values.Length; y++)
                    {
                        sbQuery.Append('\u0027'); //apastrophe
                        sbQuery.Append(values[y]);
                        sbQuery.Append('\u0027'); //apastrophe
                        if (y <= values.Length - 2)
                        {
                            sbQuery.Append('\u002C'); //comma
                        }
                    }
                    sbQuery.Append(");");

                    MySqlDataReader reader = excecuteQuery(sbQuery.ToString());
                    closeDb(con);
                    return "successfully saved";
                }
                catch (Exception)
                {
                    return "saving failed";
                }
            }
        }

        public string updateData(string tbl, string[] fields, string[] values, string[] fCondition, string[] vCondition)
        {
            if (fields.Length != values.Length)
            {
                return "fields and values must be the same count";
            }
            else if (fCondition.Length != vCondition.Length)
            {
                return "condition field and condition values musbt be the same count";
            }
            else {
                try
                {
                    StringBuilder sbQuery = new StringBuilder();
                    sbQuery.Append("UPDATE ");
                    sbQuery.Append(tbl);
                    sbQuery.Append(" SET ");
                    for (int x = 0; x < fields.Length; x++)
                    {
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(fields[x]);
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append('=');
                        sbQuery.Append('\u0022'); //qutation
                        sbQuery.Append(values[x]);
                        sbQuery.Append('\u0022'); //qutation
                        if (x <= values.Length - 2)
                        {
                            sbQuery.Append('\u002C'); //comma
                        }
                    }
                    sbQuery.Append(" WHERE ");
                    for (int y = 0; y < fCondition.Length; y++)
                    {
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(fCondition[y]);
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(" = ");
                        sbQuery.Append('\u0022'); //qutation
                        sbQuery.Append(vCondition[y]);
                        sbQuery.Append('\u0022'); //qutation
                        if (y <= fCondition.Length - 2)
                        {
                            sbQuery.Append(" AND ");
                        }
                    }
                    sbQuery.Append(';');

                    MySqlDataReader reader = excecuteQuery(sbQuery.ToString());
                    closeDb(con);
                    return "updated successfully";
                }
                catch (Exception)
                {
                    return "updating failed";
                }
            }
        }

        public string deleteData(string tbl, string[] fCondition, string[] vCondition)
        {
            if (fCondition.Length != vCondition.Length)
            {
                return "condition field and condition values musbt be the same count";
            }
            else
            {
                try
                {
                    StringBuilder sbQuery = new StringBuilder();
                    sbQuery.Append("DELETE ");
                    sbQuery.Append(" FROM ");
                    sbQuery.Append(tbl);
                    sbQuery.Append(" WHERE ");
                    for (int y = 0; y < fCondition.Length; y++)
                    {
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(fCondition[y]);
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(" = ");
                        sbQuery.Append('\u0022'); //qutation
                        sbQuery.Append(vCondition[y]);
                        sbQuery.Append('\u0022'); //qutation
                        if (y <= fCondition.Length - 2)
                        {
                            sbQuery.Append(" AND ");
                        }
                    }
                    sbQuery.Append(';');

                    MySqlDataReader reader = excecuteQuery(sbQuery.ToString());
                    closeDb(con);
                    return "deleted successfully";
                }
                catch (Exception)
                {
                    return "deleting failed";
                }
            }
        }

        ////collectData must be use like this////
        //MySqlDataReader insert = df.collectData(tbl);
        //while (insert.Read()) {
        //    MessageBox.Show(insert.GetString(0)+ " " +insert.GetString(1)+ " " +insert.GetString(2));
        //}
        //Database_Functions.closeDb(Database_Functions.con);
        public MySqlDataReader collectData(string tbl)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT * ");
                sbQuery.Append(" FROM ");
                sbQuery.Append(tbl);
                sbQuery.Append(';');

                return excecuteQuery(sbQuery.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////collectData must be use like this////
        //MySqlDataReader insert = df.collectData(tbl);
        //while (insert.Read()) {
        //    MessageBox.Show(insert.GetString(0)+ " " +insert.GetString(1)+ " " +insert.GetString(2));
        //}
        //Database_Functions.closeDb(Database_Functions.con);
        public MySqlDataReader collectData(string tbl, string[] fields)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT ");
                for (int x = 0; x < fields.Length; x++)
                {
                    sbQuery.Append('\u0060'); //GRAVE ACCENT 
                    sbQuery.Append(fields[x]);
                    sbQuery.Append('\u0060'); //GRAVE ACCENT 
                    if (x <= fields.Length - 2)
                    {
                        sbQuery.Append('\u002C'); //comma
                    }
                }
                sbQuery.Append(" FROM ");
                sbQuery.Append(tbl);
                sbQuery.Append(';');

                return excecuteQuery(sbQuery.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////collectData must be use like this////
        //MySqlDataReader insert = df.collectData(tbl);
        //while (insert.Read()) {
        //    MessageBox.Show(insert.GetString(0)+ " " +insert.GetString(1)+ " " +insert.GetString(2));
        //}
        //Database_Functions.closeDb(Database_Functions.con);
        public MySqlDataReader collectData(string tbl, string[] fields, string[] fCondition, string[] vCondition) {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT ");
                for (int x=0; x < fields.Length; x++) {
                    sbQuery.Append('\u0060'); //GRAVE ACCENT 
                    sbQuery.Append(fields[x]);
                    sbQuery.Append('\u0060'); //GRAVE ACCENT 
                    if (x <= fields.Length - 2)
                    {
                        sbQuery.Append('\u002C'); //comma
                    }
                }
                sbQuery.Append(" FROM ");
                sbQuery.Append(tbl);

                if (fCondition.Length != 0) {
                    sbQuery.Append(" WHERE ");
                    for (int y = 0; y < fCondition.Length; y++) {
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(fCondition[y]);
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append('=');
                        sbQuery.Append('\u0022'); //qutation
                        sbQuery.Append(vCondition[y]);
                        sbQuery.Append('\u0022'); //qutation
                        if (y <= fCondition.Length - 2)
                        {
                            sbQuery.Append(" AND ");
                        }
                    }
                }
                sbQuery.Append(';');

                return excecuteQuery(sbQuery.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
