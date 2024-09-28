using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExtraCoinBlock : MonoBehaviour
{
    int times;
    public Tilemap tileMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            times++;
            if(times >= 4)
            {
                // Pega o primeiro ponto de contato da colisão
                ContactPoint2D contactPoint = collision.GetContact(0);

                // Converte a posição do mundo para tile cell
                Vector3Int cellPosition = tileMap.WorldToCell(contactPoint.point);

                // Pega o tile naquela posição
                TileBase collidedTile = tileMap.GetTile(cellPosition);
                
                tileMap.SetTile(cellPosition, null);
                times = 0;
            }
        }
    }
}
