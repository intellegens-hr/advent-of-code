using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task7
{
    public class Task7
    {
        public static int FirstPart (string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            int count = 0;

            // Task 1
            //Dictionary<string, Node> nodes = GetTreeByChild(lines);
            //List<string> countedColors = new List<string>();
            //count = CountForChildNode("shiny gold", nodes,countedColors) - 1;    // The minus one is to remove yourself

            // Task 2
            Dictionary<string, Node> nodes = GetTreeByParent(lines);
            count = SumForParentNode("shiny gold", nodes) - 1;                      // The minus one is to remove yourself


            return count;
        }

        public static int SumForParentNode (string color, Dictionary<string, Node> nodes)
        {
            int sum = 1;
            if (nodes[color].Children == null || nodes[color].Children.Count == 0)
            {
                return sum;
            } else
            {
                foreach (var child in nodes[color].Children)
                {
                    sum += child.Value.Amount * (SumForParentNode(child.Key, nodes));
                }
            }
            return sum;
        }

        public static int CountForChildNode (string color, Dictionary<string, Node> nodes, List<string> countedColors)
        {
            int sum = 0;
            if (countedColors.Contains(color))
            {
                sum = 0;
            } else
            {
                countedColors.Add(color);
                sum = 1;
            }


            if (!nodes.ContainsKey(color) || nodes[color].Parents.Count == 0)
            {
                
                return sum;
            }
            else
            {
                foreach (var nextColor in nodes[color].Parents)
                {
                    sum += CountForChildNode(nextColor, nodes, countedColors);
                }
                return sum;
            }
        }

        private static Dictionary<string, Node> GetTreeByParent(List<string> lines)
        {
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            foreach (var line in lines)
            {
                var parts = line.Split("bags contain").Select(p => p.Trim()).ToList();
                Node node;
                if (!nodes.ContainsKey(parts[0]))
                {
                    // Add new node
                    node = new Node()
                    {
                        Color = parts[0],
                        Children = new Dictionary<string, Node.ChildColor>()
                    };
                    nodes.Add(node.Color, node);
                }
                else
                {
                    // Use existing node
                    node = nodes[parts[0]];
                }

                // Add new colors to node
                var (amounts,colors) = GetContainingColorsAndAmounts(parts[1]);
                int i = 0;
                foreach (var color in colors)
                {
                    if (!node.Children.ContainsKey(color))
                    {
                        node.Children.Add(color, new Node.ChildColor()
                        {
                            Color = color,
                            Amount = amounts[i]
                        });
                    }
                    i++;
                }
            }

            return nodes;
        }

        private static Dictionary<string, Node> GetTreeByChild(List<string> lines)
        {
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach (var line in lines)
            {
                var parts = line.Split("bags contain").Select(p => p.Trim()).ToList();
                Node node;

                

                var (amounts, colors) = GetContainingColorsAndAmounts(parts[1]);
                foreach (var color in colors)
                {
                    if (nodes.ContainsKey(color))
                    {
                        node = nodes[color];
                    } else
                    {
                        node = new Node()
                        {
                            Color = color,
                            Parents = new List<string>()
                        };
                        nodes.Add(color, node);
                    }

                    if (!node.Parents.Contains(parts[0]))
                    {
                        node.Parents.Add(parts[0]);
                    }
                }
            }

            return nodes;
        }

        public static (List<int>,List<string>) GetContainingColorsAndAmounts(string part)
        {
            List<string> myList = new List<string>();
            List<int> amounts = new List<int>();
            part = part.Substring(0, part.Length - 1);      // Remove . at the end
            var sections = part.Split(",").Select(p => p.Trim()).ToList();

            if (sections.Count() == 1 && sections[0] == "no other bags")
            {
                return (amounts, myList);
            } else
            {
                foreach (var section in sections)
                {
                    // remove " bags" at the end if plural or " bag" if singular
                    string color = string.Empty;
                    if (section.EndsWith(" bags"))
                    {
                        color = section.Substring(0, section.Length - 5);
                    } else if (section.EndsWith(" bag"))
                    {
                        color = section.Substring(0, section.Length - 4);
                    }
                    amounts.Add(Convert.ToInt32(color.Substring(0, color.IndexOf(" "))));

                    // remove up until first " " at the start
                    color = color.Substring(color.IndexOf(" ") + 1);
                    myList.Add(color);
                }
            }

            return (amounts, myList);
        }        
    }
}
