using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum FurniturePlacementType
{
    Floor,
    Wall,
    Ceiling,
    None,
}

[CreateAssetMenu(fileName ="NewFurniture.asset", menuName="Configs/Furniture")]
public class FurnitureConfig : ScriptableObject
{
    public FurniturePlacementType furniturePlacementType;
    public GameObject prefab;
    public int price;
    public string description;
    public string displayName;
    public Image thumbnail;
    public Vector2 minPlaneSize;

    public bool FitsOnPlane(ARPlane plane)
    {
        return  (plane.size.x < minPlaneSize.x && plane.size.y < minPlaneSize.y) ||
                (plane.size.y < minPlaneSize.x && plane.size.x < minPlaneSize.y);
    }

    public bool IsCorrectPlaneType(ARPlane plane)
    {
        switch (plane.alignment)
        {
            case PlaneAlignment.HorizontalUp:
                return furniturePlacementType == FurniturePlacementType.Ceiling;
            case PlaneAlignment.HorizontalDown:
                return furniturePlacementType == FurniturePlacementType.Floor;
            case PlaneAlignment.Vertical:
                return furniturePlacementType == FurniturePlacementType.Wall;
            case PlaneAlignment.NotAxisAligned:
            case PlaneAlignment.None:
            default:
                return furniturePlacementType == FurniturePlacementType.None;
        }
    }
}
