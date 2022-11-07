using FC.WebApi.SQLite.DataAccess;
using FC.WebApi.SQLite.DataAccess.Interface;
using FC.WebApi.SQLite.Handler.StudentHandler;
using FC.WebApi.SQLite.Model;
using Microsoft.AspNetCore.Mvc;
using Handler = FC.WebApi.SQLite.Handler.StudentHandler;
namespace FC.WebApi.SQLite.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : SQLiteBaseAPI<Student, StudentController>
{
    #region Variables
    //ICommand<Country> _commandCreateHandler;
    //ICommand<Country> _commandUpdateHandler;
    readonly string _connectionString;
    private new readonly ILogger<StudentController> _logger;
    #endregion

    #region Constructor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <param name="connectionService"></param>
    /// <param name="httpContext"></param>
    public StudentController(ILogger<StudentController> logger, IConfiguration configuration,
        IConnectionService connectionService, IHttpContextAccessor httpContext)
        : base(logger, configuration, connectionService, httpContext)
    {
        //If we need to write some business logic then only we need to create CreateHandler else we can use BaseAPI for CRUD.
        //_commandUpdateHandler = new ServiceTicketUpdateHandler(ConnectionString);
        _connectionString = configuration.GetValue<string>("DBSettings:ClientDB");
        _baseAccess = new SQLiteDataAccess<Student>(_connectionString, new TraceDB());
        //For account alone we will use Account Database hence the logic was hot-coded.
        _logger = logger;
    }
    #endregion
   
    #region Get Details
    [Route("Details/{id}")]
    [HttpGet]
    public async Task<ActionResult<Student>> GetDetails(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return  BadRequest();
        }

        return await GetById(int.Parse(id));
        //return await FilterById(m => m.Id == id);//
    }
    #endregion
        
    #region Get By Batch
    [Route("ByBatch")]
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Student>>> GetByPage(PageMetaData metaData)
    {
        IEnumerable<Student> items;
        IActionQuery<Student> query = new GetByBatchHandler(_connectionString, _logger, metaData);
        items = await query.GetHandlerAsync(null);
        BatchResult<Student> batchResult = new BatchResult<Student>()
        {
            Items = items,
            TotalItems = GetCount().Result.Count
        };
        Console.WriteLine("Batch Executed");
    
        return Ok(batchResult);
    }
    #endregion
}