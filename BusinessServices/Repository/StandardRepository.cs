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
    public class StandardRepository : IStandardInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public StandardRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public FunctionResponse Create(StandardEntity entity)
        //{
        //    try
        //    {
        //        FunctionResponse RMsg = new FunctionResponse();
        //        var standard = new tbl_Standard
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = entity.Name,
        //            Code = Generate_Code().Trim(),
        //            IsActive = entity.IsActive,               
        //            TypeName = entity.TypeName,
        //            CreatedOn = DateTime.Now,
        //            CreatedBy = entity.CreatedBy
        //        };
        //        _unitOfWork.StandardRepository.Insert(standard);
        //        int data = _unitOfWork.Save();

        //        if (data > 0)
        //        {
        //            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
        //            RMsg.Data.Add(data);
        //        }
        //        else
        //        {
        //            RMsg.Message = "No_Record_Found";
        //            RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
        //        }
        //        return RMsg;
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        public FunctionResponse Create(StandardEntity standardEntity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var standard = new tbl_Standard
                {
                    Id = Guid.NewGuid(),
                    Name = standardEntity.Name,
                    Code = standardEntity.Code,
                    TypeName = standardEntity.TypeName == null ? null : standardEntity.TypeName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = standardEntity.CreatedBy,
                    IsActive = standardEntity.IsActive == null ? false : standardEntity.IsActive,
                    BoardId = standardEntity.BoardId
                };

                var user = _unitOfWork.StandardRepository.Get(u => u.Name == standard.Name && u.Code == standard.Code);

                if (user == null)
                {
                    _unitOfWork.StandardRepository.Insert(standard);
                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(standard.Id);
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

        public string Generate_Code()
        {
            var str_standard_code = (String)null;

            int count = 0;

            var standard = _unitOfWork.StandardRepository.GetAll().ToList();

            int standard_count = standard.Count();

            if (standard_count < 1)
            {
                str_standard_code = "STD00001";
            }
            else
            {
                var Standard1 = _unitOfWork.StandardRepository.GetAll().ToList().OrderByDescending(x => x.CreatedOn).First();

                var standard_code1 = Standard1.Code;

                var standard_code_split = standard_code1.Substring(3, 5);

                int standard_code_increament = Convert.ToInt32(standard_code_split) + 1;

                int last_code_value = standard_code_increament;
                while (standard_code_increament != 0)
                {
                    /* Increment digit count */
                    count++;

                    /* Remove last digit of 'num' */
                    standard_code_increament /= 10;
                }
                var concat_len = 5 - count;
                string str_code_pref = "STD";
                //string str1 = "0";

                str_standard_code = str_code_pref.PadRight(concat_len + 3, '0') + last_code_value;

            }

            return str_standard_code;
        }


        public FunctionResponse Delete(Guid Id)
        {
            // var Topic = new tbl_Standard { };
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                var Topic = _unitOfWork.StandardRepository.GetByID(Id);
                if (Topic != null)
                {
                    // Topic.IsActive = Topic.IsActive == true ? false : true;
                    //_unitOfWork.StandardRepository.Update(Topic);
                    _unitOfWork.StandardRepository.Delete(Topic);
                }
                if (_unitOfWork.Save() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    // RMsg.Data.Add(data);
                }
                else
                {
                    RMsg.Message = "No_Record_Found";
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                }
                //Generate_Code_Delete(Topic.Code);
                return RMsg;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void Generate_Code_Delete(string delCode)
        {
            var split_del_code = delCode.Substring(3, 5);

            var standard = _unitOfWork.StandardRepository.GetAll().ToList();

            int standard_count = standard.Count();

            if (standard_count > 1)
            {

                for (var i = 0; i < standard_count; i++)
                {
                    int count = 0;

                    var change_code = standard[i].Code;
                    var standard_code_split1 = change_code.Substring(3, 5);
                    var int_code = Convert.ToInt32(standard_code_split1);
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
                    string str_code_pref = "STD";

                    var str_standard_code = str_code_pref.PadRight(concat_len + 3, '0') + last_code_value;
                    standard[i].Code = str_standard_code;
                    _unitOfWork.StandardRepository.Update(standard[i]);
                    _unitOfWork.Save();
                }
                //string str1 = "0";
            }
        }

        public FunctionResponse GetAll()
        {

            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.StandardRepository.GetStandard().OrderBy(x => x.Name).ToList();

                if (data != null && data.Count() > 0)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
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

        public FunctionResponse GetById(Guid Id)
        {

            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var data = _unitOfWork.StandardRepository.GetStandardById(Id);

                if (data != null)
                {
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(data);
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

        public FunctionResponse Update(Guid Id, StandardEntity standardEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (standardEntity != null && Id != Guid.Empty)
                {
                    var std = _unitOfWork.StandardRepository.GetByID(Id);
                    if (std != null)
                    {
                        //check Duplicate
                        if (std.Name.Trim() == standardEntity.Name.Trim() && std.TypeName.Trim() == standardEntity.TypeName.Trim() && std.IsActive==standardEntity.IsActive)
                        {
                            RMsg.Message = "Duplicate";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;

                        }
                        else if (std.IsActive != standardEntity.IsActive)
                        {
                            std.IsActive = standardEntity.IsActive;
                            std.UpdatedOn = DateTime.Now;
                            std.UpdatedBy = standardEntity.UpdatedBy;
                            _unitOfWork.StandardRepository.Update(std);
                        }
                        else
                        {
                            std.Name = standardEntity.Name.Trim();
                            std.TypeName = standardEntity.TypeName;
                            std.IsActive = standardEntity.IsActive;
                            //Added Releation
                            //std.BoardId = standardEntity.BoardId;
                            std.UpdatedOn = DateTime.Now;
                            std.UpdatedBy = standardEntity.UpdatedBy;
                            _unitOfWork.StandardRepository.Update(std);
                        }
                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Data.Add(std.Id);
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

            //try
            //{
            //    FunctionResponse RMsg = new FunctionResponse();
            //    if (standardEntity != null && Id != Guid.Empty)
            //    {

            //        var std = _unitOfWork.StandardRepository.GetByID(Id);
            //        if (std != null)
            //        {
            //            var std_Name = _unitOfWork.StandardRepository.Get(s => s.Name == standardEntity.Name && s.TypeName == standardEntity.TypeName);
            //            if (std_Name == null)
            //            {
            //                if (standardEntity.Name != null)
            //                    std.Name = standardEntity.Name == null ? null : standardEntity.Name.Trim();

            //                if (standardEntity.Code != null)
            //                    std.Code = standardEntity.Code;

            //                if (standardEntity.TypeName != null)
            //                    std.TypeName = standardEntity.TypeName;

            //                if (standardEntity.IsActive != null)
            //                    std.IsActive = standardEntity.IsActive;

            //                std.UpdatedOn = DateTime.Now;
            //                std.UpdatedBy = standardEntity.UpdatedBy;

            //                _unitOfWork.StandardRepository.Update(std);
            //                if (_unitOfWork.Save() > 0)
            //                {
            //                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
            //                    RMsg.Data.Add(std.Id);
            //                    RMsg.Message = "Success";

            //                }
            //                else
            //                {
            //                    RMsg.Message = "Error";
            //                    RMsg.Status = FunctionResponse.StatusType.ERROR;
            //                }
            //            }
            //           else
            //            { 
            //                if(std_Name.Id == std.Id)
            //                     {
            //                        if (standardEntity.Name != null)
            //                            std.Name = standardEntity.Name == null ? null : standardEntity.Name.Trim();

            //                        if (standardEntity.Code != null)
            //                            std.Code = standardEntity.Code;

            //                        if (standardEntity.TypeName != null)
            //                            std.TypeName = standardEntity.TypeName;

            //                        if (standardEntity.IsActive != null)
            //                                std.IsActive = standardEntity.IsActive;

            //                        std.UpdatedOn = DateTime.Now;
            //                    std.UpdatedBy = standardEntity.UpdatedBy;


            //                    _unitOfWork.StandardRepository.Update(std);
            //                        if (_unitOfWork.Save() > 0)
            //                        {
            //                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
            //                            RMsg.Data.Add(std.Id);
            //                            RMsg.Message = "Success";

            //                        }
            //                        else
            //                        {

            //                            RMsg.Message = "Error";
            //                            RMsg.Status = FunctionResponse.StatusType.ERROR;
            //                        }
            //                    }
            //                else {
            //                    RMsg.Message = "Duplicate";
            //                    RMsg.Status = FunctionResponse.StatusType.ERROR;
            //                }
            //            }
            //        }
            //    }
            //    return RMsg;
            //}
            //catch (Exception )
            //{
            //    throw;
            //}


        }


        //public FunctionResponse Update(Guid Id, StandardEntity entity)
        //{
        //    try
        //    {
        //        FunctionResponse RMsg = new FunctionResponse();
        //        if (entity != null)
        //        {
        //            var Model = _unitOfWork.StandardRepository.GetByID(Id);
        //            if (Model != null)
        //            {
        //                if (entity.Name != null)
        //                    Model.Name = entity.Name;

        //                if (entity.Code != null)
        //                    Model.Code = entity.Code;

        //                if (entity.IsActive != null)
        //                    Model.IsActive = entity.IsActive;

        //                if (entity.TypeName != null)
        //                    Model.TypeName = entity.TypeName;

        //                Model.UpdatedOn = DateTime.Now;
        //            }
        //            _unitOfWork.StandardRepository.Update(Model);
        //        }
        //        int data = _unitOfWork.Save();

        //        if (data > 0)
        //        {
        //            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
        //            RMsg.Data.Add(data);
        //        }
        //        else
        //        {
        //            RMsg.Message = "No_Record_Found";
        //            RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
        //        }
        //        return RMsg;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
