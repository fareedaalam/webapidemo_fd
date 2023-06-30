using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices
{
    public interface ISolutionsInterface
    {
        FunctionResponse GetById(Guid Id);
        FunctionResponse GetAll();
        FunctionResponse Create(SolutionsEntity entity);
        FunctionResponse Update(Guid Id, SolutionsEntity entity);
        FunctionResponse Delete(Guid Id);

        FunctionResponse GetByPatternId(Guid Id);
    }
}
