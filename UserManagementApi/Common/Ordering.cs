using System.ComponentModel;

namespace Common
{
    public enum Direction
    {
        [Description("asc")]
        Asc,

        [Description("desc")]
        Desc
    }

    public class Ordering
    {
        public string Key { get; set; }

        public Direction Direction { get; set; }
    }
}
