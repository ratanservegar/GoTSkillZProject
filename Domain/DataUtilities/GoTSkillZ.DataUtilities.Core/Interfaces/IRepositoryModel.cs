using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoTSkillZ.DataUtilities.Core.Interfaces
{
    public interface IRepositoryModel<T>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        T Get(int entityId);

        T Add(T T);

        T Update(T T);

        void Delete(int entityId);
    }
}