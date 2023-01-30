using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal;

internal class Order : IOrder
{
    string orderPath = @"Order.xml";
    string configPath = @"Config.xml";
    public int Add(DO.Order item)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath); //get all the elements from the file
        XElement configRoot = XmlTools.LoadListFromXMLElement(configPath);

        //check if the order exists in th file
        var orderFromFile = (from order in orderRoot.Elements()
                             where (order != null && order.Element("ID")!.Value == item.ID.ToString())
                             select order).FirstOrDefault();

        //throw an exception
        if (orderFromFile != null)
            throw new DalApi.Duplicates("the order already exists");

        //get running order ID number
        /*
        List<RunningNumber> runningList = XmlTools.LoadListFromXMLSerializer<RunningNumber>(configPath);

        var runningNum = (from number in runningList
                          where (number.typeOfnumber == "Order running number")
                          select number).FirstOrDefault();
        runningList.Remove(runningNum);//remove the saved number from list
        runningNum.numberSaved++;//add one to the saved number
        runningList.Add(runningNum);//add the number back to list
        int temp = (int)runningNum.numberSaved;//save the running number

        */
        int temp = Convert.ToInt32(configRoot.Element("OrderIncrementalID")!.Element("OrdID")!.Value);
        configRoot.Element("OrdID")!.Value = (temp + 1).ToString();
        configRoot.Save("configPath");
        //add the order to the root element
        orderRoot.Add(
            new XElement("Order",
            new XElement("ID", temp),
            new XElement("CustomerName", item.CustomerName),
            new XElement("CustomerEmail", item.CustomerEmail),
            new XElement("CustomerAddress", item.ShippingAddress),
            new XElement("OrderDate", item.OrderDate)),
            new XElement("ShippingDate", item.ShippingDate),
            new XElement("DeliveryDate", item.DeliveryDate));

        //save the root in the file
        XmlTools.SaveListToXMLElement(orderRoot, orderPath);
        orderRoot.Save(orderPath);
        return item.ID;
    }

    public void Delete(int id)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath);

        XElement orderElement;
        orderElement = (from ord in orderRoot.Elements()
                        where Convert.ToInt32(ord.Element("ID")!.Value) == id
                        select ord).FirstOrDefault()!;
        if (orderElement == null)
        {
            throw new EntityNotFound();
        }
        orderElement.Remove();
        orderRoot.Save(orderPath);
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath);
        List<DO.Order> list = new List<DO.Order>();
        list = (from ord in orderRoot.Elements()
                select new DO.Order()
                {
                    ID = Convert.ToInt32(ord.Element("ID")!.Value),
                    CustomerName = ord.Element("CustomerName")!.Value,
                    CustomerEmail = ord.Element("Email")!.Value,
                    ShippingAddress = ord.Element("Address")!.Value,
                    OrderDate = Convert.ToDateTime(ord.Element("OrderDate")!.Value),
                    ShippingDate = Convert.ToDateTime(ord.Element("ShippingDate")!.Value),
                    DeliveryDate = Convert.ToDateTime(ord.Element("DeliveryDate")!.Value)
                }).ToList();
        //List<DO.Order?> orderList = XmlTools.LoadListFromXMLSerializer<DO.Order?>(orderPath).ToList();
        return (IEnumerable<DO.Order?>)list;
    }

    public DO.Order GetByFilter(Func<DO.Order?, bool>? filter)
    {
        List<DO.Order?> orderList = GetAll().ToList();

        return (from item in orderList
                where filter!(item)
                select (DO.Order)item).FirstOrDefault();
    }

    public DO.Order? Get(int id)
    {
        List<DO.Order?> orderList = GetAll().ToList();

        return (from item in orderList
                where item != null && item?.ID == id
                select (DO.Order)item).FirstOrDefault();
        throw new DalApi.EntityNotFound("The product requested does not exist");
    }

    public void Update(DO.Order item)
    {
        XElement orderRoot = XmlTools.LoadListFromXMLElement(orderPath);
        XElement orderElement = (from ord in orderRoot.Elements()
                                 where Convert.ToInt32(ord.Element("ID")!.Value) == item.ID
                                 select ord).FirstOrDefault()!;
        orderElement?.Remove();
        orderElement!.Element("CustomerName")!.Value = item.CustomerName!;
        orderElement.Element("Email")!.Value = item.CustomerEmail!;
        orderElement.Element("Address")!.Value = item.ShippingAddress!;
        orderElement.Element("OrderDate")!.Value = item.OrderDate.ToString()!;
        orderElement.Element("ShippingDate")!.Value = item.ShippingDate.ToString()!;
        orderElement.Element("DeliveryDate")!.Value = item.DeliveryDate.ToString()!;

        orderRoot.Save(orderPath);
    }
}

