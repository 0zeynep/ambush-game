using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyununSonu : MonoBehaviour
{
    public GameObject D�nenB�y�k�ember;
    public GameObject SpawnLokasyonu;

   public void OyunuBitir()
    {
        //DonenBuyukCember Scriptini devre d��� b�rakma
        D�nenB�y�k�ember.GetComponent<DonenBuyukCember>().enabled = false;
        SpawnLokasyonu.GetComponent<KucukCubukSpawner>().enabled = false;
    }
}
