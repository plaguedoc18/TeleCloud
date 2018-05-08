using ITUniver.TeleCloud.WebCloud.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCloud.WebCloud.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        private string connectionString = "";

        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public T Clone(T obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string condition)
        {
            throw new NotImplementedException();
        }

        internal virtual string GetSelectQuery()
        {
            return "";
        }

        internal virtual T Map(SqlDataReader reader)
        {
            var properties = typeof(T).GetProperties();

            var obj = Activator.CreateInstance<T>();

            foreach (var property in properties)
            {
                var ind = reader.GetOrdinal(property.Name);
                if (!reader.IsDBNull(ind))
                {
                    property.SetValue(obj, reader[property.Name]);
                }
            }

            return obj;
        }

        public IEnumerable<T> Find(string condition)
        {
            string queryString = GetSelectQuery();

            if (!string.IsNullOrWhiteSpace(condition))
            {
                queryString += $" WHERE {condition} ";
            }

            return Find<T>(queryString, Map);
        }

        protected IEnumerable<TObj> Find<TObj>(string queryString, Func<SqlDataReader, TObj> map)
        {
            var items = new List<TObj>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var temp = reader.HasRows;

                while (reader.Read())
                {
                    items.Add(map(reader));
                }

                reader.Close();
            }

            return items;
        }

        public T Load(int id)
        {
            return Find($" [Id] = {id}").FirstOrDefault();
        }


        internal virtual string GetInsertQuery()
        {
            return "";
        }

        internal virtual string GetUpdateQuery()
        {
            return "";
        }


        internal SqlParameter[] InverseMap(T obj)
        {
            var parameters = new List<SqlParameter>();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var val = property.GetValue(obj);
                parameters.Add(new SqlParameter($"@{property.Name}", val));
            }

            return parameters.ToArray();
        }

        public bool Save(T obj)
        {
            if (obj.Id == 0)
            {

                using (var connection = new SqlConnection(connectionString))
                {
                    String query = GetInsertQuery();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddRange(InverseMap(obj));

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            else
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    String query = GetUpdateQuery();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddRange(InverseMap(obj));

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }

            return false;
        }
    }
}