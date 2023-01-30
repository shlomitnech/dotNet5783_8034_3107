using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;

namespace Dal;

internal class OrderItem : IOrderItem
{
    string orderItemPath = @"OrderItem.xml";
    string configPath = @"Config.xml";
    public int Add(DO.OrderItem item)
    {
        XElement orderItemRoot = XmlTools.LoadListFromXMLElement(orderItemPath); //get all the elements from the file

        //check if the orderItem exists in th file
        var orderItemFromFile = (from oi in orderItemRoot.Elements()
                                 where (oi != null && oi.Element("ID")!.Value == item.ID.ToString())
                                 select oi).FirstOrDefault();

        //throw an exception
        if (orderItemFromFile != null)
            throw new DalApi.Duplicates("the order already exists");

        //get running order item ID number
        List<RunningNumber> runningList = XmlTools.LoadListFromXMLSerializer<RunningNumber>(configPath);

        var runningNum = (from number in runningList
                          where (number.typeOfnumber == "Order item running number")
                          select number).FirstOrDefault();
        runningList.Remove(runningNum);//remove the saved number from list
        runningNum.numberSaved++;//add one to the saved number
        runningList.Add(runningNum);//add the number back to list
        int temp = (int)runningNum.numberSaved;//save the running number



        //add the orderItem to the root element
        orderItemRoot.Add(
            new XElement("OrderItem",
            new XElement("ID", temp),
            new XElement("ProductID", item.productID),
            new XElement("OrderID", item.orderID),
            new XElement("Price", item.Price),
            new XElement("Amount", item.amount)));

        //save the root in the file
        XmlTools.SaveListToXMLElement(orderItemRoot, orderItemPath);
        return item.ID;
    }

    public void Delete(int id)
    {
        List<DO.OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);

        DO.OrderItem temp = (from item in orderItemList
                             where item != null && item?.ID == id
                             select (DO.OrderItem)item).FirstOrDefault();

        if (temp.ID.Equals(default(Order)))
            throw new DalApi.EntityNotFound("the product does not exist");

        orderItemList.Remove(temp);

        XmlTools.SaveListToXMLSerializer(orderItemList, orderItemPath);
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        List<DO.OrderItem?> orderItemList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath).ToList();

        return (from order in orderItemList
                where filter(order)
                select order).ToList();
    }

    public DO.OrderItem GetByFilter(Func<DO.OrderItem?, bool>? filter)
    {
        List<DO.OrderItem?> orderItemList = GetAll().ToList();

        return (from item in orderItemList
                where filter(item)
                select (DO.OrderItem)item).FirstOrDefault();
    }

    public DO.OrderItem? Get(int id)
    {
        List<DO.OrderItem?> orderItemList = GetAll().ToList();

        return (from item in orderItemList
                where item != null && item?.ID == id
                select (DO.OrderItem)item).FirstOrDefault();
        throw new DalApi.EntityNotFound("the Order Item requested does not exist");
    }

    public DO.OrderItem ItemOfOrder(int id, int productId)
    {
        List<DO.OrderItem?> orderItemList = GetAll().ToList();

        return (from item in orderItemList
                where item != null && item?.productID == productId && item?.orderID == id
                select (DO.OrderItem)item).FirstOrDefault();
        throw new DalApi.EntityNotFound("the Order Item requested does not exist");
    }

    public IEnumerable<DO.OrderItem?> ItemsInOrder(int id)
    {
        List<DO.OrderItem?> orderItemList = GetAll().ToList();

        return (from item in orderItemList
                where item?.orderID == id
                select item).ToList();
    }

    public void Update(DO.OrderItem item)
    {
        DO.OrderItem? temp = Get(item.ID);//get the order item requested to update 
        List<DO.OrderItem?> orderItemList = GetAll().ToList();//get all order items from ile
        orderItemList.Remove(temp);//remove the existing order item
        orderItemList.Add(item);//add the updated order item

        XmlTools.SaveListToXMLSerializer(orderItemList, orderItemPath);//save back into file    
    }
}

