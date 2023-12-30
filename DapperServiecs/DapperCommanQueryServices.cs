using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace DapperWithGenericDemo.DapperServiecs
{
    public class DapperCommanQueryServices
    {
        private IConfiguration _Conf;
        IDbConnection dbConnection;

        public DapperCommanQueryServices(IConfiguration configuration)
        {
            _Conf=configuration;
            dbConnection = new SqlConnection(_Conf.GetConnectionString("ConnectionSTR"));
            dbConnection.Open();
        }
        public T GetData<T>(T b) 
        {
            dbConnection.Open();

            PropertyInfo[] properties = typeof(T).GetProperties();
            dynamic mainObj= Activator.CreateInstance(typeof(T));
            var data = dbConnection.QueryMultiple("", new { });
            foreach(PropertyInfo property in properties)
            {
                Type type = property.PropertyType;
                var obj = (Parent)Activator.CreateInstance(type);
                string propname=property.Name;
                var da=data.Read<One>().FirstOrDefault();
                mainObj.SetValue(obj, propname);

            }
            return b;
        }



    }
    public class MainData
    {
        public One one { get; set; }    
        public Second second { get; set; }  


    }
}
