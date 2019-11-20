

    /*//////////////////////////
     * Thank you for using:
     * [PAM] - Path Auto Miner
     * ————————————
     * Author:  Keks
     * Last update: 2019-06-16
     * ————————————
     * Guide: https://steamcommunity.com/sharedfiles/filedetails/?id=1553126390
     * Script: https://steamcommunity.com/sharedfiles/filedetails/?id=1507646929
     * Youtube: https://youtu.be/ne_i5U2Y8Fk
     * ————————————
     * Please report bugs here:
     * https://steamcommunity.com/workshop/filedetails/discussion/1507646929/2727382174640941895/
     * ————————————
     * I hope you enjoy this script and don't forget
     * to leave a comment on the steam workshop
     *//////////////////////////

    const string VERSION = "1.3.0";
    const string DATAREV = "14";

    const String pamTag = "[PAM]";
    const String controllerTag = "[PAM-Controller]";
    //Tag for LCD's of cockpits and other blocks: [PAM:<lcdIndex>] e.g: [PAM:1]

    const int gyroSpeedSmall = 15; //[RPM] small ship
    const int gyroSpeedLarge = 5; //[RPM] large ship
    const int generalSpeedLimit = 100; //[m/s] 0 = no limit (if set to 0 ignore unreachable code warning)
    const float dockingSpeed = 0.5f; //[m/s]

    //multiplied with ship size
    const float dockDist = 0.6f; //position in front of the home connector
    const float followPathDock = 2f; //stop following path, fly direct to target
    const float followPathJob = 1f; //same as dock
    const float useDockDirectionDist = 1f; //Override waypoint direction, use docking dir
    const float useJobDirectionDist = 0f; //same as dock

    //other distances
    const float wpReachedDist = 2f;//[m]
    const float drillRadius = 1.4f;//[m]

    //grinding
    const float sensorRange = 2f;//fly slow when blocks found in this range
    const float fastSpeed = 10f;//speed when no blocks are detected

    //minimum acceleration in space before ship becomes too heavy
    const float minAccelerationSmall = 0.2f;//[m/s²] small ship
    const float minAccelerationLarge = 0.1f;//[m/s²] large ship

    //stone ejection
    const float minEjection = 25f;//[%] Min amount of ejected cargo to continue job

    //LCD
    const bool setLCDFontAndSize = true;

    //Check if blocks are connected with conveyors
    const bool checkConveyorSystem = false;//temporarily disabled because of a SE bug


    public String GetElementCode(String itemName)
    {
//Here you can define custom element codes for the PAM-Controller
//You can extend this when you are using mods which adds new materials
//This is not necessary for any function of PAM, it is just a little detail on the controller screen
//It only needs to be changed in the controller pb
switch (itemName)
{
    case "IRON": return "Fe";
    case "NICKEL": return "Ni";
    case "COBALT": return "Co";
    case "MAGNESIUM": return "Mg";
    case "SILICON": return "Si";
    case "SILVER": return "Ag";
    case "GOLD": return "Au";
    case "PLATINUM": return "Pt";
    case "URANIUM": return "U";

    //add new entries here

    //example:
    //New material: ExampleOre
    //Element code: Ex

    //case "EXAMPLEORE": return "Ex";

    default: return ""; //don't change this!
}
    }
Program(){φ=GridTerminalSystem;Runtime.UpdateFrequency=UpdateFrequency.Update1;if(ј(Me,controllerTag,true))ƽ=Г.Б;Ј(Ϸ.ϵ)
;if(ƽ!=Г.Б){ύ="Welcome to [PAM]!";ɚ ω=ɖ();if(ƽ==Г.Ǻ){List<IMyShipDrill>ψ=new List<IMyShipDrill>();List<IMyShipGrinder>χ=
new List<IMyShipGrinder>();φ.GetBlocksOfType(ψ,q=>q.CubeGrid==Me.CubeGrid);φ.GetBlocksOfType(χ,q=>q.CubeGrid==Me.CubeGrid);
if(ψ.Count>0){ƽ=Г.В;ύ="Miner mode enabled!";}else if(χ.Count>0){ƽ=Г.χ;ύ="Grinder mode enabled!";}else{ƽ=Г.Ͻ;ϼ(Ђ.Ͻ);}}if(ω
==ɚ.ə)μ=false;if(ω==ɚ.ɘ)ύ="Data restore failed!";if(ω==ɚ.Ƒ)ύ="New version, wipe data";}}IMyGridTerminalSystem φ;Vector3 υ=
new Vector3();Vector3 ɉ=new Vector3();Vector3 τ=new Vector3();Vector3 σ=new Vector3();Vector3 ς=new Vector3();DateTime ρ=
new DateTime();bool π=true;int ο=0;bool ξ=false;bool ν=false;bool μ=true;bool ƿ=false;bool λ=false;bool κ=false;float ι=0;
float ϊ=0;int θ=0;int Ƒ=0;int Ω=0;int Ψ=0;float Χ=0;float Φ=0;double Υ=0;float Τ=0;List<int>Σ=new List<int>();List<int>Ρ=new
List<int>();
// MODIFIED SECTION
// This makes script run every tick, but execute PAM only every 20 ticks (vs 10 by default), lowering average runtime
// Miners are a bit wobbly, but overall run ok
// It still allows all executions that come from command calls (UP/DOWN/etc.), regardless of which tick it happens on
int upcnt=0;
void Main(string Ƹ,UpdateType Π){
 bool isOnlyUpdate1 = ((Π & UpdateType.Update1) != 0 && ((Π & ~UpdateType.Update1) == 0));
 upcnt++; 
 if ((upcnt % 20 != 0) && isOnlyUpdate1) return;
// END MODIFIED SECTION
try{if(Ȥ!=null){ҕ();ȣ();return;}ν=(Π&UpdateType.Update1)!=0;if(ν)ο++;ξ=ο>=10;
if(ξ)ο=0;if(ν){ϑ++;if(ϑ>40)ϑ=0;Ω=Math.Max(0,10-(DateTime.Now-ρ).Seconds);}if(Ƹ!="")ȿ=Ƹ;if(ƽ!=Г.Б)Κ(Ƹ);else ǋ(Ƹ);Љ=false;try
{int Ξ=Runtime.CurrentInstructionCount;float J=µ(Ξ,Runtime.MaxInstructionCount);if(J>0.90)ύ="Max. instructions >90%";if(J
>Χ)Χ=J;if(Μ){Σ.Add(Ξ);while(Σ.Count>10)Σ.RemoveAt(0);Φ=0;for(int ä=0;ä<Σ.Count;ä++){Φ+=Σ[ä];}Φ=µ(µ(Φ,Σ.Count),Runtime.
MaxInstructionCount);double Ν=Runtime.LastRunTimeMs;if(κ&&Ν>Υ)Υ=Ν;Ρ.Add((int)(Ν*1000f));while(Ρ.Count>10)Ρ.RemoveAt(0);Τ=0;for(int ä=0;ä<Ρ.
Count;ä++){Τ+=Ρ[ä];}Τ=µ(Τ,Ρ.Count)/1000f;}}catch{Χ=0;}}catch(Exception e){Ȥ=e;}}bool Μ=false;bool Λ=false;void Κ(string Ƹ){
bool Ο=false;String Ι="";if(ɘ<=1&&!Ⱦ(Ƹ))ζ(Ƹ);if(ɭ!=null&&ɭ.HasPendingMessage){MyIGCMessage ǈ=ɭ.AcceptMessage();String ſ=(
string)ǈ.Data;String Ǌ="";if(ɘ<=1&&Ȼ(ref ſ,out Ǌ,out Ι)&&Ǌ==ɠ){ζ(ſ);Ο=true;}}bool η=κ&&Ҡ==Ҍ.Е&&!λ&&!Ο&&θ==0&&!ˌ;if(ξ&&Ҡ!=Ҍ.Е)Ο
=true;if((ν&&!η)||(ξ&&η)){if(θ==0&&(Ω<=0||π)){ƿ=ɘ>0;ɘ=0;θ=1;ø();Ѷ();ɫ();ö("Scan 1");}else if(θ==1){θ=2;ø();Ҁ();ö("Scan 2"
);}else if(θ==2){θ=0;ø();Ѻ();ö("Scan 3");ρ=DateTime.Now;if(ɘ<=1&&μ)υ=ȓ(Я,Я.CenterOfMass);μ=false;if(π){π=false;ҕ();}if(ƿ
&&ɘ==0)ύ="Setup complete";}else{if(ӄ==Ӎ.ӊ&&ƽ!=Г.Ͻ){ø();ѓ();ö("Inv balance");}ø();switch(Ƒ){case 0:Ĩ();break;case 1:ĩ();
break;case 2:Ħ();break;case 3:ĸ();break;case 4:ļ();break;case 5:Ĵ();break;case 6:E(Я);break;}ö("Update: "+Ƒ);Ƒ++;if(Ƒ>6){Ƒ=0;
κ=true;if(ɛ!=Ӎ.ӌ){switch(ɛ){case Ӎ.ӈ:Ҕ();break;case Ӎ.Ӈ:Ҕ();break;case Ӎ.ӊ:Ҕ();break;case Ӎ.ғ:Ҏ();break;case Ӎ.Ӊ:ҏ();
break;}ɛ=Ӎ.ӌ;}}}if(!π){if(!ƃ(Я,true)){Я=null;μ=true;ɘ=2;}if(ɘ>=2&&Ҡ!=Ҍ.Е)ҕ();if(ɘ<=1){ϊ=Я.CalculateShipMass().PhysicalMass;ι=
(float)Я.GetShipSpeed();ɉ=ȕ(Я,υ);τ=Я.WorldMatrix.Forward;σ=Я.WorldMatrix.Left;ς=Я.WorldMatrix.Down;ˆ();if(Ҡ!=Ҍ.Е){λ=false
;Ť(false);É(false);String Q=ϔ(Ҡ)+" "+(int)Ҡ;ø();ӗ();Й(false);ö(Q);ø();ć();ö("Thruster");ø();ĕ();ö("Gyroscope");}else{if(λ
){if(ü()){ì(ς,τ,σ,0.25f,true);ĕ();ύ="Aligning to planet: "+Math.Round(Ę-0.25f,2)+"°";if(Ĝ)ΰ(true,true);}else ΰ(true,true)
;}}Λ=false;}}}ø();ȟ();ö("Print");if(Ο||Ψ<=0){ø();ȸ(Ι);ö("Broadcast");Ψ=4;}else if(ξ)Ψ--;}void ζ(string Ƹ){if(Ƹ=="")return
;var ª=Ƹ.ToUpper().Split(' ');ª.DefaultIfEmpty("");var Ƽ=ª.ElementAtOrDefault(0);var Ʒ=ª.ElementAtOrDefault(1);var ε=ª.
ElementAtOrDefault(2);var δ=ª.ElementAtOrDefault(3);String ǅ="Invalid argument: "+Ƹ;bool γ=false;switch(Ƽ){case"UP":this.Ї(false);break;
case"DOWN":this.І(false);break;case"UPLOOP":this.Ї(true);break;case"DOWNLOOP":this.І(true);break;case"APPLY":this.Ϣ(true);
break;case"MRES":Є=0;break;case"STOP":this.ҕ();break;case"PATHHOME":{this.ҕ();this.ͳ();}break;case"PATH":{this.ҕ();this.ͳ();Α
.ʹ=true;}break;case"START":{this.ҕ();қ();}break;case"ALIGN":{ΰ(!λ,false);}break;case"CONT":{this.ҕ();this.Ҕ();}break;case
"JOBPOS":{this.ҕ();this.ҏ();}break;case"HOMEPOS":{this.ҕ();this.Ҏ();}break;case"FULL":{ƈ=true;}break;case"RESET":{ȵ=true;ɘ=2;}
break;default:γ=true;break;}if(ƽ!=Г.Ͻ){switch(Ƽ){case"SHUTTLE":{α();}break;case"CFGS":{if(!ή(Ʒ,ε,δ))ύ=ǅ;}break;case"CFGB":{if
(!ϸ(Ʒ,ε))ύ=ǅ;}break;case"CFGL":{if(!ʇ(ref ɸ,true,ʈ.ĥ,Ʒ,"")||!Ϲ(ε))ύ=ǅ;}break;case"CFGE":{if(!ʇ(ref ɶ,true,ʈ.ʎ,Ʒ,"IG")||!ʇ
(ref ɷ,true,ʈ.ʏ,ε,"IG")||!ʇ(ref ɵ,true,ʈ.ʍ,δ,"IG"))ύ=ǅ;}break;case"CFGA":{if(!ʇ(ref ɴ,false,ʈ.ª,Ʒ,""))ύ=ǅ;}break;case
"CFGW":{if(!ʇ(ref ɳ,false,ʈ.ʒ,Ʒ,"")||!ʇ(ref ɲ,false,ʈ.ʒ,ε,""))ύ=ǅ;}break;case"NEXT":{ͽ(false);}break;case"PREV":{ͽ(true);}
break;default:if(γ)ύ=ǅ;break;}}else{switch(Ƽ){case"UNDOCK":{Λ=true;}break;default:if(γ)ύ=ǅ;break;}}}String β(){String Q=
"\n\n"+"Run-arguments: (Type without:[ ])\n"+"———————————————\n"+"[UP] Menu navigation up\n"+"[DOWN] Menu navigation down\n"+
"[APPLY] Apply menu point\n\n"+"[UPLOOP] \"UP\" + looping\n"+"[DOWNLOOP] \"DOWN\" + looping\n"+"[PATHHOME] Record path, set home\n"+
"[PATH] Record path, use old home\n"+"[START] Start job\n"+"[STOP] Stop every process\n"+"[CONT] Continue last job\n"+"[JOBPOS] Move to job position\n"+
"[HOMEPOS] Move to home position\n\n"+"[FULL] Simulate ship is full\n"+"[ALIGN] Align the ship to planet\n"+"[RESET] Reset all data\n";if(ƽ!=Г.Ͻ)Q+=
"[SHUTTLE] Enable shuttle mode\n"+"[NEXT] Next hole\n"+"[PREV] Previous hole\n\n"+"[CFGS width height depth]*\n"+"[CFGB done damage]*\n"+
"[CFGL maxload weightLimit]*\n"+"[CFGE minUr minBat minHyd]*\n"+"[CFGW forward backward]*\n"+"[CFGA acceleration]*\n"+"———————————————\n"+
"*[CFGS] = Config Size:\n"+" e.g.: \"CFGS 5 3 20\"\n\n"+"*[CFGB] = Config Behaviour:\n"+" When done: [HOME,STOP]\n"+
" On Damage: [HOME,JOB,STOP,IG]\n"+" e.g.: \"CFGB HOME IG\"\n\n"+"*[CFGL] = Config max load:\n"+" maxload: [10..95]\n"+" weight limit: [On/Off]\n"+
" e.g.: \"CFGL 70 on\"\n\n"+"*[CFGE] = Config energy:\n"+" minUr (Uranium): [1..25, IG]\n"+" minBat (Battery): [5..30, IG]\n"+
" minHyd (Hydrogen): [10..90, IG]\n"+" e.g.: \"CFGE 20 10 IG\"\n\n"+"*[CFGW] = Config work speed:\n"+" fwd: [0.5..10]\n"+" bwd: [0.5..10]\n"+
" e.g.: \"CFGW 1.5 2\"\n\n"+"*[CFGA] = Config acceleration:\n"+" acceleration: [10..100]\n"+" e.g.: \"CFGA 80\"\n";else Q+=
"[UNDOCK] Leave current connector\n\n";return Q;}void α(){ҕ();ƽ=Г.Ͻ;ϼ(Ђ.Ͻ);ǝ.ʹ=false;Α.ʹ=false;Ю=null;Ъ.Clear();ӄ=Ӎ.ӌ;}void ΰ(bool Ϋ,bool ί){if(!Ϋ)ύ=
"Aligning canceled";if(ί)ύ="Aligning done";if(ί||!Ϋ){λ=false;î();â(false,0,0,0,0);return;}if(ü())λ=true;}bool ή(String έ,String ά,String Ϊ)
{bool Ϋ=ӄ==Ӎ.ӊ;int Y,X,Ȝ;if(int.TryParse(έ,out Y)&&int.TryParse(ά,out X)&&int.TryParse(Ϊ,out Ȝ)){this.ҕ();ʂ=Y;ʁ=X;ʀ=Ȝ;ʷ(
false);ү(false,false);if(Ϋ)Ҕ();return true;}return false;}bool Ϲ(String ʒ){if(ʒ=="ON"){ɾ=true;return true;}if(ʒ=="OFF"){ɾ=
false;return true;}return false;}bool ϸ(String Ȝ,String Ʀ){bool B=true;if(Ȝ=="HOME")ʄ=true;else if(Ȝ=="STOP")ʄ=false;else B=
false;if(Ʀ=="HOME")ʅ=ʮ.ʭ;else if(Ʀ=="STOP")ʅ=ʮ.ʫ;else if(Ʀ=="JOB")ʅ=ʮ.ʬ;else if(Ʀ=="IG")ʅ=ʮ.ʪ;else B=false;return B;}public
enum Ϸ{ϵ,ϴ,ϳ,ϲ,ϱ,ϰ,ϯ,Ϯ,ϭ,Ϭ,ϫ,Ƕ,ſ,Ϻ,ϻ,Ѝ,Ў,Ќ,Ћ}int[]Њ=new int[Enum.GetValues(Ϸ.ſ.GetType()).Length];bool Љ=false;void Ј(Ϸ ʺ){Њ
[(int)Ѓ]=Є;Є=Њ[(int)ʺ];if(ʺ==Ϸ.ϫ)Є=0;Ѓ=ʺ;if(ƽ!=Г.Б)Š(Ѓ==Ϸ.ϳ,false,0,0);Љ=true;}void Ї(bool ʸ){if(Є>0)Є--;else if(ʸ)Є=Ѕ-1;
}void І(bool ʸ){if(Є<Ѕ-1)Є++;else if(ʸ)Є=0;}int Ѕ=0;int Є=0;Ϸ Ѓ=Ϸ.ϵ;public enum Ђ{Ё,Ѐ,Ͽ,Ͼ,Ͻ}String ϼ(Ђ ƿ){switch(ƿ){case
Ђ.Ё:ύ="Job is running";break;case Ђ.Ѐ:ύ="Connector not ready!";break;case Ђ.Ͽ:ύ="Ship modified, path outdated!";break;
case Ђ.Ͼ:ύ="Interrupted by player!";break;case Ђ.Ͻ:ύ="Shuttle mode enabled!";break;}return"";}String Ϝ(ʱ Ϫ){switch(Ϫ){case ʱ
.ʰ:return"Top-Left";case ʱ.ʯ:return"Center";default:return"";}}String Ϝ(ʵ ϩ){switch(ϩ){case ʵ.ʳ:return"Auto"+(ƽ==Г.В?
" (Ore)":"");case ʵ.ʲ:return"Auto (+Stone)";case ʵ.ʴ:return"Default";default:return"";}}String ϛ(Ӎ Ϛ){switch(Ϛ){case Ӎ.ӌ:return
"No job";case Ӎ.Ӌ:return"Job paused";case Ӎ.ӊ:return"Job active";case Ӎ.ӈ:return"Job active";case Ӎ.Ӈ:return"Job active";case Ӎ.
ί:return"Job done";case Ӎ.ґ:return"Job changed";case Ӎ.ғ:return"Move home";case Ӎ.Ӊ:return"Move to job";}return"";}String
ϙ(ʮ Ϙ){switch(Ϙ){case ʮ.ʭ:return"Return home";case ʮ.ʬ:return"Fly to job pos";case ʮ.ʫ:return"Stop";case ʮ.ʪ:return
"Ignore";}return"";}String ϗ(ʩ Ț){switch(Ț){case ʩ.ɗ:return"Off";case ʩ.ʦ:return"Drop pos (Stone) ";case ʩ.ʥ:return
"Drop pos (Sto.+Ice)";case ʩ.ʨ:return"Cur. pos (Stone)";case ʩ.ʧ:return"Cur. pos (Sto.+Ice)";case ʩ.ʤ:return"In motion (Stone)";case ʩ.ʹ:
return"In motion (Sto.+Ice)";}return"";}String ϖ(Ж ϕ){switch(ϕ){case Ж.ɗ:return"No batteries";case Ж.Ũ:return"Charging";case Ж
.Д:return"Discharging";}return"";}String ϔ(Ҍ Ϛ){String Q=ƽ==Г.Ͻ?"target":"job";switch(Ϛ){case Ҍ.Е:return"Idle";case Ҍ.ʞ:
return"Flying to XY position";case Ҍ.Ғ:return ƽ==Г.χ?"Grinding":"Mining";case Ҍ.ҋ:return"Returning";case Ҍ.ң:return
"Flying to drop pos";case Ҍ.Ҝ:return"Returning to dock";case Ҍ.Ұ:return"Flying to dock area";case Ҍ.Ү:return"Flying to job area";case Ҍ.Ҧ:
return"Flying to path";case Ҍ.ҭ:return"Flying to job position";case Ҍ.Ҭ:return"Approaching dock";case Ҍ.Ґ:return"Docking";case
Ҍ.ҫ:return"Aligning";case Ҍ.Ҫ:return"Aligning";case Ҍ.ҩ:return"Retry docking";case Ҍ.Ҩ:return"Unloading";case Ҍ.ҡ:return
ƌ;case Ҍ.ҧ:return"Undocking";case Ҍ.Ũ:return"Charging batteries";case Ҍ.ĺ:return"Waiting for uranium";case Ҍ.Ľ:return
"Filling up hydrogen";case Ҍ.ҥ:return"Waiting for ejection";case Ҍ.Ҥ:return"Waiting for ejection";case Ҍ.Ң:return"Flying to drop pos";}return
"";}String ϒ(ƀ ų){switch(ų){case ƀ.ſ:return"On \"Undock\" command";case ƀ.ž:return"On player entered cockpit";case ƀ.ż:
return"On ship is full";case ƀ.Ż:return"On ship is empty";case ƀ.Ž:return"On time delay";case ƀ.Ź:return
"On batteries empty(<25%)";case ƀ.Ÿ:return"On batteries empty(=0%)";case ƀ.ź:return"On batteries full";case ƀ.Ŷ:return"On hydrogen empty(<25%)";
case ƀ.ŵ:return"On hydrogen empty(=0%)";case ƀ.ŷ:return"On hydrogen full";}return"";}int ϑ=0;int ϐ=0;int Ϗ=0;int ώ=0;String
ύ="";bool ό(ref String B,int ϓ,int ϋ,bool ƛ,String ŭ){Ѕ+=1;if(ϓ==ϋ)ŭ=">"+ŭ+(ϑ>=2?" .":"");else ŭ=" "+ŭ;B+=ŭ+"\n";return ϓ
==ϋ&&ƛ;}int Ϩ=0;int ϧ=0;int Ϧ=0;int ϥ=0;int Ϥ=0;int ϣ=0;String Ϣ(bool ƛ){int J=0;int ƴ=Є;Ѕ=0;String Ʃ="———————————————\n";
String ƶ="--------------------------------------------\n";String Ɨ="";Ɨ+=ϛ(ӄ)+" | "+(Α.ʹ?"Ready to dock":"No dock")+"\n";Ɨ+=Ʃ;
double ϡ=Math.Max(Math.Round(this.ԍ),0);if(Ѓ==Ϸ.ϵ){bool Q=ƽ==Г.Ͻ;if(ό(ref Ɨ,ƴ,J++,ƛ," Record path & set home"))ͳ();if(ƽ==Г.В)
if(ό(ref Ɨ,ƴ,J++,ƛ," Setup mining job"))Ј(Ϸ.ϳ);if(ƽ==Г.χ)if(ό(ref Ɨ,ƴ,J++,ƛ," Setup grinding job"))Ј(Ϸ.ϳ);if(ƽ==Г.Ͻ)if(ό(
ref Ɨ,ƴ,J++,ƛ," Setup shuttle job"))Ј(Ϸ.Ќ);if(ό(ref Ɨ,ƴ,J++,ƛ," Continue job"))Ҕ();if(ό(ref Ɨ,ƴ,J++,ƛ,
" Fly to home position"))Ҏ();if(ό(ref Ɨ,ƴ,J++,ƛ," Fly to job position"))ҏ();if(ό(ref Ɨ,ƴ,J++,ƛ," Behavior settings"))if(Q)Ј(Ϸ.Ў);else Ј(Ϸ.ϰ);if
(ό(ref Ɨ,ƴ,J++,ƛ," Info"))Ј(Ϸ.Ϯ);if(ƽ!=Г.Ͻ)if(ό(ref Ɨ,ƴ,J++,ƛ," Help"))Ј(Ϸ.Ϭ);}else if(Ѓ==Ϸ.ϳ){double Ϡ=Math.Round(ʂ*А,1)
;double ϟ=Math.Round(ʁ*Л,1);String Θ="";if(ό(ref Θ,ƴ,J++,ƛ," Start new job!"))қ();if(ό(ref Θ,ƴ,J++,ƛ,
" Change current job")){ү(false,false);Ј(Ϸ.ϵ);}if(ό(ref Θ,ƴ,J++,ƛ," Width + (Width: "+ʂ+" = "+Ϡ+"m)")){Œ(ref ʂ,5,20,1);ʷ(true);}if(ό(ref Θ,ƴ,
J++,ƛ," Width -")){Œ(ref ʂ,-5,20,-1);ʷ(true);}if(ό(ref Θ,ƴ,J++,ƛ," Height + (Height: "+ʁ+" = "+ϟ+"m)")){Œ(ref ʁ,5,20,1);ʷ
(true);}if(ό(ref Θ,ƴ,J++,ƛ," Height -")){Œ(ref ʁ,-5,20,-1);ʷ(true);}if(ό(ref Θ,ƴ,J++,ƛ," Depth + ("+(ɿ==ʵ.ʴ?"Depth":"Min"
)+": "+ʀ+"m)")){Œ(ref ʀ,5,50,2);ʷ(true);}if(ό(ref Θ,ƴ,J++,ƛ," Depth -")){Œ(ref ʀ,-5,50,-2);ʷ(true);}if(ό(ref Θ,ƴ,J++,ƛ,
" Depth mode: "+Ϝ(ɿ))){ɿ=ͻ(ɿ);}if(ό(ref Θ,ƴ,J++,ƛ," Start pos: "+Ϝ(ʃ))){ʃ=ͻ(ʃ);}if(ƽ==Г.χ&&ɿ==ʵ.ʲ)ɿ=ͻ(ɿ);Ɨ+=Ǧ(8,Θ,ƴ,ref Ϥ);}else if(Ѓ==
Ϸ.Ќ){float[]ʛ=new float[]{0,3,10,30,60,300,600,1200,1800};if(ό(ref Ɨ,ƴ,J++,ƛ," Next")){Ј(Ϸ.Ћ);}if(ό(ref Ɨ,ƴ,J++,ƛ," Back"
)){Ј(Ϸ.ϵ);}Ɨ+=" Leave connector 1:\n";if(ό(ref Ɨ,ƴ,J++,ƛ," - "+ϒ(ʓ.ų)))ʓ.ų=ͻ(ʓ.ų);if(!ʓ.Ɛ())Ɨ+="\n";else if(ό(ref Ɨ,ƴ,J++
,ƛ," - Delay: "+ǔ((int)ʓ.Ų)))ʓ.Ų=ʝ(ʓ.Ų,ʛ);Ɨ+=" Leave connector 2:\n";if(ό(ref Ɨ,ƴ,J++,ƛ," - "+ϒ(ʑ.ų)))ʑ.ų=ͻ(ʑ.ų);if(!ʑ.Ɛ(
))Ɨ+="\n";else if(ό(ref Ɨ,ƴ,J++,ƛ," - Delay: "+ǔ((int)ʑ.Ų)))ʑ.Ų=ʝ(ʑ.Ų,ʛ);}else if(Ѓ==Ϸ.Ћ){if(ό(ref Ɨ,ƴ,J++,ƛ,
" Start job!"))қ();if(ό(ref Ɨ,ƴ,J++,ƛ," Back")){Ј(Ϸ.Ќ);}Ɨ+=" Timer: \"Docking connector 1\":\n";if(ό(ref Ɨ,ƴ,J++,ƛ," = "+(ʓ.Ɔ!=""?ʓ.Ɔ
:"-")))ʓ.Ɔ=ʘ(ref Ϩ);Ɨ+=" Timer: \"Leaving connector 1\":\n";if(ό(ref Ɨ,ƴ,J++,ƛ," = "+(ʓ.ƕ!=""?ʓ.ƕ:"-")))ʓ.ƕ=ʘ(ref Ϧ);Ɨ+=
" Timer: \"Docking connector 2\":\n";if(ό(ref Ɨ,ƴ,J++,ƛ," = "+(ʑ.Ɔ!=""?ʑ.Ɔ:"-")))ʑ.Ɔ=ʘ(ref ϧ);Ɨ+=" Timer: \"Leaving connector 2\":\n";if(ό(ref Ɨ,ƴ,J++,ƛ,
" = "+(ʑ.ƕ!=""?ʑ.ƕ:"-")))ʑ.ƕ=ʘ(ref ϥ);}else if(Ѓ==Ϸ.ϲ){String Ϟ=ɸ+" %";if(ξ)ϐ++;if(ϐ>1){ϐ=0;Ϗ++;if(Ϗ>1)Ϗ=0;bool[]ϝ=new bool[]
{Ш.Count==0,Ī==Ж.ɗ,Ц.Count==0};int Å=0;while(true){Å++;ώ++;if(ώ>ϝ.Length-1)ώ=0;if(Å>=ϝ.Length)break;if(!ϝ[ώ])break;}}bool
Q=ƽ==Г.Ͻ;if(!Q&&ɾ&&Ɖ!=-1&&Ϗ==0)Ϟ=Ɖ<1000000?Math.Round(Ɖ)+" Kg":Math.Round(Ɖ/1000)+" t";if(ό(ref Ɨ,ƴ,J++,ƛ," Stop!")){ҕ();
Ј(Ϸ.ϵ);}if(ό(ref Ɨ,ƴ,J++,ƛ," Behavior settings"))if(!Q)Ј(Ϸ.ϰ);else Ј(Ϸ.Ў);if(!Q){if(ό(ref Ɨ,ƴ,J++,ƛ," Next hole"))ͽ(false
);}else if(ό(ref Ɨ,ƴ,J++,ƛ," Undock"))Λ=true;Ɨ+=ƶ;if(!Q)Ɨ+="Progress: "+Math.Round(Ӏ,1)+" %\n";Ɨ+="State: "+ϔ(Ҡ)+" "+ϡ+
"m \n";Ɨ+="Load: "+Ő+" % Max: "+Ϟ+" \n";if(ώ==0)Ɨ+="Uranium: "+(Ш.Count==0?"No reactors":Math.Round(ĺ,1)+"Kg "+(ɶ==-1?"":
" Min: "+ɶ+" Kg"))+"\n";if(ώ==1)Ɨ+="Battery: "+(Ī==Ж.ɗ?ϖ(Ī):ħ+"% "+(ɷ==-1||Q?"":" Min: "+ɷ+" %"))+"\n";if(ώ==2)Ɨ+="Hydrogen: "+(
Ц.Count==0?"No tanks":Math.Round(Ľ,1)+"% "+(ɵ==-1||Q?"":" Min: "+ɵ+" %"))+"\n";}else if(Ѓ==Ϸ.ϰ){String Θ="";if(ό(ref Θ,ƴ,
J++,ƛ," Back")){if(ӄ==Ӎ.ӊ)Ј(Ϸ.ϲ);else Ј(Ϸ.ϵ);}if(ό(ref Θ,ƴ,J++,ƛ," Max load: "+ɸ+"%"))ʇ(ref ɸ,ɸ<=80?-10:-5,ʈ.ĥ,false);if(
ό(ref Θ,ƴ,J++,ƛ," Weight limit: "+(ɾ?"On":"Off")))ɾ=!ɾ;if(ό(ref Θ,ƴ,J++,ƛ," Ejection: "+ϗ(ɺ))){ɺ=ͻ(ɺ);}if(ό(ref Θ,ƴ,J++,ƛ
," Toggle sorters: "+(ɼ?"On":"Off"))){ɼ=!ɼ;if(ɼ)Ū(ū);}if(ό(ref Θ,ƴ,J++,ƛ," Unload ice: "+(ɹ?"On":"Off")))ɹ=!ɹ;if(ό(ref Θ,
ƴ,J++,ƛ," Uranium: "+(ɶ==-1?"Ignore":"Min "+ɶ+"Kg")))ʇ(ref ɶ,(ɶ>5?-5:-1),ʈ.ʎ,true);if(ό(ref Θ,ƴ,J++,ƛ," Battery: "+(ɷ==-1
?"Ignore":"Min "+ɷ+"%")))ʇ(ref ɷ,-5,ʈ.ʏ,true);if(ό(ref Θ,ƴ,J++,ƛ," Hydrogen: "+(ɵ==-1?"Ignore":"Min "+ɵ+"%")))ʇ(ref ɵ,-10
,ʈ.ʍ,true);if(ό(ref Θ,ƴ,J++,ƛ," When done: "+(ʄ?"Return home":"Stop")))ʄ=!ʄ;if(ό(ref Θ,ƴ,J++,ƛ," On damage: "+ϙ(ʅ))){ʅ=ͻ(
ʅ);}if(ό(ref Θ,ƴ,J++,ƛ," Advanced..."))Ј(Ϸ.ϯ);Ɨ+=Ǧ(8,Θ,ƴ,ref ϣ);}else if(Ѓ==Ϸ.ϯ){if(ό(ref Ɨ,ƴ,J++,ƛ," Back")){if(ӄ==Ӎ.ӊ)Ј
(Ϸ.ϲ);else Ј(Ϸ.ϵ);}if(ό(ref Ɨ,ƴ,J++,ƛ,(ƽ==Г.χ?" Grinder":" Drill")+" inv. balancing: "+(ɽ?"On":"Off")))ɽ=!ɽ;if(ό(ref Ɨ,ƴ,
J++,ƛ," Enable"+(ƽ==Г.χ?" grinders":" drills")+": "+(ʆ?"Fwd + Bwd":"Fwd")))ʆ=!ʆ;if(ό(ref Ɨ,ƴ,J++,ƛ," Work speed fwd.: "+ɳ
+"m/s"))ʇ(ref ɳ,0.5f,ʈ.ʒ,false);if(ό(ref Ɨ,ƴ,J++,ƛ," Work speed bwd.: "+ɲ+"m/s"))ʇ(ref ɲ,0.5f,ʈ.ʒ,false);if(ό(ref Ɨ,ƴ,J++
,ƛ," Acceleration: "+Math.Round(ɴ*100f)+"%"+(ɴ>0.80f?" (risky)":""))){ʇ(ref ɴ,0.1f,ʈ.ª,false);}if(ό(ref Ɨ,ƴ,J++,ƛ,
" Width overlap: "+ɻ*100f+"%"))ʟ(true,0.05f);if(ό(ref Ɨ,ƴ,J++,ƛ," Height overlap: "+ɱ*100f+"%"))ʟ(false,0.05f);}else if(Ѓ==Ϸ.Ў){if(ό(ref Ɨ
,ƴ,J++,ƛ," Back")){if(ӄ==Ӎ.ӊ)Ј(Ϸ.ϲ);else Ј(Ϸ.ϵ);}if(ό(ref Ɨ,ƴ,J++,ƛ," Max load: "+ɸ+"%"))ʇ(ref ɸ,ɸ<=80?-10:-5,ʈ.ĥ,false);
if(ό(ref Ɨ,ƴ,J++,ƛ," Unload ice: "+(ɹ?"On":"Off")))ɹ=!ɹ;if(ό(ref Ɨ,ƴ,J++,ƛ," Uranium: "+(ɶ==-1?"Ignore":"Min "+ɶ+"Kg")))ʇ(
ref ɶ,(ɶ>5?-5:-1),ʈ.ʎ,true);if(ό(ref Ɨ,ƴ,J++,ƛ," Battery: "+(ɷ==-1?"Ignore":"Charge up")))ɷ=(ɷ==-1?1:-1);if(ό(ref Ɨ,ƴ,J++,ƛ
," Hydrogen: "+(ɵ==-1?"Ignore":"Fill up")))ɵ=(ɵ==-1?1:-1);if(ό(ref Ɨ,ƴ,J++,ƛ," On damage: "+ϙ(ʅ))){ʅ=ͻ(ʅ);}if(ό(ref Ɨ,ƴ,J
++,ƛ," Acceleration: "+Math.Round(ɴ*100f)+"%"+(ɴ>0.80f?" (risky)":""))){ʇ(ref ɴ,0.1f,ʈ.ª,false);}}else if(Ѓ==Ϸ.ϴ){double Ʌ
=0;if(ˊ.Count>0)Ʌ=Vector3.Distance(ˊ.Last().ɉ,ɉ);if(ό(ref Ɨ,ƴ,J++,ƛ," Stop path recording"))ˍ();if(ƽ!=Г.Ͻ){if(ό(ref Ɨ,ƴ,J
++,ƛ," Home: "+(Ύ?"Use old home":(Α.ʹ?"Was set! ":"none "))))Ύ=!Ύ;}else{if(ό(ref Ɨ,ƴ,J++,ƛ," Connector 1: "+(Ύ?
"Use old connector":(Α.ʹ?"Was set! ":"none "))))Ύ=!Ύ;if(ό(ref Ɨ,ƴ,J++,ƛ," Connector 2: "+(Ό?"Use old connector":(ǝ.ʹ?"Was set! ":"none ")))
)Ό=!Ό;}if(ό(ref Ɨ,ƴ,J++,ƛ," Path: "+(Ί?"Use old path":(ˊ.Count>1?"Count: "+ˊ.Count:"none "))))Ί=!Ί;Ɨ+=ƶ;Ɨ+="Wp spacing: "
+Math.Round(ˇ)+"m\n";}else if(Ѓ==Ϸ.ϱ){if(ό(ref Ɨ,ƴ,J++,ƛ," Stop")){ҕ();Ј(Ϸ.ϵ);}Ɨ+=ƶ;Ɨ+="State: "+ϔ(Ҡ)+" \n";Ɨ+="Speed: "+
Math.Round(ι,1)+"m/s\n";;Ɨ+="Target dist: "+ϡ+"m\n";Ɨ+="Wp count: "+ˊ.Count+"\n";Ɨ+="Wp left: "+ԋ+"\n";}else if(Ѓ==Ϸ.Ϯ){List
<IMyTerminalBlock>ʡ=Ĳ();if(ξ)ϐ++;if(ϐ>=ʡ.Count)ϐ=0;if(ό(ref Ɨ,ƴ,J++,ƛ," Next"))Ј(Ϸ.ϭ);Ɨ+=ƶ;Ɨ+="Version: "+VERSION+"\n";Ɨ
+="Ship load: "+Math.Round(Ő,1)+"% "+Math.Round(ŏ,1)+" / "+Math.Round(Ŕ,1)+"\n";Ɨ+="Uranium: "+(Ш.Count==0?"No reactors":
Math.Round(ĺ,1)+"Kg "+Ĺ)+"\n";Ɨ+="Battery: "+(Ī==Ж.ɗ?"":ħ+"% ")+ϖ(Ī)+"\n";Ɨ+="Hydrogen: "+(Ц.Count==0?"No tanks":Math.Round(
Ľ,1)+"% ")+"\n";Ɨ+="Damage: "+(ʡ.Count==0?"None":""+(ϐ+1)+"/"+ʡ.Count+" "+ʡ[ϐ].CustomName)+"\n";}else if(Ѓ==Ϸ.ϭ){if(ό(ref
Ɨ,ƴ,J++,ƛ," Back"))Ј(Ϸ.ϵ);Ɨ+=ƶ;Ɨ+="Next scan: "+Ω+"s\n";Ɨ+="Ship size: "+Math.Round(А,1)+"m "+Math.Round(Л,1)+"m "+Math.
Round(а,1)+"m \n";Ɨ+="Broadcast: "+(ɜ?"Online - "+ɮ:"Offline")+"\n";Ɨ+="Max Instructions: "+Math.Round(Χ*100f,1)+"% \n";}else
if(Ѓ==Ϸ.Ϭ){if(ό(ref Ɨ,ƴ,J++,ƛ," Back"))Ј(Ϸ.ϵ);Ɨ+=ƶ;Ɨ+="1. Dock to your docking station\n";Ɨ+=
"2. Select Record path & set home\n";Ɨ+="3. Fly the path to the ores\n";Ɨ+="4. Select stop path recording\n";Ɨ+="5. Align ship in mining direction\n";Ɨ+=
"6. Select Setup job and start\n";}if(ɘ==2)Ɨ="Fatal setup error\nNext scan: "+Ω+"s\n";if(ȵ)Ɨ="Recompile script now";int ä=Ɨ.Split('\n').Length;for(int ʠ=
ä;ʠ<=10;ʠ++)Ɨ+="\n";Ɨ+=Ʃ;Ɨ+="Last: "+ύ+"\n";return Ɨ;}void ʟ(bool ʞ,float ʖ){ҕ();ү(true,false);if(ʞ)ʇ(ref ɻ,ʖ,ʈ.ʌ,false);
else ʇ(ref ɱ,ʖ,ʈ.ʌ,false);ѱ();Š(true,true,0,0);}float ʝ(float ʜ,float[]ʛ){float B=ʛ[0];for(int ʚ=ʛ.Length-1;ʚ>=0;ʚ--)if(ʜ<ʛ[
ʚ])B=ʛ[ʚ];return B;}String ʘ(ref int F){String Q="";if(F>=Э.Count)F=-1;if(F>=0){Q=Э[F].CustomName;}F++;return Q;}void ʗ(
string õ){if(ӄ!=Ӎ.ӊ)return;if(õ=="")return;IMyTerminalBlock q=φ.GetBlockWithName(õ);if(q==null||!(q is IMyTimerBlock)){ύ=
"Timerblock "+õ+" not found!";return;}((IMyTimerBlock)q).Trigger();}void Œ(ref int ʖ,int ʕ,int ʙ,int ʔ){if(ʕ==0)return;if(ʖ<ʙ&&ʔ>0||ʖ
<=ʙ&&ʔ<0){ʖ+=ʔ;return;}int ʣ=Math.Abs(ʕ);int Ğ=0;int ʸ=1;while(true){Ğ+=ʸ*ʣ*10;if(ʕ<0&&ʖ-ʙ<=Ğ)break;if(ʕ>0&&ʖ-ʙ<Ğ)break;ʸ
++;}ʖ+=ʸ*ʕ;}void ʷ(bool ʶ){ʂ=Math.Max(ʂ,1);ʁ=Math.Max(ʁ,1);ʀ=Math.Max(ʀ,0);Š(Ѓ==Ϸ.ϳ,false,0,0);}public enum ʵ{ʴ,ʳ,ʲ}public
enum ʱ{ʰ,ʯ}public enum ʮ{ʭ,ʬ,ʫ,ʪ}public enum ʩ{ɗ,ʨ,ʧ,ʦ,ʥ,ʤ,ʹ}Ŵ ʓ=new Ŵ();Ŵ ʑ=new Ŵ();ʮ ʅ=ʮ.ʭ;bool ʄ=true;ʱ ʃ=ʱ.ʰ;int ʂ=3;int
ʁ=3;int ʀ=30;ʵ ɿ=ʵ.ʴ;bool ɾ=true;bool ɽ=true;bool ʆ=true;bool ɼ=false;ʩ ɺ=ʩ.ɗ;bool ɹ=true;float ɸ=90;float ɷ=20;float ɶ=5
;float ɵ=20;float ɴ=0.70f;float ɳ=1.50f;float ɲ=2.50f;float ɻ=0f;float ɱ=0f;public enum ʈ{ʒ,ª,ĥ,ʐ,ʏ,ʎ,ʍ,M,ʌ};bool ʇ(ref
float J,bool ʋ,ʈ ſ,String Q,String ʉ){if(Q=="")return false;float B=-1;bool ʊ=false;if(Q.ToUpper()==ʉ)ʊ=true;else if(!float.
TryParse(Q,out B))return false;else B=Math.Max(0,B);if(ʋ)B=(float)Math.Round(B);ʇ(ref J,B,ſ,ʊ,false);return true;}void ʇ(ref
float J,float Œ,ʈ ſ,bool ʉ){ʇ(ref J,J+Œ,ſ,ʉ,true);}void ʇ(ref float J,float ʺ,ʈ ſ,bool ʉ,bool Ά){float Ğ=0;float ȥ=0;if(ſ==ʈ.
ʒ){ȥ=0.5f;Ğ=10f;}if(ſ==ʈ.ª){ȥ=0.1f;Ğ=1f;}if(ſ==ʈ.ʐ){ȥ=50f;Ğ=100f;}if(ſ==ʈ.ʏ){ȥ=5f;Ğ=30f;}if(ſ==ʈ.ʎ){ȥ=1f;Ğ=25f;}if(ſ==ʈ.ĥ
){ȥ=10f;Ğ=95f;}if(ſ==ʈ.ʍ){ȥ=10f;Ğ=90f;}if(ſ==ʈ.M){ȥ=10f;Ğ=1800;}if(ſ==ʈ.ʌ){ȥ=0.0f;Ğ=0.75f;}if(ʺ==-1&&ʉ){J=-1;return;}if(J
==-1)ʉ=false;bool È=ʺ<ȥ||ʺ>Ğ;if(È&&Ά){if(ʺ<J)J=Ğ;else if(ʺ>J)J=ȥ;}else J=ʺ;if(È&&ʉ)J=-1;else J=Math.Max(ȥ,Math.Min(J,Ğ));J
=(float)Math.Round(J,2);}void ͽ(bool ͼ){if(ͼ)Ԓ=Math.Max(0,Ԓ-1);else Ԓ++;Й(true);}Ř ͻ<Ř>(Ř ͷ){int ª=Array.IndexOf(Enum.
GetValues(ͷ.GetType()),ͷ);ª++;if(ª>=ͺ(ͷ))ª=0;return(Ř)Enum.GetValues(ͷ.GetType()).GetValue(ª);}int ͺ<Ř>(Ř ͷ){return Enum.
GetValues(ͷ.GetType()).Length;}class Ͷ{public bool ʹ=false;public Vector3 ɉ=new Vector3();public Vector3 é=new Vector3();public
Vector3 Ï=new Vector3();public Vector3 Ė=new Vector3();public Vector3 Í=new Vector3();public Vector3 Έ=new Vector3();public
float Ή=0;public float Ζ=0;public float[]Η=null;public Ͷ(){}public Ͷ(Ͷ Ε){ʹ=Ε.ʹ;ɉ=Ε.ɉ;é=Ε.é;Ï=Ε.Ï;Ė=Ε.Ė;Í=Ε.Í;Έ=Ε.Έ;Η=Ε.Η;}
public Ͷ(Vector3 ɉ,Vector3 Ï,Vector3 é,Vector3 Ė,Vector3 Í){this.ɉ=ɉ;this.é=é;this.Ï=Ï;this.Ė=Ė;this.Ή=0;this.Í=Í;}public void
Δ(List<IMyThrust>Γ,List<string>Β){Η=new float[Β.Count];for(int J=0;J<Η.Length;J++)Η[J]=-1;for(int J=0;J<Γ.Count;J++){
string Q=N(Γ[J]);int F=Β.IndexOf(Q);if(F!=-1)Η[F]=µ(Γ[J].MaxEffectiveThrust,Γ[J].MaxThrust);}}}Ͷ Α=new Ͷ();Ͷ ΐ=new Ͷ();Ͷ Ώ=new
Ͷ();bool Ύ=false;bool Ό=false;bool Ί=false;void ͳ(){ˉ.Clear();for(int J=0;J<ˊ.Count;J++)ˉ.Add(ˊ[J]);ˊ.Clear();ˌ=true;ΐ=
new Ͷ(Α);Ώ=new Ͷ(ǝ);Α.ʹ=false;if(ƽ==Г.Ͻ)ǝ.ʹ=false;for(int J=0;J<Õ.Count;J++)if(!ˋ.Contains(Õ.Keys.ElementAt(J)))ˋ.Add(Õ.
Keys.ElementAt(J));Ύ=false;Ό=false;Ί=false;Ј(Ϸ.ϴ);}void ˍ(){if(Ύ)Α=ΐ;if(Ό)ǝ=Ώ;if(Ί){ˊ.Clear();for(int J=0;J<ˉ.Count;J++)ˊ.
Add(ˉ[J]);}ˌ=false;ҕ();Ј(Ϸ.ϵ);}bool ˌ=false;List<String>ˋ=new List<string>();List<Ͷ>ˊ=new List<Ͷ>();List<Ͷ>ˉ=new List<Ͷ>();
int ˈ=0;double ˇ=0;void ˆ(){if(!ˌ)return;if(Ҡ!=Ҍ.Е){ˍ();return;}if(!ΐ.ʹ)Ύ=false;if(!Ώ.ʹ)Ό=false;if(ˉ.Count<=1)Ί=false;
IMyShipConnector º=Ò(MyShipConnectorStatus.Connectable);if(º==null)º=Ò(MyShipConnectorStatus.Connected);if(º!=null){if(Math.Round(ι,2)<=
0.20)ˈ++;else ˈ=0;if(ˈ>=5){if(ƽ==Г.Ͻ&&(Α.ʹ||Ύ)&&Vector3.Distance(Α.ɉ,º.GetPosition())>5){ǝ.Ï=Я.WorldMatrix.Forward;ǝ.Ė=Я.
WorldMatrix.Left;ǝ.é=Я.WorldMatrix.Down;ǝ.Í=Я.GetNaturalGravity();ǝ.ɉ=º.GetPosition();ǝ.ʹ=true;ǝ.Έ=º.Position;}else{Α.Ï=Я.
WorldMatrix.Forward;Α.Ė=Я.WorldMatrix.Left;Α.é=Я.WorldMatrix.Down;Α.Í=Я.GetNaturalGravity();Α.ɉ=º.GetPosition();Α.ʹ=true;Α.Έ=º.
Position;}}}double ˁ=-1;if(ˊ.Count>0){ˁ=Vector3.Distance(ɉ,ˊ.Last().ɉ);}double Ġ=Math.Max(1.5,Math.Pow(ι/100.0,2));double ˀ=Math
.Max(ι*Ġ,2);ˇ=ˀ;if((ˁ==-1)||ˁ>=ˀ){Ͷ C=new Ͷ(ɉ,τ,ς,σ,Я.GetNaturalGravity());C.Δ(Γ,ˋ);ˊ.Add(C);}}int ʿ(Vector3 ó,int ʾ){if(
ʾ==-1)return 0;double ʽ=-1;int ʼ=-1;for(int J=ˊ.Count-1;J>=0;J--){double Ʌ=Vector3.Distance(ˊ[J].ɉ,ó);if(ʽ==-1||Ʌ<ʽ){ʼ=J;
ʽ=Ʌ;}}return Math.Sign(ʼ-ʾ);}bool ʻ(Vector3 ɉ){List<Vector3>B=new List<Vector3>();for(int J=0;J<ˊ.Count;J++){B.Add(ˊ[J].ɉ
);}if(Α.ʹ&&ˊ.Count>=1){Vector3 Ͳ=new Vector3();ˑ(Α,dockDist*а,false,out Ͳ);if(Vector3.Distance(Α.ɉ,ˊ.First().ɉ)<Vector3.
Distance(Α.ɉ,ˊ.Last().ɉ)){B.Insert(0,Ͳ);B.Insert(0,Α.ɉ);}else{B.Add(Ͳ);B.Add(Α.ɉ);}}if(ƽ==Г.Ͻ){if(ǝ.ʹ&&ˊ.Count>=1){Vector3 ͱ=new
Vector3();ˑ(ǝ,dockDist*а,false,out ͱ);if(Vector3.Distance(ǝ.ɉ,ˊ.First().ɉ)<Vector3.Distance(ǝ.ɉ,ˊ.Last().ɉ)){B.Insert(0,ͱ);B.
Insert(0,ǝ.ɉ);}else{B.Add(ͱ);B.Add(ǝ.ɉ);}}}else{if(ӄ!=Ӎ.ӌ)if(ˊ.Count>0&&Vector3.Distance(ǝ.ɉ,ˊ.First().ɉ)<Vector3.Distance(ǝ.ɉ
,ˊ.Last().ɉ))B.Insert(0,ǝ.ɉ);else B.Add(ǝ.ɉ);}int ʼ=-1;double Ͱ=-1;for(int J=0;J<B.Count;J++){double Ʌ=Vector3.Distance(B
[J],ɉ);if(Ʌ<Ͱ||Ͱ==-1){Ͱ=Ʌ;ʼ=J;}}if(B.Count==0)return false;double ˮ=Vector3.Distance(B[ʼ],ɉ);double ˬ=Vector3.Distance(B[
Math.Max(0,ʼ-1)],B[ʼ])*1.5f;double ˤ=Vector3.Distance(B[Math.Min(B.Count-1,ʼ+1)],B[ʼ])*1.5f;return ˮ<ˬ||ˮ<ˤ;}Ͷ ˣ=null;void ˢ
(Ͷ C,Ӎ ˡ){ˣ=C;if(ӄ==Ӎ.ӊ)ҟ=ˡ;}Ͷ ˠ(){if(ƽ!=Г.Ͻ)return Α;return ˣ;}bool ˑ(Ͷ ː,float Ʌ,bool ˏ,out Vector3 ˎ){if(ˏ){Vector3I ƍ
=new Vector3I((int)ː.Έ.X,(int)ː.Έ.Y,(int)ː.Έ.Z);IMySlimBlock ʢ=Me.CubeGrid.GetCubeBlock(ƍ);if(ʢ==null||!(ʢ.FatBlock is
IMyShipConnector)){ˎ=new Vector3();return false;}Vector3 ē=ȑ(Я,ʢ.FatBlock.GetPosition()-ɉ);Vector3 Џ=ȑ(Я,ʢ.FatBlock.WorldMatrix.Forward)
;ˎ=ː.ɉ-Ȏ(ː.Ï,ː.é*-1,ē)-Ȏ(ː.Ï,ː.é*-1,Џ)*Ʌ;return true;}else{ˎ=ː.ɉ;return true;}}Vector3 ҿ=new Vector3();bool Ҿ=false;
Vector3 ҽ(int Y,int X,bool Ҽ){if(!Ҽ&&Ҿ)return ҿ;float ĥ=((ӂ-1f)/2f)-Y;float M=((Ӂ-1f)/2f)-X;ҿ=ǝ.ɉ+ǝ.Ė*ĥ*А+Ӆ*-1*M*Л;Ҿ=true;
return ҿ;}Vector3 һ(Vector3 Һ,float ҹ){return Һ+(ӆ*ҹ);}public enum Ҹ{ҷ,Ҷ,Ґ,Ғ,ҵ,Ҵ}Ҹ ҳ(){float Ʌ=-1;Ҹ ª=Ҹ.ҷ;if(ƽ!=Г.Ͻ){if(ӄ!=Ӎ.ӌ
){Vector3 Ȝ=Ȑ(ӆ,Ӆ*-1,ɉ-ǝ.ɉ);if(Math.Abs(Ȝ.X)<=(float)(ӂ*А)/2f&&Math.Abs(Ȝ.Y)<=(float)(Ӂ*Л)/2f){if(Ȝ.Z<=-1&&Ȝ.Z>=-Ғ*2)
return Ҹ.Ғ;if(Ȝ.Z>-1&&Ȝ.Z<а*2)return Ҹ.ҷ;}if(ӎ(ǝ.ɉ,ref Ʌ))ª=Ҹ.ҷ;}if(Α.ʹ){if(ӎ(Α.ɉ,ref Ʌ))ª=Ҹ.Ґ;for(int J=0;J<ˊ.Count;J++){if(ӎ
(ˊ[J].ɉ,ref Ʌ))ª=Ҹ.Ҷ;}if(Vector3.Distance(ɉ,Α.ɉ)<dockDist*а)ª=Ҹ.Ґ;if(Ò(MyShipConnectorStatus.Connectable)!=null||Ò(
MyShipConnectorStatus.Connected)!=null)ª=Ҹ.Ґ;}}else{Vector3 ɉ=new Vector3();IMyShipConnector Å=Ò(MyShipConnectorStatus.Connected);if(Α.ʹ){if(
ӎ(Α.ɉ,ref Ʌ))ª=Ҹ.Ґ;if(ˑ(Α,dockDist,true,out ɉ))if(ӎ(ɉ,ref Ʌ))ª=Ҹ.Ґ;if(Å!=null&&Vector3.Distance(Å.GetPosition(),Α.ɉ)<5)
return Ҹ.ҵ;}for(int J=0;J<ˊ.Count;J++)if(Vector3.Distance(ˊ[J].ɉ,Α.ɉ)>dockDist*а&&Vector3.Distance(ˊ[J].ɉ,ǝ.ɉ)>dockDist*а)if(ӎ
(ˊ[J].ɉ,ref Ʌ))ª=Ҹ.Ҷ;if(ǝ.ʹ){if(ӎ(ǝ.ɉ,ref Ʌ))ª=Ҹ.ҷ;if(ˑ(ǝ,dockDist,true,out ɉ))if(ӎ(ɉ,ref Ʌ))ª=Ҹ.ҷ;if(Å!=null&&Vector3.
Distance(Å.GetPosition(),ǝ.ɉ)<5)return Ҹ.Ҵ;}}return ª;}bool ӎ(Vector3 û,ref float Ʌ){float Ȝ=Vector3.Distance(û,ɉ);if(Ȝ<Ʌ||Ʌ==-1
){Ʌ=Ȝ;return true;}return false;}public enum Ӎ{ӌ,Ӌ,ӊ,ί,ґ,ғ,Ӊ,ӈ,Ӈ}Ͷ ǝ=new Ͷ();Vector3 ӆ;Vector3 Ӆ;Ӎ ӄ=Ӎ.ӌ;ʱ Ӄ=ʱ.ʰ;int ӂ=0;
int Ӂ=0;double Ӏ=0;bool ұ=false;void қ(){if(ɘ>0){ύ="Setup error! Can't start";return;}if(ƽ==Г.Ͻ){Ҕ();return;}ǝ.ɉ=ɉ;ǝ.Í=Я.
GetNaturalGravity();ǝ.Ï=τ;ǝ.é=ς;ǝ.Ė=σ;ӆ=О.WorldMatrix.Forward;Ӆ=ǝ.é;if(ӆ==Я.WorldMatrix.Down)Ӆ=Я.WorldMatrix.Backward;ү(true,true);Ҟ(Ҍ.ʞ)
;Җ();}void ү(bool È,bool Қ){if(ӄ==Ӎ.ӌ&&!È)return;bool ҙ=È||ӄ==Ӎ.ί||ӂ!=ʂ||Ӂ!=ʁ||Ӄ!=ʃ;if(ҙ){if(ӄ!=Ӎ.ӌ){ӄ=Ӎ.ґ;ҽ(ԕ,ԓ,Қ);ύ=
"Job changed, lost progress";}Ӄ=ʃ;ӂ=ʂ;Ӂ=ʁ;ԓ=0;ԕ=0;ԁ=0;ԑ=0;Ԃ=0;Ԓ=0;Й(true);}}void Ҙ(){æ(ɉ,0);ř(Γ,true);}int җ=0;void Җ(){ϼ(Ђ.Ё);Ҙ();ŧ(Щ,false);Ū(ū);ӄ
=Ӎ.ӊ;É(true);ҟ=ӄ;Ј(Ϸ.ϲ);К();ұ=true;җ=0;for(int J=Х.Count-1;J>=0;J--)if(ƅ(Х[J],false))җ++;if(җ>0)ύ="Started with damage";}
void ҕ(){if(ӄ==Ӎ.ӊ){ӄ=Ӎ.Ӌ;ύ="Job paused";}Ҟ(Ҍ.Е);ҟ=ӄ;â(false,0,0,0,0);í();k(new Vector3(),false);î();ũ(ChargeMode.Auto);ŗ(
false);Ť(true);ө(Ҍ.Е);Š(false,false,0,0);ř(Ъ,false);ř(Ь,true);Ū(true);Ԍ=false;ұ=false;ƈ=false;Λ=false;if(Ѓ!=Ϸ.ϵ&&Ѓ!=Ϸ.ϰ&&Ѓ!=Ϸ
.ϯ&&Ѓ!=Ϸ.Ў)Ј(Ϸ.ϵ);}void Ҕ(){Ҹ ҍ=ҳ();if(ƽ==Г.Ͻ){if(!ǝ.ʹ||!Α.ʹ)return;Җ();bool ғ=Vector3.Distance(ɉ,Α.ɉ)<Vector3.Distance(ɉ
,ǝ.ɉ);if(ɛ==Ӎ.ӈ)ғ=true;if(ɛ==Ӎ.Ӈ)ғ=false;if(ғ){ˢ(Α,Ӎ.ӈ);switch(ҍ){case Ҹ.ҵ:Ҟ(Ҍ.ҡ);break;case Ҹ.Ҷ:Ҟ(Ҍ.Ү);break;case Ҹ.Ґ:Ҟ(
Ҍ.Ҭ);break;default:Ҟ(Ҍ.ҧ);break;}}else{ˢ(ǝ,Ӎ.Ӈ);switch(ҍ){case Ҹ.Ҵ:Ҟ(Ҍ.ҡ);break;case Ҹ.ҷ:Ҟ(Ҍ.Ҭ);break;case Ҹ.Ҷ:Ҟ(Ҍ.Ү);
break;default:Ҟ(Ҍ.ҧ);break;}}}else{if(ӄ!=Ӎ.Ӌ&&ӄ!=Ӎ.ґ)return;bool ґ=ӄ==Ӎ.ґ;Җ();bool Ґ=Ƈ(false)&&Α.ʹ;switch(ҍ){case Ҹ.ҷ:Ҟ(Ґ?Ҍ.Ҧ
:Ҍ.ʞ);break;case Ҹ.Ҷ:Ҟ(Ґ?Ҍ.Ұ:Ҍ.Ү);break;case Ҹ.Ґ:Ҟ(Ґ?Ҍ.Ҭ:Ҍ.Ҩ);break;case Ҹ.Ғ:{if(ԑ!=Ԓ||ґ)Ҟ(Ҍ.ҋ);else Ҟ(Ҍ.Ғ);}break;
default:break;}}}void ҏ(){if(ӄ==Ӎ.ӌ&&!Α.ʹ)return;if(ƽ==Г.Ͻ&&(!ǝ.ʹ||!Α.ʹ))return;ύ="Move to job";Ҹ ҍ=ҳ();if(ƽ==Г.Ͻ){ˢ(ǝ,Ӎ.Ӈ);
switch(ҍ){case Ҹ.ҷ:Ҟ(Ҍ.Ҭ);break;case Ҹ.Ҷ:Ҟ(Ҍ.Ү);break;case Ҹ.Ҵ:return;default:Ҟ(Ҍ.ҧ);break;}ө(Ҍ.ҡ);}else{switch(ҍ){case Ҹ.ҷ:Ҟ(
Ҍ.Ү);break;case Ҹ.Ҷ:Ҟ(Ҍ.Ү);break;case Ҹ.Ґ:Ҟ(Ҍ.Ҩ);break;case Ҹ.Ғ:Ҟ(Ҍ.ҋ);break;default:break;}if(ӄ==Ӎ.ӌ)ө(Ҍ.Ү);else ө(Ҍ.Ҫ);
Ԍ=true;}Ҙ();Ј(Ϸ.ϱ);ŧ(Щ,false);ҟ=Ӎ.Ӊ;}void Ҏ(){if(!Α.ʹ)return;ύ="Move home";Ҹ ҍ=ҳ();if(ƽ==Г.Ͻ){ˢ(Α,Ӎ.ӈ);switch(ҍ){case Ҹ.Ҷ
:Ҟ(Ҍ.Ұ);break;case Ҹ.Ґ:Ҟ(Ҍ.Ҭ);break;case Ҹ.ҵ:return;default:Ҟ(Ҍ.ҧ);break;}ө(Ҍ.ҡ);}else{if(Ò(MyShipConnectorStatus.
Connected)!=null)return;if(Ò(MyShipConnectorStatus.Connectable)!=null){Ҟ(Ҍ.Ґ);ө(Ҍ.Ҩ);return;}switch(ҍ){case Ҹ.ҷ:Ҟ(Ҍ.Ҧ);break;case
Ҹ.Ҷ:Ҟ(Ҍ.Ұ);break;case Ҹ.Ґ:Ҟ(Ҍ.Ұ);break;case Ҹ.Ғ:Ҟ(Ҍ.Ҝ);break;default:break;}ө(Ҍ.Ҩ);}Ҙ();Ј(Ϸ.ϱ);ŧ(Щ,false);ҟ=Ӎ.ғ;}public
enum Ҍ{Е,ʞ,Ғ,ҋ,Ҝ,Ұ,Ү,ҭ,Ҭ,Ґ,ҫ,Ҫ,ҩ,Ҩ,ҧ,Ҧ,Ũ,Ľ,ĺ,ҥ,Ҥ,ң,Ң,ҡ,}Ҍ Ҡ;Ӎ ҟ;void Ҟ(Ҍ Ҳ){if(Ҳ==Ҍ.Е)ӏ=Ҍ.Е;if(ӏ!=Ҍ.Е&&Ҡ==ӏ&&Ҳ!=ӏ){ҕ();
return;}ԏ=true;Ҡ=Ҳ;}Ҍ ӏ;void ө(Ҍ ӏ){this.ӏ=ӏ;}Ӽ ӽ=null;class Ӽ{public Ͷ ӻ=null;public List<Vector3>Ӻ=new List<Vector3>();
public float ӹ=0;public float Ӹ=0;public float ӷ=0;public float Ӷ=0;public Vector3 ӵ=new Vector3();}public enum Ӵ{ӳ,Ӳ,ӱ}int[]Ӱ
=null;Ӵ ӯ(int Ӯ,bool È){if(È){Ӱ=null;;ԕ=0;ԓ=0;}if(ʃ==ʱ.ʰ){int ӭ=Ӯ+1;ԓ=(int)Math.Floor(µ(Ӯ,ӂ));if(ԓ%2==0)ԕ=Ӯ-(ԓ*ӂ);else ԕ=
ӂ-1-(Ӯ-(ԓ*ӂ));if(ԓ>=Ӂ)return Ӵ.Ӳ;else return Ӵ.ӳ;}else if(ʃ==ʱ.ʯ){if(Ӱ==null)Ӱ=new int[]{0,-1,0,0};int Ӭ=(int)Math.
Ceiling(ӂ/2f);int ӫ=(int)Math.Ceiling(Ӂ/2f);int Ӫ=(int)Math.Floor(ӂ/2f);int Ӿ=(int)Math.Floor(Ӂ/2f);int ӿ=0;while(Ӱ[2]<Math.Pow
(Math.Max(ӂ,Ӂ),2)){if(ӿ>200)return Ӵ.ӱ;ӿ++;Ӱ[2]++;if(-Ӭ<ԕ&&ԕ<=Ӫ&&-ӫ<ԓ&&ԓ<=Ӿ){if(Ӱ[3]==Ӯ){this.ԕ=ԕ-1+Ӭ;this.ԓ=ԓ-1+ӫ;return
Ӵ.ӳ;}Ӱ[3]++;}if(ԕ==ԓ||(ԕ<0&&ԕ==-ԓ)||(ԕ>0&&ԕ==1-ԓ)){int Ԕ=Ӱ[0];Ӱ[0]=-Ӱ[1];Ӱ[1]=Ԕ;}ԕ+=Ӱ[0];ԓ+=Ӱ[1];}}return Ӵ.Ӳ;}int ԕ=0;
int ԓ=0;int Ԓ=0;int ԑ=0;int Ғ=30;int Ԑ=0;bool ԏ=true;Vector3 Ԏ;double ԍ=0;bool Ԍ=false;int ԋ=0;int Ԋ=0;int ԉ=0;int э=0;int
Ԉ=0;Vector3 ԇ=new Vector3();float Ԇ=0;float ԅ=0;float Ԅ=0;float ԃ=0;float Ԃ=0;float ԁ=0;bool Ԁ=false;bool Ө=false;bool ӛ=
false;bool ӧ=false;bool ә=false;DateTime Ž=new DateTime();Ͷ Ә=null;void ӗ(){if(Ҡ==Ҍ.ʞ){if(ԏ){Ԋ=0;if(ԑ!=Ԓ){ԁ=0;}ԑ=Ԓ;}if(Ԋ==0){
Ӵ B=ӯ(Ԓ,ԏ);if(B==Ӵ.Ӳ){ӄ=Ӎ.ί;ύ="Job done";if(ʄ&&Α.ʹ){Ҟ(Ҍ.Ҧ);ө(Ҍ.Ҩ);ҟ=Ӎ.ғ;}else{Ҟ(Ҍ.ҭ);ө(Ҍ.Ҫ);ҟ=Ӎ.Ӊ;}return;}if(B==Ӵ.ӳ){Ԋ=1
;ř(Ъ,true);Ԏ=ҽ(ԕ,ԓ,true);æ(Ԏ,10);ì(ǝ.é,ǝ.Ï,ǝ.Ė,false);}}else{if(ԍ<wpReachedDist){Ҟ(Ҍ.Ғ);return;}}}if(Ҡ==Ҍ.Ғ){if(ԏ){ř(Ъ,
true);Ū(false);Ԏ=ҽ(ԕ,ԓ,false);æ(һ(Ԏ,0),0);ì(ǝ.é,ǝ.Ï,ǝ.Ė,false);Ԋ=1;Ԇ=0;ԅ=0;ԃ=0;Ԅ=-1;Ғ=ʀ;Ԁ=true;}if(!ĳ()){Ҟ(Ҍ.Ҝ);return;}if(Ƈ
(true)){Ԉ=ő("","ORE",ŉ.ņ);if((ɺ==ʩ.ʦ||ɺ==ʩ.ʥ||ɺ==ʩ.ʤ||ɺ==ʩ.ʹ)&&ƽ!=Г.χ)Ҟ(Ҍ.ң);else if((ɺ==ʩ.ʨ||ɺ==ʩ.ʧ)&&ƽ!=Г.χ)Ҟ(Ҍ.ҥ);else
Ҟ(Ҍ.Ҝ);return;}Ԃ=Vector3.Distance(ɉ,Ԏ);if(Ԃ>ԁ){ԁ=Ԃ;Ԁ=false;}if(ƽ==Г.χ&&И()==MyDetectedEntityType.SmallGrid)ԅ+=2;else ԅ-=2
;ԅ=Math.Max(100,Math.Min(400,ԅ));if(Ԋ>0&&Ԋ<ԅ){if(Ԃ>Ԇ){if(ԅ>150)Ԇ=Ԃ;else Ԇ=(float)Math.Ceiling(Ԃ);Ԋ=1;}else Ԋ++;}else{if(Ԋ
>0){ύ="Ship stuck! Retrying";Ԇ=Ԃ;Ԋ=0;Š(false,true,0,а*sensorRange);}æ(һ(Ԏ,Math.Max(0,Ԇ-а)),З(false));if(ԍ<=wpReachedDist/
2){Ԋ=1;Ԇ=0;}return;}Š(false,true,а*sensorRange,0);Vector3 Ӗ=Ԏ+ӆ*Ԃ;bool ӕ=false;if(Vector3.Distance(Ӗ,ɉ)>0.3f){Vector3 Ӕ=Ԏ
+ӆ*(Ԃ+0.1f);æ(Ӕ,4);ӕ=true;}else{float ι=З(true);Vector3 Ӛ=һ(Ԏ,Math.Max(ʀ+1,Ԃ+1));æ(true,false,false,Ӛ,Ӛ-Ԏ,ι,ι);}bool ί=
false;if(ɿ==ʵ.ʲ||ɿ==ʵ.ʳ){if(!ӕ){float ӓ=0;foreach(IMyTerminalBlock q in Ъ)ӓ+=Ŏ(q,"","",ɿ==ʵ.ʳ?new string[]{"STONE"}:null);if(
ӓ>Ԅ||Ԃ<ʀ||Ԁ){ԉ=0;ԃ=Ԃ;Ғ=(int)(Math.Max(Ғ,ԃ)+а/2);}else{ί=Ԃ-ԃ>2&&ԉ>=20;ԉ++;}Ԅ=ӓ;}}else ί=Ԃ>=Ғ;if(ԑ!=Ԓ){Ҟ(Ҍ.ҋ);Ԃ=0;return;}
if(ί){Ԓ++;Ҟ(Ҍ.ҋ);Ԃ=0;return;}}if(Ҡ==Ҍ.Ң){bool ί=false;if(ԏ){Ū(true);if((ɺ==ʩ.ʦ||ɺ==ʩ.ʥ)&&ü()&&Ȱ(ӆ,Я.GetNaturalGravity())<
25&&ӂ>=2&&Ӂ>=2){Vector3 Ӓ=ɉ;if(ԕ>0&&ԓ<Ӂ-1)Ӓ=ҽ(ԕ-1,ԓ+1,true);else if(ԕ<ӂ-1&&ԓ<Ӂ-1)Ӓ=ҽ(ԕ+1,ԓ+1,true);else if(ԕ<ӂ-1&&ԓ>0)Ӓ=ҽ(
ԕ+1,ԓ-1,true);else if(ԕ>0&&ԓ>0)Ӓ=ҽ(ԕ-1,ԓ-1,true);else ί=true;if(!ί)æ(Ӓ,10);}else ί=true;}if(ԍ<wpReachedDist/2)ί=true;if(ί
){Ҟ(Ҍ.Ҥ);return;}}if(Ҡ==Ҍ.ҥ||Ҡ==Ҍ.Ҥ){if(ԏ){æ(true,true,false,ɉ,0);ř(Ъ,false);Ū(true);Ԋ=-1;ԅ=ɺ==ʩ.ʤ||ɺ==ʩ.ʹ?0:-1;}bool B=!
ĳ();int ń=ő("STONE","ORE",ŉ.ņ);if(ɺ==ʩ.ʧ||ɺ==ʩ.ʹ||ɺ==ʩ.ʥ)ń+=ő("ICE","ORE",ŉ.ņ);bool ӑ=ń>0;bool ɘ=false;if(ԅ>=0){float Y=(
float)Math.Sin(Ȩ(ԅ))*А/3f;float X=(float)Math.Cos(Ȩ(ԅ))*Л/3f;Vector3 Ӑ=ҽ(ԕ,ԓ,true)+Ȏ(ӆ,Ӆ*-1,new Vector3(Y,X,0));æ(Ӑ,0.3f);if(
ԍ<Math.Min(А,Л)/10f)ԅ+=5f;if(ԅ>=360)ԅ=0;}if(Ԋ==-1||ń<Ԋ){Ԋ=ń;Ԇ=0;}else{Ԇ++;if(Ԇ>50)ɘ=true;}if(!ӑ||B||ɘ){if(!B){int Ӝ=ő("",
"ORE",ŉ.ņ);if(Ƈ(true))B=true;else if(100-(µ(Ӝ,Ԉ)*100)<minEjection){B=true;}else ϼ(Ђ.Ё);}if(ɘ&&B)ύ="Ejection failed";if(Ҡ==Ҍ.Ҥ
){if(B){if(Α.ʹ)Ҟ(Ҍ.Ҧ);else{ҕ();ҏ();ύ="Can´t return, no dock found";}}else Ҟ(Ҍ.ʞ);}else if(B)Ҟ(Ҍ.Ҝ);else Ҟ(Ҍ.Ғ);return;}}
if(Ҡ==Ҍ.ҋ||Ҡ==Ҍ.Ҝ||Ҡ==Ҍ.ң){if(ԏ){Ԏ=ҽ(ԕ,ԓ,false);ì(ǝ.é,ǝ.Ï,ǝ.Ė,false);ř(Ъ,ʆ);Ū(false);Ԇ=Vector3.Distance(ɉ,Ԏ);Š(false,true,
0,а*sensorRange);}æ(Ԏ,З(false));if(Vector3.Distance(ɉ,Ԏ)>=Ԇ+5){ř(Ъ,false);Ū(true);ύ="Can´t return!";}if(ԍ<wpReachedDist){
if(Ҡ==Ҍ.ҋ&&Ԍ)Ҟ(Ҍ.ҭ);if(Ҡ==Ҍ.ҋ)Ҟ(Ҍ.ʞ);if(Ҡ==Ҍ.ң)Ҟ(Ҍ.Ң);if(Ҡ==Ҍ.Ҝ){if(Α.ʹ)Ҟ(Ҍ.Ҧ);else{ҕ();ҏ();ύ=
"Can´t return, no dock found";}}return;}}if(Ҡ==Ҍ.Ҧ){if(ԏ){Ū(true);ř(Ъ,false);int F=-1;double ӟ=-1;for(int J=ˊ.Count-1;J>=0;J--){double Ʌ=Vector3.
Distance(ˊ[J].ɉ,ɉ);if(ӟ==-1||Ʌ<ӟ){F=J;ӟ=Ʌ;}}if(F==-1){Ҟ(Ҍ.Ұ);return;}ċ=ˊ[F].ɉ;æ(ċ,10);ì(ǝ.é,ǝ.Ï,ǝ.Ė,false);}if(ԍ<wpReachedDist){
Ҟ(Ҍ.Ұ);return;}}if(Ҡ==Ҍ.Ұ||Ҡ==Ҍ.Ү){if(Ҡ==Ҍ.Ү&&ӄ==Ӎ.ӊ&&ƽ!=Г.Ͻ){if(!ĳ()||Ƈ(true)){Ҟ(Ҍ.Ұ);return;}}bool ί=false;bool Ӧ=false
;bool ӥ=false;float Ӥ=0;bool ӣ=false;Ͷ C=null;if(ԏ){if(Ҡ==Ҍ.Ұ||ƽ==Г.Ͻ){Ͷ Ҋ=ˠ();ӽ=new Ӽ();ӽ.ӻ=Ҋ;ӽ.ӹ=followPathDock*а;ӽ.Ӹ=
useDockDirectionDist*а;ӽ.ӷ=10;ӽ.Ӻ.Add(Ҋ.ɉ);Vector3 Ӣ=new Vector3();if(ˑ(Ҋ,dockDist*а,true,out Ӣ))ӽ.Ӻ.Add(Ӣ);else ӽ.ӹ*=1.5f;if(ƽ==Г.Ͻ){if(Ҋ==
Α)ӽ.ӵ=ǝ.ɉ;if(Ҋ==ǝ)ӽ.ӵ=Α.ɉ;ӽ.Ӷ=dockDist*а*1.1f;}}else if(Ҡ==Ҍ.Ү){ӽ=new Ӽ();ӽ.ӻ=ǝ;ӽ.ӹ=followPathJob*а;ӽ.Ӹ=
useJobDirectionDist*а;ӽ.ӷ=10;ӽ.ӵ=Α.ɉ;ӽ.Ӷ=dockDist*а*1.1f;ӽ.Ӻ.Add(ǝ.ɉ);if(ӄ==Ӎ.ӌ){if(!Α.ʹ||ˊ.Count==0){ҕ();return;}float ӡ=Vector3.Distance(
ˊ.First().ɉ,Α.ɉ);float Ӡ=Vector3.Distance(ˊ.Last().ɉ,Α.ɉ);if(ӡ<Ӡ)ӽ.ӻ=ˊ.Last();else ӽ.ӻ=ˊ.First();}}ԇ=new Vector3();ӣ=!ʻ(ɉ
);ř(Ъ,false);Ū(true);Ԑ=-1;double ӟ=-1;for(int J=ˊ.Count-1;J>=0;J--){if(Vector3.Distance(ˊ[J].ɉ,ӽ.ӵ)<=ӽ.Ӷ)continue;double
Ʌ=Vector3.Distance(ˊ[J].ɉ,ɉ);if(ӟ==-1||Ʌ<ӟ){Ԑ=J;ӟ=Ʌ;}}э=ʿ(ӽ.ӻ.ɉ,Ԑ);Ә=null;}б(ˊ,э,ӽ.Ӻ,ӽ.ӹ,ԏ,ref Ԋ);for(int J=0;J<ӽ.Ӻ.Count
;J++){float Ʌ=Vector3.Distance(ɉ,ӽ.Ӻ[J]);if(Ʌ<=ӽ.ӹ)ί=true;if(Ʌ<=ӽ.Ӹ)Ӧ=true;}if(Ӧ)Ӥ=ӽ.ӷ;float Ӟ=Ә!=null?Ә.Ή:ι;float ӝ=(
float)Math.Max(ι*0.1f*а,wpReachedDist);if((ԍ<ӝ)||ԏ){if(!ԏ)Ԑ+=э;if(э==0||Ԑ>ˊ.Count-1||Ԑ<0)ί=true;else{ԋ=э>0?ˊ.Count-1-Ԑ:Ԑ;C=ˊ[
Ԑ];Ә=C;if(Ԑ>=1&&Ԑ<ˊ.Count-1)ԇ=C.ɉ-ˊ[Ԑ-э].ɉ;else Ә=null;ċ=C.ɉ;ӥ=true;}}if(Ӧ)ì(ӽ.ӻ.é,ӽ.ӻ.Ï,ӽ.ӻ.Ė,false);else if(ӣ)ê(ӽ.ӻ.é,
10,true);else if(ӥ&&C!=null)if(э>0)ì(C.é,C.Ï,C.Ė,90,false);else ì(C.é,-C.Ï,-C.Ė,90,false);æ(true,false,true,ċ,ԇ,Ә==null?0:
Ә.Ή,Ӥ);if(ί){ԋ=0;if(Ҡ==Ҍ.Ұ||ƽ==Г.Ͻ){Ҟ(Ҍ.Ҭ);return;}if(Ҡ==Ҍ.Ү&&Ԍ){Ҟ(Ҍ.ҭ);return;}if(Ҡ==Ҍ.Ү){Ҟ(Ҍ.ʞ);return;}}}if(Ҡ==Ҍ.Ҭ||Ҡ
==Ҍ.ҩ){Ͷ Ҋ=ˠ();if(ԏ){if(!ˑ(Ҋ,dockDist*а,true,out ċ)){ϼ(Ђ.Ѐ);ҕ();return;}æ(ċ,0);ê(Ҋ.é,90,true);}if(ԍ<followPathDock*а&&ԍ!=-
1){æ(ċ,10);ì(Ҋ.é,Ҋ.Ï,Ҋ.Ė,false);}if(Ò(MyShipConnectorStatus.Connectable)!=null||Ò(MyShipConnectorStatus.Connected)!=null)
{Ҟ(Ҍ.Ґ);return;}if(ԍ<wpReachedDist/2&&ԍ!=-1){Ҟ(Ҍ.ҫ);return;}}if(Ҡ==Ҍ.ҫ||Ҡ==Ҍ.Ҫ){if(ԏ){if(Ҡ==Ҍ.ҫ){Ͷ Ҋ=ˠ();if(!ˑ(Ҋ,dockDist
*а,true,out ċ)){ϼ(Ђ.Ѐ);ҕ();return;}æ(true,true,false,ċ,0);ì(Ҋ.é,Ҋ.Ï,Ҋ.Ė,10,false);}if(Ҡ==Ҍ.Ҫ){ì(ǝ.é,ǝ.Ï,ǝ.Ė,0.5f,false);ċ
=ǝ.ɉ;æ(true,true,false,ċ,0);}}if(Ĝ){â(false,0,0,0,0);if(Ҡ==Ҍ.ҫ)Ҟ(Ҍ.Ґ);if(Ҡ==Ҍ.Ҫ)ҕ();return;}}if(Ҡ==Ҍ.Ґ){if(Ò(
MyShipConnectorStatus.Connected)!=null){if(ƽ==Г.Ͻ)Ҟ(Ҍ.ҡ);else Ҟ(Ҍ.Ҩ);return;}Ͷ Ҋ=ˠ();if(ԏ){ԅ=0;Ž=DateTime.Now;Ԋ=0;ì(Ҋ.é,Ҋ.Ï,Ҋ.Ė,false);}
Vector3I н=new Vector3I((int)Ҋ.Έ.X,(int)Ҋ.Έ.Y,(int)Ҋ.Έ.Z);IMySlimBlock ʢ=Me.CubeGrid.GetCubeBlock(н);float л=dockingSpeed;float
к=dockingSpeed*5;float й=Math.Max(1.5f,Math.Min(5f,а*0.15f));if(!ˑ(Ҋ,0,true,out ċ)||!ˑ(Ҋ,й,true,out ԇ)||ʢ==null||!ʢ.
FatBlock.IsFunctional){ϼ(Ђ.Ѐ);ҕ();return;}if(ԅ==1||(Vector3.Distance(ɉ,ċ)<=й*1.1f&&!ԏ))ԅ=1;else{Vector3 и=ȑ(Я,ԇ-ɉ);Vector3 з=ȑ(Я
,Я.GetNaturalGravity());float Ƶ=å(и,з,null);л=Math.Min(к,Ƶ);}æ(true,false,false,ċ,ċ-ɉ,dockingSpeed,л);if(ԏ)Ԇ=(float)ԍ;
IMyShipConnector Å=Ò(MyShipConnectorStatus.Connectable);if(Å!=null){æ(false,false,false,ċ,0);if(Ԋ>0)Ԋ=0;Ԋ--;if(Ԋ<-5){Å.Connect();if(Å.
Status==MyShipConnectorStatus.Connected){if(ƽ==Г.Ͻ)Ҟ(Ҍ.ҡ);else Ҟ(Ҍ.Ҩ);í();ŧ(Щ,true);return;}}}else{float Ȝ=(float)Math.Round(ԍ
,1);if(Ȝ<Ԇ){Ԋ=-1;Ԇ=Ȝ;}else Ԋ++;if(Ԋ>20){Ҟ(Ҍ.ҩ);return;}}}if(Ҡ==Ҍ.Ҩ||Ҡ==Ҍ.ҡ||Ҡ==Ҍ.ĺ||Ҡ==Ҍ.Ľ||Ҡ==Ҍ.Ũ){bool ж=false;bool м=
false;if(ƽ==Г.Ͻ){if(ˠ()==Α)ж=true;else if(ˠ()==ǝ)м=true;}if(ԏ){ƈ=false;if(Ò(MyShipConnectorStatus.Connected)==null){Ҟ(Ҍ.ҧ);
return;}í();if(ж)ʗ(ʓ.Ɔ);if(м)ʗ(ʑ.Ɔ);Ө=false;ә=false;ӧ=false;ӛ=false;}if(Ò(MyShipConnectorStatus.Connected)==null){ҕ();ϼ(Ђ.Ͼ);
return;}if(ӄ!=Ӎ.ӊ||ɷ==-1||Ī==Ж.ɗ)ә=true;else if(ħ>=100f)ә=true;else if(ħ<=99f)ә=false;if(ӄ!=Ӎ.ӊ||ɵ==-1||Ц.Count==0)ӧ=true;else
if(Ľ>=100f)ӧ=true;else if(Ľ<=99)ӧ=false;if(ӄ!=Ӎ.ӊ||ɶ==-1||Ш.Count==0)ӛ=true;else ӛ=ĺ>=ɶ;Ŵ Ǝ=null;if(ж)Ǝ=ʓ;if(м)Ǝ=ʑ;if(Ǝ!=
null&&(Ǝ.ų==ƀ.Ź||Ǝ.ų==ƀ.Ÿ))ә=true;if(Ǝ!=null&&(Ǝ.ų==ƀ.ź))if(!Ө)ә=false;if(Ǝ!=null&&(Ǝ.ų==ƀ.Ŷ||Ǝ.ų==ƀ.ŵ))ӧ=true;if(Ǝ!=null&&(
Ǝ.ų==ƀ.ŷ))if(!Ө)ӧ=false;if(ξ){ChargeMode е=ә?ChargeMode.Auto:ChargeMode.Recharge;if(Ǝ!=null&&(Ǝ.ų==ƀ.Ÿ||Ǝ.ų==ƀ.Ź))е=
ChargeMode.Discharge;ũ(е);ŗ(!ӧ);}if(!Ө){if(ƽ==Г.Ͻ)Ө=ӄ!=Ӎ.ӊ||Ƌ(ԏ,true)||Λ;else Ө=ӄ!=Ӎ.ӊ||š();}else{if(!ә)Ҟ(Ҍ.Ũ);if(!ӧ)Ҟ(Ҍ.Ľ);if(!ӛ)
Ҟ(Ҍ.ĺ);ԏ=false;}if(Ө&&ә&&ӧ&&ӛ){ũ(ChargeMode.Auto);ŗ(false);if(ӄ==Ӎ.ӊ){if(ƽ==Г.Ͻ){if(ˠ()==Α)ʗ(ʓ.ƕ);else if(ˠ()==ǝ)ʗ(ʑ.ƕ);
if(ˠ()==Α)ˢ(ǝ,Ӎ.Ӈ);else ˢ(Α,Ӎ.ӈ);}}Ҟ(Ҍ.ҧ);return;}}if(Ҡ==Ҍ.ҧ){if(ԏ){IMyShipConnector Å=Ò(MyShipConnectorStatus.Connected);
if(Å==null){Ҟ(Ҍ.Ү);return;}IMyShipConnector д=Å.OtherConnector;ř(Å,false);ŧ(Щ,false);Ͷ C=null;if(Vector3.Distance(Å.
GetPosition(),Α.ɉ)<5f&&Α.ʹ)C=Α;if(Vector3.Distance(Å.GetPosition(),ǝ.ɉ)<5f&&ǝ.ʹ)C=ǝ;if(C!=null){if(!ˑ(C,dockDist*а,true,out ċ)){ϼ(Ђ
.Ѐ);ҕ();return;}æ(ċ,5);ì(C.é,C.Ï,C.Ė,false);}else æ(ɉ+д.WorldMatrix.Forward*dockDist*а,5);if(ӄ==Ӎ.ӊ)ϼ(Ђ.Ё);}if(ԍ<
wpReachedDist){ř(Ь,true);Ҟ(Ҍ.Ү);return;}}if(Ҡ==Ҍ.ҭ){if(ԏ){Ū(true);ř(Ъ,false);ċ=ǝ.ɉ;æ(ċ,20);ì(ǝ.é,ǝ.Ï,ǝ.Ė,false);}if(ԍ<wpReachedDist/2
){Ҟ(Ҍ.Ҫ);return;}}ԏ=false;}class г{public г(Vector3 в,float Ʌ){this.в=в;this.Ʌ=Ʌ;}public Vector3 в;public float Ʌ;}void б
(List<Ͷ>ˊ,int э,List<Vector3>ы,float Ʌ,bool È,ref int Њ){if(È){for(int ä=0;ä<ˊ.Count;ä++)ˊ[ä].Ή=0;Њ=-1;return;}if(э==0)
return;int ъ=э*-1;if(Њ==-1)Њ=ъ>0?1:ˊ.Count-2;int ś=0;while(Њ>=1&&Њ<ˊ.Count-1){if(ś>50)return;ś++;try{if((ъ<0&&Њ>=1)||(ъ>0&&Њ<=
ˊ.Count-2)){Ͷ ʜ=ˊ[Њ];bool щ=false;for(int ʠ=0;ʠ<ы.Count;ʠ++){if(Vector3.Distance(ʜ.ɉ,ы[ʠ])<=Ʌ){щ=true;break;}}if(!щ){Ͷ ш=
ˊ[Њ-ъ];Ͷ ч=ˊ[Њ+ъ];Vector3 ц=ʜ.ɉ-ч.ɉ;Vector3 х=ш.ɉ-ʜ.ɉ;Vector3 ф=ʜ.ɉ+Vector3.Normalize(ц)*х.Length();Vector3 у=ш.ɉ-ф;
Vector3 т=Ȑ(э>0?ʜ.Ï:ʜ.Ï*-1,ʜ.é*-1,у);Vector3 с=Ȑ(э>0?ʜ.Ï:ʜ.Ï*-1,ʜ.é*-1,х);Vector3 р=Ȑ(э>0?ʜ.Ï:ʜ.Ï*-1,ʜ.é*-1,ʜ.Í);ʜ.Ή=(float)
Math.Sqrt(Math.Pow(ш.Ή,2)+Math.Pow(å(-с,р,ʜ),2));for(int ʠ=0;ʠ<ы.Count;ʠ++)if(Vector3.Distance(ш.ɉ,ы[ʠ])<=Ʌ){Vector3 п=Ȑ(э>0
?ʜ.Ï:ʜ.Ï*-1,ʜ.é*-1,ы[ʠ]-ʜ.ɉ);float о=å(-п,р,ʜ);ʜ.Ή=Math.Min(ʜ.Ή,о)/2f;}if(т.Length()==0)т=new Vector3(0,0,1);Vector3 ь=Ȑ(
ʜ.Ï,ʜ.é*-1,ʜ.Í);float ã=Â(т,ь,ʜ);float ª=µ(ã,ϊ);float M=(float)Math.Sqrt(т.Length()*1.0f/(0.5f*ª));ʜ.Ή=Math.Min(ʜ.Ή,(х.
Length()/M)*ɴ);}}}catch{return;}Њ+=ъ;}Њ=-1;}void Й(bool È){if(È){Ӏ=0;return;}if(ƽ==Г.Ͻ)return;float ʜ=Ԓ*Math.Max(1,ʀ);if(ԑ==Ԓ)
ʜ+=Math.Min(ʀ,Ԃ);float ņ=ӂ*Ӂ*Math.Max(1,ʀ);Ӏ=Math.Max(Ӏ,(float)Math.Min(ʜ/ņ*100.0,100));}MyDetectedEntityType И(){try{if(
ƃ(Ю,true)&&!Ю.LastDetectedEntity.IsEmpty())return Ю.LastDetectedEntity.Type;}catch{};return MyDetectedEntityType.None;}
float З(bool Ï){if(ƽ==Г.χ&&И()==MyDetectedEntityType.None&&!ƅ(Ю,true))return fastSpeed;else return Ï?ɳ:ɲ;}public enum Ж{ɗ,Ũ,Е
,Д}public enum Г{Ǻ,В,χ,Б,Ͻ}Г ƽ=Г.Ǻ;int ɘ=0;float А=0;float Л=0;float а=0;IMyRemoteControl Я;IMySensorBlock Ю;List<
IMyTimerBlock>Э=new List<IMyTimerBlock>();List<IMyShipConnector>Ь=new List<IMyShipConnector>();List<IMyThrust>Γ=new List<IMyThrust>()
;List<IMyGyro>Ы=new List<IMyGyro>();List<IMyTerminalBlock>Ъ=new List<IMyTerminalBlock>();List<IMyLandingGear>Щ=new List<
IMyLandingGear>();List<IMyReactor>Ш=new List<IMyReactor>();List<IMyConveyorSorter>Ч=new List<IMyConveyorSorter>();List<IMyGasTank>Ц=
new List<IMyGasTank>();List<IMyTerminalBlock>Х=new List<IMyTerminalBlock>();List<IMyTerminalBlock>Ф=new List<
IMyTerminalBlock>();List<IMyTerminalBlock>У=new List<IMyTerminalBlock>();List<IMyBatteryBlock>Т=new List<IMyBatteryBlock>();List<
IMyTextPanel>С=new List<IMyTextPanel>();List<IMyTextSurface>Р=new List<IMyTextSurface>();List<IMyTextPanel>П=new List<IMyTextPanel>(
);IMyTerminalBlock О=null;bool Н(IMyTerminalBlock q)=>q.CubeGrid==Me.CubeGrid;void К(){φ.GetBlocksOfType(Х,Н);}void М(){П
.Clear();for(int J=С.Count-1;J>=0;J--){String ǧ=С[J].CustomData.ToUpper();bool ѵ=false;if(ǧ==Ȣ){ѵ=true;Μ=true;}if(ǧ==ȡ)ѵ=
true;if(ѵ){П.Add(С[J]);С.RemoveAt(J);}}ŕ(П,false,1,false);}void Ѵ(List<IMyTerminalBlock>Ĭ){Р.Clear();for(int J=0;J<Ĭ.Count;J
++){IMyTerminalBlock q=Ĭ[J];try{String ї=pamTag.Substring(0,pamTag.Length-1)+":";int F=q.CustomName.IndexOf(ї);int ѳ=-1;if
(F<0||!int.TryParse(q.CustomName.Substring(F+ї.Length,1),out ѳ))continue;if(ѳ==-1)continue;ѳ--;IMyTextSurfaceProvider Ѳ=(
IMyTextSurfaceProvider)q;if(ѳ<Ѳ.SurfaceCount&&ѳ>=0){Р.Add(Ѳ.GetSurface(ѳ));}}catch{}}}void ѱ(){if(Я==null)return;О=null;float Ѱ=0,ѯ=0,Ѯ=0,ѭ=0,
Ѭ=0,ѫ=0;List<IMyTerminalBlock>Ѫ=ћ(Ъ,pamTag,true);bool ѩ=Ѫ.Count==0;if(Ѫ.Count>0)О=Ѫ[0];else if(Ъ.Count>0)О=Ъ[0];int ś=0;
for(int J=0;J<Ъ.Count;J++){if(Ъ[J].WorldMatrix.Forward!=О.WorldMatrix.Forward){if(ѩ){ɘ=2;ύ="Mining direction is unclear!";
return;}continue;}ś++;Vector3 Ѩ=ȓ(Я,Ъ[J].GetPosition());if(J==0){Ѱ=Ѩ.X;ѯ=Ѩ.X;Ѯ=Ѩ.Y;ѭ=Ѩ.Y;Ѭ=Ѩ.Z;ѫ=Ѩ.Z;}ѯ=Math.Max(Ѩ.X,ѯ);Ѱ=Math
.Min(Ѩ.X,Ѱ);ѭ=Math.Max(Ѩ.Y,ѭ);Ѯ=Math.Min(Ѩ.Y,Ѯ);ѫ=Math.Max(Ѩ.Z,ѫ);Ѭ=Math.Min(Ѩ.Z,Ѭ);}А=(ѯ-Ѱ)*(1-ɻ)+drillRadius*2;Л=(ѭ-Ѯ)*
(1-ɱ)+drillRadius*2;if(О!=null&&О.WorldMatrix.Forward==Я.WorldMatrix.Down)Л=(ѫ-Ѭ)*(1-ɱ)+drillRadius*2;}void Ѷ(){if(ȵ){ɘ=2
;return;}List<IMyRemoteControl>ѷ=new List<IMyRemoteControl>();List<IMySensorBlock>ҁ=new List<IMySensorBlock>();List<
IMyTerminalBlock>ƣ=new List<IMyTerminalBlock>();φ.GetBlocksOfType(ѷ,Н);φ.GetBlocksOfType(С,Н);φ.GetBlocksOfType(ҁ,Н);φ.
SearchBlocksOfName(pamTag.Substring(0,pamTag.Length-1)+":",ƣ,q=>q.CubeGrid==Me.CubeGrid&&q is IMyTextSurfaceProvider);С=ћ(С,pamTag,true);М
();Ѵ(ƣ);ŕ(С,setLCDFontAndSize,1.4f,false);ŕ(Р,setLCDFontAndSize,1.4f,true);List<IMySensorBlock>Q=ћ(ҁ,pamTag,true);if(Q.
Count>0)Ю=Q[0];else Ю=null;if(ƽ==Г.В){φ.GetBlocksOfType(Ъ,q=>q.CubeGrid==Me.CubeGrid&&q is IMyShipDrill);if(Ъ.Count==0){ɘ=1;ύ
="Drills are missing";}}else if(ƽ==Г.χ){φ.GetBlocksOfType(Ъ,q=>q.CubeGrid==Me.CubeGrid&&q is IMyShipGrinder);if(Ъ.Count==
0){ɘ=1;ύ="Grinders are missing";}if(ƽ==Г.χ&&Ю==null){ɘ=1;ύ="Sensor is missing";}}else if(ƽ==Г.Ͻ){φ.GetBlocksOfType(Э,q=>q
.CubeGrid==Me.CubeGrid);}List<IMyRemoteControl>ķ=ћ(ѷ,pamTag,true);if(ķ.Count>0)ѷ=ķ;if(ѷ.Count>0)Я=ѷ[0];else{Я=null;ɘ=2;ύ=
"Remote is missing";return;}а=(float)Я.CubeGrid.WorldVolume.Radius*2;О=null;if(ƽ!=Г.Ͻ){ѱ();if(Ъ.Count>0&&О!=null){if(Ю!=null&&(О.
WorldMatrix.Forward!=Ю.WorldMatrix.Forward||!(Я.WorldMatrix.Forward==Ю.WorldMatrix.Up||Я.WorldMatrix.Down==Ю.WorldMatrix.Down))){ɘ=
1;ύ="Wrong sensor direction";}if(О.WorldMatrix.Forward!=Я.WorldMatrix.Forward&&О.WorldMatrix.Forward!=Я.WorldMatrix.Down)
{ɘ=2;ύ="Wrong remote direction";}}}}void Ҁ(){φ.GetBlocksOfType(Щ,Н);φ.GetBlocksOfType(Ь,Н);φ.GetBlocksOfType(Γ,Н);φ.
GetBlocksOfType(Ы,Н);φ.GetBlocksOfType(Т,Н);φ.GetBlocksOfType(Ш,Н);φ.GetBlocksOfType(Ц,q=>q.CubeGrid==Me.CubeGrid&&q.BlockDefinition.
ToString().ToUpper().Contains("HYDROGEN"));φ.GetBlocksOfType(Ч,Н);if(Me.CubeGrid.GridSizeEnum==MyCubeSize.Small)Ь=ћ(Ь,
"ConnectorMedium",false);else Ь=ћ(Ь,"Connector",false);List<IMyShipConnector>ѿ=ћ(Ь,pamTag,true);if(ѿ.Count>0)Ь=ѿ;if(ɘ<=1){if(Ь.Count==0){
ɘ=1;ύ="Connector is missing";}if(Ы.Count==0){ɘ=1;ύ="Gyros are missing";}if(Γ.Count==0){ɘ=1;ύ="Thrusters are missing";}}
List<IMyConveyorSorter>Ѿ=ћ(Ч,pamTag,true);if(Ѿ.Count>0)Ч=Ѿ;List<IMyLandingGear>ѽ=ћ(Щ,pamTag,true);if(ѽ.Count>0)Щ=ѽ;for(int J
=0;J<Щ.Count;J++)Щ[J].AutoLock=false;List<IMyBatteryBlock>Ѽ=ћ(Т,pamTag,true);if(Ѽ.Count>0)Т=Ѽ;List<IMyGasTank>ѻ=ћ(Ц,
pamTag,true);if(ѻ.Count>0)Ц=ѻ;}void Ѻ(){φ.GetBlocksOfType(У,q=>q.CubeGrid==Me.CubeGrid&&q.InventoryCount>0);Ф.Clear();for(int
J=У.Count-1;J>=0;J--){if(Ѹ(У[J])){Ф.Add(У[J]);У.RemoveAt(J);}}}bool ѹ(IMyTerminalBlock q){if(q.InventoryCount==0)return
false;if(ƽ==Г.Ͻ)return true;for(int ä=0;ä<Ъ.Count;ä++){IMyTerminalBlock M=Ъ[ä];if(M==null||!ƃ(M,true)||M.InventoryCount==0)
continue;if(!checkConveyorSystem||M.GetInventory(0).IsConnectedTo(q.GetInventory(0))){return true;}}return false;}bool Ѹ(
IMyTerminalBlock q){if(q is IMyCargoContainer)return true;if(q is IMyShipDrill)return true;if(q is IMyShipGrinder)return true;if(q is
IMyShipConnector){if(((IMyShipConnector)q).ThrowOut)return false;if(Me.CubeGrid.GridSizeEnum!=MyCubeSize.Large&&ј(q,"ConnectorSmall",
false))return false;else return true;}return false;}List<Ř>ћ<Ř>(List<Ř>Ĭ,String њ,bool љ){List<Ř>B=new List<Ř>();for(int J=0;
J<Ĭ.Count;J++)if(ј(Ĭ[J],њ,љ))B.Add(Ĭ[J]);return B;}bool ј<Ř>(Ř ō,String ї,bool љ){IMyTerminalBlock q=(IMyTerminalBlock)ō;
if(љ&&q.CustomName.ToUpper().Contains(ї.ToUpper()))return true;if(!љ&&q.BlockDefinition.ToString().ToUpper().Contains(ї.
ToUpper()))return true;return false;}Dictionary<String,float>ѕ=new Dictionary<String,float>();int є=0;void ѓ(){if(!ɽ)return;if(
Ъ.Count<=1)return;float ђ=0;float ё=0;for(int J=0;J<Ъ.Count;J++){IMyTerminalBlock ѐ=Ъ[J];if(ƅ(ѐ,true))continue;ђ+=(float)
ѐ.GetInventory(0).MaxVolume;ё+=(float)ѐ.GetInventory(0).CurrentVolume;}float я=(float)Math.Round(µ(ё,ђ),5);for(int J=0;J<
Math.Max(1,Math.Floor(Ъ.Count/10f));J++){float і=0;float ю=0;float ќ=0;float ѧ=0;IMyTerminalBlock Ğ=null;IMyTerminalBlock ȥ=
null;for(int ä=0;ä<Ъ.Count;ä++){IMyTerminalBlock ѐ=Ъ[ä];if(ƅ(ѐ,true))continue;float Ѧ=(float)ѐ.GetInventory(0).MaxVolume;
float ѥ=µ((float)ѐ.GetInventory(0).CurrentVolume,Ѧ);if(Ğ==null||ѥ>і){Ğ=ѐ;і=ѥ;ќ=Ѧ;}if(ȥ==null||ѥ<ю){ȥ=ѐ;ю=ѥ;ѧ=Ѧ;}}if(Ğ==null||
ȥ==null||Ğ==ȥ)return;if(checkConveyorSystem&&!Ğ.GetInventory(0).IsConnectedTo(ȥ.GetInventory(0))){if(є>20)ύ=
"Inventory balancing failed";else є++;return;}є=0;List<MyInventoryItem>Ŋ=new List<MyInventoryItem>();Ğ.GetInventory(0).GetItems(Ŋ);float џ=0;if(Ŋ.
Count==0)continue;MyInventoryItem ĵ=Ŋ[0];String Ѥ=ĵ.Type.TypeId+ĵ.Type.SubtypeId;if(!ѕ.TryGetValue(Ѥ,out џ)){if(ѡ(Ğ.
GetInventory(0),0,ȥ.GetInventory(0),out џ)){ѕ.Add(Ѥ,џ);}else{return;}}float ѣ=((і-я)*ќ/џ);float Ѣ=((я-ю)*ѧ/џ);int ń=(int)Math.Min(Ѣ,
ѣ);if(ń<=0)return;if((float)ĵ.Amount<ń)Ğ.GetInventory(0).TransferItemTo(ȥ.GetInventory(0),0,null,null,ĵ.Amount);else Ğ.
GetInventory(0).TransferItemTo(ȥ.GetInventory(0),0,null,null,ń);}}bool ѡ(IMyInventory ǟ,int F,IMyInventory Ѡ,out float џ){џ=0;float
ў=(float)ǟ.CurrentVolume;List<MyInventoryItem>ѝ=new List<MyInventoryItem>();ǟ.GetItems(ѝ);float ƻ=0;for(int J=0;J<ѝ.Count
;J++)ƻ+=(float)ѝ[J].Amount;ǟ.TransferItemTo(Ѡ,F,null,null,1);float ҝ=ў-(float)ǟ.CurrentVolume;ѝ.Clear();ǟ.GetItems(ѝ);
float ɰ=0;for(int J=0;J<ѝ.Count;J++)ɰ+=(float)ѝ[J].Amount;if(ҝ==0f||!Ȧ(0.9999,ƻ-ɰ,1.0001)){return false;}џ=ҝ;return true;}
float Ŏ(IMyTerminalBlock ō,String õ,String Ń,String[]Ō){float B=0;for(int ä=0;ä<ō.InventoryCount;ä++){IMyInventory ŋ=ō.
GetInventory(ä);List<MyInventoryItem>Ŋ=new List<MyInventoryItem>();ŋ.GetItems(Ŋ);foreach(MyInventoryItem ĵ in Ŋ){if(Ō!=null&&(Ō.
Contains(ĵ.Type.TypeId.ToUpper())||Ō.Contains(ĵ.Type.SubtypeId.ToUpper())))continue;if((õ==""||ĵ.Type.TypeId.ToUpper()==õ)&&(Ń==
""||ĵ.Type.SubtypeId.ToUpper()==Ń))B+=(float)ĵ.Amount;}}return B;}public enum ŉ{ň,Ň,ņ}class Ņ{public String õ="";public
String Ń="";public int ń=0;public ŉ į=ŉ.ņ;public Ņ(String õ,String Ń,int ń,ŉ į){this.õ=õ;this.Ń=Ń;this.ń=ń;this.į=į;}}Ņ œ(
String õ,String Ń,ŉ į,bool Œ){õ=õ.ToUpper();Ń=Ń.ToUpper();for(int J=0;J<ł.Count;J++){Ņ ĵ=ł[J];if(ĵ.õ.ToUpper()==õ&&ĵ.Ń.ToUpper
()==Ń&&(ĵ.į==į||į==ŉ.ņ))return ĵ;}Ņ B=null;if(Œ){B=new Ņ(õ,Ń,0,į);ł.Add(B);}return B;}int ő(String õ,String Ń,ŉ į){return
ő(õ,Ń,į,null);}int ő(String õ,String Ń,ŉ į,String[]Ō){int ń=0;õ=õ.ToUpper();Ń=Ń.ToUpper();;for(int J=0;J<ł.Count;J++){Ņ ĵ
=ł[J];if(Ō!=null&&Ō.Contains(ĵ.õ.ToUpper()))continue;if((õ==""||ĵ.õ.ToUpper()==õ)&&(Ń==""||ĵ.Ń.ToUpper()==Ń)&&(ĵ.į==į||į
==ŉ.ņ))ń+=ĵ.ń;}return ń;}float Ő=0;float ŏ=0;float Ŕ=0;List<Ņ>ł=new List<Ņ>();void ŀ(IMyTerminalBlock q,ŉ į){for(int J=0;J
<q.InventoryCount;J++){List<MyInventoryItem>Į=new List<MyInventoryItem>();q.GetInventory(J).GetItems(Į);for(int ä=0;ä<Į.
Count;ä++){œ(Į[ä].Type.SubtypeId,Į[ä].Type.TypeId.Replace("MyObjectBuilder_",""),į,true).ń+=(int)Į[ä].Amount;}}}void ĭ(List<Ņ
>Ĭ){for(int ī=Ĭ.Count-1;ī>0;ī--){for(int J=0;J<ī;J++){Ņ ª=Ĭ[J];Ņ q=Ĭ[J+1];if(ª.ń<q.ń)Ĭ.Move(J,J+1);}}}void ĩ(){try{ł.
Clear();for(int J=0;J<Ф.Count;J++){IMyTerminalBlock q=Ф[J];if(!ƃ(q,true))continue;ŀ(q,ŉ.ň);}if(ɺ!=ʩ.ɗ){for(int J=0;J<У.Count;
J++){IMyTerminalBlock q=У[J];if(!ƃ(q,true))continue;ŀ(q,ŉ.Ň);}}ĭ(ł);}catch(Exception e){Ȥ=e;}}void Ĩ(){Ŕ=0;ŏ=0;try{for(
int J=0;J<Ф.Count;J++){IMyTerminalBlock q=Ф[J];if(!ƃ(q,true))continue;ŏ+=(float)q.GetInventory(0).CurrentVolume;Ŕ+=(float)q
.GetInventory(0).MaxVolume;}Ő=(float)Math.Min(Math.Round(µ(ŏ,Ŕ)*100,1),100.0);}catch(Exception e){Ȥ=e;}}float ħ=0;Ж Ī;
void Ħ(){float ı=0,Ł=0,Ŀ=0,ľ=0;for(int J=0;J<Т.Count;J++){IMyBatteryBlock q=Т[J];if(!ƃ(q,true))continue;ı+=q.MaxStoredPower;
Ł+=q.CurrentStoredPower;Ŀ+=q.CurrentInput;ľ+=q.CurrentOutput;}ħ=(float)Math.Round(µ(Ł,ı)*100,1);if(Ŀ>=ľ)Ī=Ж.Ũ;else Ī=Ж.Д;
if(Ŀ==0&&ľ==0||ħ==100.0)Ī=Ж.Е;if(Т.Count==0)Ī=Ж.ɗ;}float Ľ=0;void ļ(){float Ļ=0;for(int J=0;J<Ц.Count;J++){IMyGasTank q=Ц[
J];if(!ƃ(q,true))continue;Ļ+=(float)q.FilledRatio;}Ľ=µ(Ļ,Ц.Count)*100f;}float ĺ=0;String Ĺ="";void ĸ(){ĺ=0;try{for(int J=
0;J<Ш.Count;J++){IMyReactor ķ=Ш[J];if(!ƃ(ķ,true))continue;List<MyInventoryItem>Ĭ=new List<MyInventoryItem>();ķ.
GetInventory(0).GetItems(Ĭ);float Ķ=0;for(int ä=0;ä<Ĭ.Count;ä++){MyInventoryItem ĵ=Ĭ[ä];if(ĵ.Type.SubtypeId.ToUpper()=="URANIUM"&&ĵ.
Type.TypeId.ToUpper().Contains("_INGOT"))Ķ+=(float)ĵ.Amount;}if(Ķ<ĺ||J==0){ĺ=Ķ;Ĺ=ķ.CustomName;}}}catch(Exception e){Ȥ=e;}}
void Ĵ(){if(ұ){if(Ĳ().Count>җ){ұ=false;if(ʅ!=ʮ.ʪ){ҕ();if(ʅ==ʮ.ʬ)ҏ();if(ʅ==ʮ.ʭ)if(Α.ʹ)Ҏ();else ҏ();}ύ="Damage detected";}}}
bool ĳ(){if(!κ)return true;if(ӄ==Ӎ.ӊ){if(ɷ>0&&Ī!=Ж.ɗ){if(ħ<=ɷ){ύ="Low energy! Move home";return false;}}if(ɶ>0&&Ш.Count>0){
if(ĺ<=ɶ){ύ="Low fuel: "+Ĺ;return false;}}if(ɵ>0&&Ц.Count>0){if(Ľ<=ɵ){ύ="Low hydrogen";return false;}}}return true;}List<
IMyTerminalBlock>Ĳ(){List<IMyTerminalBlock>İ=new List<IMyTerminalBlock>();for(int J=0;J<Х.Count;J++){IMyTerminalBlock q=Х[J];if(ƅ(q,
false))İ.Add(q);}return İ;}bool ƅ(IMyTerminalBlock ō,bool Ƃ){return(!ƃ(ō,Ƃ)||!ō.IsFunctional);}bool ƃ(IMyTerminalBlock q,bool
Ƃ){if(q==null)return false;try{IMyCubeBlock Ɓ=Me.CubeGrid.GetCubeBlock(q.Position).FatBlock;if(Ƃ)return Ɓ==q;else return
Ɓ.GetType()==q.GetType();}catch{return false;}}public enum ƀ{ſ,ž,Ž,ż,Ż,ź,Ź,Ÿ,ŷ,Ŷ,ŵ}class Ŵ{public ƀ ų=ƀ.ſ;public float Ų=
0;public float Ƅ=0;public string Ɔ="";public string ƕ="";DateTime Ɩ;public bool Ɣ=false;private bool Ɠ=false;public bool
ƒ(bool È){if(È){Ƅ=0;Ɠ=false;return false;}Ɠ=true;return Ƅ>Ų;}public void È(){ƒ(true);Ɣ=false;}public void Ƒ(){if(Ɠ)if((
DateTime.Now-Ɩ).TotalSeconds>1){Ƅ++;Ɩ=DateTime.Now;}}public bool Ɛ(){switch(ų){case ƀ.ž:return true;case ƀ.Ž:return true;}return
false;}}bool Ə(Ŵ Ǝ,bool È,bool Ɗ){if(È)Ǝ.È();Ǝ.Ƒ();bool B=false;String Q="";switch(Ǝ.ų){case ƀ.ſ:{Q="Waiting for command";B=
false;break;}case ƀ.ż:{Q="Waiting for cargo";B=Ƈ(true);break;}case ƀ.Ż:{Q="Unloading";B=š();break;}case ƀ.Ž:{B=true;break;}
case ƀ.ź:{Q="Charging batteries";B=ħ>=100f;break;}case ƀ.Ź:{Q="Discharging batteries";B=ħ<=25f;break;}case ƀ.Ÿ:{Q=
"Discharging batteries";B=ħ<=0f;break;}case ƀ.ŷ:{Q="Filling up hydrogen";B=Ľ>=100f;break;}case ƀ.Ŷ:{Q="Unloading hydrogen";B=Ľ<=25f;break;}case
ƀ.ŵ:{Q="Unloading hydrogen";B=Ľ<=0f;break;}case ƀ.ž:{bool ƍ=ţ();if(!ƍ)Ǝ.Ɣ=true;B=Ǝ.Ɣ&&ƍ;Q="Waiting for passengers";break;
}}if(!B)Ǝ.ƒ(true);if(B&&Ǝ.Ɛ()){B=Ǝ.ƒ(false);Q="Undocking in: "+ǔ((int)Math.Max(0,Ǝ.Ų-Ǝ.Ƅ));}if(Ɗ)ƌ=Q;return B;}String ƌ=
"";bool Ƌ(bool È,bool Ɗ){IMyShipConnector Å=Ò(MyShipConnectorStatus.Connected);if(Å==null)return false;if(Vector3.Distance
(Α.ɉ,Å.GetPosition())<5)return Ə(ʓ,È,Ɗ);if(Vector3.Distance(ǝ.ɉ,Å.GetPosition())<5)return Ə(ʑ,È,Ɗ);return false;}float Ɖ=
0;bool ƈ=false;bool Ƈ(bool ű){if(ɾ&&ƽ!=Г.Ͻ)if(Ɖ!=-1&&ϊ>=Ɖ){ύ="Ship too heavy";return true;}if(Ő>=ɸ||ƈ){ƈ=false;ύ=
"Ship is full";return true;}return false;}bool ţ(){List<IMyCockpit>Ĭ=new List<IMyCockpit>();φ.GetBlocksOfType(Ĭ,q=>q.CubeGrid==Me.
CubeGrid);for(int J=0;J<Ĭ.Count;J++)if(Ĭ[J].IsUnderControl)return true;return false;}bool š(){String[]Ō=null;if(!ɹ)Ō=new string[
]{"ICE"};if(ƽ==Г.В)return ő("","ORE",ŉ.ň,Ō)==0;if(ƽ==Г.χ)return ő("","COMPONENT",ŉ.ň,Ō)==0;else return ő("","",ŉ.ň,Ō)==0;
}void Š(bool ş,bool Ş,float ŝ,float Ŝ){if(Ю==null||Ъ.Count==0)return;Vector3 Ţ=new Vector3();int ś=0;for(int J=0;J<Ъ.
Count;J++){if(Ъ[J].WorldMatrix.Forward!=О.WorldMatrix.Forward)continue;ś++;Ţ+=Ъ[J].GetPosition();}Ţ=Ţ/ś;Vector3 Ś=ȓ(Ю,Ţ);Ю.
Enabled=true;Ю.ShowOnHUD=ş;Ю.LeftExtend=(Ş?1:ʂ)/2f*А-Ś.X;Ю.RightExtend=(Ş?1:ʂ)/2f*А+Ś.X;;Ю.TopExtend=(Ş?1:ʁ)/2f*Л+Ś.Y;;Ю.
BottomExtend=(Ş?1:ʁ)/2f*Л-Ś.Y;;Ю.FrontExtend=(ş?ʀ:ŝ)-Ś.Z;Ю.BackExtend=ş?0:Ŝ+а*0.75f+Ś.Z;Ю.DetectFloatingObjects=true;Ю.
DetectAsteroids=false;Ю.DetectLargeShips=true;Ю.DetectSmallShips=true;Ю.DetectStations=true;Ю.DetectOwner=true;Ю.DetectSubgrids=false;Ю
.DetectPlayers=false;Ю.DetectEnemy=true;Ю.DetectFriendly=true;Ю.DetectNeutral=true;}void ř<Ř>(List<Ř>ĥ,bool e){for(int J=
0;J<ĥ.Count;J++)ř((IMyTerminalBlock)ĥ[J],e);}void ŗ(bool Ŗ){for(int J=0;J<Ц.Count;J++){Ц[J].Stockpile=Ŗ;}}void ŕ<Ř>(List<
Ř>ĥ,bool Ű,float ů,bool Ů){for(int J=0;J<ĥ.Count;J++){IMyTextSurface Q=null;if(ĥ[J]is IMyTextSurface)Q=(IMyTextSurface)ĥ[
J];if(Q!=null){Q.ContentType=ContentType.TEXT_AND_IMAGE;if(!Ű)continue;Q.Font="Debug";if(Ů)continue;Q.FontSize=ů;}}}void
ř(IMyTerminalBlock q,bool e){if(q==null)return;String ŭ=e?"OnOff_On":"OnOff_Off";var Ŭ=q.GetActionWithName(ŭ);Ŭ.Apply(q);
}bool ū=true;void Ū(bool e){ū=e;if(!ɼ)return;ř(Ч,e);}void ũ(ChargeMode Ũ){for(int J=0;J<Т.Count;J++)Т[J].ChargeMode=Ũ;}
void ŧ(List<IMyLandingGear>q,bool Ŧ){for(int J=0;J<q.Count;J++){if(Ŧ)q[J].Lock();if(!Ŧ)q[J].Unlock();}}bool ť=false;void Ť(
bool e){if(ť==e)return;List<IMyShipController>ĥ=new List<IMyShipController>();φ.GetBlocksOfType(ĥ,Н);if(ĥ.Count==0)return;
for(int J=0;J<ĥ.Count;J++)ĥ[J].DampenersOverride=e;ť=e;}IMyShipConnector Ò(MyShipConnectorStatus Ñ){for(int J=0;J<Ь.Count;J
++){if(!ƃ(Ь[J],true))continue;if(Ь[J].Status==Ñ)return Ь[J];}return null;}float Ð(Vector3 Ï,Vector3 Î,Vector3 Í,Ͷ C){if(Í.
Length()==0f)return 0;Vector3 Ì=Ȑ(Ï,Î,Vector3.Normalize(Í));float Ó=Â(-Ì,C);return Ó/Í.Length();}int Ë=0;Ͷ Ê=null;void É(bool
È){float Ç=0;float Æ=0.9f;if(È){Ɖ=-1;Ë=0;Ê=null;if(ӄ!=Ӎ.ӌ&&ǝ.Í.Length()!=0){Ç=Æ*Ð(ǝ.Ï,ǝ.é*-1,ǝ.Í,null);if(Ç<Ɖ||Ɖ==-1)Ɖ=Ç;
}if(Α.ʹ&&Α.Í.Length()!=0){Ç=Æ*Ð(Α.Ï,Α.é*-1,Α.Í,null);if(Ç<Ɖ||Ɖ==-1)Ɖ=Ç;}return;}if(Ë==-1)return;if(Ë>=0){int Å=0;while(Ë<
ˊ.Count){if(Å>100)return;Å++;Ͷ C=ˊ[Ë];if(C.Í.Length()!=0f){Ç=Æ*Math.Min(Ð(C.Ï,C.é*-1,C.Í,C),Ð(C.Ï*-1,C.é*-1,C.Í,C));if(Ç<
Ɖ||Ɖ==-1)Ɖ=Ç;}else Ê=C;Ë++;}Ë=-1;}bool Ä=true;float Ã=0;if(ˊ.Count==0&&Ɖ==-1)Ä=false;if(Ê!=null){for(int M=0;M<Õ.Count;M
++){String A=Õ.Keys.ElementAt(M);float[,]K=Õ.Values.ElementAt(M);float G=0;if(!I(Ê,A,out G)){Ä=false;break;}for(int J=0;J<
K.GetLength(0);J++){for(int ä=0;ä<K.GetLength(1);ä++){float ã=Math.Abs(K[J,ä]*G);if(ã==0)continue;Ä=true;if(Ã==0||ã<Ã)Ã=ã
;}}}}if(!Ä){for(int J=0;J<Ö.GetLength(0);J++){for(int ä=0;ä<Ö.GetLength(1);ä++){float ã=Math.Abs(Ö[J,ä]);if(ã==0)continue
;if(Ã==0||ã<Ã)Ã=ã;}}}if(Ã>0){Ç=µ(Ã,Me.CubeGrid.GridSizeEnum==MyCubeSize.Small?minAccelerationSmall:minAccelerationLarge);
if(Ç>0)if(Ç<Ɖ||Ɖ==-1)Ɖ=Ç;}}void â(bool á,float à,float ß,float Þ,float Ý){for(int J=0;J<Ы.Count;J++){IMyGyro Ü=Ы[J];Ü.
GyroOverride=á;if(!á)Ü.GyroPower=100;else Ü.GyroPower=à;if(!á)continue;Vector3 Ï=Я.WorldMatrix.Forward;Vector3 Û=Я.WorldMatrix.Right
;Vector3 Î=Я.WorldMatrix.Up;Vector3 Ú=Ü.WorldMatrix.Forward;Vector3 Ù=Ü.WorldMatrix.Up;Vector3 Ø=Ü.WorldMatrix.Left*-1;if
(Ú==Ï)Ü.SetValueFloat("Roll",Ý);else if(Ú==(Ï*-1))Ü.SetValueFloat("Roll",Ý*-1);else if(Ù==(Ï*-1))Ü.SetValueFloat("Yaw",Ý)
;else if(Ù==Ï)Ü.SetValueFloat("Yaw",Ý*-1);else if(Ø==Ï)Ü.SetValueFloat("Pitch",Ý);else if(Ø==(Ï*-1))Ü.SetValueFloat(
"Pitch",Ý*-1);if(Ø==(Û*-1))Ü.SetValueFloat("Pitch",ß);else if(Ø==Û)Ü.SetValueFloat("Pitch",ß*-1);else if(Ù==Û)Ü.SetValueFloat(
"Yaw",ß);else if(Ù==(Û*-1))Ü.SetValueFloat("Yaw",ß*-1);else if(Ú==(Û*-1))Ü.SetValueFloat("Roll",ß);else if(Ú==Û)Ü.
SetValueFloat("Roll",ß*-1);if(Ù==(Î*-1))Ü.SetValueFloat("Yaw",Þ);else if(Ù==Î)Ü.SetValueFloat("Yaw",Þ*-1);else if(Ø==Î)Ü.
SetValueFloat("Pitch",Þ);else if(Ø==(Î*-1))Ü.SetValueFloat("Pitch",Þ*-1);else if(Ú==Î)Ü.SetValueFloat("Roll",Þ);else if(Ú==(Î*-1))Ü.
SetValueFloat("Roll",Þ*-1);}}float[,]Ö=new float[3,2];Dictionary<String,float[,]>Õ=new Dictionary<string,float[,]>();void E(
IMyTerminalBlock q){if(q==null)return;Ö=new float[3,2];Õ=new Dictionary<string,float[,]>();for(int J=0;J<Γ.Count;J++){IMyThrust M=Γ[J];
if(!M.IsFunctional)continue;Vector3 U=ȑ(q,M.WorldMatrix.Backward);float S=M.MaxEffectiveThrust;if(Math.Round(U.X,2)!=0.0)
if(U.X>=0)Ö[0,0]+=S;else Ö[0,1]-=S;if(Math.Round(U.Y,2)!=0.0)if(U.Y>=0)Ö[1,0]+=S;else Ö[1,1]-=S;if(Math.Round(U.Z,2)!=0.0)
if(U.Z>=0)Ö[2,0]+=S;else Ö[2,1]-=S;String Q=N(M);float[,]P=null;if(Õ.ContainsKey(Q))P=Õ[Q];else{P=new float[3,2];Õ.Add(Q,P
);}float O=M.MaxThrust;if(Math.Round(U.X,2)!=0.0)if(U.X>=0)P[0,0]+=O;else P[0,1]-=O;if(Math.Round(U.Y,2)!=0.0)if(U.Y>=0)P
[1,0]+=O;else P[1,1]-=O;if(Math.Round(U.Z,2)!=0.0)if(U.Z>=0)P[2,0]+=O;else P[2,1]-=O;}}static String N(IMyThrust M){
return M.BlockDefinition.SubtypeId;}Vector3 L(Vector3 D,float[,]K){return new Vector3(D.X>=0?K[0,0]:K[0,1],D.Y>=0?K[1,0]:K[1,1
],D.Z>=0?K[2,0]:K[2,1]);}bool I(Ͷ C,String H,out float G){G=0;int F=ˋ.IndexOf(H);if(F==-1||C.Η==null||F>=C.Η.Length)
return false;G=C.Η[F];if(G==-1)return false;return true;}Vector3 E(Vector3 D,Ͷ C){if(C!=null){Vector3 B=new Vector3();for(int
J=0;J<Õ.Keys.Count;J++){String A=Õ.Keys.ElementAt(J);float G=0;if(!I(C,A,out G)){return L(D,Ö);}B+=L(D,Õ.Values.ElementAt
(J))*G;}return B;}return L(D,Ö);}float Â(Vector3 D,Ͷ C){return Â(D,new Vector3(),C);}float Â(Vector3 D,Vector3 Á,Ͷ C){
Vector3 Z=E(D,C);Vector3 À=Z+Á*ϊ;float º=(À/D).AbsMin();return(float)(D*º).Length();}static float µ(float ª,float q){if(q==0)
return 0;return ª/q;}void k(Vector3 h,bool e){if(!e){for(int J=0;J<Γ.Count;J++)Γ[J].SetValueFloat("Override",0.0f);return;}
Vector3 Z=E(h,null);float Y=Math.Min(1,Math.Abs(µ(h.X,Z.X)));float X=Math.Min(1,Math.Abs(µ(h.Y,Z.Y)));float V=Math.Min(1,Math.
Abs(µ(h.Z,Z.Z)));for(int J=0;J<Γ.Count;J++){IMyThrust M=Γ[J];Vector3 U=ȴ(ȑ(Я,M.WorldMatrix.Backward),1);if(U.X!=0&&Math.
Sign(U.X)==Math.Sign(h.X))M.SetValueFloat("Override",M.MaxThrust*Y);else if(U.Y!=0&&Math.Sign(U.Y)==Math.Sign(h.Y))M.
SetValueFloat("Override",M.MaxThrust*X);else if(U.Z!=0&&Math.Sign(U.Z)==Math.Sign(h.Z))M.SetValueFloat("Override",M.MaxThrust*V);else
M.SetValueFloat("Override",0.0f);}}float å(Vector3 ē,Vector3 Ē,Ͷ C){if(ē.Length()==0)return 0;float Æ=1;if(Ē.Length()>0)Æ
=Math.Min(1,Ȱ(-Ē,ē)/90)*0.4f+0.6f;float đ=Â(ē,Ē,C);if(đ==0)return 0.1f;float ª=µ(đ,ϊ);float M=(float)Math.Sqrt(µ(ē.Length
(),ª*0.5f));return ª*M*Æ*ɴ;}bool Đ=false;bool ď=false;bool Ď=false;bool č=false;float Č=0;float þ=0;Vector3 ÿ=new Vector3
();Vector3 ċ=new Vector3();Vector3 Ċ=new Vector3(1,1,1);float ĉ=1;Vector3 Ĉ=new Vector3();void ć(){Vector3 Ć=ċ-ɉ;if(Ć.
Length()==0)Ć=new Vector3(0,0,-1);Vector3 ą=ȑ(Я,Ć);Vector3 Ą=Vector3.Normalize(ą);Vector3 Á=ȑ(Я,Я.GetNaturalGravity());float Ĕ
=þ>0?Math.Max(0,1-Ȱ(Ć,ÿ)/5):0;float ģ=(float)Math.Min((Č>0?Č:1000f),Math.Max(å(-ą,Á,null),þ*Ĕ));if(!Đ)ģ=0;if(ď)ģ=Math.Max
(0,1-Ę/ė)*ģ;if(generalSpeedLimit>0)ģ=Math.Min(generalSpeedLimit,ģ);if(č)ģ*=(float)Math.Min(1,µ(Ć.Length(),wpReachedDist)/
2);Vector3 Ĥ=ȑ(Я,Я.GetShipVelocities().LinearVelocity);float Ģ=(float)(Math.Max(0,15-Ȱ(-Ą,-Ĥ))/15)*0.85f+0.15f;ĉ+=Math.
Sign(Ģ-ĉ)/10f;Vector3 ġ=Ą*ģ*ĉ-(Ĥ);Vector3 Ğ=E(ġ,null);if(Ď&&ԍ>0.1f){ġ.X*=Ġ(ġ.X,ref Ċ.X,1f,Ğ.X,20);ġ.Y*=Ġ(ġ.Y,ref Ċ.Y,1f,Ğ.Y,
20);ġ.Z*=Ġ(ġ.Z,ref Ċ.Z,1f,Ğ.Z,20);}else Ċ=new Vector3(1,1,1);Ĉ=ϊ*ġ-Á*ϊ;k(Ĉ,Ď);ԍ=Vector3.Distance(ɉ,ċ);}float Ġ(float ª,ref
float Ġ,float ğ,float Ğ,float ĝ){ª=Math.Sign(Math.Round(ª,2));if(ª==Math.Sign(Ġ))Ġ+=Math.Sign(Ġ)*ğ;else Ġ=ª;if(ª==0)Ġ=1;float
B=Math.Abs(Ġ);if(B<ĝ||Ğ==0)return B;Ġ=Math.Min(ĝ,Math.Max(-ĝ,Ġ));B=Math.Abs(Ğ);return B;}bool Ĝ=false;bool ě=false;bool Ě
=false;bool ę=false;float Ę=0;float ė=2;Vector3 Ė;Vector3 Ï;Vector3 é;void ĕ(){float ß=90;float Ý=90;float Þ=90;float ò=(
float)(Me.CubeGrid.GridSizeEnum==MyCubeSize.Small?gyroSpeedSmall:gyroSpeedLarge)/100f;Vector3 ă;Vector3 ñ;Vector3 ð;if(Ě){ă=
Vector3.Normalize(ċ-ɉ);ñ=ȑ(Я,ă);ð=ȑ(Я,é);ß=Ȱ(ñ,new Vector3(0,-1,0))-90;Ý=Ȭ(ð,new Vector3(-1,0,0),ð.Y);Þ=Ȭ(ñ,new Vector3(-1,0,0)
,ñ.Z);}else{ă=Ï;ð=ȑ(Я,é);ñ=ȑ(Я,Ï);Vector3 ï=ȑ(Я,Ė);ß=Ȭ(ð,new Vector3(0,0,1),ð.Y);Ý=Ȭ(ð,new Vector3(-1,0,0),ð.Y);Þ=Ȭ(ï,new
Vector3(0,0,1),ï.X);}if(ę&&ü()){Vector3 Í=Я.GetNaturalGravity();ð=ȑ(Я,Í);ß=Ȭ(ð,new Vector3(0,0,1),ð.Y);Ý=Ȭ(ð,new Vector3(-1,0,0
),ð.Y);}if(!Ȧ(-45,Ý,45)){ß=0;Þ=0;};if(!Ȧ(-45,Þ,45))ß=0;â(ě,1,(-ß)*ò,(-Þ)*ò,(-Ý)*ò);Ę=Math.Max(Math.Abs(ß),Math.Max(Math.
Abs(Ý),Math.Abs(Þ)));Ĝ=Ę<=ė;}void î(){this.ě=false;}void ì(Vector3 é,Vector3 Ï,Vector3 ë,float è,bool ç){ê(é,è,ç);ė=è;Ě=
false;this.Ï=Ï;this.Ė=ë;}void ì(Vector3 é,Vector3 Ï,Vector3 ë,bool ç){ì(é,Ï,ë,2f,ç);}void ê(Vector3 é,float è,bool ç){ė=è;
this.ě=true;this.ę=ç;Ě=true;Ĝ=false;this.é=é;}void í(){æ(false,false,false,ċ,0);Ď=false;}void æ(Vector3 ó,float ý){æ(true,
false,false,ó,ý);}void æ(bool Ă,bool ā,bool Ā,Vector3 ó,float ý){æ(Ă,ā,Ā,ó,ó-ɉ,0.0f,ý);}void æ(bool Ă,bool ā,bool Ā,Vector3 ó
,Vector3 ÿ,float þ,float ý){Ď=true;this.Đ=Ă;ċ=ó;this.Č=ý;this.þ=þ;this.č=ā;this.ď=Ā;this.ÿ=ÿ;ԍ=Vector3.Distance(ó,ɉ);}
bool ü(){Vector3D û;return this.Я.TryGetPlanetPosition(out û);}Dictionary<String,float[]>ú=new Dictionary<string,float[]>();
float ù;void ø(){if(!Μ)return;try{ù=Runtime.CurrentInstructionCount;}catch{}}void ö(String õ){if(!Μ)return;if(ù==0)return;try
{float Ô=(Runtime.CurrentInstructionCount-ù)/Runtime.MaxInstructionCount*100;if(!ú.ContainsKey(õ))ú.Add(õ,new float[]{Ô,Ô
});else{ú[õ][0]=Ô;ú[õ][1]=Math.Max(Ô,ú[õ][1]);}}catch{}}string ô(float ã){return Math.Round(ã,2)+" ";}string ô(Vector3 ã)
{return"X"+ô(ã.X)+"Y"+ô(ã.Y)+"Z"+ô(ã.Z);}Exception Ȥ=null;void ȣ(){String Q=
"Error occurred! \nPlease copy this and paste it \nin the \"Bugs and issues\" discussion.\n"+"Version: "+VERSION+"\n";ŕ(С,setLCDFontAndSize,0.9f,false);ŕ(Р,setLCDFontAndSize,0.9f,true);for(int J=0;J<С.Count;J++)С
[J].WriteText(Q+Ȥ.ToString());for(int J=0;J<Р.Count;J++)Р[J].WriteText(Q+Ȥ.ToString());}const String Ȣ="INSTRUCTIONS";
const String ȡ="DEBUG";String ǵ="",Ƞ="";String Ƿ="";void ȟ(){String Ȟ="";String Ǒ="";Ƿ=Ϣ(false);Ȟ+=Ƿ;Ǒ+=Ƿ;Ǒ+=β();for(int J=0;
J<С.Count;J++)С[J].WriteText(Ȟ);for(int J=0;J<Р.Count;J++)Р[J].WriteText(Ȟ);Echo(Ǒ);for(int J=0;J<П.Count;J++){
IMyTextPanel ƨ=П[J];String ǧ=ƨ.CustomData.ToUpper();if(ǧ==ȡ)ƨ.WriteText(ǵ+"\n"+Ƞ);if(ǧ==Ȣ)ƨ.WriteText(ȝ());}}string ȝ(){String Q="";
try{float Ʊ=Runtime.MaxInstructionCount;Q+="Inst: "+Runtime.CurrentInstructionCount+" Time: "+Math.Round(Runtime.
LastRunTimeMs,3)+"\n";Q+="Inst. avg/max: "+(int)(Φ*Ʊ)+" / "+(int)(Χ*Ʊ)+"\n";Q+="Inst. avg/max: "+Math.Round(Φ*100f,1)+"% / "+Math.
Round(Χ*100f,1)+"% \n";Q+="Time avg/max: "+Math.Round(Τ,2)+"ms / "+Math.Round(Υ,2)+"ms \n";}catch{}for(int J=0;J<ú.Count;J++)
{Q+=""+ú.Keys.ElementAt(J)+" = "+Math.Round(ú.Values.ElementAt(J)[0],2)+" / "+Math.Round(ú.Values.ElementAt(J)[1],2)+
"%\n";}return Q;}Vector3 ȴ(Vector3 û,int ȳ){return new Vector3(Math.Round(û.X,ȳ),Math.Round(û.Y,ȳ),Math.Round(û.Z,ȳ));}
Vector3 Ȳ(Vector3 û,float ȱ){Vector3 B=new Vector3(Math.Sign(û.X),Math.Sign(û.Y),Math.Sign(û.Z));B.X=B.X==0.0?ȱ:B.X;B.Y=B.Y==
0.0?ȱ:B.Y;B.Z=B.Z==0.0?ȱ:B.Z;return B;}float Ȱ(Vector3 ȯ,Vector3 Ȫ){if(ȯ==Ȫ)return 0;float Ġ=(ȯ*Ȫ).Sum;float Ȯ=ȯ.Length();
float ȭ=Ȫ.Length();if(Ȯ==0||ȭ==0)return 0;float B=(float)((180.0f/Math.PI)*Math.Acos(Ġ/(Ȯ*ȭ)));if(float.IsNaN(B))return 0;
return B;}float Ȭ(Vector3 ȫ,Vector3 Ȫ,float ȩ){float B=Ȱ(ȫ,Ȫ);if(ȩ>0f)B*=-1;if(B>-90f)return B-90f;else return 180f-(-B-90f);}
double Ȩ(float ȧ){return(Math.PI/180)*ȧ;}bool Ȧ(double ȥ,double ª,double Ğ){return(ª>=ȥ&&ª<=Ğ);}Vector3 ȕ(IMyTerminalBlock q,
Vector3 Ȕ){return Vector3D.Transform(Ȕ,q.WorldMatrix);}Vector3 ȓ(IMyTerminalBlock q,Vector3 Ȓ){return ȑ(q,Ȓ-q.GetPosition());}
Vector3 ȑ(IMyTerminalBlock q,Vector3 ȏ){return Vector3D.TransformNormal(ȏ,MatrixD.Transpose(q.WorldMatrix));}Vector3 Ȑ(Vector3
ȍ,Vector3 Ȍ,Vector3 ȏ){MatrixD ȋ=MatrixD.CreateFromDir(ȍ,Ȍ);return Vector3D.TransformNormal(ȏ,MatrixD.Transpose(ȋ));}
Vector3 Ȏ(Vector3 ȍ,Vector3 Ȍ,Vector3 ē){MatrixD ȋ=MatrixD.CreateFromDir(ȍ,Ȍ);return Vector3D.Transform(ē,ȋ);}String Ȋ(Vector3
û){return""+û.X+"|"+û.Y+"|"+û.Z;}Vector3 ȉ(String Q){String[]Ȉ=Q.Split('|');return new Vector3(float.Parse(ȗ(Ȉ,0)),float.
Parse(ȗ(Ȉ,1)),float.Parse(ȗ(Ȉ,2)));}String Ȗ(Ͷ C){String Ȝ=":";String B=Ȋ(C.ɉ)+Ȝ+Ȋ(C.Ï)+Ȝ+Ȋ(C.é)+Ȝ+Ȋ(C.Ė)+Ȝ+Ȋ(C.Í);for(int J=
0;J<C.Η.Length;J++){B+=Ȝ;B+=Math.Round(C.Η[J],3);}return B;}Ͷ ț(String Ț){String[]Q=Ț.Split(':');Ͷ B=new Ͷ(ȉ(ȗ(Q,0)),ȉ(ȗ(
Q,1)),ȉ(ȗ(Q,2)),ȉ(ȗ(Q,3)),ȉ(ȗ(Q,4)));int J=5;List<float>Ĭ=new List<float>();while(J<Q.Length){String ș=ȗ(Q,J);float ã=0;
if(!float.TryParse(ș,out ã))break;Ĭ.Add(ã);J++;}B.Η=Ĭ.ToArray();return B;}void Ș<Ř>(Ř Q,bool Ư){if(Ư)Storage+="\n";Storage
+=Q;}void Ș<Ř>(Ř Q){Ș(Q,true);}String ȗ(String[]Q,int J){String Ɨ=Q.ElementAtOrDefault(J);if(String.IsNullOrEmpty(Ɨ))
return"";return Ɨ;}bool ȵ=false;void Save(){if(ȵ||ƽ==Г.Б){Storage="";return;}Storage=DATAREV+";";Ș(Ȋ(υ),false);Ș(Ȋ(Α.Ï));Ș(Ȋ(Α
.Ė));Ș(Ȋ(Α.é));Ș(Ȋ(Α.Í));Ș(Ȋ(Α.ɉ));Ș(Ȋ(Α.Έ));Ș(Α.ʹ);Ș(Ȋ(ǝ.ɉ));Ș(Ȋ(ǝ.Í));Ș(Ȋ(ǝ.Ï));Ș(Ȋ(ǝ.é));Ș(Ȋ(ǝ.Ė));Ș(Ȋ(ǝ.Έ));Ș(ǝ.ʹ);Ș(
Ȋ(ӆ));Ș(Ȋ(Ӆ));Ș(";");Ș((int)ƽ,false);Ș((int)ӄ);Ș((int)ҟ);Ș(ɸ);Ș(ɷ);Ș(ɶ);Ș(ɵ);Ș(ɴ);Ș(ɾ);Ș(ɹ);if(ƽ==Г.Ͻ){Ș((int)ʓ.ų);Ș(ʓ.Ų)
;Ș(ʓ.Ƅ);Ș(ʓ.Ɔ);Ș(ʓ.ƕ);Ș((int)ʑ.ų);Ș(ʑ.Ų);Ș(ʑ.Ƅ);Ș(ʑ.Ɔ);Ș(ʑ.ƕ);}else{Ș((int)ʃ);Ș((int)ʅ);Ș((int)ɺ);Ș((int)ɿ);Ș((int)Ӄ);Ș(ʄ
);Ș(ɼ);Ș(ɽ);Ș(ʆ);Ș(ʂ);Ș(ʁ);Ș(ʀ);Ș(ɳ);Ș(ɲ);Ș(ɻ);Ș(ɱ);Ș(ӂ);Ș(Ӂ);Ș(ԕ);Ș(ԓ);Ș(ԑ);Ș(Ԓ);Ș(Ғ);Ș(ԁ);}Ș(";");for(int J=0;J<ˋ.Count
;J++)Ș((J>0?"|":"")+ˋ[J],false);Ș(";");for(int J=0;J<ˊ.Count;J++)Ș(Ȗ(ˊ[J]),J>0);}Ӎ ɛ=Ӎ.ӌ;public enum ɚ{ə,ɘ,ɗ,Ƒ}ɚ ɖ(){if(
Storage=="")return ɚ.ɗ;String[]ɕ=Storage.Split(';');if(ȗ(ɕ,0)!=DATAREV){return ɚ.Ƒ;}int J=0;try{String[]Q=ȗ(ɕ,1).Split('\n');υ=
ȉ(ȗ(Q,J++));Α.Ï=ȉ(ȗ(Q,J++));Α.Ė=ȉ(ȗ(Q,J++));Α.é=ȉ(ȗ(Q,J++));Α.Í=ȉ(ȗ(Q,J++));Α.ɉ=ȉ(ȗ(Q,J++));Α.Έ=ȉ(ȗ(Q,J++));Α.ʹ=bool.
Parse(ȗ(Q,J++));ǝ.ɉ=ȉ(ȗ(Q,J++));ǝ.Í=ȉ(ȗ(Q,J++));ǝ.Ï=ȉ(ȗ(Q,J++));ǝ.é=ȉ(ȗ(Q,J++));ǝ.Ė=ȉ(ȗ(Q,J++));ǝ.Έ=ȉ(ȗ(Q,J++));ǝ.ʹ=bool.
Parse(ȗ(Q,J++));ӆ=ȉ(ȗ(Q,J++));Ӆ=ȉ(ȗ(Q,J++));Q=ȗ(ɕ,2).Split('\n');J=0;ƽ=(Г)int.Parse(ȗ(Q,J++));ӄ=(Ӎ)int.Parse(ȗ(Q,J++));ҟ=(Ӎ)
int.Parse(ȗ(Q,J++));ɸ=int.Parse(ȗ(Q,J++));ɷ=int.Parse(ȗ(Q,J++));ɶ=int.Parse(ȗ(Q,J++));ɵ=int.Parse(ȗ(Q,J++));ɴ=float.Parse(ȗ
(Q,J++));ɾ=bool.Parse(ȗ(Q,J++));ɹ=bool.Parse(ȗ(Q,J++));if(ƽ==Г.Ͻ){ʓ.ų=(ƀ)int.Parse(ȗ(Q,J++));ʓ.Ų=float.Parse(ȗ(Q,J++));ʓ.
Ƅ=float.Parse(ȗ(Q,J++));ʓ.Ɔ=ȗ(Q,J++);ʓ.ƕ=ȗ(Q,J++);ʑ.ų=(ƀ)int.Parse(ȗ(Q,J++));ʑ.Ų=float.Parse(ȗ(Q,J++));ʑ.Ƅ=float.Parse(ȗ(
Q,J++));ʑ.Ɔ=ȗ(Q,J++);ʑ.ƕ=ȗ(Q,J++);}else{ʃ=(ʱ)int.Parse(ȗ(Q,J++));ʅ=(ʮ)int.Parse(ȗ(Q,J++));ɺ=(ʩ)int.Parse(ȗ(Q,J++));ɿ=(ʵ)
int.Parse(ȗ(Q,J++));Ӄ=(ʱ)int.Parse(ȗ(Q,J++));ʄ=bool.Parse(ȗ(Q,J++));ɼ=bool.Parse(ȗ(Q,J++));ɽ=bool.Parse(ȗ(Q,J++));ʆ=bool.
Parse(ȗ(Q,J++));ʂ=int.Parse(ȗ(Q,J++));ʁ=int.Parse(ȗ(Q,J++));ʀ=int.Parse(ȗ(Q,J++));ɳ=float.Parse(ȗ(Q,J++));ɲ=float.Parse(ȗ(Q,J
++));ɻ=float.Parse(ȗ(Q,J++));ɱ=float.Parse(ȗ(Q,J++));ӂ=int.Parse(ȗ(Q,J++));Ӂ=int.Parse(ȗ(Q,J++));ԕ=int.Parse(ȗ(Q,J++));ԓ=
int.Parse(ȗ(Q,J++));ԑ=int.Parse(ȗ(Q,J++));Ԓ=int.Parse(ȗ(Q,J++));Ғ=int.Parse(ȗ(Q,J++));ԁ=float.Parse(ȗ(Q,J++));}Q=ȗ(ɕ,3).
Replace("\n","").Split('|');ˋ=Q.ToList();Q=ȗ(ɕ,4).Split('\n');ˊ.Clear();if(Q.Count()>=1&&Q[0]!="")for(int ä=0;ä<Q.Length;ä++)ˊ.
Add(ț(ȗ(Q,ä)));}catch{return ɚ.ɘ;}ɛ=ҟ;ҕ();return ɚ.ə;}String ɔ(String ǟ){int J=ǟ.IndexOf("//");if(J!=-1)ǟ=ǟ.Substring(0,J);
String[]Q=ǟ.Split('=');if(Q.Length<=1)return"";return Q[1].Trim();}String ɓ(String[]Q,String Ǩ,ref bool Ä){foreach(String ŭ in
Q)if(ŭ.StartsWith(Ǩ))return ŭ;Ä=false;return"";}bool ɜ=false;bool ɝ=false;String ɮ="";String ɯ="";IMyBroadcastListener ɭ=
null;bool ɬ=true;void ɫ(){bool ɪ=true;if(ɬ){ɬ=false;if(Me.CustomData.Contains("Antenna_Name")){List<IMyRadioAntenna>ɦ=new
List<IMyRadioAntenna>();φ.GetBlocksOfType(ɦ);for(int J=0;J<ɦ.Count;J++)if(ɦ[J].AttachedProgrammableBlock==Me.EntityId)ɦ[J].
AttachedProgrammableBlock=-1;ύ="Update custom data";Me.CustomData="";}}String ɩ=(ƽ!=Г.Б?"[PAM-Ship]":"[PAM-Controller]")+" Broadcast-settings";
try{if(Me.CustomData.Length==0||Me.CustomData.Split('\n')[0]!=ɩ)ɤ(ɩ);String[]Q=Me.CustomData.Split('\n');ɜ=bool.Parse(ɔ(ɓ(Q
,"Enable_Broadcast",ref ɪ)));bool ɨ=false;bool ɧ=true;if(ɜ){if(ƽ!=Г.Б){ɮ=ɔ(ɓ(Q,"Ship_Name",ref ɪ)).Replace(ɠ,"");}ɯ=ɔ(ɓ(Q
,"Broadcast_Channel",ref ɪ)).ToLower();ɧ=false;if(ɭ==null){ɭ=this.IGC.RegisterBroadcastListener(ɡ);ɭ.SetMessageCallback(
"");}List<IMyRadioAntenna>ɦ=new List<IMyRadioAntenna>();φ.GetBlocksOfType(ɦ);bool ɥ=false;for(int J=0;J<ɦ.Count;J++){if(ɦ[
J].EnableBroadcasting&&ɦ[J].Enabled){ɥ=true;break;}}if(ɦ.Count==0)ύ="No Antenna found";else if(!ɥ)ύ="Antenna not ready";ɨ
=ɦ.Count==0||!ɥ;if(ɝ&&!ɨ&&ƽ!=Г.Б)ύ="Antenna ok";}else if(ƽ==Г.Б)ύ="Offline - Enable in PB custom data";ɝ=ɨ;if(ɧ){if(ɭ!=
null)this.IGC.DisableBroadcastListener(ɭ);ɭ=null;}}catch{ɪ=false;}if(!ɪ){ύ="Reset custom data";ɤ(ɩ);}}void ɤ(String ɣ){
String ǧ=ɣ+"\n\n"+"Enable_Broadcast="+(ƽ==Г.Б?"true":"false")+" \n";ǧ+=ƽ!=Г.Б?"Ship_Name=Your_ship_name_here\n":"";Me.
CustomData=ǧ+"Broadcast_Channel=#default";}String ɢ(){if(ƽ!=Г.Б)return""+Me.GetId();return ɠ;}const String ɡ="[PAMCMD]";const
String ɠ="#";const Char ɟ=';';void ɞ(String ƿ,String ȷ){ɀ(ƿ,ɠ,ȷ);}void ɀ(String ƿ,String ó,String ȷ){try{if(!ɜ)return;ƿ=ɡ+ɟ+ɢ(
)+ɟ+ó+ɟ+ɯ+ɟ+ȷ+ɟ+ƿ;this.IGC.SendBroadcastMessage(ɡ,ƿ);}catch(Exception e){Ȥ=e;}}bool Ⱦ(String ƿ){return ƿ.StartsWith(ɡ);}
bool Ƚ(ref String ŭ,out string Ƹ,Char ȼ){int J=ŭ.IndexOf(ȼ);Ƹ="";if(J<0)return false;Ƹ=ŭ.Substring(0,J);ŭ=ŭ.Remove(0,J+1);
return true;}String ȿ="";bool Ȼ(ref String ƿ,out String Ǌ,out String ȹ){Ǌ="";ȹ="";if(!ɜ)return false;String Q="";if(!Ƚ(ref ƿ,
out Q,ɟ)||!Ⱦ(Q))return false;if(!Ƚ(ref ƿ,out Ǌ,ɟ))return false;if(!Ƚ(ref ƿ,out Q,ɟ)||(Q!=ɢ()&&(Q!="*"&&ƽ!=Г.Б)))return
false;if(!Ƚ(ref ƿ,out Q,ɟ)||(Q!=ɯ))return false;if(!Ƚ(ref ƿ,out ȹ,ɟ))return false;return true;}void ȸ(String ȷ){if(!ɜ)return;
String ȶ=""+ɟ;String ƿ=ƿ=VERSION+ȶ;ƿ+=ɮ+ȶ;ƿ+=(int)ƽ+ȶ;ƿ+=ô(ɉ.X)+""+ȶ;ƿ+=ô(ɉ.Y)+ȶ;ƿ+=ô(ɉ.Z)+ȶ;if(ƽ!=Г.Ͻ)ƿ+=ϛ(ҟ)+(ҟ==Ӎ.ӊ?" "+
Math.Round(Ӏ,1)+"%":"")+ȶ;else ƿ+=ϛ(ҟ)+ȶ;ƿ+=ύ+ȶ;ƿ+=Є+""+ȶ;ƿ+=Ƿ+ȶ;ƿ+=ŏ+ȶ;ƿ+=Ŕ+ȶ;for(int J=0;J<ł.Count;J++){if(ł[J].į!=ŉ.ň)
continue;ƿ+=ł[J].õ+"/"+ł[J].Ń+"/"+ł[J].ń+ȶ;}ɞ(ƿ,ȷ);}public enum Ⱥ{Ǻ,Ɂ,ɒ,ɑ}class ɐ{public DateTime ɏ;public DateTime Ɏ;public
String ǃ="";public String õ="";public String ɍ="";public String Ɍ="";public String ɋ="";public String Ƿ="";public String Ɋ="";
public Vector3 ɉ=new Vector3();public List<Ņ>Ƕ=new List<Ņ>();public Г ƽ=Г.Ǻ;public Ⱥ Ɉ;public float ɇ;public float Ɇ;public
int Ʋ;public int Ʌ=0;public int Ʉ=0;public bool Ƀ(){return(DateTime.Now-ɏ).TotalSeconds>10;}public bool ɂ(){if(Ɉ!=Ⱥ.Ɂ)
return false;return(DateTime.Now-Ɏ).TotalSeconds>=4;}public ɐ(String ǃ){this.ǃ=ǃ;}public Ⱥ Ǫ(String ǃ,bool Œ,bool ǂ,bool ǁ){if
(ǃ==""&&!ǂ)return Ⱥ.Ǻ;if(Ɉ==Ⱥ.Ɂ&&ɂ())Ɉ=Ⱥ.ɑ;if(Œ){Ɋ=ǃ;Ɉ=Ⱥ.Ɂ;Ɏ=DateTime.Now;return Ɉ;}else if(ǂ){Ɋ="";Ɉ=Ⱥ.Ǻ;}else if(Ɋ==ǃ){
if(ǁ)Ɉ=Ⱥ.ɒ;return Ɉ;}return Ⱥ.Ǻ;}}void ǀ(ɐ Ʀ,String ƿ){Ʀ.ɏ=DateTime.Now;String Å="";String ƾ="";String Ǆ="";String ƽ="";
String[]û=new string[3];Ƚ(ref ƿ,out Ʀ.ɍ,ɟ);Ƚ(ref ƿ,out Ʀ.õ,ɟ);if(Ʀ.ɍ!=VERSION)return;Ƚ(ref ƿ,out ƽ,ɟ);Ƚ(ref ƿ,out û[0],ɟ);Ƚ(
ref ƿ,out û[1],ɟ);Ƚ(ref ƿ,out û[2],ɟ);Ƚ(ref ƿ,out Ʀ.ɋ,ɟ);Ƚ(ref ƿ,out Ʀ.Ɍ,ɟ);Ƚ(ref ƿ,out Å,ɟ);Ƚ(ref ƿ,out Ʀ.Ƿ,ɟ);Ƚ(ref ƿ,out
ƾ,ɟ);Ƚ(ref ƿ,out Ǆ,ɟ);Ʀ.Ƕ.Clear();while(true){String J;if(!Ƚ(ref ƿ,out J,ɟ))break;String[]Q=J.Split('/');if(Q.Count()<3)
continue;int ƻ=0;if(!int.TryParse(Q[2],out ƻ))continue;Ʀ.Ƕ.Add(new Ņ(Q[0],Q[1],ƻ,ŉ.ň));}int ƺ=0;if(!int.TryParse(Å,out Ʀ.Ʋ))Ʀ.Ʋ=
0;if(!int.TryParse(ƽ,out ƺ))Ʀ.ƽ=Г.Ǻ;if(!float.TryParse(û[0],out Ʀ.ɉ.X))Ʀ.ɉ.X=0;if(!float.TryParse(û[1],out Ʀ.ɉ.Y))Ʀ.ɉ.Y=0
;if(!float.TryParse(û[2],out Ʀ.ɉ.Z))Ʀ.ɉ.Z=0;if(!float.TryParse(ƾ,out Ʀ.Ɇ))Ʀ.Ɇ=0;if(!float.TryParse(Ǆ,out Ʀ.ɇ))Ʀ.ɇ=0;Ʀ.ƽ=(
Г)ƺ;Ʀ.Ʌ=(int)Math.Round(Vector3.Distance(Me.GetPosition(),Ʀ.ɉ));Ʀ.Ʉ=0;for(int ä=0;ä<Ʀ.Ƕ.Count();ä++)Ʀ.Ʉ+=Ʀ.Ƕ[ä].ń;}void ƹ
(string Ƹ){if(Ƹ=="")return;var ª=Ƹ.ToUpper().Split(' ');ª.DefaultIfEmpty("");var Ƽ=ª.ElementAtOrDefault(0);var Ʒ=ª.
ElementAtOrDefault(1);String ǅ="Invalid argument: "+Ƹ;if(Ѓ==Ϸ.ϫ){if(ƞ!=null){switch(Ƽ){case"UP":{if(Є<2)break;if(ƞ.Ʋ==0){Є=1;return;}Ƨ(ƞ,
"UP");return;}case"DOWN":{if(Є<1)break;if(Є==1){Ƨ(ƞ,"MRES");break;}Ƨ(ƞ,"DOWN");break;}case"APPLY":{if(Є<2)break;Ƨ(ƞ,"APPLY")
;return;}}}}switch(Ƽ){case"UP":this.Ї(false);return;case"DOWN":{this.І(false);return;}case"APPLY":this.Ɯ(true);return;}
switch(Ƽ){case"CLEAR":Ơ();return;case"SENDALL":Ƨ(null,Ʒ);return;case"SEND":Ǐ(Ƹ.Remove(0,"SEND".Length+1));return;}ύ=ǅ;}void Ǐ(
String Ƹ){if(Ƹ=="")return;var ª=Ƹ.Split(':');if(ª.Length!=2){ύ="Missing separator \":\"";return;};ª.DefaultIfEmpty("");String
ǎ=ª.ElementAtOrDefault(0).Trim();ɐ Ʀ=Ǎ("",ǎ);if(Ʀ!=null)Ƨ(Ʀ,ª.ElementAtOrDefault(1).Trim());else ύ="Unknown ship: "+ǎ;}ɐ
Ǎ(String ǃ,String õ){ǃ=ǃ.ToUpper();õ=õ.ToUpper();for(int J=0;J<ǌ.Count;J++){if(ǃ!=""&&ǌ[J].ǃ.ToUpper()==ǃ)return ǌ[J];if(
õ!=""&&ǌ[J].õ.ToUpper()==õ)return ǌ[J];}return null;}List<ɐ>ǌ=null;void ǋ(string Ƹ){if(π){ǌ=new List<ɐ>();}String Ǌ="";
String ǉ="";if(!Ⱦ(Ƹ))ƹ(Ƹ);if(ɭ!=null&&ɭ.HasPendingMessage){MyIGCMessage ǈ=ɭ.AcceptMessage();String ſ=(string)ǈ.Data;if(Ȼ(ref ſ
,out Ǌ,out ǉ)&&Ǌ!=""&&Ǌ!=ɠ){ɐ Ʀ=Ǎ(Ǌ,"");if(Ʀ==null){Ʀ=new ɐ(Ǌ);ǌ.Add(Ʀ);}Ʀ.Ǫ(ǉ,false,false,true);ǀ(Ʀ,ſ);ǌ.Sort(delegate(ɐ
Y,ɐ X){if(Y.õ==null&&X.õ==null)return 0;else if(Y.õ==null)return-1;else if(X.õ==null)return 1;else return Y.õ.CompareTo(X
.õ);});}}if(ξ||π){if(Ω<=0||π){ύ="";π=false;Ƥ();ɫ();ρ=DateTime.Now;}}ǖ();}Ⱥ Ǉ(ɐ Ʀ,bool ǆ,string ǃ){Ⱥ B=Ⱥ.Ǻ;if(Ʀ==null){for
(int J=0;J<ǌ.Count;J++){Ʀ=ǌ[J];Ⱥ Ƶ=Ʀ.Ǫ(ǃ,false,ǆ,false);if(Ƶ==Ⱥ.ɒ)B=Ƶ;if(Ƶ==Ⱥ.ɑ)return Ƶ;if(Ƶ==Ⱥ.Ɂ)return Ƶ;}return B;}
else return Ʀ.Ǫ(ǃ,false,ǆ,false);}void Ƨ(ɐ Ʀ,String ƥ){if(Ʀ==null){for(int J=0;J<ǌ.Count;J++)ǌ[J].Ǫ(ƥ,true,false,false);ɀ(ƥ,
"*",ƥ);}else{Ʀ.Ǫ(ƥ,true,false,false);ɀ(ƥ,Ʀ.ǃ,ƥ);}}void Ƥ(){List<IMyTerminalBlock>ƣ=new List<IMyTerminalBlock>();φ.
GetBlocksOfType(С,Н);φ.SearchBlocksOfName(pamTag.Substring(0,pamTag.Length-1)+":",ƣ,q=>q.CubeGrid==Me.CubeGrid&&q is
IMyTextSurfaceProvider);С=ћ(С,pamTag,true);Ѵ(ƣ);М();ŕ(С,setLCDFontAndSize,1.15f,false);ŕ(Р,setLCDFontAndSize,1.15f,true);String Ƣ=ǲ(false);
String ơ="//Custom Data is obsolete, please delete it.\n\n";foreach(IMyTerminalBlock ƨ in С){if(ƨ.CustomData==""){ƨ.CustomData
=ǲ(true);continue;}if(!ƨ.CustomData.Contains(Ƣ)){if(!ƨ.CustomData.Contains(ơ))ƨ.CustomData=ơ+ƨ.CustomData;}}}void Ơ(){ǌ.
Clear();ƞ=null;Ј(Ϸ.ϵ);Є=0;}ɐ ƞ=null;String Ɯ(bool ƛ){int Ɲ=0;return Ɯ(ƛ,0,ref Ɲ,false,1);}String Ɯ(bool ƛ,int ƚ,ref int ƙ,
bool Ƙ,int Ɵ){String Ɨ="";String Ʃ="——————————————————\n";String ƶ="--------------------------------------------\n";if(ƞ==
null||Ѓ==Ϸ.ϵ||Ѓ==Ϸ.Ϻ)Ɨ+="[PAM]-Controller | "+ǌ.Count+" Connected ships"+"\n";else Ɨ+=Ʈ(ƞ)+"\n";Ɨ+=Ʃ;int J=0;int ƴ=Є;Ѕ=0;if(
ύ!=""){ό(ref Ɨ,ƴ,J++,ƛ,ύ);}else if(Ѓ==Ϸ.ϵ){if(ƞ==null&&ǌ.Count>=1)ƞ=ǌ[0];String Ƴ="";if(ό(ref Ƴ,ƴ,J++,ƛ,
" Send command to all"))Ј(Ϸ.Ϻ);int Ʋ=0;for(int ä=0;ä<ǌ.Count;ä++){if(Ƙ)Ƴ+="\n";ɐ Ʊ=ǌ[ä];if(ƴ==J||ƴ==J+1)ƞ=Ʊ;if(J==ƴ)Ʋ=Ƴ.Split('\n').Length-1;
if(ό(ref Ƴ,ƴ,J++,ƛ," "+Ʈ(Ʊ))){ƞ=Ʊ;Ј(Ϸ.ϫ);ƞ.Ǫ("",false,true,false);}if(J==ƴ)Ʋ=Ƴ.Split('\n').Length-1;if(ό(ref Ƴ,ƴ,J++,ƛ," "
+(Ƙ?"":"  ")+ƭ(Ʊ))){ƞ=Ʊ;Ј(Ϸ.Ƕ);}}int ư=Ɵ-2;ư+=Math.Max(0,(ƚ-1))*Ɵ;Ɨ+=Ǧ(ư,Ƴ,Ʋ,ref ƙ);}else if(Ѓ==Ϸ.ſ||Ѓ==Ϸ.Ϻ){Ϸ B=Ѓ==Ϸ.ſ?Ϸ
.ϫ:Ϸ.ϵ;ɐ Ʀ=null;if(Ѓ==Ϸ.ſ)Ʀ=ƞ;if(ό(ref Ɨ,ƴ,J++,ƛ," Back")){Ј(B);};if(ό(ref Ɨ,ƴ,J++,ƛ," [Stop] "+ǒ(Ʀ,"STOP"))){Ƨ(Ʀ,"STOP")
;};if(ό(ref Ɨ,ƴ,J++,ƛ," [Continue] "+ǒ(Ʀ,"CONT"))){Ƨ(Ʀ,"CONT");};if(ό(ref Ɨ,ƴ,J++,ƛ," [Move home] "+ǒ(Ʀ,"HOMEPOS"))){Ƨ(Ʀ,
"HOMEPOS");};if(ό(ref Ɨ,ƴ,J++,ƛ," [Move to job] "+ǒ(Ʀ,"JOBPOS"))){Ƨ(Ʀ,"JOBPOS");};if(ό(ref Ɨ,ƴ,J++,ƛ," [Full simulation] "+ǒ(Ʀ,
"FULL"))){Ƨ(Ʀ,"FULL");};if(Ʀ!=null&&Ʀ.ƽ==Г.Ͻ)if(ό(ref Ɨ,ƴ,J++,ƛ," [Undock] "+ǒ(Ʀ,"UNDOCK"))){Ƨ(Ʀ,"UNDOCK");};}else if(Ѓ==Ϸ.ϫ){
String Ư="";if(ό(ref Ư,ƴ,J++,ƛ," Back")){Ј(Ϸ.ϵ);};Ɨ+=Ư.Substring(0,Ư.Length-1).PadRight(36,' ');if(ό(ref Ɨ,ƴ,J++,ƛ," Send cmd"
)){Ј(Ϸ.ſ);};if(!ƞ.Ƀ()&&!ƞ.ɂ())Ѕ++;if(ƞ.ɂ())Ɨ+="No answer received...";else Ɨ+=ƪ(ƞ,ƞ.Ƿ);}else if(Ѓ==Ϸ.Ƕ){if(ό(ref Ɨ,ƴ,J++,
ƛ," Back")){Ј(Ϸ.ϵ);};Ɨ+=ƶ;Ɨ+=ƪ(ƞ,Ǘ(ƞ));}if(!Љ)Є=Math.Min(Ѕ-1,Є);return Ɨ;}String Ʈ(ɐ Ʀ){if(Ʀ.ɍ!=VERSION)return Ʀ.õ+
": Different version!";return Ʀ.õ+": "+ƪ(Ʀ,Ʀ.ɋ)+" "+Ʀ.Ʌ+"m";;}String ƭ(ɐ Ʀ){String Q=Ǿ("",µ(Ʀ.Ɇ,Ʀ.ɇ)*100f,100f,8,0,0)+"% ";for(int J=0;J<Ʀ.Ƕ.
Count;J++){if(J>=5)break;Ņ ĵ=Ʀ.Ƕ[J];Q+=ǚ(ǿ(Ƭ(ĵ)),3)+" "+Ǖ(ĵ.ń)+" ";}return Q;}String Ƭ(Ņ ĵ){if(ĵ.Ń.ToUpper()=="ORE"||ĵ.Ń.
ToUpper()=="INGOT"){String ƫ=GetElementCode(ĵ.õ.ToUpper());if(ƫ!="")return ƫ;}return ĵ.õ;}String ƪ(ɐ Ʀ,String Q){if(Ʀ.Ƀ())
return"No signal...("+ǔ((int)(DateTime.Now-Ʀ.ɏ).TotalSeconds)+")";return Q;}public enum ǐ{Ǻ,Ǹ,Ƿ,Ƕ,ǵ,Ǵ,ǳ}String ǲ(bool Ǳ){
String B="";if(Ǳ)B+="mode=main:1\n\n";B+="//Available modes:\n"+"//main:<Page>\n"+"//mainX:<Page>  (no empty lines)\n"+
"//menu\n"+"//inventory\n"+"//menu:<shipname>\n"+"//inventory:<shipname>";return B;}void ǰ(IMyTerminalBlock ƨ,out ǐ ǯ,out String Ǩ
){bool Ɲ=true;String ƽ=ɔ(ɓ(ƨ.CustomData.Split('\n'),"mode",ref Ɲ)).ToUpper();String[]Ǯ=ƽ.Split(':');String ǭ=Ǭ(Ǯ,0);Ǩ=Ǭ(Ǯ
,1);ǯ=ǐ.Ǻ;if(ǭ=="MAIN")ǯ=ǐ.Ǹ;else if(ǭ=="MAINX")ǯ=ǐ.ǳ;else if(ǭ=="MENU")ǯ=ǐ.Ƿ;else if(ǭ=="INVENTORY")ǯ=ǐ.Ƕ;else if(ǭ==
"DEBUG")ǯ=ǐ.ǵ;if(ǯ==ǐ.Ǻ)Ǩ=ƽ;}String Ǭ(String[]ª,int F){if(F<ª.Length)return ª[F].Trim();return"";}int ǫ=0;int ǹ=0;int ǻ=0;int Ȇ
=0;int ȇ=0;int ȅ=0;int Ȅ=0;String ȃ(ǐ ƽ,String Ǩ,bool È){int Ȃ=15;if(È){ǹ=ǫ;ǫ=0;Ȇ=ǻ;ǻ=0;}String B="";if(ƽ==ǐ.Ǹ){int F=0;B
=Ɯ(false,ǹ,ref ȇ,true,Ȃ);if(!int.TryParse(Ǩ,out F))return B;ǫ=Math.Max(F,ǫ);F--;return ǡ(F,Ȃ,B);}else if(ƽ==ǐ.ǳ){int F=0;
B=Ɯ(false,Ȇ,ref ȅ,false,Ȃ);if(!int.TryParse(Ǩ,out F))return B;ǻ=Math.Max(F,ǻ);F--;return ǡ(F,Ȃ,B);}if(ƽ==ǐ.Ǵ){return Ɯ(
false,0,ref Ȅ,true,Ȃ);}else if(ƽ==ǐ.Ƕ||ƽ==ǐ.Ƿ||ƽ==ǐ.ǵ){ɐ Ʀ=ƞ;if(Ǩ!=""){Ʀ=Ǎ("",Ǩ);if(Ʀ==null)return"Unknown ship: "+Ǩ;}else{if
(Ʀ==null)return"No ship on main screen selected.";B="Selected: ";}String Ʃ="—————————————————————\n";String ȁ="";String Ȁ
="";if(ƽ==ǐ.Ƿ){ȁ="Menu";Ȁ=ƪ(Ʀ,Ʀ.Ƿ);}else if(ƽ==ǐ.Ƕ){ȁ="Inventory";Ȁ=ƪ(Ʀ,Ǘ(Ʀ));}else if(ƽ==ǐ.ǵ){return ƪ(Ʀ,ǘ(Ʀ));}B+=Ʀ.õ+
" - "+Ʀ.Ʌ+"m | "+ǿ(""+ȁ)+"\n"+Ʃ;B+=Ȁ;return B;}return"Unknown command: "+Ǩ;;}String ǿ(String Q){if(Q=="")return Q;return Q.
First().ToString().ToUpper()+Q.Substring(1).ToLower();}string Ǿ(String õ,float ń,float Ğ,int ǽ,int Ǽ,int ǩ){float Ǜ=µ(ń,Ğ)*ǽ;
String Q="[";for(int J=0;J<ǽ;J++){if(J<=Ǜ)Q+="|";else Q+="'";}Q+="]";return Q+" "+ǚ(ǿ(õ),Ǽ).PadRight(Ǽ)+"".PadRight(ǩ)+Ǖ(ń);}
String ǚ(String Q,int Ǚ){if(Q=="")return Q;if(Q.Length>Ǚ)Q=Q.Substring(0,Ǚ-1)+".";return Q;}string ǘ(ɐ Ʀ){String Q="";Q+=Ʀ.ɍ+
"\n";Q+=Ʀ.õ+"\n";Q+=Ʀ.ɉ.X+"\n";Q+=Ʀ.ɉ.Y+"\n";Q+=Ʀ.ɉ.Z+"\n";Q+=Ʀ.ɋ+"\n";Q+=Ʀ.Ɍ+"\n";Q+=Ʀ.Ʋ+"\n";Q+=Ʀ.Ƿ.Length+"\n";Q+=Ʀ.Ɇ+
"\n";Q+=Ʀ.ɇ+"\n";Q+=Ʀ.Ƕ.Count()+"\n";return Q;}string Ǘ(ɐ Ʀ){String Q="";Q+=Ǿ("All",µ(Ʀ.Ɇ,Ʀ.ɇ)*100f,100,30,8,12)+"%\n";Q+=
"\n";for(int ä=0;ä<Ʀ.Ƕ.Count();ä++){Ņ ĵ=Ʀ.Ƕ[ä];Q+=Ǿ(ĵ.õ,ĵ.ń,Ʀ.Ʉ,30,8,10)+"\n";}return Q;}String Ǖ(float ń){if(ń>=1000000)
return Math.Round(ń/1000000f,ń/1000000f<100?1:0)+"M";if(ń>=1000)return Math.Round(ń/1000f,ń/1000f<100?1:0)+"K";return""+Math.
Round(ń);}String ǔ(int Ǔ){if(Ǔ>=60*60)return Math.Round(Ǔ/(60f*60f),1)+" h";if(Ǔ>=60)return Math.Round(Ǔ/60f,1)+" min";return
""+Ǔ+" s";}String ǒ(ɐ Ʀ,String ſ){Ⱥ Ƶ=Ǉ(Ʀ,false,ſ);if(Ƶ==Ⱥ.ɒ)return"received!";if(Ƶ==Ⱥ.Ɂ)return"pending...";if(Ƶ==Ⱥ.ɑ)
return"no answer!";return"";}void ǖ(){String Ǒ="[PAM]-Controller\n\n"+"Run-arguments: (Type without:[ ])\n"+
"———————————————\n"+"[UP] Menu navigation up\n"+"[DOWN] Menu navigation down\n"+"[APPLY] Apply menu point\n"+"[CLEAR] Clear miner list\n"+
"[SEND ship:cmd] Send to a ship*\n"+"[SENDALL cmd] Send to all ships*\n"+"———————————————\n\n"+"*[SEND] = Cmd to one ship:\n"+
" e.g.: \"SEND Miner 1:homepos\"\n\n"+"*[SENDALL] = Cmd to all ships:\n"+" e.g.: \"SENDALL homepos\"\n\n";for(int J=0;J<Р.Count;J++)Р[J].WriteText(ȃ(ǐ.Ǵ,"0",
false));for(int J=0;J<С.Count;J++){ǐ ƽ=ǐ.Ǻ;String Ǩ="";ǰ(С[J],out ƽ,out Ǩ);С[J].WriteText(ȃ(ƽ,Ǩ,J==0));}Echo(Ǒ);for(int J=0;J
<П.Count;J++){IMyTextPanel ƨ=П[J];String ǧ=ƨ.CustomData.ToUpper();if(ǧ==ȡ)ƨ.WriteText(ǵ+"\n"+Ƞ);if(ǧ==Ȣ)ƨ.WriteText(ȝ());
}}String Ǧ(int ǥ,String ŭ,int Ǥ,ref int ǣ){String[]Ǟ=ŭ.Split('\n');if(Ǥ>=ǣ+ǥ-1)ǣ++;ǣ=Math.Min(Ǟ.Count()-1-ǥ,ǣ);if(Ǥ<ǣ+1)ǣ
--;ǣ=Math.Max(0,ǣ);String B="";for(int J=0;J<ǥ;J++){int Ǣ=J+ǣ;if(Ǣ>=Ǟ.Count())break;B+=Ǟ[Ǣ]+"\n";}return B;}String ǡ(int F
,int Ǡ,String ǟ){String[]Ǟ=ǟ.Split('\n');int ǝ=F*Ǡ;int ǜ=(F+1)*(Ǡ);String B="";for(int J=ǝ;J<ǜ;J++){if(J>=Ǟ.Count())break
;B+=Ǟ[J]+"\n";}return B;}
