//Auto-generated Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//User Added Namespaces

namespace FirstHub
{
    class ThresholdManager
    {
        //Create an into to hold out user defined Threshold
        private static int _threshold;
        private static bool _thresholdToggled; // If true, item is over Threshold, false, under


        //Create a place for listeners to listen
        public event EventHandler onOverThreshold;
        public event EventHandler onUnderThreshold;
        public event EventHandler onOverThresholdToggle;
        public event EventHandler onUnderThresholdToggle;

        /// <summary>
        /// Called when the current value of an item is over the threshold
        /// </summary>
        private void OverThreshold()
        {
            //If we have listeners
            if (onOverThreshold != null)
            {
                //Send a copy of this object, and an empty set of Event Arguements
                onOverThreshold(this, new EventArgs());
            }
            //If theshold hasn't toggled
            if (!_thresholdToggled)
            {
                //Toggle Threshold
                _thresholdToggled = true;
                //If there are listeners to over threshold toggles
                if (onOverThresholdToggle != null)
                {
                    //Alert them of the change
                    onOverThresholdToggle(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Called when the current value of an item is under the threshold
        /// </summary>        
        private void UnderThreshold()
        {
            //If we have listeners
            if (onUnderThreshold != null)
            {
                //Send a copy of this object, and an empty set of Event Arguements
                onUnderThreshold(this, new EventArgs());
            }
            //If theshold has toggled
            if (_thresholdToggled)
            {
                //Toggle Threshold
                _thresholdToggled = false;
                //If there are listeners to under threshold toggles
                if (onUnderThresholdToggle != null)
                {
                    //Alert them of the change
                    onUnderThresholdToggle(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Object Creation Point
        /// </summary>
        /// <param name="Threshold">Number to alert on</param>
        public ThresholdManager(int Threshold)
        {
            //Set out global threshold for this obeject
            _threshold = Threshold;
        }

        //Make a public facing int to evaluate against Threshold
        public int current
        {
            set
            {
                //Check the value of the int compared to Threshold
                if (value > _threshold)
                {
                    //If is is over, run the OverThreshold Function
                    OverThreshold();
                }
                else
                {
                    //Otherwise consider it to be under the threshold
                    UnderThreshold();
                }
            }
        }

    }
}
