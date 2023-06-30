using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Repository
{
    public class SubTopicRepository : ISubTopicInterface
    {
        private readonly UnitOfWork _unitOfWork;

        public SubTopicRepository(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public FunctionResponse Create(SubTopicEntity subTopicEntity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var subtopic = new tbl_SubTopic
                {

                    Id = Guid.NewGuid(),
                    Name = subTopicEntity.Name,
                    Code = Generate_Code().Trim(),

                    // TypeId = subTopicEntity.TypeId,
                    TopicId = subTopicEntity.TopicId,

                    CreatedOn = DateTime.Now,
                    CreatedBy = subTopicEntity.CreatedBy,
                    IsActive = subTopicEntity.IsActive == null ? false : subTopicEntity.IsActive

                };


                var user = _unitOfWork.SubTopicRepository.Get(u => u.Name == subtopic.Name /* && u.TopicId == subtopic.TopicId && u.TypeId == subtopic.TypeId*/);

                if (user == null)
                {
                    _unitOfWork.SubTopicRepository.Insert(subtopic);
                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(subtopic.Id);
                }
                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "Duplicat";

                }

            }
            catch (Exception)
            {

                throw;
            }

            return Resp;
        }

        //public Guid CreateSubTopic(SubTopicEntity subTopicEntity)
        //{
        //    var subTopic = new tbl_SubTopic
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = subTopicEntity.Name,
        //        Code = Generate_Code().Trim(),
        //        TypeId = subTopicEntity.TypeId,
        //        TopicId = subTopicEntity.TopicId,
        //        CreatedBy = subTopicEntity.CreatedBy,
        //        CreatedOn = DateTime.Now,
        //        IsActive = subTopicEntity.IsActive
        //    };
        //    _unitOfWork.SubTopicRepository.Insert(subTopic);
        //    _unitOfWork.Save();
        //    return subTopic.Id;
        //}


        public string Generate_Code()
        {
            var str_subtopic_code = (String)null;

            int count = 0;

            var subtopics = _unitOfWork.SubTopicRepository.GetAll().ToList();

            int subtopic_count = subtopics.Count();

            if (subtopic_count < 1)
            {
                str_subtopic_code = "SUB00001";
            }
            else
            {
                var subtopics1 = _unitOfWork.SubTopicRepository.GetAll().ToList().OrderByDescending(x => x.CreatedOn).First();

                var subtopic_code1 = subtopics1.Code;

                var subtopic_code_split = subtopic_code1.Substring(3, 5);

                int subtopic_code_increament = Convert.ToInt32(subtopic_code_split) + 1;

                int last_code_value = subtopic_code_increament;
                while (subtopic_code_increament != 0)
                {
                    /* Increment digit count */
                    count++;

                    /* Remove last digit of 'num' */
                    subtopic_code_increament /= 10;
                }
                var concat_len = 5 - count;
                string str_code_pref = "SUB";
                //string str1 = "0";

                str_subtopic_code = str_code_pref.PadRight(concat_len + 3, '0') + last_code_value;

            }

            return str_subtopic_code;
        }


        public bool DeleteSubTopic(Guid subTopicId)
        {
            try
            {

                var success = false;
                if (subTopicId != null)
                {
                    var subTopic = _unitOfWork.SubTopicRepository.GetByID(subTopicId);
                    if (subTopic != null)
                    {
                        // subTopic.IsActive = subTopic.IsActive == true ? false : true;
                        // _unitOfWork.SubTopicRepository.Update(subTopic);
                        _unitOfWork.SubTopicRepository.Delete(subTopic);
                        _unitOfWork.Save();
                        success = true;
                    }
                }
                // Generate_Code_Delete(subTopic.Code);
                return success;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Generate_Code_Delete(string delCode)
        {
            var split_del_code = delCode.Substring(3, 5);

            var subtopic = _unitOfWork.SubTopicRepository.GetAll().ToList();

            int subtopic_count = subtopic.Count();

            //var str_subject_code = (String)null;

            if (subtopic_count > 0)
            {

                for (var i = 0; i < subtopic_count; i++)
                {
                    int count = 0;
                    var change_code = subtopic[i].Code;
                    var subtopic_code_split1 = change_code.Substring(3, 5);
                    var int_code = Convert.ToInt32(subtopic_code_split1);
                    var int_del_code = Convert.ToInt32(split_del_code);
                    if (int_code > int_del_code)
                    {
                        int_code--;
                    }
                    int last_code_value = int_code;

                    while (int_code != 0)
                    {
                        /* Increment digit count */
                        count++;

                        /* Remove last digit of 'num' */
                        int_code /= 10;
                    }

                    var concat_len = 5 - count;
                    string str_code_pref = "SUB";

                    var str_subtopic_code = str_code_pref.PadRight(concat_len + 3, '0') + last_code_value;
                    subtopic[i].Code = str_subtopic_code;
                    _unitOfWork.SubTopicRepository.Update(subtopic[i]);
                    _unitOfWork.Save();
                }

                //string str1 = "0";
            }
        }


        public IEnumerable<SubTopicEntity> GetAllSubTopic()
        {
            var subTopic = _unitOfWork.SubTopicRepository.GetSubTopic().OrderByDescending(x => x.CreatedOn).ToList();
            if (subTopic.Any())
            {
                //var subTopicModel = Mapper.Map<List<tbl_SubTopic>, List<SubTopicEntity>>(subTopic);
                IEnumerable<SubTopicEntity> List = subTopic;
                return List;
            }
            return null;
        }

        public IEnumerable<SubTopicEntity> GetSubTopicById(Guid subTopicId)
        {
            var subTopic = _unitOfWork.SubTopicRepository.GetSubTopicById(subTopicId);
            if (subTopic != null)
            {
                //var subTopicModel = Mapper.Map<tbl_SubTopic, SubTopicEntity>(subTopic);
                IEnumerable<SubTopicEntity> List = subTopic;
                return List;
            }
            return null;
        }
        public FunctionResponse GetSubTopicByTopicId(Guid TopicId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var data = _unitOfWork.SubTopicRepository.GetMany(x => x.TopicId == TopicId).OrderBy(x => x.Name).ToList<tbl_SubTopic>();

                if (data != null)
                {
                    var dataModel = Mapper.Map<List<tbl_SubTopic>, List<SubTopicEntity>>(data);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(dataModel);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public FunctionResponse Update(Guid subTopicId, SubTopicEntity Entity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (Entity != null && subTopicId != Guid.Empty)
                {
                    var subtopic = _unitOfWork.SubTopicRepository.GetByID(subTopicId);
                    if (subtopic != null)
                    {
                        //Check Duplicate
                        if (subtopic.Name == Entity.Name && subtopic.Name==Entity.Code && subtopic.TopicId == Entity.TopicId && subtopic.IsActive == Entity.IsActive)
                        {
                            RMsg.Message = "Duplicate";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;
                        }
                        else if (subtopic.IsActive != Entity.IsActive)
                        {
                            subtopic.IsActive = Entity.IsActive == null ? false : Entity.IsActive;
                            subtopic.UPdatedOn = DateTime.Now;
                            subtopic.UpdatedBy = Entity.UpdatedBy;
                        }
                        else
                        {
                            subtopic.Name = Entity.Name == null ? Entity.Name.Trim() : subtopic.Name;
                            subtopic.TopicId = Entity.TopicId == null ? Entity.TopicId : subtopic.TopicId;
                            subtopic.Code = Entity.Code == null ? Entity.Code : subtopic.Code;
                            subtopic.IsActive = Entity.IsActive == null ? Entity.IsActive : subtopic.IsActive;
                            subtopic.UPdatedOn = DateTime.Now;
                            subtopic.UpdatedBy = Entity.UpdatedBy;
                        }
                        _unitOfWork.SubTopicRepository.Update(subtopic);
                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Data.Add(subtopic.Id);
                            RMsg.Message = "Success";

                        }
                    }
                }
                return RMsg;
            }
            catch (Exception)
            {
                throw;
            }
        }



        //public bool UpdateSubTopic(Guid subTopicId, SubTopicEntity subTopicEntity)
        //{
        //    var success = false;
        //    if (subTopicEntity != null)
        //    {
        //        var subTopic = _unitOfWork.SubTopicRepository.GetByID(subTopicId);
        //        if (subTopic != null)
        //        {
        //            if(subTopicEntity.Name!=null)
        //                subTopic.Name = subTopicEntity.Name;
        //           // if (subTopicEntity.Name != null)
        //                //subTopic.UpdatedBy = Guid.NewGuid();

        //            subTopic.UPdatedOn = DateTime.Now;

        //            if (subTopicEntity.IsActive != null)
        //                subTopic.IsActive = subTopicEntity.IsActive;
        //            if (subTopicEntity.Code != null)
        //                subTopic.Code = subTopicEntity.Code;

        //            if (subTopicEntity.TypeId != null)
        //                subTopic.TypeId = subTopicEntity.TypeId;

        //            if (subTopicEntity.TopicId != null)
        //                subTopic.TopicId = subTopicEntity.TopicId;

        //            if (subTopicEntity.IsActive != null)
        //                subTopic.IsActive = subTopicEntity.IsActive;


        //            _unitOfWork.SubTopicRepository.Update(subTopic);
        //            _unitOfWork.Save();

        //            success = true;
        //        }
        //    }
        //    return success;
        //}
    }
}
