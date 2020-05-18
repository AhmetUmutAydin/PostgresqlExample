using PostgresqlExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgresqlExample.Managers
{
    public interface IPostgresqlManager
    {
        List<Categories> GetCategories();
        Categories GetCategory(int id);
        List<Posts> GetPosts();
        Posts GetPost(int id);
        Posts SavePost(Posts post);
        void UpdatePost(int id,Posts post);
        void DeletePost(int Id);
        List<Posts> GetCategoryPosts (int categoryId);

    }
}
