﻿using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface ITopicInterface
    {
        IEnumerable<TopicEntity> GetById(Guid TopicId);
        IEnumerable<TopicEntity> GetAll();
        FunctionResponse Create(TopicEntity topicEntity);
        FunctionResponse Update(Guid TopicId, TopicEntity topicEntity);
        bool Delete(Guid TopicId);
        FunctionResponse GetTopicBySubjectId(Guid SubjectId);

    }
}