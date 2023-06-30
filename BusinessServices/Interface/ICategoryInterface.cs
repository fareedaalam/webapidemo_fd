using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
   public interface ICategoryInterface
    {
        CategoryEntity GetCategoryById(Guid categoryId);
        IEnumerable<CategoryEntity> GetAllCategory();
        Guid CreateCategory(CategoryEntity categoryEntity);
        bool UpdateCategory(Guid categoryId, CategoryEntity categoryEntity);
        bool DeleteCategory(Guid categoryId);
    }
}
