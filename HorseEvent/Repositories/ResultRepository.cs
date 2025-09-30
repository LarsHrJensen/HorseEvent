namespace HorseEvent.Repositories
{
    public class ResultRepository : IRepository<Result>
    {
        private readonly string _connectionString;

        public ResultRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Result result)
    }
}
