using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DataProvider
{
    public delegate MapperBase<T> MapperFactory<T>();

    public abstract class QueryBase<T>
    {
        readonly string connectionString;
        readonly MapperFactory<T> mapperFactory;
        //private static string m_connectionString = @"Data Source=YOUR_DB_HERE;Initial Catalog=Test;Integrated Security=True";

        protected QueryBase(MapperFactory<T> mapperFactory)
        {
            if (mapperFactory == null)
                throw new ArgumentNullException(nameof(mapperFactory));


            connectionString = WebConfigurationManager.ConnectionStrings["connStr"].ToString();
            this.mapperFactory = mapperFactory;
        }

        protected IDbConnection GetConnection()
        {
            IDbConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public Collection<T> ExecuteReader(string commandText, CommandType commandType, Collection<IDataParameter> commandParams)
        {
            Collection<T> collection = new Collection<T>();

            using (IDbConnection connection = GetConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = commandType;

                foreach(IDataParameter param in commandParams)
                    command.Parameters.Add(param);

                try
                {
                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        try
                        {
                            MapperBase<T> mapper = this.mapperFactory();
                            collection = mapper.MapAll(reader);
                            return collection;
                        }
                        catch
                        {
                            throw;

                            // NOTE: 
                            // consider handling exeption here instead of re-throwing
                            // if graceful recovery can be accomplished
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                catch
                {
                    throw;

                    // NOTE: 
                    // consider handling exeption here instead of re-throwing
                    // if graceful recovery can be accomplished
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
