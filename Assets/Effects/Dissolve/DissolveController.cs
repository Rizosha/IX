using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Effects.Dissolve
{
    public class DissolveController : MonoBehaviour
    {
        public SkinnedMeshRenderer skin;
        public VisualEffect VFXGraph;

        private Material[] skinnedMaterials;

        public float dissolveRate;
        public float refreshRate = 0.025f;
   
        // Start is called before the first frame update
        void Start()
        {
            if (skin != null)
            {
                skinnedMaterials = skin.materials;
            }   
        }

        // Update is called once per frame
        void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Dissolve());
        }*/
        }

        IEnumerator Dissolve()
        {

            if (VFXGraph != null)
            {
                VFXGraph.Play();
            }
            if (skinnedMaterials.Length >0)
            {
                float counter = 0;
                while (skinnedMaterials[0].GetFloat("_DissolveAmount") <1)
                {
                    counter += dissolveRate;
                    for (int i = 0; i < skinnedMaterials.Length; i++)
                    {
                        skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                    }

                    yield return new WaitForSeconds(refreshRate);
                }
            
                while (skinnedMaterials[0].GetFloat("_DissolveAmount") == 1)
                {
                    counter -= dissolveRate;
                    for (int i = 0; i < skinnedMaterials.Length; i++)
                    {
                        skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                    }

                    yield return new WaitForSeconds(refreshRate);
                }
            }
        
     
        
        
        
        
        
        }
    }
}
