using DADApp.inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DADApp.forms
{
    class XMLService
    {
        private const String NAME_ATTR = "Name";
        private const String COINT_ATTR = "Count";
        private const String WEIGHT_ATTR = "WeightOne";
        private const String CATEGORY_ATTR = "Category";
        private const String DISKRIPTION_ATTR = "Discription";
        private const String INVENTORY_ROOT_ATTR = "InventoryObj";
        private const String INVENTORY_ROOT_ROOT_ATTR = "Inventory";

        public static ArrayList ParseXMLToDAO()
        {
            ArrayList listDAO = new ArrayList();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(DADConstants.PATH_TO_XML_INVENT);
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    InventoryDAO inventoryDAO = new InventoryDAO();
                    String Name = String.Empty;
                    int Count = 0;
                    double WeightOne = 0D;
                    String Category = String.Empty;
                    String Discription = String.Empty;

                    // получаем атрибут name
                    XmlNode attr = xnode.Attributes.GetNamedItem(NAME_ATTR);
                    Name = attr?.Value;

                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        switch (childnode.Name)
                        {
                            case COINT_ATTR:
                                Count = Convert.ToInt32(childnode.InnerText);
                                break;
                            case WEIGHT_ATTR:
                                WeightOne = Convert.ToDouble(childnode.InnerText);
                                break;
                            case CATEGORY_ATTR:
                                Category = attr?.Value;
                                break;
                            case DISKRIPTION_ATTR:
                                Discription = attr?.Value;
                                break;
                        }
                    }
                    listDAO.Add(new InventoryDAO(Name, Count, WeightOne, Category, Discription));
                }
            }
            return listDAO;
        }

        public static void ParseDAOToXML(ArrayList listDAO)
        {
            XmlDocument xDoc = new XmlDocument();

            ArrayList arrayListElement = new ArrayList();
            XmlNode xRoot = xDoc.CreateElement(INVENTORY_ROOT_ROOT_ATTR);
            xDoc.AppendChild(xRoot);
            // получим корневой элемент
            foreach (InventoryDAO inv in listDAO)
            {


                XmlElement personElem = xDoc.CreateElement(INVENTORY_ROOT_ATTR);

                XmlAttribute NameAttr = xDoc.CreateAttribute(NAME_ATTR);
                XmlElement CountElement = xDoc.CreateElement(COINT_ATTR);
                XmlElement WeightOneElem = xDoc.CreateElement(WEIGHT_ATTR);
                XmlElement CategoryElem = xDoc.CreateElement(CATEGORY_ATTR);
                XmlElement DiscriptionElem = xDoc.CreateElement(DISKRIPTION_ATTR);

                // создаем текстовые значения для элементов и атрибута
                XmlText NameText = xDoc.CreateTextNode(inv.Name);
                XmlText CountText = xDoc.CreateTextNode(Convert.ToString(inv.Count));
                XmlText WeightOneText = xDoc.CreateTextNode(Convert.ToString(inv.WeightOne));
                XmlText CategoryText = xDoc.CreateTextNode(inv.Category);
                XmlText DiscriptionText = xDoc.CreateTextNode(inv.Discription);


                //добавляем узлы
                NameAttr.AppendChild(NameText);
                CountElement.AppendChild(CountText);
                WeightOneElem.AppendChild(WeightOneText);
                CategoryElem.AppendChild(CategoryText);
                DiscriptionElem.AppendChild(DiscriptionText);

                personElem.Attributes.Append(NameAttr);
                personElem.AppendChild(CountElement);
                personElem.AppendChild(WeightOneElem);
                personElem.AppendChild(CategoryElem);
                personElem.AppendChild(DiscriptionElem);
                xRoot.AppendChild(personElem);

            }
            xDoc.Save(DADConstants.PATH_TO_XML_INVENT);
        }
    }
}
