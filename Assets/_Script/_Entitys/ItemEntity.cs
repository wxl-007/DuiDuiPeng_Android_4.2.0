using UnityEngine;
using System.Collections;
/// <summary>
/// 物体实体
/// </summary>
public class ItemEntity : MonoBehaviour
{
    /// <summary>
    /// 编号
    /// </summary>
    public int Id;
    /// <summary>
    /// 位置
    /// </summary>
    public Vector2 Pos;
    /// <summary>
    /// 标记 0：消除 1：不要消除 
    /// </summary>
    public int Tag;

}
