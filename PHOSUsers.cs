/*
 * Created by SharpDevelop.
 * jgustafson
 * Date: 3/13/2012
 * Time: 9:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Security.AccessControl;//DirectorySecurity etc
//using ExpertMultimedia;

namespace ProfileHarvester
{
    /// <summary>
    /// Description of PHOSUsers.
    /// </summary>
    public class PHOSUsers
    {
        public static string sDirSep=char.ToString(Path.DirectorySeparatorChar);
        public static string[] RemoteRoots_FullNameThenSlash=new String[] {@"T:\",@"G:\Home\"};//public static string StudentsRoot_FullNameThenSlash=@"T:\";
        public static string sComputerName_DONOTUSEDIRECTLY=null;
        public static Random rand=null;
        public static string sLocalProfilesRoot_FullName=@"C:\Documents and Settings";  // updated in static constructor //formerly sUserRoot
        public static DirectoryInfo diLocalProfilesRoot=new DirectoryInfo(@"C:\Documents and Settings");  // IF "C:\Users" EXISTS, changes to that in static constructor //formerly diBase
        public static readonly string[] sarrSystemCreatedFolders_ToLower=new string[]{@"c:\documents and settings",@"c:\documents and settings\all users"};
        //public static readonly string[] sarrSystemCreatedFolders_UnderUserProfile_ToLower=new string[]{@"documents",@"pictures",@"videos",@"my documents",@"my documents\my pictures",@"documents\my music",@"my documents\my music",@"documents\my pictures",@"my documents\my videos",@"documents\my videos"}; //"documents\my *" are under XP All Users folder
        public static readonly string[] sarrSystemCreatedFolders_UnderUserProfile_ToLower=new string[]{@"documents",@"pictures",@"videos",@"music"}; //"documents\my *" are under XP All Users folder
        public static readonly string[] sarrSystemCreatedFolders_AllUserProfile_ToLower=new string[]{@"c:\documents and settings\all users\documents",@"c:\documents and settings\all users\documents\my music",@"c:\documents and settings\all users\documents\my pictures",@"c:\documents and settings\all users\documents\my videos"}; //"documents\my *" are under XP All Users folder
        //public static readonly string[] sarrSystemCreatedFolders_AllUserProfileVista_ToLower=new string[]{@"c:\users\public\documents",@"c:\users\music",@"c:\users\pictures",@"c:\users\videos"}; //"documents\my *" are under XP All Users folder
        public const string sExnIfPathStartsWith="<couldn't finish:/>";
        
        static PHOSUsers() {
            try {
                rand=new Random(DateTime.Now.Year+DateTime.Now.Month+DateTime.Now.Day+DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Second+DateTime.Now.Millisecond);
                sComputerName_DONOTUSEDIRECTLY=MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead();
                if (Directory.Exists(@"C:\Users")) {
                    sLocalProfilesRoot_FullName=@"C:\Users";
                    sarrSystemCreatedFolders_ToLower=new string[] {@"c:\users",@"c:\users\public"};
                    sarrSystemCreatedFolders_AllUserProfile_ToLower=new string[]{@"c:\users\public\documents"};//doesn't have to be comprehensive, only has to cover folders where there can be user folders alongside system folders
                    
                }
                //if (Directory.Exists(@"E:\Documents and Settings")) sLocalProfilesRoot_FullName=@"E:\Documents and Settings";//debug only
                diLocalProfilesRoot=new DirectoryInfo(sLocalProfilesRoot_FullName);
            }
            catch (Exception exn) {
                ShowExn(exn,"initializing PHOSUsers","running static constructor");
            }
        }
        public PHOSUsers()
        {
        }
        
        public static string sParticiple="(before initializing)";
        
        public static void ShowErr(string sMsg) {
            Console.Error.WriteLine(sMsg);
            
        }
        public static void ShowExn(Exception exn) {
            Console.Error.WriteLine("Could not finish:"+exn.ToString());
        }
        public static void ShowExn(Exception exn, string sParticiple) {
            if (sParticiple==null) sParticiple="";
            Console.Error.WriteLine("Could not finish while "+sParticiple+":"+exn.ToString());
        }
        public static void ShowExn(Exception exn, string sParticiple, string sFuncName) {
            if (sParticiple==null) sParticiple="";
            if (sFuncName==null) sFuncName="";
            Console.Error.WriteLine("Could not finish while "+sParticiple+" in "+sFuncName+":"+exn.ToString());
        }
        
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioinfoNow"></param>
        /// <param name="Source_MachineFolderNow_ElseNull">Must end with the folder that is the machine folder</param>
        /// <param name="sMachineFolderPrefix">Can be blank, but otherwise must be something like "from-computer-" and Source_MachineFolderNow_ElseNull must start with it.</param>
        /// <returns></returns>
        public static string ComputerName_GetOrGen(PHOpInfo ioinfoNow, string Source_MachineFolderNow_ElseNull, string sMachineFolderPrefix) {
            string sReturn=MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead();
            try {
                if (ioinfoNow.MachineNameSource==PHOpInfo.MachineNameSource_NextLevelSubFolderHas_FromDashComputerDash_ComputerName_OptionalParentheticalNote) {
                    if (Source_MachineFolderNow_ElseNull!=null&&Source_MachineFolderNow_ElseNull!="") {
                        int iLastSlash=Source_MachineFolderNow_ElseNull.LastIndexOf(sDirSep);
                        sReturn=Source_MachineFolderNow_ElseNull;
                        if (iLastSlash>=0) {
                            sReturn=sReturn.Substring(iLastSlash+1);
                        }
                        int iFirstOpeningParen=Source_MachineFolderNow_ElseNull.IndexOf("(");
                        if (iFirstOpeningParen>=0) {
                            sReturn=sReturn.Substring(0,iFirstOpeningParen);
                        }
                    }
                    else ShowErr("No Source_MachineFolderNow_ElseNull param was given for ComputerName(PHOpInfo.MachineNameSource_NextLevelSubFolderHas_FromDashComputerDash_ComputerName_OptionalParentheticalNote,...)");
                }
            }
            catch (Exception exn) {
                ShowExn(exn);
            }
            return sReturn;
        }
        public static DirectoryInfo HomeFolderOfUser(string sOwner_UserName) {
            DirectoryInfo diOwnerHome=null;
            if (sOwner_UserName=="Jakeg7505") sOwner_UserName="jgustafson";//debug hard-coded fix for FCA
            //System.Security.Principal.SecurityIdentifier sid=fsNow.GetOwner(typeof(SecurityIdentifier));
            //string sOwnerNow = sid.Translate(typeof(NTAccount));
            //int iLastSlash=sOwnerNow.LastIndexOf(@"\");
            //if (iLastSlash>0) sOwnerNow=sOwnerNow.Substring(iLastSlash+1);
            string sOwner_HomeFolder_Name=sOwner_UserName;//if not found, any numbers at end will be truncated, then tries again
            sParticiple="checking for home folder of "+sOwner_HomeFolder_Name;
            //string sOwner_HomeFolder_Name_Orig=sOwner_HomeFolder_Name;
            for (int iRemote_UsersFolder_FullName=0; iRemote_UsersFolder_FullName<PHOSUsers.RemoteRoots_FullNameThenSlash.Length; iRemote_UsersFolder_FullName++) {
                if ( (diOwnerHome==null) || (!diOwnerHome.Exists) ) {
                    string sRemoteRootThenSlash=PHOSUsers.RemoteRoots_FullNameThenSlash[iRemote_UsersFolder_FullName];
                    sOwner_HomeFolder_Name=sOwner_UserName;
                    diOwnerHome=new DirectoryInfo(sRemoteRootThenSlash+sOwner_HomeFolder_Name);
                    if ( (diOwnerHome==null) || (!diOwnerHome.Exists) ) {
                        diOwnerHome=new DirectoryInfo(sRemoteRootThenSlash+sOwner_HomeFolder_Name);
                        sParticiple="looking for alternate home folder other than "+sOwner_HomeFolder_Name;
                        while (sOwner_HomeFolder_Name.Length>=1&&char.IsDigit(sOwner_HomeFolder_Name[sOwner_HomeFolder_Name.Length-1])) {
                            if (sOwner_HomeFolder_Name.Length>1) sOwner_HomeFolder_Name=sOwner_HomeFolder_Name.Substring(0,sOwner_HomeFolder_Name.Length-1);
                            else sOwner_HomeFolder_Name="";
                        }
                        sParticiple="checking alternate home folder "+sOwner_HomeFolder_Name;
                        diOwnerHome=new DirectoryInfo(sRemoteRootThenSlash+sOwner_HomeFolder_Name);
                    }
                    else break;
                }
                else break;
            }//end for each remote USERS root (as opposed to user's folder which is subfolder of users root folder)
            return diOwnerHome;
        }
        //
        public static string UserNameAndPathToUserName(string sUserNameAndPath) {
            int iLastSlash=sUserNameAndPath.LastIndexOf(@"\");
            if (iLastSlash>=0) {
                if (iLastSlash<sUserNameAndPath.Length-1) sUserNameAndPath=sUserNameAndPath.Substring(iLastSlash+1);
                else sUserNameAndPath="";
            }
            int iFirstAt=sUserNameAndPath.IndexOf("@"); //@ indicates that it is a user, but is not normally present unfortunately
            if (iFirstAt>=0) {
                if (iFirstAt>0) sUserNameAndPath=sUserNameAndPath.Substring(0,iFirstAt);
                else sUserNameAndPath="";
            }
            return sUserNameAndPath;//not fully qualified anymore
        }//end UserNameFQToUserName
        /// <summary>
        /// Breaks if group name is unknown
        /// </summary>
        /// <param name="fiNow"></param>
        /// <returns></returns>
        public static string SafeOwnerName(FileInfo fiNow) {
            //TODO: load group names from a list so that they can be ignored
            string sOwner="";
            try {
                FileSecurity fsNow=fiNow.GetAccessControl();
                //string user = System.IO.File.GetAccessControl(path).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                //fsNow.GetOwner();
                
                AuthorizationRuleCollection acl=fsNow.GetAccessRules(true,true,typeof(System.Security.Principal.NTAccount));
                bool bAlsoOwnedByAlternate0=false;
                string sAlternate0="Administrator";
                foreach (FileSystemAccessRule ace in acl) {
                    string sOwnerNow_Orig=ace.IdentityReference.Value;
                    //TODO: if (bMegaDebug) Console.WriteLine(fiNow.Name+" fsarule:"+sOwnerNow_Orig);
                    string sOwnerNow=UserNameAndPathToUserName(sOwnerNow_Orig);
                    if (sOwnerNow!=""
                        ///(iFirstAt>0)
                        &&sOwnerNow!="Everyone"
                        &&sOwnerNow!="SYSTEM"
                        &&sOwnerNow!="Administrators"
                        &&sOwnerNow!="Faculty"
                        &&sOwnerNow!="Staff"
                        &&sOwnerNow!="Students Group"
                        &&sOwnerNow!="Computer Teachers"
                        &&sOwnerNow!="Users"
                        &&sOwnerNow!="welcome"
                        &&sOwnerNow!="Domain Users"
                        &&sOwnerNow!="Domain Admins"
                        &&sOwnerNow!="Authenticated Users"
                        &&!sOwnerNow.ToLower().StartsWith("hslab")
                        &&!sOwnerNow.StartsWith("308")
                        &&sOwnerNow!="Input Devices"
                        &&sOwnerNow.ToUpper()!="INTERACTIVE"
                        &&!sOwnerNow.EndsWith(" Group")
                       ){
                        if (sOwnerNow==sAlternate0) bAlsoOwnedByAlternate0=true;
                        else {
                            sOwner=sOwnerNow;
                            break;
                        }
                    }//end if a valid user (not group)
                }//end foreach FileSystemAccessRule
                if (sOwner=="") {//if still blank after ALL owners checked, allow a ubiquitous owner to be returned
                    if (bAlsoOwnedByAlternate0) sOwner=sAlternate0;
                }
            }
            catch (Exception exn) {//don't care
                sOwner="";
            }
            return sOwner;
        }//end SafeOwnerName
        public static string MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead() {
            string sComputerName_NOTFOUND="(UnknownSourceSoUsedRandomName)";//fixed below
            string sComputerName=sComputerName_NOTFOUND;
            try {
                sComputerName_NOTFOUND=rand.Next().ToString()+"(UnknownSourceSoUsedRandomName)";
                sComputerName=SystemInformation.ComputerName+"(ComputerName)";
            }
            catch (Exception exn) {}//don't care
            try {
                if (sComputerName==null||sComputerName==""||sComputerName==sComputerName_NOTFOUND) sComputerName=System.Environment.MachineName+"(MachineName)";
            }
            catch (Exception exn) {} //don't care
            //if (sComputerName==null||sComputerName==""||sComputerName==sComputerName_NOTFOUND) {
            //  //iComputerNameUnknown++;
            //  Random rand=new Random();
            //  sComputerName=rand.Next().ToString()+"(UnknownSourceSoUsedRandomName)";
            //}
            return sComputerName;
        }//end MachineName_Safe_ElseRandom
        
        public static string UserCurrentlyLoggedIn() {
            string sReturn="";
            sReturn=Environment.UserName;//WindowsIdentity.GetCurrent()
            return sReturn;
        }
        /// <summary>
        /// formerly IsUnusableUserName -- only checks users, not groups
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="bAllowUsingAllUsers"></param>
        /// <returns></returns>
        public static bool  IsUnusableUserProfileFolderName(string sName, bool bAllowUsingAllUsers) {
            bool bReturn=false;
            string sName_ToLower=sName.ToLower();
            if (sName!=null&&sName!="") {
                if (sName_ToLower=="administrator") bReturn=true;
                else if (sName.ToUpper()=="ADMINI~1~FBM") bReturn=true;
                //else if (sName_ToLower=="ezmicro") bReturn=true;
                else if (sName_ToLower=="localservice") bReturn=true;
                else if (sName_ToLower=="networkservice") bReturn=true;
                else if (sName_ToLower=="all users") bReturn=true;
                else if (sName_ToLower=="public") bReturn=true;
                else if (sName_ToLower=="default user") bReturn=true;
                //else if (sName_ToLower=="guest") bReturn=true;
                //else if (sName_ToLower=="first") bReturn=true;
                //else if (sName_ToLower=="second") bReturn=true;
                //else if (sName_ToLower=="third") bReturn=true;
                //else if (sName_ToLower=="fourth") bReturn=true;
                //else if (sName_ToLower=="fifth") bReturn=true;
                //else if (sName_ToLower=="sixth") bReturn=true;
                else if (sName_ToLower.StartsWith("administrator.")) bReturn=true;
            }
            else bReturn=true;
            if (bAllowUsingAllUsers && ((sName_ToLower=="all users")||(sName_ToLower=="public"))) {//bForEachUser_LepidToCurrentlyLoggedInUserH) {
                bReturn=false;
            }
            return bReturn;
        }//end  IsUnusableUserProfileFolderName
        //private static bool bFirstRun_IsAUserCreatedDirectory=true;
        public static bool IsAUserCreatedDirectory(DirectoryInfo diNow, string sUser_ElseNull) {
            bool bReturn=true;
            string sUser_ElseNull_ToLower=sUser_ElseNull!=null?sUser_ElseNull.ToLower():"";
            try {
                if (diNow!=null) {
                    string diNow_Name_ToLower=diNow.Name.ToLower();
                    string diNow_FullName_ToLower=diNow.FullName.ToLower();
                    string[] sarrSystemCreatedFoldersUnderCurrentUser_ToLower=null;
                    if (sUser_ElseNull!=null) {
                        sarrSystemCreatedFoldersUnderCurrentUser_ToLower=new string[sarrSystemCreatedFolders_UnderUserProfile_ToLower.Length];
                        for (int iUser=0; iUser<sarrSystemCreatedFoldersUnderCurrentUser_ToLower.Length; iUser++) {
                            sarrSystemCreatedFoldersUnderCurrentUser_ToLower[iUser]=(sLocalProfilesRoot_FullName+sDirSep+sUser_ElseNull_ToLower+sDirSep+sarrSystemCreatedFolders_UnderUserProfile_ToLower[iUser]).ToLower();
                            //if (bFirstRun_IsAUserCreatedDirectory) {
                            //  Warning("sarrSystemCreatedFoldersUnderCurrentUser_ToLower["+iUser.ToString()+"]:"+sarrSystemCreatedFoldersUnderCurrentUser_ToLower[iUser]);
                            //}
                        }
                    }
                    if (sarrSystemCreatedFoldersUnderCurrentUser_ToLower!=null && (Array.IndexOf(sarrSystemCreatedFoldersUnderCurrentUser_ToLower,diNow_FullName_ToLower)>=0) ) bReturn=false;
                    else if (diNow_FullName_ToLower.Length==3&&diNow_FullName_ToLower.EndsWith(@":\")) bReturn=false;
                    else if (diNow_FullName_ToLower.Length==2&&diNow_FullName_ToLower.EndsWith(@":")) bReturn=false;
                    else if ((Array.IndexOf(sarrSystemCreatedFolders_ToLower,diNow_FullName_ToLower)>=0)) bReturn=false;
                    else if ((Array.IndexOf(sarrSystemCreatedFolders_AllUserProfile_ToLower,diNow_FullName_ToLower)>=0)) bReturn=false;
                }
                else bReturn=false;
            }
            catch (Exception exn) {
                ShowExn(exn,"processing path","IsAUserCreatedDirectory");
            }
            //bFirstRun_IsAUserCreatedDirectory=false;
            return bReturn;
        }
        public static bool IsAUserCreatedFile(FileInfo fiNow) {
            bool bReturn=true;
            if (fiNow!=null) {
                string fiNow_Name_ToLower=fiNow.Name.ToLower();
                if (fiNow_Name_ToLower=="thumbs.db") bReturn=false;
                else if (fiNow_Name_ToLower=="desktop.ini") bReturn=false;
                else if (fiNow_Name_ToLower=="sample pictures.lnk") bReturn=false;
                else if (fiNow_Name_ToLower=="sample music.lnk") bReturn=false;
                else if (fiNow_Name_ToLower.EndsWith(".lnk")) bReturn=false; //avoid moving/copying shortcut to home drive such as if in USERPROFILE or MYDOCS
            }
            else bReturn=false;
            return bReturn;
        }//end IsAUserCreatedFile

        /// <summary>
        /// Only if source exists & any files/folders in it then this will create dest.
        /// </summary>
        /// <param name="alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder">List of paths--if the value starts with PHOSUsers.sExnIfPathStartsWith then it is followed by a path, but there was an exception. Formerly alSkipped_EndWithSlashIfFolder.</param>
        /// <param name="sUser_ElseNull"></param>
        /// <param name="diSourceFolder"></param>
        /// <param name="diDestFolder"></param>
        /// <param name="bMove"></param>
        /// <param name="tbStatus_ElseNull"></param>
        /// <returns>how many files added to dest (updated/added), not counting already were synced</returns>
        public static int SyncFolder(ref ArrayList alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder, bool bRecursive, DirectoryInfo diSourceFolder, string DestFolder_FullName, bool bMove, string sUser_ElseNull, ref PHOpInfo ioiNow_ForCallbackOnly, bool bKeepAndDateOldFile, ListBox lbNow, out bool is_ok) {
            is_ok=false;
            int iFilesAddedOrUpdated_NotCountingAlreadySynced=0;
            //iReturnSystemFilesSkipped=0;
            //iReturnSystemFoldersSkipped=0;
            //bool bEverSkipped=false;
            int iFilesNew=0;//TODO: return this
            int iFilesUpdated=0;//TODO: return this
            int iFilesAlreadyUpToDate=0;//TODO: return this
            int iFilesTotalExpected=0;//TODO: return this: should be == iFilesNew+iFilesUpdated+iFilesAlreadyUpToDate+alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Count
            FileInfo[] fiarrNow=null;
            DirectoryInfo[] diarrSub=null;
            DirectoryInfo diDestFolder=null;
            bool bCreatedParentAnalogousToSource=false;
            
            try {
                diDestFolder=new DirectoryInfo(DestFolder_FullName);
                if (diSourceFolder!=null&&diSourceFolder.Exists) {
                    if (alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder==null) alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder=new ArrayList();
                    ioiNow_ForCallbackOnly.UpdateStatus("Processing "+diSourceFolder.FullName,true);
                    fiarrNow=diSourceFolder.GetFiles();
                    diarrSub=diSourceFolder.GetDirectories();
                }
                else {
                    string sFolderObjectToQuotedStringElseTheWordNullNotQuoted=((diSourceFolder!=null)?("\""+diSourceFolder.FullName+"\""):"null");
                    ioiNow_ForCallbackOnly.UpdateStatus("skipping folder:"+sFolderObjectToQuotedStringElseTheWordNullNotQuoted,true);
                    //Console.Error.WriteLine("Skipping non-existant folder:"+sFolderObjectToQuotedStringElseTheWordNullNotQuoted+" while checking source folder SyncFolder_Recursively");//ShowErr("Skipping non-existant folder:"+sFolderObjectToQuotedStringElseTheWordNullNotQuoted,"checking source folder","SyncFolder_Recursively");
                }
                if (fiarrNow!=null) is_ok=true;
                if (fiarrNow!=null&&fiarrNow.Length>0) {
                    iFilesTotalExpected=fiarrNow.Length;
                    foreach (FileInfo fiSrc in fiarrNow) {
                        string fiSrc_Name_ToLower=fiSrc.Name.ToLower();
                        bool bUserCreated=IsAUserCreatedFile(fiSrc);
                        //if (fiSrc.Name.ToLower()=="thumbs.db") {
                        //  iFilesTotalExpected--;
                        //  try {
                        //      fiSrc.Delete();
                        //  }
                        //  catch (Exception exn) {
                        //      if (bMove) 
                        //          alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Add(sExnIfPathStartsWith+fiSrc.FullName); //&&fiSrc.Name.ToLower()=="thumbs.db"
                        //      ShowExn(exn);
                        //  }
                        //}
                        //else 
                        bool bAllowed=ioiNow_ForCallbackOnly.IsAllowedFile(fiSrc_Name_ToLower);
//                      bool bAllowed=(ioiNow_ForCallbackOnly.mustEndWith_MustBeLowerCase!=null)?false:true;
//                      if (ioiNow_ForCallbackOnly.mustEndWith_MustBeLowerCase!=null) {
//                          foreach (string allowedEnding in ioiNow_ForCallbackOnly.mustEndWith_MustBeLowerCase) {
//                              string allowedEnding_ToLower=allowedEnding.ToLower();
//                              if (fiSrc_Name_ToLower.EndsWith(allowedEnding_ToLower)) {
//                                  bAllowed=true;
//                                  break;
//                              }
//                          }
//                      }
                        if (bUserCreated&&bAllowed) {
                            try {
                                if (!bCreatedParentAnalogousToSource&&!Directory.Exists(DestFolder_FullName)) {
                                    Directory.CreateDirectory(DestFolder_FullName);
                                    diDestFolder=new DirectoryInfo(DestFolder_FullName);
                                    bCreatedParentAnalogousToSource=true;
                                }
                                string DestFile_FullName=diDestFolder.FullName+sDirSep+fiSrc.Name;
                                FileInfo fiDest=new FileInfo(DestFile_FullName);
                                if (!fiDest.Exists||(fiDest.LastWriteTimeUtc<fiSrc.LastWriteTimeUtc)) {
                                    if (fiDest.Exists) {
                                        iFilesUpdated++;
                                        if (bKeepAndDateOldFile) {
                                            string DatedFile_FullName=Path.GetFileNameWithoutExtension(fiDest.FullName)+" "+fiDest.LastWriteTime.ToString("yyyy-MM-dd HH_mm_ss")+"."+Path.GetExtension(fiDest.FullName);
                                            if (File.Exists(DatedFile_FullName)) {
                                                if (lbNow!=null) {
                                                    lbNow.Items.Add("del \""+fiDest.FullName+"\"");
                                                    Application.DoEvents();
                                                }
                                                fiDest.Delete();
                                            }
                                            else {
                                                if (lbNow!=null) {
                                                    lbNow.Items.Add("rename \""+fiDest.FullName+"\" \""+DatedFile_FullName+"\"");
                                                    Application.DoEvents();
                                                }
                                                fiDest.MoveTo(DatedFile_FullName);
                                            }
                                        }
                                        else {
                                            if (lbNow!=null) {
                                                lbNow.Items.Add("del \""+fiDest.FullName+"\"");
                                                Application.DoEvents();
                                            }
                                            fiDest.Delete();
                                        }
                                    }
                                    else iFilesNew++;
                                    if (bMove) {
                                        if (lbNow!=null) {
                                            lbNow.Items.Add("move \""+fiSrc.FullName+"\" \""+DestFile_FullName+"\"");
                                            Application.DoEvents();
                                        }
                                        fiSrc.MoveTo(DestFile_FullName);
                                    }
                                    else {
                                        if (lbNow!=null) {
                                            lbNow.Items.Add("copy \""+fiSrc.FullName+"\" \""+DestFile_FullName+"\"");
                                            Application.DoEvents();
                                        }
                                        fiSrc.CopyTo(DestFile_FullName);
                                    }
                                    iFilesAddedOrUpdated_NotCountingAlreadySynced++;
                                }
                                else {
                                    iFilesAlreadyUpToDate++;
                                    if (bMove) {
                                        if (fiSrc.Exists) {
                                            if (lbNow!=null) {
                                                lbNow.Items.Add("REM Existing file on dest already up to date:");
                                                lbNow.Items.Add("del \""+fiDest.FullName+"\"");
                                                Application.DoEvents();
                                            }
                                            fiDest.Delete();
                                        }
                                    }
                                }
                            }
                            catch (Exception exn) {
                                //if (bMove) 
                                alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Add(sExnIfPathStartsWith+fiSrc.FullName); //&&fiSrc.Name.ToLower()=="thumbs.db"
                                ShowExn(exn);
                            }
                        }
                        else {
                            iFilesTotalExpected--;
                            try {
                                if (fiSrc.Name.ToLower()=="thumbs.db") {
                                    if (lbNow!=null) {
                                        lbNow.Items.Add("del \""+fiSrc.FullName+"\"");
                                        Application.DoEvents();
                                    }
                                    fiSrc.Delete();
                                }
                                else alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Add(fiSrc.FullName);
                            }
                            catch (Exception exn) { //don't care why
                                alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Add(sExnIfPathStartsWith+fiSrc.FullName);
                                
                            }
                        }
                    }//end foreach file
                }//end if >0 files
                if (diarrSub!=null&&diarrSub.Length>0&&bRecursive) {
                    //if (!Directory.Exists(DestFolder_FullName)) {
                    //  Directory.CreateDirectory(DestFolder_FullName);
                    //  diDestFolder=new DirectoryInfo(DestFolder_FullName);
                    //}
                    foreach (DirectoryInfo diSrcSubNow in diarrSub) {
                        try {
                            int iReturnSystemFilesSkipped_InSubdir=0;
                            string DestSubFolder_FullName=diDestFolder.FullName+sDirSep+diSrcSubNow.Name;
                            //if (!Directory.Exists(DestSubFolder_FullName)) Directory.CreateDirectory(DestSubFolder_FullName);
                            //DirectoryInfo diDestSub=new DirectoryInfo(DestSubFolder_FullName);
                            int iSkippedPrev=alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Count;
                            bool is_next_ok=false;
                            iFilesAddedOrUpdated_NotCountingAlreadySynced+=SyncFolder(ref alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder, bRecursive, diSrcSubNow, DestSubFolder_FullName, bMove, sUser_ElseNull, ref ioiNow_ForCallbackOnly, bKeepAndDateOldFile, lbNow, out is_next_ok);
                            int iReturnSystemFoldersSkipped_InSubdir=alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Count-iSkippedPrev;
                            if (bMove) {
                                if (IsAUserCreatedDirectory(diSrcSubNow,sUser_ElseNull)) {
                                    if ( (iReturnSystemFilesSkipped_InSubdir==0) && is_next_ok ){
                                        try {
                                            if (lbNow!=null) {
                                                lbNow.Items.Add("REM done synchronizing:");
                                                lbNow.Items.Add("rd /s /q \""+diSrcSubNow.FullName+"\"");
                                                Application.DoEvents();
                                            }
                                            diSrcSubNow.Delete(true);
                                        }
                                        catch (Exception exn) {
                                            if (lbNow!=null) {
                                                lbNow.Items.Add("REM FAILED deleting folder that had already been synchronized");
                                                //TODO: asdf // line above reads: C:\... "My Movies\My Music" [folder doesn't exist though]
                                                Application.DoEvents();
                                            }
                                            alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Add(sExnIfPathStartsWith+diSrcSubNow.FullName+sDirSep);
                                            ShowExn(exn,"deleting user-created subfolder \""+diSrcSubNow.FullName+"\" from source");
                                        }
                                    }
                                }
                                else alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Add(diSrcSubNow.FullName+sDirSep);
                            }
                        }
                        catch (Exception exn) {
                            alCouldntRemoveSystemCreatedSource_EndWithSlashIfFolder.Add(sExnIfPathStartsWith+diSrcSubNow.FullName+sDirSep);
                            ShowExn(exn);
                        }
                    }
                }//end if any subfolders
            }
            catch (Exception exn) {
                if (ioiNow_ForCallbackOnly!=null) ioiNow_ForCallbackOnly.UpdateStatus("Could not finish SyncFolder: "+exn.ToString(),false);
                ShowExn(exn,"before starting folder","SyncFolder_Recursively");
                is_ok = false;
            }
            return iFilesAddedOrUpdated_NotCountingAlreadySynced;  //debug: was ++ before fixed June 2 2016 (don't know why)
        }//end SyncFolder
        
        /*
        public static void SyncFolder_Recursively_ToMultipleUsers(PHOpInfo ioiInfo) {
            int iFilesFound=0;
            string User_Name="";
            string User_Name_UnknownOwner="<unknown_owner>";
            Var vUsers=new Var();
            bool bEverSkipped=false;
            string sSkipped="{unknown_user_skipped_files:";
            try {
                foreach (FileInfo fiSrc in diSourceFolder.GetFiles) {
                    User_Name=SafeOwnerName(fiSrc);
                    if (User_Name!="") {
                        
                    }
                    else {
                        User_Name=User_Name_UnknownOwner;
                        sSkipped+=((bEverSkipped)?(","):(""))+"\""+fiSrc.FullName+"\"";
                        bEverSkipped=true;
                    }
                    vUsers.Set(User_Name,(int)(vUsers.GetForcedInt(User_Name)+1));
                }//end foreach file
                sSkipped+="}";
                if (vUsers.GetForcedInt(User_Name)>0) {
                    ioiNow.alUsersWithFiles.Add(User_Name_UnknownOwner+"");
                }
            }
            catch (Exception exn) {
                
            }
            
        }
        */
        /// <summary>
        /// Move files to home folder matching profile folder:
        /// if (bForEachUser_LepidToCurrentlyLoggedInUserH) then Move Lepidopterist
        /// if (bForEachuser_DocsToSameUserOnT) then move Documents
        /// </summary>
        public static void ForEachUser_DoActions(ref PHOpInfo ioiNow, ListBox lbNow) {
            //int iMoved=0;
            StreamWriter swRetryBatch=null;
            string sSourceUsersRootNow="";
            int iTotalPotentialSourceFolders=0;
            int iUsersFoundInSourceProfilesFolder=0;
            int iUsersFoundInSourceProfilesFolder_AbleToFinish=0;
            try {
                swRetryBatch=new StreamWriter(PHOpInfo.RetryBatchFile_FullName);//ioiNow.RetryBatchFile_FullName);
                if (ioiNow.alUsersWithFiles!=null) ioiNow.alUsersWithFiles=new ArrayList();
                DirectoryInfo[] diarrUserProfileNow=null;
                sSourceUsersRootNow=diLocalProfilesRoot.FullName;//changed below when not using local profile as source
                if (ioiNow.SourceFolder_CanContainPercentUSERPROFILEPercent.StartsWith("%USERPROFILE%")) diarrUserProfileNow=diLocalProfilesRoot.GetDirectories();
                if (ioiNow.SourceFolder_CanContainPercentUSERPROFILEPercent.Contains("%USERNAME%")) {
                    int iStartPercentUSERNAMEPercent=ioiNow.SourceFolder_CanContainPercentUSERPROFILEPercent.IndexOf("%USERNAME%");
                    sSourceUsersRootNow=ioiNow.SourceFolder_CanContainPercentUSERPROFILEPercent.Substring(0,iStartPercentUSERNAMEPercent);
                    DirectoryInfo diSourceUsersRootNow=new DirectoryInfo(sSourceUsersRootNow);
                    if (diSourceUsersRootNow!=null&&diSourceUsersRootNow.Exists) diarrUserProfileNow=diSourceUsersRootNow.GetDirectories();
                }
                if (diarrUserProfileNow!=null) {
                    iTotalPotentialSourceFolders=diarrUserProfileNow.Length;
                    int iStartUserNameInDest=ioiNow.DestFolder_CanContainPercentUSERNAMEPercent.IndexOf("%USERNAME%");
                    string DestProfilesRoot_FullNameNow="";
                    if (iStartUserNameInDest>=2) {//must actually be >2 to make any sense whatsoever, since it is an absolute path
                        DestProfilesRoot_FullNameNow=ioiNow.DestFolder_CanContainPercentUSERNAMEPercent.Substring(0,iStartUserNameInDest);
                    }
                    if (DestProfilesRoot_FullNameNow!=null&&DestProfilesRoot_FullNameNow!="") {//ioiNow.DestFolder_CanContainPercentUSERNAMEPercent.Contains("%USERNAME%")) {
                        foreach (DirectoryInfo diUserLocal in diarrUserProfileNow) {
                            bool bUserHadExn=false;
                            bool bUserHadAnyRealFiles=false;
                            string SourceFolder_UnderCurrentUser_FullName=ioiNow.SourceFolder_CanContainPercentUSERPROFILEPercent.Replace("%USERPROFILE%",sLocalProfilesRoot_FullName+sDirSep+diUserLocal.Name);//NOTE: use sLocalProfilesRoot_FullName+sDirSep+diUserLocal.Name not Environment.SpecialFolder.UserProfile [.NET 4.0+] (nor System.Environment.GetEnvironmentVariable("USERPROFILE")), which IS CURRENTLY-LOGGED-IN USER!!!!
                            DirectoryInfo diSource_UnderCurrentUser=new DirectoryInfo(SourceFolder_UnderCurrentUser_FullName);
                            if (! IsUnusableUserProfileFolderName(diUserLocal.Name,true)) {
                                iUsersFoundInSourceProfilesFolder++;
                                try {
                                    ioiNow.UpdateStatus("Processing local user profile \""+diUserLocal.Name+"\"",true);
                                    //bool bDestRootOK=true;
                                    string DestProfile_CurrentUser_FullName=DestProfilesRoot_FullNameNow+sDirSep+diUserLocal.Name;
                                    string DestFolder_UnderCurrentUser_FullName=ioiNow.DestFolder_CanContainPercentUSERNAMEPercent.Replace("%USERNAME%",diUserLocal.Name);
                                    if (ioiNow.bCreateDestEvenIfNoMatchingUser) {
                                        if (!Directory.Exists(DestProfile_CurrentUser_FullName)) {
                                            Directory.CreateDirectory(DestProfile_CurrentUser_FullName);
                                        }
                                    }
                                    DirectoryInfo diDest_CurrentUser_Profile=new DirectoryInfo(DestProfile_CurrentUser_FullName);//(StudentsRoot_FullNameThenSlash+diUserLocal.Name);
                                    if (diDest_CurrentUser_Profile.Exists) {
                                        //DOES create dest IF any files or folders exist in source:
                                        ArrayList alFilesSkipped=new ArrayList();
                                        bool is_profile_folder_ok=false;
                                        int iFilesFound=SyncFolder(ref alFilesSkipped, ioiNow.bRecursive, diSource_UnderCurrentUser, DestFolder_UnderCurrentUser_FullName,  ioiNow.bMove, diUserLocal.Name, ref ioiNow, ioiNow.bKeepAndDateOldFile, lbNow, out is_profile_folder_ok);
                                        int iFilesCouldNotFinishForCurrentUser=0;
                                        foreach (string sSkippedFile in alFilesSkipped) {
                                            if (sSkippedFile.StartsWith(PHOSUsers.sExnIfPathStartsWith)) {
                                                iFilesCouldNotFinishForCurrentUser++;
                                            }
                                        }
                                        if (iFilesFound>0) ioiNow.alUsersWithFiles.Add(diUserLocal.Name+"{"
                                                                             +"files:"+iFilesFound.ToString()+";"
                                                                             +"failed:"+iFilesCouldNotFinishForCurrentUser.ToString()+";"
                                                                             +"}"
                                                                            );
                                        if (!is_profile_folder_ok) {
                                            if (lbNow!=null) lbNow.Items.Add("REM Could not access folder \""+diSource_UnderCurrentUser.FullName+"\"");
                                        }
                                        //if (bForEachuser_DocsToSameUserOnT) {
                                        //  DirectoryInfo diMyDocsLocal=new DirectoryInfo(diUserLocal.FullName+sDirSep+"My Documents");
                                        //  sParticiple="accessing \""+diMyDocsLocal.FullName+"\"";
                                        //  FileInfo[] fi2dNow=diMyDocsLocal.GetFiles();
                                        //  sParticiple="making folder name string \""+diMyDocsLocal.FullName+"\"";
                                        //  string MyDocsRemote_FullName=diUserRemote.FullName+sDirSep+"Recovered Files";
                                        //  
                                        //  //else no files to move so do nothing for this user!
                                        //  sParticiple="finished checking for file array in \""+diUserLocal.Name+"\"";
                                        //}//end if bForEachuser_DocsToSameUserOnT
                                    }//end if remote user exists
                                    else {
                                        //if (bForEachuser_DocsToSameUserOnT) {
                                            try {
                                                swRetryBatch.WriteLine("rem Warning:there is no destination user folder \""+diUserLocal.Name+"\"");
                                            }
                                            catch {}//don't care
                                        //}
                                    }
                                }
                                catch (Exception exn) {
                                    bUserHadExn=true;
                                    ShowExn(exn,"accessing \""+diUserLocal.FullName+"\"");
                                }
                                iUsersFoundInSourceProfilesFolder_AbleToFinish++;
                            }//end if is a real username
                            if (bUserHadExn) ioiNow.alUsersWithFiles.Add(diUserLocal.Name+"{error:could not finish}");
                            if (bUserHadAnyRealFiles) ioiNow.alUsersWithFiles.Add(diUserLocal.Name);
                        }//end foreach diUserLocal
                    }//end if dest contains %USERNAME%"
                    else {
                        MessageBox.Show("NOT YET IMPLEMENTED (for now destination path needs to contain \"%USERNAME%\"--excluding quote marks and case-sensitive, & can't be start with it--it has to be a proper path, existant or not, after replacing it with a username)");
                    }
                }//If source contains "%USERPROFILE%"
                else {
                    MessageBox.Show("NOT YET IMPLEMENTED (for now source path needs to start with \"%USERPROFILE%\" or contain \"%USERNAME%\"--excluding quote marks and case-sensitive)");
                }
            }
            catch (Exception exn) {
                ShowExn(exn);
            }

            sParticiple="after finished processing local user folders";
            string sResult="Finished ForEachUser_DoActions {"
                +"Local Profiles:"+sSourceUsersRootNow+";"
                +"iTotalPotentialSourceFolders:"+iTotalPotentialSourceFolders.ToString()+";"
                +"iUsersFoundInSourceProfilesFolder:"+iUsersFoundInSourceProfilesFolder.ToString()+";"
                +"iUsersFoundInSourceProfilesFolder_AbleToFinish:"+iUsersFoundInSourceProfilesFolder_AbleToFinish.ToString()+";"
                +"Had files:"+string.Join(",", (string[])ioiNow.alUsersWithFiles.ToArray(Type.GetType("System.String")))+";" //+RString.ToString(ioiNow.alUsersWithFiles,",","\"")+";"
                
                //+"BackupDocumentsToSameUserOnT:" + (bForEachuser_DocsToSameUserOnT?"yes":"no")+ ";"
                //+"CollectMortimerTheLepidopteristToCurrentlyLoggedInUserH:" + (bForEachUser_LepidToCurrentlyLoggedInUserH?"yes":"no")+ ";"
                //+"UsersWhoHadCreatedFilesInRootOfMyDocuments:" + iUsersHadFiles + ";"
                //+"UsersWhoseFilesCouldNotFinishCopying:" + iUsersHadExceptions + ";"
                //+"TotalFilesCopyable:" +iTotalFilesCopyable + ";"
                //+"TotalFilesCouldNotFinishCopying:" +iTotalFilesHadExceptions + ";"
                +"}";
            ioiNow.UpdateStatus(sResult,true);
            try {
                swRetryBatch.Close();
                StreamWriter swResult=null;
                try {
                    swResult=new StreamWriter(@"C:\tmp\"+MainForm.sMyName+"-"+sComputerName_DONOTUSEDIRECTLY+".log");
                }
                catch {
                    
                    swResult=new StreamWriter(Path.GetTempPath()+sDirSep+MainForm.sMyName+"-"+sComputerName_DONOTUSEDIRECTLY+".log");
                }
                swResult.WriteLine(DateTime.Now.ToShortDateString());
                swResult.WriteLine(sResult);
                swResult.Close();
            }
            catch (Exception exn) {
                Console.Error.WriteLine("Couldn't finish while finalizing log in ForEachUser_DoActions:"+exn.ToString());
            }
        }//end ForEachUser_DoActions()
        /*
        /// <summary>
        /// Includes a new path to the System(Local/Remote) Environment variable 'Path'
        /// Source Cited: mohanayyavu. "Set System Env Variable Using .Net ManagementScope." CodeProject®. N.p., n.d. Web. 3 Jan. 2012. <http://www.codeproject.com/Tips/121910/Set-System-Env-Variable-Using-Net-ManagementScope.aspx>.
        /// </summary>
        /// <param name="isRemote"></param>
        /// <param name="MachineName"></param>
        /// <param name="_UserName"></param>
        /// <param name="_Password"></param>
        /// <param name="fileInfo"></param>
        private void SetEnvPath(bool isRemote, string MachineName, string _UserName, string _Password, string Directory_FullName) {
            
            ManagementObjectSearcher query = null;
            ManagementObjectCollection queryCollection = null;
            ConnectionOptions _COption = new ConnectionOptions();
            //If the Target Computer is not local, Supply credentials and enable user privilages if required.
            if (isRemote) {
            _COption.Username = _UserName;
            _COption.Password = _Password;
            _COption.EnablePrivileges = true;
            }
            
            try {
                //Create Management scope for the target machine
                ManagementScope _Scope = new ManagementScope("\\\\" + MachineName + "\\root\\cimv2", _COption);
                //Create a management Class with ManagementPath
                //Win32_Environment, To get all Environment variables of Target computer.< Win32_Environment:Path > will get only 'Path' variable.
                //ManagementClass _Class = new ManagementClass(_Scope, new ManagementPath("Win32_Environment:Path"), new ObjectGetOptions());
                _Scope.Options.Timeout = TimeSpan.FromSeconds(5);
                _Scope.Connect();
                //Query to be run against target computer.
                //This will select only 'Path' varibles from all Environment variables.
                ObjectQuery setQuery = new ObjectQuery("SELECT * FROM Win32_Environment WHERE name='Path'");
                query = new ManagementObjectSearcher(_Scope, setQuery);
                queryCollection = query.Get();
                string test = "";
                if (queryCollection.Count > 0) {
                    foreach (ManagementObject EnvVar in queryCollection) {
                        test = EnvVar.GetText(TextFormat.Mof);
                        if (test.Contains("Path") && !EnvVar["VariableValue"].ToString().Contains(fileInfo.DirectoryName)) {
                            //EnvVar["VariableValue"] - Value of the Environment variable 'Path'
                            if (EnvVar["VariableValue"].ToString().EndsWith(";"))
                            EnvVar["VariableValue"] = (object)(((string)EnvVar["VariableValue"]) + fileInfo.DirectoryName.ToString() + ";");
                            else
                            EnvVar["VariableValue"] = (object)(((string)EnvVar["VariableValue"]) + ";" + fileInfo.DirectoryName.ToString() + ";");
                            //Updates the 'Path' of the target machine.
                            EnvVar.Put();
                        }
                    }
                }
            }
            catch (Exception exn)
            {
            }
        }//end SetEnvPath
         */
    }
}
