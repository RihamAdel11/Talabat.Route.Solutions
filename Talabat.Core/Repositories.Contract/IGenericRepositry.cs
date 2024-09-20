﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepositry<T>where T:BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<T?> GetWithSpecAsync(ISpecification<T> spec);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification <T>spec);
    }
}
