namespace TableManager.App.Models
{
    public class Table
    {
        public string Name { get; set; } = "Нова таблиця";
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public Cell[,] Cells { get; set; }

        public Table(int rows, int cols)
        {
            RowCount = rows;
            ColumnCount = cols;
            Cells = new Cell[rows, cols];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Cells[i, j] = new Cell(i, j);
                }
            }
        }

        public Cell? GetCell(int row, int col)
        {
            if (row >= 0 && row < RowCount && col >= 0 && col < ColumnCount)
                return Cells[row, col];
            return null;
        }
    }
}