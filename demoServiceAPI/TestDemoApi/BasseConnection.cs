using DemoCasoPracticoShigui.Data;
using Microsoft.EntityFrameworkCore;

namespace TestDemoApi
{
    public class BasseConnection
    {
        protected BBDDCasoPracticoContext getCxData(string strDataConexion)
        {
            var option = new DbContextOptionsBuilder<BBDDCasoPracticoContext>()
                .UseInMemoryDatabase(strDataConexion).Options;

            var dbContext = new BBDDCasoPracticoContext(option);
            return dbContext;
        }
    }
}
