using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Vertical,Horizontal
}
public class MapCreate : MonoBehaviour
{

}
public class TreeNode
{
    public Vector2Int bottomLeft, topRight;

    public TreeNode parentNode;
    public TreeNode leftNode, rightNode;

    public bool isDivided;

    public int depth;

    public Vector3Int roomBL, roomTR;

    private Direction direction;

    public TreeNode(Vector2Int bottomLeft, Vector2Int topRight)
    {
        this.bottomLeft = bottomLeft;
        this.topRight = topRight;
    }
    public void SetDirection()
    {
        if (topRight.x - bottomLeft.x < topRight.y - bottomLeft.y)
            direction = Direction.Vertical;

        else if (topRight.x - bottomLeft.x < topRight.y - bottomLeft.y)
            direction = Direction.Horizontal;
        else
        {
            int temp = Random.Range(1, 3);
            if (temp == 1) direction = Direction.Vertical;
            else direction = Direction.Horizontal;
        }
    }
    public bool GetDirection()
    {
        if (direction == Direction.Vertical)
            return true;
        else
            return false;
    }
    public bool DivideNode(int ratio, int minSize)
    {
        float temp;
        Vector2Int divideLine1, divideLine2;
        if (direction == Direction.Vertical)
        {
            temp = (topRight.x - bottomLeft.x);
            temp = temp * ratio / 100;
            int width = Mathf.RoundToInt(temp);
            if (width < minSize || topRight.x - bottomLeft.x + width < minSize)
                return false;
            divideLine1 = new Vector2Int(bottomLeft.x + width, topRight.y);
            divideLine2 = new Vector2Int(bottomLeft.x + width, bottomLeft.y);
        }
        else
        {
            temp = (topRight.y - bottomLeft.y);
            temp = temp * ratio / 100;
            int height = Mathf.RoundToInt(temp);
            if (height < minSize || topRight.y - bottomLeft.y - height < minSize)
                return false;
            divideLine1 = new Vector2Int(topRight.x, bottomLeft.y + height);
            divideLine2 = new Vector2Int(bottomLeft.x, bottomLeft.y + height);
        }
        leftNode = new TreeNode(bottomLeft, divideLine1);
        rightNode = new TreeNode(divideLine2, topRight);
        leftNode.parentNode = rightNode.parentNode = this;
        isDivided = true;
        return true;
    }
    public void CreateRoom()
    {
        int distanceFrom = 2;
        if (!isDivided)
        {
            roomBL = new Vector3Int(bottomLeft.x + distanceFrom, bottomLeft.y + distanceFrom, 0);
            roomTR = new Vector3Int(topRight.x - distanceFrom, topRight.y - distanceFrom, 0);
        }
    }
}
