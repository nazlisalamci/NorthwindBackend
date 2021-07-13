using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
   public  interface ICategoryService
    {
        Result Add(Category category);
        Result Update(Category category);
        Result Delete(Category category);
        IDataResult<List<Category>> GetList();


    }
}
