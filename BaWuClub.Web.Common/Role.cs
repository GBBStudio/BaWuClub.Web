using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace BaWuClub.Web.Common
{
    public class Role
    {
        private string _InitializeMessage = "初始化失败！";
        private bool _InitializeSuccess = false;

        public Role(string path) 
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
                InitializeRole(file);
        }

        public string InitializeMessage { set { _InitializeMessage = value; } get { return _InitializeMessage; } }

        public Boolean InitializeSuccess { set { _InitializeSuccess = value; } get { return _InitializeSuccess; } }

        public List<Operate> Operates { get; set; }

        public List<Menu> Menus { get; set; }

        public Dictionary<string, string> Urls { get; set; }

        private void InitializeRole(FileInfo file)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(file.ToString());
            XmlElement root=xml.DocumentElement;
            List<Operate> operates = new List<Operate>();
            Dictionary<int, string> operatesDic = new Dictionary<int, string>();
            foreach(XmlNode node in root.GetElementsByTagName("operates")){
                foreach (XmlNode n in node.SelectNodes("operate")) {
                    Operate operate = new Operate();
                    operate.Name = n.Attributes["name"].Value.ToString();
                    operate.Value = Convert.ToInt32(n.Attributes["value"].Value.ToString());
                    operates.Add(operate);
                    operatesDic.Add(Convert.ToInt32(n.Attributes["value"].Value.ToString()), n.Attributes["url"].Value.ToString());
                }
            }
            this.Operates = operates;
            List<Menu> menus = new List<Menu>();
            foreach (XmlNode node in root.GetElementsByTagName("menus")) {
                foreach (XmlNode menuNode in node.SelectNodes("menu")) {
                    Menu menu = new Menu();
                    List<SubMenu> subMenus = new List<SubMenu>();
                    foreach (XmlNode subNode in menuNode.SelectNodes("submenu"))
                    {
                        SubMenu subMenu = new SubMenu();
                        List<Item> items = new List<Item>();
                        Dictionary<int, string> Urls = new Dictionary<int, string>();
                        subMenu.Name = subNode.Attributes["name"].Value.ToString();
                        subMenu.Value = Convert.ToInt32(subNode.Attributes["value"].Value.ToString());
                        foreach (XmlNode nodeItem in subNode.SelectNodes("item"))
                        {
                            Item item = new Item();
                            item.Name = nodeItem.Attributes["name"].Value.ToString();
                            item.Value = Convert.ToInt32(nodeItem.Attributes["value"].Value);
                            item.Operates = nodeItem.Attributes["operates"].Value.ToString();
                            item.Url = nodeItem.Attributes["url"].Value.ToString();
                            items.Add(item);
                            if (item.Operates != null) {
                                string[] operateArray = (nodeItem.Attributes["operates"].Value.ToString()).Split(',');
                                foreach (string operateStr in operateArray) { 
                                  //  if(Convert.toi)
                                }
                            }
                            Urls.Add(Convert.ToInt32(nodeItem.Attributes["value"].Value), nodeItem.Attributes["url"].Value.ToString());
                        }
                        menu.Name = menuNode.Attributes["name"].Value.ToString();
                        menu.Value = Convert.ToInt32(menuNode.Attributes["value"].Value);
                        subMenu.Items = items;
                        subMenus.Add(subMenu);
                        menu.SubMenus = subMenus;
                    }
                    menus.Add(menu);      
                }
                this.Menus = menus;
            }
            this.InitializeMessage = "文件初始化完毕！";
            this.InitializeSuccess = true;
        }
    }
    
    public struct Operate
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Url { get; set; }
    }

    public struct Menu
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public List<SubMenu> SubMenus{get;set;}
    }

    public struct SubMenu
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Url { get; set; }
        public List<Item> Items { get; set; }
    }

    public struct Item {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Url { get; set; }
        public string Operates { get; set; }
    }
}
