using System.Xml.Xsl;

namespace Day13;

public struct OrderedMessage
{
    public List<Packet> packets;
    public int index2;
    public int index6;
    public OrderedMessage(string[] lines)
    {
        packets = new List<Packet>(lines.Length );
        foreach (var line in lines)
        {
            if (line.Length > 1 && line!="")
            {
                packets.Add(new Packet(line));
            }
        }

        index2 = packets.Count;
        Packet message2 = new Packet("[[2]]");
        packets.Add(message2);
        
        index6 = index2 + 1;
        Packet message6 = new Packet("[[6]]");
        packets.Add(message6);

        OrderPackets();

        index2 = packets.Select((pair,index) => (pair,index)).Where((pair,index)=>(pair.pair.IsMessage(message2))).Select(pair => pair.index).First();
        index6 = packets.Select((pair,index) => (pair,index)).Where((pair,index)=>(pair.pair.IsMessage(message6))).Select(pair => pair.index).First();
        Console.WriteLine((index2+1)*(1+index6));

    }

    private void OrderPackets()
    {
        packets=SortMerge(packets);
    }

    private List<Packet>  SortMerge(List<Packet> list)
    {
        if (list.Count <= 1)
        {
            return list;
        }

        else if (list.Count == 2)
        {
            if (!list[0].Inferior(list[1]))
            {
                var newList = new List<Packet>();
                newList.Add(list[1]);
                newList.Add(list[0]);
                return newList;
                (list[0], list[1]) = (list[1], list[0]);
                
            }
            return list;
        }
        else
        {
            int middle = list.Count / 2;
            var list1 = list.GetRange(0, middle);
            var list2 = list.GetRange(middle, list.Count-middle);
            list1 = SortMerge(list1);
            list2 = SortMerge(list2);
            return Merge(list1,list2);
        }
    }

    private List<Packet> Merge(List<Packet> list1, List<Packet> list2)
    {
        var list = new List<Packet>();
        var index1 = 0;
        var index2 = 0;
        while (index1 < list1.Count && index2 < list2.Count)
        {

            var message1 = list1[index1];
            var message2 = list2[index2];
            if (message1.Inferior(message2))
            {
                list.Add(message1);
                index1++;
            }
            else
            {
                list.Add(message2);
                index2++;
            }
        }

        for (; index1 < list1.Count; index1++)
        {
            list.Add(list1[index1]);
        }

        for (; index2 < list2.Count; index2++)
        {
            list.Add(list2[index2]);
        }

        return list;
    }
}