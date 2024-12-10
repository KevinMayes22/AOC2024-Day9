
namespace AdventOfCode9._1;

/// <summary>
/// Search Direction
/// </summary>
public enum Direction
    {
        Forward,Backward
    }
/// <summary>
/// Each block on the disk is called a sector.
/// </summary>
class Sector
{
    public int BlockId { get; set; }
    public bool Space { get; set; }
}


class Program
{
    static void Main()
    {
        var input = File.ReadAllText("input.txt");
        var isSpace = false;
        var blockId = 0;
        List<Sector> disk = [];

        // Build the disk (With spaces)
        foreach (var c in input)
        {

            var volume = Convert.ToInt32(c.ToString());

            if (isSpace)
            {
                for (var i = 0; i < volume; i++)
                {
                    disk.Add(new Sector { BlockId = 0, Space = true });
                }

            }
            else
            {
                for (var i = 0; i < volume; i++)
                {
                    disk.Add(new Sector { BlockId = blockId, Space = false });
                }

                blockId++;
            }
            isSpace = !isSpace;
        }
        
        //Defrag the disk as per the rules
        var startPosition = SearchFor(disk,0,disk.Count-1, Direction.Forward,true);
        var endPosition = SearchFor(disk,0,disk.Count-1, Direction.Backward,false);

        while ((startPosition > 0) && (endPosition > 0))
        {
            disk[startPosition].Space = disk[endPosition].Space;
            disk[startPosition].BlockId = disk[endPosition].BlockId;
            disk[endPosition].BlockId = -1;
            disk[endPosition].Space = true;
            startPosition = SearchFor(disk,startPosition,endPosition, Direction.Forward,true);
            endPosition = SearchFor(disk,startPosition,endPosition, Direction.Backward,false);
            
        }
        
        Int64 total = 0;

        for (var i = 0; i < disk.Count; i++)
        {
            if (disk[i].Space)
            {
                break;
            }
            total += disk[i].BlockId*i;
        }
        Console.WriteLine($"The total is {total}");
        return;
        foreach (var sector in disk)
        {
            if (sector.Space)
            {
                Console.Write(".");
            }
            else
            {
                Console.Write(sector.BlockId);
                
            }
        }
    }

    /// <summary>
    /// Search for, searches the disk (list object) in the direction specified
    /// for either a free space, or an occupied space specified by "space" parameter (true/false)
    /// Returns the index on the disk of the found sector
    /// If it fails to find a free space (or occupied space) it returns -1 
    /// </summary>
    /// <param name="disk"></param>
    /// <param name="startingPoint"></param>
    /// <param name="endingPoint"></param>
    /// <param name="direction"></param>
    /// <param name="space"></param>
    /// <returns>Index of the found sector or -1 if it fails</returns>
   static int SearchFor(List<Sector> disk, int startingPoint, int endingPoint, Direction direction, bool space)
    {
        if (direction == Direction.Forward)
        {
            for (int i = startingPoint; i < endingPoint; i++)
            {
                if ((disk[i].Space == space))
                {
                    return i;
                }
            }
        }

        if (direction == Direction.Backward)
        {
            for (int i = endingPoint; i > startingPoint; i--)
            {
                if ((disk[i].Space == space))
                {
                    return i;
                }
            }
        }
        return (-1);
    }
}

