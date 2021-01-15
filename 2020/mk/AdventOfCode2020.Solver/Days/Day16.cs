using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Solver.Models;

namespace AdventOfCode2020.Solver.Days
{
    public class Day16 : PuzzleDay<long>
    {
        public override int Day => 16;

        public override long First()
        {
            var input = GetInputText().Split("\r\n\r\n");

            var definitions = GetTicketDefinitions(input[0]);
            //var myTicket = GetMyTicket(input[1]);
            var nearbyTickets = GetNearbyTickets(input[2]);

            return nearbyTickets.Sum(t => t.GetErrorRate(definitions));
        }

        List<Ticket> GetNearbyTickets(string input)
        {
            return input.Split("\r\n").Skip(1).Select(a => new Ticket(a)).ToList();
        }

        Ticket GetMyTicket(string input)
        {
            return new Ticket(input.Split("\r\n")[1]);
        }

        List<TicketDefinition> GetTicketDefinitions(string input)
        {
            return input.Split("\r\n").Select(line => new TicketDefinition
            {
                Name = line.Split(": ")[0],
                From1 = int.Parse(line.Split(": ")[1].Split(" or ")[0].Split("-")[0]),
                To1 = int.Parse(line.Split(": ")[1].Split(" or ")[0].Split("-")[1]),
                From2 = int.Parse(line.Split(": ")[1].Split(" or ")[1].Split("-")[0]),
                To2 = int.Parse(line.Split(": ")[1].Split(" or ")[1].Split("-")[1])
            }).ToList();
        }

        public override long Second()
        {
            var input = GetInputText().Split("\r\n\r\n");

            var definitions = GetTicketDefinitions(input[0]);
            var myTicket = GetMyTicket(input[1]);
            var nearbyValidTickets = GetNearbyTickets(input[2]).Where(t => t.Valid(definitions)).Append(myTicket).ToList();

            var mapFieldToPossibleIndexes = new Dictionary<string, List<int>>();
            var mapFieldToIndex = new Dictionary<string, int>();

            foreach (var def in definitions)
            {
                mapFieldToPossibleIndexes[def.Name] = new List<int>();
                for (int i = 0; i < nearbyValidTickets[0].Values.Count; i++)
                {
                    if (nearbyValidTickets.All(t => def.Validate(t.Values[i])))
                    {
                        mapFieldToPossibleIndexes[def.Name].Add(i);
                    }
                }
            }

            var ordered = mapFieldToPossibleIndexes.OrderBy(a => a.Value.Count).ToList();

            for (int i = 0; i < ordered.Count(); i++)
            {
                if (ordered[i].Value.Count == 1)
                {
                    var val = ordered[i].Value[0];

                    mapFieldToIndex[ordered[i].Key] = val;
                    foreach (var item in ordered)
                    {
                        item.Value.Remove(val);
                    }
                }
            }

            return mapFieldToIndex.Where(a => a.Key.StartsWith("departure")).Select(a => (long)myTicket.Values[a.Value]).Aggregate((l, r) => l * r);
        }
    }
}
