﻿namespace PatientManagementSystem.Patients.PatientBones
{
    public struct Bone
    {
        public readonly string name;

        public bool isBroken { get; set; }
        public bool isMisaligned { get; set; }
        public bool isImpingingVessel { get; set; }
        public bool isFused { get; set; } //May remove

        public Bone(string name)
        {
            this.name = name;
            isBroken = false;
            isMisaligned = false;
            isImpingingVessel = false;
            isFused = false;
        }
        public Bone(string name, bool isBroken, bool isMisaligned, bool isImpingingVessel, bool isFused)
        {
            this.name = name;
            this.isBroken = isBroken;
            this.isMisaligned = isMisaligned;
            this.isImpingingVessel = isMisaligned ? isImpingingVessel : false; //Vessel can only be impinged if bone is misaligned
            this.isFused = isFused;
        }
    }
   
}


