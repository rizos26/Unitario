namespace ApiCalled
{
    public partial class Form1 : Form
    {
        ConfigReader Config { get; set; }
        public Form1()
        {
            InitializeComponent();
            Config = new ConfigReader();
            label1.Text = Config.test;
        }

        private void button1_Click(object sender, EventArgs e)
        {

           // new utilityRead().lecturaXML_Mode(Config.fichero);
           // new utilityRead().LecturaXML_Deserialize(Config.fichero);

           // new utilityRead().LecturaXML_PizzaDesearialize(Config.ficheroPizza);
            utilityRead utilityRead= new utilityRead();
            var data = utilityRead.LecturaXML_PizzaDesearialize(Config.ficheroPizza); 
            treeView1.Nodes.Clear();  

            AddNodesToTreeview(treeView1.Nodes,data);
        }
        private void AddNodesToTreeview(TreeNodeCollection nodes, PizzasReaded data)
        {
            foreach (var pizza in data.Pizzas)
            {
                TreeNode pizzaNode = new TreeNode($"{pizza.Nombre} - {pizza.Precio}");

                foreach (var ingrediente in pizza.Ingredientes)
                {
                    TreeNode ingredienteNode = new TreeNode(ingrediente.Nombre); 
                    pizzaNode.Nodes.Add(ingredienteNode);
                }

                nodes.Add(pizzaNode);
            }
        }


    }
}