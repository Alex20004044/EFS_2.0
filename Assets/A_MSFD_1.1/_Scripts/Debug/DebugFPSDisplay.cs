using System.Collections.Generic;
using UnityEngine;
namespace MSFD.DebugTool
{
    /// <summary>
    /// This class can display FPS in floating window
    /// </summary>
    public class DebugFPSDisplay : MonoBehaviour
    {
        [SerializeField]
        float updateValuesTime = 0.5f;
        /// <summary>Positioning rect for window.</summary>
        [SerializeField]
        Vector2 windowRectSize = new Vector2(120, 100);
        /// <summary>Shows or hides GUI (does not affect settings).</summary>
        [SerializeField]
        bool Visible = true;
        /// <summary>Unity GUI Window ID (must be unique or will cause issues).</summary>
        [SerializeField]
        int WindowId = 201;

        List<float> deltaTimes;
        Rect windowRect;
        int averageFPS = 0;
        int fpsVariation = 0;

        string visibleToogleName = "Is Visible";

        private void Awake()
        {
            deltaTimes = new List<float>();
            for(int i =0; i < 10; i++)
            {
                deltaTimes.Add(1);
            }
            InvokeRepeating("UpdateValues", 0, updateValuesTime);

            Vector2 position = new Vector2(Screen.width - windowRectSize.x, 0);
            windowRect = new Rect(position, windowRectSize);
        }
        public void OnGUI()
        {
            if (!this.Visible)
            {
                return;
            }
            this.windowRect = GUILayout.Window(this.WindowId, this.windowRect, FPSWindow, "FPS Display");
        }

        void UpdateValues()
        {
            if (!this.Visible)
            {
                return;
            }
            averageFPS = GetAverageFPS();
            fpsVariation = GetFPSVariation();
        }
        void FPSWindow(int windowId)
        {
            UpdateDeltaTimes();

            GUILayout.Label(string.Format("FPS:{0} +/-{1}", averageFPS, fpsVariation));
            GUILayout.Label(string.Format("Time:{0:#.##}ms", (float)1000/averageFPS));

            Visible = GUILayout.Toggle(Visible, visibleToogleName);
            if (GUI.changed)
            {
                this.windowRect.height = 100;
            }

            GUI.DragWindow();
        }

        void UpdateDeltaTimes()
        {
            deltaTimes.RemoveAt(0);
            deltaTimes.Add(UnityEngine.Time.deltaTime);
        }

        int GetAverageFPS()
        {
            double deltaTimeSum = 0;
            for(int i = 0; i < deltaTimes.Count; i++)
            {
                float deltaTime = deltaTimes[i];
                deltaTimeSum += deltaTime;
            }

            return (int)(deltaTimes.Count / (deltaTimeSum));
        }
        int GetFPSVariation()
        {
            int averagefps = GetAverageFPS();

            int maxFPSVariation = 0;
            for (int i = 0; i < deltaTimes.Count; i++)
            {
                float deltaTime = deltaTimes[i];
                int variation = Mathf.Abs((averagefps - (int)(1 / deltaTime)));
                if(maxFPSVariation < variation)
                {
                    maxFPSVariation = variation;
                }
            }
            return maxFPSVariation;
        }
        /*
        private void NetSimWindow(int windowId)
        {
            GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", this.Peer.RoundTripTime, this.Peer.RoundTripTimeVariance));

            bool simEnabled = this.Peer.IsSimulationEnabled;
            bool newSimEnabled = GUILayout.Toggle(simEnabled, "Simulate");
            if (newSimEnabled != simEnabled)
            {
                this.Peer.IsSimulationEnabled = newSimEnabled;
            }

            float inOutLag = this.Peer.NetworkSimulationSettings.IncomingLag;
            GUILayout.Label("Lag " + inOutLag);
            inOutLag = GUILayout.HorizontalSlider(inOutLag, 0, 500);

            this.Peer.NetworkSimulationSettings.IncomingLag = (int)inOutLag;
            this.Peer.NetworkSimulationSettings.OutgoingLag = (int)inOutLag;

            float inOutJitter = this.Peer.NetworkSimulationSettings.IncomingJitter;
            GUILayout.Label("Jit " + inOutJitter);
            inOutJitter = GUILayout.HorizontalSlider(inOutJitter, 0, 100);

            this.Peer.NetworkSimulationSettings.IncomingJitter = (int)inOutJitter;
            this.Peer.NetworkSimulationSettings.OutgoingJitter = (int)inOutJitter;

            float loss = this.Peer.NetworkSimulationSettings.IncomingLossPercentage;
            GUILayout.Label("Loss " + loss);
            loss = GUILayout.HorizontalSlider(loss, 0, 10);

            this.Peer.NetworkSimulationSettings.IncomingLossPercentage = (int)loss;
            this.Peer.NetworkSimulationSettings.OutgoingLossPercentage = (int)loss;

            // if anything was clicked, the height of this window is likely changed. reduce it to be layouted again next frame
            if (GUI.changed)
            {
                this.WindowRect.height = 100;
            }

            GUI.DragWindow();
        }*/
    }
}