using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface ICategorySubTopicInterface
    {
        IEnumerable<CategorySubTopicEntity> GetById(Guid id);
        IEnumerable<CategorySubTopicEntity> GetAll();
        FunctionResponse Create(CategorySubTopicEntity categorySubTopicEntity);
        FunctionResponse Update(Guid id, CategorySubTopicEntity categorySubTopicEntity);
        bool Delete(Guid id);
        FunctionResponse GetCategory(CategorySubTopicEntity cstEntity);
    }
}
