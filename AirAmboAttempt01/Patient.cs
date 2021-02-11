﻿using AirAmboAttempt01.Patients.PatientPhysical;
using AirAmboAttempt01.Patients.PatientMental;

namespace AirAmboAttempt01.Patients
{
    public class Patient
    {
        #region Props
        //public int _randomSeed { get;} //For Unity stuff
        private Biography _biography;
        public Biography Biography
        {
            get { return _biography; }
            set { _biography = value; }
        }

        private bool _isAlive = true;
        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        private Physical _body;
        public Physical Body
        {
            get { return _body; }
            private set { _body = value; }
        }

        private Mental _mind;
        public Mental Mind
        {
            get { return _mind; }
            private set { _mind = value; }
        }
        #endregion

        public Patient(Biography biography = null, Physical body = null, Mental mind = null)
        {
            Biography = biography ?? new Biography();
            Body = body ?? new Physical();
            Mind = mind ?? new Mental();
            //_randomSeed = MagicRandomStaticThingy;
        }

        public float BloodVolumeCheck()
        {
            float bloodVolumeRatio = _body.Blood.Volume / _body.Blood._defaultBloodSystemVolume;
            return bloodVolumeRatio;
        }
    }

    public class Biography
    {
        #region Props
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            private set { _firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        private Gender _gender;
        public Gender Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        #endregion

        public Biography(string firstName = "John", string lastName = "Doe", int age = 30, Gender gender = Gender.Other)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Gender = gender;
        }
    }
}
