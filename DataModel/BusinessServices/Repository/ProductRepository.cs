using System.Collections.Generic;
using System.Linq;
//using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using BusinessServices.Interface;
using System;

namespace BusinessServices.Repository
{
    public class ProductRepository:IProductInterface
    {
        private readonly UnitOfWork _unitOfWork;
       // Public constructor.
        public ProductRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
       // Fetches product details by id
        public BusinessEntities.ProductEntity GetProductById(Guid productId)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productId);
            if (product != null)
            {
                var productModel = Mapper.Map<tbl_Product, ProductEntity>(product);
                return productModel;
            }
            return null;
        }
       // Fetches all the products.
        public IEnumerable<BusinessEntities.ProductEntity> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
            if (products.Any())
            {
                var productsModel = Mapper.Map<List<tbl_Product>, List<ProductEntity>>(products);
                return productsModel;
            }
            return null;
        }
        // Creates a product
        public Guid CreateProduct(BusinessEntities.ProductEntity productEntity)
        {
            //using (var scope = new TransactionScope())
            //{
            var product = new tbl_Product
            {
                Name = productEntity.Name
                
                };
                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                //scope.Complete();
                return product.Id;
           // }
        }
       // Updates a product
        public bool UpdateProduct(Guid productId, BusinessEntities.ProductEntity productEntity)
        {
            var success = false;
            if (productEntity != null)
            {
               // using (var scope = new TransactionScope())
               // {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {
                        product.Name = productEntity.Name;
                        _unitOfWork.ProductRepository.Update(product);
                        _unitOfWork.Save();
                       // scope.Complete();
                        success = true;
                    }
                //}
            }
            return success;
        }
       // Deletes a particular product
        public bool DeleteProduct(Guid productId)
        {
            var success = false;
            if (productId !=null)
            {
               // using (var scope = new TransactionScope())
               // {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {

                        _unitOfWork.ProductRepository.Delete(product);
                        _unitOfWork.Save();
                       // scope.Complete();
                        success = true;
                    }
                //}
            }
            return success;
        }
    }
}
