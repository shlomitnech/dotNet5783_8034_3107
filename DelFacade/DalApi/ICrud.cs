using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T> where T : struct
{
    T Add(T item);
    T Delete(T item);
    T Update(T item);
    T Get(T item);

    IEnumerable<T> GetAll(Func<T, bool>? filter = null);
    T GetByFilter(Func<T, bool>? filter);

}

