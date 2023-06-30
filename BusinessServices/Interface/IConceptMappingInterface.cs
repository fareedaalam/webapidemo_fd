using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IConceptMappingInterface
    {
        FunctionResponse GetConceptsById(Guid Id);
        FunctionResponse GetAll();
        FunctionResponse Create(ConceptMappingEntity entity);
        FunctionResponse Update(Guid Id, ConceptMappingEntity entity);
        FunctionResponse Delete(Guid Id);
        FunctionResponse GetConcepts(ConceptMappingEntity entity);
    }
}
