using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

class BLImp : IBl
{
    #region Singleton

    static BLImp() { }

    public static BLImp Instance => Instance;



}