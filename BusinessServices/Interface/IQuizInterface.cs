using BusinessEntities;
using System; 

namespace BusinessServices.Interface
{
    public interface IQuizInterface
    {
        FunctionResponse GetById(Guid Id);
        FunctionResponse GetAll();
        FunctionResponse GetQuizByUser(QuizEntity entity);
        FunctionResponse Create(QuizEntity entity);
        FunctionResponse Update(Guid Id, QuizEntity entity);
        FunctionResponse Delete(Guid Id);
        FunctionResponse saveQuizResponse(QuizEntity quiz);
    }
}
