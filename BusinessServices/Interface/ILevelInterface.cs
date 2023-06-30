using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
   public interface ILevelInterface
    {
        LevelEntity GetById(Guid LevelId);
        FunctionResponse GetAll();
        FunctionResponse Create(LevelEntity levelEntity);
        FunctionResponse Update(Guid LevelId, LevelEntity levelEntity);
        FunctionResponse Delete(Guid LevelId);
    }
}
