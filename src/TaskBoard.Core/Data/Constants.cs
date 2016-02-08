namespace TaskBoard.Core.Data
{
    public class SQL
    {
        public const string GetById = "SELECT * FROM {0} WHERE id = @id";

        public const string GetUser = "SELECT * FROM user WHERE username = @username";
    }
}