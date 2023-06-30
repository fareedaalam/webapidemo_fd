using AutoMapper;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessServices.Repository
{
    public class LevelRepository : ILevelInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public LevelRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FunctionResponse Create(LevelEntity levelEntity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var level = new tbl_Level
                {
                    Id = Guid.NewGuid(),
                    Name = levelEntity.Name,
                    Code = Generate_Code().Trim(),
                    Range = levelEntity.Range,
                    CreatedOn = DateTime.Now,
                    CreatedBy = levelEntity.CreatedBy,
                    IsActive = levelEntity.IsActive == null ? false : levelEntity.IsActive
                };

                var user = _unitOfWork.LevelRepository.Get(u => u.Name == level.Name);

                if (user == null)
                {
                    _unitOfWork.LevelRepository.Insert(level);
                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(level.Id);
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

        //public bool Delete(Guid LevelId)
        //{
        //    var Success = false;

        //    var Level = new tbl_Level { };

        //    if (LevelId != null)
        //    {
        //        Level = _unitOfWork.LevelRepository.GetByID(LevelId);
        //        if (Level != null)
        //        {
        //            _unitOfWork.LevelRepository.Delete(Level);
        //            _unitOfWork.Save();
        //            Success = true;
        //        }
        //    }

        //    Generate_Code_Delete(Level.Code);

        //    return Success;
        //}

        public FunctionResponse Delete(Guid LevelId)
        {

            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                if (LevelId != null)
                {
                    var Level = _unitOfWork.LevelRepository.GetByID(LevelId);
                    if (Level != null)
                    {
                        // Level.IsActive = Level.IsActive == true ? false : true;
                        // _unitOfWork.LevelRepository.Update(Level);
                        _unitOfWork.LevelRepository.Delete(Level);
                    }
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }

                    // Generate_Code_Delete(Level.Code);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                    RMsg.Message = "Missing Something";
                }

                return RMsg;


            }
            catch (Exception)
            {

                throw;
            }


        }

        public void Generate_Code_Delete(string delCode)
        {
            try
            {

                var split_del_code = delCode.Substring(3, 5);

                var level_list = _unitOfWork.LevelRepository.GetAll().ToList();

                int level_count = level_list.Count();



                if (level_count > 0)
                {

                    for (var i = 0; i < level_count; i++)
                    {
                        int count = 0;

                        var change_code = level_list[i].Code;
                        var lavel_code_split1 = change_code.Substring(3, 5);
                        var last_digit_code = Convert.ToInt32(lavel_code_split1);
                        var int_del_code = Convert.ToInt32(split_del_code);
                        if (last_digit_code > int_del_code)
                        {
                            last_digit_code--;
                        }
                        int last_code_value = last_digit_code;

                        while (last_digit_code != 0)
                        {
                            /* Increment digit count */
                            count++;

                            /* Remove last digit of 'num' */
                            last_digit_code /= 10;
                        }

                        var concat_len = 5 - count;
                        string str_code_pref = "LBL";

                        var str_cst_code = str_code_pref.PadRight(concat_len + 3, '0') + last_code_value;
                        level_list[i].Code = str_cst_code;

                        _unitOfWork.LevelRepository.Update(level_list[i]);
                        _unitOfWork.Save();
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public IEnumerable<LevelEntity> GetAll()
        //{


        //    //var Levels = _unitOfWork.LevelRepository.GetAll().ToList();

        //    var Levels = _unitOfWork.LevelRepository.GetAll().OrderBy(x => x.CreatedOn).ToList();

        //    if (Levels.Any())
        //    {
        //        var LevelsModel = Mapper.Map<List<tbl_Level>, List<LevelEntity>>(Levels);
        //        return LevelsModel;
        //    }
        //    return null;
        //}

        public FunctionResponse GetAll()
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();

                var Levels = _unitOfWork.LevelRepository.GetAll().OrderBy(x => x.Name).ToList();

                if (Levels.Any() && Levels.Count > 0)
                {
                    var LevelsModel = Mapper.Map<List<tbl_Level>, List<LevelEntity>>(Levels);
                    RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                    RMsg.Data.Add(LevelsModel);
                }
                else
                {
                    RMsg.Status = FunctionResponse.StatusType.NO_RECORD;
                    RMsg.Message = "No Record Found";
                }

                return RMsg;

            }
            catch (Exception ex)
            {
                throw;
            }
            //var Levels = _unitOfWork.LevelRepository.GetAll().ToList();
        }

        public string Generate_Code()
        {
            var str_level_code = (String)null;

            int count = 0;

            var Levels = _unitOfWork.LevelRepository.GetAll().ToList();

            //var Levels2 = Levels[0];

            int level_count = Levels.Count();

            if (level_count < 1)
            {
                str_level_code = "LBL00001";
            }
            else
            {
                var Levels1 = _unitOfWork.LevelRepository.GetAll().ToList().OrderByDescending(x => x.CreatedOn).First();

                var level_code1 = Levels1.Code;

                var level_code_split = level_code1.Substring(3, 5);

                int level_code_increament = Convert.ToInt32(level_code_split) + 1;

                int last_code_value = level_code_increament;
                while (level_code_increament != 0)
                {
                    /* Increment digit count */
                    count++;

                    /* Remove last digit of 'num' */
                    level_code_increament /= 10;
                }
                var concat_len = 5 - count;
                string str_code_pref = "LBL";
                //string str1 = "0";

                str_level_code = str_code_pref.PadRight(concat_len + 3, '0') + last_code_value;

            }

            return str_level_code;
        }




        public LevelEntity GetById(Guid LevelId)
        {
            var level = _unitOfWork.LevelRepository.GetByID(LevelId);
            if (level != null)
            {
                var levelModel = Mapper.Map<tbl_Level, LevelEntity>(level);
                return levelModel;
            }
            return null;
        }

        public FunctionResponse Update(Guid LevelId, LevelEntity levelEntity)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
                if (levelEntity != null && LevelId != Guid.Empty)
                {
                    var level = _unitOfWork.LevelRepository.GetByID(LevelId);

                    if (level != null)
                    {
                        //Check Duplicate
                        if (level.Name == levelEntity.Name && level.Code == levelEntity.Code && level.IsActive == levelEntity.IsActive)
                        {
                            RMsg.Message = "Duplicate";
                            RMsg.Status = FunctionResponse.StatusType.ERROR;

                        }
                        else if (level.IsActive != levelEntity.IsActive)
                        {
                            level.IsActive = levelEntity.IsActive == null ? false : levelEntity.IsActive;
                            level.UpdatedOn = DateTime.Now;
                            if (levelEntity.UpdatedBy != null)
                                level.UpdatedBy = levelEntity.UpdatedBy;
                        }
                        else
                        {
                            level.Name = levelEntity.Name == null ? null : levelEntity.Name.Trim();
                            level.Code = levelEntity.Code == null ? null : levelEntity.Code.Trim();
                            level.Range = levelEntity.Range == null ? null : levelEntity.Range.Trim();
                            level.IsActive = levelEntity.IsActive == null ? null : levelEntity.IsActive;
                            level.UpdatedBy = levelEntity.UpdatedBy == null ? null : levelEntity.UpdatedBy;
                            level.UpdatedOn = DateTime.Now;
                        }

                        _unitOfWork.LevelRepository.Update(level);
                        if (_unitOfWork.Save() > 0)
                        {
                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                            RMsg.Data.Add(level.Id);
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
    }
}

