using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
  public  interface IQustionPatternInterface
    {
        FunctionResponse GetquestionById(Guid brdId);
        FunctionResponse GetAllquestion();
        FunctionResponse Createquestion(QuestionPatternEntity questionEntity);
        FunctionResponse Updatequestion(Guid brdId, QuestionPatternEntity questionEntity);
        FunctionResponse Deletequestion(Guid brdId);
        FunctionResponse GetQuestionPattern(QuestionPatternEntity entity);
        FunctionResponse GetQuestionPattern_Category(QuestionPatternEntity entity);
        FunctionResponse GetQuestionPatternSolution(QuestionPatternEntity entity);

    }
}
