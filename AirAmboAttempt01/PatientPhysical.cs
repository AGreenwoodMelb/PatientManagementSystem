﻿using AirAmboAttempt01.Patients.PatientBlood;
using AirAmboAttempt01.Patients.PatientBones;
using AirAmboAttempt01.Patients.PatientOrgans;

namespace AirAmboAttempt01.Patients.PatientPhysical
{

    public class Physical
    {
        #region Props
        private BloodSystem _blood = new BloodSystem();
        public BloodSystem Blood
        {
            get { return _blood; }
            set { _blood = value; }
        }

        private Limbs _limbs = new Limbs();
        public Limbs Limbs
        {
            get { return _limbs; }
            set { _limbs = value; }
        }

        private Head _head = new Head();
        public Head Head
        {
            get { return _head; }
            set { _head = value; }
        }

        private Chest _chest = new Chest();
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
        #endregion

        public Physical(bool isMale = false)
        {
            Abdomen = new Abdomen(isMale);
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
        private Brain _brain;
        public Brain Brain
        {
            get { return _brain; }
            set { _brain = value; }
        }

        public Head(Bone[] headBoneStructure = null)
        {
            if (headBoneStructure == null)
            {
                Bones = DefaultBoneStructures.DefaultHeadBones;
            }
            else
            {
                Bones = headBoneStructure;
            }
        }
    }

    public class Chest : PhysicalPart
    {
        #region Props
        private Heart _heart = new Heart();
        public Heart Heart
        {
            get { return _heart; }
            set { _heart = value; }
        }

        private Lungs _lungs = new Lungs();

        public Lungs Lungs
        {
            get { return _lungs; }
            set { _lungs = value; }
        }
        #endregion

        public Chest(Bone[] chestBoneStructure = null)
        {
            if (chestBoneStructure == null)
            {
                Bones = DefaultBoneStructures.DefaultChestBones;
            }
            else
            {
                Bones = chestBoneStructure;
            }
        }
    }

    public class Abdomen : PhysicalPart
    {
        #region Props
        private GastrointestinalTract _gastrointestinalTract = new GastrointestinalTract();
        public GastrointestinalTract GastrointestinalTract
        {
            get { return _gastrointestinalTract; }
            set { _gastrointestinalTract = value; }
        }

        private Liver _liver = new Liver();
        public Liver Liver
        {
            get { return _liver; }
            set { _liver = value; }
        }

        private Pancreas _pancreas = new Pancreas();
        public Pancreas Pancreas
        {
            get { return _pancreas; }
            set { _pancreas = value; }
        }

        private Kidneys _kidneys = new Kidneys();

        public Kidneys Kidneys
        {
            get { return _kidneys; }
            set { _kidneys = value; }
        }


        private Spleen _spleen = new Spleen();
        public Spleen Spleen
        {
            get { return _spleen; }
            set { _spleen = value; }
        }

        private Reproductive _repoductives;
        public Reproductive Reproductives
        {
            get { return _repoductives; }
            set { _repoductives = value; }
        }
        #endregion

        public Abdomen(bool isMale, Bone[] AbdomenBoneStructure = null)
        {
            if (isMale)
            {
                _repoductives = new Reproductive_Male();
            }
            else
            {
                _repoductives = new Reproductive_Female();
            }

            if (AbdomenBoneStructure == null)
            {
                Bones = DefaultBoneStructures.DefaultAbdomenBones;
            }
            else
            {
                Bones = AbdomenBoneStructure;
            }
        }

        public Organ[] GetOrgans()
        {
            return new Organ[]
            {
                GastrointestinalTract,
                Kidneys.LeftKidney,
                Kidneys.RightKidney,
                Liver,
                Pancreas,
                Spleen,
                Reproductives
            };
        }
    }

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
        private Arm _leftArm = new Arm();

        public Arm LeftArm
        {
            get { return _leftArm; }
            set { _leftArm = value; }
        }

        private Arm _rightArm = new Arm();

        public Arm RightArm
        {
            get { return _rightArm; }
            set { _rightArm = value; }
        }

    }
    public class Arm : Limb
    {
        public Arm(Bone[] armBoneStructure = null)
        {
            if (armBoneStructure == null)
            {
                Bones = DefaultBoneStructures.DefaultArmBones;
            }
            else
            {
                Bones = armBoneStructure;
            }
        }
    }

    public class Legs
    {
        private Leg _leftLeg = new Leg();

        public Leg LeftLeg
        {
            get { return _leftLeg; }
            set { _leftLeg = value; }
        }

        private Leg _rightLeg = new Leg();

        public Leg RightLeg
        {
            get { return _rightLeg; }
            set { _rightLeg = value; }
        }

    }
    public class Leg : Limb
    {
        public Leg(Bone[] legBoneStructure = null)
        {
            if (legBoneStructure == null)
            {
                Bones = DefaultBoneStructures.DefaultArmBones;
            }
            else
            {
                Bones = legBoneStructure;
            }
        }
    }
    #endregion
}