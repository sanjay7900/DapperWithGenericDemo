using Dapper;
using DapperWithGenericDemo.DapperServiecs;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DapperWithGenericDemo
{
  

    public class DataAccess
    {
        private IDbConnection dbConnection;

        public DataAccess(IDbConnection connection)
        {
            dbConnection = connection;
        }

        public T GetData<T>(string storedProcedureName) where T : class, new()
        {
            dbConnection.Open();

            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                T mainObj = Activator.CreateInstance<T>();
                using (var data = dbConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure))
                {
                    foreach (PropertyInfo property in properties)
                    {
                        Type type = property.PropertyType;
                        var obj = Activator.CreateInstance(type);
                        string columnName = property.Name;
                        var doo = data.Read(type).FirstOrDefault();
                        property.SetValue(mainObj, doo);
                    }
                }

                return mainObj;
            }
            finally
            {
                dbConnection.Close();
            }
        }

    }

}
