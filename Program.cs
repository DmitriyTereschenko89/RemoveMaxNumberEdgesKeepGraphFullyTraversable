namespace RemoveMaxNumberEdgesKeepGraphFullyTraversable
{
    internal class Program
    {
        public class RemoveMaxNumberEdgesKeepGraphFullyTraversable
        {
            private class UnionFind
            {
                private readonly int[] representivity;
                private readonly int[] components;
                private int componentSize;

                private int Find(int node)
                {
                    if (representivity[node] != node)
                    {
                        representivity[node] = Find(representivity[node]);
                    }
                    return representivity[node];
                }

                public UnionFind(int size)
                {
                    componentSize = size;
                    representivity = new int[size + 1];
                    components = new int[size + 1];
                    for (int i = 0; i <= size; ++i)
                    {
                        representivity[i] = i;
                        components[i] = i;
                    }
                }

                public int Union(int node1, int node2)
                {
                    node1 = Find(node1);
                    node2 = Find(node2);

                    if (node1 == node2)
                    {
                        return 0;
                    }

                    if (components[node1] > components[node2])
                    {
                        representivity[node2] = node1;
                        components[node1] += components[node2];
                    }
                    else
                    {
                        representivity[node1] = node2;
                        components[node2] += components[node1];
                    }
                    --componentSize;
                    return 1;
                }

                public bool AreConnected()
                {
                    return componentSize == 1;
                }
            }
            public int MaxNumEdgesToRemove(int n, int[][] edges)
            {
                UnionFind Alice = new(n);
                UnionFind Bob = new(n);
                int edgeConnected = 0;
                foreach (int[] edge in edges)
                {
                    if (edge[0] == 3)
                    {
                        edgeConnected += Alice.Union(edge[1], edge[2]) | Bob.Union(edge[1], edge[2]);
                    }
                }

                foreach (int[] edge in edges)
                {
                    if (edge[0] == 1)
                    {
                        edgeConnected += Alice.Union(edge[1], edge[2]);
                    }
                    else if (edge[0] == 2)
                    {
                        edgeConnected += Bob.Union(edge[1], edge[2]);
                    }
                }

                if (Alice.AreConnected() && Bob.AreConnected())
                {
                    return edges.Length - edgeConnected;
                }
                return -1;
            }
        }
        
        static void Main(string[] args)
        {
            RemoveMaxNumberEdgesKeepGraphFullyTraversable removeMaxNumberEdgesKeepGraphFullyTraversable = new();
            Console.WriteLine(removeMaxNumberEdgesKeepGraphFullyTraversable.MaxNumEdgesToRemove(4, new int[][]
            {
                new int[] { 3, 1, 2 }, new int[] { 3, 2, 3 }, new int[] { 1, 1, 3 },
                new int[] { 1, 2, 4 }, new int[] { 1, 1, 2 }, new int[] { 2, 3, 4 }
            }));
            Console.WriteLine(removeMaxNumberEdgesKeepGraphFullyTraversable.MaxNumEdgesToRemove(4, new int[][]
            {
                new int[] { 3, 1, 2 }, new int[] { 3, 2, 3 },
                new int[] { 1, 1, 4 }, new int[] { 2, 1, 4 }
            }));
            Console.WriteLine(removeMaxNumberEdgesKeepGraphFullyTraversable.MaxNumEdgesToRemove(4, new int[][]
            {
                new int[] { 3, 2, 3 }, new int[] { 1, 1, 2 }, new int[] {2, 3, 4 }
            }));
        }
    }
}