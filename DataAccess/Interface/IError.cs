namespace FC.WebApi.SQLite.DataAccess.Interface;

public interface IError
{
    bool IsError { get; set; }
    string ErrorMessage { get; set; }
}