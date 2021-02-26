﻿using System;
using PatientManagementSystem.Patients;
using PatientManagementSystem.Patients.PatientDrugs;
using PatientManagementSystem.Patients.PatientInfection;
using PatientManagementSystem.Patients.PatientAccessPoints;
using PatientManagementSystem.Patients.PatientDefaults;

namespace PatientManagementSystem.Patients.PatientInterventions
{
    public abstract class PatientIntervention
    {
        public float WasteProduced { get; protected set; }
        public abstract bool Intervene(Patient patient, out bool Succeeded);
    }

    #region Fluids?
    public class Transfuse : PatientIntervention
    {
        private Fluid _fluid;
        private Patient _patient;
        public Transfuse(Fluid incFluid)
        {
            WasteProduced = 0; //LATER: Replace with relevant value from a defaults file.
            _fluid = incFluid;
        }

        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            _patient = patient;
            bool output = DetermineTransfusion();
            Succeeded = false;
            return output;
        }

        private bool DetermineTransfusion()
        {
            switch (_fluid)
            {
                case Blood _:
                    return TranfuseBlood();
                case Fluid _:
                    return false;
                default:
                    throw new ArgumentException(
                         message: $"BloodSystem::Transfuse Unhandled Subtype of Fluid: {nameof(_fluid)}"
                         );
            }
        }//Redo using IVAccess Object. This whole thing is spiralling

        private bool TranfuseBlood()
        {
            Blood incBlood = (Blood)_fluid;

            if (_patient.Body.Blood.BloodTypeCompatibility(incBlood.bloodType))
            {
                Console.WriteLine("Blood Transfusion Compatible"); //Temp
            }
            else
            {
                //Handle Transfusion reaction here
                _patient.Body.Blood.HasTransfusionReaction = true;
                Console.WriteLine("Blood Transfusion Incompatible");
            }
            return _patient.Body.Blood.AddFluid(_fluid);
        }
    }
    public class AdministerDrug : PatientIntervention //Maybe redo? Or Should this represent an IM or bolus via IVAccess
    {
        private Drug _drug;
        public AdministerDrug(Drug drug)
        {
            WasteProduced = 0; //LATER: Replace with relevant value from a defaults file.
            _drug = drug;
        }

        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            _drug.Administer(patient);
            Succeeded = false;
            //throw new NotImplementedException(message: "AdministerDrug:: No Intervene method implemented");
            return true;
        }
    }
    #endregion

    #region ManageAccessPoints
    #region IVs
    public class InsertIV : PatientIntervention
    {
        private IVTargetLocation _target;

        public InsertIV(IVTargetLocation target)
        {
            _target = target;
        }

        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;
            if (patient.AccessPoints.IVs[_target].IsInserted) //Cant Insert
                return false;
          
            float successThreshold = (_target == IVTargetLocation.CentralLine) ? DefaultPlayerStatsTEMP.InsertCentralLineSuccess : DefaultPlayerStatsTEMP.InsertIVSuccess;
            float infectionThreshold = (_target == IVTargetLocation.CentralLine) ? DefaultInfectionValues.InsertCentralLine : DefaultInfectionValues.InsertIV;
            WasteProduced = (_target == IVTargetLocation.CentralLine) ? DefaultWasteProduction.InsertCentralLine : DefaultWasteProduction.InsertIV;

            if (patient.MagicRandomSeed > successThreshold) //Failed to Insert
            {
                //LATER: Increase patient CurrentPain counter by DefaultPainCaused.InsertIV;
                if (patient.MagicRandomSeed > infectionThreshold)
                {
                    patient.Body.Infections.AccessPoints.IVs[_target].Infect(new Infection()); //LATER: PLACEHOLDER INFECTION Create region and organ based look-up tables for to determine infection Type 
                }
                patient.AccessPoints.IVs[_target].IsInserted = true;
                Succeeded = true;
            }
            else
            {
                //Additional pain caused in failure?
                WasteProduced += (_target == IVTargetLocation.CentralLine) ? DefaultWasteProduction.RemoveCentralLine : DefaultWasteProduction.RemoveIV;
            }
            return true;
        }
    }
    public class RemoveIV : PatientIntervention
    {
        private IVTargetLocation _target;

        public RemoveIV(IVTargetLocation target)
        {
            _target = target;
        }

        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;
            if (!patient.AccessPoints.IVs[_target].IsInserted)
                return false;

            WasteProduced = (_target == IVTargetLocation.CentralLine) ? DefaultWasteProduction.RemoveCentralLine : DefaultWasteProduction.RemoveIV;
            patient.Body.Infections.AccessPoints.IVs[_target].CureInfection();
            patient.AccessPoints.IVs[_target].Remove();
            Succeeded = true;
            return true;
        }
    }
    #endregion
    #region Airways
    public class InsertArtificalAirway : PatientIntervention
    {
        private ArtificialAirwayType _artificialAirway;
        public InsertArtificalAirway(ArtificialAirwayType artificialAirway)
        {
            _artificialAirway = artificialAirway;
        }

        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;
            if (patient.AccessPoints.ArtificialAirway.IsInserted)
                return false;

            WasteProduced = DefaultWasteProduction.InsertAirway[_artificialAirway];
            if (patient.MagicRandomSeed > DefaultPlayerStatsTEMP.AirwayInsertionSuccess[_artificialAirway])
            {
                if (patient.MagicRandomSeed > DefaultInfectionValues.InsertAirway[_artificialAirway])
                {
                    patient.Body.Infections.AccessPoints.ArtificialAirway.Infect(new Infection()); //REVIEW: PLACEHOLDER INFECTION
                }
                patient.AccessPoints.ArtificialAirway.Insert( _artificialAirway);
                Succeeded = true;
            }
            else
            {
                WasteProduced += DefaultWasteProduction.RemoveAirway[_artificialAirway];
            }
            return true;
        }
    }
    public class RemoveArtificialAirway : PatientIntervention
    {
        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;
            if (!patient.AccessPoints.ArtificialAirway.IsInserted)
                return false;

            WasteProduced = DefaultWasteProduction.RemoveAirway[patient.AccessPoints.ArtificialAirway.AirwayType];
            patient.Body.Infections.AccessPoints.ArtificialAirway.CureInfection();
            patient.AccessPoints.ArtificialAirway.Remove();
            Succeeded = true;
            return true;
        }
    }
    #endregion
    #region Catheter
    public class InsertUrinaryCatheter : PatientIntervention
    {
        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;
            if (patient.AccessPoints.UrinaryCatheter.IsInserted || patient.Body.Abdomen.UrinaryTract.Bladder.IsUrethraBlocked)//Cant insert
                return false;

            WasteProduced = DefaultWasteProduction.InsertUrinaryCatheter;
            if (patient.MagicRandomSeed > DefaultPlayerStatsTEMP.InsertUrinaryCatheterSuccess) //Failed to insert
            {
                if (patient.MagicRandomSeed > DefaultInfectionValues.InsertUrinaryCatheter)
                {
                    patient.Body.Infections.AccessPoints.UrinaryCatheter.Infect(new Infection()); //REVIEW: PLACEHOLDER INFECTION
                }
                patient.AccessPoints.UrinaryCatheter.IsInserted = true;
                Succeeded = true;
            }
            else
            {
                WasteProduced += DefaultWasteProduction.RemoveUrinaryCatheter;
            }
            return true;
        }
    }
    public class RemoveUrinaryCatheter : PatientIntervention
    {
        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;
            if (!patient.AccessPoints.UrinaryCatheter.IsInserted)
                return false;

            WasteProduced += DefaultWasteProduction.RemoveUrinaryCatheter;
            patient.Body.Infections.AccessPoints.UrinaryCatheter.CureInfection();
            patient.AccessPoints.UrinaryCatheter.Remove();
            Succeeded = true;
            return true;
        }
    }
    #endregion
    #region CerebralShunt
    public class InsertCerebralShunt : PatientIntervention
    {
        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;
            if (patient.AccessPoints.CerebralShunt.IsInserted)
                return false;

            WasteProduced = DefaultWasteProduction.InsertCerebralShunt;
            if (patient.MagicRandomSeed > DefaultPlayerStatsTEMP.InsertCerebralShuntSuccess)
            {
                if (patient.MagicRandomSeed > DefaultInfectionValues.InsertCerebralShunt)
                {
                    patient.Body.Infections.Head.Brain.Infect(new Infection()); //REVIEW: PLACEHOLDER INFECTION
                }
                patient.AccessPoints.CerebralShunt.IsInserted = true;
                Succeeded = true;
            }
            else
            {
                WasteProduced += DefaultWasteProduction.RemoveCerebralShunt;
            }
            return true;
        }
    }
    public class RemoveCerebralShunt : PatientIntervention
    {
        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;
            if (!patient.AccessPoints.CerebralShunt.IsInserted)
                return false;

            WasteProduced = DefaultWasteProduction.RemoveCerebralShunt;
            patient.Body.Infections.AccessPoints.CerebralShunt.CureInfection();
            patient.AccessPoints.CerebralShunt.Remove();
            Succeeded = true;
            return true;
        }
    }
    #endregion
    #endregion

    public class LumbarPuncture : PatientIntervention //TODO: Finish implementing and testing 
    {
        public override bool Intervene(Patient patient, out bool Succeeded)
        {
            Succeeded = false;

            /*
            if (condition)
                return false;
            */

            //Lumbar Puncture. Pain and greater risk of causing CNS infection, High chance to fail

            WasteProduced = DefaultWasteProduction.PerformLumbarPuncture;

            if (patient.MagicRandomSeed > DefaultPlayerStatsTEMP.PerformLumbarPunctureSuccess)
            {
                Succeeded = true;
            }

            if (patient.MagicRandomSeed > DefaultInfectionValues.PerformLumbarPuncture)
            {
                patient.Body.Infections.Head.Brain.Infect(new Infection());
            }
            return true;
        }
    }
}
