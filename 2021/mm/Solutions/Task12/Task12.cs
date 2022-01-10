using AdventOfCode2021.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public static class Task12
    {
        public static List<List<string>> doubledRoutes;

        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task12\\Input.txt");
            string line = string.Empty;
            List<(string, string)> lines = new List<(string, string)>();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var parts = line.Split("-");
                    lines.Add((parts[0], parts[1]));
                }
            }
            reader.Close();

            Dictionary<string, List<List<string>>> paths = new Dictionary<string, List<List<string>>>();
            paths.Add("start", new List<List<string>>());
            paths["start"].Add(new List<string>() { "start" });
            //var listStarts = lines.Where(l => l.Item1 == "start").ToList();
            //listStarts.AddRange(lines.Where(l => l.Item2 == "start").Select(l => (l.Item2, l.Item1)).ToList());

            Stack<string> nodesToCheck = new Stack<string>();
            nodesToCheck.Push("start");
            int i = 0;
            while (nodesToCheck.Count() > 0)
            {
                var node = nodesToCheck.Pop();

                // List all possible next nodes!
                var listWhereICanGetTo = lines.Where(l => l.Item1 == node).ToList();
                listWhereICanGetTo.AddRange(lines.Where(l => l.Item2 == node).Select(l => (l.Item2, l.Item1)).ToList());

                // To each node I can get to, I can get from each route I could get to kvPair.Key + that node
                // but only if that route does not contain given node in lower.
                foreach (var nextLink in listWhereICanGetTo)
                {
                    var nextNode = nextLink.Item2;
                    foreach (var existingRoute in paths[node])
                    {
                        // Check if we can even create the new route
                        if (!IsNodeUpper(nextNode) && existingRoute.Contains(nextNode))
                        {
                            // Going back through lowercase node, continue
                            continue;
                        }
                        else
                        {
                            // New route
                            // 1. Add all routes to node
                            var newRoute = new List<string>();
                            newRoute.AddRange(existingRoute);
                            newRoute.Add(nextNode);

                            if (!paths.ContainsKey(nextNode)) paths.Add(nextNode, new List<List<string>>());
                            bool pushToStack = false;
                            if (!RouteExistsForNode(paths[nextNode], newRoute))
                            {
                                paths[nextNode].Add(newRoute);
                                pushToStack = true;
                            }

                            // 2. Update stack
                            if (pushToStack && !nodesToCheck.Contains(nextNode) && nextNode != "end") nodesToCheck.Push(nextNode);
                        }
                    }

                }
            }

            return paths["end"].Count();
        }

        private static bool RouteExistsForNode(List<List<string>> lists, List<string> newRoute)
        {
            foreach (var item in lists)
            {
                if (item.Count != newRoute.Count) continue;
                bool isSame = true;
                for (int i = 0; i < item.Count; i++)
                {
                    if (newRoute[i] != item[i])
                    {
                        isSame = false;
                        break;
                    }
                }
                if (isSame) return true;
            }

            return false;
        }

        public static int Solve2()
        {
            doubledRoutes = new List<List<string>>();

            StreamReader reader = new StreamReader(Constants.PREPATH + "Task12\\Input.txt");
            string line = string.Empty;
            List<(string, string)> lines = new List<(string, string)>();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var parts = line.Split("-");
                    lines.Add((parts[0], parts[1]));
                }
            }
            reader.Close();

            Dictionary<string, List<List<string>>> paths = new Dictionary<string, List<List<string>>>();
            paths.Add("start", new List<List<string>>());
            paths["start"].Add(new List<string>() { "start" });
            //var listStarts = lines.Where(l => l.Item1 == "start").ToList();
            //listStarts.AddRange(lines.Where(l => l.Item2 == "start").Select(l => (l.Item2, l.Item1)).ToList());

            Stack<string> nodesToCheck = new Stack<string>();
            nodesToCheck.Push("start");
            int i = 0;
            while (nodesToCheck.Count() > 0)
            {
                var node = nodesToCheck.Pop();

                // List all possible next nodes!
                var listWhereICanGetTo = lines.Where(l => l.Item1 == node).ToList();
                listWhereICanGetTo.AddRange(lines.Where(l => l.Item2 == node).Select(l => (l.Item2, l.Item1)).ToList());

                // To each node I can get to, I can get from each route I could get to kvPair.Key + that node
                // but only if that route does not contain given node in lower.
                foreach (var nextLink in listWhereICanGetTo)
                {
                    var nextNode = nextLink.Item2;
                    foreach (var existingRoute in paths[node])
                    {
                        // Check if we can even create the new route.
                        // We can create the new route if the condition below holds and there are no doubles in there already.
                        if (nextNode == "start"
                            || (!IsNodeUpper(nextNode) && ContainsSmallDoubles(existingRoute) && existingRoute.Contains(nextNode))
                            )
                        {
                            // Going back through lowercase node, continue
                            continue;
                        }
                        else
                        {
                            // New route
                            // 1. Add all routes to node
                            var newRoute = new List<string>();
                            newRoute.AddRange(existingRoute);
                            newRoute.Add(nextNode);

                            if (!paths.ContainsKey(nextNode)) paths.Add(nextNode, new List<List<string>>());
                            bool pushToStack = false;
                            if (!RouteExistsForNode(paths[nextNode], newRoute))
                            {
                                paths[nextNode].Add(newRoute);
                                pushToStack = true;
                            }

                            // 2. Update stack
                            if (pushToStack && !nodesToCheck.Contains(nextNode) && nextNode != "end") nodesToCheck.Push(nextNode);
                        }
                    }

                }
            }

            return paths["end"].Count();
        }

        private static bool ContainsSmallDoubles(List<string> existingRoute)
        {
            if (doubledRoutes.Contains(existingRoute))
            {
                return true;
            }

            var route = new List<string>();
            route.AddRange(existingRoute);

            // Leave just lowers
            route = route.Where(node => !IsNodeUpper(node)).ToList();

            if (route == null || route.Count < 2) return false;

            int x = route.Count();
            route = route.Distinct().ToList();

            if (x != route.Count())
            {
                doubledRoutes.Add(existingRoute);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsNodeUpper(string node)
        {
            return node == node.ToUpper();
        }
    }
}
