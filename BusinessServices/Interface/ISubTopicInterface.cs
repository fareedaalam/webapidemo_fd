using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface ISubTopicInterface
    {
        IEnumerable<SubTopicEntity> GetSubTopicById(Guid subTopicId);
        IEnumerable<SubTopicEntity> GetAllSubTopic();
        FunctionResponse Create(SubTopicEntity subTopicEntity);
        FunctionResponse Update(Guid subTopicId, SubTopicEntity subTopicEntity);
        bool DeleteSubTopic(Guid subTopicId);
        FunctionResponse GetSubTopicByTopicId(Guid TopicId);
    }
}
