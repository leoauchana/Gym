using MySql.Data.MySqlClient;

namespace MinimalAlumnsManager;

public class Connection
{
    public MySqlConnection ConnectionObject;
    public string ConnectionString { get; set; }

    public Connection()
    {
        ConnectionString = "server=localhost;port=3306;database=alumns_manager;user=root;password=ROOT;";
        ConnectionObject = new MySqlConnection(ConnectionString);
    }

    public async Task<MySqlConnection?> AbrirConexion()
    {
        try
        {
            if(ConnectionObject.State == System.Data.ConnectionState.Closed)
            {
                await ConnectionObject.OpenAsync();
                return ConnectionObject;
            }
        }
        catch (Exception e)
        {
            
        }
        return null;
    }

    public async Task CerrarConexion()
    {
        if (ConnectionObject.State == System.Data.ConnectionState.Open) await ConnectionObject.CloseAsync();
    }


}
