using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Logging;
using Core.Aspect.Autofac.Performance;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private ICategoryService _categoryService;
        
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
               
        }
        [ValidationAspect(typeof(ProductValidator),Priority =1)]
        [CacheRemoveAspect("IProductService.Get")]
        [CacheRemoveAspect("ICategoryService.Get")]

        public IResult  Add(Product product)
        {
            IResult result =BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfCategoryIsEnable());
            if (result != null)
            {
                return result;
            }
           
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfCategoryIsEnable()
        {
            var result = _categoryService.GetList();
            if (result.Data.Count<10)
            {
                return new ErrorResult();

            }
            return new SuccessResult();


        }

        private IResult CheckIfProductNameExists(string productName)
        {
            if (_productDal.Get(p => p.ProductName == productName) != null)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult() ;
        }

        public  IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new  SuccessResult(Messages.ProductDeleted);
        }
       public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

       public IDataResult<Product >GetById(int productId)
        {
           
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId)) ;
        }
        [PerformanceAspect(5)]
        public IDataResult< List<Product>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }
        [SecuredOperation("Product.List,Admin")]
        [CacheAspect(duration:10)]
        [LogAspect(typeof(FileLogger))]
       public IDataResult< List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
