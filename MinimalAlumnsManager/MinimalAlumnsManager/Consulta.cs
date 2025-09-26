using MinimalAlumnsManager.Models;
using MySql.Data.MySqlClient;

namespace MinimalAlumnsManager;

public class Consulta
{
    private Connection _connection;

    public Consulta(Connection connection)
    {
        _connection = connection;
    }




    public async Task<bool> InsertTeacher(ModelDto model)
    {
        var connectionObject = await _connection.AbrirConexion();
        using MySqlTransaction transaction = await connectionObject!.BeginTransactionAsync();
        string sqlUser = $"insert into users (user_id, userName, password) values (0, @UserName, @Password)";
        string sqlTeacher = $"insert into teachers (teacher_id, name, lastName, dni, email, direction, user) values (0, @Name, @LastName, @Dni, @Email, @Direction, @User_id)";
        try
        {
            using (MySqlCommand cmdUser = new MySqlCommand(sqlUser, connectionObject, transaction))
            {
                cmdUser.Parameters.AddWithValue("@UserName", model.User.UserName);
                cmdUser.Parameters.AddWithValue("@Password", model.User.Password);

                await cmdUser.ExecuteNonQueryAsync();
                var idUser = cmdUser.LastInsertedId;
                using (MySqlCommand cmd = new MySqlCommand(sqlTeacher, connectionObject, transaction))
                {
                    cmd.Parameters.AddWithValue("@Name", model.Teacher.Name);
                    cmd.Parameters.AddWithValue("@LastName", model.Teacher.LastName);
                    cmd.Parameters.AddWithValue("@Dni", model.Teacher.Dni);
                    cmd.Parameters.AddWithValue("@Email", model.Teacher.Email);
                    cmd.Parameters.AddWithValue("@Direction", model.Teacher.Direction);
                    cmd.Parameters.AddWithValue("@User_id", idUser);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            await transaction.CommitAsync();
            return true;
        }
        catch(Exception e)
        {
            await transaction.RollbackAsync();
            return false;
        }
        finally{
            connectionObject.Close();
        }
    }

    public async Task<List<ModelDto>?> GetAll()
    {
        var connectionObject = await _connection.AbrirConexion();
        var teacherList = new List<ModelDto>();

        string sql = "select t.name, t.lastName, t.dni, t.email, u.userName, u.password from teachers t INNER JOIN users u ON t.user = u.user_id";
        try
        {
            using (var cmd = new MySqlCommand(sql, connectionObject))
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var newModel = new ModelDto
                        {
                            Teacher = new Teacher
                            {
                                Name = reader["name"]?.ToString() ?? string.Empty,
                                LastName = reader["lastName"]?.ToString() ?? string.Empty,
                                Dni = Convert.ToInt32(reader["dni"]),
                                Email = reader["email"].ToString() ?? string.Empty,
                            },
                            User = new User
                            {
                                UserName = reader["userName"].ToString() ?? string.Empty,
                                Password = reader["password"].ToString() ?? string.Empty
                            }
                        };
                        teacherList.Add(newModel);
                    }
                }
            }
            return teacherList;
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        finally
        {
            await _connection.CerrarConexion();
        }
    }
}
