using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<Table> tables;
    public List<BotController> botControllers;

    public Chair GetRandomPointSitting()
{
    Table table;
    Chair chair;
    bool found = false;
    do
    {
        int indexTable = Random.Range(0, tables.Count);
        table = tables[indexTable];

        int indexChair = Random.Range(0, table.chairs.Count);
        chair = table.chairs[indexChair];

        if (chair.chairZone.isChairFound)
        {
            chair.chairZone.isChairFound = false;
            found = true;
        }

    } while (!found);

    return chair;
}

}
