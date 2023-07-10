namespace Board
{
    class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        override public string ToString(){
            return Line +
            ", "
            + Column;
        }

    }
}