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
using DataModel.GenericRepository;

namespace BusinessServices.Repository
{
    public class SubjectRepository : ISubjectInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public SubjectRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public FunctionResponse CreateSubject(SubjectEntity subjectEntity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var subject = new tbl_Subject
                {
                    Id = Guid.NewGuid(),
                    Name = subjectEntity.Name,
                    Code = Generate_Code().Trim(),
                    // Code=subjectEntity.Code,
                    CreatedOn = DateTime.Now,
                    CreatedBy = subjectEntity.CreatedBy,
                    IsActive = subjectEntity.IsActive == null ? true : subjectEntity.IsActive

                };
                var stud_user = _unitOfWork.SubjectRepository.Get(s => s.Name == subject.Name);
                if (stud_user == null)
                {
                    _unitOfWork.SubjectRepository.Insert(subject);
                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(subject.Id);

                }
                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "Duplicate";

                }
            }
            catch (Exception)
            {
                throw;
            }
            return Resp;

        }

        //Generating code
        public string Generate_Code()
        {
            var str_subject_code = (String)null;

            int count = 0;

            var subject = _unitOfWork.SubjectRepository.GetAll().ToList();

            int subject_count = subject.Count();

            if (subject_count < 1)
            {
                str_subject_code = "SUB00001";
            }
            else
            {
                var subject1 = _unitOfWork.SubjectRepository.GetAll().ToList().OrderByDescending(x => x.CreatedOn).First();

                var subject_code1 = subject1.Code;

                var subject_code_split = subject_code1.Substring(3, 5);

                int subject_code_increament = Convert.ToInt32(subject_code_split) + 1;

                int last_code_value = subject_code_increament;
                while (subject_code_increament != 0)
                {
                    /* Increment digit count */
                    count++;

                    /* Remove last digit of 'num' */
                    subject_code_increament /= 10;
                }
                var concat_len = 5 - count;
                string str_code_pref = "SUB";
                //string str1 = "0";

                str_subject_code = str_code_pref.PadRight(concat_len + 3, '0') + last_code_value;

            }

            return str_subject_code;
        }

        public bool DeleteSubject(Guid subjectId)
        {
            try
            {
                var success = false;
                if (subjectId != null)
                {
                    var subject = _unitOfWork.SubjectRepository.GetByID(subjectId);
                    if (subject != null)
                    {
                        // subject.IsActive = subject.IsActive == true ? false : true;
                        //_unitOfWork.SubjectRepository.Update(subject);                        
                        _unitOfWork.SubjectRepository.Delete(subject);
                        _unitOfWork.Save();
                        success = true;
                    }
                }
                // Generate_Code_Delete(subject.Code);
                return success;

            }
            catch (Exception)
            {

                throw;
            }


        }

        //Updating all codes after delete
        public void Generate_Code_Delete(string delCode)
        {
            var split_del_code = delCode.Substring(3, 5);

            var subject = _unitOfWork.SubjectRepository.GetAll().ToList();

            int subject_count = subject.Count();

            //var str_subject_code = (String)null;



            if (subject_count > 0)
            {

                for (var i = 0; i < subject_count; i++)
                {
                    int count = 0;
                    var change_code = subject[i].Code;
                    var subject_code_split1 = change_code.Substring(3, 5);
                    var int_code = Convert.ToInt32(subject_code_split1);
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

                    var str_subject_code = str_code_pref.PadRight(concat_len + 3, '0') + last_code_value;
                    subject[i].Code = str_subject_code;
                    _unitOfWork.SubjectRepository.Update(subject[i]);
                    _unitOfWork.Save();
                }

                //string str1 = "0";
            }
        }

        public IEnumerable<SubjectEntity> GetAllSubject()
        {
            // var subjects;
            var subjects = _unitOfWork.SubjectRepository.GetAll().OrderBy(x => x.Name).ToList();

            if (subjects.Any())
            {
                var subjectModel = Mapper.Map<List<tbl_Subject>, List<SubjectEntity>>(subjects);
                return subjectModel;
            }
            return null;
        }
        public SubjectEntity GetSubjectById(Guid subjectId)
        {
            var subject = _unitOfWork.SubjectRepository.GetByID(subjectId);
            if (subject != null)
            {
                var subjectModel = Mapper.Map<tbl_Subject, SubjectEntity>(subject);
                return subjectModel;
            }
            return null;
        }


        public FunctionResponse UpdateSubject(Guid subjectId, SubjectEntity subjectEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (subjectEntity != null && subjectId != Guid.Empty)
                {
                    //Check Record Exist
                    var subject = _unitOfWork.SubjectRepository.GetByID(subjectId);
                    if (subject != null)
                    {
                        if (subject.Name.Trim() == subjectEntity.Name.Trim() && subject.IsActive == subjectEntity.IsActive)
                        {
                            RMsg.Message = "Duplicate";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;
                        }
                        else if (subject.IsActive != subjectEntity.IsActive)
                        {
                            subject.IsActive = subjectEntity.IsActive;
                            _unitOfWork.SubjectRepository.Update(subject);
                        }
                        else
                        {
                            subject.Name = subjectEntity.Name == null ? null : subjectEntity.Name.Trim();
                            subject.IsActive = subjectEntity.IsActive;
                            _unitOfWork.SubjectRepository.Update(subject);
                        }

                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Data.Add(subject.Id);
                            RMsg.Message = "Success";

                        }
                       

                    }
                }



                //if (subject != null)
                //{
                //    var subject_Name = _unitOfWork.SubjectRepository.Get(s => s.Name == subjectEntity.Name);
                //    if (subject_Name == null)
                //    {
                //        if (subjectEntity.Name != null)
                //            subject.Name = subjectEntity.Name == null ? null : subjectEntity.Name.Trim();

                //        if (subjectEntity.Code != null)
                //            subject.Code = subjectEntity.Code;

                //        if (subjectEntity.IsActive != null)
                //            subject.IsActive = subjectEntity.IsActive;

                //        subject.UpdatedOn = DateTime.Now;
                //        subject.UpdatedBy = subjectEntity.UpdatedBy;

                //        _unitOfWork.SubjectRepository.Update(subject);
                //        if (_unitOfWork.Save() > 0)
                //        {
                //            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                //            RMsg.Data.Add(subject.Id);
                //            RMsg.Message = "Success";

                //        }
                //        else
                //        {
                //            RMsg.Message = "Error";
                //            RMsg.Status = FunctionResponse.StatusType.ERROR;
                //        }
                //    }


                //    else
                //    {
                //        if (subject_Name.Code == subjectEntity.Code)
                //        {
                //            if (subjectEntity.Name != null)
                //                subject.Name = subjectEntity.Name == null ? null : subjectEntity.Name.Trim();

                //            if (subjectEntity.Code != null)
                //                subject.Code = subjectEntity.Code;

                //            if (subjectEntity.IsActive != null)
                //                subject.IsActive = subjectEntity.IsActive;



                //            subject.UpdatedOn = DateTime.Now;
                //            subject.UpdatedBy = subjectEntity.UpdatedBy;

                //            _unitOfWork.SubjectRepository.Update(subject);

                //            }
                //            else
                //            {
                //                RMsg.Message = "Duplicate";
                //                RMsg.Status = FunctionResponse.StatusType.ERROR;
                //            }

                //        }
                //    }

                //}

                return RMsg;
            }
            catch (Exception)
            {
                throw;
            }


        }

    }


}
