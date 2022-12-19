using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public static class Kitchen
{
    public static IDal? Get()
    {
        string dalType = s_dalName
            ?? throw new ArgumentNullException();
        string dal = s_dalPackages[dalType]
            ?? throw new ArgumentNullException();

       try
        {

        }
    }
}
