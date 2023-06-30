
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
    public class CategoryRepository : ICategoryInterface
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryRepository(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public Guid CreateCategory(CategoryEntity categoryEntity)
        {
            var category = new tbl_Category
            {
                Id = Guid.NewGuid(),
                Name = categoryEntity.Name,
                CreatedOn = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                IsActive = categoryEntity.IsActive
            };
            _unitOfWork.CategoryRepository.Insert(category);
            _unitOfWork.Save();
            return category.Id;
        }

        public bool DeleteCategory(Guid categoryId)
        {
            var success = false;
            if(categoryId !=null)
            {
                var category = _unitOfWork.CategoryRepository.GetByID(categoryId);
                if(category != null)
                {
                    _unitOfWork.CategoryRepository.Delete(category);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }

        public IEnumerable<CategoryEntity> GetAllCategory()
        {
            var category = _unitOfWork.CategoryRepository.GetAll().ToList();
            if (category.Any())
            {
                var categoryModel = Mapper.Map<List<tbl_Category>, List<CategoryEntity>>(category);
                return categoryModel;
            }
            return null;
        }

        public CategoryEntity GetCategoryById(Guid categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetByID(categoryId);
            if (category != null)
            {
                var categoryModel = Mapper.Map<tbl_Category, CategoryEntity>(category);
                return categoryModel;
            }
            return null;
        }

        public bool UpdateCategory(Guid categoryId, CategoryEntity categoryEntity)
        {
            var success = false;
            if (categoryEntity != null)
            {
                var category = _unitOfWork.CategoryRepository.GetByID(categoryId);
                if (category != null)
                {
                    category.Name = categoryEntity.Name;                    
                    category.UpdatedBy = Guid.NewGuid();
                    category.UpdatedOn = DateTime.Now;
                    category.IsActive = categoryEntity.IsActive;

                    _unitOfWork.CategoryRepository.Update(category);
                    _unitOfWork.Save();

                    success = true;
                }
            }
            return success;
        }
    }
}
