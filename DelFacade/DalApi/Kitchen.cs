using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public static class Kitchen
{
    public static IDal? Get()
    {
        string dalType = s_dalName
            ?? throw new Exceptions($"DAL name is not extracted from the configuration");
        string dal = s_dalPackages[dalType]
           ?? throw new Exceptions($"Package for {dalType} is not found in packages list");

        try
        {
            Assembly.Load(dal ?? throw new Exceptions($"Package {dal} is null"));
        }
        catch (Exception)
        {
            throw new Exceptions("Failed to load {dal}.dll package");
        }

        Type? type = Type.GetType($"Dal.{dal}, {dal}")
            ?? throw new Exceptions($"Class Dal.{dal} was not found in {dal}.dll");

        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                   .GetValue(null) as IDal
            ?? throw new Exceptions($"Class {dal} is not singleton or Instance property not found");
    }

}
