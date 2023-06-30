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
    public class StateRepository : IStateInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public StateRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FunctionResponse GetStateByCountryId(Guid Id)
        {
            FunctionResponse RMsg = new FunctionResponse();
            IEnumerable<StateEntity> data = _unitOfWork.StateRepository.GetStateByCountryID(Id);            

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

        public Guid CreateState(StateEntity stateEntity)
        {
            var State = new tbl_State
            {
                Id = Guid.NewGuid(),
                Name = stateEntity.Name,
                Code=stateEntity.Code,
                CountryId=stateEntity.CountryId,               
                IsActive = stateEntity.IsActive,
               
            };
            _unitOfWork.StateRepository.Insert(State);
            _unitOfWork.Save();
            return State.Id;
        }

        public bool DeleteState(Guid stateId)
        {
            var Success = false;
            if (stateId != null)
            {
                var state = _unitOfWork.StateRepository.GetByID(stateId);
                if (state != null)
                {
                    _unitOfWork.StateRepository.Delete(state);
                    _unitOfWork.Save();
                    Success = true;
                }
            }
            return Success;
        }

        public IEnumerable<StateEntity> GetAllStates()
        {
            var states = _unitOfWork.StateRepository.GetState();
            //var states = _unitOfWork.StateRepository.GetAll().ToList();
            if (states.Any())
            {
               // var statesModel = Mapper.Map<List<tbl_State>, List<StateEntity>>(states);
                IEnumerable<StateEntity> TList = states;
                return TList;
            }
            return null;
        }

        public IEnumerable<StateEntity> GetStateById(Guid stateId)
        {
           // var states = _unitOfWork.StateRepository.GetByID(stateId);
            var states = _unitOfWork.StateRepository.GetStateById(stateId);
            if (states != null)
            {
                IEnumerable<StateEntity> TList = states;
                //var statesModel = Mapper.Map<tbl_State, StateEntity>(states);
                return TList;
            }
            return null;
        }

        public bool UpdateState(Guid stateId, StateEntity stateEntity)
        {
            var Success = false;
            if (stateEntity != null)
            {
                var states = _unitOfWork.StateRepository.GetByID(stateId);
                if (states != null)
                {
                    states.Name = stateEntity.Name;
                    states.Code = stateEntity.Code;
                    states.CountryId = stateEntity.CountryId;
                    states.IsActive = stateEntity.IsActive;
                    states.UpdatedOn = DateTime.Now;
                    states.UpdatedBy = stateEntity.UpdatedBy;

                }
                _unitOfWork.StateRepository.Update(states);
                _unitOfWork.Save();
                Success = true;
            }
            return Success;
        }
    }
}
