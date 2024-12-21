namespace ZGT.Trouble.API.Controllers
{
    public class GenericController<T, U, V> : ControllerBase where V : DbContext
    {
        protected DbContextOptions<V> options;
        protected readonly ILogger logger;
        protected dynamic manager;

        public GenericController(ILogger logger, DbContextOptions<V> options)
        {
            this.options = options;
            this.logger = logger;
            this.manager = (U)Activator.CreateInstance(typeof(U), logger, options);
        }


        /// <summary>
        /// Get all entities of a particular type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            try
            {
                return Ok(await manager.LoadAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a particular entity 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(Guid id)
        {
            try
            {
                return Ok(await manager.LoadByIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPost("{rollback?}")]
        public async Task<ActionResult> Post([FromBody] T entity, bool rollback)
        {
            try
            {
                Guid id = await manager.InsertAsync(entity, rollback);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPut("{id}/{rollback?}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] T entity, bool rollback)
        {
            try
            {
                int rowsaffected = await manager.UpdateAsync(entity, rollback);

                //Create a small json bit
                var result = new Dictionary<string, string>();
                result.Add("rowsaffected", rowsaffected.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{rollback?}")]
        public async Task<ActionResult> Delete(Guid id, bool rollback)
        {
            try
            {
                int rowsaffected = await manager.DeleteAsync(id, rollback);

                //Create a small json bit
                var result = new Dictionary<string, string>();
                result.Add("rowsaffected", rowsaffected.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
    }


