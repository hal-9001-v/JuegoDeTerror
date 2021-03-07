using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

    public string itemName;
    public Texture itemIcon;
    public AudioClip pickingSound;
    public AudioClip nearSound;

    [Range(0.1f,10 )]
    public float delay;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (source != null && pickingSound != null)
        {
            source.clip = pickingSound;

            StartCoroutine(playNearSound());

        }
    }

    public void addToInventory()
    {
        //inventory.AddItem(this);
        GameEventManager.sharedInstance.AddedItemToInventoryEvent(this);

        StopAllCoroutines();

        if (pickingSound != null && source != null)
        {
            source.clip = pickingSound;
            source.Play();
        }

    }

    IEnumerator playNearSound()
    {

        while (true)
        {

            if (!source.isPlaying)
            {

                if (source != null && nearSound != null)
                {
                    source.clip = nearSound;
                    source.Play();

                }
            }
            else
            {

                yield return new WaitForSeconds(delay);

            }





        }



    }

    public abstract void useItem();

}