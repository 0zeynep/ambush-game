using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urun : MonoBehaviour
{
    float x, y;
    bool DussunMu = true;
    GameObject SecimAraci;
    public static Urun ilkSecilenUrun;
    public static Urun ikinciSecilenUrun;
    public Vector3 HedefKonum;
    public bool yerDegisim = false;
    public Urun[,] tempUrunKoordinat;
    public List<Urun> silinecekXUrunler;
    public List<Urun> silinecekXUrunler1;
    public List<Urun> silinecekYUrunler;
    public List<Urun> silinecekYUrunler1;
    public List<Urun> urun_y_ekseni;
    public string renk;

    void Start()
    {
        tempUrunKoordinat = new Urun[4, 7];
        silinecekXUrunler = new List<Urun>();
        silinecekXUrunler1 = new List<Urun>();
        silinecekYUrunler = new List<Urun>();
        silinecekYUrunler1 = new List<Urun>();
        SecimAraci = GameObject.FindGameObjectWithTag("Secim");
    }

    
    void Update()
    {
        if (DussunMu)
        {
            if (transform.position.y - y < 0.05f)
            {
                DussunMu = false;
                transform.position = new Vector3(x, y, 0);
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, 0), Time.deltaTime * 3f);
        }

        if (yerDegisim)
        {
            YerDegis();
        }
    }

    public void YeniKonum(float _x, float _y)
    {
        x = _x;
        y = _y;
 
    }

    void OnMouseDown()
    {
        SecimAraci.transform.position = transform.position;
        UrunDurumu();
    }

    void UrunDurumu()
    {
        if (ilkSecilenUrun == null)
        {
            ilkSecilenUrun = this;
        }
        else
        {
            ikinciSecilenUrun = this;
            if (ilkSecilenUrun != ikinciSecilenUrun)
            {
                float fark_x = ilkSecilenUrun.x - ikinciSecilenUrun.x;
                float fark_y = ilkSecilenUrun.y - ikinciSecilenUrun.y;

                if (fark_x + fark_y >= -4.29)
                {
                    //KONTROL SA�LANMADI D�KKAT!!
                    ilkSecilenUrun.HedefKonum = ikinciSecilenUrun.transform.position; // yer de�i�tirsinler.
                    ikinciSecilenUrun.HedefKonum = ilkSecilenUrun.transform.position;
                    ilkSecilenUrun.yerDegisim = true;
                    ikinciSecilenUrun.yerDegisim = true;

                    DegiskenDuzenle();
                    ilkSecilenUrun = null;
                    // ilkSecilenUrun.X_ekseni_kontrol();
                }
                else
                {
                    ilkSecilenUrun = ikinciSecilenUrun;
                }
            }
                ikinciSecilenUrun = null;
        }
    }

    void DegiskenDuzenle()
    {
        int ilkUrunKoordinatX = 0, ilkUrunKoordinatY = 0, ikinciUrunKoordinatX = 0, ikinciUrunKoordinatY = 0;

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                if (SekerOlusturma.o_urunler[i, j].x == ilkSecilenUrun.x && SekerOlusturma.o_urunler[i, j].y == ilkSecilenUrun.y)
                {
                    ilkUrunKoordinatX = i;
                    ilkUrunKoordinatY = j;
                }

                if (SekerOlusturma.o_urunler[i, j].x == ikinciSecilenUrun.x && SekerOlusturma.o_urunler[i, j].y == ikinciSecilenUrun.y)
                {
                    ikinciUrunKoordinatX = i;
                    ikinciUrunKoordinatY = j;
                }
            }
        }

        tempUrunKoordinat[0, 0] = SekerOlusturma.o_urunler[ilkUrunKoordinatX, ilkUrunKoordinatY];
        SekerOlusturma.o_urunler[ilkUrunKoordinatX, ilkUrunKoordinatY] = SekerOlusturma.o_urunler[ikinciUrunKoordinatX, ikinciUrunKoordinatY];
        SekerOlusturma.o_urunler[ikinciUrunKoordinatX, ikinciUrunKoordinatY] = tempUrunKoordinat[0, 0];

        //ilkSecilenUrun.yerDegisim = false;
        //ikinciSecilenUrun.yerDegisim = false;

        int ilkSecilenHedefKonumX = 0, ilkSecilenHedefKonumY = 0, ikinciSecilenHedefKonumX = 0, ikinciSecilenHedefKonumY = 0, satirXEslesenler = 1, satirYEslesenler = 1;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (SekerOlusturma.o_urunler[i, j].x == ilkSecilenUrun.x && SekerOlusturma.o_urunler[i, j].y == ilkSecilenUrun.y)
                {
                    ilkSecilenHedefKonumX = i;
                    ilkSecilenHedefKonumY = j;
                }
                if (SekerOlusturma.o_urunler[i, j].x == ikinciSecilenUrun.x && SekerOlusturma.o_urunler[i, j].y == ikinciSecilenUrun.y)
                {
                    ikinciSecilenHedefKonumX = i;
                    ikinciSecilenHedefKonumY = j;
                }
            }
        }

        //Debug.Log(ilkSecilenHedefKonumX + " " + ilkSecilenHedefKonumY);
        //Debug.Log(ikinciSecilenHedefKonumX + " " + ikinciSecilenHedefKonumY);

        silinecekXUrunler.Add(SekerOlusturma.o_urunler[ilkSecilenHedefKonumX, ilkSecilenHedefKonumY]);
        silinecekXUrunler1.Add(SekerOlusturma.o_urunler[ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumY]);
        silinecekYUrunler.Add(SekerOlusturma.o_urunler[ilkSecilenHedefKonumX, ilkSecilenHedefKonumY]);
        silinecekYUrunler1.Add(SekerOlusturma.o_urunler[ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumY]);

        int ilkSecilenHedefKonumXTemp = ilkSecilenHedefKonumX, ilkSecilenHedefKonumYTemp = ilkSecilenHedefKonumY, ikinciSecilenHedefKonumXTemp = ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumYTemp = ikinciSecilenHedefKonumY;

        for (int y = ilkSecilenHedefKonumY; y < 7; y++)
        {
            if (y == 6)
            {
                continue;
            }

            if (SekerOlusturma.o_urunler[ilkSecilenHedefKonumX, ilkSecilenHedefKonumYTemp].renk == SekerOlusturma.o_urunler[ilkSecilenHedefKonumX, ilkSecilenHedefKonumYTemp + 1].renk)
            {
                satirXEslesenler++;
                silinecekXUrunler.Add(SekerOlusturma.o_urunler[ilkSecilenHedefKonumX, ilkSecilenHedefKonumYTemp + 1]);
            }
            else
            {
                break;
            }

            ilkSecilenHedefKonumYTemp++;
        }

        ilkSecilenHedefKonumYTemp = ilkSecilenHedefKonumY;

        for (int y = ilkSecilenHedefKonumY; y > 0; y--)
        {
            if (y == 0)
            {
                continue;
            }

            if (SekerOlusturma.o_urunler[ilkSecilenHedefKonumX, ilkSecilenHedefKonumYTemp].renk == SekerOlusturma.o_urunler[ilkSecilenHedefKonumX, ilkSecilenHedefKonumYTemp - 1].renk)
            {
                satirXEslesenler++;
                silinecekXUrunler.Add(SekerOlusturma.o_urunler[ilkSecilenHedefKonumX, ilkSecilenHedefKonumYTemp - 1]);
            }
            else
            {
                break;
            }

            ilkSecilenHedefKonumYTemp--;
        }

        if (satirXEslesenler > 2)
        {
            satirXEslesenler = 1;
        }
        else
        {
            satirXEslesenler = 1;
            silinecekXUrunler.Clear();
        }

        for (int y = ikinciSecilenHedefKonumY; y < 7; y++)
        {
            if (y == 6)
            {
                continue;
            }

            if (SekerOlusturma.o_urunler[ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumYTemp].renk == SekerOlusturma.o_urunler[ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumYTemp + 1].renk)
            {
                satirXEslesenler++;
                silinecekXUrunler1.Add(SekerOlusturma.o_urunler[ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumYTemp + 1]);
            }
            else
            {
                break;
            }

            ikinciSecilenHedefKonumYTemp++;
        }

        ikinciSecilenHedefKonumYTemp = ikinciSecilenHedefKonumY;

        for (int y = ikinciSecilenHedefKonumY; y > 0; y--)
        {
            if (y == 0)
            {
                continue;
            }

            if (SekerOlusturma.o_urunler[ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumYTemp].renk == SekerOlusturma.o_urunler[ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumYTemp - 1].renk)
            {
                satirXEslesenler++;
                silinecekXUrunler1.Add(SekerOlusturma.o_urunler[ikinciSecilenHedefKonumX, ikinciSecilenHedefKonumYTemp - 1]);
            }
            else
            {
                break;
            }

            ikinciSecilenHedefKonumYTemp--;
        }

        if (satirXEslesenler > 2)
        {
            satirXEslesenler = 1;
        }
        else
        {
            satirXEslesenler = 1;
            silinecekXUrunler1.Clear();
        }


        for (int x = ilkSecilenHedefKonumX; x < 4; x++)
        {
            if (x == 3)
            {
                continue;
            }

            if (SekerOlusturma.o_urunler[ilkSecilenHedefKonumXTemp, ilkSecilenHedefKonumY].renk == SekerOlusturma.o_urunler[ilkSecilenHedefKonumXTemp + 1, ilkSecilenHedefKonumY].renk)
            {
                satirYEslesenler++;
                silinecekYUrunler.Add(SekerOlusturma.o_urunler[ilkSecilenHedefKonumXTemp + 1, ilkSecilenHedefKonumY]);
            }
            else
            {
                break;
            }

            ilkSecilenHedefKonumXTemp++;
        }

        ilkSecilenHedefKonumXTemp = ilkSecilenHedefKonumX;

        for (int x = ilkSecilenHedefKonumX; x < 0; x--)
        {
            if (x == 0)
            {
                continue;
            }

            if (SekerOlusturma.o_urunler[ilkSecilenHedefKonumXTemp, ilkSecilenHedefKonumY].renk == SekerOlusturma.o_urunler[ilkSecilenHedefKonumXTemp - 1, ilkSecilenHedefKonumY].renk)
            {
                satirYEslesenler++;
                silinecekYUrunler.Add(SekerOlusturma.o_urunler[ilkSecilenHedefKonumXTemp - 1, ilkSecilenHedefKonumY]);
            }
            else
            {
                break;
            }

            ilkSecilenHedefKonumXTemp--;
        }

        if (satirYEslesenler > 2)
        {
            satirYEslesenler = 1;
        }
        else
        {
            satirYEslesenler = 1;
            silinecekYUrunler.Clear();
        }

        for (int x = ikinciSecilenHedefKonumX; x < 4; x++)
        {
            if (x == 3)
            {
                continue;
            }

            if (SekerOlusturma.o_urunler[ikinciSecilenHedefKonumXTemp, ikinciSecilenHedefKonumY].renk == SekerOlusturma.o_urunler[ikinciSecilenHedefKonumXTemp + 1, ikinciSecilenHedefKonumY].renk)
            {
                satirYEslesenler++;
                silinecekYUrunler.Add(SekerOlusturma.o_urunler[ikinciSecilenHedefKonumXTemp + 1, ikinciSecilenHedefKonumY]);
            }
            else
            {
                break;
            }

            ikinciSecilenHedefKonumXTemp++;
        }

        ikinciSecilenHedefKonumXTemp = ikinciSecilenHedefKonumX;

        for (int x = ikinciSecilenHedefKonumX; x < 0; x--)
        {
            if (x == 0)
            {
                continue;
            }

            if (SekerOlusturma.o_urunler[ikinciSecilenHedefKonumXTemp, ikinciSecilenHedefKonumY].renk == SekerOlusturma.o_urunler[ikinciSecilenHedefKonumXTemp - 1, ikinciSecilenHedefKonumY].renk)
            {
                satirYEslesenler++;
                silinecekYUrunler.Add(SekerOlusturma.o_urunler[ikinciSecilenHedefKonumXTemp - 1, ikinciSecilenHedefKonumY]);
            }
            else
            {
                break;
            }

            ikinciSecilenHedefKonumXTemp--;
        }

        if (satirYEslesenler > 2)
        {
            satirYEslesenler = 1;
        }
        else
        {
            satirYEslesenler = 1;
            silinecekYUrunler1.Clear();
        }

        Debug.Log(silinecekXUrunler.Count);
        Debug.Log(silinecekXUrunler1.Count);
        Debug.Log(silinecekYUrunler.Count);
        Debug.Log(silinecekYUrunler1.Count);

        if (silinecekXUrunler.Count > 2 || silinecekXUrunler1.Count>2 || silinecekYUrunler.Count>2 || silinecekYUrunler1.Count>2)
        {
            //Destroy(gameObject);
            if (silinecekXUrunler.Count > 2)
            {
                foreach (var item in silinecekXUrunler)
                {
                    Destroy(item.gameObject);
                }
            }
            if(silinecekXUrunler1.Count>2)
            {
                foreach (var item in silinecekXUrunler1)
                {
                    Destroy(item.gameObject);
                }
            }
            if(silinecekYUrunler.Count > 2)
            {
                foreach (var item in silinecekYUrunler)
                {
                    Destroy(item.gameObject);
                }
            }
            else
            {
                foreach (var item in silinecekYUrunler1)
                {
                    Destroy(item.gameObject);
                }
            }
                
        }
        else
        {
            ilkSecilenUrun.yerDegisim = false;
            ikinciSecilenUrun.yerDegisim = false;
        }
       


        /*int xEslesenler = 1; int sonXEkseni = 0, yEslesenler = 1, sonYEkseni = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (j == 6)
                {
                    continue;
                }

                if (SekerOlusturma.o_urunler[i, j].renk == SekerOlusturma.o_urunler[i, j+1].renk)
                {
                    sonXEkseni = j + 1;
                    xEslesenler++;
                }
                else
                {
                    if (xEslesenler > 2)
                    {
                        for (int g = 0; g < xEslesenler; g++)
                        {
                            silinecekUrunler.Add(SekerOlusturma.o_urunler[i, sonXEkseni]);
                            sonXEkseni--;
                        }
                    }
                    xEslesenler = 1;
                }
            }
            xEslesenler = 1;
        }

        for (int j = 0; j < 7; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                
                if (i ==3 )
                {
                    continue;
                }

                if (SekerOlusturma.o_urunler[i, j].renk == SekerOlusturma.o_urunler[i+1, j].renk)
                {
                    sonYEkseni = i + 1;
                    yEslesenler++;
                }
                else
                {
                    if (yEslesenler > 2)
                    {
                        for (int g = 0; g < yEslesenler; g++)
                        {
                            silinecekUrunler.Add(SekerOlusturma.o_urunler[sonYEkseni, j]);
                            sonYEkseni--;
                        }
                    }
                    yEslesenler = 1;
                }
            }
            yEslesenler = 1;
        }

        for (int i = 0; i < silinecekUrunler.Count; i++)
        {
            // ... Okey
        }*/

    }

    void YerDegis()
    {    
        transform.position = Vector3.Lerp(transform.position, HedefKonum, 0.1f);
    }

   /* void X_ekseni_kontrol()
    {
       // Debug.Log(urun);
        int ilkUrunKoordinatX = 0, ilkUrunKoordinatY = 0, ikinciUrunKoordinatX = 0, ikinciUrunKoordinatY = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (SekerOlusturma.o_urunler[i, j].x == ilkSecilenUrun.x && SekerOlusturma.o_urunler[i, j].y == ilkSecilenUrun.y)
                {
                    ilkUrunKoordinatX = i;
                    ilkUrunKoordinatY = j;
                }
            }
        }

        Urun urun_sagdaki = SekerOlusturma.o_urunler[ilkUrunKoordinatX, ilkUrunKoordinatY];
        if (renk == urun_sagdaki.renk)
        {
            urun_x_ekseni.Add(urun_sagdaki);
        }
        else
        {
           // break;
        }
    }*/
    


















        /* for (int i = x+1; i < SekerOlusturma.o_urunler.GetLength(0);i++) //0 dersem x ekseni i�in vericek onun yerine  i<6 da denilebilir belki?
         {
             Urun urun_sagdaki = SekerOlusturma.o_urunler[i,y];
             if(renk== urun_sagdaki.renk)
             {
                 urun_x_ekseni.Add(urun_sagdaki);
             }
             else
             {
                 break;
             }
         }
         for (int i = x-1; i>0; i--) //0 dersem x ekseni i�in vericek onun yerine  i<6 da denilebilir belki?
         {
             Urun urun_sagdaki = SekerOlusturma.o_urunler[i, y];
             if (renk == urun_sagdaki.renk)
             {
                 urun_x_ekseni.Add(urun_sagdaki);
             }
             else
             {
                 break;
             }
         }



     }*/
        /* void Y_ekseni_kontrol()
         {
             for (int i = y + 1; i < SekerOlusturma.o_urunler.GetLength(0); i++) //0 dersem x ekseni i�in vericek onun yerine  i<6 da denilebilir belki?
             {
                 Urun urun_sagdaki = SekerOlusturma.o_urunler[x,i];
                 if (renk == urun_sagdaki.renk)
                 {
                     urun_y_ekseni.Add(urun_sagdaki);
                 }
                 else
                 {
                     break;
                 }
             }
             for (int i = y - 1; i > 0; i--) //0 dersem x ekseni i�in vericek onun yerine  i<6 da denilebilir belki?
             {
                 Urun urun_sagdaki = SekerOlusturma.o_urunler[x, i];
                 if (renk == urun_sagdaki.renk)
                 {
                     urun_y_ekseni.Add(urun_sagdaki);
                 }
                 else
                 {
                     break;
                 }
             }
         }*/

    }
