//using System.Collections.Generic;
//using System.Linq;
//using Convai.Scripts.Utils;
//using UnityEngine;
//using UnityEngine.Events;

//namespace Convai.Scripts.Narrative_Design
//{
//    public class NarrativeDesignTrigger : MonoBehaviour
//    {
//        public ConvaiNPC convaiNPC;
//        [HideInInspector] public int selectedTriggerIndex;
//        [HideInInspector] public List<string> availableTriggers;
//        public UnityEvent onTriggerEvent;

//        private void Awake()
//        {
//            onTriggerEvent.AddListener(InvokeSelectedTrigger);
//        }

//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.CompareTag("Player")) InvokeSelectedTrigger();
//        }


//        private void OnValidate()
//        {
//            availableTriggers = null;

//            if (convaiNPC != null)
//            {
//                NarrativeDesignManager narrativeDesignManager = convaiNPC.GetComponent<NarrativeDesignManager>();
//                if (narrativeDesignManager != null) availableTriggers = narrativeDesignManager.triggerDataList.Select(trigger => trigger.triggerName).ToList();
//            }
//        }

//        /// <summary>
//        ///   Invokes the selected trigger.
//        /// </summary>
//        public void InvokeSelectedTrigger()
//        {
//            if (convaiNPC != null && availableTriggers != null && selectedTriggerIndex >= 0 && selectedTriggerIndex < availableTriggers.Count)
//            {
//                string selectedTriggerName = availableTriggers[selectedTriggerIndex];
//                ConvaiNPCManager.Instance.SetActiveConvaiNPC(convaiNPC);
//                convaiNPC.TriggerEvent(selectedTriggerName);
//            }
//        }
//    }
//}
//using System.Collections.Generic;
//using System.Linq;
//using Convai.Scripts.Utils;
//using UnityEngine;
//using UnityEngine.Events;

//namespace Convai.Scripts.Narrative_Design
//{
//    public class NarrativeDesignTrigger : MonoBehaviour
//    {
//        public ConvaiNPC convaiNPC;
//        [HideInInspector] public int selectedTriggerIndex;
//        [HideInInspector] public List<string> availableTriggers;
//        public UnityEvent onTriggerEvent;
//        private CharacterSpawner1 characterSpawner;

//        private void Awake()
//        {
//            //onTriggerEvent.AddListener(InvokeSelectedTrigger);
//            //Debug.Log("Calling InvokeSelectedTrigger");
//        }

//        private void OnEnable()
//        {
//            //Following is code added on top of demo
//            characterSpawner = FindObjectOfType<CharacterSpawner1>();
//            if (characterSpawner != null)
//            {
//                // Subscribe to the OnCharacterSpawned event
//                characterSpawner.OnCharacterSpawned += HandleCharacterSpawned;
//                Debug.Log("Subscribed to OnCharacterSpawned event.");
//            }
//            else
//            {
//                Debug.LogWarning("CharacterSpawner1 instance not found in the scene.");
//            }
//            //End code added on top of demo
//        }

//        //private void Update()
//        //{
//        //    if (Input.GetKeyDown(KeyCode.B))
//        //    {
//        //        InvokeSelectedTrigger();
//        //    }
//        //}

//        //Following is code added on top of demo
//        private void HandleCharacterSpawned()
//        {
//            Debug.Log("Character spawned event received. Invoking selected trigger.");
//            InvokeSelectedTrigger();
//        }
//        //End code added on top of demo

//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.CompareTag("Player")) InvokeSelectedTrigger();
//        }


//        private void OnValidate()
//        {
//            availableTriggers = null;

//            if (convaiNPC != null)
//            {
//                NarrativeDesignManager narrativeDesignManager = convaiNPC.GetComponent<NarrativeDesignManager>();
//                if (narrativeDesignManager != null) availableTriggers = narrativeDesignManager.triggerDataList.Select(trigger => trigger.triggerName).ToList();
//            }
//        }

//        /// <summary>
//        ///   Invokes the selected trigger.
//        /// </summary>
//        public void InvokeSelectedTrigger()
//        {
//            if (convaiNPC != null && availableTriggers != null && selectedTriggerIndex >= 0 && selectedTriggerIndex < availableTriggers.Count)
//            {
//                string selectedTriggerName = availableTriggers[selectedTriggerIndex];
//                ConvaiNPCManager.Instance.SetActiveConvaiNPC(convaiNPC);
//                convaiNPC.TriggerEvent(selectedTriggerName);
//                Debug.Log("Trigger event invoked: " + selectedTriggerName);
//            }
//        }
//    }
//}

//working on this currently
using System.Collections.Generic;
using System.Linq;
using Convai.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Convai.Scripts.Narrative_Design
{
    public class NarrativeDesignTrigger : MonoBehaviour
    {
        public ConvaiNPC convaiNPC;
        [HideInInspector] public int selectedTriggerIndex;
        [HideInInspector] public List<string> availableTriggers;
        public UnityEvent onTriggerEvent;
        //private CharacterSpawner1 characterSpawner;

        private void Awake()
        {
            //onTriggerEvent.AddListener(InvokeSelectedTrigger);
            //characterSpawner = FindObjectOfType<CharacterSpawner1>();

        }

        private void Start()
        {
            InvokeSelectedTrigger();
        }

        //private void OnEnable()
        //{
        //    //Following is code added on top of demo
        //    if (characterSpawner != null)
        //    {
        //        // Subscribe to the OnCharacterSpawned event
        //        characterSpawner.OnCharacterSpawned += HandleCharacterSpawned;
        //        Debug.Log("Subscribed to OnCharacterSpawned");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("CharacterSpawner1 instance not found in the scene.");
        //    }
        //    //End code added on top of demo
        //}

        //private void Update()
        //{
        //    //if (Input.GetKeyDown(KeyCode.B))
        //    //{
        //    //    InvokeSelectedTrigger();
        //    //}
            
        //}

        ////Following is code added on top of demo
        //private void HandleCharacterSpawned()
        //{
        //    Debug.Log("Calling InvokeSelectedTrigger");
        //    InvokeSelectedTrigger();
        //}
        ////End code added on top of demo

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) InvokeSelectedTrigger();
        }


        private void OnValidate()
        {
            availableTriggers = null;

            if (convaiNPC != null)
            {
                NarrativeDesignManager narrativeDesignManager = convaiNPC.GetComponent<NarrativeDesignManager>();
                if (narrativeDesignManager != null) availableTriggers = narrativeDesignManager.triggerDataList.Select(trigger => trigger.triggerName).ToList();
            }
        }

        /// <summary>
        ///   Invokes the selected trigger.
        /// </summary>
        public void InvokeSelectedTrigger()
        {
            if (convaiNPC != null && availableTriggers != null && selectedTriggerIndex >= 0 && selectedTriggerIndex < availableTriggers.Count)
            {
                string selectedTriggerName = availableTriggers[selectedTriggerIndex];
                ConvaiNPCManager.Instance.SetActiveConvaiNPC(convaiNPC);
                convaiNPC.TriggerEvent(selectedTriggerName);
                Debug.Log("Called trigger");
            }
        }
    }
}


