/*
 * Created by SharpDevelop.
 * jgustafson
 * Date: 3/14/2012
 * Time: 9:31 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;
//using ExpertMultimedia;

namespace ProfileHarvester {
    /// <summary>
    /// Description of SyncData.
    /// </summary>
    public class PHOpInfo {
        public static string sDirSep=char.ToString(Path.DirectorySeparatorChar);
        public const int MachineNameSource_DoNotUse=0;
        public const int MachineNameSource_TheOneRunningThisProgram=1;
        public const int MachineNameSource_NextLevelSubFolderHas_FromDashComputerDash_ComputerName_OptionalParentheticalNote=2;
        
        public const int OpCode_None=0;
        public const int OpCode_Sync=1;
        public bool bRecursive=true;
        
        public static string RetryBatchFile_FullName=@"C:\tmp\"+MainForm.sMyName+"-retry-MoveLocalDocumentsToNetworkDriveIfExists-"+Environment.MachineName+".bat";//fixed in static constructor
    
        public int MachineNameSource=MachineNameSource_TheOneRunningThisProgram;
        public int OpCode=OpCode_Sync;
        public bool bMove=true;
        public bool bCreateDestEvenIfNoMatchingUser=false;
        public bool bGetMachineName_FromTextAfter_FromDashComputerDash=false;
        public string SourceFolder_CanContainPercentUSERPROFILEPercent="";
        public string DestFolder_CanContainPercentUSERNAMEPercent="";
        public ArrayList alUsersWithFiles=new ArrayList();
        public TextBox tbStatus=null;
        public ListBox lbStatus=null;
        public bool bKeepAndDateOldFile=false;
        public ArrayList mustEndWith_MustBeLowerCase = null;
        static PHOpInfo() {
            bool bChangeIt=false;
            try {
                if (!Directory.Exists(RetryBatchFile_FullName)) {
                    bChangeIt=true;
                }
            }
            catch {
                bChangeIt=true;
            }
            if (bChangeIt) {
                RetryBatchFile_FullName=Path.GetTempPath()+sDirSep+MainForm.sMyName+"-retry-MoveLocalDocumentsToNetworkDriveIfExists-"+Environment.MachineName+".bat";
            }
        }
        public PHOpInfo() {
            Console.Error.WriteLine("Warning: PHOpInfo default constructor was used--it is possible not all variables are set.");
        }
        public static string ToOneLine(string sMsg) {
            if (sMsg!=null) {
                sMsg=sMsg.Replace("\r\n"," ").Replace("\r"," ").Replace("\n"," ");
            }
            else sMsg="";
            return sMsg;
        }
        public bool IsAllowedFile(string filename) {
            bool bAllowed=(this.mustEndWith_MustBeLowerCase!=null)?false:true;
            if (this.mustEndWith_MustBeLowerCase!=null) {
                foreach (string allowedEnding in this.mustEndWith_MustBeLowerCase) {
                    string allowedEnding_ToLower=allowedEnding.ToLower();
                    if (filename.EndsWith(allowedEnding_ToLower)) {
                        bAllowed=true;
                        break;
                    }
                }
            }
            return bAllowed;
        }
        public void UpdateStatus(string sMsg, bool bForceRefresh) {
            if (tbStatus!=null) {
                tbStatus.Text=sMsg;
                if (lbStatus!=null) {
                    lbStatus.Items.Add("REM "+ToOneLine(sMsg));
                }
            }
            if (bForceRefresh) Application.DoEvents();
        }
        public PHOpInfo(int iOpCode, bool set_bRecursive, string sSourceFolder_CanContainPercentUSERPROFILEPercent, string sDestFolder_CanContainPercentUSERNAMEPercent, bool bMove_ElseCopyIfAnyToDoForThisOpCode, TextBox tbStatus_ElseNull, bool KeepAndDateOldFile, bool CreateDestEvenIfNoMatchingUser, ArrayList set_mustEndWith_MustBeLowerCase) {
            OpCode=iOpCode;
            bRecursive=set_bRecursive;
            SourceFolder_CanContainPercentUSERPROFILEPercent=sSourceFolder_CanContainPercentUSERPROFILEPercent;
            DestFolder_CanContainPercentUSERNAMEPercent=sDestFolder_CanContainPercentUSERNAMEPercent;
            bMove=bMove_ElseCopyIfAnyToDoForThisOpCode;
            tbStatus=tbStatus_ElseNull;
            bKeepAndDateOldFile=KeepAndDateOldFile;
            bCreateDestEvenIfNoMatchingUser=CreateDestEvenIfNoMatchingUser;
            mustEndWith_MustBeLowerCase=set_mustEndWith_MustBeLowerCase;
        }
    }
}
