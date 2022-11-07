namespace FC.WebApi.SQLite.DataAccess.Interface;

/// <summary>
/// An Query execution with Error handler
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IActionQuery<TModel> : IQuery<TModel>,  IError
{
    //Task<TModel> GetDetailHandlerAsync(TModel model);
}