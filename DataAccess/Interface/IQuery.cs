namespace FC.WebApi.SQLite.DataAccess.Interface;

public interface IQuery<TModel>
{
    string Message { get; set; }

    IEnumerable<TModel> GetHandler(TModel model);

    Task<IEnumerable<TModel>> GetHandlerAsync(TModel model);
}