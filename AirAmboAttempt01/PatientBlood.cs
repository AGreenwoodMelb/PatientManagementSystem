﻿using System;
using System.Collections.Generic;

namespace AirAmboAttempt01.Patients.PatientBlood
{
    public enum BloodABO
    {
        O,
        A,
        B,
        AB
    }
    public enum BloodRhesus
    {
        Negative,
        Positive
    }
    public struct BloodType
    {
        public BloodABO ABO;
        public BloodRhesus Rhesus;
    }
    public enum BleedingSeverity
    {
        None,
        Mild,
        Moderate,
        Severe,
        Extreme
    }
    public static class DefaultBloodLossBaseRates
    {
        public static readonly float Superficial = 1f;

        public static readonly float Brain = 1f;

        public static readonly float Heart = 1f;
        public static readonly float Lung = 1f;

        public static readonly float GI = 1f;
        public static readonly float Kidney = 1f;
        public static readonly float Bladder = 1f;
        public static readonly float Liver = 1f;
        public static readonly float Pancreas = 1f;
        public static readonly float Spleen = 1f;
        public static readonly float Reproductive_Male = 1f;
        public static readonly float Reproductive_Female = 1f;

        public static readonly Dictionary<BleedingSeverity, float> BleedingSeverityMultiplier = new Dictionary<BleedingSeverity, float>()
        {
            { BleedingSeverity.None, 0},
            { BleedingSeverity.Mild, 0.5f},
            { BleedingSeverity.Moderate, 1f},
            { BleedingSeverity.Severe, 2f},
            { BleedingSeverity.Extreme, 2.5f}
        };

       
    }
    public class BloodSystem : Blood
    {
        #region DefaultValues
        static readonly float _defaultBloodSystemVolume = 6000f; //mL
        #endregion

        private IllilcitDrugsProfile _illicitDrugsProfile;
        public IllilcitDrugsProfile IllilcitDrugsProfile
        {
            get { return _illicitDrugsProfile; }
        }

        #region Constructors
        public BloodSystem()
        {
            Volume = _defaultBloodSystemVolume;
        }

        public BloodSystem(Blood bloodSetup) : base(bloodSetup.bloodType, bloodSetup.Volume, bloodSetup.FluidProfile)
        {

        }

        public BloodSystem(BloodType bloodType, FluidProfile fluidProfile, float bloodVolume) : base(bloodType, bloodVolume, fluidProfile)
        {

        }
        #endregion

        public bool Transfuse(Fluid incFluid)
        {
            bool successFlag = false;

            switch (incFluid)
            {
                case Blood incBlood:
                    AddFluid(incBlood);
                    break;
                case Drug incDrug:
                    DoingDrugs(incDrug);
                    break;
                case Fluid incBaseFluid:
                    AddFluid(incBaseFluid);
                    break;
                default:
                    throw new ArgumentException(
                        message: "BloodSystem::Transfuse Unhandled Subtype of Fluid",
                        paramName: nameof(incFluid)
                        );
            }
            return successFlag;
        }

        private bool DoingDrugs(Drug incDrug)
        {
            switch (incDrug.drugType)
            {

                case DrugType.Stimulant:
                    _illicitDrugsProfile.stimulants = true;
                    break;
                case DrugType.Sedative:
                    _illicitDrugsProfile.sedetives = true;
                    break;
                case DrugType.Opiods:
                    _illicitDrugsProfile.opiods = true;
                    break;
                case DrugType.Hallucinogens:
                    _illicitDrugsProfile.hallucinogens = true;
                    break;
                case DrugType.Detoxer:
                    _illicitDrugsProfile = new IllilcitDrugsProfile(); //Bool default is false
                    break;
                case DrugType.None:
                default:
                    break;
            }

            return true;
        }

        public float BloodVolumeCheck()
        {
            float bloodVolumeRatio = Volume / _defaultBloodSystemVolume;
            Console.WriteLine(bloodVolumeRatio);
            return bloodVolumeRatio;
        }//Probably not needed here //Put In condition / event Manager
    }
}
