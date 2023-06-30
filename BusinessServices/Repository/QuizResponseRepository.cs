using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Repository
{
    class QuizResponseRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public QuizResponseRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
