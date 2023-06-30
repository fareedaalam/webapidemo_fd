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
    public class LocationRepository : ILocationInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public LocationRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Guid CreateLocation(LocationEntity locationEntity)
        {
            var Location = new tbl_Location
            {
                Id = Guid.NewGuid(),
                Name = locationEntity.Name,
                Code = locationEntity.Code,
                CityId = locationEntity.CityId,
                IsActive = locationEntity.IsActive,
                CreatedOn = DateTime.Now,
                CreatedBy = locationEntity.CreatedBy
            };
            _unitOfWork.LocationRepository.Insert(Location);
            _unitOfWork.Save();
            return Location.Id;
        }

        public bool DeleteLocation(Guid locationId)
        {
            var Success = false;
            if (locationId != null)
            {
                var location = _unitOfWork.LocationRepository.GetByID(locationId);
                if (location != null)
                {
                    _unitOfWork.LocationRepository.Delete(location);
                    _unitOfWork.Save();
                    Success = true;
                }
            }
            return Success;
        }

        public FunctionResponse GetAllLocation()
        {
           
            FunctionResponse RMsg = new FunctionResponse();
           
            try
            {
                IEnumerable<LocationEntity> data = _unitOfWork.LocationRepository.GetLocation();
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

        public FunctionResponse GetLocationById(Guid locationId)
        {
            
            FunctionResponse RMsg = new FunctionResponse();

            try
            {
                IEnumerable<LocationEntity> data = _unitOfWork.LocationRepository.GetLocationById(locationId);
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

        public bool UpdateLocation(Guid locationId, LocationEntity locationEntity)
        {
            var Success = false;
            if (locationEntity != null)
            {
                var locations = _unitOfWork.LocationRepository.GetByID(locationId);
                if (locations != null)
                {
                    locations.Name = locationEntity.Name;
                    locations.Code = locationEntity.Code;
                    locations.CityId = locationEntity.CityId;
                    locations.IsActive = locationEntity.IsActive;
                    locations.UpdatedOn = DateTime.Now;
                    locations.UpdatedBy = locationEntity.UpdatedBy;
                }
                _unitOfWork.LocationRepository.Update(locations);
                _unitOfWork.Save();
                Success = true;
            }
            return Success;
        }

        public FunctionResponse GetLocationByCityId(Guid CityId)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
               var data = _unitOfWork.LocationRepository.GetMany(x => x.CityId == CityId).ToList<tbl_Location>();

                if (data != null)
                {
                    var dataModel = Mapper.Map<List<tbl_Location>, List<LocationEntity>>(data);
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
    }
}
