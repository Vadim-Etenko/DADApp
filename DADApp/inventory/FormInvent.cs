using DADApp.forms;
using DADApp.inventory;
using System;
using System.Collections;
using System.Windows.Forms;

namespace DADApp
{
    public partial class Инвентарь : Form
    {
        private ArrayList inventoryList;
        

        public Инвентарь()
        {
            InitializeComponent();
            inventoryList = XMLService.ParseXMLToDAO();
            if (inventoryList == null)
            {
                inventoryList = new ArrayList();
            }
            LoadToDataGrid(inventoryList);
        }

        private void FormInvent_Load(object sender, EventArgs e)
        {

        }

        private void МенюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           XMLService.ParseDAOToXML(UpdateList(inventoryList));
        }

        private void LoadToDataGrid(ArrayList invList)
        {
            foreach (InventoryDAO inv in invList)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = inv.Name;
                row.Cells[1].Value = inv.Count;
                row.Cells[2].Value = inv.WeightOne;
                row.Cells[3].Value = inv.Category;
                row.Cells[4].Value = inv.TotalWeight;
                row.Cells[3].Value = inv.Discription;
                dataGridView1.Rows.Add(row);
            }
        }

        

        public ArrayList UpdateList(ArrayList listDAO)
        {
            String Name = String.Empty;
            int Count = 0;
            double WeightOne = 0D;
            String Category = String.Empty;
            String Discription = String.Empty;
            listDAO = new ArrayList();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                int j = 0;
                if (dataGridView1[j, i].Value != null)
                {
                    Name = dataGridView1[j++, i].Value.ToString();
                    Count = int.Parse(dataGridView1[j++, i].Value.ToString());
                    WeightOne = Double.Parse(dataGridView1[j++, i].Value.ToString());
                    Category = dataGridView1[j++, i].Value.ToString();
                    Discription = dataGridView1[j++, i].Value.ToString();
                    listDAO.Add(new InventoryDAO(Name, Count, WeightOne, Category, Discription));
                }
            }
            return listDAO;
        }

        private void Инвентарь_FormClosing(object sender, FormClosingEventArgs e)
        {
            XMLService.ParseDAOToXML(UpdateList(inventoryList));
            Application.Exit();
        }
    }
}
