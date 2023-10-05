using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Renderer mesh;
    public Material transparent;
    [SerializeField] Material[] myMaterials;

    private void Start()
    {
        //GetColorBack();
        mesh = GetComponent<Renderer>();
        myMaterials = mesh.materials;
    }
    public void Faded()
    {
        if (GameManager.IsState(GameState.Gameplay) && mesh != null)
            for (int i = 0; i < myMaterials.Length; i++)
            {
                mesh.material = transparent;
            }

    }
    public void GetColorBack()
    {
        if (mesh != null)
        {
            for (int i = 0; i < myMaterials.Length; i++)
            {
                mesh.materials[i] = myMaterials[i];
            }
            mesh.material = myMaterials[0];
            
        }

    }
}
