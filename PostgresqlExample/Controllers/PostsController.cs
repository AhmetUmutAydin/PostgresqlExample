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
    [Route("api/v1/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostgresqlManager postgresqlManager;
        public PostsController(IPostgresqlManager postgresqlManager)
        {
            this.postgresqlManager = postgresqlManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Posts>> Get()
        {
            var posts = postgresqlManager.GetPosts();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult<Posts> Get(int id)
        {
            var post = postgresqlManager.GetPost(id);
            if (post is null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public ActionResult Post(Posts post)
        {
            var savedPost = postgresqlManager.SavePost(post);

            return CreatedAtAction("Get",new {id=post.Id }, savedPost);
        }

        [HttpPut("{id}")]
        public ActionResult<Posts> Put(int id,Posts post)
        {
            var Checkpost = postgresqlManager.GetPost(id);
            if (Checkpost is null) return NotFound();

            postgresqlManager.UpdatePost(id,post);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Posts> Delete(int id)
        {
            var post= postgresqlManager.GetPost(id);
            if (post is null) return NotFound();

            postgresqlManager.DeletePost(id);

            return NoContent();
        }

        [HttpGet("Category/{categoryId}")]
        public ActionResult<IEnumerable<Posts>> CategoryPosts(int categoryId)
        {
            var post = postgresqlManager.GetCategory(categoryId);
            if (post is null) return NotFound();

            var posts = postgresqlManager.GetCategoryPosts(categoryId);

            return Ok(posts);
        }

    }
}