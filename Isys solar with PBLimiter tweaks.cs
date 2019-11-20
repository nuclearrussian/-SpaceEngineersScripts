
// Isy's Solar Alignment Script
// ============================
// Version: 4.3.1
// Date: 2019-10-13

// =======================================================================================
//                                                                            --- Configuration ---
// =======================================================================================

// --- Essential Configuration ---
// =======================================================================================

// Name of the group with all the solar related rotors (not needed in gyro mode)
string rotorGroupName = "Solar Rotors";

// By enabling gyro mode, the script will no longer use rotors but all gyroscopes on the grid instead.
// This mode only makes sense when used on a SHIP in SPACE. Gyro mode deactivates the following
// features: night mode, rotate to sunrise, time calculation and triggering external timer blocks.
bool useGyroMode = false;

// Name of the reference group for gyro mode. Put your main cockpit, flight seat or remote control in this group!
string referenceGroupName = "Solar Reference";


// --- Rotate to sunrise ---
// =======================================================================================

// Rotate the panels towards the sunrise during the night? (Possible values: true | false, default: true)
// The angle is figured out automatically based on the first lock of the day.
// If you want to set the angles yourself, set manualAngle to true and adjust the angles to your likings.
bool rotateToSunrise = false;
bool manualAngle = false;
int manualAngleVertical = 0;
int manualAngleHorizontal = 0;


// --- Power fallback ---
// =======================================================================================

// With this option, you can enable your reactors and hydrogen engines as a safety fallback, if not enough power is available
// to power all your machines or if the battery charge gets low. By default, all reactors and hydrogen engines
// on the same grid will be used. If you only want to use specific ones, put their names or group in the list.
// Example: string[] fallbackDevices = { "Small Reactor 1", "Base reactor group", "Hydrogen Engine" };
bool useReactorFallback = true;
bool useHydrogenEngineFallback = false;
string[] fallbackDevices = { };

// Activation order
// By default, the hydrogen engine will be turned on first and the reactors after that if still not enough power is available.
// Set this value to false, and the reactors will be used first.
bool activateHydrogenEngineFirst = false;

// Activation on low battery?
// The fallback devices will be active until 'turnOffAtPercent' of the max battery charge after it was turned on at 'turnOnAtPercent'.
bool activateOnLowBattery = true;
double turnOnAtPercent = 15;
double turnOffAtPercent = 30;

// Activate on overload?
// If the combined output of batteries, solar panels and wind turbines is more than 'overloadPercentage' of their max output, the fallback devices will be turned on.
bool activateOnOverload = true;
double overloadPercentage = 50;


// --- Base Light Management ---
// =======================================================================================

// Enable base light management? (Possible values: true | false, default: false)
// Lights will be turned on/off based on daytime.
bool baseLightManagement = false;

// Simple mode: toggle lights based on max. solar output (percentage). Time based toggle will be deactivated.
bool simpleMode = false;
int simpleThreshold = 50;

// Define the times when your lights should be turned on or off. If simple mode is active, this does nothing.
int lightOffHour = 8;
int lightOnHour = 18;

// To only toggle specific lights, declare groups for them.
// Example: string[] baseLightGroups = { "Interior Lights", "Spotlights", "Hangar Lights" };
string[] baseLightGroups = { };


// --- LCD panels ---
// =======================================================================================

// To display the main script informations, add the following keyword to any LCD name (default: !ISA-main).
// You can enable or disable specific informations on the LCD by editing its custom data.
string mainLcdKeyword = "!ISA-main";

// To display compact stats (made for small screens, add the following keyword to any LCD name (default: !ISA-compact).
string compactLcdKeyword = "!ISA-compact";

// To display all current warnings and problems, add the following keyword to any LCD name (default: !ISA-warnings).
string warningsLcdKeyword = "!ISA-warnings";

// To display the script performance, add the following keyword to any LCD name (default: !ISA-performance).
string performanceLcdKeyword = "!ISA-performance";

// Default font ("Debug" or "Monospace") and fontsize for new LCDs
string defaultFont = "Debug";
float defaultFontSize = 0.6f;


// --- Terminal statistics ---
// =======================================================================================

// The script can display informations in the names of the used blocks. The shown information is a percentage of
// the current efficiency (solar panels and oxygen farms) or the fill level (batteries and tanks).
// You can enable or disable single statistics or disable all using the master switch below.
bool enableTerminalStatistics = false;

bool showSolarStats = true;
bool showWindTurbineStats = true;
bool showBatteryStats = true;
bool showOxygenFarmStats = true;
bool showOxygenTankStats = true;


// --- External timer blocks ---
// =======================================================================================

// Trigger external timer blocks at specific events? (action "Start" will be applied which takes the delay into account)
// Events can be: "sunrise", "sunset", a time like "15:00" or a number for every X seconds
// Every event needs a timer block name in the exact same order as the events.
// Calling the same timer block with multiple events requires it's name multiple times in the timers list!
// Example:
// string[] events = { "sunrise", "sunset", "15:00", "30" };
// string[] timers = { "Timer 1", "Timer 1", "Timer 2", "Timer 3" };
// This will trigger "Timer 1" at sunrise and sunset, "Timer 2" at 15:00 and "Timer 3" every 30 seconds.
bool triggerTimerBlock = false;
string[] events = { };
string[] timers = { };


// --- Settings for enthusiasts ---
// =======================================================================================

// Change percentage of the last locked output where the script should realign for a new best output (default: 1, gyro: 5)
double realginPercentageRotor =5;
double realignPercentageGyro = 5;

// Percentage of the max detected output where the script starts night mode (default: 10)
double nightPercentage = 10;

// Percentage of the max detected output where the script detects night for time calculation (default: 50)
double nightTimePercentage = 50;

// Rotor speeds (speeds are automatically scaled between these values based on the output)
const float rotorMinSpeed = 0.05f;
const float rotorMaxSpeed = 1.0f;

// Rotor options
float rotorTorqueLarge = 33600000f;
float rotorTorqueSmall = 448000f;
bool setInertiaTensor = true;
bool setRotorLockWhenStopped = false;

// Min gyro RPM, max gyro RPM and gyro power for gyro mode
const double minGyroRPM = 0.1;
const double maxGyroRPM = 1;
const float gyroPower = 1f;


// =======================================================================================
//                                                                      --- End of Configuration ---
//                                                        Don't change anything beyond this point!
// =======================================================================================

int upcnt=0;
float Ǩ=0;float ǩ=0;float Ǫ=0;float ǫ=0;float Ǭ=0;float ǭ=0;List<IMyMotorStator>Ǯ=new List<IMyMotorStator>();List<
IMyMotorStator>ǯ=new List<IMyMotorStator>();List<IMyMotorStator>ǰ=new List<IMyMotorStator>();List<IMyGyro>Ǳ=new List<IMyGyro>();List<
IMyTextPanel>ǲ=new List<IMyTextPanel>();List<IMyTerminalBlock>ǳ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>Ǵ=new List<
IMyTerminalBlock>();List<IMyTerminalBlock>ǵ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>Ƕ=new List<IMyTerminalBlock>();List<
IMyInteriorLight>ǧ=new List<IMyInteriorLight>();List<IMyReflectorLight>ǒ=new List<IMyReflectorLight>();List<IMyPowerProducer>Ǜ=new List<
IMyPowerProducer>();int Ǔ=0;int ǔ=0;int Ǖ=0;int ǖ=0;List<IMySolarPanel>Ǘ=new List<IMySolarPanel>();int ǘ=0;bool Ǚ=false;bool ǚ=false;int
ǜ=30;int ǥ=90;int ǝ=10;bool Ǟ=true;List<string>ǟ=new List<string>{"output=0","outputLast=0","outputLocked=0",
"outputMax=0","outputMaxAngle=0","outputMaxDayBefore=0","outputBestPanel=0","direction=1","directionChanged=0","directionTimer=0",
"allowRotation=1","rotationDone=1","timeSinceRotation=0","firstLockOfDay=0","sunriseAngle=0"};List<IMyShipController>Ǡ=new List<
IMyShipController>();double ǡ=0;double Ǣ=0;double ǣ=0;double Ǥ=1;double Ǧ=1;double Ǒ=1;bool ǐ=false;bool Ƒ=false;bool Ɣ=false;double ƕ=0;
double Ɩ=0;double Ɨ=0;bool Ƙ=true;bool ƙ=true;bool ƚ=true;double ƛ=0;double Ɯ=0;double Ɲ=0;List<IMyBatteryBlock>ƞ=new List<
IMyBatteryBlock>();float Ɵ=0;float Ơ=0;float ơ=0;float Ƣ=0;float ƣ=0;float Ƥ=0;List<IMyOxygenFarm>ƥ=new List<IMyOxygenFarm>();List<
IMyGasTank>Ʀ=new List<IMyGasTank>();double ƨ=0;double ƒ=0;double Ž=0;int Ɔ=0;List<IMyPowerProducer>ž=new List<IMyPowerProducer>();
float ſ=0;float ƀ=0;string Ɓ="0 kW";string Ƃ="0 kW";string ƃ="0 kW";string Ƅ="0 kW";string ƅ="0 kW";string Ƈ="0 kW";string Ɛ=
"0 kW";string ƈ="0 kW";string Ɖ="0 kW";string Ɗ="0 kW";string Ƌ="0 kW";string ƌ="0 L";string ƍ="0 L";string Ǝ=
"Checking setup...";string Ə;string Ƨ;string[]Ʃ={"/","-","\\","|"};int ǎ=0;int Ƽ=0;int ƽ=270;const int ƾ=7200;int ƿ=ƾ;const int ǀ=ƾ/2;int ǁ
=ǀ;string[]Ŗ={"showHeading=true","showWarnings=true","showCurrentOperation=true","showSolarStats=true",
"showTurbineStats=true","showBatteryStats=true","showOxygenStats=true","showLocationTime=true","scrollTextIfNeeded=true"};string[]ǂ={
"showSolarStats=false","showTurbineStats=false","showBatteryStats=false","showOxygenStats=false","showLocationTime=false","showRealTime=false"
,"scrollTextIfNeeded=false"};string[]ǃ={"showHeading=true","scrollTextIfNeeded=true"};bool Ǆ=false;bool ǅ=false;bool ǆ=
false;int Ǉ=0;int ǈ=0;double ǉ=0;string Ǌ="";string Ċ;int ǋ=0;bool ǌ=false;HashSet<string>Ǎ=new HashSet<string>();HashSet<
string>Ǐ=new HashSet<string>();string ƻ="align";int ƪ=3;bool Ƴ=false;string ƫ="both";bool Ƭ=false;double ƭ=0;double Ʈ=0;int Ư=
0;int ư=0;int Ʊ=1;bool Ʋ=true;bool ƴ=true;int ƺ=0;string[]Ƶ={"Get blocks","Get block stats","Time Calculation",
"Rotation Logic","Reactor Fallback"};Program(){Ö();realginPercentageRotor=(realginPercentageRotor%100)/100;realignPercentageGyro=(
realignPercentageGyro%100)/100;nightPercentage=(nightPercentage%100)/100;nightTimePercentage=(nightTimePercentage%100)/100;Runtime.
UpdateFrequency=UpdateFrequency.Update1;}void Main(string ƶ){upcnt++;if (upcnt % 30 != 0) return; if(ǋ>=10){throw new Exception(
"Too many errors. Please recompile!\n\nScript stoppped!\n");}try{Š("",true);if(ƶ!=""){ƻ=ƶ.ToLower();Ʊ=3;}if(Ʋ){Ƹ();Ȕ(false);ª();Ʋ=false;}if(ư<Ư){ư++;return;}if(ƴ){if(ƺ==0)Ć();if(
ƺ==1)č();if(ƺ==2)ć();if(ƺ==3)ċ();if(ƺ>3)ƺ=0;ƴ=false;return;}if(Ʊ==0||ǌ){Ƹ();ǌ=false;if(Ʊ==0){Š(Ƶ[Ʊ]);Z();Ʊ++;}return;}ư=0
;ƴ=true;if(Ʊ==1){Ɋ();}if(Ʊ==2&&!useGyroMode){a();if(baseLightManagement)F();if(triggerTimerBlock)S();}if(Ʊ==3){if(!ȯ(ƻ)){
if(useGyroMode){ɀ();}else{Ʉ();}}}if(Ʊ==4){if(useReactorFallback||useHydrogenEngineFallback)J();foreach(var Ă in Ǯ){double
Ʒ=ɇ(Ă,"output");Ɉ(Ă,"outputLast",Ʒ);}ǫ=Ǫ;Save();}Š(Ƶ[Ʊ]);Z();if(Ʊ>=4){Ʊ=0;Ǎ=new HashSet<string>(Ǐ);Ǐ.Clear();if(ǋ>0)ǋ--;
if(Ǎ.Count==0)Ċ=null;Ƨ=Ə;Ə="";}else{Ʊ++;}ǎ=ǎ>=3?0:ǎ+1;}catch(NullReferenceException){ǋ++;ǌ=true;R(
"Execution of script step aborted:\n"+Ƶ[Ʊ]+" (ID: "+Ʊ+")\n\nCached block not available..");}catch(Exception e){ǋ++;ǌ=true;R(
"Critical error in script step:\n"+Ƶ[Ʊ]+" (ID: "+Ʊ+")\n\n"+e);}}void Ƹ(){if(Ŧ==null){Ũ(Me.CubeGrid);}if(!useGyroMode){var ƹ=GridTerminalSystem.
GetBlockGroupWithName(rotorGroupName);if(ƹ!=null){ƹ.GetBlocksOfType<IMyMotorStator>(Ǯ);if(Ǯ.Count==0){R(
"There are no rotors in the rotor group:\n'"+rotorGroupName+"'");}}else{R("Rotor group not found:\n'"+rotorGroupName+"'");}HashSet<IMyCubeGrid>Ɠ=new HashSet<
IMyCubeGrid>();foreach(var Ă in Ǯ){if(!Ă.IsFunctional)R("'"+Ă.CustomName+"' is broken!\nRepair it to use it for aligning!");if(!Ă.
Enabled)R("'"+Ă.CustomName+"' is turned off!\nTurn it on to use it for aligning!");if(!Ă.IsAttached)R("'"+Ă.CustomName+
"' has no rotor head!\nAdd one to use it for aligning!");Ɠ.Add(Ă.CubeGrid);if(Ă.CubeGrid.GridSize==0.5){Ă.Torque=rotorTorqueSmall;}else{Ă.Torque=rotorTorqueLarge;}if(Ă.
GetOwnerFactionTag()!=Me.GetOwnerFactionTag()){R("'"+Ă.CustomName+
"' has a different owner / faction!\nAll blocks should have the same owner / faction!");}}ǯ.Clear();ǰ.Clear();foreach(var Ă in Ǯ){if(Ɠ.Contains(Ă.TopGrid)){ǯ.Add(Ă);}else{ǰ.Add(Ă);if(Ă.CubeGrid!=Ŧ&&
setInertiaTensor){try{Ă.SetValueBool("ShareInertiaTensor",true);}catch(Exception){}}}}List<IMyMotorStator>ȶ=new List<IMyMotorStator>();ȶ
.AddRange(ǰ);ǰ.Clear();bool ȷ;foreach(var ȸ in ȶ){ȷ=true;foreach(var Ă in ǰ){if(Ă.TopGrid==ȸ.TopGrid){ȸ.RotorLock=false;ȸ
.TargetVelocityRPM=0f;ȸ.Torque=0f;ȸ.BrakingTorque=0f;ȷ=false;break;}}if(ȷ)ǰ.Add(ȸ);}Ǘ.Clear();ƥ.Clear();foreach(var Ǿ in
ǰ){Ǩ=0;ǩ=0;Ŏ(Ǿ.TopGrid,true);var ȹ=new List<IMySolarPanel>();GridTerminalSystem.GetBlocksOfType<IMySolarPanel>(ȹ,Ⱥ=>ō.
Contains(Ⱥ.CubeGrid)&&Ⱥ.IsWorking);var Ȼ=new List<IMyOxygenFarm>();GridTerminalSystem.GetBlocksOfType<IMyOxygenFarm>(Ȼ,ȼ=>ō.
Contains(ȼ.CubeGrid)&&ȼ.IsWorking);foreach(var ä in ȹ){Ǘ.Add(ä);Ǩ+=ä.MaxOutput;if(ä.MaxOutput>ǩ)ǩ=ä.MaxOutput;}foreach(var Ô in
Ȼ){ƥ.Add(Ô);Ǩ+=Ô.GetOutput();if(Ô.GetOutput()>ǩ)ǩ=Ô.GetOutput();}if(ȹ.Count==0&&Ȼ.Count==0){R("'"+Ǿ.CustomName+
"' can't see the sun!\nAdd a solar panel or oxygen farm to it!");}Ɉ(Ǿ,"output",Ǩ);Ɉ(Ǿ,"outputBestPanel",ǩ);if(Ǩ>ɇ(Ǿ,"outputMax")){Ɉ(Ǿ,"outputMax",Ǩ);Ɉ(Ǿ,"outputMaxAngle",ā(Ǿ));}}
foreach(var ȓ in ǯ){double Ȉ=0;ǩ=float.MaxValue;foreach(var Ǿ in ǰ){if(Ǿ.CubeGrid==ȓ.TopGrid){Ȉ+=ɇ(Ǿ,"output");if(ɇ(Ǿ,
"outputBestPanel")<ǩ)ǩ=(float)ɇ(Ǿ,"outputBestPanel");}}Ɉ(ȓ,"output",Ȉ);Ɉ(ȓ,"outputBestPanel",ǩ);if(Ȉ>ɇ(ȓ,"outputMax")){Ɉ(ȓ,"outputMax",Ȉ)
;Ɉ(ȓ,"outputMaxAngle",ā(ȓ));}}}if(useGyroMode){if(Me.CubeGrid.IsStatic){R(
"The grid is stationary!\nConvert it to a ship in the Info tab!");}var Ƚ=GridTerminalSystem.GetBlockGroupWithName(referenceGroupName);if(Ƚ!=null){Ƚ.GetBlocksOfType<IMyShipController>(Ǡ
);if(Ǡ.Count==0){R("There are no cockpits, flight seats or remote controls in the reference group:\n'"+referenceGroupName
+"'");}}else{R("Reference group not found!\nPut your main cockpit, flight seat or remote control in a group called '"+
referenceGroupName+"'!");}GridTerminalSystem.GetBlocksOfType<IMyGyro>(Ǳ,Ȩ=>Ȩ.IsSameConstructAs(Me)&&Ȩ.IsWorking);if(Ǳ.Count==0){R(
"No gyroscopes found!\nAre they enabled and completely built?");}GridTerminalSystem.GetBlocksOfType<IMySolarPanel>(Ǘ,Ⱥ=>Ⱥ.IsSameConstructAs(Me)&&Ⱥ.IsWorking);GridTerminalSystem.
GetBlocksOfType<IMyOxygenFarm>(ƥ,ȼ=>ȼ.IsSameConstructAs(Me)&&ȼ.IsWorking);}if(ǘ!=Ǘ.Count||Ɔ!=ƥ.Count){foreach(var Ă in Ǯ){Ɉ(Ă,
"outputMax",0);}Ǭ=0;ǘ=Ǘ.Count;Ɔ=ƥ.Count;R("Amount of solar panels or oxygen farms changed!\nRestarting..");}if(Ǘ.Count==0&&ƥ.Count
==0){R("No solar panels or oxygen farms found!\nHow should I see the sun now?");}ƞ.Clear();GridTerminalSystem.
GetBlocksOfType<IMyBatteryBlock>(ƞ,ũ=>ũ.IsSameConstructAs(Me)&&ũ.IsWorking);if(ƞ.Count==0){R(
"No batteries found!\nDon't you want to store your Power?");}ž.Clear();GridTerminalSystem.GetBlocksOfType<IMyPowerProducer>(ž,ȵ=>ȵ.BlockDefinition.TypeIdString.Contains(
"WindTurbine")&&ȵ.IsSameConstructAs(Me)&&ȵ.IsWorking);Ʀ.Clear();GridTerminalSystem.GetBlocksOfType<IMyGasTank>(Ʀ,ȵ=>!ȵ.
BlockDefinition.SubtypeId.Contains("Hydrogen")&&ȵ.IsSameConstructAs(Me)&&ȵ.IsWorking);if(useReactorFallback||useHydrogenEngineFallback)
{Ǜ.Clear();foreach(var P in fallbackDevices){var Ȫ=GridTerminalSystem.GetBlockGroupWithName(P);if(Ȫ!=null){var ȧ=new List
<IMyPowerProducer>();Ȫ.GetBlocksOfType<IMyPowerProducer>(ȧ,Ȩ=>Ȩ.BlockDefinition.TypeIdString.Contains("Reactor")||Ȩ.
BlockDefinition.TypeIdString.Contains("HydrogenEngine"));Ǜ.AddRange(ȧ);}else{IMyPowerProducer ȩ=GridTerminalSystem.GetBlockWithName(P)
as IMyPowerProducer;if(ȩ!=null){Ǜ.Add(ȩ);}else{R("Power fallback device not found:\n'"+P+"'");}}}if(Ǜ.Count==0){
GridTerminalSystem.GetBlocksOfType<IMyPowerProducer>(Ǜ,Ȩ=>(Ȩ.BlockDefinition.TypeIdString.Contains("Reactor")||Ȩ.BlockDefinition.
TypeIdString.Contains("HydrogenEngine"))&&Ȩ.IsSameConstructAs(Me)&&Ȩ.IsFunctional);}if(!useReactorFallback)Ǜ.RemoveAll(Ȩ=>Ȩ.
BlockDefinition.TypeIdString.Contains("Reactor"));if(!useHydrogenEngineFallback)Ǜ.RemoveAll(Ȩ=>Ȩ.BlockDefinition.TypeIdString.Contains(
"HydrogenEngine"));Ǉ=Ǜ.Count(Ȩ=>Ȩ.BlockDefinition.TypeIdString.Contains("Reactor"));ǈ=Ǜ.Count(Ȩ=>Ȩ.BlockDefinition.TypeIdString.Contains
("HydrogenEngine"));}if(baseLightManagement){ǧ.Clear();ǒ.Clear();if(baseLightGroups.Length>0){var ȫ=new List<
IMyInteriorLight>();var ȳ=new List<IMyReflectorLight>();foreach(var Ȭ in baseLightGroups){var ȭ=GridTerminalSystem.GetBlockGroupWithName
(Ȭ);if(ȭ!=null){ȭ.GetBlocksOfType<IMyInteriorLight>(ȫ);ǧ.AddRange(ȫ);ȭ.GetBlocksOfType<IMyReflectorLight>(ȳ);ǒ.AddRange(ȳ
);}else{R("Light group not found:\n'"+Ȭ+"'");}}}else{GridTerminalSystem.GetBlocksOfType<IMyInteriorLight>(ǧ,Ȯ=>Ȯ.
IsSameConstructAs(Me));GridTerminalSystem.GetBlocksOfType<IMyReflectorLight>(ǒ,Ȯ=>Ȯ.IsSameConstructAs(Me));}}ǳ=ŕ(mainLcdKeyword,Ŗ);Ǵ=ŕ(
compactLcdKeyword,ǂ);ǵ=ŕ(warningsLcdKeyword,ǃ);Ƕ=ŕ(performanceLcdKeyword,ǃ);}bool ȯ(string Ȱ){bool ȱ=true;if(Ȱ=="pause"){Ȕ();if(Ƴ){ƻ=
"align";Ƴ=false;return false;}else{ƻ="paused";Ƴ=true;}Ǝ="Automatic alignment paused.\n";Ǝ+="Run 'pause' again to continue..";}
else if(Ȱ=="paused"){Ǝ="Automatic alignment paused.\n";Ǝ+="Run 'pause' again to continue..";}else if(Ȱ=="realign"&&!
useGyroMode){ȝ();Ǝ="Forced realign by user.\n";Ǝ+="Searching highest output for "+ǥ+" more seconds.";if(ǥ==0){ƻ="";ǥ=90;}else{ǥ-=1;
}}else if(Ȱ=="reset"&&!useGyroMode){Ƽ=0;ƽ=270;ǁ=ǀ;ƿ=ƾ;Ǝ="Calculated time resetted.\n";Ǝ+="Continuing in "+ƪ+" seconds.";
if(ƪ==0){ƻ="";ƪ=3;}else{ƪ-=1;}}else if(Ȱ.Contains("rotate")&&!useGyroMode){String[]Ȳ=Ȱ.Split(' ');bool Ⱦ=false;ƫ="both";Ƭ=
false;if(Ȳ[0].Contains("pause"))Ƭ=true;if(Ȳ.Length==2){if(Ȳ[1].Contains("h")){Ⱦ=Double.TryParse(Ȳ[1].Replace("h",""),out ƭ);ƫ
="horizontalOnly";}else if(Ȳ[1].Contains("v")){Ⱦ=Double.TryParse(Ȳ[1].Replace("v",""),out Ʈ);ƫ="verticalOnly";}if(Ⱦ){Ǝ=
"Checking rotation parameters...";ƻ="rotNormal";}else{Ȕ();R("Wrong format!\n\nShould be (e.g. 90 degrees):\nrotate h90 OR\nrotate v90");}}else if(Ȳ.
Length==3){string ȴ="rotNormal";if(Ȳ[1].Contains("v")){Ⱦ=Double.TryParse(Ȳ[1].Replace("v",""),out Ʈ);if(Ⱦ)Ⱦ=Double.TryParse(Ȳ[
2].Replace("h",""),out ƭ);ȴ="rotVH1";}else{Ⱦ=Double.TryParse(Ȳ[1].Replace("h",""),out ƭ);if(Ⱦ)Ⱦ=Double.TryParse(Ȳ[2].
Replace("v",""),out Ʈ);}if(Ⱦ){Ǝ="Checking rotation parameters...";ƻ=ȴ;}else{Ȕ();R(
"Wrong format!\n\nShould be (e.g. 90 degrees):\nrotate h90 v90 OR\nrotate v90 h90");}}else{Ȕ();R("Not enough parameters!\n\nShould be 2 or 3:\nrotate h90 OR\nrotate h90 v90");}}else if(Ȱ=="rotNormal"){Ǝ
="Rotating to user defined values...";bool ȃ=Ȣ(ƫ,ƭ,Ʈ);if(ȃ&&Ƭ){ƻ="paused";}else if(ȃ&&!Ƭ){ƻ="resume";}}else if(Ȱ==
"rotVH1"){Ǝ="Rotating to user defined values...";bool ȃ=Ȣ("verticalOnly",ƭ,Ʈ);if(ȃ)ƻ="rotVH2";}else if(Ȱ=="rotVH2"){Ǝ=
"Rotating to user defined values...";bool ȃ=Ȣ("horizontalOnly",ƭ,Ʈ);if(ȃ&&Ƭ){ƻ="paused";}else if(ȃ&&!Ƭ){ƻ="resume";}}else{ȱ=false;}return ȱ;}double ɇ(
IMyTerminalBlock ú,string ł){ɉ(ú);var Ĳ=ú.CustomData.Split('\n').ToList();int İ=Ĳ.FindIndex(O=>O.StartsWith(ł+"="));if(İ>-1){return
Convert.ToDouble(Ĳ[İ].Replace(ł+"=",""));}return 0;}void Ɉ(IMyTerminalBlock ú,string ł,double Ü){ɉ(ú);var Ĳ=ú.CustomData.Split(
'\n').ToList();int İ=Ĳ.FindIndex(O=>O.StartsWith(ł+"="));if(İ>-1){Ĳ[İ]=ł+"="+Ü;ú.CustomData=String.Join("\n",Ĳ);}}void ɉ(
IMyTerminalBlock ú){var Ĳ=ú.CustomData.Split('\n').ToList();if(Ĳ.Count!=ǟ.Count){ú.CustomData=String.Join("\n",ǟ);}}void Ɋ(){Ǫ=0;ǭ=0;
foreach(var ä in Ǘ){Ǫ+=ä.MaxOutput;ǭ+=ä.CurrentOutput;if(showSolarStats&&enableTerminalStatistics){double ɋ=0;double.TryParse(ä
.CustomData,out ɋ);if(ɋ<ä.MaxOutput){ɋ=ä.MaxOutput;ä.CustomData=ɋ.ToString();}ù(ä,true,"",ä.MaxOutput,ɋ);}}foreach(var Ô
in ƥ){Ǫ+=Ô.GetOutput()/1000000;}if(Ǫ>Ǭ){Ǭ=Ǫ;}Ɓ=Ǫ.Ļ();ƃ=ǭ.Ļ();Ƃ=Ǭ.Ļ();Ɵ=0;Ơ=0;ơ=0;Ƣ=0;ƣ=0;Ƥ=0;foreach(var Ó in ƞ){Ɵ+=Ó.
CurrentInput;Ơ+=Ó.MaxInput;ơ+=Ó.CurrentOutput;Ƣ+=Ó.MaxOutput;ƣ+=Ó.CurrentStoredPower;Ƥ+=Ó.MaxStoredPower;if(showBatteryStats&&
enableTerminalStatistics){string ü="";if(Ó.CurrentStoredPower<Ó.MaxStoredPower*0.99){ü="Draining";if(Ó.CurrentInput>Ó.CurrentOutput)ü=
"Recharging";}ù(Ó,true,ü,Ó.CurrentStoredPower,Ó.MaxStoredPower);}}Ƅ=Ɵ.Ļ();ƅ=Ơ.Ļ();Ƈ=ơ.Ļ();Ɛ=Ƣ.Ļ();ƈ=ƣ.Ļ(true);Ɖ=Ƥ.Ļ(true);ſ=0;ƀ=0;
foreach(var ȿ in ž){ſ+=ȿ.CurrentOutput;ƀ+=ȿ.MaxOutput;if(showWindTurbineStats&&enableTerminalStatistics){ù(ȿ,true,"",ȿ.
CurrentOutput,ȿ.MaxOutput);}}Ɗ=ſ.Ļ();Ƌ=ƀ.Ļ();ƨ=0;ƒ=0;Ž=0;foreach(var Ô in ƥ){ƨ+=Ô.GetOutput();if(showOxygenFarmStats&&
enableTerminalStatistics){ù(Ô,true,"",Ô.GetOutput(),1);}}ƨ=Math.Round(ƨ/ƥ.Count*100,2);foreach(var Õ in Ʀ){ƒ+=Õ.Capacity;Ž+=Õ.Capacity*Õ.
FilledRatio;if(showOxygenTankStats&&enableTerminalStatistics){ù(Õ,true,"",Õ.FilledRatio,1);}}ƌ=ƒ.Ŀ();ƍ=Ž.Ŀ();}void ɀ(){if(Ǳ.Count==
0)return;if(Ǡ[0].IsUnderControl){Ȕ();Ǝ="Automatic alignment paused.\n";Ǝ+="Ship is currently controlled by a player.";
return;}int ǝ=10;bool Ɂ=false;bool ɂ=false;bool Ƀ=false;string Ǹ="";double Ȉ=Ǫ;double Ȋ=Ǭ;double Ʒ=ǫ;double ǹ=maxGyroRPM-(
maxGyroRPM-minGyroRPM)*(Ȉ/Ȋ);ǹ=ǹ/(Math.PI*3);if(!Ȉ.ń(ǡ-ǡ*realignPercentageGyro,ǡ+ǡ*realignPercentageGyro)&&Ƙ&&ƛ>=ǝ){ƙ=false;ƚ=
false;ǡ=0;if(Ȉ<Ʒ&&ƕ==3&&!ǐ){Ǥ=-Ǥ;ƕ=0;ǐ=true;}Ǻ((float)(Ǥ*ǹ),0,0);if(Ǥ==-1){Ǹ="down";}else{Ǹ="up";}if(Ȉ<Ʒ&&ƕ>=4){Ȅ();ƙ=true;ƚ=
true;ǡ=Ȉ;ǐ=false;ƕ=0;ƛ=0;}else{Ɂ=true;ƕ++;}}else if(Ƙ){Ȅ();ƙ=true;ƚ=true;ǐ=false;ƕ=0;ƛ++;}else{ƛ++;}if(!Ȉ.ń(ǣ-ǣ*
realignPercentageGyro,ǣ+ǣ*realignPercentageGyro)&&ƚ&&Ɲ>=ǝ){Ƙ=false;ƙ=false;ǣ=0;if(Ȉ<Ʒ&&Ɨ==3&&!Ɣ){Ǒ=-Ǒ;Ɨ=0;Ɣ=true;}Ǻ(0,0,(float)(Ǒ*ǹ));if(Ǒ==-
1){Ǹ="left";}else{Ǹ="right";}if(Ȉ<Ʒ&&Ɨ>=4){Ȅ();Ƙ=true;ƙ=true;ǣ=Ȉ;Ɣ=false;Ɨ=0;Ɲ=0;}else{Ƀ=true;Ɨ++;}}else if(ƚ){Ȅ();Ƙ=true
;ƙ=true;Ɣ=false;Ɨ=0;Ɲ++;}else{Ɲ++;}if(!Ȉ.ń(Ǣ-Ǣ*realignPercentageGyro,Ǣ+Ǣ*realignPercentageGyro)&&ƙ&&Ɯ>=ǝ){Ƙ=false;ƚ=false
;Ǣ=0;if(Ȉ<Ʒ&&Ɩ==3&&!Ƒ){Ǧ=-Ǧ;Ɩ=0;Ƒ=true;}Ǻ(0,(float)(Ǧ*ǹ),0);if(Ǧ==-1){Ǹ="left";}else{Ǹ="right";}if(Ȉ<Ʒ&&Ɩ>=4){Ȅ();Ƙ=true;
ƚ=true;Ǣ=Ȉ;Ƒ=false;Ɩ=0;Ɯ=0;}else{ɂ=true;Ɩ++;}}else if(ƙ){Ȅ();Ƙ=true;ƚ=true;Ƒ=false;Ɩ=0;Ɯ++;}else{Ɯ++;}if(!Ɂ&&!ɂ&&!Ƀ){Ǝ=
"Aligned.";}else if(Ɂ){Ǝ="Aligning by pitching the ship "+Ǹ+"..";}else if(ɂ){Ǝ="Aligning by yawing the ship "+Ǹ+"..";}else if(Ƀ){Ǝ
="Aligning by rolling the ship "+Ǹ+"..";}}void Ʉ(){if(Ǫ<Ǭ*nightPercentage&&ǜ>=30){Ǝ="Night Mode.";Ǚ=true;if(
rotateToSunrise&&!ǚ){if(manualAngle){ǚ=Ȣ("both",manualAngleHorizontal,manualAngleVertical);}else{ǚ=Ȣ("sunrise");}if(ǚ){foreach(var Ă in
Ǯ){Ɉ(Ă,"firstLockOfDay",1);Ɉ(Ă,"rotationDone",0);}}}else{Ȕ();}return;}if(Ǚ){Ǚ=false;ǜ=0;foreach(var Ă in Ǯ){Ɉ(Ă,
"outputMaxDayBefore",ɇ(Ă,"outputMax"));Ɉ(Ă,"outputMax",0);}}else if(ǜ>172800){ǜ=0;}else{ǜ++;}ǚ=false;Ǟ=true;ǝ=Ǫ<Ǭ*0.5?30:10;int ș=Ʌ(ǯ,true);
int Ȗ=Ʌ(ǰ);if(ș==0&&Ȗ==0){Ǝ="Aligned.";}else if(ș==0){Ǝ="Aligning "+Ȗ+" horizontal rotors..";}else if(Ȗ==0){Ǝ="Aligning "+ș
+" vertical rotors..";}else{Ǝ="Aligning "+Ȗ+" horizontal and "+ș+" vertical rotors..";}}int Ʌ(List<IMyMotorStator>Ɇ,bool
Ȧ=false){int ȇ=0;foreach(var Ă in Ɇ){double Ȉ=ɇ(Ă,"output");double Ʒ=ɇ(Ă,"outputLast");double ȉ=ɇ(Ă,"outputLocked");
double Ȋ=ɇ(Ă,"outputMax");double Ǹ=ɇ(Ă,"direction");double ż=ɇ(Ă,"directionChanged");double õ=ɇ(Ă,"directionTimer");double ȋ=ɇ
(Ă,"allowRotation");double Ȍ=ɇ(Ă,"timeSinceRotation");bool ȍ=false;if(ȋ==0||Ȍ<ǝ){Ȃ(Ă);Ɉ(Ă,"allowRotation",1);Ɉ(Ă,
"timeSinceRotation",Ȍ+1);continue;}if(!Ȉ.ń(ȉ-ȉ*realginPercentageRotor,ȉ+ȉ*realginPercentageRotor)){if(Ȧ){ȕ(Ă,false);}else{Ȓ(Ă,false);}ȉ=0;
if(Ȉ<Ʒ&&õ==2&&ż==0){Ǹ=-Ǹ;õ=0;ż=1;}if((Ă.LowerLimitDeg!=float.MinValue||Ă.UpperLimitDeg!=float.MaxValue)&&õ>=5){double Ȏ=ā(
Ă);float ȏ=(float)Math.Round(Ă.LowerLimitDeg);float Ȑ=(float)Math.Round(Ă.UpperLimitDeg);if(Ȏ==ȏ||Ȏ==360+ȏ||Ȏ==Ȑ||Ȏ==360+
Ȑ){if(Ȉ<Ʒ&&ż==0){Ǹ=-Ǹ;õ=0;ż=1;}else{ȍ=true;}}}bool ȑ=Ȉ.ń(Ȋ*0.998,Ȋ*1.002);float ǹ=(float)(rotorMaxSpeed-rotorMaxSpeed*Ȉ.ŵ
(Ȋ)+rotorMinSpeed);if(!ȑ)ǹ+=rotorMinSpeed;Ƿ(Ă,Ǹ,ǹ);if((Ȉ<Ʒ&&!ȑ&&õ>=3)||Ȉ==0||ȍ){Ȃ(Ă);if(ɇ(Ă,"firstLockOfDay")==1){if(Ȉ>ɇ(
Ă,"outputMaxDayBefore")*0.9){Ɉ(Ă,"firstLockOfDay",0);Ɉ(Ă,"sunriseAngle",ā(Ă));}}ȉ=Ȉ;ż=0;õ=0;Ȍ=0;}else{ȇ++;õ++;}Ɉ(Ă,
"outputLocked",ȉ);Ɉ(Ă,"direction",Ǹ);Ɉ(Ă,"directionChanged",ż);Ɉ(Ă,"directionTimer",õ);Ɉ(Ă,"timeSinceRotation",Ȍ);}else{Ȃ(Ă);}}return
ȇ;}void Ȓ(IMyMotorStator Ă,bool Ü){foreach(var ȓ in ǯ){if(Ă.CubeGrid==ȓ.TopGrid){if(Ü){Ɉ(ȓ,"allowRotation",1);}else{Ȃ(ȓ);
Ɉ(ȓ,"allowRotation",0);}}}}void ȕ(IMyMotorStator Ă,bool Ü){foreach(var Ǿ in ǰ){if(Ă.TopGrid==Ǿ.CubeGrid){if(Ü){Ɉ(Ǿ,
"allowRotation",1);}else{Ȃ(Ǿ);Ɉ(Ǿ,"allowRotation",0);}}}}void Ƿ(IMyMotorStator Ă,double Ǹ,float ǹ=rotorMinSpeed){Ă.RotorLock=false;Ă.
TargetVelocityRPM=ǹ*(float)Ǹ;}void Ǻ(double ǻ,double Ǽ,double ǽ){Vector3D ǿ=new Vector3D(-ǻ,Ǽ,ǽ);Vector3D Ȇ=Vector3D.TransformNormal(ǿ,Ǡ[
0].WorldMatrix);foreach(var Ȁ in Ǳ){Vector3D ȁ=Vector3D.TransformNormal(Ȇ,Matrix.Transpose(Ȁ.WorldMatrix));Ȁ.GyroOverride
=true;Ȁ.GyroPower=gyroPower;Ȁ.Pitch=(float)ȁ.X;Ȁ.Yaw=(float)ȁ.Y;Ȁ.Roll=(float)ȁ.Z;}}void Ȃ(IMyMotorStator Ă,bool ȃ=true){
Ă.TargetVelocityRPM=0f;if(ȃ){Ɉ(Ă,"rotationDone",1);}else{Ɉ(Ă,"rotationDone",0);}if(setRotorLockWhenStopped){Ă.RotorLock=
true;}}void Ȅ(bool ȅ=false){foreach(var Ȁ in Ǳ){Ȁ.Pitch=0;Ȁ.Yaw=0;Ȁ.Roll=0;if(ȅ)Ȁ.GyroOverride=false;}}void Ȕ(bool ȃ=true){
foreach(var Ă in Ǯ){Ȃ(Ă,ȃ);Ɉ(Ă,"timeSinceRotation",0);}Ȅ(true);ƛ=0;Ɯ=0;Ɲ=0;}bool Ȟ(IMyMotorStator Ă,double ȟ,bool Ƞ=true){
double Ȏ=ā(Ă);bool ȡ=false;if(Ƞ){if(Ă.CustomName.IndexOf("[90]")>=0){ȟ+=90;}else if(Ă.CustomName.IndexOf("[180]")>=0){ȟ+=180;}
else if(Ă.CustomName.IndexOf("[270]")>=0){ȟ+=270;}if(ȟ>=360)ȟ-=360;if(Ă.Orientation.Up.ToString()=="Down"){ȡ=true;}else if(Ă
.Orientation.Up.ToString()=="Backward"){ȡ=true;}else if(Ă.Orientation.Up.ToString()=="Left"){ȡ=true;}}if(Ă.LowerLimitDeg
!=float.MinValue||Ă.UpperLimitDeg!=float.MaxValue){if(ȡ)ȟ=-ȟ;if(ȟ>Ă.UpperLimitDeg){ȟ=Math.Floor(Ă.UpperLimitDeg);}if(ȟ<Ă.
LowerLimitDeg){ȟ=Math.Ceiling(Ă.LowerLimitDeg);}}else{if(ȡ)ȟ=360-ȟ;}if(Ȏ.ń(ȟ-1,ȟ+1)||Ȏ.ń(360+ȟ-1,360+ȟ+1)){Ȃ(Ă);return true;}else{int
Ǹ=Ȏ<ȟ?1:-1;if(Ȏ<=90&&ȟ>=270){Ǹ=-1;}if(Ȏ>=270&&ȟ<=90){Ǹ=1;}Single ǹ=Math.Abs(Ȏ-ȟ)>15?1f:0.2f;if(Math.Abs(Ȏ-ȟ)<3)ǹ=0.1f;Ƿ(Ă
,Ǹ,ǹ);return false;}}bool Ȣ(string ȣ,double Ȥ=0,double ȥ=0){bool ȃ=true;int ș=0;int Ȗ=0;if(Ǟ){Ǟ=false;Ȕ(false);}if(ȣ!=
"verticalOnly"){foreach(var Ǿ in ǰ){if(ɇ(Ǿ,"rotationDone")==1)continue;bool Ƞ=true;double ȟ=Ȥ;if(ȣ=="sunrise"){ȟ=ɇ(Ǿ,"sunriseAngle");Ƞ
=false;}if(!Ȟ(Ǿ,ȟ,Ƞ)){ȃ=false;Ȗ++;Ə=Ȗ+" horizontal rotors are set to "+Ȥ+"°";if(ȣ=="sunrise")Ə=Ȗ+
" horizontal rotors are set to sunrise position";}}}if(!ȃ)return false;if(ȣ!="horizontalOnly"){foreach(var ȓ in ǯ){if(ɇ(ȓ,"rotationDone")==1)continue;bool Ƞ=true;double
ȟ=ȥ;if(ȣ=="sunrise"){ȟ=ɇ(ȓ,"sunriseAngle");Ƞ=false;}if(!Ȟ(ȓ,ȟ,Ƞ)){ȃ=false;ș++;Ə=ș+" vertical rotors are set to "+ȥ+"°";if
(ȣ=="sunrise")Ə=ș+" vertical rotors are set to sunrise position";}}}if(ȃ)Ǟ=true;return ȃ;}void ȝ(){int Ȗ=0;int ș=0;if(ǥ==
90){foreach(var Ă in Ǯ){Ȃ(Ă,false);double ȗ=1;if(Ă.Orientation.Up.ToString()=="Up"){ȗ=-1;}else if(Ă.Orientation.Up.
ToString()=="Forward"){ȗ=-1;}else if(Ă.Orientation.Up.ToString()=="Right"){ȗ=-1;}Ɉ(Ă,"outputMax",ɇ(Ă,"output"));Ɉ(Ă,"direction",
ȗ);Ɉ(Ă,"directionChanged",0);Ɉ(Ă,"directionTimer",0);Ǭ=0;}}foreach(var Ǿ in ǰ){if(ɇ(Ǿ,"rotationDone")==1)continue;double
Ȉ=ɇ(Ǿ,"output");double Ʒ=ɇ(Ǿ,"outputLast");double Ȋ=ɇ(Ǿ,"outputMax");double Ș=ɇ(Ǿ,"outputMaxAngle");double Ǹ=ɇ(Ǿ,
"direction");double ż=ɇ(Ǿ,"directionChanged");double õ=ɇ(Ǿ,"directionTimer");if(Ȋ==0)Ȋ=1;if(ż!=2){Ȗ++;if(Ȉ<Ʒ&&õ>=7&&ż==0){Ɉ(Ǿ,
"direction",-Ǹ);Ɉ(Ǿ,"directionChanged",1);õ=0;}if((Ǿ.LowerLimitDeg!=float.MinValue||Ǿ.UpperLimitDeg!=float.MaxValue)&&õ>=3&&ż==0){
double Ț=ā(Ǿ);float ț=(float)Math.Round(Ǿ.LowerLimitDeg);float Ȝ=(float)Math.Round(Ǿ.UpperLimitDeg);if(Ț==ț||Ț==360+ț||Ț==Ȝ||Ț
==360+Ȝ){Ɉ(Ǿ,"direction",-Ǹ);Ɉ(Ǿ,"directionChanged",1);õ=0;}}Ƿ(Ǿ,Ǹ,(float)(2.75-Ȉ.ŵ(Ȋ)*2));if(Ȉ<Ʒ&&õ>=7&&ż==1){Ȃ(Ǿ,false);
Ɉ(Ǿ,"directionChanged",2);}else{Ɉ(Ǿ,"directionTimer",õ+1);}}else{if(!Ȟ(Ǿ,Ș,false))Ȗ++;}}if(Ȗ!=0)return;foreach(var ȓ in ǯ
){if(ɇ(ȓ,"rotationDone")==1)continue;double Ȉ=ɇ(ȓ,"output");double Ʒ=ɇ(ȓ,"outputLast");double Ȋ=ɇ(ȓ,"outputMax");double Ș
=ɇ(ȓ,"outputMaxAngle");double Ǹ=ɇ(ȓ,"direction");double ż=ɇ(ȓ,"directionChanged");double õ=ɇ(ȓ,"directionTimer");if(Ȋ==0)
Ȋ=1;if(ż!=2){ș++;if(Ȉ<Ʒ&&õ>=7&&ż==0){Ɉ(ȓ,"direction",-Ǹ);Ɉ(ȓ,"directionChanged",1);õ=0;}if((ȓ.LowerLimitDeg!=float.
MinValue||ȓ.UpperLimitDeg!=float.MaxValue)&&õ>=3&&ż==0){double Ñ=ā(ȓ);float ö=(float)Math.Round(ȓ.LowerLimitDeg);float ø=(float)
Math.Round(ȓ.UpperLimitDeg);if(Ñ==ö||Ñ==360+ö||Ñ==ø||Ñ==360+ø){Ɉ(ȓ,"direction",-Ǹ);Ɉ(ȓ,"directionChanged",1);õ=0;}}Ƿ(ȓ,Ǹ,(
float)(2.75-Ȉ.ŵ(Ȋ)*2));if(Ȉ<Ʒ&&õ>=7&&ż==1){Ȃ(ȓ,false);Ɉ(ȓ,"directionChanged",2);}else{Ɉ(ȓ,"directionTimer",õ+1);}}else{if(!Ȟ(
ȓ,Ș,false))ș++;}}if(Ȗ==0&&ș==0){ǥ=0;}}void ù(IMyTerminalBlock ú,bool û=true,string ü="",double ý=0,double þ=0){string ÿ=ú
.CustomName;string Ā=System.Text.RegularExpressions.Regex.Match(ú.CustomName,@" *\(\d+\.*\d*%.*\)").Value;if(Ā!=String.
Empty){ÿ=ú.CustomName.Replace(Ā,"");}if(û){ÿ+=" ("+ý.Ķ(þ);if(ü!=""){ÿ+=", "+ü;}ÿ+=")";}if(ÿ!=ú.CustomName){ú.CustomName=ÿ;}}
double ā(IMyMotorStator Ă){return Math.Round(Ă.Angle*180/Math.PI);}StringBuilder ă(IMyTextSurface Æ,bool W=true,bool Ą=true,
bool ą=true,bool ï=true,bool ó=true,bool ð=true,bool ñ=true,bool ç=true){bool î=false;StringBuilder Y=new StringBuilder();if
(W){Y.Append("Isy's Solar Alignment "+Ʃ[ǎ]+"\n");Y.Append(Æ.Ģ('=',Æ.Ŭ(Y))).Append("\n\n");}if(Ą&&Ċ!=null){Y.Append(
"Warning!\n"+Ċ+"\n\n");î=true;}if(ą){string è=Ǝ+"\n"+Ƨ;Y.Append(è);Y.Append('\n'.ĳ(3-è.Count(é=>é=='\n')));î=true;}if(ï){Y.Append(
"Statistics for "+Ǘ.Count+" Solar Panels:\n");Y.Append(Ú(Æ,"Efficiency",Ǫ,Ǭ,Ɓ,Ƃ));Y.Append(Ú(Æ,"Output",ǭ,Ǫ,ƃ,Ɓ)+"\n\n");î=true;}if(ó&&ž.
Count>0){Y.Append("Statistics for "+ž.Count+" Wind Turbines:\n");Y.Append(Ú(Æ,"Output",ſ,ƀ,Ɗ,Ƌ)+"\n\n");î=true;}if(ð&&ƞ.Count
>0){Y.Append("Statistics for "+ƞ.Count+" Batteries:\n");Y.Append(Ú(Æ,"Input",Ɵ,Ơ,Ƅ,ƅ));Y.Append(Ú(Æ,"Output",ơ,Ƣ,Ƈ,Ɛ));Y.
Append(Ú(Æ,"Charge",ƣ,Ƥ,ƈ,Ɖ)+"\n\n");î=true;}if(ñ&&(ƥ.Count>0||Ʀ.Count>0)){Y.Append("Statistics for Oxygen:\n");if(ƥ.Count>0){
Y.Append(Ú(Æ,ƥ.Count+" Farms",ƨ,100));}if(Ʀ.Count>0){Y.Append(Ú(Æ,Ʀ.Count+" Tanks",Ž,ƒ,ƍ,ƌ));}Y.Append("\n\n");î=true;}if
(ç&&!useGyroMode){string ê="";string ë="";string ì="";if(ƿ<Ƽ){ë=" inaccurate";ê="*";}else if(ƿ==ƾ||ǁ==ǀ){ë=
" inaccurate, still calculating";ê="*";}if(Ƽ<ǁ&&ê==""){ì=" / Dusk in: "+y(ǁ-Ƽ);}else if(Ƽ>ǁ&&ê==""){ì=" / Dawn in: "+y(ƿ-Ƽ);}Y.Append(
"Time of your location:\n");Y.Append("Time: "+e(Ƽ)+ì+ê+"\n");Y.Append("Dawn: "+e(ƿ)+" / Daylength: "+y(ǁ)+ê+"\n");Y.Append("Dusk: "+e(ǁ)+
" / Nightlength: "+y(ƿ-ǁ)+ê+"\n");if(ê!=""){Y.Append(ê+ë);}î=true;}if(!î){Y.Append("-- No informations to show --");}return Y;}
StringBuilder í(IMyTextSurface Æ,bool ï=false,bool ó=false,bool ð=false,bool ñ=false,bool ç=false,bool ò=false){bool î=false;
StringBuilder Y=new StringBuilder();if(ï){Y.Append("Statistics for "+Ǘ.Count+" Solar Panels:\n");Y.Append(Ú(Æ,"Efficiency",Ǫ,Ǭ,Ɓ,Ƃ,á:
true));Y.Append(Ú(Æ,"Output",ǭ,Ǫ,ƃ,Ɓ,á:true));î=true;}if(ó&&ž.Count>0){if(î)Y.Append("\n");Y.Append("Statistics for "+ž.
Count+" Wind Turbines:\n");Y.Append(Ú(Æ,"Output",ſ,ƀ,Ɗ,Ƌ,á:true));î=true;}if(ð&&ƞ.Count>0){if(î)Y.Append("\n");Y.Append(
"Statistics for "+ƞ.Count+" Batteries:\n");Y.Append(Ú(Æ,"Input",Ɵ,Ơ,Ƅ,ƅ,á:true));Y.Append(Ú(Æ,"Output",ơ,Ƣ,Ƈ,Ɛ,á:true));Y.Append(Ú(Æ,
"Charge",ƣ,Ƥ,ƈ,Ɖ,á:true));î=true;}if(ñ&&(ƥ.Count>0||Ʀ.Count>0)){if(î)Y.Append("\n");Y.Append("Statistics for Oxygen:\n");if(ƥ.
Count>0){Y.Append(Ú(Æ,ƥ.Count+" Farms",ƨ,100,á:true));}if(Ʀ.Count>0){Y.Append(Ú(Æ,Ʀ.Count+" Tanks",Ž,ƒ,ƍ,ƌ,á:true));}î=true;}
if(ç){if(î)Y.Append("\n");if(useGyroMode){Y.Append("Location time is not available in gyro mode!");}else{string q=e(Ƽ);Y.
Append(Æ.Ģ(' ',(Æ.ř()-Æ.Ŭ(q))/2)).Append(q+"\n");}î=true;}if(ò){if(î)Y.Append("\n");string q=DateTime.Now.ToString(@"HH:mm:ss"
);Y.Append(Æ.Ģ(' ',(Æ.ř()-Æ.Ŭ(q))/2)).Append(q+"\n");î=true;}if(!î){Y.Append(
"Edit the custom data and set,\nwhat should be shown here!");}return Y;}void Ć(string z=null){if(ǳ.Count==0){ƺ++;return;}for(int O=Ǔ;O<ǳ.Count;O++){if(Ŕ())return;Ǔ++;var Č=ǳ[O].ė(
mainLcdKeyword);foreach(var ô in Č){var æ=ô.Key;var T=ô.Value;if(!æ.GetText().EndsWith("\a")){æ.Font=defaultFont;æ.FontSize=
defaultFontSize;æ.Alignment=VRage.Game.GUI.TextPanel.TextAlignment.LEFT;æ.ContentType=VRage.Game.GUI.TextPanel.ContentType.
TEXT_AND_IMAGE;}bool W=T.Ł("showHeading");bool Ą=T.Ł("showWarnings");bool ą=T.Ł("showCurrentOperation");bool ï=T.Ł("showSolarStats");
bool ó=T.Ł("showTurbineStats");bool ð=T.Ł("showBatteryStats");bool ñ=T.Ł("showOxygenStats");bool ç=T.Ł("showLocationTime");
bool X=T.Ł("scrollTextIfNeeded");StringBuilder Y=new StringBuilder();if(z!=null){Y.Append(z);}else{Y=ă(æ,W,Ą,ą,ï,ó,ð,ñ,ç);}Y
=æ.Ĩ(Y,W?3:0,X);æ.WriteText(Y.Append("\a"));}}ƺ++;Ǔ=0;}void č(){if(Ǵ.Count==0){ƺ++;return;}for(int O=ǔ;O<Ǵ.Count;O++){if(
Ŕ())return;ǔ++;var Č=Ǵ[O].ė(compactLcdKeyword);foreach(var ô in Č){var æ=ô.Key;var T=ô.Value;if(!æ.GetText().EndsWith(
"\a")){æ.Font=defaultFont;æ.FontSize=defaultFontSize;æ.Alignment=VRage.Game.GUI.TextPanel.TextAlignment.LEFT;æ.ContentType=
VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;}bool ï=T.Ł("showSolarStats");bool ó=T.Ł("showTurbineStats");bool ð=T.Ł(
"showBatteryStats");bool ñ=T.Ł("showOxygenStats");bool ç=T.Ł("showLocationTime");bool ò=T.Ł("showRealTime");bool X=T.Ł(
"scrollTextIfNeeded");StringBuilder Y=new StringBuilder();Y=í(æ,ï,ó,ð,ñ,ç,ò);Y=æ.Ĩ(Y,0,X);æ.WriteText(Y.Append("\a"));}}ƺ++;ǔ=0;}void ć(){if
(ǵ.Count==0){ƺ++;return;}StringBuilder Ĉ=new StringBuilder();if(Ǎ.Count==0){Ĉ.Append("- No problems detected -");}else{
int ĉ=1;foreach(var Ċ in Ǎ){Ĉ.Append(ĉ+". "+Ċ.Replace("\n"," ")+"\n");ĉ++;}}for(int O=Ǖ;O<ǵ.Count;O++){if(Ŕ())return;Ǖ++;
var Č=ǵ[O].ė(warningsLcdKeyword);foreach(var ô in Č){var æ=ô.Key;var T=ô.Value;if(!æ.GetText().EndsWith("\a")){æ.Font=
defaultFont;æ.FontSize=defaultFontSize;æ.Alignment=VRage.Game.GUI.TextPanel.TextAlignment.LEFT;æ.ContentType=VRage.Game.GUI.
TextPanel.ContentType.TEXT_AND_IMAGE;}bool W=T.Ł("showHeading");bool X=T.Ł("scrollTextIfNeeded");StringBuilder Y=new
StringBuilder();if(W){Y.Append("Isy's Solar Alignment Warnings\n");Y.Append(æ.Ģ('=',æ.Ŭ(Y))).Append("\n\n");}Y.Append(Ĉ);Y=æ.Ĩ(Y,W?3:
0,X);æ.WriteText(Y.Append("\a"));}}ƺ++;Ǖ=0;}void ċ(){if(Ƕ.Count==0){ƺ++;return;}for(int O=ǖ;O<Ƕ.Count;O++){if(Ŕ())return;
ǖ++;var Č=Ƕ[O].ė(performanceLcdKeyword);foreach(var ô in Č){var æ=ô.Key;var T=ô.Value;if(!æ.GetText().EndsWith("\a")){æ.
Font=defaultFont;æ.FontSize=defaultFontSize;æ.Alignment=VRage.Game.GUI.TextPanel.TextAlignment.LEFT;æ.ContentType=VRage.Game
.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;}bool W=T.Ł("showHeading");bool X=T.Ł("scrollTextIfNeeded");StringBuilder Y=new
StringBuilder();if(W){Y.Append("Isy's Solar Alignment Performance\n");Y.Append(æ.Ģ('=',æ.Ŭ(Y))).Append("\n\n");}Y.Append(Ď);Y=æ.Ĩ(Y,W
?3:0,X);æ.WriteText(Y.Append("\a"));}}ƺ++;ǖ=0;}void Z(){if(Ş==99){Ş=0;}else{Ş++;}Echo("Isy's Solar Alignment "+Ʃ[ǎ]+
"\n========================\n");if(Ċ!=null){Echo("Warning!\n"+Ċ+"\n");}StringBuilder Y=new StringBuilder();Y.Append("Script is running in "+(
useGyroMode?"gyro":"rotor")+" mode\n\n");Y.Append("Task: "+Ƶ[Ʊ]+"\n");Y.Append("Script step: "+Ʊ+" / "+(Ƶ.Length-1)+"\n\n");Ď=Y.
Append(Ď);Y.Append("Main Grid: "+Ŧ.CustomName+"\n");if(ō.Count>0)Y.Append("Connected Grids: "+ō.Count+"\n");if(Ǯ.Count>0)Y.
Append("Rotors: "+Ǯ.Count+"\n");if(Ǳ.Count>0)Y.Append("Gyros: "+Ǳ.Count+"\n");if(Ǘ.Count>0)Y.Append("Solar Panels: "+Ǘ.Count+
"\n");if(ž.Count>0)Y.Append("Wind Turbines: "+ž.Count+"\n");if(ƥ.Count>0)Y.Append("Oxygen Farms: "+ƥ.Count+"\n");if(Ʀ.Count>
0)Y.Append("Oxygen Tanks: "+Ʀ.Count+"\n");if(ƞ.Count>0)Y.Append("Batteries: "+ƞ.Count+"\n");if(Ǉ>0)Y.Append("Reactors: "+
Ǉ+"\n");if(ǈ>0)Y.Append("Hydrogen Engines: "+ǈ+"\n");if(ǳ.Count>0)Y.Append("LCDs: "+ǳ.Count+"\n");if(ǲ.Count>0)Y.Append(
"Corner LCDs: "+ǲ.Count+"\n");if(ǧ.Count>0)Y.Append("Lights: "+ǧ.Count+"\n");if(ǒ.Count>0)Y.Append("Spotlights: "+ǒ.Count+"\n");if(
timers.Length>0)Y.Append("Timer Blocks: "+timers.Length+"\n");Echo(Ď.ToString());if(ǳ.Count==0){Echo(
"Hint:\nBuild a LCD and add the main LCD\nkeyword '"+mainLcdKeyword+"' to its name to get\nmore informations about your base\nand the current script actions.\n");}}void a()
{Ƽ+=1;ƽ+=1;if(Ƽ>172800){Ƽ=0;ƽ=0;}double d=Ǭ*nightTimePercentage;if(Ǫ<d&&ǫ>=d&&ƽ>300){ǁ=Ƽ;ƽ=0;}if(Ǫ>d&&ǫ<=d&&ƽ>300){if(ǁ!=
ǀ){ƿ=Ƽ;}Ƽ=0;ƽ=0;}if(ǁ>ƿ){ƿ=ǁ*2;}}string e(double f,bool h=false){string j="";f=f%ƿ;double k=ǁ+(ƿ-ǁ)/2D;double m=ƿ/24D;
double q;if(f<k){q=(f+(ƿ-k))/m;}else{q=(f-k)/m;}double u=Math.Floor(q);double v=Math.Floor((q%1*100)*0.6);string w=u.ToString(
"00");string x=v.ToString("00");j=w+":"+x;if(h){return u.ToString();}else{return j;}}string y(int N){string U="";TimeSpan A=
TimeSpan.FromSeconds(N);U=A.ToString(@"hh\:mm\:ss");return U;}void J(){if(Ǜ.Count==0)return;double B=turnOnAtPercent%100/100;
double C=turnOffAtPercent%100/100;double D=overloadPercentage%100/100;if(Ǌ=="lowBat"||Ǌ==""){if(activateOnLowBattery&&ƣ<Ƥ*B){Ǆ
=true;Ǌ="lowBat";}else if(activateOnLowBattery&&ƣ>Ƥ*C){Ǆ=false;Ǌ="";}}if(Ǌ=="overload"||Ǌ==""){if(activateOnOverload&&ơ+ǭ
+ſ>(Ƣ+Ǫ+ƀ)*D){Ǆ=true;Ǌ="overload";}else{Ǆ=false;Ǌ="";}}if(ƣ<ǉ||(Ǆ&&ǅ&&ǆ)){ǅ=true;ǆ=true;}else{if(
activateHydrogenEngineFirst&&ǈ>0){ǆ=true;ǅ=false;}else if(!activateHydrogenEngineFirst&&Ǉ>0){ǆ=false;ǅ=true;}else{ǆ=true;ǅ=true;}}ǉ=ƣ;foreach(var E
in Ǜ){if(Ǆ){if(ǅ&&E.BlockDefinition.TypeIdString.Contains("Reactor")){E.Enabled=true;}else if(ǆ&&E.BlockDefinition.
TypeIdString.Contains("HydrogenEngine")){E.Enabled=true;}else{E.Enabled=false;}}else{E.Enabled=false;}}if(Ǌ=="lowBat")Ə=
"Power fallback active: Low battery charge!";if(Ǌ=="overload")Ə="Power fallback active: Overload!";}void F(){if(ǧ.Count==0&&ǒ.Count==0)return;int G=0;int.TryParse(e
(Ƽ,true),out G);bool H=true;if(!simpleMode){if(Ƽ!=ƿ&&G>=lightOffHour&&G<lightOnHour){H=false;}else if(Ƽ==ƿ&&Ǫ>Ǭ*
nightTimePercentage){H=false;}}else{if(Ǫ>Ǭ*(simpleThreshold%100)/100)H=false;}foreach(var I in ǧ){I.Enabled=H;}foreach(var K in ǒ){K.
Enabled=H;}}void S(){if(events.Length==0){R("No events for triggering specified!");}else if(timers.Length==0){R(
"No timers for triggering specified!");}else if(events.Length!=timers.Length){R("Every event needs a timer block name!\nFound "+events.Length+" events and "+
timers.Length+" timers.");}else{int L=-1;string M="";int N;for(int O=0;O<=events.Length-1;O++){if(events[O]=="sunrise"&&Ƽ==0){
L=O;M="sunrise";}else if(events[O]=="sunset"&&Ƽ==ǁ){L=O;M="sunset";}else if(int.TryParse(events[O],out N)==true&&Ƽ%N==0){
L=O;M=N+" seconds";}else if(e(Ƽ)==events[O]){L=O;M=events[O];}}foreach(var P in timers){var Q=GridTerminalSystem.
GetBlockWithName(P)as IMyTimerBlock;if(Q==null){R("External timer block not found:\n'"+Q.CustomName+"'");}else{if(Q.GetOwnerFactionTag()
!=Me.GetOwnerFactionTag()){R("'"+Q.CustomName+
"' has a different owner / faction!\nAll blocks should have the same owner / faction!");}if(Q.Enabled==false){R("'"+Q.CustomName+"' is turned off!\nTurn it on in order to be used by the script!");}}}if(L>=0
){var Q=GridTerminalSystem.GetBlockWithName(timers[L])as IMyTimerBlock;if(Q!=null){Q.ApplyAction("Start");Ǝ=
"External timer triggered! Reason: "+M;}}}}void R(string z){Ǎ.Add(z);Ǐ.Add(z);Ċ=Ǎ.ElementAt(0);}void ª(){foreach(var ä in Ǘ){ä.CustomData="";ù(ä,false);}
foreach(var Ó in ƞ){ù(Ó,false);}foreach(var Ô in ƥ){ù(Ô,false);}foreach(var Õ in Ʀ){ù(Õ,false);}}void Ö(){if(Storage.Length>0){
var T=Storage.Split('\n');foreach(var Ø in T){var Ù=Ø.Split('=');if(Ù.Length!=2)continue;if(Ù[0]=="dayTimer"){int.TryParse(
Ù[1],out Ƽ);}else if(Ù[0]=="dayLength"){int.TryParse(Ù[1],out ƿ);}else if(Ù[0]=="sunSet"){int.TryParse(Ù[1],out ǁ);}else
if(Ù[0]=="outputLast"){float.TryParse(Ù[1],out ǫ);}else if(Ù[0]=="maxDetectedOutput"){float.TryParse(Ù[1],out Ǭ);}else if(
Ù[0]=="solarPanelsCount"){int.TryParse(Ù[1],out ǘ);}else if(Ù[0]=="oxygenFarmsCount"){int.TryParse(Ù[1],out Ɔ);}else if(Ù
[0]=="action"){ƻ=Ù[1];}}if(ƻ=="paused")Ƴ=true;}}void Save(){string T="";T+="dayTimer="+Ƽ+"\n";T+="dayLength="+ƿ+"\n";T+=
"sunSet="+ǁ+"\n";T+="outputLast="+ǫ+"\n";T+="maxDetectedOutput="+Ǭ+"\n";T+="solarPanelsCount="+Ǘ.Count+"\n";T+=
"oxygenFarmsCount="+ƥ.Count+"\n";T+="action="+ƻ;Storage=T;}StringBuilder Ú(IMyTextSurface Æ,string Û,double Ü,double Ý,string Þ=null,string
ß=null,bool à=false,bool á=false){string â=Ü.ToString();string ã=Ý.ToString();if(Þ!=null){â=Þ;}if(ß!=null){ã=ß;}float å=Æ
.FontSize;float Ò=Æ.ř();char µ=' ';float Ç=Æ.Ų(µ);StringBuilder º=new StringBuilder(" "+Ü.Ķ(Ý));º=Æ.Ģ(µ,Æ.Ŭ("99999.9%")-Æ
.Ŭ(º)).Append(º);StringBuilder À=new StringBuilder(â+" / "+ã);StringBuilder Á=new StringBuilder();StringBuilder Â=new
StringBuilder();StringBuilder Ã;double Ä=0;if(Ý>0)Ä=Ü/Ý>=1?1:Ü/Ý;if(á&&!à){if(å<=0.5||(å<=1&&Ò>512)){Á.Append(Å(Æ,Ò*0.25f,Ä)+" "+Û);Ã
=Æ.Ģ(µ,Ò*0.75-Æ.Ŭ(Á)-Æ.Ŭ(â+" /"));Á.Append(Ã).Append(À);Ã=Æ.Ģ(µ,Ò-Æ.Ŭ(Á)-Æ.Ŭ(º));Á.Append(Ã);Á.Append(º);}else{Á.Append(Å
(Æ,Ò*0.3f,Ä)+" "+Û);Ã=Æ.Ģ(µ,Ò-Æ.Ŭ(Á)-Æ.Ŭ(º));Á.Append(Ã);Á.Append(º);}}else{Á.Append(Û+" ");if(å<=0.6||(å<=1&&Ò>512)){Ã=Æ
.Ģ(µ,Ò*0.5-Æ.Ŭ(Á)-Æ.Ŭ(â+" /"));Á.Append(Ã).Append(À);Ã=Æ.Ģ(µ,Ò-Æ.Ŭ(Á)-Æ.Ŭ(º));Á.Append(Ã).Append(º);if(!à){Â=Å(Æ,Ò,Ä).
Append("\n");}}else{Ã=Æ.Ģ(µ,Ò-Æ.Ŭ(Á)-Æ.Ŭ(À));Á.Append(Ã).Append(À);if(!à){Â=Å(Æ,Ò-Æ.Ŭ(º),Ä);Â.Append(º).Append("\n");}}}return
Á.Append("\n").Append(Â);}StringBuilder Å(IMyTextSurface Æ,float È,double Ä){StringBuilder É,Ê;char Ë='[';char Ì=']';char
Í='I';char Î='.';float Ï=Æ.Ų(Ë);float Ð=Æ.Ų(Ì);float V=È-Ï-Ð;É=Æ.Ģ(Í,V*Ä);Ê=Æ.Ģ(Î,V-Æ.Ŭ(É));return new StringBuilder().
Append(Ë).Append(É).Append(Ê).Append(Ì);}StringBuilder Ď=new StringBuilder("No performance Information available!");Dictionary
<string,int>Ġ=new Dictionary<string,int>();List<int>Ś=new List<int>(new int[100]);List<double>ś=new List<double>(new
double[100]);double Ŝ,ŝ;int Ş=0;DateTime ş;void Š(string š,bool ŉ=false){if(ŉ){ş=DateTime.Now;return;}Ş=Ş>=99?0:Ş+1;int Ţ=
Runtime.CurrentInstructionCount;if(Ţ>Ŝ)Ŝ=Ţ;Ś[Ş]=Ţ;double ţ=Ś.Sum()/Ś.Count;Ď.Clear();Ď.Append("Instructions: "+Ţ+" / "+Runtime.
MaxInstructionCount+"\n");Ď.Append("Max. Instructions: "+Ŝ+" / "+Runtime.MaxInstructionCount+"\n");Ď.Append("Avg. Instructions: "+Math.
Floor(ţ)+" / "+Runtime.MaxInstructionCount+"\n\n");double Ť=(DateTime.Now-ş).TotalMilliseconds;if(Ť>ŝ&&Ġ.ContainsKey(š))ŝ=Ť;ś
[Ş]=Ť;double ť=ś.Sum()/ś.Count;Ď.Append("Last runtime: "+Math.Round(Ť,4)+" ms\n");Ď.Append("Max. runtime: "+Math.Round(ŝ,
4)+" ms\n");Ď.Append("Avg. runtime: "+Math.Round(ť,4)+" ms\n\n");Ď.Append("Instructions per Method:\n");Ġ[š]=Ţ;foreach(
var P in Ġ.OrderByDescending(O=>O.Value)){Ď.Append("- "+P.Key+": "+P.Value+"\n");}Ď.Append("\n");}IMyCubeGrid Ŧ=null;
HashSet<IMyCubeGrid>ŧ=new HashSet<IMyCubeGrid>();void Ũ(IMyCubeGrid ŏ){ŧ.Add(ŏ);List<IMyMotorStator>ő=new List<IMyMotorStator>(
);List<IMyPistonBase>Œ=new List<IMyPistonBase>();GridTerminalSystem.GetBlocksOfType<IMyMotorStator>(ő,œ=>œ.IsAttached&&œ.
TopGrid==ŏ&&!ŧ.Contains(œ.CubeGrid));GridTerminalSystem.GetBlocksOfType<IMyPistonBase>(Œ,ŋ=>ŋ.IsAttached&&ŋ.TopGrid==ŏ&&!ŧ.
Contains(ŋ.CubeGrid));if(ő.Count==0&&Œ.Count==0){Ŧ=ŏ;return;}else{foreach(var Ă in ő){Ũ(Ă.CubeGrid);}foreach(var Ō in Œ){Ũ(Ō.
CubeGrid);}}}HashSet<IMyCubeGrid>ō=new HashSet<IMyCubeGrid>();void Ŏ(IMyCubeGrid ŏ,bool Ő=false){if(Ő)ō.Clear();ō.Add(ŏ);List<
IMyMotorStator>ő=new List<IMyMotorStator>();List<IMyPistonBase>Œ=new List<IMyPistonBase>();GridTerminalSystem.GetBlocksOfType<
IMyMotorStator>(ő,œ=>œ.CubeGrid==ŏ&&œ.IsAttached&&!ō.Contains(œ.TopGrid));GridTerminalSystem.GetBlocksOfType<IMyPistonBase>(Œ,ŋ=>ŋ.
CubeGrid==ŏ&&ŋ.IsAttached&&!ō.Contains(ŋ.TopGrid));foreach(var Ă in ő){Ŏ(Ă.TopGrid);}foreach(var Ō in Œ){Ŏ(Ō.TopGrid);}}bool Ŕ(
double Ü=10){return Runtime.CurrentInstructionCount>Ü*1000;}List<IMyTerminalBlock>ŕ(string Ę,string[]Ŗ=null){string ŗ=
"[IsyLCD]";var Ř=new List<IMyTerminalBlock>();GridTerminalSystem.GetBlocksOfType<IMyTextSurfaceProvider>(Ř,ũ=>ũ.IsSameConstructAs(
Me)&&(ũ.CustomName.Contains(Ę)||(ũ.CustomName.Contains(ŗ)&&ũ.CustomData.Contains(Ę))));var Ū=Ř.FindAll(ũ=>ũ.CustomName.
Contains(Ę));foreach(var Æ in Ū){Æ.CustomName=Æ.CustomName.Replace(Ę,"").Replace(" "+Ę,"").TrimEnd(' ');bool ų=false;if(Æ is
IMyTextSurface){if(!Æ.CustomName.Contains(ŗ))ų=true;if(!Æ.CustomData.Contains(Ę))Æ.CustomData="@0 "+Ę+(Ŗ!=null?"\n"+String.Join("\n",Ŗ
):"");}else if(Æ is IMyTextSurfaceProvider){if(!Æ.CustomName.Contains(ŗ))ų=true;int Ŵ=(Æ as IMyTextSurfaceProvider).
SurfaceCount;for(int O=0;O<Ŵ;O++){if(!Æ.CustomData.Contains("@"+O)){Æ.CustomData+=(Æ.CustomData==""?"":"\n\n")+"@"+O+" "+Ę+(Ŗ!=null?
"\n"+String.Join("\n",Ŗ):"");break;}}}else{Ř.Remove(Æ);}if(ų)Æ.CustomName+=" "+ŗ;}return Ř;}
}public static partial class ĵ{public static float ŵ(this float ķ,float ĸ){return ĸ==0?1:ķ/ĸ;}public static double ŵ(this
double ķ,double ĸ){return ĸ==0?1:ķ/ĸ;}}public static partial class ĵ{private static Dictionary<char,float>Ŷ=new Dictionary<
char,float>();public static void ŷ(string Ÿ,float Ź){foreach(char ū in Ÿ){Ŷ[ū]=Ź;}}public static void ź(){if(Ŷ.Count>0)
return;ŷ("3FKTabdeghknopqsuy£µÝàáâãäåèéêëðñòóôõöøùúûüýþÿāăąďđēĕėęěĝğġģĥħĶķńņňŉōŏőśŝşšŢŤŦũūŭůűųŶŷŸșȚЎЗКЛбдекруцяёђћўџ",18);ŷ(
"ABDNOQRSÀÁÂÃÄÅÐÑÒÓÔÕÖØĂĄĎĐŃŅŇŌŎŐŔŖŘŚŜŞŠȘЅЊЖф□",22);ŷ("#0245689CXZ¤¥ÇßĆĈĊČŹŻŽƒЁЌАБВДИЙПРСТУХЬ€",20);ŷ("￥$&GHPUVY§ÙÚÛÜÞĀĜĞĠĢĤĦŨŪŬŮŰŲОФЦЪЯжы†‡",21);ŷ(
"！ !I`ijl ¡¨¯´¸ÌÍÎÏìíîïĨĩĪīĮįİıĵĺļľłˆˇ˘˙˚˛˜˝ІЇії‹›∙",9);ŷ("？7?Jcz¢¿çćĉċčĴźżžЃЈЧавийнопсъьѓѕќ",17);ŷ("（）：《》，。、；【】(),.1:;[]ft{}·ţťŧț",10);ŷ("+<=>E^~¬±¶ÈÉÊË×÷ĒĔĖĘĚЄЏЕНЭ−",19);
ŷ("L_vx«»ĹĻĽĿŁГгзлхчҐ–•",16);ŷ("\"-rª­ºŀŕŗř",11);ŷ("WÆŒŴ—…‰",32);ŷ("'|¦ˉ‘’‚",7);ŷ("@©®мшњ",26);ŷ("mw¼ŵЮщ",28);ŷ("/ĳтэє",
15);ŷ("\\°“”„",13);ŷ("*²³¹",12);ŷ("¾æœЉ",29);ŷ("%ĲЫ",25);ŷ("MМШ",27);ŷ("½Щ",30);ŷ("ю",24);ŷ("ј",8);ŷ("љ",23);ŷ("ґ",14);ŷ(
"™",31);}public static Vector2 Ż(this IMyTextSurface æ,StringBuilder z){ź();Vector2 È=new Vector2();if(æ.Font=="Monospace")
{float å=æ.FontSize;È.X=(float)(z.Length*19.4*å);È.Y=(float)(28.8*å);return È;}else{float å=(float)(æ.FontSize*0.779);
foreach(char ū in z.ToString()){try{È.X+=Ŷ[ū]*å;}catch{}}È.Y=(float)(28.8*æ.FontSize);return È;}}public static float Ŭ(this
IMyTextSurface Æ,StringBuilder z){Vector2 ŭ=Æ.Ż(z);return ŭ.X;}public static float Ŭ(this IMyTextSurface Æ,string z){Vector2 ŭ=Æ.Ż(new
StringBuilder(z));return ŭ.X;}public static float Ų(this IMyTextSurface Æ,char Ů){float ů=Ŭ(Æ,new string(Ů,1));return ů;}public
static int Ű(this IMyTextSurface Æ){Vector2 Ğ=Æ.SurfaceSize;float ġ=Æ.TextureSize.Y;Ğ.Y*=512/ġ;float ű=Ğ.Y*(100-Æ.TextPadding*
2)/100;Vector2 ŭ=Æ.Ż(new StringBuilder("T"));return(int)(ű/ŭ.Y);}public static float ř(this IMyTextSurface Æ){Vector2 Ğ=Æ
.SurfaceSize;float ġ=Æ.TextureSize.Y;Ğ.X*=512/ġ;return Ğ.X*(100-Æ.TextPadding*2)/100;}public static StringBuilder Ģ(this
IMyTextSurface Æ,char ģ,double Ĥ){int ĥ=(int)(Ĥ/Ų(Æ,ģ));if(ĥ<0)ĥ=0;return new StringBuilder().Append(ģ,ĥ);}private static DateTime Ħ=
DateTime.Now;private static Dictionary<int,List<int>>ħ=new Dictionary<int,List<int>>();public static StringBuilder Ĩ(this
IMyTextSurface Æ,StringBuilder z,int ĩ=3,bool X=true,int Ī=0){int ī=Æ.GetHashCode();if(!ħ.ContainsKey(ī)){ħ[ī]=new List<int>{1,3,ĩ,0};
}int Ĭ=ħ[ī][0];int ĭ=ħ[ī][1];int Į=ħ[ī][2];int į=ħ[ī][3];var ı=z.ToString().TrimEnd('\n').Split('\n');List<string>ğ=new
List<string>();if(Ī==0)Ī=Æ.Ű();float Ò=Æ.ř();StringBuilder Ė,ď=new StringBuilder();for(int O=0;O<ı.Length;O++){if(O<ĩ||O<Į||
ğ.Count-Į>Ī||Æ.Ŭ(ı[O])<=Ò){ğ.Add(ı[O]);}else{try{ď.Clear();float Đ,đ;var Ē=ı[O].Split(' ');string ē=System.Text.
RegularExpressions.Regex.Match(ı[O],@"\d+(\.|\:)\ ").Value;Ė=Æ.Ģ(' ',Æ.Ŭ(ē));foreach(var Ĕ in Ē){Đ=Æ.Ŭ(ď);đ=Æ.Ŭ(Ĕ);if(Đ+đ>Ò){ğ.Add(ď.
ToString());ď=new StringBuilder(Ė+Ĕ+" ");}else{ď.Append(Ĕ+" ");}}ğ.Add(ď.ToString());}catch{ğ.Add(ı[O]);}}}if(X){if(ğ.Count>Ī){
if(DateTime.Now.Second!=į){į=DateTime.Now.Second;if(ĭ>0)ĭ--;if(ĭ<=0)Į+=Ĭ;if(Į+Ī-ĩ>=ğ.Count&&ĭ<=0){Ĭ=-1;ĭ=3;}if(Į<=ĩ&&ĭ<=0)
{Ĭ=1;ĭ=3;}}}else{Į=ĩ;Ĭ=1;ĭ=3;}ħ[ī][0]=Ĭ;ħ[ī][1]=ĭ;ħ[ī][2]=Į;ħ[ī][3]=į;}else{Į=ĩ;}StringBuilder ĕ=new StringBuilder();for(
var Ø=0;Ø<ĩ;Ø++){ĕ.Append(ğ[Ø]+"\n");}for(var Ø=Į;Ø<ğ.Count;Ø++){ĕ.Append(ğ[Ø]+"\n");}return ĕ;}public static Dictionary<
IMyTextSurface,string>ė(this IMyTerminalBlock ú,string Ę,Dictionary<string,string>ę=null){var Ě=new Dictionary<IMyTextSurface,string>(
);if(ú is IMyTextSurface){Ě[ú as IMyTextSurface]=ú.CustomData;}else if(ú is IMyTextSurfaceProvider){var ě=System.Text.
RegularExpressions.Regex.Matches(ú.CustomData,@"@(\d) *("+Ę+@")");int Ĝ=(ú as IMyTextSurfaceProvider).SurfaceCount;foreach(System.Text.
RegularExpressions.Match ĝ in ě){int İ=-1;if(int.TryParse(ĝ.Groups[1].Value,out İ)){if(İ>=Ĝ)continue;string Ĳ=ú.CustomData;int ŉ=Ĳ.IndexOf
("@"+İ);int ŀ=Ĳ.IndexOf("@",ŉ+1)-ŉ;string T=ŀ<=0?Ĳ.Substring(ŉ):Ĳ.Substring(ŉ,ŀ);Ě[(ú as IMyTextSurfaceProvider).
GetSurface(İ)]=T;}}}return Ě;}public static bool Ł(this string T,string ł){var Ĳ=T.Replace(" ","").Split('\n');foreach(var Ø in Ĳ)
{if(Ø.StartsWith(ł+"=")){try{return Convert.ToBoolean(Ø.Replace(ł+"=",""));}catch{return true;}}}return true;}public
static string Ń(this string T,string ł){var Ĳ=T.Replace(" ","").Split('\n');foreach(var Ø in Ĳ){if(Ø.StartsWith(ł+"=")){return
Ø.Replace(ł+"=","");}}return"";}}public static partial class ĵ{public static bool ń(this double Ü,double Ņ,double ã,bool
ņ=false,bool Ň=false){bool ň=Ü>=Ņ;bool Ŋ=Ü<=ã;if(Ň)ň=Ü>Ņ;if(ņ)Ŋ=Ü<ã;return ň&&Ŋ;}}public static partial class ĵ{public
static string ĳ(this char ĺ,int Ĵ){if(Ĵ<=0){return"";}return new string(ĺ,Ĵ);}}public static partial class ĵ{public static
string Ķ(this double ķ,double ĸ){double Ĺ=Math.Round(ķ/ĸ*100,1);if(ĸ==0){return"0%";}else{return Ĺ+"%";}}public static string
Ķ(this float ķ,float ĸ){double Ĺ=Math.Round(ķ/ĸ*100,1);if(ĸ==0){return"0%";}else{return Ĺ+"%";}}}public static partial
class ĵ{public static string Ļ(this float Ü,bool ļ=false){string Ľ="MW";string ľ=Ü<0?"-":"";Ü=Math.Abs(Ü);if(Ü<1){Ü*=1000;Ľ=
"kW";}else if(Ü>=1000&&Ü<1000000){Ü/=1000;Ľ="GW";}else if(Ü>=1000000&&Ü<1000000000){Ü/=1000000;Ľ="TW";}else if(Ü>=1000000000
){Ü/=1000000000;Ľ="PW";}if(ļ)Ľ+="h";return ľ+Math.Round(Ü,1)+" "+Ľ;}}public static partial class ĵ{public static string Ŀ
(this double Ü){string Ľ="L";if(Ü>=1000&&Ü<1000000){Ü/=1000;Ľ="KL";}else if(Ü>=1000000&&Ü<1000000000){Ü/=1000000;Ľ="ML";}
else if(Ü>=1000000000&&Ü<1000000000000){Ü/=1000000000;Ľ="BL";}else if(Ü>=1000000000000){Ü/=1000000000000;Ľ="TL";}return Math
.Round(Ü,1)+" "+Ľ;}
