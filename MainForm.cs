/*
 * Created by SharpDevelop.
 * User: Jakeg7505
 * Date: 10/27/2011
 * Time: 7:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.ComponentModel;
using System.Globalization;
using System.Management.Instrumentation;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using System.Text;//Encoding etc
using Microsoft.Win32;//RegKey etc
using System.Reflection; //Assembly etc
using System.Text.RegularExpressions;

namespace ProfileHarvester {
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form {
        private static string sParticiple = null;
        private string sResults="";
        private int iUsers=0;
        public static Random randGlobal=new Random();//global so Next() and Next(min,max) work even if occurring at same millisecond
        public static string sMyName="ProfileHarvester";
        public static readonly string SecretFile_Name=sMyName+"-dat";
        public static string StudentsACLPrefix="";//@"FBM\";
        public static string StudentsACLSuffix="@fbm.local";
        public static string kivy_BAD_PREPENDENV_PATH=@"C:\Kivy141;C:\Kivy141\Python;C:\Kivy141\gstreamer\bin;C:\Kivy141\MinGW\bin;";
        /// <summary>
        /// May need to be prepended to Environment PATH variable in order for PyDev to use Kivy properly
        /// </summary>
        //public static string kivy_PREPENDENV_PATH=@"C:\utils\kivy;C:\utils\kivy\Python;C:\utils\kivy\gstreamer\bin;C:\utils\kivy\MinGW\bin;";
        /// <summary>
        /// May need to be set as Environment GST_REGISTRY variable in order for PyDev to use Kivy properly
        /// </summary>
        public static string kivy_SETENV_GST_REGISTRY=@"C:\kivy\gstreamer\registry.bin";
        /// <summary>
        /// May need to be set as Environment PLUGIN_PATH variable in order for PyDev to use Kivy properly
        /// </summary>
        public static string kivy_SETENV_GST_PLUGIN_PATH=@"C:\kivy\gstreamer\lib\gstreamer-0.10";
        public static MainForm mainformNow=null;
        public static int iTotalFilesCopyable=0;
        public static int iTotalFilesHadExceptions=0;
        public static int iUsersHadFiles=0;
        public static int iUsersHadExceptions=0;
        //public static bool bForEachuser_DocsToSameUserOnT=true;
        //public static bool bForEachUser_LepidToCurrentlyLoggedInUserH=true;
        //public static string sLepidSrc_RelFolder=@"%USERPROFILE%\Desktop\lepidopterist-post2";
        //public static string sLepidRemoteRoot_FullName=@"H:\lepidopterist-post2";
        public static string[] sarrFileToCopy_Name=new string[]{"contestdata.txt","savegame.pkl"};//public static string[] sarrLepidFile_Name_NoFileExt=new string[]{"contestdata","savegame"};//public static string[] sarrLepidFile_Name_NoNameOnlyADotAndAFileExt=new string[]{".txt",".pkl"};
        public static string sDirSep=char.ToString(Path.DirectorySeparatorChar);
        //public static string sStatus {
        //  get {
        //      return (mainformNow!=null&&mainformNow.tbStatus!=null&&mainformNow.tbStatus.Text!=null)?mainformNow.tbStatus.Text:"";
        //  }
        //  set {
        //      if (mainformNow!=null) {
        //          mainformNow.tbStatus.Text=value;
        //      }
        //  }
        //}
        #region fileops
        public int iFilesDone=0;
        public int iFilesTotal=0;
        public int iExns=0;
        public int iDeleted=0;
        //public string sSkipped="";
        //public int iSkippedFiles=0;
        ArrayList alDone=new ArrayList();
        ArrayList alSkippedUsers=new ArrayList();
        ArrayList alProcessedUsers=new ArrayList();
        ArrayList alSkippedFiles=new ArrayList();
        ArrayList alProcessedFiles=new ArrayList();
        #endregion fileops
        void ShowExn(Exception exn) {
            lbMain.Items.Add(Regex.Replace(exn.ToString(), @"\t|\n|\r", ""));//(exn.ToString().Replace("\n"," ").Replace("\r",""));
        }
        void ResetStats() {
            iFilesDone=0;
            iFilesTotal=0;
            iExns=0;
            iDeleted=0;
            sResults="";
            iUsers=0;
            
            if (alSkippedUsers==null) alSkippedUsers=new ArrayList();
            else alSkippedUsers.Clear();
            if (alProcessedUsers==null) alProcessedUsers=new ArrayList();
            else alProcessedUsers.Clear();
            if (alSkippedFiles==null) alSkippedFiles=new ArrayList();
            else alSkippedFiles.Clear();
            if (alProcessedFiles==null) alProcessedFiles=new ArrayList();
            else alProcessedFiles.Clear();
        }
        
        //public static string ToString(ArrayList alNow) {
        //  string sReturn="";
        //  foreach (string sNow in alNow) {
        //      if (sNow!=null) sReturn+=((sReturn=="")?(""):("; "))+sNow;
        //  }
        //  return sReturn;
        //}
        
        public MainForm() {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            mainformNow=this;
        }
        public static string ToString(ArrayList this_al, string field_delimiter, string text_delimiter) {
            string result = "";
            if (this_al!=null) {
                string this_text_delimiter = "";
                if (text_delimiter!=null) this_text_delimiter = text_delimiter;
                foreach (string this_val in this_al) {
                    result += ((result!=""&&field_delimiter!=null)?field_delimiter:"") + text_delimiter+this_val+text_delimiter;
                }
            }
            return result;
        }
        /// <summary>
        /// was string sFolderToMove_ElseNull, bool bUseFileTypeFilters, TextBox tbStatus_ElseNull
        /// </summary>
        /// <param name="sFolderToMove_ElseNull"></param>
        /// <param name="bUseFileTypeFilters"></param>
        /// <param name="tbStatus_ElseNull"></param>
        void ForEachUser_DoActions(bool bResetStatistics, int iOpCode, bool bRecursive, string sSourceFolder_CanContainPercentUSERPROFILEPercent, string sDestFolder_CanContainPercentUSERNAMEPercent, bool bMove, bool bKeepAndDateOldFiles, bool CreateProfileOnDestIfDoesNotExist, ArrayList mustEndWith_MustBeLowerCase) {
            this.menuStrip1.Enabled=false;
            this.lblBusy.Visible=true;
            if (bResetStatistics) {
                ResetStats();
            }
            PHOpInfo ioiNow=new PHOpInfo(iOpCode,bRecursive,sSourceFolder_CanContainPercentUSERPROFILEPercent,sDestFolder_CanContainPercentUSERNAMEPercent,bMove,this.tbStatus,bKeepAndDateOldFiles,CreateProfileOnDestIfDoesNotExist,mustEndWith_MustBeLowerCase);
            ioiNow.lbStatus=this.lbMain;
            PHOSUsers.ForEachUser_DoActions(ref ioiNow, lbMain);
            if (ioiNow.alUsersWithFiles!=null) {
                foreach (string sUserResult in ioiNow.alUsersWithFiles) {
                    sResults+=((sResults=="")?(""):(" "))+sUserResult;
                    iUsers++;
                }
            }
            this.menuStrip1.Enabled=true;
            this.lblBusy.Text="Done: "+iUsers.ToString()+" user(s) had new/updated files ("+sResults+").";//this.lblBusy.Visible=false;
        }
        
        /// <summary>
        /// uses globals in region fileops. Not completely implemented--only moves files in c:\tmp so far
        /// </summary>
        /// <param name="sRemoteRootThenSlash"></param>
        public void ForEachFile_DoActions_ONLYMOVESFILESINTMPSOFAR(bool bResetStatistics) {
            if (bResetStatistics) {
                ResetStats();
            }
            //string sDelBat="cleantmp.bat";
            //StreamWriter srDel=new StreamWriter(sDelBat);
            //srDel.WriteLine(@"del /y C:\tmp\*.*");
            //srDel.Close();
            //ProcessStartInfo psiNow=new ProcessStartInfo(sDelBat);
            //Process procNow=Process.Start(psiNow);
            //procNow.WaitForExit();
            //File.Delete(sDelBat);
            //string SourceFileNow_FullName="";
            string DestFileNow_FullName="";
            string DestFolderNow_FullName="";
            string sLastCommand="";
            this.lblBusy.Text="Moving...";
            this.lblBusy.Visible=true;
            Application.DoEvents();
            DirectoryInfo diNow=null;
            string[] sarrFilters=new string[]{"blend","blend1","blend2","avi","png","mov","gz","dvd","mpg","mpeg"};
            FileInfo[][] fi2dNow=null;
            fi2dNow=new FileInfo[sarrFilters.Length][];
            try {
                diNow=new DirectoryInfo(@"C:\tmp");
                int iFilter=0;
                iFilesTotal=0;//this is ok since we are going by the file not by the outer loop that could contain ForEachFile_DoActions_ONLYMOVESFILESINTMPSOFAR
                for (iFilter=0; iFilter<fi2dNow.Length; iFilter++) {
                    fi2dNow[iFilter]=diNow.GetFiles("*."+sarrFilters[iFilter]);
                    iFilesTotal+=(fi2dNow[iFilter]!=null)?fi2dNow[iFilter].Length:0;
                }
                for (iFilter=0; iFilter<fi2dNow.Length; iFilter++) {
                    if (fi2dNow[iFilter]!=null) {
                        foreach (FileInfo fiNow in fi2dNow[iFilter]) {
                            try {
                                sParticiple="setting date & owner variables";
                                string sDateSuffix=fiNow.LastWriteTime.ToString("yyyy-MM-dd HH.MM");
                                string NameNew_Name=fiNow.Name;//+sDateSuffix+"."+sarrFilters[iFilter];
                                string NameNew_Name_Original_ToLower=NameNew_Name.ToLower();
                                if (NameNew_Name=="My Project.prel") NameNew_Name="My Project "+sDateSuffix+".prel";
                                //else if (NameNew_Name_Original_ToLower=="quit.blend") NameNew_Name="quit "+sDateSuffix+".blend"; //keep original name so Recover Last Session works
                                //else if (NameNew_Name_Original_ToLower=="game.blend") NameNew_Name="game "+sDateSuffix+".blend"; //keep original name so game simulation cache works
                                else if (NameNew_Name_Original_ToLower=="untitled.blend") NameNew_Name="untitled "+sDateSuffix+".blend";
                                else if (NameNew_Name_Original_ToLower=="untitled."+sarrFilters[iFilter]) NameNew_Name="Untitled "+sDateSuffix+"."+sarrFilters[iFilter];
                                else if ( NameNew_Name.Length==13 && NameNew_Name.StartsWith("0001-") && NameNew_Name_Original_ToLower.EndsWith(".avi") ) NameNew_Name=NameNew_Name.Substring(0,9)+" "+sDateSuffix+".avi";
                                else if ( NameNew_Name.Length==13 && NameNew_Name.StartsWith("0000-") && NameNew_Name_Original_ToLower.EndsWith(".avi") ) NameNew_Name=NameNew_Name.Substring(0,9)+" "+sDateSuffix+".avi";
                                else if ( NameNew_Name.Length==13 && NameNew_Name.StartsWith("0001-") && NameNew_Name_Original_ToLower.EndsWith(".dvd") ) NameNew_Name=NameNew_Name.Substring(0,9)+" "+sDateSuffix+".dvd";
                                else if ( NameNew_Name.Length==13 && NameNew_Name.StartsWith("0000-") && NameNew_Name_Original_ToLower.EndsWith(".dvd") ) NameNew_Name=NameNew_Name.Substring(0,9)+" "+sDateSuffix+".dvd";
                                string sOwner_UserName=PHOSUsers.SafeOwnerName(fiNow);
                                
                                
                                
                                
                                DirectoryInfo diOwnerHome=PHOSUsers.HomeFolderOfUser(sOwner_UserName);
                                
                                //NOTE: if diOwnerHome.Exists is still false, that is handled later
                                
                                sParticiple="setting destination tmp folder";
                                if (diOwnerHome!=null) DestFolderNow_FullName=diOwnerHome.FullName+sDirSep+"tmp";//sRemoteRootThenSlash+sOwner_HomeFolder_Name+sDirSep+"tmp";
                                sParticiple="setting SecretFileNow_FullName for name "+NameNew_Name;
                                DestFileNow_FullName=DestFolderNow_FullName+sDirSep+NameNew_Name;
                                if (diOwnerHome!=null&&diOwnerHome.Exists) {//TODO:?? non-working condition was here before: fiNow!=""&&
                                    if (!Directory.Exists(DestFolderNow_FullName)) {
                                        sParticiple="creating directory "+DestFolderNow_FullName;
                                        Directory.CreateDirectory(DestFolderNow_FullName);//,diOwnerHome.GetAccessControl());
                                    }
                                    
                                    int iTries=0;
                                    int iTriesMax=10;
                                    bool bSuccess=false;
                                    Exception exnPrev=null;
                                    try {
                                        if ( (NameNew_Name.ToLower()=="quit.blend") && File.Exists(DestFileNow_FullName) ) {
                                            string RenameDestTo_FullName=DestFileNow_FullName.Substring(0,DestFileNow_FullName.Length-6)+sDateSuffix+".blend";
                                            //lbMain.Items.Add("REM previous quit.blend renamed to \""+Path.GetFileName(RenameDestTo_FullName)+"\"");
                                            sLastCommand="rename \""+DestFileNow_FullName+"\" \""+RenameDestTo_FullName+"\"";
                                            lbMain.Items.Add(sLastCommand);
                                            Console.WriteLine(sLastCommand);
                                            File.Move(DestFileNow_FullName,RenameDestTo_FullName);
                                        }
                                    }
                                    catch {}//don't care
                                    while (iTries<iTriesMax) {
                                        try {
                                            //if ( fiNow.Name.Length==8 && fiNow.Name.StartsWith("0") && fiNow.Name.ToLower().EndsWith(".png") ) {
                                            //  sParticiple="deleting temporary png file from source";
                                            //  fiNow.Delete();
                                            //  iDeleted++;
                                            //}
                                            //else 
                                                if (!File.Exists(DestFileNow_FullName)) {
                                                sParticiple="moving new file to destination";
                                                fiNow.MoveTo(DestFileNow_FullName);
                                                sLastCommand="move /y \""+fiNow.FullName+"\" \""+DestFileNow_FullName+"\"";
                                                lbMain.Items.Add(sLastCommand);
                                                Console.WriteLine(sLastCommand);
                                            }
                                            else if ( (new FileInfo(DestFileNow_FullName)).LastWriteTimeUtc < fiNow.LastWriteTimeUtc) {
                                                sParticiple="renaming destination file to accommodate new copy";
                                                //sLastCommand="del \""+DestFileNow_FullName+"\"";
                                                //File.Delete(DestFileNow_FullName);
                                                string DestFileNow_NewName="";
                                                if (fiNow.Name.Length==8 //8 chars e.g. 0000.png (Blender image sequence from tmp)
                                                    && char.IsDigit(fiNow.Name[0])
                                                    && char.IsDigit(fiNow.Name[1])
                                                    && char.IsDigit(fiNow.Name[2])
                                                    && char.IsDigit(fiNow.Name[3])
                                                    && (fiNow.Name[4]=='.')
                                                   ) {
                                                    DestFileNow_NewName=sDateSuffix+" "+fiNow.Name;
                                                }
                                                else {
                                                    DestFileNow_NewName=Path.GetFileNameWithoutExtension(DestFileNow_FullName)+" ["+sDateSuffix+"]"+Path.GetExtension(DestFileNow_FullName);//DOT is included with GetExtension
                                                }
                                                string DestFileNow_NewFullName=Path.GetDirectoryName(DestFileNow_FullName)+char.ToString(Path.DirectorySeparatorChar)+DestFileNow_NewName;
                                                sLastCommand="rename \""+DestFileNow_FullName+"\" \""+DestFileNow_NewFullName+"\"";
                                                lbMain.Items.Add(sLastCommand);
                                                File.Move(DestFileNow_FullName,DestFileNow_NewFullName);
                                                sParticiple="moving new copy of existing file after renaming old one on destination";
                                                sLastCommand="move \""+fiNow.FullName+"\" \""+DestFileNow_FullName+"\"";
                                                lbMain.Items.Add(sLastCommand);
                                                fiNow.MoveTo(DestFileNow_FullName);
                                            }
                                            else {
                                                sParticiple="deleting old or same version of file from source";
                                                sLastCommand="del \""+fiNow.FullName+"\"";
                                                lbMain.Items.Add(sLastCommand);
                                                fiNow.Delete();
                                                iDeleted++;
                                            }
                                            bSuccess=true;
                                            break;
                                        }
                                        catch (Exception exn) {
                                            lblBusy.Text="File was busy for \""+diOwnerHome.Name+"\", retrying...";
                                            Application.DoEvents();
                                            exnPrev=exn;//tbStatus.Text=exn.ToString();
                                            Thread.Sleep(randGlobal.Next(10,1000));
                                        }
                                    }//end while iTries<iTriesMax copying file
                                    if (bSuccess) {
                                        lblBusy.Text="Moving..."+iFilesDone.ToString()+"/"+iFilesTotal.ToString();
                                        sParticiple="setting status after processing file";
                                        Application.DoEvents();
                                        iFilesDone++;
                                        if (!alProcessedUsers.Contains(sOwner_UserName)) alProcessedUsers.Add(sOwner_UserName);
                                        alSkippedUsers.Remove(sOwner_UserName);
                                        if (!alProcessedFiles.Contains(fiNow.FullName)) alProcessedFiles.Add(fiNow.FullName);
                                        alSkippedFiles.Remove(fiNow.FullName);
                                    }
                                    else {
                                        ShowExn(exnPrev);
                                        iExns++;
                                    }
                                }
                                else {
                                    sParticiple="skipping user that wasn't found on any home root";
                                    string sMsg="REM USER \""+sOwner_UserName+"\" NOT FOUND in any remote user folders";
                                    lbMain.Items.Add(sMsg);
                                    string sOwner_HomeFolder_Name="";
                                    if (diOwnerHome!=null) sOwner_HomeFolder_Name=diOwnerHome.FullName;
                                    if (sOwner_HomeFolder_Name!=sOwner_UserName) sMsg+=" (also checked for folder \""+sOwner_HomeFolder_Name+"\")";
                                    sMsg+=": move \""+fiNow.FullName+"\" \""+DestFileNow_FullName+"\"";
                                    Console.WriteLine(sMsg);
                                    if (sOwner_UserName==null) sOwner_UserName="null";
                                    else sOwner_UserName="\""+sOwner_UserName.Replace("\"","\\\"")+"\"";
                                    if (!alSkippedUsers.Contains(sOwner_UserName) && !alProcessedUsers.Contains(sOwner_UserName)) alSkippedUsers.Add(sOwner_UserName);
                                    if (!alSkippedFiles.Contains(fiNow.FullName) && !alProcessedFiles.Contains(fiNow.FullName)) alSkippedFiles.Add(fiNow.FullName);
                                }
                            }
                            catch (Exception exn) {
                                Console.Error.WriteLine("Could not finish "+sParticiple+" (\""+fiNow.FullName+"\"):"+exn.ToString());
                                iExns++;
                            }
                        }//foreach file using filter
                    }//if filter iFilter != null
                }//for filter iFilter
                lblBusy.Text="Done ("+iFilesDone.ToString()+"/"+iFilesTotal.ToString()+") {skipped-users:"+ToString(alSkippedUsers,",","")+"; skipped-files:"+alSkippedFiles.Count.ToString()+"; iDeleted:"+iDeleted.ToString()+"; iExns:"+iExns.ToString()+"}";
            }
            catch (Exception exn) {
                lblBusy.Text="FAILED (at:"+iFilesDone.ToString()+"/"+iFilesTotal.ToString()+") {skipped-users:"+ToString(alSkippedUsers,",","")+"; skipped-files:"+alSkippedFiles.Count.ToString()+"; iDeleted:"+iDeleted.ToString()+"}; Couldn't finish: "+exn.ToString();
            }
        }//end ForEachFile_DoActions_ONLYMOVESFILESINTMPSOFAR
        
        void CopyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItemClick(object sender, EventArgs e)
        {
            LocalDocumentsToNetworkDriveIfExists(false);
        }
        
        void MoveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItemClick(object sender, EventArgs e) {
            LocalDocumentsToNetworkDriveIfExists(true);
        }
        void LocalDocumentsToNetworkDriveIfExists(bool bMove) {
            //NOTE: COMPUTERNAME can usually be detected by parsing <drive>:\WINNT\Debug\NetSetup.LOG or <drive>:\Windows\Debug\NetSetup.LOG
            bool bResetStatistics=true;
            bool bRecursive=true;
            if (bMove) this.lblBusy.Text = "Moving local documents...";
            else this.lblBusy.Text = "Copying local documents...";
            try {
	            //bForEachuser_DocsToSameUserOnT=true;
	            //bForEachUser_LepidToCurrentlyLoggedInUserH=false;
	            string userProfileSubFolderName = @"%USERPROFILE%";
	            ArrayList ifInRootMustEndWithAnyOfStrings = new ArrayList();
	            int mask_count = 0;
	            bool ok_enable = true;
	            if (localProfilesMasksTextBox.Text.Length>0) {
		            localProfilesMasksTextBox.Text = localProfilesMasksTextBox.Text.Trim();
		            string[] masks = localProfilesMasksTextBox.Text.Split(new char[] {','});
		            foreach (string mask_original in masks) {
		            	string mask = mask_original.Trim();
		            	if (mask.StartsWith("*")) mask = mask.Substring(1);
		            	if (mask.Length>0) {
		            		if (!mask.Contains("*")) {
		            			ifInRootMustEndWithAnyOfStrings.Add(mask);
		            		}
		            		else {
		            			MessageBox.Show("Cannot continue since can only use masks with one star at beginning for now (such as *.prel)--cannot use '"+mask_original+"'");
		            			ok_enable = false;
		            		}
		            	}
		            	else {
		            		MessageBox.Show("Cannot continue since can't copy all (plain * not allowed in masks) due to system files in profile root--cannot use '"+mask_original+"'");
		            		ok_enable = false;
		            	}
		            }
		            mask_count = ifInRootMustEndWithAnyOfStrings.Count;
	            }
	            if (ok_enable) {
	            	DialogResult dr = DialogResult.Yes;
		            if (mask_count < 1) {
		            	dr = MessageBox.Show("NOTE: the mask_count is " + mask_count.ToString() + ", so no files will be copied from USERPROFILE root (only subfolders). Continue anyway?", "ProfileHarvester", MessageBoxButtons.YesNo);
		            }
		            if (doNotDetectOtherRemoteRootsCheckBox.Checked) {
		                if (!remoteProfilesTextBox.Text.EndsWith(sDirSep))
		                    remoteProfilesTextBox.Text+=sDirSep;
		                ForEachUser_DoActions(bResetStatistics,PHOpInfo.OpCode_Sync, bRecursive, this.localDocsTextBox.Text, Path.Combine(remoteProfilesTextBox.Text,remoteDocsTextBox.Text), bMove, true, createIfNotOnDestCheckBox.Checked, null); //PHOSUsers.StudentsRoot_FullNameThenSlash+@"%USERNAME%\Recovered Files\from-computer-"+PHOSUsers.MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead(),true);
		                bResetStatistics=false;
		                
		                ForEachUser_DoActions(bResetStatistics,PHOpInfo.OpCode_Sync, false, userProfileSubFolderName, Path.Combine(remoteProfilesTextBox.Text,remoteDocsTextBox.Text), bMove, true, createIfNotOnDestCheckBox.Checked, ifInRootMustEndWithAnyOfStrings); //PHOSUsers.StudentsRoot_FullNameThenSlash+@"%USERNAME%\Recovered Files\from-computer-"+PHOSUsers.MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead(),true);
		            }
		            else {
		                foreach (string sRemoteRoot_Original in PHOSUsers.RemoteRoots_FullNameThenSlash) {
		                    //TODO: %MYDOCUMENTS%\Adobe\Premiere Elements\10.0
		                    string sRemoteRoot=sRemoteRoot_Original;
		                    if (!sRemoteRoot.EndsWith(sDirSep))
		                        sRemoteRoot+=sDirSep;
		                    ForEachUser_DoActions(bResetStatistics,PHOpInfo.OpCode_Sync, bRecursive, this.localDocsTextBox.Text, Path.Combine(sRemoteRoot,remoteDocsTextBox.Text), bMove, true, createIfNotOnDestCheckBox.Checked, null); //PHOSUsers.StudentsRoot_FullNameThenSlash+@"%USERNAME%\Recovered Files\from-computer-"+PHOSUsers.MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead(),true);
		                    bResetStatistics=false;
		                    ForEachUser_DoActions(bResetStatistics,PHOpInfo.OpCode_Sync, false, userProfileSubFolderName, Path.Combine(sRemoteRoot,remoteDocsTextBox.Text), bMove, true, createIfNotOnDestCheckBox.Checked, ifInRootMustEndWithAnyOfStrings); //PHOSUsers.StudentsRoot_FullNameThenSlash+@"%USERNAME%\Recovered Files\from-computer-"+PHOSUsers.MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead(),true);
		                }
		            }
	            }
            }
            catch (Exception exn) {
	            this.lblBusy.Text = "Failed";
            	lbMain.Items.Add(REM_THIS(exn.ToString()));
            }
        }//end LocalDocumentsToNetworkDriveIfExists
        
        string REM_THIS(string newline_separated_values_s) {
        	string[] sarr = newline_separated_values_s.Split(new char[] {'\n'});
        	string result = "";
        	foreach (string s in sarr) {
        		result += "REM " + s;
        	}
        	return result;
        }
        
        void LocalTempToNetworkDriveIfExists(bool bMove) {
            bool bRecursive=false;
            
            ArrayList alTheoreticalTemp = new ArrayList();
            //Unlike LocalDocsToNetworkDriveIfExists,
            // uses TEMP folders under profile instead of localDocsTextBox.Text
            alTheoreticalTemp.Add(@"%USERPROFILE%\Local Settings\Temp");
            alTheoreticalTemp.Add(@"%USERPROFILE%\AppData\Local\Temp");
            
            //Unlike LocalDocsToNetworkDriveIfExists,
            // uses remoteTempTextBox.Text instead of remoteDocsTextBox.Text

            //NOTE: COMPUTERNAME can usually be detected by parsing <drive>:\WINNT\Debug\NetSetup.LOG or <drive>:\Windows\Debug\NetSetup.LOG
            bool bResetStatistics=true;
            foreach (string sTheoreticalTemp in alTheoreticalTemp) {
                if (doNotDetectOtherRemoteRootsCheckBox.Checked) {
                    if (!remoteProfilesTextBox.Text.EndsWith(sDirSep))
                        remoteProfilesTextBox.Text+=sDirSep;
                    ForEachUser_DoActions(bResetStatistics,PHOpInfo.OpCode_Sync, bRecursive, sTheoreticalTemp, Path.Combine(remoteProfilesTextBox.Text,remoteTempTextBox.Text), bMove, true, createIfNotOnDestCheckBox.Checked,null); //PHOSUsers.StudentsRoot_FullNameThenSlash+@"%USERNAME%\Recovered Files\from-computer-"+PHOSUsers.MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead(),true);
                    bResetStatistics=false;
                }
                else {
                    foreach (string sRemoteRoot_Original in PHOSUsers.RemoteRoots_FullNameThenSlash) {
                        string sRemoteRoot=sRemoteRoot_Original;
                        if (!sRemoteRoot.EndsWith(sDirSep))
                            sRemoteRoot+=sDirSep;
                        ForEachUser_DoActions(bResetStatistics,PHOpInfo.OpCode_Sync, bRecursive, sTheoreticalTemp, Path.Combine(sRemoteRoot,remoteTempTextBox.Text), bMove, true, createIfNotOnDestCheckBox.Checked,null); //PHOSUsers.StudentsRoot_FullNameThenSlash+@"%USERNAME%\Recovered Files\from-computer-"+PHOSUsers.MachineName_Safe_ElseRandom_DONOTUSEDIRECTLY_Use_ComputerName_GetOrGen_MethodInstead(),true);
                        bResetStatistics=false;
                    }
                }
            }//end for each theoretical temp folder location
        }//end LocalTempToNetworkDriveIfExists
        
        void CollectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItemClick(object sender, EventArgs e)
        {
            //bForEachuser_DocsToSameUserOnT=false;
            //bForEachUser_LepidToCurrentlyLoggedInUserH=true;
            //ForEachUser_DoActions();
            MessageBox.Show("NOT YET IMPLEMENTED");
        }
        /// <summary>
        /// AddAndRemovePaths from PATH environment variable, using registry
        /// </summary>
        /// <param name="alAdd"></param>
        /// <param name="alRemove"></param>
        /// <param name="statusLabel"></param>
        /// <param name="statusListBox"></param>
        /// <param name="doTestButDontWrite"></param>
        public static void AddAndRemovePaths(ArrayList alAdd, ArrayList alRemove, Label statusLabel, ListBox statusListBox, bool IsToAddKivyEnvironmentVariables, bool doTestButDontWrite) {
            bool bGood=false;//debug: result is not checked
            statusLabel.Text="AddAndRemovePaths...";
            string sLastSetCommand="";
            char[] carrTrim=new char[] {' ','\n','\r','\t',';'};
            char[] exploder=new Char[] {';'};
            if (statusLabel==null) statusLabel=new Label();//just create a dummy to avoid null reference exceptions
            statusLabel.Text="Working...";
            statusLabel.Visible=true;
            ArrayList addList=new ArrayList();
            ArrayList removeList=new ArrayList();
            if (alAdd!=null) {
                foreach (string s in alAdd) {
                    string trimmedOuter=s.Trim(carrTrim);
                    string[] exploded=trimmedOuter.Split(exploder);
                    if (exploded!=null) {
                        foreach (string innerString in exploded) {
                            string trimmedInner=innerString.Trim();
                            if (!string.IsNullOrEmpty(trimmedInner)) {
                                addList.Add(trimmedInner);
                            }
                        }
                    }
                }
            }
            if (alRemove!=null) {
                foreach (string s in alRemove) {
                    string trimmedOuter=s.Trim(carrTrim);
                    string[] exploded=trimmedOuter.Split(exploder);
                    if (exploded!=null) {
                        foreach (string innerString in exploded) {
                            string trimmedInner=innerString.Trim();
                            if (!string.IsNullOrEmpty(trimmedInner)) {
                                removeList.Add(trimmedInner);
                            }
                        }
                    }
                }
            }
            alRemove=removeList;
            alAdd=addList;
            if ((addList!=null&&addList.Count>0)||(removeList!=null&&removeList.Count>0)) {
                //We'll be working with: \\HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment\    STRING VALUE: Path
                RegistryKey hiveRegKey = Registry.LocalMachine;//Registry.LocalMachine.OpenSubKey(@"MainKey", true);
                string envSubKeyString=@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
                RegistryKey envRegKey = hiveRegKey.OpenSubKey(envSubKeyString);
                ASCIIEncoding enc = new ASCIIEncoding();
                string pathOriginalString=null;
                string pathVariableRegStringName="Path";
                if (envRegKey!=null) {
                    if (IsToAddKivyEnvironmentVariables) {
                        try {
                            envRegKey.SetValue("GST_REGISTRY",kivy_SETENV_GST_REGISTRY);
                            envRegKey.SetValue("GST_PLUGIN_PATH",kivy_SETENV_GST_PLUGIN_PATH);
                        }
                        catch (Exception exn2) {
                            statusListBox.Items.Add("Could not finish writing gstreamer plugin paths"+exn2.ToString().Replace("\n","").Replace("\r",""));
                        }
                    }
                    try {
                        object o=envRegKey.GetValue(pathVariableRegStringName);
                        byte[] value1 = null;
                    
                        if (o!=null) {
                            //value1=(byte[])o;
                            //pathOriginalString=enc.GetString(value1).ToString();
                            pathOriginalString=(string)o;
                        }
                        else {
                            
                        }
                        //pathOriginalString=envRegKey.GetValue(pathVariableRegStringName);
                        if (!string.IsNullOrEmpty(pathOriginalString)) {
                            statusLabel.Text="opened registry key...";
                            statusLabel.Text=pathOriginalString;
                            statusListBox.Items.Add("REM path was:"+pathOriginalString);
                            string pathNewString=pathOriginalString;
                            pathNewString=pathNewString.Trim(carrTrim);
                            string pathNewString_ToLower=pathNewString.ToLower();
                            if (removeList!=null) {
                                foreach (string removeString_Original in removeList) {
                                    string removeString=removeString_Original;
                                    string removeString_ToLower=removeString.ToLower();
                                    while (pathNewString_ToLower.EndsWith(removeString_ToLower)) {
                                        pathNewString_ToLower=pathNewString_ToLower.Substring(0,pathNewString_ToLower.Length-removeString_ToLower.Length);
                                        pathNewString=pathNewString.Substring(0,pathNewString.Length-removeString.Length);
                                    }
                                    removeString_ToLower+=";";
                                    removeString+=";";
                                    while (pathNewString_ToLower.EndsWith(removeString_ToLower)) {
                                        pathNewString_ToLower=pathNewString_ToLower.Substring(0,pathNewString_ToLower.Length-removeString_ToLower.Length);
                                        pathNewString=pathNewString.Substring(0,pathNewString.Length-removeString.Length);
                                    }
                                    int iStart=pathNewString_ToLower.LastIndexOf(removeString_ToLower);
                                    while (iStart>-1) {
                                        pathNewString_ToLower=pathNewString_ToLower.Remove(iStart,removeString_ToLower.Length);
                                        pathNewString=pathNewString.Remove(iStart,removeString_ToLower.Length);
                                        iStart=pathNewString_ToLower.LastIndexOf(removeString_ToLower);
                                    }
                                }
                            }
                            if (addList!=null) {
                                for (int index=addList.Count-1; index>=0; index--) {
                                    string addString=((string)addList[index]).Trim(carrTrim);
                                    if (!string.IsNullOrEmpty(addString)) pathNewString=addString+";"+pathNewString;
                                }
                            }
                            if (!doTestButDontWrite) {
                                envRegKey.SetValue(pathVariableRegStringName,pathNewString);
                            }
                            statusListBox.Items.Add("set PATH="+pathNewString);
                        }
                        else {
                            statusLabel.Text="INACCESSIBLE ";
                            statusListBox.Items.Add("REM FAILED opening string value \""+pathVariableRegStringName+"\" in:"+envSubKeyString);
                        }
                    }
                    catch (Exception exn) {
                        statusLabel.Text="COULDN'T FINISH";
                        statusListBox.Items.Add("REM FAILED working on string value \""+pathVariableRegStringName+"\" in "+envSubKeyString+":"+exn.ToString().Replace("\r","").Replace("\n",""));
                        //statusListBox.Items.Add("REM "+exn.ToString().Replace("\n\r","").Replace("\r\n","").Replace("\r","").Replace("\n",""));
                    }
                    envRegKey.Close();
                }
                else {
                    statusLabel.Text="FAILED opening RegKey";
                    statusListBox.Items.Add("REM FAILED opening RegKey:"+envSubKeyString);
                }
                if (hiveRegKey!=null) {
                    hiveRegKey.Close();
                    hiveRegKey=null;
                }
                /*
                //if (sPathToAdd!="") {
                //MessageBox.Show("NOT YET IMPLEMENTED");//SetEnvPath(false,sComputerName,"","",
                string GetPath_Output_FullName="ProfileHarvester-GetPath-lastrun.txt";
                if (File.Exists(GetPath_Output_FullName)) File.Delete(GetPath_Output_FullName);
                string BatGetPath_FullName="ProfileHarvester-GetPath-lastrun.bat";
                if (File.Exists(BatGetPath_FullName)) File.Delete(BatGetPath_FullName);
                StreamWriter BatGetPath_Stream=new StreamWriter(BatGetPath_FullName);
                //BatGetPath_Stream.WriteLine(Directory.GetCurrentDirectory().Substring(0,2));//type c: or whatever
                //BatGetPath_Stream.WriteLine("cd "+Directory.GetCurrentDirectory());//type c: or whatever
                BatGetPath_Stream.WriteLine("echo %PATH% > \""+GetPath_Output_FullName+"\""); //unusable since doesn't create file named by GetPath_Output_FullName on Winodws 7
                BatGetPath_Stream.Close();
                ProcessStartInfo psiBatchThatMakesText=new ProcessStartInfo();
                psiBatchThatMakesText.FileName=BatGetPath_FullName;
                Process pBatchThatMakesText=Process.Start(psiBatchThatMakesText);
                //pBatchThatMakesText.WaitForInputIdle();//only works (& only needed) for GUI
                pBatchThatMakesText.WaitForExit();
                //Process.Start(BatGetPath_FullName);
                
                string sExistingPaths_SemicolonSeparated="";
                StreamReader BatGetPath_InStream=new StreamReader(GetPath_Output_FullName);//unusable since doesn't get created on Windows 7 (by command started above)
                sExistingPaths_SemicolonSeparated=BatGetPath_InStream.ReadLine();
                if (sExistingPaths_SemicolonSeparated!=null) {
                    sExistingPaths_SemicolonSeparated=sExistingPaths_SemicolonSeparated.Trim(carrTrim);
                    if (sExistingPaths_SemicolonSeparated!="") {
                        if (!sExistingPaths_SemicolonSeparated.Contains(sPathToAdd+";") && !sExistingPaths_SemicolonSeparated.EndsWith(sPathToAdd)) {
                            string BatchAddPath_FullName="ProfileHarvester-AddPath-lastrun.bat";
                            if (File.Exists(BatchAddPath_FullName)) File.Delete(BatchAddPath_FullName);
                            StreamWriter BatSet_Stream=new StreamWriter(BatchAddPath_FullName);
                            string SetOutput_FullName="ProfileHarvester-AddPath-lastrun-RESULT.txt";
                            if (File.Exists(SetOutput_FullName)) File.Delete(SetOutput_FullName);
                            if (!sExistingPaths_SemicolonSeparated.EndsWith(";")) {
                                sPathToAdd=";"+sPathToAdd;
                            }
                            sLastSetCommand="SETX PATH \"%PATH%"+sPathToAdd+"\" -m 2> \""+SetOutput_FullName+"\""; //-m is for machine instead of user!
                            //2> is needed to get error!!
                            BatSet_Stream.WriteLine(sLastSetCommand);
                            BatSet_Stream.Close();
                            ProcessStartInfo psiBatchSetPathAndSendErrToResultText = new ProcessStartInfo();
                            psiBatchSetPathAndSendErrToResultText.FileName=BatchAddPath_FullName;
                            psiBatchSetPathAndSendErrToResultText.UseShellExecute = false; //must be false to runas
                            psiBatchSetPathAndSendErrToResultText.Verb="runas";
                            
                            
                            
                            if (this.tbInput0.Text!="") {
                                string plainText=this.tbInput0.Text;
                                System.Security.SecureString ssPwd=new System.Security.SecureString();
                                for (int iChar=0; iChar<plainText.Length; iChar++) {
                                    ssPwd.AppendChar(plainText[iChar]);
                                }
                                psiBatchSetPathAndSendErrToResultText.Password=ssPwd;
                            }
                            try {
                                Process pBatchSetPathAndSendErrToResultText = Process.Start(psiBatchSetPathAndSendErrToResultText);
                                try {
                                    pBatchSetPathAndSendErrToResultText.WaitForInputIdle();//only works (& only needed) for GUI
                                }
                                catch {}//don't care

                                pBatchSetPathAndSendErrToResultText.WaitForExit();
                                //Process.Start(BatchAddPath_FullName);
                                //Thread.Sleep(10);//in case the user pressed the dialog button late
                                
                                StreamReader BatSetOutput_StreamIn=new StreamReader(SetOutput_FullName);
                                string SetResult_Text=BatSetOutput_StreamIn.ReadToEnd();
                                BatSetOutput_StreamIn.Close();
                                if (SetResult_Text!=null&&SetResult_Text!="") {
                                    string sMsg=SetResult_Text;
                                    if (sMsg.ToLower().Contains("denied")) {
                                        sMsg+="\n\n Try putting your Windows password in the Input box then try again.";
                                    }
                                    statusLabel.Text=(sMsg);
                                }
                                else {
                                    bGood=true;
                                    statusLabel.Text="SUCCESS";
                                }
                            }
                            catch (Exception exn) {
                                string sMsg="Could not finish adding SecretFileNow_FullName. \n\n"+exn.ToString();
                                if (!sMsg.ToLower().Contains("canceled by the user")) {
                                    statusLabel.Text=(sMsg);
                                }
                                else {
                                    sMsg="Cannot add SecretFileNow_FullName since you did not provide administrator credentials.";
                                    statusLabel.Text=(sMsg);
                                }
                            }
                        }
                        else {
                            statusLabel.Text="SUCCESS (was already done)";
                        }
                    }
                    else {
                        statusLabel.Text=("Output from \""+BatGetPath_FullName+"\" was blank");
                    }
                }
                else statusLabel.Text=("Couldn't write output to \""+GetPath_Output_FullName+"\"");
                //}
                //else statusLabel.Text=("Text in textbox was blank--you must something in Location box before using this");
                */
            }
            else statusLabel.Text=("No paths were added to the Location box");
        }
        void TsmiPathClick(object sender, EventArgs e)
        {
            string sPathToAdd=this.tbLocation.Text;
            ArrayList alRemove=new ArrayList();
            alRemove.Add(kivy_BAD_PREPENDENV_PATH);
            ArrayList alAdd=new ArrayList();
            alAdd.Add(sPathToAdd);
            bool doTestButDontWrite=readOnlyCheckBox.Checked;
            bool IsToAddKivyEnvironmentVariables=false;
            AddAndRemovePaths(alAdd,alRemove, this.lblBusy, this.lbMain, IsToAddKivyEnvironmentVariables, doTestButDontWrite);
        }//end TsmiPathClick
        
        void ClearTmpToolStripMenuItemClick(object sender, EventArgs e) {
            //string sDelBat="cleantmp.bat";
            //StreamWriter srDel=new StreamWriter(sDelBat);
            //srDel.WriteLine(@"del /y C:\tmp\*.*");
            //srDel.Close();
            //ProcessStartInfo psiNow=new ProcessStartInfo(sDelBat);
            //Process procNow=Process.Start(psiNow);
            //procNow.WaitForExit();
            //File.Delete(sDelBat);
            this.lblBusy.Text="Clearing tmp...";
            this.lblBusy.Visible=true;
            Application.DoEvents();
            //int iFilesDone=0;
            //int iFilesTotal=0;
            DirectoryInfo diNow=null;
            string[] sarrFilters=new string[]{"gz","png"};//,"avi"};
            FileInfo[][] fi2dNow=null;
            fi2dNow=new FileInfo[sarrFilters.Length][];
            int iExns=0;
            try {
                diNow=new DirectoryInfo(@"C:\tmp");
                int iFilter=0;
                for (iFilter=0; iFilter<fi2dNow.Length; iFilter++) {
                    fi2dNow[iFilter]=diNow.GetFiles("*."+sarrFilters[iFilter]);
                    iFilesTotal+=(fi2dNow[iFilter]!=null)?fi2dNow[iFilter].Length:0;
                }
                for (iFilter=0; iFilter<fi2dNow.Length; iFilter++) {
                    if (fi2dNow[iFilter]!=null) {
                        foreach (FileInfo fiNow in fi2dNow[iFilter]) {
                            try {
                                fiNow.Delete();
                                lblBusy.Text="Working..."+iFilesDone.ToString()+"/"+iFilesTotal.ToString();
                                Application.DoEvents();
                                iFilesDone++;
                            }
                            catch (Exception exn) {
                                Console.Error.WriteLine("Could not finish deleting \""+fiNow.FullName+"\":"+exn.ToString());
                                iExns++;
                            }
                        }
                    }
                }
                lblBusy.Text="Done ("+iFilesDone.ToString()+"/"+iFilesTotal.ToString()+")";
            }
            catch (Exception exn) {
                lblBusy.Text="FAILED (at:"+iFilesDone.ToString()+"/"+iFilesTotal.ToString()+"; Couldn't finish: "+exn.ToString();
            }
        }//end ClearTmpToolStripMenuItemClick
        
        
        void MoveTmpFilesToTheOwnersTmpOnTToolStripMenuItemClick(object sender, EventArgs e) {
            bool bResetStats=true;
            try {
	            foreach (string sRemoteRootThenSlash in PHOSUsers.RemoteRoots_FullNameThenSlash) {
	            	this.lblBusy.Text = "Moving files in tmp by owner...";
	                //there are multiple remote roots since there is: \\Fbmfs1\main\home and \\Fbmfs1\student
	                ForEachFile_DoActions_ONLYMOVESFILESINTMPSOFAR(bResetStats);
	                bResetStats = false;
	            }
            	this.lblBusy.Text = "Done";
            }
            catch (Exception exn) {
            	this.lblBusy.Text = "Failed";
            	lbMain.Items.Add(REM_THIS(exn.ToString()));
            }
        }//end MoveTmpFilesToTheOwnersTmpOnTToolStripMenuItemClick
        
        void SilverToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.tbLocation.Text=@"E:\Program Files\Java\jre7\bin";
        }
        
        void FCAToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.tbLocation.Text=@";C:\Python27;C:\Python27\scripts;C:\Program Files\Java\jre7\bin";
        }
        /// <summary>
        /// Saves in user profile & only allowed to be decrypted as that user
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="File_Name"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static bool LoadTextSecurely(out string sReturn, string SecretFileNow_FullName, string sKey) {
            sParticiple="setting sReturn to null";
            sReturn=null;
            sParticiple="after setting sReturn to null";
            bool bGood=false;
            try {
                
                sParticiple="getting FileInfo";
                FileInfo fiNow = new FileInfo(SecretFileNow_FullName);
                int iGot = 0;
                int iGet = 0;
                
                sParticiple="getting Length";
                iGet=(int)fiNow.Length;
                sParticiple="creating array";
                byte[] encryptedData = new byte[iGet];
                sParticiple="checking for file";
                if (File.Exists(SecretFileNow_FullName)) {
                    sParticiple="opening file";
                    using (FileStream fs = File.OpenRead(SecretFileNow_FullName)) {//closes automatically
                    //FileStream fs = File.OpenRead(SecretFileNow_FullName);
                        sParticiple="reading file";
                        iGot = fs.Read(encryptedData, 0, iGet);
                        sParticiple="finished reading file";
                        sParticiple="closing file";
                        //fs.Close();
                        //fs=null;
                    }
                    if (iGot==iGet) {
                        sParticiple="encoding key";
                        //byte[] encryptedData = File.ReadAllBytes(SecretFileNow_FullName);
                        byte[] entropyBytes = Encoding.Unicode.GetBytes(sKey);
                        sParticiple="protecting password";
                        byte[] decryptedData = ProtectedData.Unprotect(encryptedData, entropyBytes, DataProtectionScope.CurrentUser);
                        sParticiple="decoding data";
                        sReturn=Encoding.Unicode.GetString(decryptedData);
                        if (sReturn!=null&&sReturn.Length>0) bGood=true;
                        sParticiple="showing status";
                        mainformNow.tbStatus.Text="Got "+iGot.ToString()+" bytes from password cache.";
                        sParticiple="finished with "+(bGood?"good":"bad")+" result";
                    }
                    else {
                        sParticiple="skipping due to different length encrypted data than expected";
                        mainformNow.tbStatus.Text=("last password cache failed, so cancelled load");
                    }
                }
                else mainformNow.tbStatus.Text="no password was found. Some operations may require a password in the Input box.";
            }
            catch (CryptographicException cexn) {
                mainformNow.tbStatus.Text=String.Format("Could not finish decrypting (are you logged in as the same user on the same machine?): {0}", cexn.Message);
            }
            catch (Exception e) {
                mainformNow.tbStatus.Text=String.Format("Could not finish decrypting: {0}", e.Message);
            }
            return bGood;
        }//end LoadTextSecurely
        /// <summary>
        /// Saves in user profile & only allowed to be decrypted as that user
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="File_Name"></param>
        /// <param name="sKey"></param>
        public static void SaveTextSecurely(string plainText, string File_Name, string sKey) {
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
            byte[] entropyBytes = Encoding.Unicode.GetBytes(sKey);
            byte[] encryptedBytes = ProtectedData.Protect(plainBytes, entropyBytes, DataProtectionScope.CurrentUser);
            string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string SecretFileNow_FullName = myDocs + @"\"+File_Name;
            // Delete the file if it exists
            if (File.Exists(SecretFileNow_FullName)) File.Delete(SecretFileNow_FullName);
            //Create the file and write the bytes. Use of 'using' will close
            // the stream at the end of the block
            using (FileStream fs = File.Create(SecretFileNow_FullName)) {
                fs.Write(encryptedBytes, 0, encryptedBytes.Length);
            }
        }//end SaveTextSecurely
        void DoLoadPassword() {
            string sSalt="0";
            sSalt+=";";
            sSalt+="1000";
            string sKey=Environment.UserName;
            sKey=sSalt+sKey;
            Application.DoEvents();
            string sPrevMsg=this.tbStatus.Text;
            Application.DoEvents();
            try {
                //if (File.Exists(SecretFile_Name)) {
                sParticiple="calling LoadTextSecurely";
                string plainText1="";
                
                sParticiple="getting mydocs SecretFileNow_FullName";
                string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                sParticiple="combining mydocs SecretFileNow_FullName";
                string SecretFileNow_FullName = myDocs + @"\"+SecretFile_Name;
                string SecretFile_TheoreticalFullName="";
                sParticiple="checking for SecretFileNow_Name in alternate location";
                try {
                    SecretFile_TheoreticalFullName=Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), SecretFile_Name);
                    if (File.Exists(SecretFile_TheoreticalFullName)) {
                        SecretFileNow_FullName=SecretFile_TheoreticalFullName;
                    }
                }
                catch {} //don't care
                
                if (File.Exists(SecretFileNow_FullName)) {
                    bool bGood=LoadTextSecurely(out plainText1, SecretFileNow_FullName,sKey);
                    sParticiple="finished calling LoadTextSecurely";
                    if (bGood) {
                        if (plainText1!=null) {
                            if (plainText1.Length==0) this.tbStatus.Text="Warning: saved password is 0 characters.";
                            this.tbInput0.Text=plainText1;
                        }
                        else this.tbStatus.Text="password cache was null for an unknown reason";
                    }
                    else {
                        if (sPrevMsg==this.tbStatus.Text)
                            this.tbStatus.Text="password cache was bad for an unknown reason";
                    }
                }
                else {
                    this.tbStatus.Text="Welcome to ProfileHarvester (no administrator password setting)";
                }
            }
            catch (Exception exn) {
                this.tbStatus.Text="Couldn't finish checking password cache while "+sParticiple+": "+exn.ToString();
                this.tbStatus.Text+=( "; filename:" + ((SecretFile_Name!=null)?("\""+SecretFile_Name+"\""):"<null>") );
            } //do nothing.  It's a secret anyway!
        }
        void SetPasswordToStringInInputBoxToolStripMenuItemClick(object sender, EventArgs e)
        {
            //the original version of code below is from Muhammad Mosa on c-sharpcorner.com <http://www.c-sharpcorner.com/UploadFile/mosessaur/dpapiprotecteddataclass01052006142332PM/dpapiprotecteddataclass.aspx> January 05, 2006. May 16, 2012.
            string plainText=this.tbInput0.Text;
            string sSalt="0";
            sSalt+=";";
            sSalt+="1000";
            string sKey=Environment.UserName;
            sKey=sSalt+sKey;
            SaveTextSecurely(plainText, SecretFile_Name,sKey);
        }
        
        void MainFormLoad(object sender, EventArgs e)
        {
            timer1.Start();
            ArrayList sourceDrivesList=new ArrayList(); //in order of ascending priority
            sourceDrivesList.Add(@"C:\");
            sourceDrivesList.Add(@"K:\");
            this.localProfilesTextBox.Text=@"C:\Users";
            try {
                foreach (string DriveLetterString in sourceDrivesList) {
                    try {
                        if ( !string.IsNullOrEmpty(DriveLetterString)
                            && Directory.Exists(Path.Combine(DriveLetterString,@"Documents and Settings\All Users")) ) {
                            this.localProfilesTextBox.Text=Path.Combine(DriveLetterString,"Documents and Settings");
                            if (this.localProfilesTextBox.Text==@"\Documents and Settings") this.localProfilesTextBox.Text=@"C:\Documents and Settings"; //TODO: find out why this happens
                        }
                        if ( !string.IsNullOrEmpty(DriveLetterString)
                            && Directory.Exists(Path.Combine(DriveLetterString,@"Windows\SysWOW64"))
                             && Directory.Exists(Path.Combine(DriveLetterString,@"Users\Public")) ) {
                            this.localProfilesTextBox.Text=Path.Combine(DriveLetterString,@"\Users");
                            if (this.localProfilesTextBox.Text==@"\Users") this.localProfilesTextBox.Text=@"C:\Users"; //TODO: find out why this happens
                        }
                    }
                    catch {} //don't care
                }
            }
            catch (Exception exn) {
                this.lbMain.Items.Add("REM Couldn't finish checking for profiles folder.");
            }
            ArrayList destProfilesFolderList=new ArrayList(); //in order of ascending priority
            destProfilesFolderList.Add(@"T:\");
            destProfilesFolderList.Add(@"\\FCAFILES\Student\");
            destProfilesFolderList.Add(@"L:\ProfileHarvesterRecovered");
            this.remoteProfilesTextBox.Text=@"";
            try {
                foreach (string DestRootString in destProfilesFolderList) {
                    try {
                        if (Directory.Exists(DestRootString)) {
                            this.remoteProfilesTextBox.Text=DestRootString;
                        }
                    }
                    catch {} //don't care
                }
            }
            catch (Exception exn) {
                this.lbMain.Items.Add("REM Couldn't finish checking for profiles folder.");
            }
        }
        
        void Timer1Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.tbStatus.Text="checking password cache...";
            DoLoadPassword();
        }
        
        void LblInput0Click(object sender, EventArgs e)
        {
        }
        
        void ProfilesLabelClick(object sender, EventArgs e)
        {
            
        }
        
        void LocalProfilesTextBoxTextChanged(object sender, EventArgs e)
        {
            
        }
        
        void MenuStrip1ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }
        
        
        void CreateIfNotOnDestCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            
        }
        
        void MoveLocalTempToNetworkDriveTempToolStripMenuItemClick(object sender, EventArgs e)
        {
            LocalTempToNetworkDriveIfExists(true);
        }
    }//end MainForm
}//end namespace
