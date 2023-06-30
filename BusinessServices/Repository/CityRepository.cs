using BusinessServices;
using BusinessServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataModel.UnitOfWork;
using DataModel;
using AutoMapper;

namespace BusinessServices.Repository
{
    public class CityRepository : ICityInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public CityRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public FunctionResponse GetCityByStateId(Guid Id)
        {
            FunctionResponse RMsg = new FunctionResponse();
            IEnumerable<CityEntity> data = _unitOfWork.CityRepository.GetCityByStateID(Id);

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
        public Guid CreateCity(CityEntity cityEntity)
        {

            var city = new tbl_City
            {
                Id = Guid.NewGuid(),
                Name = cityEntity.Name,
                Code = cityEntity.Code,
                StateId = cityEntity.StateId,
                IsActive = cityEntity.IsActive,
                CreatedOn = DateTime.Now,
                CreatedBy = cityEntity.CreatedBy
            };
            _unitOfWork.CityRepository.Insert(city);
            _unitOfWork.Save();
            return city.Id;
        }

        public bool DeleteCity(Guid cityId)
        {
            var Success = false;
            if (cityId != null)
            {
                var city = _unitOfWork.CityRepository.GetByID(cityId);
                if (city != null)
                {
                    _unitOfWork.CityRepository.Delete(city);
                    _unitOfWork.Save();
                    Success = true;
                }
            }
            return Success;
        }

        public FunctionResponse GetAllcities()
        {
            FunctionResponse RMsg = new FunctionResponse();
            IEnumerable<CityEntity> data = _unitOfWork.CityRepository.GetCity();

            try
            {
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

        public FunctionResponse GetcityById(Guid cityId)
        {
            ////var city = _unitOfWork.CityRepository.GetByID(cityId);
            //var city = _unitOfWork.CityRepository.GetCityById(cityId);
            //if (city != null)
            //{
            //    // var cityModel = Mapper.Map<tbl_City, CityEntity>(city);
            //    IEnumerable<CityEntity> TList = city;
            //    return TList;
            //}
            FunctionResponse RMsg = new FunctionResponse();
            IEnumerable<CityEntity> data = (IEnumerable<CityEntity>)_unitOfWork.CityRepository.GetCityById(cityId);
            try
            {
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

        public bool UpdateCity(Guid cityId, CityEntity cityEntity)
        {
            var Success = false;
            if (cityEntity != null)
            {
                var city = _unitOfWork.CityRepository.GetByID(cityId);
                if (city != null)
                {
                    city.Name = cityEntity.Name;
                    city.Code = cityEntity.Code;
                    city.StateId = cityEntity.StateId;
                    city.IsActive = cityEntity.IsActive;
                    city.UpdatedOn = DateTime.Now;
                    city.UpdatedBy = cityEntity.UpdatedBy;
                }
                _unitOfWork.CityRepository.Update(city);
                _unitOfWork.Save();
                Success = true;
            }
            return Success;
        }
    }
}
