﻿using PatientManagementSystem.Patients.PatientBlood;
using PatientManagementSystem.Patients.PatientBones;
using PatientManagementSystem.Patients.PatientDefaults;
using PatientManagementSystem.Patients.PatientInfection;
using PatientManagementSystem.Patients.PatientOrgans;
using PatientManagementSystem.Patients.Vascular;
using System;

namespace PatientManagementSystem.Patients.PatientPhysical
{
    /*This class should eventually hold all the calculation based results (e.g GetINR()) as access to all components is available here
     * 
     */

    public class Physical
    {
        #region Props
        private float _bodyTemperature;//Centigrade 
        public float BodyTemperature//May change this later
        {
            get { return (float) Math.Round(_bodyTemperature, 1); }
            set { _bodyTemperature = value; }
        }

        private Anthropometrics _anthropometrics;
        public Anthropometrics Anthropometrics
        {
            get { return _anthropometrics; }
            set { _anthropometrics = value; }
        }

        private BloodSystem _blood;
        public BloodSystem Blood
        {
            get { return _blood; }
            set { _blood = value; }
        }

        private Limbs _limbs;
        public Limbs Limbs
        {
            get { return _limbs; }
            set { _limbs = value; }
        }

        private Head _head;
        public Head Head
        {
            get { return _head; }
            set { _head = value; }
        }

        private Chest _chest;
        public Chest Chest
        {
            get { return _chest; }
            set { _chest = value; }
        }

        private Abdomen _abdomen;
        public Abdomen Abdomen
        {
            get { return _abdomen; }
            set { _abdomen = value; }
        }

        private Infections _infections;
        public Infections Infections
        {
            get { return _infections; }
            set { _infections = value; }
        }

        private VascularSystem _vascularSystem;

        public VascularSystem VascularSystem
        {
            get { return _vascularSystem; }
            set { _vascularSystem = value; }
        }

        #endregion

        public Physical(Anthropometrics metrics = null, Head head = null, Chest chest = null, Abdomen abdomen = null, BloodSystem blood = null, Limbs limbs = null, Infections infections = null, VascularSystem vascularSystem = null)
        {
            _anthropometrics = metrics ?? new Anthropometrics();
            Head = head ?? new Head();
            Chest = chest ?? new Chest();
            Abdomen = abdomen ?? new Abdomen();
            Blood = blood ?? new BloodSystem();
            Limbs = limbs ?? new Limbs();
            Infections = infections ?? new Infections();
            VascularSystem = vascularSystem ?? new VascularSystem();
        }
    }

    public abstract class PhysicalPart
    {
        public BleedingSeverity SurfaceBleedingSeverity;
        public PainSeverity PainSeverity;

        private Bone[] _bones;
        public Bone[] Bones
        {
            get { return _bones; }
            protected set { _bones = value; }
        }

        public bool AnyBonesBroken()
        {
            foreach (Bone bone in Bones)
            {
                if (bone.isBroken)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Head : PhysicalPart
    {
        #region Props
        private Brain _brain;
        public Brain Brain
        {
            get { return _brain; }
            set { _brain = value; }
        }
        #endregion

        public Head(Bone[] headBoneStructure = null, Brain brain = null)
        {
            Bones = headBoneStructure ?? DefaultBoneStructures.DefaultHeadBones;
            Brain = brain ?? new Brain();
        }
    }

    public class Chest : PhysicalPart
    {
        #region Props
        private Heart _heart;
        public Heart Heart
        {
            get { return _heart; }
            set { _heart = value; }
        }

        private RespiratorySystem _lungs;
        public RespiratorySystem Lungs
        {
            get { return _lungs; }
            set { _lungs = value; }
        }
        #endregion

        public Chest(Bone[] chestBoneStructure = null, Heart heart = null, RespiratorySystem lungs = null)
        {
            Bones = chestBoneStructure ?? DefaultBoneStructures.DefaultChestBones;
            Heart = heart ?? new Heart();
            Lungs = lungs ?? new RespiratorySystem();
        }
    }

    public class Abdomen : PhysicalPart
    {
        #region Props
        private GastrointestinalTract _gastrointestinalTract;
        public GastrointestinalTract GastrointestinalTract
        {
            get { return _gastrointestinalTract; }
            set { _gastrointestinalTract = value; }
        }

        private Liver _liver;
        public Liver Liver
        {
            get { return _liver; }
            set { _liver = value; }
        }

        private Pancreas _pancreas;
        public Pancreas Pancreas
        {
            get { return _pancreas; }
            set { _pancreas = value; }
        }

        private UrinaryTract _kidneys;
        public UrinaryTract UrinaryTract
        {
            get { return _kidneys; }
            set { _kidneys = value; }
        }

        private Spleen _spleen;
        public Spleen Spleen
        {
            get { return _spleen; }
            set { _spleen = value; }
        }

        private Reproductive _reproductives;
        public Reproductive Reproductives
        {
            get { return _reproductives; }
            set { _reproductives = value; }
        }
        #endregion

        public Abdomen(Bone[] abdomenBoneStructure = null, GastrointestinalTract gastrointestinalTract = null, Liver liver = null, Pancreas pancreas = null, UrinaryTract urinaryTract = null, Spleen spleen = null, Reproductive reproductives = null)
        {
            Bones = abdomenBoneStructure ?? DefaultBoneStructures.DefaultAbdomenBones;
            GastrointestinalTract = gastrointestinalTract ?? new GastrointestinalTract();
            Liver = liver ?? new Liver();
            Pancreas = pancreas ?? new Pancreas();
            UrinaryTract = urinaryTract ?? new UrinaryTract();
            Spleen = spleen ?? new Spleen();
            Reproductives = reproductives ?? new Reproductive_Female();
        }
    }

    //What am I doing with this? Fractures? Infections? Stuff????
    #region LimbRelatedClasses
    public class Limbs
    {
        private Arms _arms = new Arms();

        public Arms Arms
        {
            get { return _arms; }
            set { _arms = value; }
        }

        private Legs _legs = new Legs();

        public Legs Legs
        {
            get { return _legs; }
            set { _legs = value; }
        }
    }

    public class Limb : PhysicalPart
    {
        //Bool isImmobile //For later
    }

    public class Arms
    {
        #region Props
        private Arm _leftArm;
        public Arm LeftArm
        {
            get { return _leftArm; }
            set { _leftArm = value; }
        }

        private Arm _rightArm;
        public Arm RightArm
        {
            get { return _rightArm; }
            set { _rightArm = value; }
        }
        #endregion

        public Arms(Arm leftArm = null, Arm rightArm = null)
        {
            LeftArm = leftArm ?? new Arm();
            RightArm = rightArm ?? new Arm();
        }
    }
    public class Arm : Limb
    {
        public Arm(Bone[] armBoneStructure = null)
        {
            Bones = armBoneStructure ?? DefaultBoneStructures.DefaultArmBones;
        }
    }

    public class Legs
    {
        #region Props
        private Leg _leftLeg;
        public Leg LeftLeg
        {
            get { return _leftLeg; }
            set { _leftLeg = value; }
        }

        private Leg _rightLeg;
        public Leg RightLeg
        {
            get { return _rightLeg; }
            set { _rightLeg = value; }
        }
        #endregion

        public Legs(Leg leftLeg = null, Leg rightLeg = null)
        {
            LeftLeg = leftLeg ?? new Leg();
            RightLeg = rightLeg ?? new Leg();
        }
    }
    public class Leg : Limb
    {
        public Leg(Bone[] legBoneStructure = null)
        {
            Bones = legBoneStructure ?? DefaultBoneStructures.DefaultLegBones;
        }
    }
    #endregion

    public class Anthropometrics
    {
        /* NOTES:
         * Currently this class should only contain readonly values as it is passed as a reference to the PatientResults object hat players have access to.
         * 
         */

        public readonly int Age;
        public readonly float Height; //cm
        public readonly float Weight; //kg
        public float BMI => (float)Math.Round((Weight / Math.Pow((Height / 100), 2)), 1);

        public Anthropometrics()
        {
            Age = DefaultPatientMetrics.DefaultAge;
            Height = DefaultPatientMetrics.DefaultHeight;
            Weight = DefaultPatientMetrics.DefaultWeight;
        }
        public Anthropometrics(int age, float height, float weight)
        {
            Age = Math.Clamp(age, DefaultPatientMetrics.MinAge, DefaultPatientMetrics.MaxAge);
            Height = Math.Clamp(height, DefaultPatientMetrics.MinHeight, DefaultPatientMetrics.MaxHeight);
            Weight = Math.Clamp(weight, DefaultPatientMetrics.MinWeight, DefaultPatientMetrics.MaxWeight);
        }
    }
}
