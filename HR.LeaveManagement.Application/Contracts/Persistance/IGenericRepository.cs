using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistance
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}




/*

Generic interface, meaning it can be used with different types of classes (T)

This interface is typically used to provide a common set of CRUD (Create, Read, Update, Delete) operations that can be applied to any entity type without needing to duplicate code for each entity. You can have a single implementation of a repository that can handle different entities

Benefits:
Code Reusability: You write your data access logic once and use it with any entity type (e.g., Customer, Product, etc.).
Abstraction: It abstracts away the underlying data access mechanism, so the rest of the application doesn’t need to know whether you’re using a database, in-memory store, or any other persistence mechanism.
Maintainability: If the data access logic needs to change, you only need to modify the repository implementation in one place, and it will apply to all entities.

In short, IGenericRepository<T> helps keep your code clean, DRY (Don't Repeat Yourself), and easier to maintain by providing a generic solution for data access.

*/