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
    public class CountryRepository : ICountryInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public CountryRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public Guid CreateCountry(CountryEntity CountryEntity)
        //{
        //    var Country = new tbl_Country
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = CountryEntity.Name,
        //        Code= CountryEntity.Code,
        //        IsActive = CountryEntity.IsActive,
        //        CreatedOn = DateTime.Now,
        //        CreatedBy=CountryEntity.CreatedBy

        //    };
        //    _unitOfWork.CountryRepository.Insert(Country);
        //    _unitOfWork.Save();
        //    return Country.Id;
        //}
        public FunctionResponse CreateCountry(CountryEntity CountryEntity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var Country = new tbl_Country
                {
                    Id = Guid.NewGuid(),
                    Name = CountryEntity.Name,
                    Code = CountryEntity.Code,
                    CreatedOn = DateTime.Now,
                    CreatedBy = CountryEntity.CreatedBy,
                    IsActive = CountryEntity.IsActive == null ? false : CountryEntity.IsActive

                };

                var user = _unitOfWork.CountryRepository.Get(u => u.Name == Country.Name);

                if (user == null)
                {
                    _unitOfWork.CountryRepository.Insert(Country);
                    _unitOfWork.Save();
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                    Resp.Data.Add(Country.Id);
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

        public bool DeleteCountry(Guid countryId)
        {
            var Success = false;
            if (countryId != null)
            {
                var country = _unitOfWork.CountryRepository.GetByID(countryId);
                if (country != null)
                {
                    _unitOfWork.CountryRepository.Delete(country);
                    _unitOfWork.Save();
                    Success = true;
                }
            }
            return Success;
        }

        public IEnumerable<CountryEntity> GetAllCountry()
        {
            var countries = _unitOfWork.CountryRepository.GetAll().OrderBy(x => x.Name).ToList();
            if (countries.Any())
            {
                var countriesModel = Mapper.Map<List<tbl_Country>, List<CountryEntity>>(countries);
                return countriesModel;
            }
            return null;
        }

        public CountryEntity GetCountryById(Guid countryId)
        {
            var countries = _unitOfWork.CountryRepository.GetByID(countryId);
            if (countries != null)
            {
                var countriesModel = Mapper.Map<tbl_Country, CountryEntity>(countries);
                return countriesModel;
            }
            return null;
        }

        //public bool UpdateCountry(Guid countryId, CountryEntity countryEntity)
        //{
        //    var Success = false;
        //    if (countryEntity != null)
        //    {
        //        var countries = _unitOfWork.CountryRepository.GetByID(countryId);
        //        if (countries != null)
        //        {
        //            countries.Name = countryEntity.Name;
        //            countries.Code = countryEntity.Code;
        //            countries.IsActive = countryEntity.IsActive;
        //            countries.UpdatedOn = DateTime.Now;
        //            countries.UpdatedBy = countryEntity.UpdatedBy;
        //        }
        //        _unitOfWork.CountryRepository.Update(countries);
        //        _unitOfWork.Save();
        //        Success = true;
        //    }
        //    return Success;
        //}
       
                                public FunctionResponse UpdateCountry(Guid countryId, CountryEntity countryEntity)
                                {

                                    try
                                    {
                                        FunctionResponse RMsg = new FunctionResponse();
                                        if (countryEntity != null && countryId != Guid.Empty)
                                        {

                                            var country = _unitOfWork.CountryRepository.GetByID(countryId);
                                            if (country != null)
                                            {
                                                var country_Name = _unitOfWork.CountryRepository.Get(s => s.Name == countryEntity.Name);
                                                if (country_Name == null)
                                                {
                                                    if (countryEntity.Name != null)
                                                        country.Name = countryEntity.Name == null ? null : countryEntity.Name.Trim();

                                                    if (countryEntity.Code != null)
                                                        country.Code = countryEntity.Code;


                                                    if (countryEntity.IsActive != null)
                                                        country.IsActive = countryEntity.IsActive;



                                                    country.UpdatedOn = DateTime.Now;
                                                    country.UpdatedBy = countryEntity.UpdatedBy;

                                                    _unitOfWork.CountryRepository.Update(country);
                                                    if (_unitOfWork.Save() > 0)
                                                    {
                                                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                                        RMsg.Data.Add(country.Id);
                                                        RMsg.Message = "Success";

                                                    }
                                                    else
                                                    {
                                                        RMsg.Message = "Error";
                                                        RMsg.Status = FunctionResponse.StatusType.ERROR;
                                                    }
                                                }
                                                else
                                                {
                                                    //var country_Code = _unitOfWork.CountryRepository.Get(s => s.Code == countryEntity.Code);
                                                    if (country.IsActive != countryEntity.IsActive)
                                                    {
                                                        if (countryEntity.Name != null)
                                                            country.Name = countryEntity.Name == null ? null : countryEntity.Name.Trim();

                                                        if (countryEntity.Code != null)
                                                            country.Code = countryEntity.Code;


                                                        if (countryEntity.IsActive != null)
                                                            country.IsActive = countryEntity.IsActive;



                                                        country.UpdatedOn = DateTime.Now;
                                                        country.UpdatedBy = countryEntity.UpdatedBy;

                                                        _unitOfWork.CountryRepository.Update(country);
                                                        if (_unitOfWork.Save() > 0)
                                                        {
                                                            RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                                                            RMsg.Data.Add(country.Id);
                                                            RMsg.Message = "Success";

                                                        }
                                                        else
                                                        {
                                                            RMsg.Message = "Error";
                                                            RMsg.Status = FunctionResponse.StatusType.ERROR;
                                                        }

                                                    }


                                                    else
                                                    {
                                                        RMsg.Message = "Duplicate";
                                                        RMsg.Status = FunctionResponse.StatusType.ERROR;
                                                    }

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
                    
