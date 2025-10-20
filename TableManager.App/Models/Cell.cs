namespace TableManager.App.Models
{
    public class Cell
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string Expression { get; set; } = "";
        public string Result { get; set; } = "";

        public Cell(int row, int col)
        {
            RowIndex = row;
            ColumnIndex = col;
        }
    }
}