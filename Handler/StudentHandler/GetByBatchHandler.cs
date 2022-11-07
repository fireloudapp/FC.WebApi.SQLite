using FC.WebApi.SQLite.DataAccess;
using FC.WebApi.SQLite.DataAccess.Interface;
using FC.WebApi.SQLite.Model;
using SqlKata;

namespace FC.WebApi.SQLite.Handler.StudentHandler;

public class GetByBatchHandler: SQLiteDataAccess<Student>, IActionQuery<Student>
{
    #region Variable
    private string _conString;
    private ILogger<object> _logger;
    private PageMetaData _metaData;
    #endregion

    #region Constructor
    public GetByBatchHandler(string connString, ILogger<object> logger, PageMetaData metaData) : base(connString)
    {
        //_config = sqlConfig;
        _conString = connString;
        _logger = logger;
        _metaData = metaData;
    }
    #endregion
    
    #region Not implemented

    public  IEnumerable<Student> GetHandler(Student model)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Get Handler Method
    public async Task<IEnumerable<Student>> GetHandlerAsync(Student model = null)
    {
        #region Get the Pagination, Filter, Search & Sorted Data.
        IBaseAccess<Student> baseAccess = new SQLiteDataAccess<Student>(_conString);
        
        #region Sorting & Pagination
        var query = new Query(typeof(Student).Name)
            .Limit(_metaData.PageSize)
            .Offset(_metaData.Page * 10);
        query = _metaData.SortDirection.Equals("A") ? query.OrderBy(_metaData.SortLabel) : query.OrderByDesc(_metaData.SortLabel);
        #endregion
        
        #region Filtering
        if (!string.IsNullOrEmpty(_metaData.SearchText))
        {
            query.WhereRaw($"\"{_metaData.SearchField}\" LIKE '%{_metaData.SearchText}%'");
        }
        IEnumerable<Student> ctryData = await baseAccess.ExecuteQuery(query);

        _logger.LogInformation($"CountryHander.GetBatchHandler API Executed Count : {ctryData.Count()}");

        #endregion
            
        IEnumerable<Student> datas = ctryData.ToList();
        
        if (datas == null)
        {
            IsError = true;
            ErrorMessage = $"Pagination is not available. No record found/search does not matches.";
            datas = null;
            _logger.LogWarning(ErrorMessage);
        }
        
        return datas;

        #endregion
    }
    #endregion
    
    #region Message & Errors
    public string Message { get; set; }
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; }
    #endregion
}