using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessServices.Interface;
using DataModel;
using DataModel.UnitOfWork;

namespace BusinessServices.Repository
{
    public class MailerRepository : IMailerInterface
    {
        private readonly UnitOfWork _unitOfWork;
        public MailerRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public FunctionResponse Create(MailerEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var duplicate = _unitOfWork.MailerRepository.Get(n => n.Name.Trim() == entity.Name.Trim());
                if (duplicate == null)
                {
                    var mailer = new tbl_Mailer
                    {
                        Id = Guid.NewGuid(),
                        Name = entity.Name.Trim(),
                        Mailer = entity.Mailer,
                        IsActive = entity.IsActive == null ? false : entity.IsActive
                    };

                    _unitOfWork.MailerRepository.Insert(mailer);

                    if (_unitOfWork.Save() > 0)
                    {
                        Resp.Status = FunctionResponse.StatusType.SUCCESS;
                        Resp.Message = "Success";
                    }
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
        public FunctionResponse GetAll()
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var data = _unitOfWork.MailerRepository.GetAll();

                if (data != null)
                {
                    Resp.Data.Add(data);
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                }

                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "No Record";

                }

            }
            catch (Exception)
            {

                throw;
            }

            return Resp;
        }
        public FunctionResponse GetById(Guid Id)
        {

            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var data = _unitOfWork.MailerRepository.GetByID(Id);

                if (data != null)
                {
                    Resp.Data.Add(data);
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                }

                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "No Record";

                }

            }
            catch (Exception)
            {

                throw;
            }

            return Resp;
        }
        public FunctionResponse GetByName(string name)
        {

            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var data = _unitOfWork.MailerRepository.Get(x=>x.Name==name);

                if (data != null)
                {
                    Resp.Data.Add(data);
                    Resp.Status = FunctionResponse.StatusType.SUCCESS;
                    Resp.Message = "Success";
                }

                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "No Record";

                }

            }
            catch (Exception)
            {

                throw;
            }

            return Resp;
        }
        public FunctionResponse Update(Guid Id, MailerEntity entity)
        {
            FunctionResponse Resp = new FunctionResponse();
            try
            {
                var data = _unitOfWork.MailerRepository.GetByID(Id);
                if (data != null)
                {
                   // var duplicate = _unitOfWork.MailerRepository.Get(x => x.Name.Trim() == entity.Name.Trim());
                    //if (duplicate==null)
                    //{
                        data.Name = entity.Name == null ? data.Name : entity.Name;
                        data.Mailer = entity.Mailer == null ? data.Mailer : entity.Mailer;
                        data.IsActive = entity.IsActive == null ? false : entity.IsActive;
                        _unitOfWork.MailerRepository.Update(data);

                        if (_unitOfWork.Save() > 0)
                        {
                            Resp.Status = FunctionResponse.StatusType.SUCCESS;
                            Resp.Message = "Success";
                        }
                    //}
                    //else
                    //{
                    //    Resp.Status = FunctionResponse.StatusType.ERROR;
                    //    Resp.Message = "Duplicate";

                    //}

                }
                else
                {
                    Resp.Status = FunctionResponse.StatusType.ERROR;
                    Resp.Message = "No Record";

                }



            }
            catch (Exception)
            {

                throw;
            }

            return Resp;
        }
        public FunctionResponse Delete(Guid Id)
        {
            try
            {
                FunctionResponse RMsg = new FunctionResponse();
               
                    _unitOfWork.MailerRepository.Delete(Id);
                    if (_unitOfWork.Save() > 0)
                    {
                        RMsg.Status = FunctionResponse.StatusType.SUCCESS;
                        RMsg.Message = "Success";
                    }
                //}

                //else
                //{
                //    RMsg.Status = FunctionResponse.StatusType.CRITICAL_ERROR;
                //    RMsg.Message = "Something Wrong";
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
