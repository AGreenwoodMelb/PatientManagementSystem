﻿#region URGENTS

#endregion

#region TODOS
//TODO: (Started?) Rebuild OrganState enum, perhaps use a float to indicate organ health? another float for OrganFunctionEffectiveness (nice short variable name there) 
//TODO: Reorganise files and class locations
#endregion

#region REVIEWS
//REVIEW: Consider scrapping that awful HeartStructures/ HeartVessels crap
#endregion

#region LATERS
//LATER: Implement Pain System
//LATER: Rebuild the Fluid system to be based on values and not ratios (unless appropriate) That PatientFluid.cs is looking thinner than a russian supermodel. Oh look a struct, lets see how long that lasts...
//LATER: Implement all the blood tests and background variables
//LATER: Break Vessels(Arteries?) into its own System mirroring Infections
//LATER: Rebuild the Bleeding system to mirror the infections system?
//LATER: Rebuild the Bones System to mirror the Infections system
//LATER: Rebuild Lung System because its trash
//LATER: Implement AscultateLungs, AscultateLung, AsculateLobe for breath sounds - enum BreathSounds Normal, Wheeze, Bubbling, None. if RespRate = 0 then BreathSounds = none
#endregion
