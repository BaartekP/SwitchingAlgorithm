using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchingNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Node> allRouters = new List<Node>();
            List<List<Node>> BFS_lists = new List<List<Node>>();
            List<List<Node>> DFS_lists = new List<List<Node>>();
            List<List<Node>> BellmanFord_lists = new List<List<Node>>();
            List<List<Node>> Dijkstra_lists = new List<List<Node>>();
            List<Node> Network = new List<Node>();

            Node R1 = new Node("R1");
            Node R2 = new Node("R2");
            Node R3 = new Node("R3");
            Node R10 = new Node("R10");
            Node R99 = new Node("R99");

            allRouters = initialize(R1, R2, R3, R10, R99);
            List<string> routersNames = new List<string>();

            foreach (Node router in allRouters)
            {
                routersNames.Add(router.getNodeName());
            }

            clearAllRouters(allRouters);

            //Console.WriteLine("Network created");

            for (int startingIndex = 0; startingIndex < allRouters.Count; startingIndex++)
            {
                List<Node> temp = BFS(startingIndex, allRouters);
                               
                R1 = new Node("R1");
                R2 = new Node("R2");
                R3 = new Node("R3");
                R10 = new Node("R10");
                R99 = new Node("R99");
                BFS_lists.Add(temp);
                //Console.WriteLine();
                //Console.WriteLine();
            }

            foreach (List<Node> list in BFS_lists) { 
                foreach (Node node in list) {
                    node.deleteNeighbours();
                }
            }

            allRouters = initialize(R1, R2, R3, R10, R99);
            //Console.WriteLine();

            foreach(List < Node > list in BFS_lists) {
                foreach (Node node in list)
                {
                    int a = routersNames.IndexOf(node.getNodeName());
                    List<Node> n = allRouters.ElementAt(a).getNeighbours();
                    foreach (Node router in n) {
                        int x = allRouters.IndexOf(router); // index of neigbour in all routers\
                        Node y = list.ElementAt(x); //Get Node to save as neighbour
                        node.addNeighbour(y);
                    }
                }
            }


            //Console.WriteLine("BFS Algorithm Routing Tables done!\n");

            for (int startingIndex = 0; startingIndex < allRouters.Count; startingIndex++)
            {
                DFS_lists.Add(DFS(startingIndex, allRouters));
                R1 = new Node("R1");
                R2 = new Node("R2");
                R3 = new Node("R3");
                R10 = new Node("R10");
                R99 = new Node("R99");
               // Console.WriteLine();
            }
            //Console.WriteLine("DFS Algorithm Routing Tables done!\n");

            for (int startingIndex = 0; startingIndex < allRouters.Count; startingIndex++)
            {
                BellmanFord_lists.Add(BellmanFord(startingIndex, allRouters));
                R1 = new Node("R1");
                R2 = new Node("R2");
                R3 = new Node("R3");
                R10 = new Node("R10");
                R99 = new Node("R99");
                //Console.WriteLine();
            }
            //Console.WriteLine("Bellman-Ford Algorithm Routing Tables done!\n");

            for (int startingIndex = 0; startingIndex < allRouters.Count; startingIndex++)
            {
                Dijkstra_lists.Add(Dijkstra(startingIndex, allRouters));
                R1 = new Node("R1");
                R2 = new Node("R2");
                R3 = new Node("R3");
                R10 = new Node("R10");
                R99 = new Node("R99");
               //Console.WriteLine();
            }
            //Console.WriteLine("Dijkstra Algorithm Routing Tables done!\n");
            clearAllRouters(allRouters);
            foreach (Node router in allRouters) {
                Network.Add(router);
            }

            string userInput = "";

            do
            {
                Console.WriteLine("\nWhat do you want to do?");
                Console.WriteLine("[1] Establish the connection.");
                Console.WriteLine("[2] Remove the connection.");
                Console.WriteLine("[3] See BFS Algorithm Routing Tables.");
                Console.WriteLine("[4] See DFS Algorithm Routing Tables.");
                Console.WriteLine("[5] See Bellman-Ford Algorithm Routing Tables.");
                Console.WriteLine("[6] See Dijkstra Algorithm Routing Tables.");
                Console.WriteLine("Put exit to exit");
                
                userInput = Console.ReadLine();
                Console.Clear();
                switch (userInput)
                {
                    case "1":
                        foreach (Node router in allRouters) {
                            Console.Write($"{router.getNodeName()}\t");
                        }
                        Console.WriteLine();
                        Console.Write("Please give the source router: ");
                        string start = Console.ReadLine();
                        Console.Write("Please give the destination router: ");
                        string end = Console.ReadLine();
                        // Console.WriteLine("Please choose the algorithm: ");
                        Console.WriteLine("BFS is only avaiable algorithm now");
                        //Console.WriteLine("[1] BFS Algorithm.");
                        //Console.WriteLine("[2] DFS Algorithm.");
                        //Console.WriteLine("[3] Bellman-Ford Algorithm.");
                        //Console.WriteLine("[4] Dijkstra Algorithm.");
                        //string choosenAlgorithm = Console.ReadLine();

                        string choosenAlgorithm = "1";

                        int positionOfListAndParent = routersNames.IndexOf(start);
                        List<Node> listToOperate = new List<Node>();
                        Node startNode, endNode, previousNode;
                        previousNode = null;
                        int previousX = 0;

                        Console.WriteLine();

                        switch (choosenAlgorithm)
                        {
                            case "1":
                                listToOperate = BFS_lists.ElementAt(positionOfListAndParent);
                                Network = listToOperate;
                                startNode = listToOperate.ElementAt(positionOfListAndParent);
                                endNode = listToOperate.ElementAt(routersNames.IndexOf(end));
                                while (endNode != startNode)
                                {
                                    int x = endNode.getNeighbours().Count / 5 * positionOfListAndParent;
                                    int y = x + endNode.getNeighbours().Count / 5;
                                    for (int i = x; i < y; i++) {
                                        if (endNode.getNeighbours().ElementAt(i).getNodeName() == endNode.getParent())
                                        {
                                            Node c = endNode.getNeighbours().ElementAt(i);
                                            int e = routersNames.IndexOf(c.getNodeName());
                                            int g = endNode.getNeighbours().IndexOf(c);
                                            g -= x;

                                            endNode.setOccupiedLink(g, true);
                                            if (previousNode != null) {
                                                int a = endNode.getNeighbours().IndexOf(previousNode);      //8
                                                int b = a / (endNode.getNeighbours().Count / 5);            //2
                                                int h = a - (endNode.getNeighbours().Count / 5) * b;        //8 - 6 =2    
                                                endNode.setOccupiedLink(h, true);                           //7 - 6 =1
                                            }                                                               //6 - 6 =0

                                            int f = listToOperate.IndexOf(endNode);
                                            Network.ElementAt(f).updateNode(endNode);
                                            previousNode = endNode;
                                            endNode = c;
                                            break;
                                        }
                                    }
                                    previousX = x;
                                }
                                if (previousNode != null)
                                {
                                    int a = endNode.getNeighbours().IndexOf(previousNode); 
                                    int b = a / (endNode.getNeighbours().Count / 5);        
                                    int h = a - (endNode.getNeighbours().Count / 5) * b;
                                    endNode.setOccupiedLink(h, true);
                                    int f = listToOperate.IndexOf(endNode);
                                    Network.ElementAt(f).updateNode(endNode);
                                }

                                int j = 0;
                                foreach (Node node in Network) {
                                    Console.WriteLine(node.getNodeName());
                                    int x = node.getNeighbours().Count / 5 * j;
                                    int y = x + node.getNeighbours().Count / 5;
                                    for (int i = x; i < y; i++)
                                    {
                                            Console.WriteLine($"{node.getNeighbours().ElementAt(i).getNodeName()}" +
                                                $" is occupied : {node.getOccupiedNeighbour(node.getNeighbours().ElementAt(i), j)}");
                                    }
                                    j++;
                                    Console.WriteLine();
                                }
                                break;
                            case "2":
                                break;
                            case "3":
                                break;
                            case "4":
                                break;
                            default:
                                Console.WriteLine("Wrong algorithm selected!");
                                break;

                        }


                        break;
                    case "2":
                        Console.Write("This option is not supported yet");
                        break;
                    case "3":
                        foreach (List<Node> list in BFS_lists) {
                            printInfo(routersNames, list,"bfs");
                        }
                        break;
                    case "4":
                        Console.WriteLine("This operation can't be executed");
                        //foreach (List<Node> list in DFS_lists)
                        //{
                        //    printInfo(routersNames, list, "dfs");
                        //}
                        break;
                    case "5":
                        Console.WriteLine("This operation can't be executed");
                        //foreach (List<Node> list in BellmanFord_lists)
                        //{
                        //    printInfo(routersNames, list, "bf");
                        //}

                        break;
                    case "6":
                        Console.WriteLine("This operation can't be executed");
                        //foreach (List<Node> list in Dijkstra_lists)
                        //{
                        //    printInfo(routersNames, list, "bfs");
                        //}
                        break;
                    default:
                        Console.WriteLine("Wrong option selected!");
                        break;
                }

                Console.WriteLine("\n\nPress enter or type exit");

                userInput = Console.ReadLine();
                Console.Clear();


            } while (userInput != "exit");


            Console.ReadLine();



        }

        private static void printInfo(List<string> names, List<Node> list, string a)
        {
            Console.WriteLine();
            foreach (Node router in list)
            {
                Console.Write($"{router.getNodeName()}\t");
            }
            Console.WriteLine();
            foreach (Node router in list)
            {
                Console.Write($"{router.getParent()}\t");
            }
            Console.WriteLine();
            switch (a)
            {
                case "bfs":

                    foreach (Node router in list)
                    {
                        Console.Write($"{router.getDistance()}\t");
                    }
                    Console.WriteLine();
                    foreach (Node router in list)
                    {
                        Console.Write($"{router.getColor()}\t");
                    }
                    Console.WriteLine();
                    break;

                case "dfs":

                    foreach (Node router in list)
                    {
                        Console.Write($"{router.getStart()}\t");
                    }
                    Console.WriteLine();
                    foreach (Node router in list)
                    {
                        Console.Write($"{router.getStop()}\t");
                    }
                    Console.WriteLine();
                    foreach (Node router in list)
                    {
                        Console.Write($"{router.getColor()}\t");
                    }

                    break;
                case "bf":

                    foreach (Node router in list)
                    {
                        Console.Write($"{router.getDistance()}\t");
                    }

                    break;
                case "dj":

                    foreach (Node router in list)
                    {
                        Console.Write($"{router.getDistance()}\t");
                    }

                    break;
            }
        }

        private static void clearAllRouters(List<Node> allRouters)
        {
            foreach (Node router in allRouters) {
                router.setColor("white");
                router.setDIstance(-1);
                router.setParent("");
                router.setStart(-1);
                router.setEnd(-1);
            }

        }
        
        private static List<Node> initialize(Node R1, Node R2, Node R3, Node R10, Node R99)
        {
            List<Node> allRouters = new List<Node>();
            R1.addNeighbour(R2, 1);
            R1.addNeighbour(R3, 5);
            R1.addNeighbour(R99, 2);
            R1.createOccupiedNeighbours();
            allRouters.Add(R1);


            R2.addNeighbour(R1, 1);
            R2.addNeighbour(R10, 4);
            R2.createOccupiedNeighbours();
            allRouters.Add(R2);

            R3.addNeighbour(R1, 5);
            R3.addNeighbour(R10, 1);
            R3.createOccupiedNeighbours();
            allRouters.Add(R3);


            R10.addNeighbour(R2, 4);
            R10.addNeighbour(R3, 1);
            R10.addNeighbour(R99, 1);
            R10.createOccupiedNeighbours();
            allRouters.Add(R10);

            R99.addNeighbour(R1, 2);
            R99.addNeighbour(R10, 1);
            R99.createOccupiedNeighbours();
            allRouters.Add(R99);

            return allRouters;
            
        }
      
        private static List<Node> BFS(int index, List<Node> allRouters)
        {

            clearAllRouters(allRouters);
            List<Node> BFS_ = new List<Node>();
            string parenttName = null;
            int distance = 0;
            Queue<Node> Q = new Queue<Node>();

            allRouters.ElementAt(index).setParent("NIL");
            allRouters.ElementAt(index).setColor("white");
            allRouters.ElementAt(index).setDIstance(distance);
            
            Q.Enqueue(allRouters.ElementAt(index));
            while (Q.Count != 0) {
                Node node = Q.Dequeue();
                parenttName = node.getNodeName();
                distance += 1;
                foreach (Node neighbour in node.getNeighbours()) {
                    if (neighbour.getColor() == "white") {
                        neighbour.setColor("gray");
                        neighbour.setParent(node.getNodeName());
                        neighbour.setDIstance(distance);
                        Q.Enqueue(neighbour);
                    }
                }
                node.setColor("black");
            }

            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getNodeName()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getParent()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getDistance()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getColor()}\t");
            //}

            //Console.ReadLine();

            foreach (Node router in allRouters) {
                Node x = router.DeepCopy();
                //x.deleteNeighbours();
                BFS_.Add(x);
            }
            return BFS_;
        }

        private static List<Node> DFS(int startingIndex, List<Node> allRouters) {
            clearAllRouters(allRouters);
            List<Node> DFS_ = new List<Node>();
            int time = 0;
            allRouters.ElementAt(startingIndex).setParent("NIL");

            foreach (Node router in allRouters) {
                if (router.getNodeName() == allRouters.ElementAt(startingIndex).getNodeName() &&
                    router.getColor() == "white") {
                    VisitNode(router,ref time);
                }
                Node x = router.DeepCopy();
                DFS_.Add(x);
            }

            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getNodeName()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getParent()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getStart()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getStop()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getColor()}\t");
            //}
            //Console.ReadLine();

            return DFS_;
        }

        private static void VisitNode(Node router,ref int time)
        {
            router.setColor("grey");
            time += 1;
            router.setStart(time);
            foreach (Node neighbour in router.getNeighbours()) {
                if (neighbour.getColor() == "white") {
                    neighbour.setParent(router.getNodeName());
                    VisitNode(neighbour,ref time);
                }
            }
            router.setColor("black");
            time += 1;
            router.setEnd(time);
        }

        private static List<Node> BellmanFord(int startingIndex, List<Node> allRouters)
        {
            clearAllRouters(allRouters);
            List<Node> BF = new List<Node>();
            int infinity = 1000000;

            foreach (Node router in allRouters) {
                router.setDIstance(infinity);
            }

            allRouters.ElementAt(startingIndex).setParent("NIL");
            allRouters.ElementAt(startingIndex).setDIstance(0);

            for (int i = 0; i < allRouters.Count; i++)
            {
                foreach (Node router in allRouters)
                {
                    foreach (Node neighbour in router.getNeighbours()) {
                        int distance = router.getDistance() + router.getDistances(neighbour.getNodeName());
                        if (neighbour.getDistance() > distance) {
                            neighbour.setDIstance(distance);
                            neighbour.setParent(router.getNodeName());
                        }
                    }
                }
            }

            foreach (Node router in allRouters)
            {
                Node x = router.DeepCopy();
                BF.Add(x);
            }

            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getNodeName()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getParent()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getDistance()}\t");
            //}
            //Console.ReadLine();


            return BF;
        }

        private static List<Node> Dijkstra(int startingIndex, List<Node> allRouters)
        {
            clearAllRouters(allRouters);
            List<Node> DJ = new List<Node>();
            int infinity = 1000000;
            List<Node> Q = new List<Node>();
            Node nodeParent = allRouters.ElementAt(startingIndex);

            foreach (Node router in allRouters) {
                router.setDIstance(infinity);
            }

            nodeParent.setDIstance(0);
            nodeParent.setParent("NIL");

            Q.Add(nodeParent);

            while (Q.Count != 0) {
                int position = findMin(Q);
                Node minNode = Q.ElementAt(position);
                foreach (Node neighbour in minNode.getNeighbours())
                {
                    int distance = minNode.getDistance() + minNode.getDistances(neighbour.getNodeName());
                    if (neighbour.getDistance() > distance)
                    {
                        neighbour.setDIstance(distance);
                        neighbour.setParent(minNode.getNodeName());
                        Q.Add(neighbour);
                    }
                }
                Q.Remove(minNode);
            }

            foreach (Node router in allRouters)
            {
                Node x = router.DeepCopy();
                DJ.Add(x);
            }

            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getNodeName()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getParent()}\t");
            //}
            //Console.WriteLine();
            //foreach (Node router in allRouters)
            //{
            //    Console.Write($"{router.getDistance()}\t");
            //}
            //Console.ReadLine();


            return DJ;
        }

        private static int findMin(List<Node> q)
        {
            int distance = q.ElementAt(0).getDistance();
            int position = 0;
            foreach (Node node in q) {
                if (node.getDistance() < distance)
                    position = q.IndexOf(node);
            }
            return position;

            throw new NotImplementedException();
        }


    }

    public class Node
    {
        private string nodeName;
        private string parent;
        private List<Node> neighbours;
        private Dictionary<string,int> distances;
        private List<bool> occupiedNeighbours;
        private string color;
        private int distance;
        private int start;
        private int end;


        public Node(string nodeName, string parent, List<Node> neighbours, Dictionary<string, int> distances,
            List<bool> occupiedNeighbours,string color,int distance, int start, int end)
        {
            this.nodeName = nodeName;
            this.parent = parent;
            this.neighbours = neighbours;
            this.distances = distances;
            this.occupiedNeighbours = occupiedNeighbours;
            this.color = color;
            this.distance = distance;
            this.start = start;
            this.end = end;
        }

        public void deleteNeighbours() {
            this.neighbours.Clear();
        }

        public Node(string nodeName)
        {
            this.nodeName = nodeName;
            this.parent = "";
            this.neighbours = new List<Node>();
            this.distances = new Dictionary<string, int>();
            this.occupiedNeighbours = new List<bool>();
            this.color = "white";
            this.distance = -1;
            this.start = 0;
            this.end = 0;
        }

        public Node()
        {
            this.nodeName = "";
            this.parent = "";
            this.neighbours = new List<Node>();
            this.distances = new Dictionary<string, int>();
            this.occupiedNeighbours = new List<bool>();
            this.color = "white";
            this.distance = -1;
            this.start = 0;
            this.end = 0;
        }

        public void setNeighbour(Node neighbour,int position) {
            this.neighbours[position] = neighbour;
        }
        public void setStart(int start) { this.start = start; }
        public void setEnd(int stop) { this.end = stop; }
        public void setColor(string color) { this.color = color; }
        public void setDIstance(int distance) { this.distance = distance; }
        public void setNodeName(string nodeName) { this.nodeName = nodeName; }
        public void setParent(string parent) { this.parent = parent; }
        public void setOccupiedLink(int position, bool val) {
            this.occupiedNeighbours[position] = val;
        }
        public void addNeighbour(Node neighbour, int distance)
        {
            this.neighbours.Add(neighbour);
            this.distances.Add(neighbour.getNodeName(),distance);
        }

        public void addNeighbour(Node neighbour)
        {
            this.neighbours.Add(neighbour);
        }

        public void createOccupiedNeighbours()
        {
            for (int i = 0; i < this.neighbours.Count; i++)
            {
                this.occupiedNeighbours.Add(false);
            }
        }

        public int getStart() { return this.start; }
        public int getStop() { return this.end; }
        public int getDistance() { return this.distance; }
        public string getColor() { return this.color; } 
        public string getNodeName() { return this.nodeName; }
        public string getParent() { return this.parent; }
        public List<Node> getNeighbours() { return this.neighbours; }
        public int getDistances(string name) { return this.distances[name]; }
        public Dictionary<string, int> getDistances() { return this.distances; }

        public List<bool> getOccupiedNeighbours() { return this.occupiedNeighbours; }

        public bool getOccupiedNeighbour(Node node, int c) {
            int i = getNeighbours().IndexOf(node);// - getNeighbours().Count / 5; ;
            int x = getNeighbours().Count / 5;

            i = i - c*x;

            return this.occupiedNeighbours[i];

        }

        public object Shallowcopy()
        {
            return this.MemberwiseClone();
        } 

        public Node DeepCopy()
        {
            Node deepcopyNode = new Node(this.nodeName, this.parent,this.neighbours, this.distances,
           this.occupiedNeighbours, this.color, this.distance, this.start, this.end);

            return deepcopyNode;
        }

        internal void updateNode(Node endNode)
        {
            this.neighbours = endNode.getNeighbours();
            this.distances = endNode.getDistances();
            this.occupiedNeighbours = endNode.getOccupiedNeighbours();
            this.color = endNode.getColor();
            this.distance = endNode.getDistance();
            this.start = endNode.getStart();
            this.end = endNode.getStop();
            this.parent = endNode.getParent();
        }

        internal void printInfo(string a)
        {
            Console.WriteLine(
                $"nodeName = {this.nodeName}\n" +
                $"parent = {this.parent}\n" +
                $"neighbours = {this.neighbours}" +
                $"this.distances = distances " +
                $"this.occupiedNeighbours = occupiedNeighbours" +
                $"this.color = color" +
                $"this.distance = distance" +
                $"this.start = start" +
                $"this.end = end");
        }
    }

}
