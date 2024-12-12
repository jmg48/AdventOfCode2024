namespace AdventOfCode2024;

public enum Dir
{
    N,
    S,
    E,
    W
};

public static class DirExtensions
{
    public static Dir TurnLeft(this Dir dir) =>
        dir switch
        {
            Dir.N => Dir.W,
            Dir.W => Dir.S,
            Dir.S => Dir.E,
            Dir.E => Dir.N
        };

    public static Dir TurnRight(this Dir dir) =>
        dir switch
        {
            Dir.N => Dir.E,
            Dir.E => Dir.S,
            Dir.S => Dir.W,
            Dir.W => Dir.N
        };

    public static Dir TurnBack(this Dir dir) =>
        dir switch
        {
            Dir.N => Dir.S,
            Dir.E => Dir.W,
            Dir.S => Dir.N,
            Dir.W => Dir.E
        };
}