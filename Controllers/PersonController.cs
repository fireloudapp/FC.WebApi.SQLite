using FC.WebApi.SQLite.DataAccess;
using FC.WebApi.SQLite.DataAccess.Interface;
using FC.WebApi.SQLite.Handler.StudentHandler;
using FC.WebApi.SQLite.Model;
using Microsoft.AspNetCore.Mvc;

namespace FC.WebApi.SQLite.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : SQLiteBaseAPI<Person, PersonController>
{
    #region Variables
    //ICommand<Country> _commandCreateHandler;
    //ICommand<Country> _commandUpdateHandler;
    readonly string _connectionString;
    private new readonly ILogger<PersonController> _logger;
    #endregion

    #region Constructor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <param name="connectionService"></param>
    /// <param name="httpContext"></param>
    public PersonController(ILogger<PersonController> logger, IConfiguration configuration,
        IConnectionService connectionService, IHttpContextAccessor httpContext)
        : base(logger, configuration, connectionService, httpContext)
    {
        //If we need to write some business logic then only we need to create CreateHandler else we can use BaseAPI for CRUD.
        //_commandUpdateHandler = new ServiceTicketUpdateHandler(ConnectionString);
        _connectionString = configuration.GetValue<string>("DBSettings:ClientDB");
        _baseAccess = new SQLiteDataAccess<Person>(_connectionString, new TraceDB());
        
        //For account alone we will use Account Database hence the logic was hot-coded.
        _logger = logger;
        _logger.LogInformation($"Connection String : {_connectionString}");
    }
    #endregion
   
    #region Get Details
    [Route("Details/{id}")]
    [HttpGet]
    public async Task<ActionResult<Person>> GetDetails(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return  BadRequest();
        }

        return await GetById(int.Parse(id));
        //return await FilterById(m => m.Id == id);//
    }
    #endregion
       
}