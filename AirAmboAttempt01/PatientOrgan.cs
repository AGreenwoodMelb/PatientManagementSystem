﻿using AirAmboAttempt01.Patients.PatientBlood;
using AirAmboAttempt01.Patients.PatientInfection;

namespace AirAmboAttempt01.Patients.PatientOrgans
{
    #region BaseOrganClasses
    public class Organ
    {
        private BleedingSeverity _isBleeding = BleedingSeverity.None;
        public BleedingSeverity IsBleeding
        {
            get { return _isBleeding; }
            protected set { _isBleeding = value; }
        }
        public readonly float BloodLossRate;

        public Organ(float bloodLossRate)
        {
            BloodLossRate = bloodLossRate;
        }
    }
    #endregion
    #region PracticalOrganClasses
    #region HeadOrgans
    public class Brain : Organ
    {
        #region Props
        private float _currentPressure;
        public float CurrentPressure
        {
            get { return _currentPressure; }
            set { _currentPressure = value; }
        }

        private bool _isSeizing;
        public bool IsSeizing
        {
            get { return _isSeizing; }
            set { _isSeizing = value; }
        }
        #endregion

        public Brain() : base(DefaultBloodLossBaseRates.Brain)
        {

        }
    }
    #endregion
    #region ChestOrgans
    public class Heart : Organ
    {
        private bool _isBeating;
        private bool _isArrythmic;
        private bool _hasPaceMaker;

        private int _beatsPerMinute;

        public Heart() : base(DefaultBloodLossBaseRates.Heart)
        {

        }
    }
    public class Lung : Organ
    {
        private LungLobe _upperLobe = new LungLobe();

        public LungLobe UpperLobe
        {
            get { return _upperLobe; }
            set { _upperLobe = value; }
        }

        private LungLobe _middleLobe = new LungLobe();

        public LungLobe MiddleLobe
        {
            get { return _middleLobe; }
            set { _middleLobe = value; }
        }

        private LungLobe _lowerLobe = new LungLobe();

        public LungLobe LowerLobe
        {
            get { return _lowerLobe; }
            set { _lowerLobe = value; }
        }


        public Lung(bool isLeft) : base(DefaultBloodLossBaseRates.Lung)
        {
            if (isLeft)
                MiddleLobe = null;
        }
    }
    public class LungLobe
    {
        public Infection infection;
        public bool isDestroyed;
    }
    public class Lungs
    {
        private Lung _leftLung = new Lung(true);

        public Lung LeftLung
        {
            get { return _leftLung; }
            set { _leftLung = value; }
        }

        private Lung _rightLung = new Lung(false);

        public Lung RightLung
        {
            get { return _rightLung; }
            set { _rightLung = value; }
        }
    }
    #endregion
    #region AbdomenOrgans
    public class Kidney : Organ
    {
        public Kidney() : base(DefaultBloodLossBaseRates.Kidney)
        {

        }
    }
    public class Kidneys
    {
        private Kidney _leftKidney = new Kidney();

        public Kidney LeftKidney
        {
            get { return _leftKidney; }
            set { _leftKidney = value; }
        }

        private Kidney _rightKidney = new Kidney();

        public Kidney RightKidney
        {
            get { return _rightKidney; }
            set { _rightKidney = value; }
        }
    }

    public class Liver : Organ
    {
        public Liver() : base(DefaultBloodLossBaseRates.Liver)
        {

        }
    }

    public class GastrointestinalTract : Organ
    {
        public GastrointestinalTract() : base(DefaultBloodLossBaseRates.GI)
        {

        }
    }

    public class Spleen : Organ
    {
        public Spleen() : base(DefaultBloodLossBaseRates.Spleen)
        {

        }
    }

    public class Pancreas : Organ
    {
        public Pancreas() : base(DefaultBloodLossBaseRates.Pancreas)
        {

        }
    }

    public abstract class Reproductive : Organ
    {
        public Reproductive(float bloodLossRate) : base(bloodLossRate)
        {

        }
    }

    public class Reproductive_Male : Reproductive
    {
        public Reproductive_Male() : base(DefaultBloodLossBaseRates.Reproductive_Male)
        {

        }
    }

    public class Reproductive_Female : Reproductive
    {
        readonly bool isPregnant;
        public Reproductive_Female() : base(DefaultBloodLossBaseRates.Reproductive_Female)
        {

        }
    }
    #endregion
    #endregion
}

