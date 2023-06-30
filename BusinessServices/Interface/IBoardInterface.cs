using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices
{
    public interface IBoardInterface
    {
        IEnumerable<BoardEntity> GetById(Guid brdId);
        FunctionResponse GetAll();
        FunctionResponse Create(BoardEntity boardEntity);
        FunctionResponse Update(Guid brdId, BoardEntity boardEntity);
        bool Delete(Guid id);
    }
}
