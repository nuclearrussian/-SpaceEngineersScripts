/*
 * R e a d m e
 * -----------
 * 
 * This implements a homing missile that's for NPCs to use only. These implement augmented pronav guidance to hit players.
 */
List<å>ú=new List<å>();List<å>û=new List<å>();double ü=0;double ý=1;int þ=2;double ø=0;double ÿ=0;string Ā="missile";
static Program ā;public enum ă{ċ=0,Ą,ą,Ć}Program(){ù();ā=this;}void Main(string ć,UpdateType Ĉ){if(Runtime.TimeSinceLastRun.
TotalSeconds>0){ø=Runtime.TimeSinceLastRun.TotalSeconds;ÿ+=ø;}if((Ĉ&UpdateType.Update1)!=0){Echo("STIME"+ÿ);if(û.Count()==0){Runtime
.UpdateFrequency=UpdateFrequency.None;return;}for(int ĉ=0;ĉ<û.Count();ĉ++){å Ċ=û[ĉ];if(Ċ.ï){û.Remove(Ċ);ĉ--;ä();continue;
}if(ÿ>=Ċ.ñ){Echo("RUN MISSILE");Ċ.Ď(ÿ);}}}if(ć.ToLower()=="fire"){if(û.Count()>=þ||ú.Count()==0||ÿ<ü+ý)return;å Ċ=ú[0];ú.
Remove(Ċ);û.Add(Ċ);Runtime.UpdateFrequency=UpdateFrequency.Update1;ä();ü=ÿ;}}void ù(){ä();List<IMyBlockGroup>â=new List<
IMyBlockGroup>();GridTerminalSystem.GetBlockGroups(â);foreach(IMyBlockGroup ö in â){if(!ö.Name.Contains(Ā))continue;å ã=new å(ö);if(ã
.ó&&!ã.ï){ú.Add(ã);}}ä();}void ä(){Me.CustomData=""+ú.Count()+":"+û.Count();}class å{public IMyRemoteControl æ;public
IMyBeacon ç;public IMyRadioAntenna è;public IMySmallGatlingGun é;public List<IMyTerminalBlock>ê=new List<IMyTerminalBlock>();
public List<IMyWarhead>ë=new List<IMyWarhead>();public List<IMyGyro>ì=new List<IMyGyro>();public List<IMyThrust>í=new List<
IMyThrust>();bool î=false;public bool ï=false;ă ð=ă.ċ;public double ñ=0;public const double ò=0.1;public bool ó=false;const
double ô=3;const double õ=1.5;double Ă=0;double ø=0;double Č=1.5;Vector3D ğ;Vector3D Ġ;Vector3D ġ;Vector3D Ģ;Vector3D ģ;
Vector3D Ĥ;bool ĥ=false;bool Ħ=false;bool ħ=false;double Ĩ;double ĩ=2;double ı=14400;double Ī=10000;const double ī=0;double Ĭ=
4.5+2.3;int ĭ=0;double Į=9999999999;Í į=new Í(100,0.0,35,0.5);Í İ=new Í(100,0.0,35,0.5);public å(IMyBlockGroup ö){List<
IMyTerminalBlock>Ĳ=new List<IMyTerminalBlock>();ö.GetBlocks(Ĳ);foreach(IMyTerminalBlock g in Ĳ){if(g is IMySmallGatlingGun)é=g as
IMySmallGatlingGun;else if(g is IMyRemoteControl){æ=g as IMyRemoteControl;ê.Add(g);}else if(g is IMyReactor){ê.Add(g);(g as IMyReactor).
Enabled=false;}else if(g is IMyGyro)ì.Add(g as IMyGyro);else if(g is IMyWarhead)ë.Add(g as IMyWarhead);else if(g is IMyBeacon)ç
=g as IMyBeacon;else if(g is IMyThrust)í.Add(g as IMyThrust);else if(g is IMyRadioAntenna)è=g as IMyRadioAntenna;}if(ê.
Count()==0)return;if(æ==null)return;if(ç==null&&è==null)return;if(é==null)return;for(int ĉ=0;ĉ<í.Count();ĉ++){í[ĉ].
ThrustOverridePercentage=0;Vector3D ė=í[ĉ].WorldMatrix.Backward;if(ė.Dot(æ.WorldMatrix.Forward)<0.707){í.Remove(í[ĉ]);ĉ--;continue;}}if(ç!=null)
ç.Enabled=false;if(è!=null)è.Enabled=false;ó=true;ï=e(ê);}public void Ď(double ď){ĭ++;ø=ď-Ă;ñ=ď+ò;ï=e(ê);á();if(!ħ&&ĥ)Ĝ()
;Đ();Ă=ď;}void Đ(){if(ï||(ð!=ă.ċ&&!ĥ)){c();return;}ā.Echo(""+ð);Ģ=æ.GetPosition();Ĥ=(æ.GetShipVelocities().LinearVelocity
-ģ)/ø;ģ=æ.GetShipVelocities().LinearVelocity;Ī=æ.CalculateShipMass().TotalMass;switch(ð){case ă.Ą:đ();break;case ă.ą:ē();
break;case ă.Ć:Ĕ();break;default:if(ĥ)ð=ă.Ą;break;}}void đ(){m(false);if(ç!=null){ç.Enabled=true;ç.Radius=1000;}if(è!=null){è
.Enabled=true;è.Radius=1000;}é.ApplyAction("ShootOnce");ð=ă.ą;Ĩ=Ă+ø+ĩ;foreach(IMyGyro Ē in ì){Ē.Enabled=true;Ē.
GyroOverride=true;Ē.Yaw=0;Ē.Pitch=0;Ē.Roll=0;}}void ē(){m(true);if(Ă+ø>Ĩ){if(ç!=null){ç.Enabled=true;ç.Radius=1000;}if(è!=null){è.
Enabled=true;è.Radius=1000;}ð=ă.Ć;}}void Ĕ(){Vector3D ĕ;Vector3D Ė=ğ-Ģ;if(Ė.LengthSquared()>4000*4000)ĕ=Ė;else ĕ=E(Ģ,ģ,Ĥ,ğ,Ġ,ġ,
Ī,ı);double Ę=0;double Ğ=0;double ę=ī*Math.PI/30;Ã(ĕ,æ.WorldMatrix.Forward,æ.WorldMatrix.Left,æ.WorldMatrix.Up,out Ę,out
Ğ);Ę=į.Ú(Ę,ø);Ğ=İ.Ú(Ğ,ø);o(Ğ,Ę,ę,ì,æ);m(true);Vector3D N=ğ-Ģ;Vector3D Ě=Ġ-ģ;double ě=Math.Sqrt(N.LengthSquared()/Ě.
LengthSquared());if(ě<Č&&Ê(N,Ě)<0.17){ĝ(true);}if(N.LengthSquared()<Ĭ*Ĭ){c();}if(N.LengthSquared()<Į)Į=N.LengthSquared();else if(Ě.
Dot(N)>0&&Į<1000)c();}void Ĝ(){foreach(IMyTerminalBlock g in ê){if(g is IMyReactor)g.ApplyAction("OnOff_On");}foreach(
IMyGyro Ē in ì){Ē.Enabled=true;}foreach(IMyThrust M in í){M.Enabled=true;}ħ=true;ĝ(false);m(false);ı=h(í);}void ĝ(bool n){
foreach(IMyWarhead č in ë){č.IsArmed=n;}}void á(){Vector3D w=Vector3D.Zero;Vector3D Y=Vector3D.Zero;Vector3D Z=Vector3D.Zero;Ħ=
ĥ;ĥ=æ.GetNearestPlayer(out w);if(ĥ&&Ħ){Y=(w-ğ)/ø;Z=(Y-Ġ)/ø;}ğ=w;Ġ=Y;ġ=Z;}void c(){ï=true;foreach(IMyWarhead d in ë){if(d
==null||!d.IsFunctional)continue;d.Detonate();}ç?.ApplyAction("OnOff_Off");è?.ApplyAction("OnOff_Off");}bool e(List<
IMyTerminalBlock>f){foreach(IMyTerminalBlock g in f){if(g==null||!g.IsFunctional){return true;}}return false;}double h(List<IMyThrust>j)
{double l=0;foreach(IMyThrust M in j){l+=M.MaxThrust;}return l;}void m(bool n){if(î==n)return;î=n;foreach(IMyThrust M in
í){M.Enabled=n;M.ThrustOverridePercentage=n?1:0;}}void o(double p,double q,double r,List<IMyGyro>s,IMyTerminalBlock t,int
u=1){if(ï)return;var W=new Vector3D(-p,q,r);var A=t.WorldMatrix;var U=Vector3D.TransformNormal(W,A);foreach(IMyGyro B in
s){var C=B.WorldMatrix;var D=Vector3D.TransformNormal(U,Matrix.Transpose(C));if(u>0){B.Pitch=(float)D.X;B.Yaw=(float)D.Y;
B.Roll=(float)D.Z;}else{B.Pitch=0f;B.Yaw=0f;B.Roll=0f;}B.GyroOverride=true;u++;}}Vector3D E(Vector3D F,Vector3D G,
Vector3D H,Vector3D I,Vector3D J,Vector3D K,double L,double M){Vector3D N=I-F;Vector3D O=new Vector3D(N);O.Normalize();Vector3D
P=new Vector3D(G);P.Normalize();Vector3D Q=J-G;Vector3D R=K-H;Vector3D S=N.Cross(Q)/(N.LengthSquared());Vector3D T=ô*(Q.
Cross(S));if(õ!=0)T+=õ*Á(K,O);double k=T.Length();double V=M/L;double v=k/V;if(v>2)v=2;Vector3D Ì=Vector3D.Zero+T/k*v+P*(1-v)
;return Ì;}}class Í{public double Î=0;public double Ï=0;public double Ð=0;double Ñ=0;double Ò=0;double Ó=0;public int Ô=0
;public double Õ{get;private set;}public Í(double à,double Ö,double Ø,double Ù){Î=à;Ï=Ö;Ð=Ø;Ñ=Ù;}public double Ú(double Û
,double Ü){double Ý=(Û-Ó)/Ü;if(Ô==0){Ý=0;}Ô++;if(Ñ!=0)Ò=Ò*(Ñ);Ò+=Û*Ü;Ó=Û;this.Õ=Î*Û+Ï*Ò+Ð*Ý;return this.Õ;}public void Þ(
){Ô=0;Ò=0;Ó=0;}}static Vector3D ß(Vector3D x,Vector3D y){Vector3D z=x.Dot(y)/y.LengthSquared()*y;return z;}static
Vector3D Á(Vector3D x,Vector3D y){Vector3D z=x.Dot(y)/y.LengthSquared()*y;return x-z;}static Vector3D ª(Vector3D x,Vector3D y,
double µ=1){Vector3D º=ß(x,y);Vector3D À=x-º;Vector3D Â=º-À*µ;return Â;}static double Ê(Vector3D x,Vector3D y){if(Vector3D.
IsZero(x)||Vector3D.IsZero(y))return 0;else return Math.Acos(MathHelper.Clamp(x.Dot(y)/Math.Sqrt(x.LengthSquared()*y.
LengthSquared()),-1,1));}static void Ã(Vector3D Ä,Vector3D Å,Vector3D Æ,Vector3D Ç,out double È,out double É){var X=ß(Ä,Ç);var Ë=Ä-X;
È=Ê(Å,Ë);É=Ê(Ä,Ë);È=-1*Math.Sign(Æ.Dot(Ä))*È;É=Math.Sign(Ç.Dot(Ä))*É;if(É==0&&È==0&&Ä.Dot(Å)<0){È=Math.PI;}}
