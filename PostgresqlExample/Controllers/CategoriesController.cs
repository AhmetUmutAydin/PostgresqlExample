using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostgresqlExample.Managers;
using PostgresqlExample.Models;

namespace PostgresqlExample.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IPostgresqlManager postgresqlManager;
        public CategoriesController(IPostgresqlManager postgresqlManager)
        {
            this.postgresqlManager = postgresqlManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categories>> Get()
        {
            var categories = postgresqlManager.GetCategories();

            return Ok(categories);
        }

    }
}