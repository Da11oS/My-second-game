using UnityEngine;


public class Grid<TgridObject> 
{
    public int Width;
    public int Height;
    public float CellSize;

    private TgridObject[,] _array;
    public TextMesh[,] TextMeshArray;
    public Vector3 OriginPosition;
    public Grid(int width, int height, float cellSize, Vector3 originPosition, System.Func<Grid<TgridObject>, int, int, TgridObject> createGridObject)
    {
        OriginPosition = originPosition;
        Width = width;
        Height = height;
        CellSize = cellSize;
        _array = new TgridObject[Width, Height];
        TextMeshArray = new TextMesh[Width, Height];

        for (int x = 0; x < _array.GetLength(0); x++)
            for (int y = 0; y < _array.GetLength(1); y++)
                _array[x, y] = createGridObject(this, x, y); 

        for (int x = 0; x < _array.GetLength(0); x++)
        {
            for (int y = 0; y < _array.GetLength(1); y++)
            {

                TextMeshArray[x,y] = GetTextMesh(_array[x,y].ToString(), new Vector3 (CellSize, CellSize) * 0.5f + GetWorldPosition(new Vector3(x, y, 0)), 5, Color.white,TextAnchor.MiddleCenter, 1);
                Debug.DrawLine(GetWorldPosition( new Vector3(x, y, 0)), GetWorldPosition(new Vector3(x, y+1, 0)), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(new Vector3(x, y, 0)), GetWorldPosition(new Vector3(x+1, y, 0)), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(new Vector3(0, Height)), GetWorldPosition(new Vector3(Width, Height)), Color.white, 100f);
        Debug.DrawLine( GetWorldPosition(new Vector3(Width, 0)), GetWorldPosition(new Vector3(Width, Height)), Color.white, 100f);

    }
    public void SetTextMeshFontSize(int size)
    {
        foreach(TextMesh mesh in TextMeshArray)
        {
            mesh.fontSize = size;
        }
    }
    private Vector3 GetWorldPosition(Vector3 position )
    {
        return position * CellSize + OriginPosition;
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize + OriginPosition;
    }

    private void SetGridObjet(int x, int y, TgridObject value)
    {
        if(x >= 0 && y >=0 && x < _array.GetLength(0) && y < _array.GetLength(1))
        _array[x,y] = value;
        TextMeshArray[x, y].text = value.ToString();
    }
    private void SetGridObjet(Vector3 postion, TgridObject value)
    {
        GetXY(postion, out int x, out int y);
        SetGridObjet(x, y, value);
    }
    public TgridObject GetGridObjet(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _array.GetLength(0) && y < _array.GetLength(1))
            return _array[x, y];
        else return default(TgridObject);
    }
    public TgridObject GetGridObjet(Vector3 position)
    {
        GetXY(position, out int x, out int y);
        return GetGridObjet(x, y);
    }
    public int GetWidth()
    {
        return Width;
    }

    public int GetHeight()
    {
        return Height;
    }
    public float GetCellSize()
    {
        return CellSize;
    }
    public void GetXY(Vector3 position, out int x, out int y)
    {
            x = Mathf.FloorToInt((position.x - OriginPosition.x) / CellSize);
            y = Mathf.FloorToInt((position.y - OriginPosition.y) / CellSize);
    }
        private TextMesh GetTextMesh(string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_text", typeof(TextMesh));
        gameObject.transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.anchor = textAnchor;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}

