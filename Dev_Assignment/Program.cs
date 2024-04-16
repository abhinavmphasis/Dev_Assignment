using Dev_Assignment.Models;

public class Program
{
    public static short Id { get; private set; }

    public static void Main(string[] args)
    {
        // Test data
        var items = new List<Item>
        {
            new Item { Id = 1, LotNumber = 300, SequenceNumber = 1, BoxId = 1 },
            new Item { Id = 2, LotNumber = 300, SequenceNumber = 2, BoxId = 1 },
            new Item { Id = 3, LotNumber = 300, SequenceNumber = 3, BoxId = 1 },
            new Item { Id = 4, LotNumber = 300, SequenceNumber = 4, BoxId = 1 },
            new Item { Id = 5, LotNumber = 300, SequenceNumber = 5, BoxId = 1 },
            new Item { Id = 6, LotNumber = 307, SequenceNumber = 1, BoxId = 1 },
            new Item { Id = 7, LotNumber = 307, SequenceNumber = 2, BoxId = 1 },
            new Item { Id = 8, LotNumber = 307, SequenceNumber = 5, BoxId = 1 },
            new Item { Id = 9, LotNumber = 307, SequenceNumber = 6, BoxId = 1 },
            new Item { Id = 10, LotNumber = 307, SequenceNumber = 7, BoxId = 1 },
            new Item { Id = 11, LotNumber = 307, SequenceNumber = 9, BoxId = 1 },
            new Item { Id = 12, LotNumber = 307, SequenceNumber = 10, BoxId = 1 },
            new Item { Id = 13, LotNumber = 307, SequenceNumber = 11, BoxId = 1 },
            new Item { Id = 14, LotNumber = 341, SequenceNumber = 1, BoxId = 1 },
            new Item { Id = 15, LotNumber = 341, SequenceNumber = 5, BoxId = 1 },
            new Item { Id = 16, LotNumber = 341, SequenceNumber = 6, BoxId = 2 },
            new Item { Id = 17, LotNumber = 341, SequenceNumber = 7, BoxId = 2 },
            new Item { Id = 18, LotNumber = 341, SequenceNumber = 8, BoxId = 2 },
            new Item { Id = 19, LotNumber = 341, SequenceNumber = 20, BoxId = 2 },
            new Item { Id = 20, LotNumber = 341, SequenceNumber = 21, BoxId = 2 },
            new Item { Id = 21, LotNumber = 341, SequenceNumber = 22, BoxId = 2 },
            new Item { Id = 22, LotNumber = 341, SequenceNumber = 23, BoxId = 2 },
            new Item { Id = 23, LotNumber = 341, SequenceNumber = 24, BoxId = 2 },
            new Item { Id = 24, LotNumber = 341, SequenceNumber = 25, BoxId = 3 },
            new Item { Id = 25, LotNumber = 388, SequenceNumber = 1, BoxId = 3 },
            new Item { Id = 26, LotNumber = 388, SequenceNumber = 2, BoxId = 3 },
            new Item { Id = 27, LotNumber = 388, SequenceNumber = 3, BoxId = 3 },
            new Item { Id = 28, LotNumber = 388, SequenceNumber = 4, BoxId = 3 },
            new Item { Id = 29, LotNumber = 388, SequenceNumber = 5, BoxId = 3 },
            new Item { Id = 30, LotNumber = 388, SequenceNumber = 6, BoxId = 3 },
        };
        Console.WriteLine("Enter Box Id");
        Id = short.Parse(Console.ReadLine());
        var result = GetBoxItemsInfo(Id, items);
        Console.WriteLine(result);
        Console.ReadLine();
    }

    public static string GetBoxItemsInfo(short boxId, List<Item> items)
    {
        var itemsInBox = items.Where(i => i.BoxId == boxId).OrderBy(i => i.LotNumber).ThenBy(i => i.SequenceNumber).ToList();
        var resultItems = "";

        if (itemsInBox.Count == 0)
        {
            return "No available items exist in the box.";
        }

        var currentLot = itemsInBox[0].LotNumber;
        var sequences = new List<List<int>>();
        sequences.Add(new List<int> { itemsInBox[0].SequenceNumber });

        for (int i = 1; i < itemsInBox.Count; i++)
        {
            if (itemsInBox[i].LotNumber == currentLot)
            {
                if (itemsInBox[i].SequenceNumber == itemsInBox[i - 1].SequenceNumber + 1)
                {
                    sequences.Last().Add(itemsInBox[i].SequenceNumber);
                }
                else
                {
                    sequences.Add(new List<int> { itemsInBox[i].SequenceNumber });
                }
            }
            else
            {
                resultItems += $"{currentLot} ({string.Join(",", sequences.Select(s => s.Count == 1 ? s[0].ToString() : $"{s.First()}-{s.Last()}"))}), ";
                currentLot = itemsInBox[i].LotNumber;
                sequences.Clear();
                sequences.Add(new List<int> { itemsInBox[i].SequenceNumber });
            }
        }

        resultItems += $"{currentLot} ({string.Join(",", sequences.Select(s => s.Count == 1 ? s[0].ToString() : $"{s.First()}-{s.Last()}"))})";

        return resultItems;
    }
}