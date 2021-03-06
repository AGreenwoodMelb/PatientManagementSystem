﻿using PatientManagementSystem.Patients.PatientAccessPoints;
using PatientManagementSystem.Patients.PatientDefaults;
using PatientManagementSystem.Patients.PatientOrgans;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagementSystem.Patients.PatientInfection
{
    public enum InfectionSeverity
    {
        None,
        Mild,
        Moderate,
        Severe,
        Extreme
    }
    public enum InfectionPathogenType
    {
        None,
        Bacterial,
        Viral,
        Prion,
        Other
    }
    public enum InfectionTreatmentResistance
    {
        None,
        Susceptible,
        Stardard,
        Resistant,
        Immune
    }

    public struct Infection
    {
        public InfectionPathogenType PathogenType;

        private float _infectionLevel;
        public float InfectionLevel
        {
            get
            {
                return _infectionLevel;
            }
            set
            {
                _infectionLevel = Math.Clamp(value, 0f, 1f);
                if (_infectionLevel == 0)
                {
                    PathogenType = InfectionPathogenType.None;
                    TreatmentResistance = InfectionTreatmentResistance.None;
                }
            }
        }
        public InfectionSeverity Severity
        {
            get
            {
                InfectionSeverity severity = InfectionSeverity.None;
                for (int i = DefaultInfectionValues.SeverityLookup.Length - 1; i >= 0; i--)
                {
                    if (InfectionLevel <= DefaultInfectionValues.SeverityLookup[i].Item2)
                    {
                        severity = DefaultInfectionValues.SeverityLookup[i].Item1;
                    }
                }
                return severity;
            }
        } //Because if it worked by luck and chance in the Organ class why not do it again.....
        public InfectionTreatmentResistance TreatmentResistance;//LATER: Implement Drug specific resistances //Should this value automatically increase itself? //When should this be used? //This should eventually be broken out into a drug specific resistance

        public void CureInfection()
        {
            InfectionLevel = 0f;
        }

        public void Infect(Infection infection)
        {
            InfectionLevel = infection.InfectionLevel;
            PathogenType = infection.PathogenType;
            TreatmentResistance = infection.TreatmentResistance;
        }

    }

    public class Infections
    {
        #region Props
        public HeadContainer Head { get; set; }
        public ChestContainer Chest { get; set; }
        public AbdomenContainer Abdomen { get; set; }
        public AccessPointsContainer AccessPoints { get; set; }

        public bool HasSepticaemia;
        #endregion

        #region Constructors
        public Infections()
        {
            Head = new HeadContainer();
            Chest = new ChestContainer();
            Abdomen = new AbdomenContainer();
            AccessPoints = new AccessPointsContainer();
            HasSepticaemia = false;
        }//Default Constructor

        public Infections(Infections infections)
        {
            Head = infections.Head;
            Chest = infections.Chest;
            Abdomen = infections.Abdomen;
        }//Copy Constructor? I dont think this will actually decouple the references because infections.Head is a class reference, although does it matter? Am I going to return an Infections object or an Infection struct object?
        #endregion

        public Infection GetStrongestInfection()
        {
            Infection result = new Infection();
            foreach (Infection infection in GetInfectionsArray())
            {
                if (infection.Severity > result.Severity)
                    result = infection;
            }
            return result;
        }//This should find the highest level of infection severity and return an Infection object so that values can be calculated off the severity and type
        public Infection GetStrongestInfectionHead()
        {
            Infection result = new Infection();
            foreach (Infection infection in Head.GetInfections())
            {
                if (infection.Severity > result.Severity)
                    result = infection;
            }
            return result;
        }//I think this will be useful for the ExamineBrainCT PatientExamination
        public Infection GetStrongestInfectionChest()
        {
            Infection result = new Infection();
            foreach (Infection infection in Chest.GetInfections())
            {
                if (infection.Severity > result.Severity)
                    result = infection;
            }
            return result;
        }
        public Infection GetStrongestInfectionAbdomen()
        {
            Infection result = new Infection();
            foreach (Infection infection in Abdomen.GetInfections())
            {
                if (infection.Severity > result.Severity)
                    result = infection;
            }
            return result;
        }
        public void TreatInfection(InfectionPathogenType infectionType)
        {
            if (infectionType == InfectionPathogenType.None)
                return;

            Infection[] infections = GetInfectionsArray().Concat(AccessPoints.GetInfections()).ToArray();

            foreach (Infection infection in infections)
            {
                if (infection.PathogenType == infectionType)
                {
                    //Handle Resistance thing here?
                    //infection.DecreaseInfection();
                }
            }
        }//LATER: Implement second parameter of DrugType for resistance calc

        public IEnumerable<Infection> GetInfectionsArray()
        {
            return Head.GetInfections().Concat(Chest.GetInfections().Concat(Abdomen.GetInfections()));
        }//Still pretty bad

        #region ContainerClasses
        public abstract class DefaultContainer
        {
            public Infection Surface;
            public virtual IEnumerable<Infection> GetInfections()
            {
                return new Infection[]
                {
                    Surface
                };
            }
        }

        public class HeadContainer : DefaultContainer
        {
            public Infection Brain;

            public override IEnumerable<Infection> GetInfections()
            {
                return new Infection[]
                {
                    Brain,
                }.Concat(base.GetInfections());
            }
        }

        public class ChestContainer : DefaultContainer
        {
            public Infection Heart;
            public Dictionary<LungLobeLocation, Infection> LeftLung = new Dictionary<LungLobeLocation, Infection>()
            {
                {LungLobeLocation.Upper, new Infection() },
                {LungLobeLocation.Lower, new Infection() },

            };
            public Dictionary<LungLobeLocation, Infection> RightLung = new Dictionary<LungLobeLocation, Infection>()
            {
                {LungLobeLocation.Upper, new Infection() },
                {LungLobeLocation.Middle, new Infection() },
                {LungLobeLocation.Lower, new Infection() },
            };

            public override IEnumerable<Infection> GetInfections()
            {
                IEnumerable<Infection> structures = new Infection[]
                {
                    Heart,
                }.Concat(base.GetInfections());
                IEnumerable<Infection> leftLung = LeftLung.Values.ToArray();
                IEnumerable<Infection> rightLung = RightLung.Values.ToArray();

                return structures.Concat(leftLung.Concat(rightLung)); ;
            }
        }

        public class AbdomenContainer : DefaultContainer
        {
            public Infection GastrointestinalTract;
            public Infection Liver;
            public Infection Spleen;
            public Infection Pancreas;
            public Infection LeftKidney;
            public Infection RightKidney;
            public Infection Bladder;
            public Infection Reproductives;

            public override IEnumerable<Infection> GetInfections()
            {
                return new Infection[]
                {
                    GastrointestinalTract,
                    Liver,
                    Spleen,
                    Pancreas,
                    LeftKidney,
                    RightKidney,
                    Bladder,
                    Reproductives,
                }.Concat(base.GetInfections());
            }
        }
        public class AccessPointsContainer
        {
            /* NOTES:
             * These do not directly contribute to patient health but have an increased risk of infecting their associated systems (e.g) IVs - Blood, Shunt - Brain, Catheter - bladder, Airway - Lung
             * Treating the Patient does not treat the Access Point unless the treatment is directly given through that AccessPoint 
             */

            public Dictionary<IVTargetLocation, Infection> IVs = new Dictionary<IVTargetLocation, Infection>()
            {
                {IVTargetLocation.ArmLeft, new Infection() },
                {IVTargetLocation.ArmRight, new Infection() },
                {IVTargetLocation.LegLeft, new Infection() },
                {IVTargetLocation.LegRight, new Infection() },
                {IVTargetLocation.CentralLine, new Infection() },
            };

            public Infection ArtificialAirway;

            public Infection CerebralShunt;

            public Infection UrinaryCatheter;

            public IEnumerable<Infection> GetInfections()
            {
                return new Infection[]{
                    IVs[IVTargetLocation.ArmLeft],
                    IVs[IVTargetLocation.ArmRight],
                    IVs[IVTargetLocation.LegLeft],
                    IVs[IVTargetLocation.LegRight],
                    IVs[IVTargetLocation.CentralLine],
                    CerebralShunt,
                    ArtificialAirway,
                    UrinaryCatheter,
                };
            }
        }
        #endregion
    }
}

