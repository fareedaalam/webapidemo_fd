using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.Interface
{
    public interface ISubjectInterface
    {
        SubjectEntity GetSubjectById(Guid subjectId);
        IEnumerable<SubjectEntity> GetAllSubject();
        FunctionResponse CreateSubject(SubjectEntity subjectEntity);
        FunctionResponse UpdateSubject(Guid subjectId, SubjectEntity subjectEntity);
        bool DeleteSubject(Guid subjectId);
    }
}
