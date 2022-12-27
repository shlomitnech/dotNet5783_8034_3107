using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T> where T : struct
{
    int Add(T item);
    void Delete(int id);
    void Update(T item);
    T Get(int id);
    IEnumerable<T> GetAll();

}

