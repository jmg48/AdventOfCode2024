namespace AdventOfCode2024;

public record Coord(int X, int Y)
{
    public Coord Move(Dir dir, int dist = 1) =>
        dir switch
        {
            Dir.N => new Coord(this.X - dist, this.Y),
            Dir.S => new Coord(this.X + dist, this.Y),
            Dir.E => new Coord(this.X, this.Y + dist),
            Dir.W => new Coord(this.X, this.Y - dist),
        };

    public bool IsInBoundsOf(IList<string> input)
        => this.X >= 0 && this.Y >= 0 && this.X < input.Count && this.Y < input[0].Length;

    public bool IsInBoundsOf<T>(IList<List<T>> input)
        => this.X >= 0 && this.Y >= 0 && this.X < input.Count && this.Y < input[0].Count;
}

public record LongCoord(long X, long Y);