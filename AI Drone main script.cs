/*
 * R e a d m e
 * -----------
 * 
 * In this file you can include any instructions or other comments you want to have injected onto the 
 * top of your final script. You can safely delete this file if you do not want any such comments.
 */
const double Ĝ=125;const double ĝ=0.725;const double Ğ=7500;const double ğ=10000;const double Ġ=3000;const double ġ=800;
const double Ģ=3;const double ģ=1500;const double Ĥ=1000;const double ĥ=5000;int Ħ=0;const int ħ=30;string Ĩ="Fighter";string
ĩ="Gremlin";bool ī=false;bool ĺ=false;bool Ĭ=false;double ĭ=1.37;Đ Į=0;IMyRadioAntenna į;IMyRadioAntenna İ;List<IMyThrust
>ı=new List<IMyThrust>();List<IMyThrust>Ĳ=new List<IMyThrust>();List<IMyThrust>ĳ=new List<IMyThrust>();List<IMyThrust>Ĵ=
new List<IMyThrust>();List<IMyThrust>ĵ=new List<IMyThrust>();List<IMyThrust>Ķ=new List<IMyThrust>();List<IMyCameraBlock>ķ=
new List<IMyCameraBlock>();List<IMyTerminalBlock>ĸ=new List<IMyTerminalBlock>();IMyRemoteControl Ĺ;List<IMyGyro>Ļ=new List<
IMyGyro>();List<IMyUserControllableGun>Ī=new List<IMyUserControllableGun>();int ě=750;Vector3D Á;Vector3D ā;bool Ă=false;
Vector3D Ã;Vector3D Ä;double æ;double ă;double Ą=0.1;Ö ą=new Ö(5,0.1,2,0.75);double Ć=2;double ć=200000;Ö Ĉ=new Ö(5,0.3,3,0.75);
Ö ĉ=new Ö(5,0.3,3,0.75);Ö Ċ=new Ö(5,0.0,2,0.75);int ċ=0;const int Č=150;int ę=0;const int č=200;Random Ď=new Random();
IMyProgrammableBlock ď;enum Đ{đ=0,Ē,ē,Ĕ,ĕ}Program(){ļ();}void Main(string Ė,UpdateType ė){if(!ī){Runtime.UpdateFrequency=0;return;}if(
Runtime.TimeSinceLastRun.TotalSeconds!=0){æ+=Runtime.TimeSinceLastRun.TotalSeconds;}if(æ<Ą)return;ă+=æ;if(Ņ()){ĺ=true;}if((ė&
UpdateType.Update1)!=0){bool Ę=Ă;ā=Á;Ă=Ĺ.GetNearestPlayer(out Á);if(Á==Vector3D.Zero)Ă=false;if(Ă&&!Ę)ā=Á;Ã=Ĺ.GetPosition();Ä=Ĺ.
GetShipVelocities().LinearVelocity;Vector3D y;if(w(Ä.Length(),Ä*2+100,out y)){Ě(y,y-Ã,Vector3D.Zero,Vector3D.Zero);}ś();Echo(
"Delta time:"+æ);Echo("State:"+Į);æ=0;}}void ļ(){IMyBlockGroup Ľ=GridTerminalSystem.GetBlockGroupWithName(Ĩ);if(Ľ==null){Echo(
"ERROR: NULL BLOCK GROUP");return;}List<IMyTerminalBlock>ŕ=new List<IMyTerminalBlock>();Ľ.GetBlocks(ŕ);List<IMyThrust>Ŗ=new List<IMyThrust>();
foreach(IMyTerminalBlock œ in ŕ){if(œ is IMyRemoteControl)Ĺ=œ as IMyRemoteControl;else if(œ is IMyUserControllableGun)Ī.Add(œ
as IMyUserControllableGun);else if(œ is IMyThrust)Ŗ.Add(œ as IMyThrust);else if(œ is IMyGyro)Ļ.Add(œ as IMyGyro);else if(œ
is IMyCameraBlock){ķ.Add(œ as IMyCameraBlock);(œ as IMyCameraBlock).EnableRaycast=true;}else if(œ is IMyProgrammableBlock)
{ĸ.Add(œ as IMyProgrammableBlock);if(œ.CustomName.Contains("Missile"))ď=œ as IMyProgrammableBlock;}else if(œ is
IMyRadioAntenna){į=œ as IMyRadioAntenna;if(œ.HasAction("FuckingTalk")&&œ.CustomName.Contains("Chat")){İ=œ as IMyRadioAntenna;}}}if(Ĺ==
null){Echo("ERROR: NULL SHIP REF");return;}if(Ŗ.Count()==0){Echo("ERROR: NO THRUSTERS");return;}if(Ļ.Count()==0){Echo(
"ERROR: NO GYROS");return;}foreach(IMyThrust A in Ŗ){Vector3D ŗ=A.WorldMatrix.Backward;double Ř=ŗ.Dot(Ĺ.WorldMatrix.Forward);double ř=ŗ.
Dot(Ĺ.WorldMatrix.Right);double Ś=ŗ.Dot(Ĺ.WorldMatrix.Up);if(Ř>0.7)ı.Add(A);if(Ř<-0.7)Ĳ.Add(A);if(ř>0.7)ĳ.Add(A);if(ř<-0.7)
Ĵ.Add(A);if(Ś>0.7)ĵ.Add(A);if(Ś<-0.7)Ķ.Add(A);}ĸ.Add(Ĺ);ī=true;Runtime.UpdateFrequency=UpdateFrequency.Update1;Echo(
"Setup complete!");}void ś(){if(ĺ){return;}switch(Į){case Đ.Ē:ŝ();break;case Đ.ē:š();break;case Đ.Ĕ:ş();break;case Đ.ĕ:ŀ();break;case Đ.đ
:default:Ŝ();break;}}void Ŝ(){Vector3D º=Á-Ã;if(Ä.LengthSquared()>0.01){Ě(Ã,Vector3D.Zero,Vector3D.Zero,Vector3D.Zero);}
else{Z();X();}if(º.LengthSquared()<Ğ*Ğ&&Ă){Į=Đ.Ē;}}void ŝ(){Vector3D º=Á-Ĺ.GetPosition();double ľ=º.LengthSquared();double Ş
=Ä.LengthSquared();Vector3D Š=M(Ä,º);double ŧ=Ĝ*ĝ;if(Š.LengthSquared()<ŧ*ŧ||Ä.Dot(º)<0){Ě(Á,º,Vector3D.Zero,Vector3D.Zero
);}else{Z();X();if(Ş<Ĝ*Ĝ*0.9&&ľ>Ĥ*Ĥ&&ľ<ĥ*ĥ){ď.TryRun("Fire");}}if(ľ<Ġ*Ġ){Į=Đ.ē;}if(ľ>ğ*ğ||!Ă){Į=Đ.đ;}}void š(){Vector3D º
=Á-Ã;double Ł=æ;if(Ł==0)Ł=0.1;Vector3D Â=(Á-ā)/Ł;Vector3D ł=À(Á,Â,Ã,Ä,ě,Ł)+Á;double Ţ=(Ĝ+Ä.Length())/2;Vector3D ţ=À(Á,Â,Ã
,Vector3D.Zero,Ţ,Ł)+Á;Vector3D h=Ĺ.WorldMatrix.Up;int Ŀ=Ď.Next(4);if(Ŀ==1)h=Ĺ.WorldMatrix.Right+Ĺ.WorldMatrix.Down;else
if(Ŀ==2)h=Ĺ.WorldMatrix.Left+Ĺ.WorldMatrix.Down;if((ł-Ã).LengthSquared()<ġ*ġ){Vector3D m=Ĺ.WorldMatrix.Forward;Vector3D Ń=
ł-Ã;Ń.Normalize();if(m.Dot(Ń)>0.99){ō(true);}else{ō(false);}Ě(Ã+Ĺ.WorldMatrix.Forward*10,Ä,Ń,h);}else{ō(false);Ě(ţ,Â,
Vector3D.Zero,h);}Vector3D Ť=Â-Ä;double ť=º.Length()/Ť.Length();double Ŧ=W(º,Ť);if(Ä.LengthSquared()<Ĝ*Ĝ*0.9&&º.LengthSquared()>
Ĥ*Ĥ&&º.LengthSquared()<ĥ*ĥ){ď.TryRun("Fire");}if(ť<Ģ&&Ŧ<10){ō(false);Į=Đ.Ĕ;Ħ=0;ę=0;string Ó=String.Format(
"{3} - ENGAGED TARGET AT GPS:TARGET:{0:F2}:{1:F2}:{2:F2}:",Á.X,Á.Y,Á.Z,ĩ.ToUpper());Ñ(İ,Ó);}if(º.LengthSquared()>ğ*ğ||!Ă){ō(false);Į=Đ.đ;}}void ş(){Vector3D º=Á-Ã;Vector3D ń=ā-(Ã
-Ä*æ);double ľ=º.LengthSquared();Vector3D h=Ĺ.WorldMatrix.Up;if(Ä.Dot(º)>0){h=º*-1;h.Normalize();Vector3D Ā=Ã+h.Cross(Ĺ.
WorldMatrix.Right)*5000-Ä*35;Ě(Ā,Ā-Ã,Vector3D.Zero,Vector3D.Zero);}else{int Ŀ=Ď.Next(4);if(Ŀ==1)h=Ĺ.WorldMatrix.Right+Ĺ.WorldMatrix
.Down;else if(Ŀ==2)h=Ĺ.WorldMatrix.Left+Ĺ.WorldMatrix.Down;Ě(Ã-º,-º*100,Vector3D.Zero,h);}if(ľ<ń.LengthSquared()+100){Ħ++
;}ę++;if(Ħ>ħ){Į=Đ.ĕ;ċ=0;Ħ=0;ę=0;}if(ľ>ģ*ģ&&ľ<Ġ*Ġ){Į=Đ.ē;ę=0;}if(ę>č){Į=Đ.ē;ę=0;}if(ľ>ğ*ğ||!Ă){Į=Đ.đ;ę=0;}}void ŀ(){double
Ł=æ;if(Ł==0)Ł=0.1;Vector3D º=Á-Ĺ.GetPosition();Vector3D Â=(Á-ā)/Ł;Vector3D ł=À(Á,Â,Ã,Ä,ě,Ł)+Á;Vector3D Ń=ł-Ã;double ľ=º.
LengthSquared();ċ+=1;Vector3D h=Ĺ.WorldMatrix.Up;int Ŀ=Ď.Next(4);if(Ŀ==1)h=Ĺ.WorldMatrix.Right+Ĺ.WorldMatrix.Down;else if(Ŀ==2)h=Ĺ.
WorldMatrix.Left+Ĺ.WorldMatrix.Down;if((ł-Ã).LengthSquared()<ġ*ġ*1.5){Vector3D m=Ĺ.WorldMatrix.Forward;Ń.Normalize();if(m.Dot(Ń)>
0.99){ō(true);}else{ō(false);}}else{ō(false);}Ě(Ã-º*100,-º*100,Ń,h);if(ċ>Č){Į=Đ.ē;ċ=0;}if(ľ>ģ*ģ&&ľ<Ġ*Ġ){Į=Đ.ē;}if(ľ>ğ*ğ||!Ă)
{Į=Đ.đ;}}bool Ņ(){foreach(IMyTerminalBlock œ in ĸ){if(œ==null)return true;if(!œ.IsFunctional)return true;}return false;}
IMyCameraBlock ņ(List<IMyCameraBlock>Ň,Vector3D ň){const double ŉ=0.70712;foreach(IMyCameraBlock Ŋ in Ň){if(Ŋ==null)continue;Vector3D
j=ň-Ŋ.GetPosition();Vector3D ŋ=Ŋ.WorldMatrix.Forward;double Ō=j.Length();if(Ō==0)continue;if(Ŋ.CanScan(Ō)&&Ŋ.CanScan(ň)&&
ŉ<j.Dot(ŋ)/Ō){return Ŋ;}}return null;}void ō(bool Ŏ,bool ŏ=false){if(Ĭ==Ŏ&&!ŏ)return;Ĭ=Ŏ;foreach(IMyUserControllableGun Ő
in Ī){if(Ő==null)continue;if((Ő is IMySmallGatlingGun)||(Ő is IMySmallMissileLauncher)){Ő.ApplyAction("OnOff_On");Ő.
SetValueBool("Shoot",Ŏ);}}}double ő(Vector3D Œ){double Ŕ=Œ.Length()/ĭ;return Ŕ*Œ.Length()/2;}void Ě(Vector3D Ā,Vector3D d,Vector3D g
,Vector3D h){double i=Ĺ.CalculateShipMass().TotalMass;Vector3D j=Ā-Ã;double k=j.Length();if(k==0)k=0.0000001;Vector3D l=d
-Ä;Vector3D m=Ĺ.WorldMatrix.Forward;Vector3D n=Ĺ.WorldMatrix.Right;Vector3D o=Ĺ.WorldMatrix.Up;double p=ą.ä(k,æ);Vector3D
q=Vector3D.Zero+j/k*p+l*Ć;q*=i;ê(q,Ĺ);if(g==Vector3D.Zero){g=m;if(q.LengthSquared()>ć*ć){g=q*1;g.Normalize();}}if(h==
Vector3D.Zero){h=Ĺ.WorldMatrix.Up;}double r=0;double s=0;double u=0;ô(g,m,n*-1,o,out r,out s);r=Ĉ.ä(r,æ);s=ĉ.ä(s,æ);if(h.Dot(o)>
0){u=Math.PI/2-Math.Acos(MathHelper.Clamp(h.Dot(n),-1,1));}else{u=Math.PI+Math.Acos(MathHelper.Clamp(h.Dot(n),-1,1));}u=Ċ
.ä(u,æ);þ(s,r,u,Ļ,Ĺ);}bool w(double È,Vector3D x,out Vector3D y){y=Vector3D.Zero;foreach(IMyCameraBlock z in ķ){if(z.
AvailableScanRange>=È&&z.WorldMatrix.Forward.Dot(x)>0.707){MyDetectedEntityInfo ª=z.Raycast(È);if(ª.IsEmpty())continue;Vector3D µ=(ª.
HitPosition??Vector3D.Zero)-Ã;Vector3D º=ª.Position-Ã;if(º.LengthSquared()<100||µ.LengthSquared()<100)continue;}}return false;}
Vector3D À(Vector3D Á,Vector3D Â,Vector3D Ã,Vector3D Ä,double Å,double Æ=0){Vector3D Ç=Á+Æ*Â-Ã;Vector3D É=Â-Ä;double v=Å;double
e=v*v;double O=Ç.Dot(Ç);double B=É.Dot(É);double C=Ç.Dot(É);double E=B-e;double F=2*C;double G=O;double H=F*F-4*E*G;if(H<
0){return Á;}double I=(-F+Math.Sqrt(H))/(2*E);double J=(-F-Math.Sqrt(H))/(2*E);if(I<0&&J<0){return Á+Æ*Â;}double K=Math.
Max(I,J);Vector3D L=(Vector3D)(É)*K;return L;}Vector3D M(Vector3D E,Vector3D F){Vector3D N=E.Dot(F)/F.LengthSquared()*F;
return N;}Vector3D Q(Vector3D E,Vector3D F,double R=1){Vector3D S=M(E,F);Vector3D T=E-S;Vector3D U=S-T*R;return U;}double W(
Vector3D E,Vector3D F){if(Vector3D.IsZero(E)||Vector3D.IsZero(F))return 0;else return Math.Acos(MathHelper.Clamp(E.Dot(F)/Math.
Sqrt(E.LengthSquared()*F.LengthSquared()),-1,1));}void X(){foreach(IMyGyro Y in Ļ){Y.Pitch=0f;Y.Yaw=0f;Y.Roll=0f;}}void Z(){
foreach(IMyThrust A in ı){if(A==null)continue;A.SetValueFloat("Override",float.MinValue);}foreach(IMyThrust A in Ĳ){if(A==null)
continue;A.SetValueFloat("Override",float.MinValue);}foreach(IMyThrust A in ĳ){if(A==null)continue;A.SetValueFloat("Override",
float.MinValue);}foreach(IMyThrust A in Ĵ){if(A==null)continue;A.SetValueFloat("Override",float.MinValue);}foreach(IMyThrust
A in ĵ){if(A==null)continue;A.SetValueFloat("Override",float.MinValue);}foreach(IMyThrust A in Ķ){if(A==null)continue;A.
SetValueFloat("Override",float.MinValue);}}void ê(Vector3D ë,IMyTerminalBlock Ê){if(ĺ)return;Vector3D ì=Ê.WorldMatrix.Forward;
Vector3D í=Ê.WorldMatrix.Right;Vector3D î=Ê.WorldMatrix.Up;float ï=(float)Math.Round(ì.Dot(ë),4);float ð=(float)Math.Round(í.Dot
(ë),4);float ñ=(float)Math.Round(î.Dot(ë),4);Echo(ë.ToString());foreach(IMyThrust A in ı){if(A==null)continue;if(ï>0){
float ò=Math.Min(ï,A.MaxThrust);ï-=ò;A.SetValueFloat("Override",ò);}else{A.SetValueFloat("Override",float.MinValue);}}foreach
(IMyThrust A in Ĳ){if(A==null)continue;if(ï<0){float ò=Math.Min(-ï,A.MaxThrust);ï+=ò;A.SetValueFloat("Override",ò);}else{
A.SetValueFloat("Override",float.MinValue);}}foreach(IMyThrust A in ĳ){if(A==null)continue;if(ð>0){float ò=Math.Min(ð,A.
MaxThrust);ð-=ò;A.SetValueFloat("Override",ò);}else{A.SetValueFloat("Override",float.MinValue);}}foreach(IMyThrust A in Ĵ){if(A==
null)continue;if(ð<0){float ò=Math.Min(-ð,A.MaxThrust);ð+=ò;A.SetValueFloat("Override",ò);}else{A.SetValueFloat("Override",
float.MinValue);}}foreach(IMyThrust A in ĵ){if(A==null)continue;if(ñ>0){float ò=Math.Min(ñ,A.MaxThrust);ñ-=ò;A.SetValueFloat(
"Override",ò);}else{A.SetValueFloat("Override",float.MinValue);}}foreach(IMyThrust A in Ķ){if(A==null)continue;if(ñ<0){float ò=
Math.Min(-ñ,A.MaxThrust);ñ+=ò;A.SetValueFloat("Override",ò);}else{A.SetValueFloat("Override",float.MinValue);}}}void ô(
Vector3D õ,Vector3D ö,Vector3D ø,Vector3D ù,out double ú,out double û){var ü=M(õ,ù);var ý=õ-ü;ú=W(ö,ý);û=W(õ,ý);ú*=-1*Math.Sign(
ø.Dot(õ));û*=Math.Sign(ù.Dot(õ));if(û==0&&ú==0&&õ.Dot(ö)<0){ú=Math.PI;}}void þ(double ÿ,double ó,double é,List<IMyGyro>Ø,
IMyTerminalBlock Ê,int Ë=0){if(ĺ)return;var Ì=new Vector3D(-ÿ,ó,é);var Í=Ê.WorldMatrix;var Î=Vector3D.TransformNormal(Ì,Í);foreach(
IMyGyro Y in Ø){var Ï=Y.WorldMatrix;var Ð=Vector3D.TransformNormal(Î,Matrix.Transpose(Ï));if(Ë>0){Y.Pitch=(float)Ð.X;Y.Yaw=(
float)Ð.Y;Y.Roll=(float)Ð.Z;}else{Y.Pitch=0f;Y.Yaw=0f;Y.Roll=0f;}Y.GyroOverride=true;Ë++;}}void Ñ(IMyRadioAntenna Ò,string Ó)
{if(Ò==null)return;if(!Ò.IsFunctional||!Ò.IsWorking||!Ò.Enabled)return;string Ô="FuckingTalk";string Õ="setTalkMessage";Ò
.SetValue<string>(Õ,Ó);Ò.ApplyAction(Ô);}class Ö{double Ù=0;double ç=0;double Ú=0;double Û=0;double Ü=0;double Ý=0;public
int Þ=0;public double ß{get;private set;}public Ö(double à,double á,double â,double ã){Ù=à;ç=á;Ú=â;Û=ã;}public double ä(
double å,double æ){double f=(å-Ý)/æ;if(Þ==0){f=0;}Þ++;if(Û!=0)Ü=Ü*(Û);Ü+=å*æ;Ý=å;this.ß=Ù*å+ç*Ü+Ú*f;return this.ß;}public void
è(){Þ=0;Ü=0;Ý=0;}}
