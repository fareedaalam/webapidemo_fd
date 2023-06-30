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
    public class UserLogRepository : IUserLogInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public UserLogRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Guid Create(UserLogEntity userLogEntity)
        {
            var userLog = new tbl_UserLog
            {
                Id=Guid.NewGuid(),
                UserId=userLogEntity.UserId,
                PageName=userLogEntity.PageName,
                IsActive=userLogEntity.IsActive,
                CreatedOn = DateTime.Now,
                CreatedBy = userLogEntity.CreatedBy
            };
            _unitOfWork.UserLogRepository.Insert(userLog);
            _unitOfWork.Save();
            return userLog.Id;
        }

        public bool Delete(Guid id)
        {
            var success = false;
            if (id != null)
            {
                var userLog = _unitOfWork.UserLogRepository.GetByID(id);
                if (userLog != null)
                {

                    _unitOfWork.UserLogRepository.Delete(userLog);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }

        public IEnumerable<UserLogEntity> GetAll()
        {
            var userLog = _unitOfWork.UserLogRepository.GetAll().ToList();
            if (userLog.Any())
            {
                var userLogModel = Mapper.Map<List<tbl_UserLog>, List<UserLogEntity>>(userLog);
                return userLogModel;
            }
            return null;
        }

        public UserLogEntity GetById(Guid id)
        {
            var userLog = _unitOfWork.UserLogRepository.GetByID(id);
            if (userLog != null)
            {
                var userLogModel = Mapper.Map<tbl_UserLog, UserLogEntity>(userLog);
                return userLogModel;
            }
            return null;
        }

        public bool Update(Guid id, UserLogEntity userLogEntity)
        {
            var success = false;
            if (userLogEntity != null)
            {
                var userLog = _unitOfWork.UserLogRepository.GetByID(id);
                if (userLog != null)
                {
                    userLog.UserId = userLogEntity.UserId;
                    userLog.PageName = userLogEntity.PageName;
                    userLog.UpdataedBy = Guid.NewGuid();
                    userLog.UpdatedOn = DateTime.Now;
                    userLog.IsActive = userLogEntity.IsActive;

                    _unitOfWork.UserLogRepository.Update(userLog);
                    _unitOfWork.Save();

                    success = true;
                }
            }
            return success;
        }
    }
}
