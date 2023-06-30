using BusinessEntities;
using System;

namespace BusinessServices.Interface
{
    public interface ICurriculumInterface
    {
        #region Curriculum
        FunctionResponse GetById(Guid Id);
        FunctionResponse GetAll();
      
        FunctionResponse Create(CurriculumEntity entity);
        FunctionResponse Update(Guid Id, CurriculumEntity entity);
        FunctionResponse Delete(Guid Id);

        //Extra
        FunctionResponse GetByUser(Guid Id);
     //   FunctionResponse Filter(CurriculumEntity entity);


        #endregion

        #region CurriculumDetails
        FunctionResponse GetDetails(Guid Id);
        FunctionResponse GetDetails();
        FunctionResponse CreateDetails(CurriculumDetailsEntity entity);
        FunctionResponse UpdateDetails(Guid Id,CurriculumDetailsEntity entity);
        FunctionResponse DeleteDetails(Guid Id);
        #endregion
    }
}
